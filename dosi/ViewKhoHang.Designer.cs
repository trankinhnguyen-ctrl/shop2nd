namespace dosi
{
    partial class ViewKhoHang
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

        private void InitializeComponent()
        {
            panelSearch = new Panel();
            Tim_bt = new Button();
            ThanhTimKiem = new TextBox();
            panelInput = new Panel();
            panelMainContainer = new Panel();
            tableContent = new TableLayoutPanel();
            panelFields = new Panel();
            lblSL = new Label();
            txtSL = new TextBox();
            lblGiaBan = new Label();
            txt_GiaBan = new TextBox();
            lblTenSP = new Label();
            txt_TenSP = new TextBox();
            lblMaSP = new Label();
            txt_MaSP = new TextBox();
            lblSectionTitle = new Label();
            pic_HinhSP = new PictureBox();
            panelActions = new Panel();
            Them_bt = new Button();
            CardLayout = new FlowLayoutPanel();
            panelSearch.SuspendLayout();
            panelInput.SuspendLayout();
            panelMainContainer.SuspendLayout();
            tableContent.SuspendLayout();
            panelFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pic_HinhSP).BeginInit();
            panelActions.SuspendLayout();
            SuspendLayout();
            // 
            // panelSearch
            // 
            panelSearch.BackColor = Color.FromArgb(241, 245, 249);
            panelSearch.Controls.Add(Tim_bt);
            panelSearch.Controls.Add(ThanhTimKiem);
            panelSearch.Dock = DockStyle.Top;
            panelSearch.Location = new Point(0, 0);
            panelSearch.Name = "panelSearch";
            panelSearch.Padding = new Padding(20, 15, 20, 15);
            panelSearch.Size = new Size(1100, 75);
            panelSearch.TabIndex = 0;
            // 
            // Tim_bt
            // 
            Tim_bt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Tim_bt.BackColor = Color.FromArgb(99, 102, 241);
            Tim_bt.FlatAppearance.BorderSize = 0;
            Tim_bt.FlatStyle = FlatStyle.Flat;
            Tim_bt.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            Tim_bt.ForeColor = Color.White;
            Tim_bt.Location = new Point(960, 15);
            Tim_bt.Name = "Tim_bt";
            Tim_bt.Size = new Size(120, 44);
            Tim_bt.TabIndex = 1;
            Tim_bt.Text = "Tìm kiếm";
            Tim_bt.UseVisualStyleBackColor = false;
            // 
            // ThanhTimKiem
            // 
            ThanhTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ThanhTimKiem.BackColor = Color.White;
            ThanhTimKiem.BorderStyle = BorderStyle.FixedSingle;
            ThanhTimKiem.Font = new Font("Segoe UI", 13F);
            ThanhTimKiem.ForeColor = Color.FromArgb(15, 23, 42);
            ThanhTimKiem.Location = new Point(20, 19);
            ThanhTimKiem.Name = "ThanhTimKiem";
            ThanhTimKiem.PlaceholderText = "  Tìm kiếm sản phẩm...";
            ThanhTimKiem.Size = new Size(925, 36);
            ThanhTimKiem.TabIndex = 0;
            // 
            // panelInput
            // 
            panelInput.BackColor = Color.FromArgb(241, 245, 249);
            panelInput.Controls.Add(panelMainContainer);
            panelInput.Dock = DockStyle.Bottom;
            panelInput.Location = new Point(0, 310);
            panelInput.Name = "panelInput";
            panelInput.Padding = new Padding(20);
            panelInput.Size = new Size(1100, 390);
            panelInput.TabIndex = 1;
            // 
            // panelMainContainer
            // 
            panelMainContainer.BackColor = Color.White;
            panelMainContainer.Controls.Add(tableContent);
            panelMainContainer.Controls.Add(panelActions);
            panelMainContainer.Dock = DockStyle.Fill;
            panelMainContainer.Location = new Point(20, 20);
            panelMainContainer.Name = "panelMainContainer";
            panelMainContainer.Padding = new Padding(20);
            panelMainContainer.Size = new Size(1060, 350);
            panelMainContainer.TabIndex = 0;
            // 
            // tableContent
            // 
            tableContent.ColumnCount = 2;
            tableContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tableContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableContent.Controls.Add(panelFields, 0, 0);
            tableContent.Controls.Add(pic_HinhSP, 1, 0);
            tableContent.Dock = DockStyle.Fill;
            tableContent.Location = new Point(20, 20);
            tableContent.Name = "tableContent";
            tableContent.RowCount = 1;
            tableContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableContent.Size = new Size(1020, 250);
            tableContent.TabIndex = 0;
            // 
            // panelFields
            // 
            panelFields.Controls.Add(lblSL);
            panelFields.Controls.Add(txtSL);
            panelFields.Controls.Add(lblGiaBan);
            panelFields.Controls.Add(txt_GiaBan);
            panelFields.Controls.Add(lblTenSP);
            panelFields.Controls.Add(txt_TenSP);
            panelFields.Controls.Add(lblMaSP);
            panelFields.Controls.Add(txt_MaSP);
            panelFields.Controls.Add(lblSectionTitle);
            panelFields.Dock = DockStyle.Fill;
            panelFields.Location = new Point(0, 0);
            panelFields.Margin = new Padding(0);
            panelFields.Name = "panelFields";
            panelFields.Size = new Size(561, 250);
            panelFields.TabIndex = 0;
            // 
            // lblSectionTitle
            // 
            lblSectionTitle.AutoSize = true;
            lblSectionTitle.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            lblSectionTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblSectionTitle.Location = new Point(0, 0);
            lblSectionTitle.Name = "lblSectionTitle";
            lblSectionTitle.Size = new Size(174, 30);
            lblSectionTitle.Text = "Thông tin chi tiết";
            // 
            // lblMaSP
            // 
            lblMaSP.AutoSize = true;
            lblMaSP.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblMaSP.ForeColor = Color.FromArgb(71, 85, 105);
            lblMaSP.Location = new Point(0, 42);
            lblMaSP.Name = "lblMaSP";
            lblMaSP.Size = new Size(55, 21);
            lblMaSP.Text = "Mã SP";
            // 
            // txt_MaSP
            // 
            txt_MaSP.BackColor = Color.FromArgb(248, 250, 252);
            txt_MaSP.BorderStyle = BorderStyle.FixedSingle;
            txt_MaSP.Font = new Font("Segoe UI", 11F);
            txt_MaSP.ForeColor = Color.FromArgb(15, 23, 42);
            txt_MaSP.Location = new Point(0, 65);
            txt_MaSP.Name = "txt_MaSP";
            txt_MaSP.Size = new Size(530, 32);
            txt_MaSP.TabIndex = 9;
            // 
            // lblTenSP
            // 
            lblTenSP.AutoSize = true;
            lblTenSP.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblTenSP.ForeColor = Color.FromArgb(71, 85, 105);
            lblTenSP.Location = new Point(0, 110);
            lblTenSP.Name = "lblTenSP";
            lblTenSP.Size = new Size(57, 21);
            lblTenSP.Text = "Tên SP";
            // 
            // txt_TenSP
            // 
            txt_TenSP.BackColor = Color.FromArgb(248, 250, 252);
            txt_TenSP.BorderStyle = BorderStyle.FixedSingle;
            txt_TenSP.Font = new Font("Segoe UI", 11F);
            txt_TenSP.ForeColor = Color.FromArgb(15, 23, 42);
            txt_TenSP.Location = new Point(0, 133);
            txt_TenSP.Name = "txt_TenSP";
            txt_TenSP.Size = new Size(530, 32);
            txt_TenSP.TabIndex = 7;
            // 
            // lblGiaBan
            // 
            lblGiaBan.AutoSize = true;
            lblGiaBan.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblGiaBan.ForeColor = Color.FromArgb(71, 85, 105);
            lblGiaBan.Location = new Point(0, 178);
            lblGiaBan.Name = "lblGiaBan";
            lblGiaBan.Size = new Size(63, 21);
            lblGiaBan.Text = "Giá Bán";
            // 
            // txt_GiaBan
            // 
            txt_GiaBan.BackColor = Color.FromArgb(248, 250, 252);
            txt_GiaBan.BorderStyle = BorderStyle.FixedSingle;
            txt_GiaBan.Font = new Font("Segoe UI", 11F);
            txt_GiaBan.ForeColor = Color.FromArgb(15, 23, 42);
            txt_GiaBan.Location = new Point(0, 201);
            txt_GiaBan.Name = "txt_GiaBan";
            txt_GiaBan.Size = new Size(250, 32);
            txt_GiaBan.TabIndex = 5;
            // 
            // lblSL
            // 
            lblSL.AutoSize = true;
            lblSL.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblSL.ForeColor = Color.FromArgb(71, 85, 105);
            lblSL.Location = new Point(280, 178);
            lblSL.Name = "lblSL";
            lblSL.Size = new Size(78, 21);
            lblSL.Text = "Số Lượng";
            // 
            // txtSL
            // 
            txtSL.BackColor = Color.FromArgb(248, 250, 252);
            txtSL.BorderStyle = BorderStyle.FixedSingle;
            txtSL.Font = new Font("Segoe UI", 11F);
            txtSL.ForeColor = Color.FromArgb(15, 23, 42);
            txtSL.Location = new Point(280, 201);
            txtSL.Name = "txtSL";
            txtSL.Size = new Size(250, 32);
            txtSL.TabIndex = 1;
            // 
            // pic_HinhSP
            // 
            pic_HinhSP.BackColor = Color.FromArgb(248, 250, 252);
            pic_HinhSP.BorderStyle = BorderStyle.None;
            pic_HinhSP.Dock = DockStyle.Fill;
            pic_HinhSP.Location = new Point(581, 40);
            pic_HinhSP.Margin = new Padding(20, 40, 0, 10);
            pic_HinhSP.Name = "pic_HinhSP";
            pic_HinhSP.Size = new Size(439, 200);
            pic_HinhSP.SizeMode = PictureBoxSizeMode.CenterImage;
            pic_HinhSP.TabIndex = 1;
            pic_HinhSP.TabStop = false;
            // 
            // panelActions
            // 
            panelActions.Controls.Add(Them_bt);
            panelActions.Dock = DockStyle.Bottom;
            panelActions.Location = new Point(20, 270);
            panelActions.Name = "panelActions";
            panelActions.Size = new Size(1020, 60);
            panelActions.TabIndex = 1;
            // 
            // Them_bt
            // 
            Them_bt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Them_bt.BackColor = Color.FromArgb(34, 197, 94);
            Them_bt.FlatAppearance.BorderSize = 0;
            Them_bt.FlatStyle = FlatStyle.Flat;
            Them_bt.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            Them_bt.ForeColor = Color.White;
            Them_bt.Location = new Point(880, 10);
            Them_bt.Name = "Them_bt";
            Them_bt.Size = new Size(140, 44);
            Them_bt.TabIndex = 0;
            Them_bt.Text = "Thêm SP";
            Them_bt.UseVisualStyleBackColor = false;
            // 
            // CardLayout
            // 
            CardLayout.AutoScroll = true;
            CardLayout.BackColor = Color.FromArgb(241, 245, 249);
            CardLayout.Dock = DockStyle.Fill;
            CardLayout.Location = new Point(0, 75);
            CardLayout.Name = "CardLayout";
            CardLayout.Padding = new Padding(20, 10, 20, 10);
            CardLayout.Size = new Size(1100, 235);
            CardLayout.TabIndex = 2;
            // 
            // ViewKhoHang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            Controls.Add(CardLayout);
            Controls.Add(panelInput);
            Controls.Add(panelSearch);
            Name = "ViewKhoHang";
            Size = new Size(1100, 700);
            panelSearch.ResumeLayout(false);
            panelSearch.PerformLayout();
            panelInput.ResumeLayout(false);
            panelMainContainer.ResumeLayout(false);
            tableContent.ResumeLayout(false);
            panelFields.ResumeLayout(false);
            panelFields.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pic_HinhSP).EndInit();
            panelActions.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Panel panelSearch;
        private TextBox ThanhTimKiem;
        private Button Tim_bt;
        private Panel panelInput;
        private Panel panelMainContainer;
        private TableLayoutPanel tableContent;
        private Panel panelFields;
        private Label lblSectionTitle;
        private Label lblMaSP;
        private TextBox txt_MaSP;
        private Label lblTenSP;
        private TextBox txt_TenSP;
        private Label lblGiaBan;
        private TextBox txt_GiaBan;
        private Label lblSL;
        private TextBox txtSL;
        private PictureBox pic_HinhSP;
        private Panel panelActions;
        private Button Them_bt;
        private FlowLayoutPanel CardLayout;
    }
}