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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            panelSearch = new Panel();
            Tim_bt = new Button();
            ThanhTimKiem = new TextBox();
            panelInput = new Panel();
            tableContent = new TableLayoutPanel();
            panelFields = new Panel();
            lblSL = new Label();
            txtSL = new TextBox();
            lblSize = new Label();
            txt_Size = new TextBox();
            lblTenSP = new Label();
            txt_TenSP = new TextBox();
            lblMaSP = new Label();
            txt_MaSP = new TextBox();
            pic_HinhSP = new PictureBox();
            panelActions = new Panel();
            Them_bt = new Button();
            CardLayout = new FlowLayoutPanel();
            panelSearch.SuspendLayout();
            panelInput.SuspendLayout();
            tableContent.SuspendLayout();
            panelFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pic_HinhSP).BeginInit();
            panelActions.SuspendLayout();
            SuspendLayout();
            // 
            // panelSearch
            // 
            panelSearch.Controls.Add(Tim_bt);
            panelSearch.Controls.Add(ThanhTimKiem);
            panelSearch.Dock = DockStyle.Top;
            panelSearch.Location = new Point(0, 0);
            panelSearch.Name = "panelSearch";
            panelSearch.Padding = new Padding(15);
            panelSearch.Size = new Size(1100, 70);
            panelSearch.TabIndex = 0;
            // 
            // Tim_bt
            // 
            Tim_bt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Tim_bt.BackColor = Color.FromArgb(79, 70, 229);
            Tim_bt.FlatStyle = FlatStyle.Flat;
            Tim_bt.ForeColor = Color.White;
            Tim_bt.Location = new Point(975, 15);
            Tim_bt.Name = "Tim_bt";
            Tim_bt.Size = new Size(110, 40);
            Tim_bt.TabIndex = 1;
            Tim_bt.Text = "Tìm Kiếm";
            Tim_bt.UseVisualStyleBackColor = false;
            // 
            // ThanhTimKiem
            // 
            ThanhTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ThanhTimKiem.Font = new Font("Segoe UI", 12F);
            ThanhTimKiem.Location = new Point(15, 18);
            ThanhTimKiem.Name = "ThanhTimKiem";
            ThanhTimKiem.PlaceholderText = "Tìm sản phẩm...";
            ThanhTimKiem.Size = new Size(950, 34);
            ThanhTimKiem.TabIndex = 0;
            // 
            // panelInput
            // 
            panelInput.BackColor = Color.White;
            panelInput.Controls.Add(tableContent);
            panelInput.Controls.Add(panelActions);
            panelInput.Dock = DockStyle.Bottom;
            panelInput.Location = new Point(0, 350);
            panelInput.Name = "panelInput";
            panelInput.Padding = new Padding(15);
            panelInput.Size = new Size(1100, 350);
            panelInput.TabIndex = 1;
            // 
            // tableContent
            // 
            tableContent.ColumnCount = 2;
            tableContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableContent.Controls.Add(panelFields, 0, 0);
            tableContent.Controls.Add(pic_HinhSP, 1, 0);
            tableContent.Dock = DockStyle.Fill;
            tableContent.Location = new Point(15, 15);
            tableContent.Name = "tableContent";
            tableContent.RowCount = 1;
            tableContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableContent.Size = new Size(1070, 260);
            tableContent.TabIndex = 0;
            // 
            // panelFields
            // 
            panelFields.Controls.Add(lblSL);
            panelFields.Controls.Add(txtSL);
            panelFields.Controls.Add(lblSize);
            panelFields.Controls.Add(txt_Size);
            panelFields.Controls.Add(lblTenSP);
            panelFields.Controls.Add(txt_TenSP);
            panelFields.Controls.Add(lblMaSP);
            panelFields.Controls.Add(txt_MaSP);
            panelFields.Dock = DockStyle.Fill;
            panelFields.Location = new Point(3, 3);
            panelFields.Name = "panelFields";
            panelFields.Size = new Size(529, 254);
            panelFields.TabIndex = 0;
            // 
            // lblSL
            // 
            lblSL.AutoSize = true;
            lblSL.Location = new Point(5, 170);
            lblSL.Name = "lblSL";
            lblSL.Size = new Size(72, 20);
            lblSL.TabIndex = 0;
            lblSL.Text = "Số Lượng";
            // 
            // txtSL
            // 
            txtSL.Location = new Point(5, 190);
            txtSL.Name = "txtSL";
            txtSL.Size = new Size(510, 27);
            txtSL.TabIndex = 1;
            // 
            // lblSize
            // 
            lblSize.AutoSize = true;
            lblSize.Location = new Point(5, 115);
            lblSize.Name = "lblSize";
            lblSize.Size = new Size(36, 20);
            lblSize.TabIndex = 4;
            lblSize.Text = "Size";
            // 
            // txt_Size
            // 
            txt_Size.Location = new Point(5, 135);
            txt_Size.Name = "txt_Size";
            txt_Size.Size = new Size(510, 27);
            txt_Size.TabIndex = 5;
            // 
            // lblTenSP
            // 
            lblTenSP.AutoSize = true;
            lblTenSP.Location = new Point(5, 60);
            lblTenSP.Name = "lblTenSP";
            lblTenSP.Size = new Size(52, 20);
            lblTenSP.TabIndex = 6;
            lblTenSP.Text = "Tên SP";
            // 
            // txt_TenSP
            // 
            txt_TenSP.Location = new Point(5, 80);
            txt_TenSP.Name = "txt_TenSP";
            txt_TenSP.Size = new Size(510, 27);
            txt_TenSP.TabIndex = 7;
            // 
            // lblMaSP
            // 
            lblMaSP.AutoSize = true;
            lblMaSP.Location = new Point(5, 5);
            lblMaSP.Name = "lblMaSP";
            lblMaSP.Size = new Size(50, 20);
            lblMaSP.TabIndex = 8;
            lblMaSP.Text = "Mã SP";
            // 
            // txt_MaSP
            // 
            txt_MaSP.Location = new Point(5, 25);
            txt_MaSP.Name = "txt_MaSP";
            txt_MaSP.Size = new Size(510, 27);
            txt_MaSP.TabIndex = 9;
            // 
            // pic_HinhSP
            // 
            pic_HinhSP.BackColor = Color.FromArgb(248, 250, 252);
            pic_HinhSP.BorderStyle = BorderStyle.FixedSingle;
            pic_HinhSP.Dock = DockStyle.Fill;
            pic_HinhSP.Location = new Point(545, 10);
            pic_HinhSP.Margin = new Padding(10);
            pic_HinhSP.Name = "pic_HinhSP";
            pic_HinhSP.Size = new Size(515, 240);
            pic_HinhSP.SizeMode = PictureBoxSizeMode.Zoom;
            pic_HinhSP.TabIndex = 1;
            pic_HinhSP.TabStop = false;
            pic_HinhSP.Click += this.pic_HinhSP_Click;
            // 
            // panelActions
            // 
            panelActions.Controls.Add(Them_bt);
            panelActions.Dock = DockStyle.Bottom;
            panelActions.Location = new Point(15, 275);
            panelActions.Name = "panelActions";
            panelActions.Size = new Size(1070, 60);
            panelActions.TabIndex = 1;
            // 
            // Them_bt
            // 
            Them_bt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Them_bt.BackColor = Color.FromArgb(34, 197, 94);
            Them_bt.FlatStyle = FlatStyle.Flat;
            Them_bt.ForeColor = Color.White;
            Them_bt.Location = new Point(920, 10);
            Them_bt.Name = "Them_bt";
            Them_bt.Size = new Size(140, 40);
            Them_bt.TabIndex = 0;
            Them_bt.Text = "Thêm SP";
            Them_bt.UseVisualStyleBackColor = false;
            Them_bt.Click += Them_bt_Click;
            // 
            // CardLayout
            // 
            CardLayout.AutoScroll = true;
            CardLayout.Dock = DockStyle.Fill;
            CardLayout.Location = new Point(0, 70);
            CardLayout.Name = "CardLayout";
            CardLayout.Padding = new Padding(15);
            CardLayout.Size = new Size(1100, 280);
            CardLayout.TabIndex = 2;
            // 
            // ViewKhoHang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(CardLayout);
            Controls.Add(panelInput);
            Controls.Add(panelSearch);
            Name = "ViewKhoHang";
            Size = new Size(1100, 700);
            panelSearch.ResumeLayout(false);
            panelSearch.PerformLayout();
            panelInput.ResumeLayout(false);
            tableContent.ResumeLayout(false);
            panelFields.ResumeLayout(false);
            panelFields.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pic_HinhSP).EndInit();
            panelActions.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelSearch;
        private TextBox ThanhTimKiem;
        private Button Tim_bt;
        private Panel panelInput;
        private TableLayoutPanel tableContent;
        private Panel panelFields;
        private Label lblMaSP;
        private TextBox txt_MaSP;
        private Label lblTenSP;
        private TextBox txt_TenSP;
        private Label lblSize;
        private TextBox txt_Size;
        private Label lblSL;
        private TextBox txtSL;
        private PictureBox pic_HinhSP;
        private Panel panelActions;
        private Button Them_bt;
        private FlowLayoutPanel CardLayout;
    }
}