namespace dosi
{
    partial class TheSanPham
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            HinhAnh = new PictureBox();
            TenSP = new Label();
            MaSP = new Label();
            SL = new Label();
            GiaBan = new Label();
            ((System.ComponentModel.ISupportInitialize)HinhAnh).BeginInit();
            SuspendLayout();
            // 
            // HinhAnh
            // 
            HinhAnh.BackColor = Color.FromArgb(248, 250, 252);
            HinhAnh.Dock = DockStyle.Top;
            HinhAnh.ImageLocation = "";
            HinhAnh.Location = new Point(0, 0);
            HinhAnh.Name = "HinhAnh";
            HinhAnh.Size = new Size(180, 135);
            HinhAnh.SizeMode = PictureBoxSizeMode.Zoom;
            HinhAnh.TabIndex = 0;
            HinhAnh.TabStop = false;
            // 
            // TenSP
            // 
            TenSP.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            TenSP.ForeColor = Color.FromArgb(15, 23, 42);
            TenSP.Location = new Point(10, 142);
            TenSP.Name = "TenSP";
            TenSP.Size = new Size(160, 23);
            TenSP.TabIndex = 1;
            TenSP.Text = "Tên sản phẩm";
            // 
            // MaSP
            // 
            MaSP.Font = new Font("Segoe UI", 8.5F);
            MaSP.ForeColor = Color.FromArgb(100, 116, 139);
            MaSP.Location = new Point(10, 167);
            MaSP.Name = "MaSP";
            MaSP.Size = new Size(95, 20);
            MaSP.TabIndex = 2;
            MaSP.Text = "SP001";
            // 
            // SL
            // 
            SL.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            SL.ForeColor = Color.FromArgb(79, 70, 229);
            SL.Location = new Point(105, 167);
            SL.Name = "SL";
            SL.Size = new Size(65, 20);
            SL.TabIndex = 3;
            SL.Text = "Tồn: 10";
            SL.TextAlign = ContentAlignment.TopRight;
            // 
            // GiaBan
            // 
            GiaBan.Font = new Font("Segoe UI Bold", 11F, FontStyle.Bold);
            GiaBan.ForeColor = Color.FromArgb(225, 29, 72);
            GiaBan.Location = new Point(10, 189);
            GiaBan.Name = "GiaBan";
            GiaBan.Size = new Size(160, 24);
            GiaBan.TabIndex = 4;
            GiaBan.Text = "0đ";
            GiaBan.TextAlign = ContentAlignment.BottomLeft;
            // 
            // TheSanPham
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BorderStyle = BorderStyle.None; // Bỏ viền mặc định để tự vẽ bo góc mịn hơn
            Controls.Add(GiaBan);
            Controls.Add(SL);
            Controls.Add(MaSP);
            Controls.Add(TenSP);
            Controls.Add(HinhAnh);
            Cursor = Cursors.Hand;
            Margin = new Padding(10);
            Name = "TheSanPham";
            Size = new Size(180, 220);
            ((System.ComponentModel.ISupportInitialize)HinhAnh).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox HinhAnh;
        private Label TenSP;
        private Label MaSP;
        private Label SL;
        private Label GiaBan;
    }
}