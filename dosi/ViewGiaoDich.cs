using System.Data;
using System.Data.SQLite;
using Dapper;

namespace dosi
{
    public partial class ViewGiaoDich : UserControl
    {
        private string ConnectionString = "Data Source=QuanLyKho.db";
        private DataTable gioHang = new DataTable();

        public ViewGiaoDich()
        {
            InitializeComponent();
            KhoiTaoGioHang();

            this.Load += ViewGiaoDich_Load;
            this.Resize += (s, e) => { LoadSanPham(txtSearchProduct.Text); };
            txtSearchProduct.TextChanged += TxtSearchProduct_TextChanged;

            txtPhone.Leave += TxtPhone_Leave;
            btnThanhToan.Click += BtnThanhToan_Click;

            dgvCart.CellValidating += DgvCart_CellValidating;
            dgvCart.CellValueChanged += DgvCart_CellValueChanged;
            dgvCart.RowsRemoved += DgvCart_RowsRemoved;
            dgvCart.EditingControlShowing += DgvCart_EditingControlShowing;

            dgvCart.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        private void TxtPhone_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhone.Text)) return;

            using (var conn = new SQLiteConnection(ConnectionString))
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

        private void DgvCart_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvCart.Columns[e.ColumnIndex].Name == "SoLuong")
            {
                string? maSP = dgvCart.Rows[e.RowIndex].Cells["MaSP"].Value?.ToString();
                if (string.IsNullOrEmpty(maSP)) return;

                string input = e.FormattedValue?.ToString() ?? "";
                int maxStock = LayTonKhoThucTe(maSP);

                if (!int.TryParse(input, out int inputQty))
                {
                    if (dgvCart.EditingControl is TextBox tb) tb.Text = "1";
                }
                else if (inputQty < 1)
                {
                    if (dgvCart.EditingControl is TextBox tb) tb.Text = "1";
                }
                else if (inputQty > maxStock)
                {
                    MessageBox.Show("Kho chỉ còn " + maxStock + " sản phẩm!");
                    if (dgvCart.EditingControl is TextBox tb) tb.Text = maxStock.ToString();
                }
            }
        }

        private void BtnThanhToan_Click(object? sender, EventArgs e)
        {
            if (gioHang.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại khách hàng!");
                return;
            }

            using (var conn = new SQLiteConnection(ConnectionString))
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

                            if (sanPhamInfo == null) throw new Exception("Không tìm thấy sản phẩm: " + maSP);

                            conn.Execute(
                                @"INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu) 
                          VALUES (@spId, @khId, 'Xuat', @sl, @ngay, @note)",
                                new
                                {
                                    spId = sanPhamInfo.id,
                                    khId = khId,
                                    sl = slBan,
                                    ngay = thoiGianTao,
                                    note = "Ban hang: " + row["TenSP"]
                                },
                                transaction: trans);

                            int rowsAffected = conn.Execute(
                                "UPDATE SanPham SET so_luong_ton = so_luong_ton - @sl WHERE id = @spId AND so_luong_ton >= @sl",
                                new { sl = slBan, spId = sanPhamInfo.id },
                                transaction: trans);

                            if (rowsAffected == 0)
                            {
                                throw new Exception("Sản phẩm " + row["TenSP"] + " không đủ số lượng tồn kho!");
                            }
                        }

                        trans.Commit();
                        MessageBox.Show("Thanh toán và cập nhật kho thành công!");
                        LamMoiSauThanhToan();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Lỗi hệ thống: " + ex.Message);
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

        private void DgvCart_EditingControlShowing(object? sender, DataGridViewEditingControlShowingEventArgs e)
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
            decimal tong = 0;
            foreach (DataRow row in gioHang.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    int sl = row["SoLuong"] == DBNull.Value ? 0 : Convert.ToInt32(row["SoLuong"]);
                    decimal gia = row["DonGia"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DonGia"]);
                    tong += sl * gia;
                }
            }
            lblTongTien.Text = "Tổng: " + tong.ToString("N0") + "đ";
        }

        private void dgvCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvCart.BeginEdit(true);
            }
        }

        private void DgvCart_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) TinhTongTien();
        }

        private void DgvCart_RowsRemoved(object? sender, DataGridViewRowsRemovedEventArgs e)
        {
            TinhTongTien();
        }

        private int LayTonKhoThucTe(string maSP)
        {
            try
            {
                using (var conn = new SQLiteConnection(ConnectionString))
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

        private void ViewGiaoDich_Load(object? sender, EventArgs e)
        {
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
                using (var conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    string sql = "SELECT id, ma_sp AS MaSP, ten_sp AS TenSP, so_luong_ton AS SoLuong, hinh_anh AS HinhAnh, gia_ban AS GiaBan FROM SanPham";
                    if (!string.IsNullOrEmpty(searchKey)) sql += " WHERE ma_sp LIKE @key OR ten_sp LIKE @key";
                    var danhSach = conn.Query<SanPham>(sql, new { key = "%" + searchKey + "%" }).ToList();
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
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ThemVaoGioHang(SanPham sp)
        {
            if (sp.SoLuong <= 0) return;
            DataRow[] rows = gioHang.Select("MaSP = '" + sp.MaSP + "'");
            if (rows.Length > 0)
            {
                int currentQty = (int)rows[0]["SoLuong"];
                if (currentQty < sp.SoLuong) rows[0]["SoLuong"] = currentQty + 1;
            }
            else gioHang.Rows.Add(sp.MaSP, sp.TenSP, 1, 100000);
            TinhTongTien();
        }
    }
}