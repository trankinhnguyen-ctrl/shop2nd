using System.Drawing;
using System.Windows.Forms;

namespace dosi
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panelSidebar = new Panel();
            btnPhanTich = new ReaLTaiizor.Controls.HopeButton();
            picPhanTich = new PictureBox();
            btnGiaoDich = new ReaLTaiizor.Controls.HopeButton();
            picGiaoDich = new PictureBox();
            btnKhachHang = new ReaLTaiizor.Controls.HopeButton();
            picKhachHang = new PictureBox();
            btnKhoHang = new ReaLTaiizor.Controls.HopeButton();
            picKhoHang = new PictureBox();
            btnTongQuan = new ReaLTaiizor.Controls.HopeButton();
            picTongQuan = new PictureBox();
            lblLogoSub = new Label();
            lblLogoText = new Label();
            picLogo = new PictureBox();
            panelMain = new Panel();
            panelContent = new Panel();
            panelShadow = new Panel();
            panelTopBar = new Panel();
            lblPageTitle = new Label();
            panelSidebar.SuspendLayout();
            btnPhanTich.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPhanTich).BeginInit();
            btnGiaoDich.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picGiaoDich).BeginInit();
            btnKhachHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picKhachHang).BeginInit();
            btnKhoHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picKhoHang).BeginInit();
            btnTongQuan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picTongQuan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            panelMain.SuspendLayout();
            panelTopBar.SuspendLayout();
            SuspendLayout();
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.White;
            panelSidebar.BackgroundImageLayout = ImageLayout.Center;
            panelSidebar.Controls.Add(btnPhanTich);
            panelSidebar.Controls.Add(btnGiaoDich);
            panelSidebar.Controls.Add(btnKhachHang);
            panelSidebar.Controls.Add(btnKhoHang);
            panelSidebar.Controls.Add(btnTongQuan);
            panelSidebar.Controls.Add(lblLogoSub);
            panelSidebar.Controls.Add(lblLogoText);
            panelSidebar.Controls.Add(picLogo);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(250, 700);
            panelSidebar.TabIndex = 0;
            // 
            // btnPhanTich
            // 
            btnPhanTich.BorderColor = Color.FromArgb(220, 223, 230);
            btnPhanTich.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnPhanTich.Controls.Add(picPhanTich);
            btnPhanTich.Cursor = Cursors.Hand;
            btnPhanTich.DangerColor = Color.FromArgb(245, 108, 108);
            btnPhanTich.DefaultColor = Color.FromArgb(255, 255, 255);
            btnPhanTich.Font = new Font("Segoe UI", 12F);
            btnPhanTich.HoverTextColor = Color.FromArgb(51, 65, 85);
            btnPhanTich.InfoColor = Color.FromArgb(144, 147, 153);
            btnPhanTich.Location = new Point(15, 340);
            btnPhanTich.Name = "btnPhanTich";
            btnPhanTich.PrimaryColor = Color.White;
            btnPhanTich.Size = new Size(220, 50);
            btnPhanTich.SuccessColor = Color.FromArgb(103, 194, 58);
            btnPhanTich.TabIndex = 5;
            btnPhanTich.Text = "          Phân tích";
            btnPhanTich.TextColor = Color.FromArgb(51, 65, 85);
            btnPhanTich.WarningColor = Color.FromArgb(230, 162, 60);
            btnPhanTich.Click += btnPhanTich_Click;
            // 
            // picPhanTich
            // 
            picPhanTich.BackColor = Color.Transparent;
            picPhanTich.Image = (Image)resources.GetObject("picPhanTich.Image");
            picPhanTich.Location = new Point(15, 13);
            picPhanTich.Name = "picPhanTich";
            picPhanTich.Size = new Size(24, 24);
            picPhanTich.SizeMode = PictureBoxSizeMode.Zoom;
            picPhanTich.TabIndex = 8;
            picPhanTich.TabStop = false;
            // 
            // btnGiaoDich
            // 
            btnGiaoDich.BorderColor = Color.FromArgb(220, 223, 230);
            btnGiaoDich.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnGiaoDich.Controls.Add(picGiaoDich);
            btnGiaoDich.Cursor = Cursors.Hand;
            btnGiaoDich.DangerColor = Color.FromArgb(245, 108, 108);
            btnGiaoDich.DefaultColor = Color.FromArgb(255, 255, 255);
            btnGiaoDich.Font = new Font("Segoe UI", 12F);
            btnGiaoDich.HoverTextColor = Color.FromArgb(51, 65, 85);
            btnGiaoDich.InfoColor = Color.FromArgb(144, 147, 153);
            btnGiaoDich.Location = new Point(15, 280);
            btnGiaoDich.Name = "btnGiaoDich";
            btnGiaoDich.PrimaryColor = Color.White;
            btnGiaoDich.Size = new Size(220, 50);
            btnGiaoDich.SuccessColor = Color.FromArgb(103, 194, 58);
            btnGiaoDich.TabIndex = 3;
            btnGiaoDich.Text = "          Giao dịch";
            btnGiaoDich.TextColor = Color.FromArgb(51, 65, 85);
            btnGiaoDich.WarningColor = Color.FromArgb(230, 162, 60);
            btnGiaoDich.Click += btnGiaoDich_Click;
            // 
            // picGiaoDich
            // 
            picGiaoDich.BackColor = Color.Transparent;
            picGiaoDich.Image = (Image)resources.GetObject("picGiaoDich.Image");
            picGiaoDich.Location = new Point(15, 13);
            picGiaoDich.Name = "picGiaoDich";
            picGiaoDich.Size = new Size(24, 24);
            picGiaoDich.SizeMode = PictureBoxSizeMode.Zoom;
            picGiaoDich.TabIndex = 7;
            picGiaoDich.TabStop = false;
            // 
            // btnKhachHang
            // 
            btnKhachHang.BorderColor = Color.FromArgb(220, 223, 230);
            btnKhachHang.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnKhachHang.Controls.Add(picKhachHang);
            btnKhachHang.Cursor = Cursors.Hand;
            btnKhachHang.DangerColor = Color.FromArgb(245, 108, 108);
            btnKhachHang.DefaultColor = Color.FromArgb(255, 255, 255);
            btnKhachHang.Font = new Font("Segoe UI", 12F);
            btnKhachHang.HoverTextColor = Color.FromArgb(51, 65, 85);
            btnKhachHang.InfoColor = Color.FromArgb(144, 147, 153);
            btnKhachHang.Location = new Point(15, 220);
            btnKhachHang.Name = "btnKhachHang";
            btnKhachHang.PrimaryColor = Color.White;
            btnKhachHang.Size = new Size(220, 50);
            btnKhachHang.SuccessColor = Color.FromArgb(103, 194, 58);
            btnKhachHang.TabIndex = 2;
            btnKhachHang.Text = "          Khách hàng";
            btnKhachHang.TextColor = Color.FromArgb(51, 65, 85);
            btnKhachHang.WarningColor = Color.FromArgb(230, 162, 60);
            btnKhachHang.Click += btnKhachHang_Click;
            // 
            // picKhachHang
            // 
            picKhachHang.BackColor = Color.Transparent;
            picKhachHang.Image = (Image)resources.GetObject("picKhachHang.Image");
            picKhachHang.Location = new Point(15, 13);
            picKhachHang.Name = "picKhachHang";
            picKhachHang.Size = new Size(24, 24);
            picKhachHang.SizeMode = PictureBoxSizeMode.Zoom;
            picKhachHang.TabIndex = 6;
            picKhachHang.TabStop = false;
            // 
            // btnKhoHang
            // 
            btnKhoHang.BorderColor = Color.FromArgb(220, 223, 230);
            btnKhoHang.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnKhoHang.Controls.Add(picKhoHang);
            btnKhoHang.Cursor = Cursors.Hand;
            btnKhoHang.DangerColor = Color.FromArgb(245, 108, 108);
            btnKhoHang.DefaultColor = Color.FromArgb(255, 255, 255);
            btnKhoHang.Font = new Font("Segoe UI", 12F);
            btnKhoHang.HoverTextColor = Color.FromArgb(51, 65, 85);
            btnKhoHang.InfoColor = Color.FromArgb(144, 147, 153);
            btnKhoHang.Location = new Point(15, 160);
            btnKhoHang.Name = "btnKhoHang";
            btnKhoHang.PrimaryColor = Color.White;
            btnKhoHang.Size = new Size(220, 50);
            btnKhoHang.SuccessColor = Color.FromArgb(103, 194, 58);
            btnKhoHang.TabIndex = 1;
            btnKhoHang.Text = "          Kho hàng";
            btnKhoHang.TextColor = Color.FromArgb(51, 65, 85);
            btnKhoHang.WarningColor = Color.FromArgb(230, 162, 60);
            btnKhoHang.Click += btnKhoHang_Click;
            // 
            // picKhoHang
            // 
            picKhoHang.BackColor = Color.Transparent;
            picKhoHang.Image = (Image)resources.GetObject("picKhoHang.Image");
            picKhoHang.Location = new Point(15, 13);
            picKhoHang.Name = "picKhoHang";
            picKhoHang.Size = new Size(24, 24);
            picKhoHang.SizeMode = PictureBoxSizeMode.Zoom;
            picKhoHang.TabIndex = 5;
            picKhoHang.TabStop = false;
            // 
            // btnTongQuan
            // 
            btnTongQuan.BorderColor = Color.FromArgb(220, 223, 230);
            btnTongQuan.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnTongQuan.Controls.Add(picTongQuan);
            btnTongQuan.Cursor = Cursors.Hand;
            btnTongQuan.DangerColor = Color.FromArgb(245, 108, 108);
            btnTongQuan.DefaultColor = Color.FromArgb(255, 255, 255);
            btnTongQuan.Font = new Font("Segoe UI", 12F);
            btnTongQuan.HoverTextColor = Color.FromArgb(51, 65, 85);
            btnTongQuan.InfoColor = Color.FromArgb(144, 147, 153);
            btnTongQuan.Location = new Point(15, 100);
            btnTongQuan.Name = "btnTongQuan";
            btnTongQuan.PrimaryColor = Color.White;
            btnTongQuan.Size = new Size(220, 50);
            btnTongQuan.SuccessColor = Color.FromArgb(103, 194, 58);
            btnTongQuan.TabIndex = 0;
            btnTongQuan.Text = "          Tổng quan";
            btnTongQuan.TextColor = Color.FromArgb(51, 65, 85);
            btnTongQuan.WarningColor = Color.FromArgb(230, 162, 60);
            btnTongQuan.Click += btnTongQuan_Click;
            // 
            // picTongQuan
            // 
            picTongQuan.BackColor = Color.Transparent;
            picTongQuan.ImageLocation = "D:\\Dev\\vtc\\2Hand\\shop2nd\\dosi\\bin\\Debug\\net8.0-windows\\icon\\home.png";
            picTongQuan.InitialImage = null;
            picTongQuan.Location = new Point(15, 13);
            picTongQuan.Name = "picTongQuan";
            picTongQuan.Size = new Size(24, 24);
            picTongQuan.SizeMode = PictureBoxSizeMode.Zoom;
            picTongQuan.TabIndex = 4;
            picTongQuan.TabStop = false;
            picTongQuan.Click += picTongQuan_Click;
            // 
            // lblLogoSub
            // 
            lblLogoSub.AutoSize = true;
            lblLogoSub.Font = new Font("Segoe UI", 9F);
            lblLogoSub.ForeColor = Color.FromArgb(100, 116, 139);
            lblLogoSub.Location = new Point(73, 50);
            lblLogoSub.Name = "lblLogoSub";
            lblLogoSub.Size = new Size(99, 20);
            lblLogoSub.TabIndex = 3;
            lblLogoSub.Text = "Hệ thống kho";
            // 
            // lblLogoText
            // 
            lblLogoText.AutoSize = true;
            lblLogoText.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblLogoText.ForeColor = Color.FromArgb(30, 41, 59);
            lblLogoText.Location = new Point(70, 20);
            lblLogoText.Name = "lblLogoText";
            lblLogoText.Size = new Size(155, 32);
            lblLogoText.TabIndex = 2;
            lblLogoText.Text = "2Hand Store";
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.Image = (Image)resources.GetObject("picLogo.Image");
            picLogo.Location = new Point(12, 12);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(60, 60);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 1;
            picLogo.TabStop = false;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(248, 250, 252);
            panelMain.Controls.Add(panelContent);
            panelMain.Controls.Add(panelShadow);
            panelMain.Controls.Add(panelTopBar);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(250, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(850, 700);
            panelMain.TabIndex = 1;
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.FromArgb(248, 250, 252);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 61);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(850, 639);
            panelContent.TabIndex = 4;
            // 
            // panelShadow
            // 
            panelShadow.BackColor = Color.FromArgb(248, 250, 252);
            panelShadow.Dock = DockStyle.Top;
            panelShadow.Location = new Point(0, 56);
            panelShadow.Name = "panelShadow";
            panelShadow.Size = new Size(850, 5);
            panelShadow.TabIndex = 3;
            // 
            // panelTopBar
            // 
            panelTopBar.BackColor = Color.White;
            panelTopBar.Controls.Add(lblPageTitle);
            panelTopBar.Dock = DockStyle.Top;
            panelTopBar.Location = new Point(0, 0);
            panelTopBar.Name = "panelTopBar";
            panelTopBar.Size = new Size(850, 56);
            panelTopBar.TabIndex = 2;
            // 
            // lblPageTitle
            // 
            lblPageTitle.Dock = DockStyle.Fill;
            lblPageTitle.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            lblPageTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblPageTitle.Location = new Point(0, 0);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Padding = new Padding(24, 0, 0, 0);
            lblPageTitle.Size = new Size(850, 56);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "Tổng quan";
            lblPageTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(1100, 700);
            Controls.Add(panelMain);
            Controls.Add(panelSidebar);
            MinimumSize = new Size(1000, 600);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ thống Quản lý 2Hand Store";
            panelSidebar.ResumeLayout(false);
            panelSidebar.PerformLayout();
            btnPhanTich.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picPhanTich).EndInit();
            btnGiaoDich.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picGiaoDich).EndInit();
            btnKhachHang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picKhachHang).EndInit();
            btnKhoHang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picKhoHang).EndInit();
            btnTongQuan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picTongQuan).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            panelMain.ResumeLayout(false);
            panelTopBar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private ReaLTaiizor.Controls.HopeButton btnKhoHang;
        private ReaLTaiizor.Controls.HopeButton btnKhachHang;
        private ReaLTaiizor.Controls.HopeButton btnTongQuan;
        private ReaLTaiizor.Controls.HopeButton btnGiaoDich;
        private ReaLTaiizor.Controls.HopeButton btnPhanTich;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelTopBar;
        private System.Windows.Forms.Panel panelShadow;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblLogoText;
        private System.Windows.Forms.Label lblLogoSub;
        private System.Windows.Forms.PictureBox picTongQuan;
        private System.Windows.Forms.PictureBox picKhoHang;
        private System.Windows.Forms.PictureBox picKhachHang;
        private System.Windows.Forms.PictureBox picGiaoDich;
        private System.Windows.Forms.PictureBox picPhanTich;
    }
}