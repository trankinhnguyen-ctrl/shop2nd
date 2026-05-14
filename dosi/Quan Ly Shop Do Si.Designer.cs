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
            button1 = new Button();
            panelMain = new Panel();
            panelSidebar.SuspendLayout();
            SuspendLayout();
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(245, 245, 245);
            panelSidebar.Controls.Add(btnGiaoDich);
            panelSidebar.Controls.Add(btnKhachHang);
            panelSidebar.Controls.Add(btnKhoHang);
            panelSidebar.Controls.Add(button1);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Padding = new Padding(10);
            panelSidebar.Size = new Size(200, 700);
            panelSidebar.TabIndex = 0;
            // 
            // btnGiaoDich
            // 
            btnGiaoDich.Dock = DockStyle.Top;
            btnGiaoDich.Location = new Point(10, 190);
            btnGiaoDich.Name = "btnGiaoDich";
            btnGiaoDich.Size = new Size(180, 60);
            btnGiaoDich.TabIndex = 3;
            btnGiaoDich.Text = "Giao Dịch";
            btnGiaoDich.UseVisualStyleBackColor = true;
            btnGiaoDich.Click += this.btnGiaoDich_Click;
            // 
            // btnKhachHang
            // 
            btnKhachHang.Dock = DockStyle.Top;
            btnKhachHang.Location = new Point(10, 130);
            btnKhachHang.Name = "btnKhachHang";
            btnKhachHang.Size = new Size(180, 60);
            btnKhachHang.TabIndex = 2;
            btnKhachHang.Text = "Khách Hàng";
            btnKhachHang.UseVisualStyleBackColor = true;
            btnKhachHang.Click += btnKhachHang_Click;
            // 
            // btnKhoHang
            // 
            btnKhoHang.Dock = DockStyle.Top;
            btnKhoHang.Location = new Point(10, 70);
            btnKhoHang.Margin = new Padding(0, 10, 0, 0);
            btnKhoHang.Name = "btnKhoHang";
            btnKhoHang.Size = new Size(180, 60);
            btnKhoHang.TabIndex = 1;
            btnKhoHang.Text = "Kho Hàng";
            btnKhoHang.UseVisualStyleBackColor = true;
            btnKhoHang.Click += btnKhoHang_Click;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Top;
            button1.Location = new Point(10, 10);
            button1.Name = "button1";
            button1.Size = new Size(180, 60);
            button1.TabIndex = 0;
            button1.Text = "button2";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panelMain
            // 
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(200, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(900, 700);
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
            Text = "Quản Lý Cửa Hàng";
            panelSidebar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelSidebar;
        private Button btnKhoHang;
        private Button btnKhachHang;
        private Button button1;
        private Button btnGiaoDich;
        private Panel panelMain;
    }
}