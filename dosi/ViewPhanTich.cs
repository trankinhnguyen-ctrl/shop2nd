using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace dosi
{
    public class ViewPhanTich : UserControl
    {
        private readonly string ConnectionString = "Data Source=QuanLyKho.db";

        private DateTimePicker dtpFrom = null!;
        private DateTimePicker dtpTo = null!;
        private Label lblKpiRevenue = null!;
        private Label lblKpiProfit = null!;
        private Label lblKpiOrders = null!;
        private Label lblKpiItems = null!;
        private Chart chartRevenue = null!;
        private Panel panelChartContainer = null!;
        private DataGridView dgvTopProducts = null!;
        private DataGridView dgvTopCustomers = null!;
        private Button _btnTatCa = null!;
        private bool _tatCa = false;

        public ViewPhanTich()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.FromArgb(241, 245, 249);
            this.Padding = new Padding(20);
            BuildUI();
            this.Load += ViewPhanTich_Load;
        }

        private void ViewPhanTich_Load(object? sender, EventArgs e)
        {
            InitChart();
            TaiDuLieu();
        }

        private void BuildUI()
        {
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1,
                BackColor = Color.Transparent,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
            };
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 72));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 108));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
            this.Controls.Add(mainLayout);

            mainLayout.Controls.Add(BuildFilterPanel(), 0, 0);
            mainLayout.Controls.Add(BuildKpiRow(), 0, 1);
            mainLayout.Controls.Add(BuildChartSection(), 0, 2);
            mainLayout.Controls.Add(BuildListsSection(), 0, 3);
        }

        private Panel BuildFilterPanel()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };

            var lblTitle = new Label
            {
                Text = "Phân Tích & Báo Cáo",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                AutoSize = true,
                Location = new Point(0, 0),
            };

            var lblFrom = new Label
            {
                Text = "Từ ngày:",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(100, 116, 139),
                AutoSize = true,
                Location = new Point(0, 44),
            };

            dtpFrom = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today.AddDays(-30),
                Width = 130,
                Location = new Point(70, 40),
                Font = new Font("Segoe UI", 9),
            };

            var lblTo = new Label
            {
                Text = "Đến ngày:",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(100, 116, 139),
                AutoSize = true,
                Location = new Point(214, 44),
            };

            dtpTo = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today,
                Width = 130,
                Location = new Point(293, 40),
                Font = new Font("Segoe UI", 9),
            };

            var btnFilter = new Button
            {
                Text = "Lọc",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(99, 102, 241),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(80, 28),
                Location = new Point(437, 40),
                Cursor = Cursors.Hand,
            };
            btnFilter.FlatAppearance.BorderSize = 0;
            btnFilter.Click += (s, e) =>
            {
                _tatCa = false;
                _btnTatCa.BackColor = Color.White;
                _btnTatCa.ForeColor = Color.FromArgb(99, 102, 241);
                TaiDuLieu();
            };

            _btnTatCa = new Button
            {
                Text = "Tất cả",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(99, 102, 241),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(80, 28),
                Location = new Point(525, 40),
                Cursor = Cursors.Hand,
            };
            _btnTatCa.FlatAppearance.BorderSize = 1;
            _btnTatCa.FlatAppearance.BorderColor = Color.FromArgb(99, 102, 241);
            _btnTatCa.Click += (s, e) =>
            {
                _tatCa = true;
                _btnTatCa.BackColor = Color.FromArgb(99, 102, 241);
                _btnTatCa.ForeColor = Color.White;
                TaiDuLieu();
            };

            panel.Controls.AddRange(new Control[] { lblTitle, lblFrom, dtpFrom, lblTo, dtpTo, btnFilter, _btnTatCa });
            return panel;
        }

        private TableLayoutPanel BuildKpiRow()
        {
            var tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 0, 0, 10),
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var specs = new (string title, string initVal, Color accent)[]
            {
                ("Doanh Thu", "0 ₫", Color.FromArgb(99, 102, 241)),
                ("Lợi Nhuận", "0 ₫", Color.FromArgb(16, 185, 129)),
                ("Số Đơn Hàng", "0", Color.FromArgb(245, 158, 11)),
                ("Sản Phẩm Đã Bán", "0", Color.FromArgb(239, 68, 68)),
            };

            Label[] kpiLabels = new Label[4];

            for (int i = 0; i < 4; i++)
            {
                var (title, initVal, accent) = specs[i];
                int col = i;

                var card = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.White,
                    Margin = new Padding(col > 0 ? 8 : 0, 0, col < 3 ? 8 : 0, 0),
                };

                var accentStrip = new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 4,
                    BackColor = accent,
                };

                var lblTitle = new Label
                {
                    Text = title,
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    Dock = DockStyle.Top,
                    Height = 30,
                    Padding = new Padding(16, 10, 0, 0),
                };

                var lblVal = new Label
                {
                    Text = initVal,
                    Font = new Font("Segoe UI", 16, FontStyle.Bold),
                    ForeColor = accent,
                    Dock = DockStyle.Fill,
                    Padding = new Padding(16, 0, 0, 0),
                    TextAlign = ContentAlignment.MiddleLeft,
                };

                kpiLabels[i] = lblVal;

                card.Controls.Add(lblVal);
                card.Controls.Add(lblTitle);
                card.Controls.Add(accentStrip);
                tlp.Controls.Add(card, i, 0);
            }

            lblKpiRevenue = kpiLabels[0];
            lblKpiProfit = kpiLabels[1];
            lblKpiOrders = kpiLabels[2];
            lblKpiItems = kpiLabels[3];
            return tlp;
        }

        private Panel BuildChartSection()
        {
            var wrapper = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 10),
                Padding = new Padding(16, 8, 16, 8),
            };

            var lblChartTitle = new Label
            {
                Text = "Doanh Thu Theo Ngày",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Dock = DockStyle.Top,
                Height = 32,
            };

            panelChartContainer = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };

            wrapper.Controls.Add(panelChartContainer);
            wrapper.Controls.Add(lblChartTitle);
            return wrapper;
        }

        private void InitChart()
        {
            chartRevenue = new Chart { Dock = DockStyle.Fill, BackColor = Color.White };
            ((System.ComponentModel.ISupportInitialize)chartRevenue).BeginInit();

            chartRevenue.BorderlineColor = Color.Transparent;
            chartRevenue.BorderlineDashStyle = ChartDashStyle.NotSet;

            var area = new ChartArea("main") { BackColor = Color.White, BorderColor = Color.Transparent };
            area.AxisX.MajorGrid.LineColor = Color.FromArgb(226, 232, 240);
            area.AxisX.LineColor = Color.FromArgb(203, 213, 225);
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8);
            area.AxisX.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(226, 232, 240);
            area.AxisY.LineColor = Color.FromArgb(203, 213, 225);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8);
            area.AxisY.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            chartRevenue.ChartAreas.Add(area);

            var series = new Series("DoanhThu")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.FromArgb(99, 102, 241),
                BorderWidth = 3,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 7,
                MarkerColor = Color.FromArgb(99, 102, 241),
                XValueType = ChartValueType.DateTime,
                ChartArea = "main",
            };
            chartRevenue.Series.Add(series);
            chartRevenue.Legends.Clear();

            ((System.ComponentModel.ISupportInitialize)chartRevenue).EndInit();

            chartRevenue.MouseClick += ChartRevenue_MouseClick;
            chartRevenue.MouseMove += (s, e) =>
            {
                var hit = chartRevenue.HitTest(e.X, e.Y);
                chartRevenue.Cursor = hit.ChartElementType == ChartElementType.DataPoint
                    ? Cursors.Hand
                    : Cursors.Default;
            };

            panelChartContainer.Controls.Add(chartRevenue);
        }

        private void ChartRevenue_MouseClick(object? sender, MouseEventArgs e)
        {
            var hit = chartRevenue.HitTest(e.X, e.Y);
            if (hit.ChartElementType != ChartElementType.DataPoint) return;
            var point = chartRevenue.Series["DoanhThu"].Points[hit.PointIndex];
            var date = DateTime.FromOADate(point.XValue).Date;
            using var dlg = new ChiTietForm(date);
            dlg.ShowDialog(this);
        }

        private TableLayoutPanel BuildListsSection()
        {
            var tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            dgvTopProducts = BuildStyledGrid();
            dgvTopCustomers = BuildStyledGrid();

            tlp.Controls.Add(BuildListCard("Top 5 Sản Phẩm Bán Chạy", dgvTopProducts, new Padding(0, 0, 8, 0)), 0, 0);
            tlp.Controls.Add(BuildListCard("Top 5 Khách Hàng Thân Thiết", dgvTopCustomers, new Padding(8, 0, 0, 0)), 1, 0);
            return tlp;
        }

        private Panel BuildListCard(string title, DataGridView grid, Padding margin)
        {
            var card = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Margin = margin };

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Dock = DockStyle.Top,
                Height = 40,
                Padding = new Padding(16, 12, 0, 0),
            };

            grid.Dock = DockStyle.Fill;
            card.Controls.Add(grid);
            card.Controls.Add(lblTitle);
            return card;
        }

        private DataGridView BuildStyledGrid()
        {
            var dgv = new DataGridView
            {
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(241, 245, 249),
                BackgroundColor = Color.White,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9),
                ColumnHeadersHeight = 36,
                EnableHeadersVisualStyles = false,
            };
            dgv.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(238, 242, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(100, 116, 139);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            dgv.RowTemplate.Height = 36;
            return dgv;
        }

        private void TaiDuLieu()
        {
            DateTime tuNgay = _tatCa ? new DateTime(1900, 1, 1) : dtpFrom.Value.Date;
            DateTime denNgay = _tatCa ? new DateTime(2100, 1, 1) : dtpTo.Value.Date.AddDays(1);

            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();

            decimal tongDoanhThu = conn.ExecuteScalar<decimal>(@"
                SELECT COALESCE(SUM(nx.so_luong * sp.gia_ban), 0)
                FROM NhapXuat nx JOIN SanPham sp ON nx.SAPHAM_id = sp.id
                LEFT JOIN HoaDon hd ON nx.hoadon_id = hd.id
                WHERE nx.loai_giao_dich = 'Xuat'
                  AND nx.ngay_tao >= @tuNgay AND nx.ngay_tao < @denNgay
                  AND (nx.hoadon_id IS NULL OR hd.TrangThai != 'DaChinhSua')",
                new { tuNgay, denNgay });
            lblKpiRevenue.Text = tongDoanhThu.ToString("N0") + " ₫";

            decimal loiNhuan = conn.ExecuteScalar<decimal>(@"
                SELECT COALESCE(SUM(nx.so_luong * (sp.gia_ban - COALESCE(sp.gia_von, 0))), 0)
                FROM NhapXuat nx JOIN SanPham sp ON nx.SAPHAM_id = sp.id
                LEFT JOIN HoaDon hd ON nx.hoadon_id = hd.id
                WHERE nx.loai_giao_dich = 'Xuat'
                  AND nx.ngay_tao >= @tuNgay AND nx.ngay_tao < @denNgay
                  AND (nx.hoadon_id IS NULL OR hd.TrangThai != 'DaChinhSua')",
                new { tuNgay, denNgay });
            lblKpiProfit.Text = loiNhuan.ToString("N0") + " ₫";

            int tongDon = conn.ExecuteScalar<int>(@"
                SELECT COUNT(DISTINCT COALESCE(CAST(nx.hoadon_id AS TEXT), CAST(nx.KHACH_id AS TEXT) || '_' || nx.ngay_tao))
                FROM NhapXuat nx
                LEFT JOIN HoaDon hd ON nx.hoadon_id = hd.id
                WHERE nx.loai_giao_dich = 'Xuat'
                  AND nx.ngay_tao >= @tuNgay AND nx.ngay_tao < @denNgay
                  AND (nx.hoadon_id IS NULL OR hd.TrangThai != 'DaChinhSua')",
                new { tuNgay, denNgay });
            lblKpiOrders.Text = tongDon.ToString("N0");

            int tongSanPham = conn.ExecuteScalar<int>(@"
                SELECT COALESCE(SUM(nx.so_luong), 0)
                FROM NhapXuat nx
                LEFT JOIN HoaDon hd ON nx.hoadon_id = hd.id
                WHERE nx.loai_giao_dich = 'Xuat'
                  AND nx.ngay_tao >= @tuNgay AND nx.ngay_tao < @denNgay
                  AND (nx.hoadon_id IS NULL OR hd.TrangThai != 'DaChinhSua')",
                new { tuNgay, denNgay });
            lblKpiItems.Text = tongSanPham.ToString("N0");

            chartRevenue.Series["DoanhThu"].Points.Clear();
            var chartRows = conn.Query(@"
                SELECT DATE(nx.ngay_tao) AS Ngay, SUM(nx.so_luong * sp.gia_ban) AS DoanhThu
                FROM NhapXuat nx JOIN SanPham sp ON nx.SAPHAM_id = sp.id
                LEFT JOIN HoaDon hd ON nx.hoadon_id = hd.id
                WHERE nx.loai_giao_dich = 'Xuat'
                  AND nx.ngay_tao >= @tuNgay AND nx.ngay_tao < @denNgay
                  AND (nx.hoadon_id IS NULL OR hd.TrangThai != 'DaChinhSua')
                GROUP BY DATE(nx.ngay_tao)
                ORDER BY Ngay ASC",
                new { tuNgay, denNgay }).ToList();

            foreach (var row in chartRows)
            {
                if (DateTime.TryParse(row.Ngay?.ToString(), out DateTime dt))
                    chartRevenue.Series["DoanhThu"].Points.AddXY(dt, Convert.ToDouble(row.DoanhThu ?? 0));
            }

            var topProds = conn.Query(@"
                SELECT sp.ten_sp AS TenSP, SUM(nx.so_luong) AS SoLuong, SUM(nx.so_luong * sp.gia_ban) AS DoanhThu
                FROM NhapXuat nx JOIN SanPham sp ON nx.SAPHAM_id = sp.id
                LEFT JOIN HoaDon hd ON nx.hoadon_id = hd.id
                WHERE nx.loai_giao_dich = 'Xuat'
                  AND nx.ngay_tao >= @tuNgay AND nx.ngay_tao < @denNgay
                  AND (nx.hoadon_id IS NULL OR hd.TrangThai != 'DaChinhSua')
                GROUP BY sp.id
                ORDER BY SoLuong DESC
                LIMIT 5",
                new { tuNgay, denNgay }).ToList();

            var dtProds = new DataTable();
            dtProds.Columns.Add("Sản Phẩm");
            dtProds.Columns.Add("Số Lượng");
            dtProds.Columns.Add("Doanh Thu");
            foreach (var r in topProds)
                dtProds.Rows.Add(
                    r.TenSP?.ToString() ?? "",
                    r.SoLuong?.ToString() ?? "0",
                    Convert.ToDecimal(r.DoanhThu ?? 0).ToString("N0") + " ₫");
            dgvTopProducts.DataSource = dtProds;

            var topCusts = conn.Query(@"
                SELECT k.ho_ten AS HoTen, SUM(nx.so_luong) AS SoLuong, SUM(nx.so_luong * sp.gia_ban) AS TongChi
                FROM NhapXuat nx
                JOIN Khach k ON nx.KHACH_id = k.id
                JOIN SanPham sp ON nx.SAPHAM_id = sp.id
                LEFT JOIN HoaDon hd ON nx.hoadon_id = hd.id
                WHERE nx.loai_giao_dich = 'Xuat'
                  AND nx.ngay_tao >= @tuNgay AND nx.ngay_tao < @denNgay
                  AND (nx.hoadon_id IS NULL OR hd.TrangThai != 'DaChinhSua')
                GROUP BY k.id
                ORDER BY TongChi DESC
                LIMIT 5",
                new { tuNgay, denNgay }).ToList();

            var dtCusts = new DataTable();
            dtCusts.Columns.Add("Khách Hàng");
            dtCusts.Columns.Add("Số Lượng");
            dtCusts.Columns.Add("Tổng Chi");
            foreach (var r in topCusts)
                dtCusts.Rows.Add(
                    r.HoTen?.ToString() ?? "",
                    r.SoLuong?.ToString() ?? "0",
                    Convert.ToDecimal(r.TongChi ?? 0).ToString("N0") + " ₫");
            dgvTopCustomers.DataSource = dtCusts;
        }
    }
}
