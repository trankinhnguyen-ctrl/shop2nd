namespace dosi
{
    partial class ViewGiaoDich
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
            splitMain = new SplitContainer();
            flpProducts = new FlowLayoutPanel();
            panelSearch = new Panel();
            flpCategory = new FlowLayoutPanel();
            txtSearchProduct = new TextBox();
            panelOrderInfo = new Panel();
            btnThanhToan = new Button();
            lblTongTien = new Label();
            grpCart = new GroupBox();
            dgvCart = new DataGridView();
            grpCustomer = new GroupBox();
            txtAddress = new TextBox();
            lblAddress = new Label();
            txtName = new TextBox();
            lblName = new Label();
            txtPhone = new TextBox();
            lblPhone = new Label();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            panelSearch.SuspendLayout();
            panelOrderInfo.SuspendLayout();
            grpCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            grpCustomer.SuspendLayout();
            SuspendLayout();

            // --- splitMain ---
            splitMain.Dock = DockStyle.Fill;
            splitMain.Location = new Point(0, 0);
            splitMain.Name = "splitMain";
            splitMain.Size = new Size(1100, 700);
            splitMain.SplitterDistance = 750; // Tối ưu lại khoảng cách mặc định cho không gian bán hàng rộng rãi
            splitMain.TabIndex = 0;
            // splitMain.Panel1 (Bên trái: Tìm kiếm & Danh sách sản phẩm)
            splitMain.Panel1.Controls.Add(flpProducts);
            splitMain.Panel1.Controls.Add(panelSearch);
            splitMain.Panel1.Padding = new Padding(12, 12, 6, 12);
            // splitMain.Panel2 (Bên phải: Thông tin đơn hàng & Khách)
            splitMain.Panel2.Controls.Add(panelOrderInfo);
            splitMain.Panel2.Padding = new Padding(6, 12, 12, 12);

            // --- panelSearch ---
            panelSearch.Controls.Add(txtSearchProduct);
            panelSearch.Controls.Add(flpCategory);
            panelSearch.Dock = DockStyle.Top;
            panelSearch.Location = new Point(12, 12);
            panelSearch.Name = "panelSearch";
            panelSearch.Size = new Size(732, 107);
            panelSearch.TabIndex = 0;

            // --- flpCategory ---
            flpCategory.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            flpCategory.AutoScroll = true;
            flpCategory.BackColor = Color.FromArgb(248, 250, 252);
            flpCategory.Location = new Point(0, 55);
            flpCategory.Name = "flpCategory";
            flpCategory.Padding = new Padding(0, 8, 0, 8);
            flpCategory.Size = new Size(732, 52);
            flpCategory.TabIndex = 1;
            flpCategory.WrapContents = false;

            // --- txtSearchProduct ---
            txtSearchProduct.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearchProduct.Font = new Font("Segoe UI", 11F);
            txtSearchProduct.Location = new Point(0, 10);
            txtSearchProduct.Name = "txtSearchProduct";
            txtSearchProduct.PlaceholderText = "Tìm nhanh tên hoặc mã sản phẩm để bán...";
            txtSearchProduct.Size = new Size(732, 32);
            txtSearchProduct.TabIndex = 0;

            // --- flpProducts ---
            flpProducts.AutoScroll = true;
            flpProducts.BackColor = Color.FromArgb(248, 250, 252); // Đổi sang màu nền xám Slate cực nhẹ hiện đại
            flpProducts.Dock = DockStyle.Fill;
            flpProducts.Location = new Point(12, 67);
            flpProducts.Name = "flpProducts";
            flpProducts.Padding = new Padding(8);
            flpProducts.Size = new Size(732, 621);
            flpProducts.TabIndex = 1;

            // --- panelOrderInfo ---
            panelOrderInfo.BackColor = Color.White;
            panelOrderInfo.Controls.Add(btnThanhToan);
            panelOrderInfo.Controls.Add(lblTongTien);
            panelOrderInfo.Controls.Add(grpCart);
            panelOrderInfo.Controls.Add(grpCustomer);
            panelOrderInfo.Dock = DockStyle.Fill;
            panelOrderInfo.Location = new Point(6, 12);
            panelOrderInfo.Name = "panelOrderInfo";
            panelOrderInfo.Size = new Size(326, 676);
            panelOrderInfo.TabIndex = 0;

            // --- grpCustomer (Khối thông tin khách mua) ---
            grpCustomer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpCustomer.Controls.Add(txtAddress);
            grpCustomer.Controls.Add(lblAddress);
            grpCustomer.Controls.Add(txtName);
            grpCustomer.Controls.Add(lblName);
            grpCustomer.Controls.Add(txtPhone);
            grpCustomer.Controls.Add(lblPhone);
            grpCustomer.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            grpCustomer.Location = new Point(4, 0);
            grpCustomer.Name = "grpCustomer";
            grpCustomer.Size = new Size(318, 215);
            grpCustomer.TabIndex = 0;
            grpCustomer.TabStop = false;
            grpCustomer.Text = "Thông tin khách hàng";

            // lblPhone
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 9F);
            lblPhone.Location = new Point(12, 28);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(97, 20);
            lblPhone.Text = "Số điện thoại";

            // txtPhone
            txtPhone.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPhone.Font = new Font("Segoe UI", 9.5F);
            txtPhone.Location = new Point(12, 51);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(294, 29);
            txtPhone.TabIndex = 1;

            // lblName
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 9F);
            lblName.Location = new Point(12, 88);
            lblName.Name = "lblName";
            lblName.Size = new Size(111, 20);
            lblName.Text = "Tên khách hàng";

            // txtName
            txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtName.Font = new Font("Segoe UI", 9.5F);
            txtName.Location = new Point(12, 111);
            txtName.Name = "txtName";
            txtName.Size = new Size(294, 29);
            txtName.TabIndex = 3;

            // lblAddress
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Segoe UI", 9F);
            lblAddress.Location = new Point(12, 148);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(55, 20);
            lblAddress.Text = "Địa chỉ";

            // txtAddress
            txtAddress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtAddress.Font = new Font("Segoe UI", 9.5F);
            txtAddress.Location = new Point(12, 171);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(294, 29);
            txtAddress.TabIndex = 5;

            // --- grpCart (Khối giỏ hàng linh hoạt) ---
            grpCart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpCart.Controls.Add(dgvCart);
            grpCart.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            grpCart.Location = new Point(4, 225);
            grpCart.Name = "grpCart";
            grpCart.Size = new Size(318, 335);
            grpCart.TabIndex = 1;
            grpCart.TabStop = false;
            grpCart.Text = "Giỏ hàng hiện tại";

            // dgvCart
            dgvCart.AllowUserToAddRows = false; // Tắt dòng trống thừa dưới đáy bảng
            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCart.BackgroundColor = Color.White;
            dgvCart.BorderStyle = BorderStyle.None;
            dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCart.Dock = DockStyle.Fill;
            dgvCart.Location = new Point(3, 23);
            dgvCart.Name = "dgvCart";
            dgvCart.RowHeadersVisible = false;
            dgvCart.RowHeadersWidth = 51;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.Size = new Size(312, 309);
            dgvCart.TabIndex = 0;

            // --- lblTongTien ---
            lblTongTien.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblTongTien.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTongTien.ForeColor = Color.FromArgb(220, 38, 38); // Màu đỏ Crimson chuẩn UI thương mại
            lblTongTien.Location = new Point(4, 570);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(318, 35);
            lblTongTien.TabIndex = 2;
            lblTongTien.Text = "Tổng: 0đ";
            lblTongTien.TextAlign = ContentAlignment.MiddleRight;

            // --- btnThanhToan ---
            btnThanhToan.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnThanhToan.BackColor = Color.FromArgb(22, 163, 74); // Chuyển sang màu xanh lá Success tạo cảm giác chốt đơn tin cậy
            btnThanhToan.Cursor = Cursors.Hand;
            btnThanhToan.FlatAppearance.BorderSize = 0;
            btnThanhToan.FlatStyle = FlatStyle.Flat;
            btnThanhToan.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnThanhToan.ForeColor = Color.White;
            btnThanhToan.Location = new Point(4, 616);
            btnThanhToan.Name = "btnThanhToan";
            btnThanhToan.Size = new Size(318, 50);
            btnThanhToan.TabIndex = 3;
            btnThanhToan.Text = "XÁC NHẬN THANH TOÁN";
            btnThanhToan.UseVisualStyleBackColor = false;

            // --- ViewGiaoDich Control Settings ---
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitMain);
            Name = "ViewGiaoDich";
            Size = new Size(1100, 700);

            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            panelSearch.ResumeLayout(false);
            panelSearch.PerformLayout();
            panelOrderInfo.ResumeLayout(false);
            grpCart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            grpCustomer.ResumeLayout(false);
            grpCustomer.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitMain;
        private FlowLayoutPanel flpProducts;
        private Panel panelSearch;
        private TextBox txtSearchProduct;
        private Panel panelOrderInfo;
        private GroupBox grpCustomer;
        private TextBox txtAddress;
        private Label lblAddress;
        private TextBox txtName;
        private Label lblName;
        private TextBox txtPhone;
        private Label lblPhone;
        private GroupBox grpCart;
        private DataGridView dgvCart;
        private Label lblTongTien;
        private Button btnThanhToan;
        private FlowLayoutPanel flpCategory;
    }
}