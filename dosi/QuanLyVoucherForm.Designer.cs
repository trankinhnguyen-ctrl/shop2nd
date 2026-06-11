namespace dosi
{
    partial class QuanLyVoucherForm
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
            pnlLeft = new Panel();
            lblDanhSach = new Label();
            pnlVoucherList = new Panel();
            btnThemMoi = new Button();
            pnlDivider = new Panel();
            pnlRight = new Panel();
            lblFormTitle = new Label();
            sep1 = new Panel();
            lblSection1 = new Label();
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
            sep2 = new Panel();
            lblSection2 = new Label();
            lbl_LoaiDK = new Label();
            cbo_LoaiDK = new ComboBox();
            txt_GiaTriDK = new TextBox();
            lbl_UnitDK = new Label();
            sep3 = new Panel();
            lblSection3 = new Label();
            lbl_LoaiGiam = new Label();
            cbo_LoaiGiam = new ComboBox();
            lbl_GiaTriGiam = new Label();
            txt_GiaTriGiam = new TextBox();
            lbl_GiamToiDa = new Label();
            txt_GiamToiDa = new TextBox();
            chk_TrangThai = new CheckBox();
            btn_Luu = new Button();
            btn_Xoa = new Button();
            btnReset = new Button();
            btnDong = new Button();
            pnlLeft.SuspendLayout();
            pnlRight.SuspendLayout();
            SuspendLayout();
            //
            // pnlLeft
            //
            pnlLeft.BackColor = Color.FromArgb(248, 250, 252);
            pnlLeft.Controls.Add(lblDanhSach);
            pnlLeft.Controls.Add(pnlVoucherList);
            pnlLeft.Controls.Add(btnThemMoi);
            pnlLeft.Location = new Point(0, 0);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(220, 614);
            pnlLeft.TabIndex = 0;
            //
            // lblDanhSach
            //
            lblDanhSach.AutoSize = true;
            lblDanhSach.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblDanhSach.ForeColor = Color.FromArgb(51, 65, 85);
            lblDanhSach.Location = new Point(12, 14);
            lblDanhSach.Name = "lblDanhSach";
            lblDanhSach.TabIndex = 0;
            lblDanhSach.Text = "Danh sách khuyến mãi";
            //
            // pnlVoucherList
            //
            pnlVoucherList.AutoScroll = true;
            pnlVoucherList.BackColor = Color.FromArgb(248, 250, 252);
            pnlVoucherList.Location = new Point(0, 42);
            pnlVoucherList.Name = "pnlVoucherList";
            pnlVoucherList.Size = new Size(220, 530);
            pnlVoucherList.TabIndex = 1;
            //
            // btnThemMoi
            //
            btnThemMoi.BackColor = Color.FromArgb(99, 102, 241);
            btnThemMoi.Cursor = Cursors.Hand;
            btnThemMoi.FlatStyle = FlatStyle.Flat;
            btnThemMoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnThemMoi.ForeColor = Color.White;
            btnThemMoi.Location = new Point(8, 578);
            btnThemMoi.Name = "btnThemMoi";
            btnThemMoi.Size = new Size(204, 28);
            btnThemMoi.TabIndex = 2;
            btnThemMoi.Text = "+ Thêm mới";
            btnThemMoi.UseVisualStyleBackColor = false;
            btnThemMoi.FlatAppearance.BorderSize = 0;
            btnThemMoi.Click += btnThemMoi_Click;
            //
            // pnlDivider
            //
            pnlDivider.BackColor = Color.FromArgb(226, 232, 240);
            pnlDivider.Location = new Point(220, 0);
            pnlDivider.Name = "pnlDivider";
            pnlDivider.Size = new Size(1, 614);
            pnlDivider.TabIndex = 1;
            //
            // pnlRight
            //
            pnlRight.BackColor = Color.White;
            pnlRight.Controls.Add(lblFormTitle);
            pnlRight.Controls.Add(sep1);
            pnlRight.Controls.Add(lblSection1);
            pnlRight.Controls.Add(lbl_MaKM);
            pnlRight.Controls.Add(txt_MaKM);
            pnlRight.Controls.Add(lbl_TenKM);
            pnlRight.Controls.Add(txt_TenKM);
            pnlRight.Controls.Add(lbl_BatDau);
            pnlRight.Controls.Add(dtp_BatDau);
            pnlRight.Controls.Add(lbl_KetThuc);
            pnlRight.Controls.Add(dtp_KetThuc);
            pnlRight.Controls.Add(chk_GioiHanSL);
            pnlRight.Controls.Add(txt_SoLuong);
            pnlRight.Controls.Add(sep2);
            pnlRight.Controls.Add(lblSection2);
            pnlRight.Controls.Add(lbl_LoaiDK);
            pnlRight.Controls.Add(cbo_LoaiDK);
            pnlRight.Controls.Add(txt_GiaTriDK);
            pnlRight.Controls.Add(lbl_UnitDK);
            pnlRight.Controls.Add(sep3);
            pnlRight.Controls.Add(lblSection3);
            pnlRight.Controls.Add(lbl_LoaiGiam);
            pnlRight.Controls.Add(cbo_LoaiGiam);
            pnlRight.Controls.Add(lbl_GiaTriGiam);
            pnlRight.Controls.Add(txt_GiaTriGiam);
            pnlRight.Controls.Add(lbl_GiamToiDa);
            pnlRight.Controls.Add(txt_GiamToiDa);
            pnlRight.Controls.Add(chk_TrangThai);
            pnlRight.Controls.Add(btn_Luu);
            pnlRight.Controls.Add(btn_Xoa);
            pnlRight.Controls.Add(btnReset);
            pnlRight.Controls.Add(btnDong);
            pnlRight.Location = new Point(221, 0);
            pnlRight.Name = "pnlRight";
            pnlRight.Size = new Size(679, 614);
            pnlRight.TabIndex = 2;
            //
            // lblFormTitle
            //
            lblFormTitle.AutoSize = true;
            lblFormTitle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblFormTitle.ForeColor = Color.FromArgb(99, 102, 241);
            lblFormTitle.Location = new Point(16, 16);
            lblFormTitle.Name = "lblFormTitle";
            lblFormTitle.TabIndex = 0;
            lblFormTitle.Text = "THÊM KHUYẾN MÃI MỚI";
            //
            // sep1
            //
            sep1.BackColor = Color.FromArgb(226, 232, 240);
            sep1.Location = new Point(16, 44);
            sep1.Name = "sep1";
            sep1.Size = new Size(627, 1);
            sep1.TabIndex = 1;
            //
            // lblSection1
            //
            lblSection1.AutoSize = true;
            lblSection1.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblSection1.ForeColor = Color.FromArgb(100, 116, 139);
            lblSection1.Location = new Point(16, 54);
            lblSection1.Name = "lblSection1";
            lblSection1.TabIndex = 2;
            lblSection1.Text = "THÔNG TIN KHUYẾN MÃI";
            //
            // lbl_MaKM
            //
            lbl_MaKM.AutoSize = true;
            lbl_MaKM.Font = new Font("Segoe UI", 9.5F);
            lbl_MaKM.ForeColor = Color.FromArgb(71, 85, 105);
            lbl_MaKM.Location = new Point(16, 82);
            lbl_MaKM.Name = "lbl_MaKM";
            lbl_MaKM.TabIndex = 3;
            lbl_MaKM.Text = "Mã khuyến mãi:";
            //
            // txt_MaKM
            //
            txt_MaKM.BackColor = Color.FromArgb(248, 250, 252);
            txt_MaKM.Font = new Font("Segoe UI", 9.5F);
            txt_MaKM.ForeColor = Color.FromArgb(15, 23, 42);
            txt_MaKM.Location = new Point(166, 79);
            txt_MaKM.Name = "txt_MaKM";
            txt_MaKM.Size = new Size(477, 27);
            txt_MaKM.TabIndex = 4;
            //
            // lbl_TenKM
            //
            lbl_TenKM.AutoSize = true;
            lbl_TenKM.Font = new Font("Segoe UI", 9.5F);
            lbl_TenKM.ForeColor = Color.FromArgb(71, 85, 105);
            lbl_TenKM.Location = new Point(16, 120);
            lbl_TenKM.Name = "lbl_TenKM";
            lbl_TenKM.TabIndex = 5;
            lbl_TenKM.Text = "Tên chương trình:";
            //
            // txt_TenKM
            //
            txt_TenKM.BackColor = Color.FromArgb(248, 250, 252);
            txt_TenKM.Font = new Font("Segoe UI", 9.5F);
            txt_TenKM.ForeColor = Color.FromArgb(15, 23, 42);
            txt_TenKM.Location = new Point(166, 117);
            txt_TenKM.Name = "txt_TenKM";
            txt_TenKM.Size = new Size(477, 27);
            txt_TenKM.TabIndex = 6;
            //
            // lbl_BatDau
            //
            lbl_BatDau.AutoSize = true;
            lbl_BatDau.Font = new Font("Segoe UI", 9.5F);
            lbl_BatDau.ForeColor = Color.FromArgb(71, 85, 105);
            lbl_BatDau.Location = new Point(16, 158);
            lbl_BatDau.Name = "lbl_BatDau";
            lbl_BatDau.TabIndex = 7;
            lbl_BatDau.Text = "Ngày bắt đầu:";
            //
            // dtp_BatDau
            //
            dtp_BatDau.Checked = false;
            dtp_BatDau.Font = new Font("Segoe UI", 9.5F);
            dtp_BatDau.Format = DateTimePickerFormat.Short;
            dtp_BatDau.Location = new Point(166, 155);
            dtp_BatDau.Name = "dtp_BatDau";
            dtp_BatDau.ShowCheckBox = true;
            dtp_BatDau.Size = new Size(215, 27);
            dtp_BatDau.TabIndex = 8;
            //
            // lbl_KetThuc
            //
            lbl_KetThuc.AutoSize = true;
            lbl_KetThuc.Font = new Font("Segoe UI", 9.5F);
            lbl_KetThuc.ForeColor = Color.FromArgb(71, 85, 105);
            lbl_KetThuc.Location = new Point(16, 196);
            lbl_KetThuc.Name = "lbl_KetThuc";
            lbl_KetThuc.TabIndex = 9;
            lbl_KetThuc.Text = "Ngày kết thúc:";
            //
            // dtp_KetThuc
            //
            dtp_KetThuc.Checked = false;
            dtp_KetThuc.Font = new Font("Segoe UI", 9.5F);
            dtp_KetThuc.Format = DateTimePickerFormat.Short;
            dtp_KetThuc.Location = new Point(166, 193);
            dtp_KetThuc.Name = "dtp_KetThuc";
            dtp_KetThuc.ShowCheckBox = true;
            dtp_KetThuc.Size = new Size(215, 27);
            dtp_KetThuc.TabIndex = 10;
            //
            // chk_GioiHanSL
            //
            chk_GioiHanSL.AutoSize = true;
            chk_GioiHanSL.Font = new Font("Segoe UI", 9.5F);
            chk_GioiHanSL.Location = new Point(16, 232);
            chk_GioiHanSL.Name = "chk_GioiHanSL";
            chk_GioiHanSL.TabIndex = 11;
            chk_GioiHanSL.Text = "Giới hạn số lượng:";
            chk_GioiHanSL.CheckedChanged += chk_GioiHanSL_CheckedChanged;
            //
            // txt_SoLuong
            //
            txt_SoLuong.BackColor = Color.FromArgb(248, 250, 252);
            txt_SoLuong.Font = new Font("Segoe UI", 9.5F);
            txt_SoLuong.ForeColor = Color.FromArgb(15, 23, 42);
            txt_SoLuong.Location = new Point(351, 229);
            txt_SoLuong.Name = "txt_SoLuong";
            txt_SoLuong.PlaceholderText = "Số lượng";
            txt_SoLuong.Size = new Size(130, 27);
            txt_SoLuong.TabIndex = 12;
            txt_SoLuong.Visible = false;
            //
            // sep2
            //
            sep2.BackColor = Color.FromArgb(226, 232, 240);
            sep2.Location = new Point(16, 268);
            sep2.Name = "sep2";
            sep2.Size = new Size(627, 1);
            sep2.TabIndex = 13;
            //
            // lblSection2
            //
            lblSection2.AutoSize = true;
            lblSection2.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblSection2.ForeColor = Color.FromArgb(100, 116, 139);
            lblSection2.Location = new Point(16, 278);
            lblSection2.Name = "lblSection2";
            lblSection2.TabIndex = 14;
            lblSection2.Text = "ĐIỀU KIỆN ÁP DỤNG";
            //
            // lbl_LoaiDK
            //
            lbl_LoaiDK.AutoSize = true;
            lbl_LoaiDK.Font = new Font("Segoe UI", 9.5F);
            lbl_LoaiDK.ForeColor = Color.FromArgb(71, 85, 105);
            lbl_LoaiDK.Location = new Point(16, 306);
            lbl_LoaiDK.Name = "lbl_LoaiDK";
            lbl_LoaiDK.TabIndex = 15;
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
            cbo_LoaiDK.Location = new Point(166, 303);
            cbo_LoaiDK.Name = "cbo_LoaiDK";
            cbo_LoaiDK.Size = new Size(190, 27);
            cbo_LoaiDK.TabIndex = 16;
            cbo_LoaiDK.SelectedIndexChanged += cbo_LoaiDK_SelectedIndexChanged;
            //
            // txt_GiaTriDK
            //
            txt_GiaTriDK.BackColor = Color.FromArgb(248, 250, 252);
            txt_GiaTriDK.Font = new Font("Segoe UI", 9.5F);
            txt_GiaTriDK.ForeColor = Color.FromArgb(15, 23, 42);
            txt_GiaTriDK.Location = new Point(362, 303);
            txt_GiaTriDK.Name = "txt_GiaTriDK";
            txt_GiaTriDK.PlaceholderText = "0";
            txt_GiaTriDK.Size = new Size(82, 27);
            txt_GiaTriDK.TabIndex = 17;
            txt_GiaTriDK.Visible = false;
            //
            // lbl_UnitDK
            //
            lbl_UnitDK.AutoSize = true;
            lbl_UnitDK.Font = new Font("Segoe UI", 9F);
            lbl_UnitDK.ForeColor = Color.FromArgb(100, 116, 139);
            lbl_UnitDK.Location = new Point(450, 306);
            lbl_UnitDK.Name = "lbl_UnitDK";
            lbl_UnitDK.TabIndex = 18;
            lbl_UnitDK.Visible = false;
            //
            // sep3
            //
            sep3.BackColor = Color.FromArgb(226, 232, 240);
            sep3.Location = new Point(16, 341);
            sep3.Name = "sep3";
            sep3.Size = new Size(627, 1);
            sep3.TabIndex = 19;
            //
            // lblSection3
            //
            lblSection3.AutoSize = true;
            lblSection3.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblSection3.ForeColor = Color.FromArgb(100, 116, 139);
            lblSection3.Location = new Point(16, 351);
            lblSection3.Name = "lblSection3";
            lblSection3.TabIndex = 20;
            lblSection3.Text = "MỨC GIẢM GIÁ";
            //
            // lbl_LoaiGiam
            //
            lbl_LoaiGiam.AutoSize = true;
            lbl_LoaiGiam.Font = new Font("Segoe UI", 9.5F);
            lbl_LoaiGiam.ForeColor = Color.FromArgb(71, 85, 105);
            lbl_LoaiGiam.Location = new Point(16, 379);
            lbl_LoaiGiam.Name = "lbl_LoaiGiam";
            lbl_LoaiGiam.TabIndex = 21;
            lbl_LoaiGiam.Text = "Loại giảm:";
            //
            // cbo_LoaiGiam
            //
            cbo_LoaiGiam.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo_LoaiGiam.Font = new Font("Segoe UI", 9.5F);
            cbo_LoaiGiam.Items.AddRange(new object[] { "Giảm tiền mặt (đ)", "Giảm theo %" });
            cbo_LoaiGiam.Location = new Point(166, 376);
            cbo_LoaiGiam.Name = "cbo_LoaiGiam";
            cbo_LoaiGiam.Size = new Size(477, 27);
            cbo_LoaiGiam.TabIndex = 22;
            cbo_LoaiGiam.SelectedIndexChanged += cbo_LoaiGiam_SelectedIndexChanged;
            //
            // lbl_GiaTriGiam
            //
            lbl_GiaTriGiam.AutoSize = true;
            lbl_GiaTriGiam.Font = new Font("Segoe UI", 9.5F);
            lbl_GiaTriGiam.ForeColor = Color.FromArgb(71, 85, 105);
            lbl_GiaTriGiam.Location = new Point(16, 417);
            lbl_GiaTriGiam.Name = "lbl_GiaTriGiam";
            lbl_GiaTriGiam.TabIndex = 23;
            lbl_GiaTriGiam.Text = "Giá trị giảm:";
            //
            // txt_GiaTriGiam
            //
            txt_GiaTriGiam.BackColor = Color.FromArgb(248, 250, 252);
            txt_GiaTriGiam.Font = new Font("Segoe UI", 9.5F);
            txt_GiaTriGiam.ForeColor = Color.FromArgb(15, 23, 42);
            txt_GiaTriGiam.Location = new Point(166, 414);
            txt_GiaTriGiam.Name = "txt_GiaTriGiam";
            txt_GiaTriGiam.PlaceholderText = "Nhập giá trị";
            txt_GiaTriGiam.Size = new Size(477, 27);
            txt_GiaTriGiam.TabIndex = 24;
            //
            // lbl_GiamToiDa
            //
            lbl_GiamToiDa.AutoSize = true;
            lbl_GiamToiDa.Font = new Font("Segoe UI", 9.5F);
            lbl_GiamToiDa.ForeColor = Color.FromArgb(71, 85, 105);
            lbl_GiamToiDa.Location = new Point(16, 455);
            lbl_GiamToiDa.Name = "lbl_GiamToiDa";
            lbl_GiamToiDa.TabIndex = 25;
            lbl_GiamToiDa.Text = "Giảm tối đa (đ):";
            lbl_GiamToiDa.Visible = false;
            //
            // txt_GiamToiDa
            //
            txt_GiamToiDa.BackColor = Color.FromArgb(248, 250, 252);
            txt_GiamToiDa.Font = new Font("Segoe UI", 9.5F);
            txt_GiamToiDa.ForeColor = Color.FromArgb(15, 23, 42);
            txt_GiamToiDa.Location = new Point(166, 452);
            txt_GiamToiDa.Name = "txt_GiamToiDa";
            txt_GiamToiDa.PlaceholderText = "Để trống = không giới hạn";
            txt_GiamToiDa.Size = new Size(477, 27);
            txt_GiamToiDa.TabIndex = 26;
            txt_GiamToiDa.Visible = false;
            //
            // chk_TrangThai
            //
            chk_TrangThai.AutoSize = true;
            chk_TrangThai.Checked = true;
            chk_TrangThai.CheckState = CheckState.Checked;
            chk_TrangThai.Font = new Font("Segoe UI", 9.5F);
            chk_TrangThai.Location = new Point(16, 492);
            chk_TrangThai.Name = "chk_TrangThai";
            chk_TrangThai.TabIndex = 27;
            chk_TrangThai.Text = "Đang hoạt động";
            //
            // btn_Luu
            //
            btn_Luu.BackColor = Color.FromArgb(22, 163, 74);
            btn_Luu.Cursor = Cursors.Hand;
            btn_Luu.FlatStyle = FlatStyle.Flat;
            btn_Luu.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btn_Luu.ForeColor = Color.White;
            btn_Luu.Location = new Point(16, 536);
            btn_Luu.Name = "btn_Luu";
            btn_Luu.Size = new Size(210, 40);
            btn_Luu.TabIndex = 28;
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
            btn_Xoa.Location = new Point(236, 536);
            btn_Xoa.Name = "btn_Xoa";
            btn_Xoa.Size = new Size(110, 40);
            btn_Xoa.TabIndex = 29;
            btn_Xoa.Text = "Xóa";
            btn_Xoa.UseVisualStyleBackColor = false;
            btn_Xoa.Visible = false;
            btn_Xoa.FlatAppearance.BorderSize = 0;
            btn_Xoa.Click += BtnXoa_Click;
            //
            // btnReset
            //
            btnReset.BackColor = Color.FromArgb(241, 245, 249);
            btnReset.Cursor = Cursors.Hand;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 10F);
            btnReset.ForeColor = Color.FromArgb(51, 65, 85);
            btnReset.Location = new Point(356, 536);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(140, 40);
            btnReset.TabIndex = 30;
            btnReset.Text = "Làm mới";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.FlatAppearance.BorderSize = 1;
            btnReset.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnReset.Click += btnReset_Click;
            //
            // btnDong
            //
            btnDong.BackColor = Color.FromArgb(241, 245, 249);
            btnDong.Cursor = Cursors.Hand;
            btnDong.DialogResult = DialogResult.Cancel;
            btnDong.FlatStyle = FlatStyle.Flat;
            btnDong.Font = new Font("Segoe UI", 10F);
            btnDong.ForeColor = Color.FromArgb(51, 65, 85);
            btnDong.Location = new Point(506, 536);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(157, 40);
            btnDong.TabIndex = 31;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = false;
            btnDong.FlatAppearance.BorderSize = 1;
            btnDong.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            //
            // QuanLyVoucherForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(900, 614);
            Controls.Add(pnlLeft);
            Controls.Add(pnlDivider);
            Controls.Add(pnlRight);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "QuanLyVoucherForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Quản lý khuyến mãi";
            pnlLeft.ResumeLayout(false);
            pnlLeft.PerformLayout();
            pnlRight.ResumeLayout(false);
            pnlRight.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlLeft;
        private Label lblDanhSach;
        private Panel pnlVoucherList;
        private Button btnThemMoi;
        private Panel pnlDivider;
        private Panel pnlRight;
        private Label lblFormTitle;
        private Panel sep1;
        private Label lblSection1;
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
        private Panel sep2;
        private Label lblSection2;
        private Label lbl_LoaiDK;
        private ComboBox cbo_LoaiDK;
        private TextBox txt_GiaTriDK;
        private Label lbl_UnitDK;
        private Panel sep3;
        private Label lblSection3;
        private Label lbl_LoaiGiam;
        private ComboBox cbo_LoaiGiam;
        private Label lbl_GiaTriGiam;
        private TextBox txt_GiaTriGiam;
        private Label lbl_GiamToiDa;
        private TextBox txt_GiamToiDa;
        private CheckBox chk_TrangThai;
        private Button btn_Luu;
        private Button btn_Xoa;
        private Button btnReset;
        private Button btnDong;
    }
}
