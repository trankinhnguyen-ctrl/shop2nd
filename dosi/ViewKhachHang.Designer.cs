namespace dosi
{
    partial class ViewKhachHang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewKhachHang));
            mainSplit = new SplitContainer();
            panelLeft = new Panel();
            listKhachHang = new FlowLayoutPanel();
            panelSearch = new Panel();
            txtSearch = new TextBox();
            panelRight = new Panel();
            dgvHistory = new DataGridView();
            panelStats = new TableLayoutPanel();
            panelStat3 = new Panel();
            lblStatValue3 = new Label();
            lblStatTitle3 = new Label();
            panelStat2 = new Panel();
            lblStatValue2 = new Label();
            lblStatTitle2 = new Label();
            panelStat1 = new Panel();
            lblStatValue1 = new Label();
            lblStatTitle1 = new Label();
            panelHeader = new Panel();
            lblAddress = new Label();
            lblPhone = new Label();
            lblHoTen = new Label();
            lblGhiChu = new Label();
            picAvatar = new PictureBox();
            picChinhSua = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)mainSplit).BeginInit();
            mainSplit.Panel1.SuspendLayout();
            mainSplit.Panel2.SuspendLayout();
            mainSplit.SuspendLayout();
            panelLeft.SuspendLayout();
            panelSearch.SuspendLayout();
            panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            panelStats.SuspendLayout();
            panelStat3.SuspendLayout();
            panelStat2.SuspendLayout();
            panelStat1.SuspendLayout();
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picChinhSua).BeginInit();
            SuspendLayout();
            // 
            // mainSplit
            // 
            mainSplit.BackColor = Color.FromArgb(226, 232, 240);
            mainSplit.Dock = DockStyle.Fill;
            mainSplit.Location = new Point(0, 0);
            mainSplit.Name = "mainSplit";
            // 
            // mainSplit.Panel1
            // 
            mainSplit.Panel1.Controls.Add(panelLeft);
            // 
            // mainSplit.Panel2
            // 
            mainSplit.Panel2.Controls.Add(panelRight);
            mainSplit.Size = new Size(1100, 700);
            mainSplit.SplitterDistance = 360;
            mainSplit.SplitterWidth = 2;
            mainSplit.TabIndex = 0;
            mainSplit.SplitterMoved += mainSplit_SplitterMoved;
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(241, 245, 249);
            panelLeft.Controls.Add(listKhachHang);
            panelLeft.Controls.Add(panelSearch);
            panelLeft.Dock = DockStyle.Fill;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Padding = new Padding(15, 15, 5, 15);
            panelLeft.Size = new Size(360, 700);
            panelLeft.TabIndex = 0;
            // 
            // listKhachHang
            // 
            listKhachHang.AutoScroll = true;
            listKhachHang.BackColor = Color.FromArgb(241, 245, 249);
            listKhachHang.Dock = DockStyle.Fill;
            listKhachHang.Location = new Point(15, 75);
            listKhachHang.Name = "listKhachHang";
            listKhachHang.Size = new Size(340, 610);
            listKhachHang.TabIndex = 0;
            // 
            // panelSearch
            // 
            panelSearch.BackColor = Color.FromArgb(241, 245, 249);
            panelSearch.Controls.Add(txtSearch);
            panelSearch.Dock = DockStyle.Top;
            panelSearch.Location = new Point(15, 15);
            panelSearch.Name = "panelSearch";
            panelSearch.Size = new Size(340, 60);
            panelSearch.TabIndex = 1;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.BackColor = Color.White;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 12F);
            txtSearch.ForeColor = Color.FromArgb(15, 23, 42);
            txtSearch.Location = new Point(0, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "  Tìm kiếm khách hàng...";
            txtSearch.Size = new Size(335, 34);
            txtSearch.TabIndex = 0;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.FromArgb(241, 245, 249);
            panelRight.Controls.Add(dgvHistory);
            panelRight.Controls.Add(panelStats);
            panelRight.Controls.Add(panelHeader);
            panelRight.Dock = DockStyle.Fill;
            panelRight.Location = new Point(0, 0);
            panelRight.Name = "panelRight";
            panelRight.Padding = new Padding(10, 15, 15, 15);
            panelRight.Size = new Size(738, 700);
            panelRight.TabIndex = 0;
            // 
            // dgvHistory
            // 
            dgvHistory.BackgroundColor = Color.White;
            dgvHistory.BorderStyle = BorderStyle.None;
            dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistory.Dock = DockStyle.Fill;
            dgvHistory.Location = new Point(10, 305);
            dgvHistory.Name = "dgvHistory";
            dgvHistory.RowHeadersWidth = 51;
            dgvHistory.Size = new Size(713, 380);
            dgvHistory.TabIndex = 0;
            // 
            // panelStats
            // 
            panelStats.ColumnCount = 3;
            panelStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            panelStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            panelStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            panelStats.Controls.Add(panelStat3, 2, 0);
            panelStats.Controls.Add(panelStat2, 1, 0);
            panelStats.Controls.Add(panelStat1, 0, 0);
            panelStats.Dock = DockStyle.Top;
            panelStats.Location = new Point(10, 175);
            panelStats.Name = "panelStats";
            panelStats.RowCount = 1;
            panelStats.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelStats.Size = new Size(713, 130);
            panelStats.TabIndex = 1;
            // 
            // panelStat3
            // 
            panelStat3.BackColor = Color.White;
            panelStat3.Controls.Add(lblStatValue3);
            panelStat3.Controls.Add(lblStatTitle3);
            panelStat3.Dock = DockStyle.Fill;
            panelStat3.Location = new Point(477, 3);
            panelStat3.Margin = new Padding(3, 3, 0, 12);
            panelStat3.Name = "panelStat3";
            panelStat3.Padding = new Padding(10);
            panelStat3.Size = new Size(236, 115);
            panelStat3.TabIndex = 0;
            // 
            // lblStatValue3
            // 
            lblStatValue3.Dock = DockStyle.Fill;
            lblStatValue3.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold);
            lblStatValue3.ForeColor = Color.FromArgb(79, 70, 229);
            lblStatValue3.Location = new Point(10, 35);
            lblStatValue3.Name = "lblStatValue3";
            lblStatValue3.Size = new Size(216, 70);
            lblStatValue3.TabIndex = 0;
            lblStatValue3.Text = "-";
            lblStatValue3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStatTitle3
            // 
            lblStatTitle3.Dock = DockStyle.Top;
            lblStatTitle3.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblStatTitle3.ForeColor = Color.FromArgb(100, 116, 139);
            lblStatTitle3.Location = new Point(10, 10);
            lblStatTitle3.Name = "lblStatTitle3";
            lblStatTitle3.Size = new Size(216, 25);
            lblStatTitle3.TabIndex = 1;
            lblStatTitle3.Text = "NĂM THÀNH VIÊN";
            lblStatTitle3.TextAlign = ContentAlignment.TopCenter;
            // 
            // panelStat2
            // 
            panelStat2.BackColor = Color.White;
            panelStat2.Controls.Add(lblStatValue2);
            panelStat2.Controls.Add(lblStatTitle2);
            panelStat2.Dock = DockStyle.Fill;
            panelStat2.Location = new Point(240, 3);
            panelStat2.Margin = new Padding(3, 3, 3, 12);
            panelStat2.Name = "panelStat2";
            panelStat2.Padding = new Padding(10);
            panelStat2.Size = new Size(231, 115);
            panelStat2.TabIndex = 1;
            // 
            // lblStatValue2
            // 
            lblStatValue2.Dock = DockStyle.Fill;
            lblStatValue2.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold);
            lblStatValue2.ForeColor = Color.FromArgb(225, 29, 72);
            lblStatValue2.Location = new Point(10, 35);
            lblStatValue2.Name = "lblStatValue2";
            lblStatValue2.Size = new Size(211, 70);
            lblStatValue2.TabIndex = 0;
            lblStatValue2.Text = "0đ";
            lblStatValue2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStatTitle2
            // 
            lblStatTitle2.Dock = DockStyle.Top;
            lblStatTitle2.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblStatTitle2.ForeColor = Color.FromArgb(100, 116, 139);
            lblStatTitle2.Location = new Point(10, 10);
            lblStatTitle2.Name = "lblStatTitle2";
            lblStatTitle2.Size = new Size(211, 25);
            lblStatTitle2.TabIndex = 1;
            lblStatTitle2.Text = "TỔNG CHI TIÊU";
            lblStatTitle2.TextAlign = ContentAlignment.TopCenter;
            // 
            // panelStat1
            // 
            panelStat1.BackColor = Color.White;
            panelStat1.Controls.Add(lblStatValue1);
            panelStat1.Controls.Add(lblStatTitle1);
            panelStat1.Dock = DockStyle.Fill;
            panelStat1.Location = new Point(0, 3);
            panelStat1.Margin = new Padding(0, 3, 3, 12);
            panelStat1.Name = "panelStat1";
            panelStat1.Padding = new Padding(10);
            panelStat1.Size = new Size(234, 115);
            panelStat1.TabIndex = 2;
            // 
            // lblStatValue1
            // 
            lblStatValue1.Dock = DockStyle.Fill;
            lblStatValue1.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold);
            lblStatValue1.ForeColor = Color.FromArgb(14, 165, 233);
            lblStatValue1.Location = new Point(10, 35);
            lblStatValue1.Name = "lblStatValue1";
            lblStatValue1.Size = new Size(214, 70);
            lblStatValue1.TabIndex = 0;
            lblStatValue1.Text = "0";
            lblStatValue1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStatTitle1
            // 
            lblStatTitle1.Dock = DockStyle.Top;
            lblStatTitle1.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblStatTitle1.ForeColor = Color.FromArgb(100, 116, 139);
            lblStatTitle1.Location = new Point(10, 10);
            lblStatTitle1.Name = "lblStatTitle1";
            lblStatTitle1.Size = new Size(214, 25);
            lblStatTitle1.TabIndex = 1;
            lblStatTitle1.Text = "ĐƠN HÀNG";
            lblStatTitle1.TextAlign = ContentAlignment.TopCenter;
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.White;
            panelHeader.Controls.Add(lblAddress);
            panelHeader.Controls.Add(lblPhone);
            panelHeader.Controls.Add(lblHoTen);
            panelHeader.Controls.Add(lblGhiChu);
            panelHeader.Controls.Add(picAvatar);
            panelHeader.Controls.Add(picChinhSua);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(10, 15);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(713, 160);
            panelHeader.TabIndex = 2;
            panelHeader.Click += panelHeader_Click;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Segoe UI", 9.5F);
            lblAddress.ForeColor = Color.FromArgb(71, 85, 105);
            lblAddress.Location = new Point(145, 84);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(57, 21);
            lblAddress.TabIndex = 0;
            lblAddress.Text = "Địa chỉ";
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblPhone.ForeColor = Color.FromArgb(100, 116, 139);
            lblPhone.Location = new Point(145, 54);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(99, 23);
            lblPhone.TabIndex = 1;
            lblPhone.Text = "0905123456";
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold);
            lblHoTen.ForeColor = Color.FromArgb(15, 23, 42);
            lblHoTen.Location = new Point(143, 14);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(216, 29);
            lblHoTen.TabIndex = 2;
            lblHoTen.Text = "Tên Khách Hàng";
            // 
            // lblGhiChu
            // 
            lblGhiChu.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblGhiChu.ForeColor = Color.FromArgb(100, 116, 139);
            lblGhiChu.Location = new Point(143, 112);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new Size(520, 38);
            lblGhiChu.TabIndex = 4;
            // 
            // picAvatar
            // 
            picAvatar.BackColor = Color.FromArgb(241, 245, 249);
            picAvatar.Image = (Image)resources.GetObject("picAvatar.Image");
            picAvatar.Location = new Point(20, 30);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(100, 100);
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picAvatar.TabIndex = 3;
            picAvatar.TabStop = false;
            // 
            // picChinhSua
            // 
            picChinhSua.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picChinhSua.BackColor = Color.Transparent;
            picChinhSua.Cursor = Cursors.Hand;
            picChinhSua.Image = (Image)resources.GetObject("picChinhSua.Image");
            picChinhSua.Location = new Point(675, 12);
            picChinhSua.Name = "picChinhSua";
            picChinhSua.Size = new Size(32, 32);
            picChinhSua.SizeMode = PictureBoxSizeMode.Zoom;
            picChinhSua.TabIndex = 5;
            picChinhSua.TabStop = false;
            // 
            // ViewKhachHang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            Controls.Add(mainSplit);
            Name = "ViewKhachHang";
            Size = new Size(1100, 700);
            mainSplit.Panel1.ResumeLayout(false);
            mainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplit).EndInit();
            mainSplit.ResumeLayout(false);
            panelLeft.ResumeLayout(false);
            panelSearch.ResumeLayout(false);
            panelSearch.PerformLayout();
            panelRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            panelStats.ResumeLayout(false);
            panelStat3.ResumeLayout(false);
            panelStat2.ResumeLayout(false);
            panelStat1.ResumeLayout(false);
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            ((System.ComponentModel.ISupportInitialize)picChinhSua).EndInit();
            ResumeLayout(false);
        }

        private SplitContainer mainSplit;
        private Panel panelLeft;
        private FlowLayoutPanel listKhachHang;
        private Panel panelSearch;
        private TextBox txtSearch;
        private Panel panelRight;
        private DataGridView dgvHistory;
        private TableLayoutPanel panelStats;
        private Panel panelStat3;
        private Label lblStatValue3;
        private Label lblStatTitle3;
        private Panel panelStat2;
        private Label lblStatValue2;
        private Label lblStatTitle2;
        private Panel panelStat1;
        private Label lblStatValue1;
        private Label lblStatTitle1;
        private Panel panelHeader;
        private Label lblAddress;
        private Label lblPhone;
        private Label lblHoTen;
        private Label lblGhiChu;
        private PictureBox picAvatar;
        private PictureBox picChinhSua;
    }
}