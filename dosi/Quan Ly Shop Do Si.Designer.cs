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
            panelSidebar = new Panel();
            btnGiaoDich = new Button();
            btnKhachHang = new Button();
            btnKhoHang = new Button();
            btnTongQuan = new Button();
            panelMain = new Panel();
            panelSidebar.SuspendLayout();
            SuspendLayout();

            panelSidebar.BackColor = Color.FromArgb(245, 245, 245);
            panelSidebar.Controls.Add(btnGiaoDich);
            panelSidebar.Controls.Add(btnKhachHang);
            panelSidebar.Controls.Add(btnKhoHang);
            panelSidebar.Controls.Add(btnTongQuan);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Padding = new Padding(10);
            panelSidebar.Size = new Size(200, 700);
            panelSidebar.TabIndex = 0;

            btnGiaoDich.Dock = DockStyle.Top;
            btnGiaoDich.FlatStyle = FlatStyle.Flat;
            btnGiaoDich.Location = new Point(10, 190);
            btnGiaoDich.Name = "btnGiaoDich";
            btnGiaoDich.Size = new Size(180, 60);
            btnGiaoDich.TabIndex = 3;
            btnGiaoDich.Text = "Giao Dịch";
            btnGiaoDich.UseVisualStyleBackColor = true;
            btnGiaoDich.Click += btnGiaoDich_Click;

            btnKhachHang.Dock = DockStyle.Top;
            btnKhachHang.FlatStyle = FlatStyle.Flat;
            btnKhachHang.Location = new Point(10, 130);
            btnKhachHang.Name = "btnKhachHang";
            btnKhachHang.Size = new Size(180, 60);
            btnKhachHang.TabIndex = 2;
            btnKhachHang.Text = "Khách Hàng";
            btnKhachHang.UseVisualStyleBackColor = true;
            btnKhachHang.Click += btnKhachHang_Click;

            btnKhoHang.Dock = DockStyle.Top;
            btnKhoHang.FlatStyle = FlatStyle.Flat;
            btnKhoHang.Location = new Point(10, 70);
            btnKhoHang.Name = "btnKhoHang";
            btnKhoHang.Size = new Size(180, 60);
            btnKhoHang.TabIndex = 1;
            btnKhoHang.Text = "Kho Hàng";
            btnKhoHang.UseVisualStyleBackColor = true;
            btnKhoHang.Click += btnKhoHang_Click;

            btnTongQuan.Dock = DockStyle.Top;
            btnTongQuan.FlatStyle = FlatStyle.Flat;
            btnTongQuan.Location = new Point(10, 10);
            btnTongQuan.Name = "btnTongQuan";
            btnTongQuan.Size = new Size(180, 60);
            btnTongQuan.TabIndex = 0;
            btnTongQuan.Text = "Tổng Quan";
            btnTongQuan.UseVisualStyleBackColor = true;
            btnTongQuan.Click += btnTongQuan_Click;

            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(200, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(900, 700);
            panelMain.TabIndex = 1;

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 700);
            Controls.Add(panelMain);
            Controls.Add(panelSidebar);
            MinimumSize = new Size(1000, 600);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ thống Quản lý 2Hand Store";
            panelSidebar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelSidebar;
        private Button btnKhoHang;
        private Button btnKhachHang;
        private Button btnTongQuan;
        private Button btnGiaoDich;
        private Panel panelMain;
    }
}