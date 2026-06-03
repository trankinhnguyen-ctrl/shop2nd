using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace dosi
{
    public partial class Form1 : Form
    {
        private ReaLTaiizor.Controls.HopeButton? currentActiveButton;

        public Form1()
        {
            InitializeComponent();
            SetupButtonHoverEffects();
            panelShadow.Paint += PanelShadow_Paint;
            this.Load += (s, e) => MoTrangTongQuan();
        }

        private void PanelShadow_Paint(object? sender, PaintEventArgs e)
        {
            using var brush = new LinearGradientBrush(
                panelShadow.ClientRectangle,
                Color.FromArgb(18, 0, 0, 0),
                Color.Transparent,
                LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(brush, panelShadow.ClientRectangle);
        }

        private void SetupButtonHoverEffects()
        {
            ReaLTaiizor.Controls.HopeButton[] menus = { btnTongQuan, btnKhoHang, btnKhachHang, btnGiaoDich, btnPhanTich };
            foreach (var btn in menus)
            {
                btn.MouseEnter += (s, e) =>
                {
                    if (btn != currentActiveButton)
                    {
                        btn.PrimaryColor = Color.FromArgb(241, 245, 249);
                        btn.TextColor = Color.FromArgb(51, 65, 85);
                        btn.Invalidate();
                    }
                };
                btn.MouseLeave += (s, e) =>
                {
                    if (btn != currentActiveButton)
                    {
                        btn.PrimaryColor = Color.White;
                        btn.TextColor = Color.FromArgb(51, 65, 85);
                        btn.Invalidate();
                    }
                };
            }
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContent.Controls.Clear();
            panelContent.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void UpdateMenuUI(ReaLTaiizor.Controls.HopeButton activeBtn)
        {
            ReaLTaiizor.Controls.HopeButton[] menus = { btnTongQuan, btnKhoHang, btnKhachHang, btnGiaoDich, btnPhanTich };
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

            if (activeBtn == btnTongQuan)       lblPageTitle.Text = "Tổng quan";
            else if (activeBtn == btnKhoHang)   lblPageTitle.Text = "Kho hàng";
            else if (activeBtn == btnKhachHang) lblPageTitle.Text = "Khách hàng";
            else if (activeBtn == btnGiaoDich)  lblPageTitle.Text = "Giao dịch";
            else if (activeBtn == btnPhanTich)  lblPageTitle.Text = "Phân tích";
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

        public void MoTrangKhachHang(int khachId)
        {
            ViewKhachHang uc = new ViewKhachHang();
            addUserControl(uc);
            UpdateMenuUI(btnKhachHang);
            uc.ChonKhachHangTheoId(khachId);
        }

        private void btnGiaoDich_Click(object sender, EventArgs e)
        {
            ViewGiaoDich uc = new ViewGiaoDich();
            addUserControl(uc);
            UpdateMenuUI(btnGiaoDich);
        }

        private void btnPhanTich_Click(object sender, EventArgs e)
        {
            ViewPhanTich uc = new ViewPhanTich();
            addUserControl(uc);
            UpdateMenuUI(btnPhanTich);
        }

        private void picTongQuan_Click(object sender, EventArgs e)
        {

        }
    }
}