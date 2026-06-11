namespace dosi
{
    public partial class ThemKhuyenMaiForm : Form
    {
        private readonly KhuyenMai? _km;
        private readonly bool _isEdit;

        public ThemKhuyenMaiForm(KhuyenMai? km = null)
        {
            _km = km;
            _isEdit = km != null;
            InitializeComponent();
            if (_isEdit)
            {
                Text = "Sửa khuyến mãi";
                txt_MaKM.ReadOnly = true;
                txt_MaKM.BackColor = Color.FromArgb(241, 245, 249);
                btn_Luu.Text = "Lưu thay đổi";
                btn_Luu.Size = new Size(190, 40);
                btn_Xoa.Visible = true;
                btn_Huy.Location = new Point(320, 516);
                HienThiDuLieu();
            }
            else
            {
                txt_MaKM.Text = TrySinhMa();
            }
        }

        private string TrySinhMa()
        {
            try { return KhuyenMai.SinhMaKM(); } catch { return "KM001"; }
        }

        // ─────────────────────────────────────────────────────────────────────
        //  EVENT HANDLERS
        // ─────────────────────────────────────────────────────────────────────

        private void chk_GioiHanSL_CheckedChanged(object? sender, EventArgs e)
        {
            txt_SoLuong.Visible = chk_GioiHanSL.Checked;
            if (chk_GioiHanSL.Checked) txt_SoLuong.Focus();
        }

        private void CboLoaiDK_Changed(object? sender, EventArgs e)
        {
            int idx = cbo_LoaiDK.SelectedIndex;
            bool hasCond = idx > 0;
            txt_GiaTriDK.Visible = hasCond;
            lbl_UnitDK.Visible = hasCond;
            if (hasCond)
            {
                lbl_UnitDK.Text = idx switch
                {
                    1 => "đ",
                    2 => "SP",
                    3 => "tháng",
                    _ => ""
                };
            }
        }

        private void CboLoaiGiam_Changed(object? sender, EventArgs e)
        {
            bool isPercent = cbo_LoaiGiam.SelectedIndex == 1;
            lbl_GiamToiDa.Visible = isPercent;
            txt_GiamToiDa.Visible = isPercent;
        }

        // ─────────────────────────────────────────────────────────────────────
        //  DATA
        // ─────────────────────────────────────────────────────────────────────

        private void HienThiDuLieu()
        {
            var km = _km!;
            txt_MaKM.Text = km.MaKM;
            txt_TenKM.Text = km.TenKM;

            if (!string.IsNullOrEmpty(km.NgayBatDau) && DateTime.TryParse(km.NgayBatDau, out var bd))
            { dtp_BatDau.Checked = true; dtp_BatDau.Value = bd; }
            else dtp_BatDau.Checked = false;

            if (!string.IsNullOrEmpty(km.NgayKetThuc) && DateTime.TryParse(km.NgayKetThuc, out var kt))
            { dtp_KetThuc.Checked = true; dtp_KetThuc.Value = kt; }
            else dtp_KetThuc.Checked = false;

            if (km.SoLuongTon.HasValue)
            {
                chk_GioiHanSL.Checked = true;
                txt_SoLuong.Text = km.SoLuongTon.Value.ToString();
                txt_SoLuong.Visible = true;
            }

            cbo_LoaiDK.SelectedIndex = Math.Clamp(km.LoaiDieuKien, 0, 3);
            if (km.GiaTriDieuKien.HasValue) txt_GiaTriDK.Text = km.GiaTriDieuKien.Value.ToString("N0");

            cbo_LoaiGiam.SelectedIndex = km.LoaiGiamGia == 2 ? 1 : 0;
            txt_GiaTriGiam.Text = km.GiaTriGiam.ToString("N0");
            if (km.GiamToiDa.HasValue) txt_GiamToiDa.Text = km.GiamToiDa.Value.ToString("N0");

            chk_TrangThai.Checked = km.TrangThai == 1;
        }

        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrWhiteSpace(txt_MaKM.Text))
            { MessageBox.Show("Vui lòng nhập mã khuyến mãi!", "Thiếu thông tin"); return false; }

            if (string.IsNullOrWhiteSpace(txt_TenKM.Text))
            { MessageBox.Show("Vui lòng nhập tên chương trình!", "Thiếu thông tin"); return false; }

            if (string.IsNullOrWhiteSpace(txt_GiaTriGiam.Text))
            { MessageBox.Show("Vui lòng nhập giá trị giảm!", "Thiếu thông tin"); return false; }

            if (cbo_LoaiDK.SelectedIndex > 0 && string.IsNullOrWhiteSpace(txt_GiaTriDK.Text))
            { MessageBox.Show("Vui lòng nhập giá trị điều kiện!", "Thiếu thông tin"); return false; }

            return true;
        }

        private KhuyenMai DocDuLieuForm()
        {
            var km = new KhuyenMai
            {
                MaKM = txt_MaKM.Text.Trim(),
                TenKM = txt_TenKM.Text.Trim(),
                NgayBatDau = dtp_BatDau.Checked ? dtp_BatDau.Value.ToString("yyyy-MM-dd") : null,
                NgayKetThuc = dtp_KetThuc.Checked ? dtp_KetThuc.Value.ToString("yyyy-MM-dd") : null,
                SoLuongTon = chk_GioiHanSL.Checked && int.TryParse(txt_SoLuong.Text, out int sl) ? sl : (int?)null,
                LoaiDieuKien = cbo_LoaiDK.SelectedIndex,
                GiaTriDieuKien = cbo_LoaiDK.SelectedIndex > 0 && decimal.TryParse(txt_GiaTriDK.Text.Replace(",", "").Replace(".", ""), out decimal gdk) ? gdk : (decimal?)null,
                LoaiGiamGia = cbo_LoaiGiam.SelectedIndex == 1 ? 2 : 1,
                GiaTriGiam = decimal.TryParse(txt_GiaTriGiam.Text.Replace(",", "").Replace(".", ""), out decimal gg) ? gg : 0,
                GiamToiDa = !string.IsNullOrWhiteSpace(txt_GiamToiDa.Text) && decimal.TryParse(txt_GiamToiDa.Text.Replace(",", "").Replace(".", ""), out decimal gtd) ? gtd : (decimal?)null,
                TrangThai = chk_TrangThai.Checked ? 1 : 0
            };
            if (_isEdit) km.id = _km!.id;
            return km;
        }

        // ─────────────────────────────────────────────────────────────────────
        //  HANDLERS
        // ─────────────────────────────────────────────────────────────────────

        private void BtnLuu_Click(object? sender, EventArgs e)
        {
            if (!KiemTraDuLieu()) return;
            try
            {
                var km = DocDuLieuForm();
                if (_isEdit) km.CapNhatDatabase();
                else km.LuuVaoDatabase();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Xác nhận xóa khuyến mãi này?\nLịch sử áp dụng sẽ được giữ nguyên.",
                "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                _km!.XoaKhoiDatabase();
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
