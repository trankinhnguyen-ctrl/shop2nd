using System;
using System.Drawing;
using System.Windows.Forms;

namespace dosi
{
    public class SuaKhachHangForm : Form
    {
        private readonly KhachHang _kh;

        private readonly TextBox txtHoTen = new TextBox();
        private readonly TextBox txtSoDienThoai = new TextBox();
        private readonly TextBox txtDiaChi = new TextBox();
        private readonly TextBox txtGhiChu = new TextBox();

        public SuaKhachHangForm(KhachHang kh)
        {
            _kh = kh;
            InitUI();
            HienThiDuLieu();
        }

        private void InitUI()
        {
            Text = "Chỉnh sửa khách hàng";
            Size = new Size(440, 360);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 10F);

            void AddRow(string labelText, TextBox txt, int y, bool multiline = false)
            {
                var lbl = new Label
                {
                    Text = labelText,
                    Location = new Point(20, y + 4),
                    Size = new Size(120, 24),
                    TextAlign = ContentAlignment.MiddleLeft,
                    ForeColor = Color.FromArgb(51, 65, 85)
                };
                txt.Location = new Point(148, y);
                txt.Size = new Size(255, multiline ? 72 : 30);
                txt.Font = new Font("Segoe UI", 10F);
                txt.BorderStyle = BorderStyle.FixedSingle;
                txt.BackColor = Color.FromArgb(248, 250, 252);
                if (multiline)
                {
                    txt.Multiline = true;
                    txt.ScrollBars = ScrollBars.Vertical;
                }
                Controls.Add(lbl);
                Controls.Add(txt);
            }

            AddRow("Họ tên:", txtHoTen, 20);
            AddRow("Số điện thoại:", txtSoDienThoai, 68);
            AddRow("Địa chỉ:", txtDiaChi, 116);
            AddRow("Ghi chú:", txtGhiChu, 164, multiline: true);

            var btnLuu = new Button
            {
                Text = "Lưu",
                Location = new Point(148, 258),
                Size = new Size(118, 38),
                BackColor = Color.FromArgb(124, 58, 237),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.Click += BtnLuu_Click;

            var btnHuy = new Button
            {
                Text = "Hủy",
                Location = new Point(278, 258),
                Size = new Size(118, 38),
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = Color.FromArgb(51, 65, 85),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F),
                Cursor = Cursors.Hand
            };
            btnHuy.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnHuy.Click += (s, e) => DialogResult = DialogResult.Cancel;

            Controls.Add(btnLuu);
            Controls.Add(btnHuy);
            AcceptButton = btnLuu;
            CancelButton = btnHuy;
        }

        private void HienThiDuLieu()
        {
            txtHoTen.Text = _kh.HoTen;
            txtSoDienThoai.Text = _kh.SoDienThoai;
            txtDiaChi.Text = _kh.DiaChi;
            txtGhiChu.Text = _kh.GhiChu;
        }

        private void BtnLuu_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            _kh.HoTen = txtHoTen.Text.Trim();
            _kh.SoDienThoai = txtSoDienThoai.Text.Trim();
            _kh.DiaChi = txtDiaChi.Text.Trim();
            _kh.GhiChu = txtGhiChu.Text.Trim();

            try
            {
                _kh.CapNhatDatabase();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
