using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace dosi
{
    public class ChiTietForm : Form
    {
        private readonly string ConnectionString = "Data Source=QuanLyKho.db";
        private readonly DateTime _ngay;
        private FlowLayoutPanel flpInvoices = null!;

        public ChiTietForm(DateTime ngay)
        {
            _ngay = ngay;
            BuildUI();
            this.Load += (s, e) => TaiDuLieu();
        }

        private void BuildUI()
        {
            this.Text = $"Chi Tiết Hóa Đơn — {_ngay:dd/MM/yyyy}";
            this.Size = new Size(1000, 800);
            this.MinimumSize = new Size(650, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(241, 245, 249);
            this.MinimizeBox = false;
            this.MaximizeBox = true;
            this.FormBorderStyle = FormBorderStyle.Sizable;

            var accentStrip = new Panel
            {
                Dock = DockStyle.Top,
                Height = 4,
                BackColor = Color.FromArgb(99, 102, 241),
            };

            var titleBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 56,
                BackColor = Color.White,
                Padding = new Padding(20, 0, 0, 0),
            };
            titleBar.Controls.Add(new Label
            {
                Text = $"Hóa Đơn Ngày {_ngay:dd/MM/yyyy}",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
            });

            var bottomBar = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 56,
                BackColor = Color.White,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(8, 10, 8, 0),
            };
            var btnClose = new Button
            {
                Text = "Đóng",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(99, 102, 241),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 34),
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.Cancel,
            };
            btnClose.FlatAppearance.BorderSize = 0;
            this.CancelButton = btnClose;
            bottomBar.Controls.Add(btnClose);

            flpInvoices = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.FromArgb(241, 245, 249),
                Padding = new Padding(16, 12, 4, 12),
            };

            this.Controls.Add(flpInvoices);
            this.Controls.Add(bottomBar);
            this.Controls.Add(titleBar);
            this.Controls.Add(accentStrip);
        }

        private void TaiDuLieu()
        {
            var tuNgay = _ngay;
            var denNgay = _ngay.AddDays(1);

            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();

            var rows = conn.Query(@"
                SELECT
                    nx.hoadon_id                        AS HoaDonId,
                    nx.KHACH_id                         AS KhachId,
                    hd.ma_hd                            AS MaHD,
                    hd.ngay_tao                         AS ThoiGian,
                    hd.tong_tien                        AS TongTien,
                    COALESCE(k.ho_ten, '(Khách lẻ)')   AS KhachHang,
                    sp.ten_sp                           AS TenSP,
                    nx.so_luong                         AS SoLuong,
                    sp.gia_ban                          AS DonGia,
                    nx.so_luong * sp.gia_ban            AS ThanhTien
                FROM NhapXuat nx
                JOIN SanPham sp ON nx.SAPHAM_id = sp.id
                LEFT JOIN Khach k ON nx.KHACH_id = k.id
                LEFT JOIN HoaDon hd ON nx.hoadon_id = hd.id
                WHERE nx.loai_giao_dich = 'Xuat'
                  AND nx.ngay_tao >= @tuNgay AND nx.ngay_tao < @denNgay
                ORDER BY nx.hoadon_id ASC, nx.id ASC",
                new { tuNgay, denNgay }).ToList();

            var invoiceGroups = rows
                .GroupBy(r => r.HoaDonId?.ToString()
                    ?? (r.KhachId?.ToString() ?? "") + "_" + (r.ThoiGian?.ToString() ?? ""))
                .ToList();

            int cardWidth = flpInvoices.Width
                - flpInvoices.Padding.Left
                - flpInvoices.Padding.Right
                - SystemInformation.VerticalScrollBarWidth
                - 4;
            if (cardWidth < 300) cardWidth = 700;

            foreach (var group in invoiceGroups)
            {
                var items = group.ToList();

                decimal totalAmount = items[0].TongTien != null
                    ? Convert.ToDecimal(items[0].TongTien)
                    : items.Sum(r => Convert.ToDecimal(r.ThanhTien ?? 0));

                string customerName = items[0].KhachHang?.ToString() ?? "(Khách lẻ)";
                string maHD = items[0].MaHD?.ToString() ?? "---";
                string timeStr = DateTime.TryParse(items[0].ThoiGian?.ToString(), out DateTime hdt)
                    ? hdt.ToString("HH:mm:ss")
                    : "";

                const int accentH = 3;
                const int headerH = 72;
                const int colHeaderH = 36;
                const int rowH = 34;
                int dgvH = colHeaderH + items.Count * rowH;
                int cardH = accentH + headerH + dgvH;

                var card = new Panel
                {
                    Width = cardWidth,
                    Height = cardH,
                    BackColor = Color.White,
                    Margin = new Padding(0, 0, 0, 14),
                };

                var cardAccent = new Panel
                {
                    Dock = DockStyle.Top,
                    Height = accentH,
                    BackColor = Color.FromArgb(99, 102, 241),
                };

                var headerTlp = new TableLayoutPanel
                {
                    Dock = DockStyle.Top,
                    Height = headerH,
                    ColumnCount = 2,
                    RowCount = 2,
                    BackColor = Color.FromArgb(248, 250, 252),
                    Padding = new Padding(16, 8, 16, 8),
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                };
                headerTlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62));
                headerTlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38));
                headerTlp.RowStyles.Add(new RowStyle(SizeType.Percent, 48));
                headerTlp.RowStyles.Add(new RowStyle(SizeType.Percent, 52));

                headerTlp.Controls.Add(new Label
                {
                    Text = maHD,
                    Font = new Font("Segoe UI", 8, FontStyle.Bold),
                    ForeColor = Color.FromArgb(99, 102, 241),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.BottomLeft,
                }, 0, 0);

                headerTlp.Controls.Add(new Label
                {
                    Text = customerName,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = Color.FromArgb(30, 41, 59),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.TopLeft,
                }, 0, 1);

                headerTlp.Controls.Add(new Label
                {
                    Text = timeStr,
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.FromArgb(148, 163, 184),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.BottomRight,
                }, 1, 0);

                headerTlp.Controls.Add(new Label
                {
                    Text = totalAmount.ToString("N0") + " ₫",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.FromArgb(99, 102, 241),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.TopRight,
                }, 1, 1);

                var dgv = BuildItemGrid();
                dgv.Dock = DockStyle.Fill;

                var dt = new DataTable();
                dt.Columns.Add("Sản Phẩm");
                dt.Columns.Add("Số Lượng");
                dt.Columns.Add("Đơn Giá");
                dt.Columns.Add("Thành Tiền");

                foreach (var r in items)
                    dt.Rows.Add(
                        r.TenSP?.ToString() ?? "",
                        r.SoLuong?.ToString() ?? "0",
                        Convert.ToDecimal(r.DonGia ?? 0).ToString("N0") + " ₫",
                        Convert.ToDecimal(r.ThanhTien ?? 0).ToString("N0") + " ₫");

                dgv.DataSource = dt;
                foreach (DataGridViewColumn col in dgv.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgv.ClearSelection();

                card.Controls.Add(dgv);
                card.Controls.Add(headerTlp);
                card.Controls.Add(cardAccent);
                flpInvoices.Controls.Add(card);
            }

            if (!invoiceGroups.Any())
                flpInvoices.Controls.Add(new Label
                {
                    Text = "Không có giao dịch nào trong ngày này.",
                    Font = new Font("Segoe UI", 11),
                    ForeColor = Color.FromArgb(148, 163, 184),
                    Width = cardWidth,
                    Height = 60,
                    TextAlign = ContentAlignment.MiddleCenter,
                });
        }

        private DataGridView BuildItemGrid()
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
                AllowUserToResizeRows = false,
                AllowUserToOrderColumns = false,
                ScrollBars = ScrollBars.None,
            };
            dgv.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(238, 242, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 250, 252);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(100, 116, 139);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            dgv.RowTemplate.Height = 34;
            return dgv;
        }
    }
}
