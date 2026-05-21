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
            panelSidebar = new System.Windows.Forms.Panel();
            picLogo = new System.Windows.Forms.PictureBox();
            lblLogoText = new System.Windows.Forms.Label();
            lblLogoSub = new System.Windows.Forms.Label();
            btnTongQuan = new ReaLTaiizor.Controls.HopeButton();
            picTongQuan = new System.Windows.Forms.PictureBox();
            btnKhoHang = new ReaLTaiizor.Controls.HopeButton();
            picKhoHang = new System.Windows.Forms.PictureBox();
            btnKhachHang = new ReaLTaiizor.Controls.HopeButton();
            picKhachHang = new System.Windows.Forms.PictureBox();
            btnGiaoDich = new ReaLTaiizor.Controls.HopeButton();
            picGiaoDich = new System.Windows.Forms.PictureBox();
            panelMain = new System.Windows.Forms.Panel();
            panelSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(picLogo)).BeginInit();
            btnTongQuan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(picTongQuan)).BeginInit();
            btnKhoHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(picKhoHang)).BeginInit();
            btnKhachHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(picKhachHang)).BeginInit();
            btnGiaoDich.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(picGiaoDich)).BeginInit();
            SuspendLayout();

            panelSidebar.BackColor = System.Drawing.Color.White;
            panelSidebar.Controls.Add(btnGiaoDich);
            panelSidebar.Controls.Add(btnKhachHang);
            panelSidebar.Controls.Add(btnKhoHang);
            panelSidebar.Controls.Add(btnTongQuan);
            panelSidebar.Controls.Add(lblLogoSub);
            panelSidebar.Controls.Add(lblLogoText);
            panelSidebar.Controls.Add(picLogo);
            panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            panelSidebar.Location = new System.Drawing.Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new System.Drawing.Size(250, 700);
            panelSidebar.TabIndex = 0;

            picLogo.BackColor = System.Drawing.Color.FromArgb(124, 58, 237); // Giữ màu tím chủ đạo
            picLogo.Location = new System.Drawing.Point(20, 20);
            picLogo.Name = "picLogo";
            picLogo.Size = new System.Drawing.Size(40, 40);
            picLogo.TabIndex = 1;
            picLogo.TabStop = false;

            lblLogoText.AutoSize = true;
            lblLogoText.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblLogoText.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblLogoText.Location = new System.Drawing.Point(70, 20);
            lblLogoText.Name = "lblLogoText";
            lblLogoText.Size = new System.Drawing.Size(150, 32);
            lblLogoText.TabIndex = 2;
            lblLogoText.Text = "2Hand Store";

            lblLogoSub.AutoSize = true;
            lblLogoSub.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblLogoSub.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblLogoSub.Location = new System.Drawing.Point(73, 50);
            lblLogoSub.Name = "lblLogoSub";
            lblLogoSub.Size = new System.Drawing.Size(95, 20);
            lblLogoSub.TabIndex = 3;
            lblLogoSub.Text = "Hệ thống kho";

            // btnTongQuan
            btnTongQuan.Controls.Add(picTongQuan);
            btnTongQuan.Cursor = System.Windows.Forms.Cursors.Hand;
            btnTongQuan.HoverTextColor = System.Drawing.Color.White;
            btnTongQuan.Location = new System.Drawing.Point(15, 100);
            btnTongQuan.Name = "btnTongQuan";
            btnTongQuan.PrimaryColor = System.Drawing.Color.FromArgb(124, 58, 237);
            btnTongQuan.Size = new System.Drawing.Size(220, 50);
            btnTongQuan.TabIndex = 0;
            btnTongQuan.Text = "          Tổng quan";
            btnTongQuan.TextColor = System.Drawing.Color.White;
            // Thuộc tính bo tròn mặc định của HopeButton
            btnTongQuan.Click += new System.EventHandler(btnTongQuan_Click);

            picTongQuan.BackColor = System.Drawing.Color.Transparent;
            picTongQuan.Location = new System.Drawing.Point(15, 13);
            picTongQuan.Name = "picTongQuan";
            picTongQuan.Size = new System.Drawing.Size(24, 24);
            picTongQuan.TabIndex = 4;
            picTongQuan.TabStop = false;

            // btnKhoHang
            btnKhoHang.Controls.Add(picKhoHang);
            btnKhoHang.Cursor = System.Windows.Forms.Cursors.Hand;
            btnKhoHang.HoverTextColor = System.Drawing.Color.FromArgb(124, 58, 237); // Hover text màu tím
            btnKhoHang.Location = new System.Drawing.Point(15, 160);
            btnKhoHang.Name = "btnKhoHang";
            btnKhoHang.PrimaryColor = System.Drawing.Color.White; // Nền trắng ban đầu
            btnKhoHang.Size = new System.Drawing.Size(220, 50);
            btnKhoHang.TabIndex = 1;
            btnKhoHang.Text = "          Kho hàng";
            btnKhoHang.TextColor = System.Drawing.Color.FromArgb(51, 65, 85);
            btnKhoHang.Click += new System.EventHandler(btnKhoHang_Click);

            picKhoHang.BackColor = System.Drawing.Color.Transparent;
            picKhoHang.Location = new System.Drawing.Point(15, 13);
            picKhoHang.Name = "picKhoHang";
            picKhoHang.Size = new System.Drawing.Size(24, 24);
            picKhoHang.TabIndex = 5;
            picKhoHang.TabStop = false;

            // btnKhachHang
            btnKhachHang.Controls.Add(picKhachHang);
            btnKhachHang.Cursor = System.Windows.Forms.Cursors.Hand;
            btnKhachHang.HoverTextColor = System.Drawing.Color.FromArgb(124, 58, 237);
            btnKhachHang.Location = new System.Drawing.Point(15, 220);
            btnKhachHang.Name = "btnKhachHang";
            btnKhachHang.PrimaryColor = System.Drawing.Color.White;
            btnKhachHang.Size = new System.Drawing.Size(220, 50);
            btnKhachHang.TabIndex = 2;
            btnKhachHang.Text = "          Khách hàng";
            btnKhachHang.TextColor = System.Drawing.Color.FromArgb(51, 65, 85);
            btnKhachHang.Click += new System.EventHandler(btnKhachHang_Click);

            picKhachHang.BackColor = System.Drawing.Color.Transparent;
            picKhachHang.Location = new System.Drawing.Point(15, 13);
            picKhachHang.Name = "picKhachHang";
            picKhachHang.Size = new System.Drawing.Size(24, 24);
            picKhachHang.TabIndex = 6;
            picKhachHang.TabStop = false;

            // btnGiaoDich
            btnGiaoDich.Controls.Add(picGiaoDich);
            btnGiaoDich.Cursor = System.Windows.Forms.Cursors.Hand;
            btnGiaoDich.HoverTextColor = System.Drawing.Color.FromArgb(124, 58, 237);
            btnGiaoDich.Location = new System.Drawing.Point(15, 280);
            btnGiaoDich.Name = "btnGiaoDich";
            btnGiaoDich.PrimaryColor = System.Drawing.Color.White;
            btnGiaoDich.Size = new System.Drawing.Size(220, 50);
            btnGiaoDich.TabIndex = 3;
            btnGiaoDich.Text = "          Giao dịch";
            btnGiaoDich.TextColor = System.Drawing.Color.FromArgb(51, 65, 85);
            btnGiaoDich.Click += new System.EventHandler(btnGiaoDich_Click);

            picGiaoDich.BackColor = System.Drawing.Color.Transparent;
            picGiaoDich.Location = new System.Drawing.Point(15, 13);
            picGiaoDich.Name = "picGiaoDich";
            picGiaoDich.Size = new System.Drawing.Size(24, 24);
            picGiaoDich.TabIndex = 7;
            picGiaoDich.TabStop = false;

            panelMain.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            panelMain.Location = new System.Drawing.Point(250, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new System.Drawing.Size(850, 700);
            panelMain.TabIndex = 1;

            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            ClientSize = new System.Drawing.Size(1100, 700);
            Controls.Add(panelMain);
            Controls.Add(panelSidebar);
            MinimumSize = new System.Drawing.Size(1000, 600);
            Name = "Form1";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Hệ thống Quản lý 2Hand Store";
            panelSidebar.ResumeLayout(false);
            panelSidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(picLogo)).EndInit();
            btnTongQuan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(picTongQuan)).EndInit();
            btnKhoHang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(picKhoHang)).EndInit();
            btnKhachHang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(picKhachHang)).EndInit();
            btnGiaoDich.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(picGiaoDich)).EndInit();
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