using System.Xml.Linq;

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

        #region Component Designer generated code

        private void InitializeComponent()
        {
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
            picAvatar = new PictureBox();
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
            SuspendLayout();

            mainSplit.Dock = DockStyle.Fill;
            mainSplit.Location = new Point(0, 0);
            mainSplit.Name = "mainSplit";
            mainSplit.Panel1.Controls.Add(panelLeft);
            mainSplit.Panel2.Controls.Add(panelRight);
            mainSplit.Size = new Size(1100, 700);
            mainSplit.SplitterDistance = 350;
            mainSplit.TabIndex = 0;

            panelLeft.Controls.Add(listKhachHang);
            panelLeft.Controls.Add(panelSearch);
            panelLeft.Dock = DockStyle.Fill;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Padding = new Padding(10);
            panelLeft.Size = new Size(350, 700);
            panelLeft.TabIndex = 0;

            listKhachHang.AutoScroll = true;
            listKhachHang.Dock = DockStyle.Fill;
            listKhachHang.Location = new Point(10, 60);
            listKhachHang.Name = "listKhachHang";
            listKhachHang.Size = new Size(330, 630);
            listKhachHang.TabIndex = 1;

            panelSearch.Controls.Add(txtSearch);
            panelSearch.Dock = DockStyle.Top;
            panelSearch.Location = new Point(10, 10);
            panelSearch.Name = "panelSearch";
            panelSearch.Size = new Size(330, 50);
            panelSearch.TabIndex = 0;

            txtSearch.Dock = DockStyle.Top;
            txtSearch.Font = new Font("Segoe UI", 11F);
            txtSearch.Location = new Point(0, 0);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = " Tìm kiếm khách hàng...";
            txtSearch.Size = new Size(330, 32);
            txtSearch.TabIndex = 0;

            panelRight.Controls.Add(dgvHistory);
            panelRight.Controls.Add(panelStats);
            panelRight.Controls.Add(panelHeader);
            panelRight.Dock = DockStyle.Fill;
            panelRight.Location = new Point(0, 0);
            panelRight.Name = "panelRight";
            panelRight.Padding = new Padding(20);
            panelRight.Size = new Size(746, 700);
            panelRight.TabIndex = 0;

            dgvHistory.BackgroundColor = Color.White;
            dgvHistory.BorderStyle = BorderStyle.None;
            dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistory.Dock = DockStyle.Fill;
            dgvHistory.Location = new Point(20, 260);
            dgvHistory.Name = "dgvHistory";
            dgvHistory.Size = new Size(706, 420);
            dgvHistory.TabIndex = 2;

            panelStats.ColumnCount = 3;
            panelStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            panelStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            panelStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            panelStats.Controls.Add(panelStat3, 2, 0);
            panelStats.Controls.Add(panelStat2, 1, 0);
            panelStats.Controls.Add(panelStat1, 0, 0);
            panelStats.Dock = DockStyle.Top;
            panelStats.Location = new Point(20, 140);
            panelStats.Name = "panelStats";
            panelStats.RowCount = 1;
            panelStats.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelStats.Size = new Size(706, 120);
            panelStats.TabIndex = 1;

            panelStat3.BackColor = Color.WhiteSmoke;
            panelStat3.Controls.Add(lblStatValue3);
            panelStat3.Controls.Add(lblStatTitle3);
            panelStat3.Dock = DockStyle.Fill;
            panelStat3.Location = new Point(473, 10);
            panelStat3.Name = "panelStat3";
            panelStat3.Size = new Size(230, 100);

            lblStatValue3.Dock = DockStyle.Fill;
            lblStatValue3.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblStatValue3.Location = new Point(0, 30);
            lblStatValue3.Text = "2.5";
            lblStatValue3.TextAlign = ContentAlignment.MiddleCenter;

            lblStatTitle3.Dock = DockStyle.Top;
            lblStatTitle3.Text = "NĂM THÀNH VIÊN";
            lblStatTitle3.TextAlign = ContentAlignment.MiddleCenter;

            panelStat2.BackColor = Color.WhiteSmoke;
            panelStat2.Controls.Add(lblStatValue2);
            panelStat2.Controls.Add(lblStatTitle2);
            panelStat2.Dock = DockStyle.Fill;
            panelStat2.Location = new Point(238, 10);
            panelStat2.Name = "panelStat2";
            panelStat2.Size = new Size(229, 100);

            lblStatValue2.Dock = DockStyle.Fill;
            lblStatValue2.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblStatValue2.Text = "4.5M";
            lblStatValue2.TextAlign = ContentAlignment.MiddleCenter;

            lblStatTitle2.Dock = DockStyle.Top;
            lblStatTitle2.Text = "TỔNG CHI TIÊU";
            lblStatTitle2.TextAlign = ContentAlignment.MiddleCenter;

            panelStat1.BackColor = Color.WhiteSmoke;
            panelStat1.Controls.Add(lblStatValue1);
            panelStat1.Controls.Add(lblStatTitle1);
            panelStat1.Dock = DockStyle.Fill;
            panelStat1.Location = new Point(3, 10);
            panelStat1.Name = "panelStat1";
            panelStat1.Size = new Size(229, 100);

            lblStatValue1.Dock = DockStyle.Fill;
            lblStatValue1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblStatValue1.Text = "18";
            lblStatValue1.TextAlign = ContentAlignment.MiddleCenter;

            lblStatTitle1.Dock = DockStyle.Top;
            lblStatTitle1.Text = "ĐƠN HÀNG";
            lblStatTitle1.TextAlign = ContentAlignment.MiddleCenter;

            panelHeader.Controls.Add(lblAddress);
            panelHeader.Controls.Add(lblPhone);
            panelHeader.Controls.Add(lblHoTen);
            panelHeader.Controls.Add(picAvatar);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(20, 20);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(706, 120);

            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(135, 80);
            lblAddress.Text = "Hải Châu, Đà Nẵng";

            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(135, 55);
            lblPhone.Text = "0905123456";

            lblHoTen.AutoSize = true;
            lblHoTen.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblHoTen.Location = new Point(135, 15);
            lblHoTen.Text = "Nguyễn Văn An";

            picAvatar.BackColor = Color.Gainsboro;
            picAvatar.Location = new Point(15, 10);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(100, 100);
            picAvatar.SizeMode = PictureBoxSizeMode.StretchImage;

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
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
            ResumeLayout(false);
        }

        #endregion

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
        private PictureBox picAvatar;
    }
}