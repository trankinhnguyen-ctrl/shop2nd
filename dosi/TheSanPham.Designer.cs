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
            ((System.ComponentModel.ISupportInitialize)HinhAnh).BeginInit();
            SuspendLayout();
            // 
            // HinhAnh
            // 
            HinhAnh.BackColor = Color.FromArgb(245, 245, 245);
            HinhAnh.Dock = DockStyle.Top;
            HinhAnh.ImageLocation = "";
            HinhAnh.Location = new Point(0, 0);
            HinhAnh.Name = "HinhAnh";
            HinhAnh.Size = new Size(180, 150);
            HinhAnh.SizeMode = PictureBoxSizeMode.Zoom;
            HinhAnh.TabIndex = 0;
            HinhAnh.TabStop = false;
            // 
            // TenSP
            // 
            TenSP.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            TenSP.Location = new Point(5, 155);
            TenSP.Name = "TenSP";
            TenSP.Size = new Size(170, 25);
            TenSP.TabIndex = 1;
            TenSP.Text = "Tên sản phẩm";
            // 
            // MaSP
            // 
            MaSP.ForeColor = Color.Gray;
            MaSP.Location = new Point(5, 180);
            MaSP.Name = "MaSP";
            MaSP.Size = new Size(100, 20);
            MaSP.TabIndex = 2;
            MaSP.Text = "Mã: SP001";
            // 
            // SL
            // 
            SL.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            SL.ForeColor = Color.FromArgb(37, 99, 235);
            SL.Location = new Point(110, 180);
            SL.Name = "SL";
            SL.Size = new Size(65, 20);
            SL.TabIndex = 3;
            SL.Text = "Kho: 10";
            SL.TextAlign = ContentAlignment.TopRight;
            // 
            // TheSanPham
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(SL);
            Controls.Add(MaSP);
            Controls.Add(TenSP);
            Controls.Add(HinhAnh);
            Cursor = Cursors.Hand;
            Name = "TheSanPham";
            Size = new Size(180, 210);
            ((System.ComponentModel.ISupportInitialize)HinhAnh).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox HinhAnh;
        private Label TenSP;
        private Label MaSP;
        private Label SL;
    }
}