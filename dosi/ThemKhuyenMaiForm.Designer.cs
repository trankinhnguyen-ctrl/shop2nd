namespace dosi
{
    partial class ThemKhuyenMaiForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblSection_ThongTin = new Label();
            lbl_MaKM = new Label();
            txt_MaKM = new TextBox();
            lbl_TenKM = new Label();
            txt_TenKM = new TextBox();
            lbl_BatDau = new Label();
            dtp_BatDau = new DateTimePicker();
            lbl_KetThuc = new Label();
            dtp_KetThuc = new DateTimePicker();
            chk_GioiHanSL = new CheckBox();
            txt_SoLuong = new TextBox();
            sep1 = new Panel();
            lblSection_DieuKien = new Label();
            lbl_LoaiDK = new Label();
            cbo_LoaiDK = new ComboBox();
            txt_GiaTriDK = new TextBox();
            lbl_UnitDK = new Label();
            sep2 = new Panel();
            lblSection_MucGiam = new Label();
            lbl_LoaiGiam = new Label();
            cbo_LoaiGiam = new ComboBox();
            lbl_GiaTriGiam = new Label();
            txt_GiaTriGiam = new TextBox();
            lbl_GiamToiDa = new Label();
            txt_GiamToiDa = new TextBox();
            chk_TrangThai = new CheckBox();
            btn_Luu = new Button();
            btn_Xoa = new Button();
            btn_Huy = new Button();
            SuspendLayout();
            //
            // lblSection_ThongTin
            //
            lblSection_ThongTin.AutoSize = true;
            lblSection_ThongTin.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblSection_ThongTin.ForeColor = Color.FromArgb(99, 102, 241);
            lblSection_ThongTin.Location = new Point(20, 14);
            lblSection_ThongTin.Name = "lblSection_ThongTin";
            lblSection_ThongTin.TabIndex = 0;
            lblSection_ThongTin.Text = "THÔNG TIN KHUYẾN MÃI";
            //
            // lbl_MaKM
            //
            lbl_MaKM.AutoSize = true;
            lbl_MaKM.Font = new Font("Segoe UI", 9.5F);
            lbl_MaKM.ForeColor = Color.FromArgb(71, 85, 105);
            lbl_MaKM.Location = new Point(20, 46);
            lbl_MaKM.Name = "lbl_MaKM";
            lbl_MaKM.TabIndex = 1;
            lbl_MaKM.Text = "Mã khuyến mãi:";
            //
            // txt_MaKM
            //
            txt_MaKM.BackColor = Color.FromArgb(248, 250, 252);
            txt_MaKM.Font = new Font("Segoe UI", 9.5F);
            txt_MaKM.ForeColor = Color.FromArgb(15, 23, 42);
            txt_MaKM.Location = new Point(175, 43);
            txt_MaKM.Name = "txt_MaKM";
            txt_MaKM.Size = new Size(280, 27);
            txt_MaKM.TabIndex = 2;
            //
            // lbl_TenKM
            //
            lbl_TenKM.AutoSize = true;
            lbl_TenKM.Font = new Font("Segoe UI", 9.5F);
            lbl_TenKM.ForeColor = Color.FromArgb(71, 85, 105);
            lbl_TenKM.Location = new Point(20, 86);
            lbl_TenKM.Name = "lbl_TenKM";
            lbl_TenKM.TabIndex = 3;
            lbl_TenKM.Text = "Tên chương trình:";
            //
            // txt_TenKM
            //
            txt_TenKM.BackColor = Color.FromArgb(248, 250, 252);
            txt_TenKM.Font = new Font("Segoe UI", 9.5F);
            txt_TenKM.ForeColor = Color.FromArgb(15, 23, 42);
            txt_TenKM.Location = new Point(175, 83);
            txt_TenKM.Name = "txt_TenKM";
            txt_TenKM.Size = new Size(280, 27);
            txt_TenKM.TabIndex = 4;
            //
            // lbl_BatDau
            //
            lbl_BatDau.AutoSize = true;
            lbl_BatDau.Font = new Font("Segoe UI", 9.5F);
            lbl_BatDau.ForeColor = Color.FromArgb(71, 85, 105);
            lbl_BatDau.Location = new Point(20, 126);
            lbl_BatDau.Name = "lbl_BatDau";
            lbl_BatDau.TabIndex = 5;
            lbl_BatDau.Text = "Ngày bắt đầu:";
            //
            // dtp_BatDau
            //
            dtp_BatDau.Checked = false;
            dtp_BatDau.Font = new Font("Segoe UI", 9.5F);
            dtp_BatDau.Format = DateTimePickerFormat.Short;
            dtp_BatDau.Location = new Point(175, 123);
            dtp_BatDau.Name = "dtp_BatDau";
            dtp_BatDau.ShowCheckBox = true;
            dtp_BatDau.Size = new Size(200, 27);
            dtp_BatDau.TabIndex = 6;
            //
            // lbl_KetThuc
            //
            lbl_KetThuc.AutoSize = true;
            lbl_KetThuc.Font = new Font("Segoe UI", 9.5F);
            lbl_KetThuc.ForeColor = Color.FromArgb(71, 85, 105);
            lbl_KetThuc.Location = new Point(20, 166);
            lbl_KetThuc.Name = "lbl_KetThuc";
            lbl_KetThuc.TabIndex = 7;
            lbl_KetThuc.Text = "Ngày kết thúc:";
            //
            // dtp_KetThuc
            //
            dtp_KetThuc.Checked = false;
            dtp_KetThuc.Font = new Font("Segoe UI", 9.5F);
            dtp_KetThuc.Format = DateTimePickerFormat.Short;
            dtp_KetThuc.Location = new Point(175, 163);
            dtp_KetThuc.Name = "dtp_KetThuc";
            dtp_KetThuc.ShowCheckBox = true;
            dtp_KetThuc.Size = new Size(200, 27);
            dtp_KetThuc.TabIndex = 8;
            //
            // chk_GioiHanSL
            //
            chk_GioiHanSL.AutoSize = true;
            chk_GioiHanSL.Font = new Font("Segoe UI", 9.5F);
            chk_GioiHanSL.Location = new Point(20, 206);
            chk_GioiHanSL.Name = "chk_GioiHanSL";
            chk_GioiHanSL.TabIndex = 9;
            chk_GioiHanSL.Text = "Giới hạn số lượng:";
            chk_GioiHanSL.CheckedChanged += chk_GioiHanSL_CheckedChanged;
            //
            // txt_SoLuong
            //
            txt_SoLuong.BackColor = Color.FromArgb(248, 250, 252);
            txt_SoLuong.Font = new Font("Segoe UI", 9.5F);
            txt_SoLuong.ForeColor = Color.FromArgb(15, 23, 42);
            txt_SoLuong.Location = new Point(320, 203);
            txt_SoLuong.Name = "txt_SoLuong";
            txt_SoLuong.PlaceholderText = "Số lượng";
            txt_SoLuong.Size = new Size(135, 27);
            txt_SoLuong.TabIndex = 10;
            txt_SoLuong.Visible = false;
            //
            // sep1
            //
            sep1.BackColor = Color.FromArgb(226, 232, 240);
            sep1.Location = new Point(10, 238);
            sep1.Name = "sep1";
            sep1.Size = new Size(460, 1);
            sep1.TabIndex = 11;
            //
            // lblSection_DieuKien
            //
            lblSection_DieuKien.AutoSize = true;
            lblSection_DieuKien.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblSection_DieuKien.ForeColor = Color.FromArgb(99, 102, 241);
            lblSection_DieuKien.Location = new Point(20, 248);
            lblSection_DieuKien.Name = "lblSection_DieuKien";
            lblSection_DieuKien.TabIndex = 12;
            lblSection_DieuKien.Text = "ĐIỀU KIỆN ÁP DỤNG";
            //
            // lbl_LoaiDK
            //
            lbl_LoaiDK.AutoSize = true;
            lbl_LoaiDK.Font = new Font("Segoe UI", 9.5F);
            lbl_LoaiDK.ForeColor = Color.FromArgb(71, 85, 105);
            lbl_LoaiDK.Location = new Point(20, 280);
            lbl_LoaiDK.Name = "lbl_LoaiDK";
            lbl_LoaiDK.TabIndex = 13;
            lbl_LoaiDK.Text = "Loại điều kiện:";
            //
            // cbo_LoaiDK
            //
            cbo_LoaiDK.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo_LoaiDK.Font = new Font("Segoe UI", 9.5F);
            cbo_LoaiDK.Items.AddRange(new object[] {
                "Không có điều kiện",
                "Tổng tiền đơn hàng ≥",
                "Số lượng sản phẩm ≥",
                "Khách hàng lâu năm ≥"
            });
            cbo_LoaiDK.Location = new Point(175, 277);
            cbo_LoaiDK.Name = "cbo_LoaiDK";
            cbo_LoaiDK.Size = new Size(165, 27);
            cbo_LoaiDK.TabIndex = 14;
            cbo_LoaiDK.SelectedIndexChanged += CboLoaiDK_Changed;
            //
            // txt_GiaTriDK
            //
            txt_GiaTriDK.BackColor = Color.FromArgb(248, 250, 252);
            txt_GiaTriDK.Font = new Font("Segoe UI", 9.5F);
            txt_GiaTriDK.ForeColor = Color.FromArgb(15, 23, 42);
            txt_GiaTriDK.Location = new Point(345, 277);
            txt_GiaTriDK.Name = "txt_GiaTriDK";
            txt_GiaTriDK.PlaceholderText = "0";
            txt_GiaTriDK.Size = new Size(75, 27);
            txt_GiaTriDK.TabIndex = 15;
            txt_GiaTriDK.Visible = false;
            //
            // lbl_UnitDK
            //
            lbl_UnitDK.AutoSize = true;
            lbl_UnitDK.Font = new Font("Segoe UI", 9F);
            lbl_UnitDK.ForeColor = Color.FromArgb(100, 116, 139);
            lbl_UnitDK.Location = new Point(425, 280);
            lbl_UnitDK.Name = "lbl_UnitDK";
            lbl_UnitDK.TabIndex = 16;
            lbl_UnitDK.Visible = false;
            //
            // sep2
            //
            sep2.BackColor = Color.FromArgb(226, 232, 240);
            sep2.Location = new Point(10, 318);
            sep2.Name = "sep2";
            sep2.Size = new Size(460, 1);
            sep2.TabIndex = 17;
            //
            // lblSection_MucGiam
            //
            lblSection_MucGiam.AutoSize = true;
            lblSection_MucGiam.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblSection_MucGiam.ForeColor = Color.FromArgb(99, 102, 241);
            lblSection_MucGiam.Location = new Point(20, 328);
            lblSection_MucGiam.Name = "lblSection_MucGiam";
            lblSection_MucGiam.TabIndex = 18;
            lblSection_MucGiam.Text = "MỨC GIẢM GIÁ";
            //
            // lbl_LoaiGiam
            //
            lbl_LoaiGiam.AutoSize = true;
            lbl_LoaiGiam.Font = new Font("Segoe UI", 9.5F);
            lbl_LoaiGiam.ForeColor = Color.FromArgb(71, 85, 105);
            lbl_LoaiGiam.Location = new Point(20, 360);
            lbl_LoaiGiam.Name = "lbl_LoaiGiam";
            lbl_LoaiGiam.TabIndex = 19;
            lbl_LoaiGiam.Text = "Loại giảm:";
            //
            // cbo_LoaiGiam
            //
            cbo_LoaiGiam.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo_LoaiGiam.Font = new Font("Segoe UI", 9.5F);
            cbo_LoaiGiam.Items.AddRange(new object[] { "Giảm tiền mặt (đ)", "Giảm theo %" });
            cbo_LoaiGiam.Location = new Point(175, 357);
            cbo_LoaiGiam.Name = "cbo_LoaiGiam";
            cbo_LoaiGiam.Size = new Size(280, 27);
            cbo_LoaiGiam.TabIndex = 20;
            cbo_LoaiGiam.SelectedIndexChanged += CboLoaiGiam_Changed;
            //
            // lbl_GiaTriGiam
            //
            lbl_GiaTriGiam.AutoSize = true;
            lbl_GiaTriGiam.Font = new Font("Segoe UI", 9.5F);
            lbl_GiaTriGiam.ForeColor = Color.FromArgb(71, 85, 105);
            lbl_GiaTriGiam.Location = new Point(20, 400);
            lbl_GiaTriGiam.Name = "lbl_GiaTriGiam";
            lbl_GiaTriGiam.TabIndex = 21;
            lbl_GiaTriGiam.Text = "Giá trị giảm:";
            //
            // txt_GiaTriGiam
            //
            txt_GiaTriGiam.BackColor = Color.FromArgb(248, 250, 252);
            txt_GiaTriGiam.Font = new Font("Segoe UI", 9.5F);
            txt_GiaTriGiam.ForeColor = Color.FromArgb(15, 23, 42);
            txt_GiaTriGiam.Location = new Point(175, 397);
            txt_GiaTriGiam.Name = "txt_GiaTriGiam";
            txt_GiaTriGiam.PlaceholderText = "Nhập giá trị";
            txt_GiaTriGiam.Size = new Size(280, 27);
            txt_GiaTriGiam.TabIndex = 22;
            //
            // lbl_GiamToiDa
            //
            lbl_GiamToiDa.AutoSize = true;
            lbl_GiamToiDa.Font = new Font("Segoe UI", 9.5F);
            lbl_GiamToiDa.ForeColor = Color.FromArgb(71, 85, 105);
            lbl_GiamToiDa.Location = new Point(20, 440);
            lbl_GiamToiDa.Name = "lbl_GiamToiDa";
            lbl_GiamToiDa.TabIndex = 23;
            lbl_GiamToiDa.Text = "Giảm tối đa (đ):";
            lbl_GiamToiDa.Visible = false;
            //
            // txt_GiamToiDa
            //
            txt_GiamToiDa.BackColor = Color.FromArgb(248, 250, 252);
            txt_GiamToiDa.Font = new Font("Segoe UI", 9.5F);
            txt_GiamToiDa.ForeColor = Color.FromArgb(15, 23, 42);
            txt_GiamToiDa.Location = new Point(175, 437);
            txt_GiamToiDa.Name = "txt_GiamToiDa";
            txt_GiamToiDa.PlaceholderText = "Để trống = không giới hạn";
            txt_GiamToiDa.Size = new Size(280, 27);
            txt_GiamToiDa.TabIndex = 24;
            txt_GiamToiDa.Visible = false;
            //
            // chk_TrangThai
            //
            chk_TrangThai.AutoSize = true;
            chk_TrangThai.Checked = true;
            chk_TrangThai.CheckState = CheckState.Checked;
            chk_TrangThai.Font = new Font("Segoe UI", 9.5F);
            chk_TrangThai.Location = new Point(20, 482);
            chk_TrangThai.Name = "chk_TrangThai";
            chk_TrangThai.TabIndex = 25;
            chk_TrangThai.Text = "Đang hoạt động";
            //
            // btn_Luu
            //
            btn_Luu.BackColor = Color.FromArgb(22, 163, 74);
            btn_Luu.Cursor = Cursors.Hand;
            btn_Luu.FlatStyle = FlatStyle.Flat;
            btn_Luu.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btn_Luu.ForeColor = Color.White;
            btn_Luu.Location = new Point(20, 516);
            btn_Luu.Name = "btn_Luu";
            btn_Luu.Size = new Size(220, 40);
            btn_Luu.TabIndex = 26;
            btn_Luu.Text = "Thêm khuyến mãi";
            btn_Luu.UseVisualStyleBackColor = false;
            btn_Luu.FlatAppearance.BorderSize = 0;
            btn_Luu.Click += BtnLuu_Click;
            //
            // btn_Xoa
            //
            btn_Xoa.BackColor = Color.FromArgb(239, 68, 68);
            btn_Xoa.Cursor = Cursors.Hand;
            btn_Xoa.FlatStyle = FlatStyle.Flat;
            btn_Xoa.Font = new Font("Segoe UI", 10F);
            btn_Xoa.ForeColor = Color.White;
            btn_Xoa.Location = new Point(220, 516);
            btn_Xoa.Name = "btn_Xoa";
            btn_Xoa.Size = new Size(90, 40);
            btn_Xoa.TabIndex = 27;
            btn_Xoa.Text = "Xóa";
            btn_Xoa.UseVisualStyleBackColor = false;
            btn_Xoa.Visible = false;
            btn_Xoa.FlatAppearance.BorderSize = 0;
            btn_Xoa.Click += BtnXoa_Click;
            //
            // btn_Huy
            //
            btn_Huy.BackColor = Color.FromArgb(241, 245, 249);
            btn_Huy.Cursor = Cursors.Hand;
            btn_Huy.DialogResult = DialogResult.Cancel;
            btn_Huy.FlatStyle = FlatStyle.Flat;
            btn_Huy.Font = new Font("Segoe UI", 10F);
            btn_Huy.ForeColor = Color.FromArgb(51, 65, 85);
            btn_Huy.Location = new Point(250, 516);
            btn_Huy.Name = "btn_Huy";
            btn_Huy.Size = new Size(140, 40);
            btn_Huy.TabIndex = 28;
            btn_Huy.Text = "Hủy";
            btn_Huy.UseVisualStyleBackColor = false;
            btn_Huy.FlatAppearance.BorderSize = 1;
            btn_Huy.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            //
            // ThemKhuyenMaiForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(480, 570);
            Controls.Add(btn_Huy);
            Controls.Add(btn_Xoa);
            Controls.Add(btn_Luu);
            Controls.Add(chk_TrangThai);
            Controls.Add(txt_GiamToiDa);
            Controls.Add(lbl_GiamToiDa);
            Controls.Add(txt_GiaTriGiam);
            Controls.Add(lbl_GiaTriGiam);
            Controls.Add(cbo_LoaiGiam);
            Controls.Add(lbl_LoaiGiam);
            Controls.Add(lblSection_MucGiam);
            Controls.Add(sep2);
            Controls.Add(lbl_UnitDK);
            Controls.Add(txt_GiaTriDK);
            Controls.Add(cbo_LoaiDK);
            Controls.Add(lbl_LoaiDK);
            Controls.Add(lblSection_DieuKien);
            Controls.Add(sep1);
            Controls.Add(txt_SoLuong);
            Controls.Add(chk_GioiHanSL);
            Controls.Add(dtp_KetThuc);
            Controls.Add(lbl_KetThuc);
            Controls.Add(dtp_BatDau);
            Controls.Add(lbl_BatDau);
            Controls.Add(txt_TenKM);
            Controls.Add(lbl_TenKM);
            Controls.Add(txt_MaKM);
            Controls.Add(lbl_MaKM);
            Controls.Add(lblSection_ThongTin);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "ThemKhuyenMaiForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thêm khuyến mãi mới";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSection_ThongTin;
        private Label lbl_MaKM;
        private TextBox txt_MaKM;
        private Label lbl_TenKM;
        private TextBox txt_TenKM;
        private Label lbl_BatDau;
        private DateTimePicker dtp_BatDau;
        private Label lbl_KetThuc;
        private DateTimePicker dtp_KetThuc;
        private CheckBox chk_GioiHanSL;
        private TextBox txt_SoLuong;
        private Panel sep1;
        private Label lblSection_DieuKien;
        private Label lbl_LoaiDK;
        private ComboBox cbo_LoaiDK;
        private TextBox txt_GiaTriDK;
        private Label lbl_UnitDK;
        private Panel sep2;
        private Label lblSection_MucGiam;
        private Label lbl_LoaiGiam;
        private ComboBox cbo_LoaiGiam;
        private Label lbl_GiaTriGiam;
        private TextBox txt_GiaTriGiam;
        private Label lbl_GiamToiDa;
        private TextBox txt_GiamToiDa;
        private CheckBox chk_TrangThai;
        private Button btn_Luu;
        private Button btn_Xoa;
        private Button btn_Huy;
    }
}
