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
            panelSidebar.SuspendLayout();
            btnGiaoDich.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picGiaoDich).BeginInit();
            btnKhachHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picKhachHang).BeginInit();
            btnKhoHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picKhoHang).BeginInit();
            btnTongQuan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picTongQuan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            SuspendLayout();
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.White;
            panelSidebar.BackgroundImage = (Image)resources.GetObject("panelSidebar.BackgroundImage");
            panelSidebar.BackgroundImageLayout = ImageLayout.Center;
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
            // btnGiaoDich
            // 
            btnGiaoDich.BorderColor = Color.FromArgb(220, 223, 230);
            btnGiaoDich.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnGiaoDich.Controls.Add(picGiaoDich);
            btnGiaoDich.Cursor = Cursors.Hand;
            btnGiaoDich.DangerColor = Color.FromArgb(245, 108, 108);
            btnGiaoDich.DefaultColor = Color.FromArgb(255, 255, 255);
            btnGiaoDich.Font = new Font("Segoe UI", 12F);
            btnGiaoDich.HoverTextColor = Color.FromArgb(124, 58, 237);
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
            btnKhachHang.HoverTextColor = Color.FromArgb(124, 58, 237);
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
            btnKhoHang.HoverTextColor = Color.FromArgb(124, 58, 237);
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
            btnTongQuan.BackColor = Color.Fuchsia;
            btnTongQuan.BackgroundImage = (Image)resources.GetObject("btnTongQuan.BackgroundImage");
            btnTongQuan.BackgroundImageLayout = ImageLayout.Stretch;
            btnTongQuan.BorderColor = Color.FromArgb(220, 223, 230);
            btnTongQuan.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnTongQuan.Controls.Add(picTongQuan);
            btnTongQuan.Cursor = Cursors.Hand;
            btnTongQuan.DangerColor = Color.FromArgb(245, 108, 108);
            btnTongQuan.DefaultColor = Color.FromArgb(255, 255, 255);
            btnTongQuan.Font = new Font("Segoe UI", 12F);
            btnTongQuan.HoverTextColor = Color.White;
            btnTongQuan.InfoColor = Color.FromArgb(144, 147, 153);
            btnTongQuan.Location = new Point(15, 100);
            btnTongQuan.Name = "btnTongQuan";
            btnTongQuan.PrimaryColor = Color.FromArgb(124, 58, 237);
            btnTongQuan.Size = new Size(220, 50);
            btnTongQuan.SuccessColor = Color.FromArgb(103, 194, 58);
            btnTongQuan.TabIndex = 0;
            btnTongQuan.Text = "          Tổng quan";
            btnTongQuan.TextColor = Color.White;
            btnTongQuan.WarningColor = Color.FromArgb(230, 162, 60);
            btnTongQuan.Click += btnTongQuan_Click;
            // 
            // picTongQuan
            // 
            picTongQuan.BackColor = Color.Transparent;
            picTongQuan.ImageLocation = "D:\\Dev\\vtc\\2Hand\\shop2nd\\dosi\\bin\\Debug\\net8.0-windows\\icon\\home.png";
            picTongQuan.InitialImage = (Image)resources.GetObject("picTongQuan.InitialImage");
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
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(250, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(850, 700);
            panelMain.TabIndex = 1;
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
            btnGiaoDich.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picGiaoDich).EndInit();
            btnKhachHang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picKhachHang).EndInit();
            btnKhoHang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picKhoHang).EndInit();
            btnTongQuan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picTongQuan).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private ReaLTaiizor.Controls.HopeButton btnKhoHang;
        private ReaLTaiizor.Controls.HopeButton btnKhachHang;
        private ReaLTaiizor.Controls.HopeButton btnTongQuan;
        private ReaLTaiizor.Controls.HopeButton btnGiaoDich;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblLogoText;
        private System.Windows.Forms.Label lblLogoSub;
        private System.Windows.Forms.PictureBox picTongQuan;
        private System.Windows.Forms.PictureBox picKhoHang;
        private System.Windows.Forms.PictureBox picKhachHang;
        private System.Windows.Forms.PictureBox picGiaoDich;
    }
}