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
            button4 = new Button();
            btnKhachHang = new Button();
            btnKhoHang = new Button();
            button1 = new Button();
            panelMain = new Panel();
            panelSidebar.SuspendLayout();
            SuspendLayout();

            panelSidebar.BackColor = Color.FromArgb(245, 245, 245);
            panelSidebar.Controls.Add(button4);
            panelSidebar.Controls.Add(btnKhachHang);
            panelSidebar.Controls.Add(btnKhoHang);
            panelSidebar.Controls.Add(button1);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Padding = new Padding(10);
            panelSidebar.Size = new Size(200, 700);
            panelSidebar.TabIndex = 0;

            button1.Dock = DockStyle.Top;
            button1.Location = new Point(10, 10);
            button1.Name = "button1";
            button1.Size = new Size(180, 60);
            button1.TabIndex = 0;
            button1.Text = "button2";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;

            btnKhoHang.Dock = DockStyle.Top;
            btnKhoHang.Location = new Point(10, 80);
            btnKhoHang.Margin = new Padding(0, 10, 0, 0);
            btnKhoHang.Name = "btnKhoHang";
            btnKhoHang.Size = new Size(180, 60);
            btnKhoHang.TabIndex = 1;
            btnKhoHang.Text = "Kho Hàng";
            btnKhoHang.UseVisualStyleBackColor = true;
            btnKhoHang.Click += btnKhoHang_Click;

            btnKhachHang.Dock = DockStyle.Top;
            btnKhachHang.Location = new Point(10, 150);
            btnKhachHang.Name = "btnKhachHang";
            btnKhachHang.Size = new Size(180, 60);
            btnKhachHang.TabIndex = 2;
            btnKhachHang.Text = "Khách Hàng";
            btnKhachHang.UseVisualStyleBackColor = true;
            btnKhachHang.Click += btnKhachHang_Click;

            button4.Dock = DockStyle.Top;
            button4.Location = new Point(10, 220);
            button4.Name = "button4";
            button4.Size = new Size(180, 60);
            button4.TabIndex = 3;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;

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
            Text = "Quản Lý Cửa Hàng";
            panelSidebar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelSidebar;
        private Button btnKhoHang;
        private Button btnKhachHang;
        private Button button1;
        private Button button4;
        private Panel panelMain;
    }
}