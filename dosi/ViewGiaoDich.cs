using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace dosi
{
    public partial class ViewGiaoDich : UserControl
    {
        private string ConnectionString = "Data Source=QuanLyKho.db";
        private DataTable gioHang = new DataTable();
        private int? _selectedPhanLoaiId = null;

        private static readonly Color[] _pillColors = {
            Color.FromArgb(236, 72, 153),
            Color.FromArgb(245, 158, 11),
            Color.FromArgb(16, 185, 129),
            Color.FromArgb(239, 68, 68),
            Color.FromArgb(14, 165, 233),
            Color.FromArgb(168, 85, 247),
            Color.FromArgb(20, 184, 166),
            Color.FromArgb(234, 88, 12),
        };

        public ViewGiaoDich()
        {
            InitializeComponent();

            // Kích hoạt Double-Buffered chống nhấp nháy khi render danh sách sản phẩm
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            KhoiTaoGioHang();
            StyleCartDataGridView(); // Định hình giao diện phẳng hiện đại cho giỏ hàng

            this.Load += ViewGiaoDich_Load;
            this.Resize += (s, e) => { LoadSanPham(txtSearchProduct.Text); };
            txtSearchProduct.TextChanged += TxtSearchProduct_TextChanged;

            txtPhone.Leave += TxtPhone_Leave;
            btnThanhToan.Click += BtnThanhToan_Click;

            dgvCart.CellValidating += dgvCart_CellValidating;
            dgvCart.CellValueChanged += dgvCart_CellValueChanged;
            dgvCart.RowsRemoved += dgvCart_RowsRemoved;
            dgvCart.EditingControlShowing += dgvCart_EditingControlShowing;

            dgvCart.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        private void StyleCartDataGridView()
        {
            dgvCart.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvCart.BackgroundColor = Color.White;
            dgvCart.GridColor = Color.FromArgb(241, 245, 249);
            dgvCart.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.MultiSelect = false;
            dgvCart.EnableHeadersVisualStyles = false;

            dgvCart.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCart.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            dgvCart.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(100, 116, 139);
            dgvCart.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dgvCart.ColumnHeadersHeight = 32;

            dgvCart.RowTemplate.Height = 32;
            dgvCart.DefaultCellStyle.SelectionBackColor = Color.FromArgb(238, 242, 255);
            dgvCart.DefaultCellStyle.SelectionForeColor = Color.FromArgb(79, 70, 229);
            dgvCart.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvCart.DefaultCellStyle.ForeColor = Color.FromArgb(51, 65, 85);
        }

        private void TxtPhone_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhone.Text)) return;

            try
            {
                using (var conn = new SqliteConnection(ConnectionString))
                {
                    conn.Open();
                    var khach = conn.QueryFirstOrDefault("SELECT ho_ten AS Ten, dia_chi AS DiaChi FROM Khach WHERE so_dien_thoai = @sdt", new { sdt = txtPhone.Text });

                    if (khach != null)
                    {
                        txtName.Text = khach.Ten;
                        txtAddress.Text = khach.DiaChi;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm khách hàng: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvCart_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvCart.Columns[e.ColumnIndex].Name == "SoLuong")
            {
                string? maSP = dgvCart.Rows[e.RowIndex].Cells["MaSP"].Value?.ToString();
                if (string.IsNullOrEmpty(maSP)) return;

                string input = e.FormattedValue?.ToString() ?? "";
                int maxStock = LayTonKhoThucTe(maSP);

                if (!int.TryParse(input, out int inputQty) || inputQty < 1)
                {
                    if (dgvCart.EditingControl is TextBox tb) tb.Text = "1";
                }
                else if (inputQty > maxStock)
                {
                    MessageBox.Show("Kho chỉ còn " + maxStock + " sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (dgvCart.EditingControl is TextBox tb) tb.Text = maxStock.ToString();
                }
            }
        }

        private void BtnThanhToan_Click(object? sender, EventArgs e)
        {
            if (gioHang.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var conn = new SqliteConnection(ConnectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        var khId = conn.ExecuteScalar<int?>(
                            "SELECT id FROM Khach WHERE so_dien_thoai = @sdt",
                            new { sdt = txtPhone.Text },
                            transaction: trans);

                        if (khId == null)
                        {
                            khId = conn.ExecuteScalar<int>(
                                "INSERT INTO Khach (ho_ten, so_dien_thoai, dia_chi) VALUES (@ten, @sdt, @dc); SELECT last_insert_rowid();",
                                new { ten = txtName.Text, sdt = txtPhone.Text, dc = txtAddress.Text },
                                transaction: trans);
                        }
                        else
                        {
                            conn.Execute(
                                "UPDATE Khach SET ho_ten = @ten, dia_chi = @dc WHERE id = @id",
                                new { ten = txtName.Text, dc = txtAddress.Text, id = khId },
                                transaction: trans);
                        }

                        string thoiGianTao = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        foreach (DataRow row in gioHang.Rows)
                        {
                            if (row.RowState == DataRowState.Deleted) continue;

                            string maSP = row["MaSP"].ToString()!;
                            int slBan = Convert.ToInt32(row["SoLuong"]);

                            var sanPhamInfo = conn.QueryFirstOrDefault(
                                "SELECT id, gia_ban FROM SanPham WHERE ma_sp = @ma",
                                new { ma = maSP },
                                transaction: trans);

                            if (sanPhamInfo == null) throw new Exception("Không tìm thấy sản phẩm trên hệ thống: " + maSP);

                            conn.Execute(
                                @"INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu) 
                                  VALUES (@spId, @khId, 'Xuat', @sl, @ngay, @note)",
                                new
                                {
                                    spId = sanPhamInfo.id,
                                    khId = khId,
                                    sl = slBan,
                                    ngay = thoiGianTao,
                                    note = "Bán hàng trực tiếp: " + row["TenSP"]
                                },
                                transaction: trans);

                            int rowsAffected = conn.Execute(
                                "UPDATE SanPham SET so_luong_ton = so_luong_ton - @sl WHERE id = @spId AND so_luong_ton >= @sl",
                                new { sl = slBan, spId = sanPhamInfo.id },
                                transaction: trans);

                            if (rowsAffected == 0)
                            {
                                throw new Exception("Sản phẩm " + row["TenSP"] + " vừa hết hoặc không đủ số lượng tồn kho thực tế!");
                            }
                        }

                        trans.Commit();
                        MessageBox.Show("Thanh toán đơn hàng và cập nhật dữ liệu kho thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LamMoiSauThanhToan();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Hệ thống đã hủy giao dịch do xuất hiện lỗi: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LamMoiSauThanhToan()
        {
            gioHang.Clear();
            txtPhone.Clear();
            txtName.Clear();
            txtAddress.Clear();
            lblTongTien.Text = "Tổng: 0đ";
            LoadSanPham();
        }

        private void dgvCart_EditingControlShowing(object? sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvCart.CurrentCell.ColumnIndex == dgvCart.Columns["SoLuong"].Index && e.Control is TextBox tb)
            {
                tb.TextChanged -= TextBox_RealtimeUpdate;
                tb.TextChanged += TextBox_RealtimeUpdate;

                tb.BeginInvoke(new Action(() => {
                    tb.SelectionStart = tb.Text.Length;
                    tb.SelectionLength = 0;
                }));
            }
        }

        private void TextBox_RealtimeUpdate(object? sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (dgvCart.CurrentRow == null) return;
                string? maSP = dgvCart.CurrentRow.Cells["MaSP"].Value?.ToString();
                if (string.IsNullOrEmpty(maSP)) return;

                int maxStock = LayTonKhoThucTe(maSP);

                if (int.TryParse(tb.Text, out int inputQty))
                {
                    if (inputQty > maxStock)
                    {
                        tb.Text = maxStock.ToString();
                        tb.SelectionStart = tb.Text.Length;
                    }
                }
                TinhTongTienTuGiaoDien(tb.Text);
            }
        }

        private void TinhTongTienTuGiaoDien(string currentEditingText)
        {
            decimal tong = 0;
            if (dgvCart.CurrentCell == null) return;
            int editingRowIndex = dgvCart.CurrentCell.RowIndex;

            foreach (DataGridViewRow r in dgvCart.Rows)
            {
                if (r.IsNewRow) continue;
                int sl = 0;
                if (r.Index == editingRowIndex) int.TryParse(currentEditingText, out sl);
                else sl = r.Cells["SoLuong"].Value == DBNull.Value ? 0 : Convert.ToInt32(r.Cells["SoLuong"].Value);

                decimal gia = r.Cells["DonGia"].Value == DBNull.Value ? 0 : Convert.ToDecimal(r.Cells["DonGia"].Value);
                tong += sl * gia;
            }
            lblTongTien.Text = "Tổng: " + tong.ToString("N0") + "đ";
        }

        private void TinhTongTien()
        {
            // Lấy các dòng hiện tại (bỏ qua các dòng trạng thái Deleted)
            decimal tong = gioHang.AsEnumerable()
                .Where(row => row.RowState != DataRowState.Deleted && row.RowState != DataRowState.Detached)
                .Sum(row => (row["SoLuong"] == DBNull.Value ? 0 : Convert.ToInt32(row["SoLuong"])) *
                            (row["DonGia"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DonGia"])));

            lblTongTien.Text = "Tổng: " + tong.ToString("N0") + "đ";
        }

        private void dgvCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvCart.BeginEdit(true);
            }
        }

        private void dgvCart_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) TinhTongTien();
        }

        private void dgvCart_RowsRemoved(object? sender, DataGridViewRowsRemovedEventArgs e)
        {
            TinhTongTien();
        }

        private int LayTonKhoThucTe(string maSP)
        {
            try
            {
                using (var conn = new SqliteConnection(ConnectionString))
                {
                    conn.Open();
                    return conn.ExecuteScalar<int>("SELECT so_luong_ton FROM SanPham WHERE ma_sp = @ma", new { ma = maSP });
                }
            }
            catch { return 0; }
        }

        private void KhoiTaoGioHang()
        {
            if (gioHang.Columns.Count == 0)
            {
                gioHang.Columns.Add("MaSP", typeof(string));
                gioHang.Columns.Add("TenSP", typeof(string));
                gioHang.Columns.Add("SoLuong", typeof(int));
                gioHang.Columns.Add("DonGia", typeof(decimal));
                gioHang.Columns.Add("ThanhTien", typeof(decimal), "SoLuong * DonGia");
            }

            dgvCart.DataSource = gioHang;

            if (!dgvCart.Columns.Contains("btnXoa"))
            {
                DataGridViewButtonColumn btnXoa = new DataGridViewButtonColumn();
                btnXoa.Name = "btnXoa";
                btnXoa.HeaderText = "Thao tác";
                btnXoa.Text = "Xóa";
                btnXoa.UseColumnTextForButtonValue = true;
                dgvCart.Columns.Add(btnXoa);
            }

            dgvCart.Columns["MaSP"].ReadOnly = true;
            dgvCart.Columns["TenSP"].ReadOnly = true;
            dgvCart.Columns["ThanhTien"].ReadOnly = true;

            dgvCart.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            dgvCart.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";

            dgvCart.CellContentClick += DgvCart_CellContentClick;
        }

        private void DgvCart_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCart.Columns[e.ColumnIndex].Name == "btnXoa")
            {
                if (dgvCart.Rows[e.RowIndex].DataBoundItem is DataRowView rowView)
                {
                    rowView.Row.Delete();
                    gioHang.AcceptChanges();
                    TinhTongTien();
                }
            }
        }

        private void EnsureDatabase()
        {
            using (var conn = new SqliteConnection(ConnectionString))
            {
                conn.Open();
                conn.Execute(@"CREATE TABLE IF NOT EXISTS PhanLoai (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL
                )");
                try { conn.Execute("ALTER TABLE SanPham ADD COLUMN phanloai_id INTEGER"); } catch { }
            }
        }

        private void LoadPhanLoai()
        {
            flpCategory.SuspendLayout();
            flpCategory.Controls.Clear();

            bool allSelected = _selectedPhanLoaiId == null;
            Button btnAll = CreatePillButton("Tất cả", Color.FromArgb(99, 102, 241), allSelected);
            btnAll.Click += (s, e) =>
            {
                _selectedPhanLoaiId = null;
                LoadPhanLoai();
                LoadSanPham(txtSearchProduct.Text);
            };
            flpCategory.Controls.Add(btnAll);

            try
            {
                using (var conn = new SqliteConnection(ConnectionString))
                {
                    conn.Open();
                    var list = conn.Query("SELECT id, name AS Ten FROM PhanLoai ORDER BY id").ToList();
                    for (int i = 0; i < list.Count; i++)
                    {
                        int id = (int)(long)list[i].id;
                        string ten = (string)list[i].Ten;
                        Color color = _pillColors[i % _pillColors.Length];
                        bool selected = _selectedPhanLoaiId == id;

                        Button btn = CreatePillButton(ten, color, selected);
                        int capturedId = id;
                        btn.Click += (s, e) =>
                        {
                            _selectedPhanLoaiId = capturedId;
                            LoadPhanLoai();
                            LoadSanPham(txtSearchProduct.Text);
                        };
                        flpCategory.Controls.Add(btn);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải phân loại: " + ex.Message);
            }

            flpCategory.ResumeLayout();
        }

        private Button CreatePillButton(string text, Color color, bool selected)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btn.Height = 32;
            btn.Width = TextRenderer.MeasureText(text, btn.Font).Width + 28;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = selected ? color : Color.White;
            btn.ForeColor = selected ? Color.White : color;
            btn.Cursor = Cursors.Hand;
            btn.Margin = new Padding(0, 0, 8, 0);
            btn.UseVisualStyleBackColor = false;

            int r = btn.Height / 2;
            using (var path = new GraphicsPath())
            {
                path.StartFigure();
                path.AddArc(0, 0, r * 2, r * 2, 180, 90);
                path.AddArc(btn.Width - r * 2 - 1, 0, r * 2, r * 2, 270, 90);
                path.AddArc(btn.Width - r * 2 - 1, btn.Height - r * 2 - 1, r * 2, r * 2, 0, 90);
                path.AddArc(0, btn.Height - r * 2 - 1, r * 2, r * 2, 90, 90);
                path.CloseFigure();
                btn.Region = new Region(path);
            }

            // WinForms FlatStyle borders don't follow Region clipping — draw only the outline
            if (!selected)
            {
                btn.Paint += (s, e) =>
                {
                    var b = (Button)s!;
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    int bR = b.Height / 2;
                    using (var p = new GraphicsPath())
                    {
                        p.StartFigure();
                        p.AddArc(0, 0, bR * 2, bR * 2, 180, 90);
                        p.AddArc(b.Width - bR * 2 - 1, 0, bR * 2, bR * 2, 270, 90);
                        p.AddArc(b.Width - bR * 2 - 1, b.Height - bR * 2 - 1, bR * 2, bR * 2, 0, 90);
                        p.AddArc(0, b.Height - bR * 2 - 1, bR * 2, bR * 2, 90, 90);
                        p.CloseFigure();
                        using (var pen = new Pen(color, 1.5f))
                            e.Graphics.DrawPath(pen, p);
                    }
                };
            }

            return btn;
        }

        private void ViewGiaoDich_Load(object? sender, EventArgs e)
        {
            EnsureDatabase();
            LoadPhanLoai();
            LoadSanPham();
        }

        private void TxtSearchProduct_TextChanged(object? sender, EventArgs e)
        {
            LoadSanPham(txtSearchProduct.Text);
        }

        private void LoadSanPham(string searchKey = "")
        {
            try
            {
                flpProducts.SuspendLayout();
                flpProducts.Controls.Clear();
                using (var conn = new SqliteConnection(ConnectionString))
                {
                    conn.Open();
                    string sql = "SELECT id, ma_sp AS MaSP, ten_sp AS TenSP, so_luong_ton AS SoLuong, hinh_anh AS HinhAnh, gia_ban AS GiaBan FROM SanPham WHERE 1=1";
                    if (!string.IsNullOrEmpty(searchKey)) sql += " AND (ma_sp LIKE @key OR ten_sp LIKE @key)";
                    if (_selectedPhanLoaiId.HasValue) sql += " AND phanloai_id = @plId";
                    var danhSach = conn.Query<SanPham>(sql, new { key = "%" + searchKey + "%", plId = _selectedPhanLoaiId }).ToList();

                    foreach (var sp in danhSach)
                    {
                        TheSanPham card = new TheSanPham();
                        card.LayDuLieu(sp);

                        Action selectProduct = () => { ThemVaoGioHang(sp); };
                        card.Click += (s, ev) => selectProduct();
                        foreach (Control child in card.Controls) child.Click += (s, ev) => selectProduct();

                        flpProducts.Controls.Add(card);
                    }
                }
                flpProducts.ResumeLayout();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi hiển thị danh sách sản phẩm: " + ex.Message); }
        }

        private void ThemVaoGioHang(SanPham sp)
        {
            if (sp.SoLuong <= 0)
            {
                MessageBox.Show("Sản phẩm này đã hết hàng tồn trong kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataRow[] rows = gioHang.Select("MaSP = '" + sp.MaSP + "'");
            if (rows.Length > 0)
            {
                int currentQty = (int)rows[0]["SoLuong"];
                if (currentQty < sp.SoLuong) rows[0]["SoLuong"] = currentQty + 1;
                else MessageBox.Show("Số lượng chọn đã đạt giới hạn tối đa trong kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // SỬA LỖI: Gán trực tiếp giá bán sp.GiaBan thực tế từ DB thay vì số cứng 100000
                gioHang.Rows.Add(sp.MaSP, sp.TenSP, 1, sp.GiaBan);
            }
            TinhTongTien();
        }
    }
}