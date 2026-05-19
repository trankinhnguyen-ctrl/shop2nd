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
            btnKhachHang = new Button();
            btnGiaoDich = new Button();
            btnKhoHang = new Button();
            btnTongQuan = new Button();
            panelMain = new Panel();
            panelSidebar.SuspendLayout();
            SuspendLayout();
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(245, 245, 245);
            panelSidebar.Controls.Add(btnKhachHang);
            panelSidebar.Controls.Add(btnGiaoDich);
            panelSidebar.Controls.Add(btnKhoHang);
            panelSidebar.Controls.Add(btnTongQuan);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Padding = new Padding(10);
            panelSidebar.Size = new Size(202, 700);
            panelSidebar.TabIndex = 0;
            // 
            // btnKhachHang
            // 
            btnKhachHang.FlatStyle = FlatStyle.Flat;
            btnKhachHang.Location = new Point(8, 208);
            btnKhachHang.Name = "btnKhachHang";
            btnKhachHang.Size = new Size(182, 60);
            btnKhachHang.TabIndex = 2;
            btnKhachHang.Text = "Khách Hàng";
            btnKhachHang.UseVisualStyleBackColor = true;
            btnKhachHang.Click += btnKhachHang_Click;
            // 
            // btnGiaoDich
            // 
            btnGiaoDich.FlatStyle = FlatStyle.Flat;
            btnGiaoDich.Location = new Point(8, 142);
            btnGiaoDich.Name = "btnGiaoDich";
            btnGiaoDich.Size = new Size(182, 60);
            btnGiaoDich.TabIndex = 3;
            btnGiaoDich.Text = "Giao Dịch";
            btnGiaoDich.UseVisualStyleBackColor = true;
            btnGiaoDich.Click += btnGiaoDich_Click;
            // 
            // btnKhoHang
            // 
            btnKhoHang.FlatStyle = FlatStyle.Flat;
            btnKhoHang.Location = new Point(7, 76);
            btnKhoHang.Name = "btnKhoHang";
            btnKhoHang.Size = new Size(182, 60);
            btnKhoHang.TabIndex = 1;
            btnKhoHang.Text = "Kho Hàng";
            btnKhoHang.UseVisualStyleBackColor = true;
            btnKhoHang.Click += btnKhoHang_Click;
            // 
            // btnTongQuan
            // 
            btnTongQuan.FlatStyle = FlatStyle.Flat;
            btnTongQuan.Location = new Point(7, 10);
            btnTongQuan.Name = "btnTongQuan";
            btnTongQuan.Size = new Size(182, 60);
            btnTongQuan.TabIndex = 0;
            btnTongQuan.Text = "Tổng Quan";
            btnTongQuan.UseVisualStyleBackColor = true;
            btnTongQuan.Click += btnTongQuan_Click;
            // 
            // panelMain
            // 
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(202, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(898, 700);
            panelMain.TabIndex = 1;
            // 
            // Form1
            // 
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