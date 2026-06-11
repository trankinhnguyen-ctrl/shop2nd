using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Drawing.Drawing2D;

namespace dosi
{
    public partial class ViewGiaoDich : UserControl
    {
        private string ConnectionString = "Data Source=QuanLyKho.db";
        private DataTable gioHang = new DataTable();
        private int? _selectedPhanLoaiId = null;

        private string _editingOrderId = "";
        private Panel panelEditBanner = null!;
        private Label lblEditBanner = null!;

        // Voucher state
        private List<KhuyenMai> _appliedVouchers = new();
        private string _ngayTaoKhach = "";
        private decimal _tongGoc = 0;
        private decimal _tongTienGiam = 0;

        // Voucher UI controls (created programmatically)
        private GroupBox grpVoucher = null!;
        private Label lblTienGiam = null!;

        public bool DangChinhSua => !string.IsNullOrEmpty(_editingOrderId);

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

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            KhoiTaoGioHang();
            StyleCartDataGridView();
            SetupEditBanner();
            SetupVoucherUI();

            this.Load += ViewGiaoDich_Load;
            this.Resize += (s, e) => { LoadSanPham(txtSearchProduct.Text); };
            txtSearchProduct.TextChanged += TxtSearchProduct_TextChanged;

            txtPhone.Leave += TxtPhone_Leave;
            txtPhone.KeyDown += TxtPhone_KeyDown;
            txtName.KeyDown += TxtName_KeyDown;
            btnThanhToan.Click += BtnThanhToan_Click;

            dgvCart.CellValidating += dgvCart_CellValidating;
            dgvCart.CellValueChanged += dgvCart_CellValueChanged;
            dgvCart.RowsRemoved += dgvCart_RowsRemoved;
            dgvCart.EditingControlShowing += dgvCart_EditingControlShowing;

            dgvCart.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        // ─────────────────────────────────────────────────────────────────────
        //  KHUYẾN MÃI UI SETUP
        // ─────────────────────────────────────────────────────────────────────
        private void SetupVoucherUI()
        {
            grpCart.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpCart.Height = 215;

            lblTienGiam = new Label
            {
                Location = new Point(4, 548),
                Size = new Size(318, 20),
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.FromArgb(22, 163, 74),
                TextAlign = ContentAlignment.MiddleRight,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Visible = false
            };
            panelOrderInfo.Controls.Add(lblTienGiam);

            grpVoucher = new GroupBox
            {
                Location = new Point(4, 444),
                Size = new Size(318, 100),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Bold),
                Text = "Khuyến mãi",
                BackColor = Color.White,
                ForeColor = Color.FromArgb(51, 65, 85)
            };

            // Applied tag row
            var flpApplied = new FlowLayoutPanel
            {
                Location = new Point(6, 20),
                Size = new Size(304, 32),
                WrapContents = true,
                AutoScroll = false,
                BackColor = Color.White,
                Name = "flpApplied"
            };
            grpVoucher.Controls.Add(flpApplied);

            // Main apply button — takes most of the row
            var btnThem = new Button
            {
                Text = "+ Áp dụng khuyến mãi",
                Location = new Point(6, 58),
                Size = new Size(272, 32),
                BackColor = Color.FromArgb(99, 102, 241),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnThem.FlatAppearance.BorderSize = 0;
            btnThem.Click += BtnThemVoucher_Click;
            grpVoucher.Controls.Add(btnThem);

            // Gear icon — right of apply button, opens management
            var btnQL = new Button
            {
                Text = "⚙",
                Location = new Point(284, 58),
                Size = new Size(26, 32),
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = Color.FromArgb(100, 116, 139),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f),
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnQL.FlatAppearance.BorderSize = 1;
            btnQL.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnQL.Click += (s, e) =>
            {
                using var form = new QuanLyVoucherForm();
                form.ShowDialog();
            };
            grpVoucher.Controls.Add(btnQL);

            panelOrderInfo.Controls.Add(grpVoucher);
            panelOrderInfo.Controls.SetChildIndex(grpVoucher, 0);
        }

        private void BtnThemVoucher_Click(object? sender, EventArgs e)
        {
            if (gioHang.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var applied = _appliedVouchers.Select(v => v.id).ToHashSet();
            var available = KhuyenMai.LayDanhSach(chiHieuLuc: true)
                .Where(k => !applied.Contains(k.id))
                .ToList();

            using var form = new ChonVoucherForm(available);
            if (form.ShowDialog() != DialogResult.OK || form.SelectedVoucher is not KhuyenMai km) return;

            int tongSL = LayTongSoLuongGioHang();
            decimal giam = km.TinhTienGiam(_tongGoc, tongSL, _ngayTaoKhach);

            if (giam <= 0)
            {
                string reason = km.LoaiDieuKien switch
                {
                    1 => $"Đơn hàng chưa đạt tối thiểu {km.GiaTriDieuKien:N0}đ.",
                    2 => $"Giỏ hàng chưa đạt tối thiểu {km.GiaTriDieuKien:N0} sản phẩm.",
                    3 => $"Khách hàng chưa đủ {km.GiaTriDieuKien} tháng.",
                    _ => "Khuyến mãi này không áp dụng được."
                };
                MessageBox.Show("Không thể áp dụng khuyến mãi.\n" + reason, "Không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _appliedVouchers.Add(km);
            AddVoucherTag(km);
            TinhTongTien();
        }

        private void AddVoucherTag(KhuyenMai km)
        {
            var flpApplied = grpVoucher.Controls.OfType<FlowLayoutPanel>().First();

            var tag = new Panel
            {
                Height = 24,
                Width = 100,
                BackColor = Color.FromArgb(238, 242, 255),
                Name = "tag_" + km.id
            };

            var lblTag = new Label
            {
                Text = km.MaKM,
                Dock = DockStyle.Left,
                AutoSize = false,
                Width = 72,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI Semibold", 8.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241)
            };

            var btnRemove = new Button
            {
                Text = "×",
                Dock = DockStyle.Right,
                Width = 22,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 116, 139),
                Cursor = Cursors.Hand,
                BackColor = Color.Transparent
            };
            btnRemove.FlatAppearance.BorderSize = 0;

            var capturedKm = km;
            var capturedTag = tag;
            btnRemove.Click += (s, e) =>
            {
                _appliedVouchers.Remove(capturedKm);
                flpApplied.Controls.Remove(capturedTag);
                TinhTongTien();
            };

            tag.Controls.Add(btnRemove);
            tag.Controls.Add(lblTag);
            flpApplied.Controls.Add(tag);
        }

        // ─────────────────────────────────────────────────────────────────────
        //  EDIT ORDER
        // ─────────────────────────────────────────────────────────────────────
        private void SetupEditBanner()
        {
            lblEditBanner = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(154, 52, 18),
                Padding = new Padding(8, 0, 0, 0)
            };

            panelEditBanner = new Panel
            {
                Dock = DockStyle.Top,
                Height = 42,
                BackColor = Color.FromArgb(254, 215, 170),
                Visible = false
            };
            panelEditBanner.Controls.Add(lblEditBanner);
            splitMain.Panel2.Controls.Add(panelEditBanner);
        }

        public void LoadEditOrder(EditOrderContext ctx)
        {
            _editingOrderId = ctx.MaHoaDonGoc;

            // Clear vouchers when loading edit order
            _appliedVouchers.Clear();
            RefreshAppliedVoucherTags();

            gioHang.Clear();

            txtPhone.Text = ctx.SoDienThoai;
            txtName.Text = ctx.TenKhachHang;
            txtAddress.Text = ctx.DiaChi;
            _ngayTaoKhach = "";

            foreach (var item in ctx.Items)
                gioHang.Rows.Add(item.MaSP, item.TenSP, item.SoLuong, item.DonGia);

            TinhTongTien();

            lblEditBanner.Text = $"⚠  Đang chỉnh sửa đơn {ctx.MaHoaDonGoc}  ·  Đơn gốc đã được hoàn kho  ·  Xác nhận để lưu đơn mới";
            panelEditBanner.Visible = true;

            btnThanhToan.Text = "LƯU ĐƠN ĐÃ CHỈNH SỬA";
            btnThanhToan.BackColor = Color.FromArgb(234, 88, 12);
        }

        private void RefreshAppliedVoucherTags()
        {
            var flpApplied = grpVoucher?.Controls.OfType<FlowLayoutPanel>().FirstOrDefault();
            flpApplied?.Controls.Clear();
        }

        public bool XacNhanHuyChinhSua()
        {
            var result = MessageBox.Show(
                "Bạn có đơn đang chỉnh sửa chưa lưu.\n" +
                "Nếu thoát, mọi thay đổi sẽ bị hủy và đơn gốc sẽ được khôi phục.\n\n" +
                "Bạn có chắc muốn hủy chỉnh sửa?",
                "Cảnh báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                RollbackEditOrder(_editingOrderId);
                return true;
            }
            return false;
        }

        private void RollbackEditOrder(string maHoaDon)
        {
            try
            {
                using var conn = new SqliteConnection(ConnectionString);
                conn.Open();
                using var trans = conn.BeginTransaction();

                var items = conn.Query(@"
                    SELECT nx.SAPHAM_id AS SpId, nx.so_luong AS SoLuong
                    FROM NhapXuat nx JOIN HoaDon hd ON nx.hoadon_id = hd.id
                    WHERE hd.ma_hd = @maHD AND nx.loai_giao_dich = 'Xuat'",
                    new { maHD = maHoaDon }, transaction: trans).ToList();

                foreach (dynamic item in items)
                {
                    conn.Execute(
                        "UPDATE SanPham SET so_luong_ton = MAX(0, so_luong_ton - @sl) WHERE id = @id",
                        new { sl = (int)(long)item.SoLuong, id = (long)item.SpId },
                        transaction: trans);
                }

                conn.Execute("UPDATE HoaDon SET TrangThai = 'HoanThanh' WHERE ma_hd = @maHD",
                    new { maHD = maHoaDon }, transaction: trans);

                trans.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hủy chỉnh sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ResetEditMode();
            gioHang.Clear();
            txtPhone.Clear();
            txtName.Clear();
            txtAddress.Clear();
            TinhTongTien();
        }

        private void ResetEditMode()
        {
            _editingOrderId = "";
            panelEditBanner.Visible = false;
            btnThanhToan.Text = "XÁC NHẬN THANH TOÁN";
            btnThanhToan.BackColor = Color.FromArgb(22, 163, 74);
        }

        // ─────────────────────────────────────────────────────────────────────
        //  TÍNH TIỀN
        // ─────────────────────────────────────────────────────────────────────
        private void TinhTongTien()
        {
            decimal tongGoc = gioHang.AsEnumerable()
                .Where(row => row.RowState != DataRowState.Deleted && row.RowState != DataRowState.Detached)
                .Sum(row => (row["SoLuong"] == DBNull.Value ? 0 : Convert.ToInt32(row["SoLuong"])) *
                            (row["DonGia"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DonGia"])));

            int tongSL = gioHang.AsEnumerable()
                .Where(row => row.RowState != DataRowState.Deleted && row.RowState != DataRowState.Detached)
                .Sum(row => row["SoLuong"] == DBNull.Value ? 0 : Convert.ToInt32(row["SoLuong"]));

            CapNhatHienThiTong(tongGoc, tongSL);
        }

        private void TinhTongTienTuGiaoDien(string currentEditingText)
        {
            decimal tongGoc = 0;
            int tongSL = 0;
            if (dgvCart.CurrentCell == null) return;
            int editingRowIndex = dgvCart.CurrentCell.RowIndex;

            foreach (DataGridViewRow r in dgvCart.Rows)
            {
                if (r.IsNewRow) continue;
                int sl = 0;
                if (r.Index == editingRowIndex) int.TryParse(currentEditingText, out sl);
                else sl = r.Cells["SoLuong"].Value == DBNull.Value ? 0 : Convert.ToInt32(r.Cells["SoLuong"].Value);

                decimal gia = r.Cells["DonGia"].Value == DBNull.Value ? 0 : Convert.ToDecimal(r.Cells["DonGia"].Value);
                tongGoc += sl * gia;
                tongSL += sl;
            }
            CapNhatHienThiTong(tongGoc, tongSL);
        }

        private void CapNhatHienThiTong(decimal tongGoc, int tongSL)
        {
            _tongGoc = tongGoc;

            decimal remaining = tongGoc;
            decimal tongGiam = 0;
            foreach (var v in _appliedVouchers)
            {
                decimal g = v.TinhTienGiam(remaining, tongSL, _ngayTaoKhach);
                tongGiam += g;
                remaining -= g;
                if (remaining < 0) remaining = 0;
            }
            _tongTienGiam = tongGiam;
            decimal phaiThanh = Math.Max(0, tongGoc - tongGiam);

            if (tongGiam > 0)
            {
                lblTongTien.Text = "Tổng: " + phaiThanh.ToString("N0") + "đ";
                lblTienGiam.Text = "Tổng gốc: " + tongGoc.ToString("N0") + "đ  ·  Giảm: -" + tongGiam.ToString("N0") + "đ";
                lblTienGiam.Visible = true;
            }
            else
            {
                lblTongTien.Text = "Tổng: " + tongGoc.ToString("N0") + "đ";
                lblTienGiam.Visible = false;
            }
        }

        private int LayTongSoLuongGioHang()
        {
            return gioHang.AsEnumerable()
                .Where(row => row.RowState != DataRowState.Deleted && row.RowState != DataRowState.Detached)
                .Sum(row => row["SoLuong"] == DBNull.Value ? 0 : Convert.ToInt32(row["SoLuong"]));
        }

        // ─────────────────────────────────────────────────────────────────────
        //  CHECKOUT
        // ─────────────────────────────────────────────────────────────────────
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

            string editingId = _editingOrderId;

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
                            string ngayTaoKhach = DateTime.Now.ToString("yyyy-MM-dd");
                            khId = conn.ExecuteScalar<int>(
                                "INSERT INTO Khach (ho_ten, so_dien_thoai, dia_chi, ngay_tao) VALUES (@ten, @sdt, @dc, @ngayTao); SELECT last_insert_rowid();",
                                new { ten = txtName.Text, sdt = txtPhone.Text, dc = txtAddress.Text, ngayTao = ngayTaoKhach },
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
                        string maHoaDon = "HD" + DateTime.Now.ToString("yyyyMMddHHmmss");

                        decimal tongTienHD = gioHang.AsEnumerable()
                            .Where(r => r.RowState != DataRowState.Deleted)
                            .Sum(r => Convert.ToDecimal(r["SoLuong"]) * Convert.ToDecimal(r["DonGia"]));

                        int tongSLGioHang = LayTongSoLuongGioHang();

                        decimal remaining = tongTienHD;
                        decimal tongGiamHD = 0;
                        var voucherApplied = new List<(KhuyenMai km, decimal giam)>();
                        foreach (var v in _appliedVouchers)
                        {
                            decimal g = v.TinhTienGiam(remaining, tongSLGioHang, _ngayTaoKhach);
                            voucherApplied.Add((v, g));
                            tongGiamHD += g;
                            remaining -= g;
                            if (remaining < 0) remaining = 0;
                        }
                        decimal phaiThanhHD = Math.Max(0, tongTienHD - tongGiamHD);

                        string ghiChu = DangChinhSua ? "Chỉnh sửa từ " + editingId : "";

                        long hoaDonId = conn.ExecuteScalar<long>(
                            @"INSERT INTO HoaDon (ma_hd, KHACH_id, ngay_tao, tong_tien, tong_tien_giam, tien_phai_thanh, ghi_chu, TrangThai)
                              VALUES (@ma, @khId, @ngay, @tong, @giamHD, @phaiThanh, @ghiChu, 'HoanThanh');
                              SELECT last_insert_rowid();",
                            new { ma = maHoaDon, khId, ngay = thoiGianTao, tong = tongTienHD, giamHD = tongGiamHD, phaiThanh = phaiThanhHD, ghiChu },
                            transaction: trans);

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
                                @"INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu, hoadon_id)
                                  VALUES (@spId, @khId, 'Xuat', @sl, @ngay, @note, @hdId)",
                                new
                                {
                                    spId = sanPhamInfo.id,
                                    khId,
                                    sl = slBan,
                                    ngay = thoiGianTao,
                                    note = (DangChinhSua ? "Chỉnh sửa: " : "Bán hàng trực tiếp: ") + row["TenSP"],
                                    hdId = hoaDonId
                                },
                                transaction: trans);

                            int rowsAffected = conn.Execute(
                                "UPDATE SanPham SET so_luong_ton = so_luong_ton - @sl WHERE id = @spId AND so_luong_ton >= @sl",
                                new { sl = slBan, spId = sanPhamInfo.id },
                                transaction: trans);

                            if (rowsAffected == 0)
                                throw new Exception("Sản phẩm " + row["TenSP"] + " vừa hết hoặc không đủ số lượng tồn kho thực tế!");
                        }

                        foreach (var (km, giam) in voucherApplied)
                        {
                            if (giam <= 0) continue;
                            conn.Execute(
                                "INSERT INTO HoaDon_KhuyenMai (hoadon_id, khuyenmai_id, so_tien_giam) VALUES (@hdId, @kmId, @giam)",
                                new { hdId = hoaDonId, kmId = km.id, giam },
                                transaction: trans);

                            if (km.SoLuongTon.HasValue)
                            {
                                conn.Execute(
                                    "UPDATE KhuyenMai SET so_luong_ton = MAX(0, so_luong_ton - 1) WHERE id = @id",
                                    new { id = km.id },
                                    transaction: trans);
                            }
                        }

                        if (DangChinhSua)
                        {
                            conn.Execute(
                                "UPDATE HoaDon SET TrangThai = 'DaChinhSua', MaHoaDonMoi = @newMa WHERE ma_hd = @oldMa",
                                new { newMa = maHoaDon, oldMa = editingId },
                                transaction: trans);
                        }

                        trans.Commit();

                        string msgSuccess = DangChinhSua
                            ? $"Đã lưu đơn chỉnh sửa thành công!\nMã đơn mới: {maHoaDon}"
                            : "Thanh toán đơn hàng và cập nhật dữ liệu kho thành công!";

                        if (tongGiamHD > 0)
                            msgSuccess += $"\nĐã áp dụng {voucherApplied.Count} khuyến mãi, tiết kiệm {tongGiamHD:N0}đ";

                        MessageBox.Show(msgSuccess, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (MessageBox.Show("Bạn có muốn in hóa đơn không?", "In hóa đơn",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            HoaDonPdfService.InHoaDonTheoMa(maHoaDon);

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
            ResetEditMode();
            gioHang.Clear();
            txtPhone.Clear();
            txtName.Clear();
            txtAddress.Clear();
            _ngayTaoKhach = "";

            _appliedVouchers.Clear();
            RefreshAppliedVoucherTags();

            lblTongTien.Text = "Tổng: 0đ";
            lblTienGiam.Visible = false;
            _tongGoc = 0;
            _tongTienGiam = 0;

            LoadSanPham();
        }

        // ─────────────────────────────────────────────────────────────────────
        //  PHONE LOOKUP
        // ─────────────────────────────────────────────────────────────────────
        private void TxtPhone_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhone.Text)) return;

            try
            {
                using var conn = new SqliteConnection(ConnectionString);
                conn.Open();
                var khach = conn.QueryFirstOrDefault(
                    "SELECT ho_ten AS Ten, dia_chi AS DiaChi, ngay_tao AS NgayTao FROM Khach WHERE so_dien_thoai = @sdt",
                    new { sdt = txtPhone.Text });

                if (khach != null)
                {
                    txtName.Text = khach.Ten;
                    txtAddress.Text = khach.DiaChi;
                    _ngayTaoKhach = khach.NgayTao ?? "";
                }
                else
                {
                    _ngayTaoKhach = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm khách hàng: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TxtPhone_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            TxtPhone_Leave(sender, EventArgs.Empty);
            txtName.Focus();
            txtName.BeginInvoke(new Action(() => { txtName.SelectionStart = txtName.Text.Length; txtName.SelectionLength = 0; }));
        }

        private void TxtName_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            txtAddress.Focus();
            txtAddress.BeginInvoke(new Action(() => { txtAddress.SelectionStart = txtAddress.Text.Length; txtAddress.SelectionLength = 0; }));
        }

        // ─────────────────────────────────────────────────────────────────────
        //  CART VALIDATION / EVENTS
        // ─────────────────────────────────────────────────────────────────────
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

        private void dgvCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                dgvCart.BeginEdit(true);
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
                using var conn = new SqliteConnection(ConnectionString);
                conn.Open();
                return conn.ExecuteScalar<int>("SELECT so_luong_ton FROM SanPham WHERE ma_sp = @ma", new { ma = maSP });
            }
            catch { return 0; }
        }

        // ─────────────────────────────────────────────────────────────────────
        //  GIỎ HÀNG
        // ─────────────────────────────────────────────────────────────────────
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

        // ─────────────────────────────────────────────────────────────────────
        //  DATABASE / LOAD
        // ─────────────────────────────────────────────────────────────────────
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
                using var conn = new SqliteConnection(ConnectionString);
                conn.Open();
                var list = conn.Query("SELECT id, ten AS Ten FROM PhanLoai ORDER BY id").ToList();
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
            btn.BackColor = Color.White;
            btn.ForeColor = selected ? Color.White : color;
            btn.Cursor = Cursors.Hand;
            btn.Margin = new Padding(0, 4, 8, 4);
            btn.UseVisualStyleBackColor = false;

            btn.Paint += (s, e) =>
            {
                var b = (Button)s!;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                Color parentBg = b.Parent?.BackColor ?? Color.FromArgb(248, 250, 252);
                using (var bgBrush = new SolidBrush(parentBg))
                    e.Graphics.FillRectangle(bgBrush, b.ClientRectangle);

                const int inset = 1;
                int diam = b.Height - inset * 2 - 1;
                using (var p = new GraphicsPath())
                {
                    p.AddArc(inset, inset, diam, diam, 90, 180);
                    p.AddArc(b.Width - inset - diam - 1, inset, diam, diam, 270, 180);
                    p.CloseFigure();

                    if (selected)
                    {
                        using (var brush = new SolidBrush(color))
                            e.Graphics.FillPath(brush, p);
                    }
                    else
                    {
                        using (var brush = new SolidBrush(Color.White))
                            e.Graphics.FillPath(brush, p);
                        using (var pen = new Pen(color, 1.5f))
                            e.Graphics.DrawPath(pen, p);
                    }
                }

                TextRenderer.DrawText(e.Graphics, b.Text, b.Font, b.ClientRectangle,
                    b.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };

            return btn;
        }

        private void ViewGiaoDich_Load(object? sender, EventArgs e)
        {
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
                gioHang.Rows.Add(sp.MaSP, sp.TenSP, 1, sp.GiaBan);
            }
            TinhTongTien();
        }
    }
}
