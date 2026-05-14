namespace dosi
{
    partial class SuaSanPhamForm
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
            lblMa = new Label();
            txt_MaSP = new TextBox();
            lblTen = new Label();
            txt_TenSP = new TextBox();
            lblGia = new Label();
            txt_GiaBan = new TextBox();
            lblSL = new Label();
            txt_SoLuong = new TextBox();
            pic_HinhSP = new PictureBox();
            btn_Luu = new Button();
            btn_Xoa = new Button();
            ((System.ComponentModel.ISupportInitialize)pic_HinhSP).BeginInit();
            SuspendLayout();
            // 
            // lblMa
            // 
            lblMa.Location = new Point(20, 20);
            lblMa.Name = "lblMa";
            lblMa.Size = new Size(100, 20);
            lblMa.TabIndex = 10;
            lblMa.Text = "Mã sản phẩm:";
            // 
            // txt_MaSP
            // 
            txt_MaSP.Location = new Point(20, 45);
            txt_MaSP.Name = "txt_MaSP";
            txt_MaSP.Size = new Size(250, 27);
            txt_MaSP.TabIndex = 9;
            // 
            // lblTen
            // 
            lblTen.Location = new Point(20, 85);
            lblTen.Name = "lblTen";
            lblTen.Size = new Size(100, 20);
            lblTen.TabIndex = 8;
            lblTen.Text = "Tên sản phẩm:";
            // 
            // txt_TenSP
            // 
            txt_TenSP.Location = new Point(20, 110);
            txt_TenSP.Name = "txt_TenSP";
            txt_TenSP.Size = new Size(250, 27);
            txt_TenSP.TabIndex = 7;
            // 
            // lblGia
            // 
            lblGia.Location = new Point(20, 150);
            lblGia.Name = "lblGia";
            lblGia.Size = new Size(100, 20);
            lblGia.TabIndex = 6;
            lblGia.Text = "Giá bán (đ):";
            // 
            // txt_GiaBan
            // 
            txt_GiaBan.Location = new Point(20, 175);
            txt_GiaBan.Name = "txt_GiaBan";
            txt_GiaBan.Size = new Size(250, 27);
            txt_GiaBan.TabIndex = 5;
            // 
            // lblSL
            // 
            lblSL.Location = new Point(20, 215);
            lblSL.Name = "lblSL";
            lblSL.Size = new Size(100, 20);
            lblSL.TabIndex = 4;
            lblSL.Text = "Số lượng tồn:";
            // 
            // txt_SoLuong
            // 
            txt_SoLuong.Location = new Point(20, 240);
            txt_SoLuong.Name = "txt_SoLuong";
            txt_SoLuong.Size = new Size(250, 27);
            txt_SoLuong.TabIndex = 3;
            // 
            // pic_HinhSP
            // 
            pic_HinhSP.BackColor = Color.Gainsboro;
            pic_HinhSP.BorderStyle = BorderStyle.FixedSingle;
            pic_HinhSP.Cursor = Cursors.Hand;
            pic_HinhSP.Location = new Point(290, 45);
            pic_HinhSP.Name = "pic_HinhSP";
            pic_HinhSP.Size = new Size(220, 222);
            pic_HinhSP.SizeMode = PictureBoxSizeMode.Zoom;
            pic_HinhSP.TabIndex = 2;
            pic_HinhSP.TabStop = false;
            pic_HinhSP.Click += pic_HinhSP_Click;
            // 
            // btn_Luu
            // 
            btn_Luu.BackColor = Color.LightGreen;
            btn_Luu.FlatStyle = FlatStyle.Flat;
            btn_Luu.Location = new Point(20, 290);
            btn_Luu.Name = "btn_Luu";
            btn_Luu.Size = new Size(230, 45);
            btn_Luu.TabIndex = 1;
            btn_Luu.Text = "Lưu thay đổi";
            btn_Luu.UseVisualStyleBackColor = false;
            btn_Luu.Click += btn_Luu_Click;
            // 
            // btn_Xoa
            // 
            btn_Xoa.BackColor = Color.Tomato;
            btn_Xoa.FlatStyle = FlatStyle.Flat;
            btn_Xoa.ForeColor = Color.White;
            btn_Xoa.Location = new Point(280, 290);
            btn_Xoa.Name = "btn_Xoa";
            btn_Xoa.Size = new Size(230, 45);
            btn_Xoa.TabIndex = 0;
            btn_Xoa.Text = "Xóa sản phẩm";
            btn_Xoa.UseVisualStyleBackColor = false;
            btn_Xoa.Click += btn_Xoa_Click;
            // 
            // SuaSanPhamForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(540, 360);
            Controls.Add(btn_Xoa);
            Controls.Add(btn_Luu);
            Controls.Add(pic_HinhSP);
            Controls.Add(txt_SoLuong);
            Controls.Add(lblSL);
            Controls.Add(txt_GiaBan);
            Controls.Add(lblGia);
            Controls.Add(txt_TenSP);
            Controls.Add(lblTen);
            Controls.Add(txt_MaSP);
            Controls.Add(lblMa);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "SuaSanPhamForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Sửa sản phẩm";
            Load += SuaSanPhamForm_Load;
            ((System.ComponentModel.ISupportInitialize)pic_HinhSP).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMa;
        private TextBox txt_MaSP;
        private Label lblTen;
        private TextBox txt_TenSP;
        private Label lblGia;
        private TextBox txt_GiaBan;
        private Label lblSL;
        private TextBox txt_SoLuong;
        private PictureBox pic_HinhSP;
        private Button btn_Luu;
        private Button btn_Xoa;
    }
}