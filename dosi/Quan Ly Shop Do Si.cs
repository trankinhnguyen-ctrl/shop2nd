using System;
using System.Drawing;
using System.Windows.Forms;

namespace dosi
{
    public partial class Form1 : Form
    {
        private ReaLTaiizor.Controls.HopeButton currentActiveButton;

        public Form1()
        {
            InitializeComponent();
            SetupButtonHoverEffects();
            this.Load += (s, e) => MoTrangTongQuan();
        }

        private void SetupButtonHoverEffects()
        {
            ReaLTaiizor.Controls.HopeButton[] menus = { btnTongQuan, btnKhoHang, btnKhachHang, btnGiaoDich };
            foreach (var btn in menus)
            {
                btn.MouseEnter += (s, e) =>
                {
                    if (btn != currentActiveButton)
                    {
                        btn.PrimaryColor = Color.FromArgb(241, 245, 249);
                        btn.Invalidate();
                    }
                };
                btn.MouseLeave += (s, e) =>
                {
                    if (btn != currentActiveButton)
                    {
                        btn.PrimaryColor = Color.White;
                        btn.Invalidate();
                    }
                };
            }
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void UpdateMenuUI(ReaLTaiizor.Controls.HopeButton activeBtn)
        {
            ReaLTaiizor.Controls.HopeButton[] menus = { btnTongQuan, btnKhoHang, btnKhachHang, btnGiaoDich };
            foreach (var btn in menus)
            {
                btn.PrimaryColor = Color.White;
                btn.TextColor = Color.FromArgb(51, 65, 85);
                btn.Invalidate();
            }

            activeBtn.PrimaryColor = Color.FromArgb(124, 58, 237);
            activeBtn.TextColor = Color.White;
            activeBtn.Invalidate();
            currentActiveButton = activeBtn;
        }

        private void MoTrangTongQuan()
        {
            ViewTongQuan uc = new ViewTongQuan();
            addUserControl(uc);
            UpdateMenuUI(btnTongQuan);
        }

        private void btnTongQuan_Click(object sender, EventArgs e)
        {
            MoTrangTongQuan();
        }

        private void btnKhoHang_Click(object sender, EventArgs e)
        {
            ViewKhoHang uc = new ViewKhoHang();
            addUserControl(uc);
            UpdateMenuUI(btnKhoHang);
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            ViewKhachHang uc = new ViewKhachHang();
            addUserControl(uc);
            UpdateMenuUI(btnKhachHang);
        }

        private void btnGiaoDich_Click(object sender, EventArgs e)
        {
            ViewGiaoDich uc = new ViewGiaoDich();
            addUserControl(uc);
            UpdateMenuUI(btnGiaoDich);
        }
    }
}