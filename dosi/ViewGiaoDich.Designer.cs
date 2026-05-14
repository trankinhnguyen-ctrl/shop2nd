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

            splitMain.Dock = DockStyle.Fill;
            splitMain.Location = new Point(0, 0);
            splitMain.Name = "splitMain";

            splitMain.Panel1.Controls.Add(flpProducts);
            splitMain.Panel1.Controls.Add(panelSearch);
            splitMain.Panel1.Padding = new Padding(10);

            splitMain.Panel2.Controls.Add(panelOrderInfo);
            splitMain.Panel2.Padding = new Padding(10);
            splitMain.Size = new Size(1100, 700);
            splitMain.SplitterDistance = 770;
            splitMain.TabIndex = 0;

            flpProducts.AutoScroll = true;
            flpProducts.BackColor = Color.WhiteSmoke;
            flpProducts.Dock = DockStyle.Fill;
            flpProducts.Location = new Point(10, 70);
            flpProducts.Name = "flpProducts";
            flpProducts.Padding = new Padding(5);
            flpProducts.Size = new Size(750, 620);
            flpProducts.TabIndex = 1;

            panelSearch.Controls.Add(txtSearchProduct);
            panelSearch.Dock = DockStyle.Top;
            panelSearch.Location = new Point(10, 10);
            panelSearch.Name = "panelSearch";
            panelSearch.Size = new Size(750, 60);
            panelSearch.TabIndex = 0;

            txtSearchProduct.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearchProduct.Font = new Font("Segoe UI", 12F);
            txtSearchProduct.Location = new Point(5, 13);
            txtSearchProduct.Name = "txtSearchProduct";
            txtSearchProduct.PlaceholderText = "Tìm tên hoặc mã sản phẩm để bán...";
            txtSearchProduct.Size = new Size(740, 34);
            txtSearchProduct.TabIndex = 0;

            panelOrderInfo.BackColor = Color.White;
            panelOrderInfo.Controls.Add(btnThanhToan);
            panelOrderInfo.Controls.Add(lblTongTien);
            panelOrderInfo.Controls.Add(grpCart);
            panelOrderInfo.Controls.Add(grpCustomer);
            panelOrderInfo.Dock = DockStyle.Fill;
            panelOrderInfo.Location = new Point(10, 10);
            panelOrderInfo.Name = "panelOrderInfo";
            panelOrderInfo.Size = new Size(306, 680);
            panelOrderInfo.TabIndex = 0;

            btnThanhToan.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnThanhToan.BackColor = Color.FromArgb(37, 99, 235);
            btnThanhToan.FlatStyle = FlatStyle.Flat;
            btnThanhToan.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnThanhToan.ForeColor = Color.White;
            btnThanhToan.Location = new Point(5, 620);
            btnThanhToan.Name = "btnThanhToan";
            btnThanhToan.Size = new Size(296, 50);
            btnThanhToan.TabIndex = 3;
            btnThanhToan.Text = "THANH TOÁN";
            btnThanhToan.UseVisualStyleBackColor = false;

            lblTongTien.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblTongTien.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTongTien.ForeColor = Color.Red;
            lblTongTien.Location = new Point(5, 580);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(296, 35);
            lblTongTien.TabIndex = 2;
            lblTongTien.Text = "Tổng: 0đ";
            lblTongTien.TextAlign = ContentAlignment.MiddleRight;

            grpCart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpCart.Controls.Add(dgvCart);
            grpCart.Location = new Point(5, 230);
            grpCart.Name = "grpCart";
            grpCart.Size = new Size(296, 340);
            grpCart.TabIndex = 1;
            grpCart.TabStop = false;
            grpCart.Text = "Giỏ hàng";

            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCart.BackgroundColor = Color.White;
            dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCart.Dock = DockStyle.Fill;
            dgvCart.Location = new Point(3, 23);
            dgvCart.Name = "dgvCart";
            dgvCart.RowHeadersVisible = false;
            dgvCart.RowHeadersWidth = 51;
            dgvCart.Size = new Size(290, 314);
            dgvCart.TabIndex = 0;

            grpCustomer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpCustomer.Controls.Add(txtAddress);
            grpCustomer.Controls.Add(lblAddress);
            grpCustomer.Controls.Add(txtName);
            grpCustomer.Controls.Add(lblName);
            grpCustomer.Controls.Add(txtPhone);
            grpCustomer.Controls.Add(lblPhone);
            grpCustomer.Location = new Point(5, 5);
            grpCustomer.Name = "grpCustomer";
            grpCustomer.Size = new Size(296, 220);
            grpCustomer.TabIndex = 0;
            grpCustomer.TabStop = false;
            grpCustomer.Text = "Thông tin khách hàng";

            txtAddress.Location = new Point(10, 175);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(275, 27);
            txtAddress.TabIndex = 5;

            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(10, 150);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(55, 20);
            lblAddress.Text = "Địa chỉ";

            txtName.Location = new Point(10, 115);
            txtName.Name = "txtName";
            txtName.Size = new Size(275, 27);
            txtName.TabIndex = 3;

            lblName.AutoSize = true;
            lblName.Location = new Point(10, 90);
            lblName.Name = "lblName";
            lblName.Size = new Size(111, 20);
            lblName.Text = "Tên khách hàng";

            txtPhone.Location = new Point(10, 55);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(275, 27);
            txtPhone.TabIndex = 1;

            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(10, 30);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(97, 20);
            lblPhone.Text = "Số điện thoại";

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
    }
}