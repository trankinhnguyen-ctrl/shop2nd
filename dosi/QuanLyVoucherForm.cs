using System.Drawing.Drawing2D;

namespace dosi
{
    public partial class QuanLyVoucherForm : Form
    {
        private KhuyenMai? _editingKm;

        public QuanLyVoucherForm()
        {
            InitializeComponent();
            ReloadVoucherList();
        }

        // ─────────────────────────────────────────────────────────────────────
        //  EVENT HANDLERS
        // ─────────────────────────────────────────────────────────────────────

        private void btnThemMoi_Click(object? sender, EventArgs e) => SwitchToAddMode();

        private void btnReset_Click(object? sender, EventArgs e) => SwitchToAddMode();

        private void chk_GioiHanSL_CheckedChanged(object? sender, EventArgs e)
        {
            txt_SoLuong.Visible = chk_GioiHanSL.Checked;
            if (chk_GioiHanSL.Checked) txt_SoLuong.Focus();
        }

        private void cbo_LoaiDK_SelectedIndexChanged(object? sender, EventArgs e)
        {
            int idx = cbo_LoaiDK.SelectedIndex;
            bool has = idx > 0;
            txt_GiaTriDK.Visible = has;
            lbl_UnitDK.Visible = has;
            if (has) lbl_UnitDK.Text = idx switch { 1 => "đ", 2 => "SP", 3 => "tháng", _ => "" };
        }

        private void cbo_LoaiGiam_SelectedIndexChanged(object? sender, EventArgs e)
        {
            bool pct = cbo_LoaiGiam.SelectedIndex == 1;
            lbl_GiamToiDa.Visible = pct;
            txt_GiamToiDa.Visible = pct;
        }

        // ─────────────────────────────────────────────────────────────────────
        //  LEFT LIST
        // ─────────────────────────────────────────────────────────────────────

        private void ReloadVoucherList()
        {
            pnlVoucherList.SuspendLayout();
            pnlVoucherList.Controls.Clear();
            List<KhuyenMai> list;
            try { list = KhuyenMai.LayDanhSach(); }
            catch { list = new(); }

            int y = 0;
            foreach (var km in list)
            {
                var row = BuildVoucherRow(km);
                row.Location = new Point(0, y);
                pnlVoucherList.Controls.Add(row);
                y += row.Height;
            }
            pnlVoucherList.ResumeLayout();
        }

        private Panel BuildVoucherRow(KhuyenMai km)
        {
            bool active = km.ConHieuLuc();
            Color dotColor = active ? Color.FromArgb(22, 163, 74) : Color.FromArgb(156, 163, 175);

            var row = new Panel
            {
                Size = new Size(220, 54),
                BackColor = Color.FromArgb(248, 250, 252),
                Cursor = Cursors.Hand,
                Tag = km
            };

            row.Controls.Add(new Panel
            {
                Location = new Point(0, 53),
                Size = new Size(220, 1),
                BackColor = Color.FromArgb(226, 232, 240)
            });

            var dot = new Panel { Location = new Point(8, 23), Size = new Size(8, 8), BackColor = Color.Transparent };
            dot.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using var br = new SolidBrush(dotColor);
                e.Graphics.FillEllipse(br, 0, 0, 7, 7);
            };
            row.Controls.Add(dot);

            row.Controls.Add(new Label
            {
                Text = km.MaKM,
                Location = new Point(22, 7),
                Size = new Size(190, 20),
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            });

            row.Controls.Add(new Label
            {
                Text = km.TenKM,
                Location = new Point(22, 28),
                Size = new Size(190, 18),
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.FromArgb(100, 116, 139),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            });

            Action select = () =>
            {
                foreach (Control c in pnlVoucherList.Controls)
                    if (c is Panel rp) rp.BackColor = Color.FromArgb(248, 250, 252);
                row.BackColor = Color.FromArgb(238, 242, 255);
                SwitchToEditMode(km);
            };

            row.Click += (s, e) => select();
            foreach (Control c in row.Controls) c.Click += (s, e) => select();

            return row;
        }

        private void HighlightRow(int kmId)
        {
            foreach (Control c in pnlVoucherList.Controls)
            {
                if (c is Panel row)
                    row.BackColor = row.Tag is KhuyenMai km && km.id == kmId
                        ? Color.FromArgb(238, 242, 255)
                        : Color.FromArgb(248, 250, 252);
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        //  MODE SWITCHING
        // ─────────────────────────────────────────────────────────────────────

        private void SwitchToAddMode()
        {
            _editingKm = null;
            lblFormTitle.Text = "THÊM KHUYẾN MÃI MỚI";
            txt_MaKM.ReadOnly = false;
            txt_MaKM.BackColor = Color.FromArgb(248, 250, 252);
            btn_Luu.Text = "Thêm khuyến mãi";
            btn_Luu.BackColor = Color.FromArgb(22, 163, 74);
            btn_Xoa.Visible = false;

            foreach (Control c in pnlVoucherList.Controls)
                if (c is Panel rp) rp.BackColor = Color.FromArgb(248, 250, 252);

            ResetForm();
            try { txt_MaKM.Text = KhuyenMai.SinhMaKM(); } catch { }
        }

        private void SwitchToEditMode(KhuyenMai km)
        {
            _editingKm = km;
            lblFormTitle.Text = $"CHỈNH SỬA: {km.MaKM}";
            txt_MaKM.ReadOnly = true;
            txt_MaKM.BackColor = Color.FromArgb(241, 245, 249);
            btn_Luu.Text = "Lưu thay đổi";
            btn_Luu.BackColor = Color.FromArgb(99, 102, 241);
            btn_Xoa.Visible = true;
            HienThiDuLieu(km);
        }

        private void ResetForm()
        {
            txt_MaKM.Text = "";
            txt_TenKM.Text = "";
            dtp_BatDau.Checked = false;
            dtp_KetThuc.Checked = false;
            chk_GioiHanSL.Checked = false;
            txt_SoLuong.Text = "";
            txt_SoLuong.Visible = false;
            cbo_LoaiDK.SelectedIndex = 0;
            txt_GiaTriDK.Text = "";
            txt_GiaTriDK.Visible = false;
            lbl_UnitDK.Visible = false;
            cbo_LoaiGiam.SelectedIndex = 0;
            txt_GiaTriGiam.Text = "";
            txt_GiamToiDa.Text = "";
            lbl_GiamToiDa.Visible = false;
            txt_GiamToiDa.Visible = false;
            chk_TrangThai.Checked = true;
        }

        private void HienThiDuLieu(KhuyenMai km)
        {
            txt_MaKM.Text = km.MaKM;
            txt_TenKM.Text = km.TenKM;

            if (!string.IsNullOrEmpty(km.NgayBatDau) && DateTime.TryParse(km.NgayBatDau, out var bd))
            { dtp_BatDau.Checked = true; dtp_BatDau.Value = bd; }
            else dtp_BatDau.Checked = false;

            if (!string.IsNullOrEmpty(km.NgayKetThuc) && DateTime.TryParse(km.NgayKetThuc, out var kt))
            { dtp_KetThuc.Checked = true; dtp_KetThuc.Value = kt; }
            else dtp_KetThuc.Checked = false;

            if (km.SoLuongTon.HasValue)
            { chk_GioiHanSL.Checked = true; txt_SoLuong.Text = km.SoLuongTon.Value.ToString(); txt_SoLuong.Visible = true; }
            else
            { chk_GioiHanSL.Checked = false; txt_SoLuong.Visible = false; }

            cbo_LoaiDK.SelectedIndex = Math.Clamp(km.LoaiDieuKien, 0, 3);
            txt_GiaTriDK.Text = km.GiaTriDieuKien.HasValue ? km.GiaTriDieuKien.Value.ToString("N0") : "";

            cbo_LoaiGiam.SelectedIndex = km.LoaiGiamGia == 2 ? 1 : 0;
            txt_GiaTriGiam.Text = km.GiaTriGiam.ToString("N0");
            txt_GiamToiDa.Text = km.GiamToiDa.HasValue ? km.GiamToiDa.Value.ToString("N0") : "";

            chk_TrangThai.Checked = km.TrangThai == 1;
        }

        // ─────────────────────────────────────────────────────────────────────
        //  VALIDATION + READ FORM
        // ─────────────────────────────────────────────────────────────────────

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
                GiaTriDieuKien = cbo_LoaiDK.SelectedIndex > 0 && decimal.TryParse(
                    txt_GiaTriDK.Text.Replace(",", "").Replace(".", ""), out decimal gdk) ? gdk : (decimal?)null,
                LoaiGiamGia = cbo_LoaiGiam.SelectedIndex == 1 ? 2 : 1,
                GiaTriGiam = decimal.TryParse(
                    txt_GiaTriGiam.Text.Replace(",", "").Replace(".", ""), out decimal gg) ? gg : 0,
                GiamToiDa = !string.IsNullOrWhiteSpace(txt_GiamToiDa.Text) && decimal.TryParse(
                    txt_GiamToiDa.Text.Replace(",", "").Replace(".", ""), out decimal gtd) ? gtd : (decimal?)null,
                TrangThai = chk_TrangThai.Checked ? 1 : 0
            };
            if (_editingKm != null) km.id = _editingKm.id;
            return km;
        }

        // ─────────────────────────────────────────────────────────────────────
        //  BUTTON HANDLERS
        // ─────────────────────────────────────────────────────────────────────

        private void BtnLuu_Click(object? sender, EventArgs e)
        {
            if (!KiemTraDuLieu()) return;
            try
            {
                var km = DocDuLieuForm();
                if (_editingKm != null)
                {
                    km.CapNhatDatabase();
                    MessageBox.Show("Đã lưu thay đổi!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _editingKm = km;
                    ReloadVoucherList();
                    HighlightRow(km.id);
                }
                else
                {
                    km.LuuVaoDatabase();
                    MessageBox.Show("Đã thêm khuyến mãi mới!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReloadVoucherList();
                    SwitchToAddMode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            if (_editingKm == null) return;
            var confirm = MessageBox.Show(
                $"Xác nhận xóa khuyến mãi {_editingKm.MaKM}?\nLịch sử áp dụng sẽ được giữ nguyên.",
                "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;
            try
            {
                _editingKm.XoaKhoiDatabase();
                ReloadVoucherList();
                SwitchToAddMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
