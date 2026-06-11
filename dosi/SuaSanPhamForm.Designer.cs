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
            lbl_MaSPValue = new Label();
            lblPhanLoai = new Label();
            cbo_PhanLoai = new ComboBox();
            lblTen = new Label();
            txt_TenSP = new TextBox();
            lblGia = new Label();
            txt_GiaBan = new TextBox();
            lblGiaVon = new Label();
            txt_GiaVon = new TextBox();
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
            // lbl_MaSPValue
            //
            lbl_MaSPValue.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lbl_MaSPValue.ForeColor = Color.FromArgb(99, 102, 241);
            lbl_MaSPValue.Location = new Point(20, 42);
            lbl_MaSPValue.Name = "lbl_MaSPValue";
            lbl_MaSPValue.Size = new Size(250, 24);
            lbl_MaSPValue.TabIndex = 13;
            //
            // lblPhanLoai
            //
            lblPhanLoai.Location = new Point(20, 80);
            lblPhanLoai.Name = "lblPhanLoai";
            lblPhanLoai.Size = new Size(100, 20);
            lblPhanLoai.TabIndex = 11;
            lblPhanLoai.Text = "Phân loại:";
            //
            // cbo_PhanLoai
            //
            cbo_PhanLoai.BackColor = Color.FromArgb(248, 250, 252);
            cbo_PhanLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo_PhanLoai.FlatStyle = FlatStyle.Flat;
            cbo_PhanLoai.Font = new Font("Segoe UI", 10F);
            cbo_PhanLoai.ForeColor = Color.FromArgb(15, 23, 42);
            cbo_PhanLoai.Location = new Point(20, 103);
            cbo_PhanLoai.Name = "cbo_PhanLoai";
            cbo_PhanLoai.Size = new Size(250, 27);
            cbo_PhanLoai.TabIndex = 12;
            //
            // lblTen
            //
            lblTen.Location = new Point(20, 143);
            lblTen.Name = "lblTen";
            lblTen.Size = new Size(100, 20);
            lblTen.TabIndex = 8;
            lblTen.Text = "Tên sản phẩm:";
            //
            // txt_TenSP
            //
            txt_TenSP.Location = new Point(20, 166);
            txt_TenSP.Name = "txt_TenSP";
            txt_TenSP.Size = new Size(250, 27);
            txt_TenSP.TabIndex = 7;
            //
            // lblGia
            //
            lblGia.Location = new Point(20, 206);
            lblGia.Name = "lblGia";
            lblGia.Size = new Size(100, 20);
            lblGia.TabIndex = 6;
            lblGia.Text = "Giá bán (đ):";
            //
            // txt_GiaBan
            //
            txt_GiaBan.Location = new Point(20, 229);
            txt_GiaBan.Name = "txt_GiaBan";
            txt_GiaBan.Size = new Size(250, 27);
            txt_GiaBan.TabIndex = 5;
            //
            // lblGiaVon
            //
            lblGiaVon.Location = new Point(20, 269);
            lblGiaVon.Name = "lblGiaVon";
            lblGiaVon.Size = new Size(100, 20);
            lblGiaVon.TabIndex = 14;
            lblGiaVon.Text = "Giá vốn (đ):";
            //
            // txt_GiaVon
            //
            txt_GiaVon.Location = new Point(20, 292);
            txt_GiaVon.Name = "txt_GiaVon";
            txt_GiaVon.Size = new Size(250, 27);
            txt_GiaVon.TabIndex = 15;
            //
            // lblSL
            //
            lblSL.Location = new Point(20, 332);
            lblSL.Name = "lblSL";
            lblSL.Size = new Size(100, 20);
            lblSL.TabIndex = 4;
            lblSL.Text = "Số lượng tồn:";
            //
            // txt_SoLuong
            //
            txt_SoLuong.Location = new Point(20, 355);
            txt_SoLuong.Name = "txt_SoLuong";
            txt_SoLuong.Size = new Size(250, 27);
            txt_SoLuong.TabIndex = 3;
            //
            // pic_HinhSP
            //
            pic_HinhSP.BackColor = Color.Gainsboro;
            pic_HinhSP.BorderStyle = BorderStyle.FixedSingle;
            pic_HinhSP.Cursor = Cursors.Hand;
            pic_HinhSP.Location = new Point(290, 20);
            pic_HinhSP.Name = "pic_HinhSP";
            pic_HinhSP.Size = new Size(220, 280);
            pic_HinhSP.SizeMode = PictureBoxSizeMode.Zoom;
            pic_HinhSP.TabIndex = 2;
            pic_HinhSP.TabStop = false;
            pic_HinhSP.Click += pic_HinhSP_Click;
            //
            // btn_Luu
            //
            btn_Luu.BackColor = Color.LightGreen;
            btn_Luu.FlatStyle = FlatStyle.Flat;
            btn_Luu.Location = new Point(20, 405);
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
            btn_Xoa.Location = new Point(280, 405);
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
            ClientSize = new Size(540, 470);
            Controls.Add(btn_Xoa);
            Controls.Add(btn_Luu);
            Controls.Add(pic_HinhSP);
            Controls.Add(cbo_PhanLoai);
            Controls.Add(lblPhanLoai);
            Controls.Add(txt_SoLuong);
            Controls.Add(lblSL);
            Controls.Add(txt_GiaVon);
            Controls.Add(lblGiaVon);
            Controls.Add(txt_GiaBan);
            Controls.Add(lblGia);
            Controls.Add(txt_TenSP);
            Controls.Add(lblTen);
            Controls.Add(lbl_MaSPValue);
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
        private Label lbl_MaSPValue;
        private Label lblPhanLoai;
        private ComboBox cbo_PhanLoai;
        private Label lblTen;
        private TextBox txt_TenSP;
        private Label lblGia;
        private TextBox txt_GiaBan;
        private Label lblGiaVon;
        private TextBox txt_GiaVon;
        private Label lblSL;
        private TextBox txt_SoLuong;
        private PictureBox pic_HinhSP;
        private Button btn_Luu;
        private Button btn_Xoa;
    }
}