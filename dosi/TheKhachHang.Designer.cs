namespace dosi
{
    partial class TheKhachHang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TheKhachHang));
            lblTen = new Label();
            lblSdt = new Label();
            picAvatar = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            SuspendLayout();
            // 
            // lblTen
            // 
            lblTen.AutoSize = true;
            lblTen.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTen.Location = new Point(70, 12);
            lblTen.Name = "lblTen";
            lblTen.Size = new Size(140, 25);
            lblTen.TabIndex = 0;
            lblTen.Text = "Nguyen Van A";
            // 
            // lblSdt
            // 
            lblSdt.AutoSize = true;
            lblSdt.ForeColor = Color.DimGray;
            lblSdt.Location = new Point(70, 40);
            lblSdt.Name = "lblSdt";
            lblSdt.Size = new Size(89, 20);
            lblSdt.TabIndex = 1;
            lblSdt.Text = "0912345678";
            // 
            // picAvatar
            // 
            picAvatar.BackColor = Color.LightSteelBlue;
            picAvatar.Image = (Image)resources.GetObject("picAvatar.Image");
            picAvatar.Location = new Point(12, 12);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(45, 45);
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picAvatar.TabIndex = 2;
            picAvatar.TabStop = false;
            // 
            // TheKhachHang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(picAvatar);
            Controls.Add(lblSdt);
            Controls.Add(lblTen);
            Cursor = Cursors.Hand;
            Margin = new Padding(0, 0, 0, 1);
            Name = "TheKhachHang";
            Padding = new Padding(5);
            Size = new Size(330, 75);
            Load += TheKhachHang_Load;
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTen;
        private Label lblSdt;
        private PictureBox picAvatar;
    }
}