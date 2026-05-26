namespace dosi
{
    partial class ViewTongQuan
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
            lblTitle = new Label();
            tableStats = new TableLayoutPanel();
            card4 = new Panel();
            lblVal4 = new Label();
            lblTitle4 = new Label();
            card3 = new Panel();
            lblVal3 = new Label();
            lblTitle3 = new Label();
            card2 = new Panel();
            lblVal2 = new Label();
            lblTitle2 = new Label();
            card1 = new Panel();
            lblVal1 = new Label();
            lblTitle1 = new Label();
            panelHistory = new Panel();
            flpHistory = new FlowLayoutPanel();
            lblHistoryTitle = new Label();
            tableStats.SuspendLayout();
            card4.SuspendLayout();
            card3.SuspendLayout();
            card2.SuspendLayout();
            card1.SuspendLayout();
            panelHistory.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTitle.Location = new Point(25, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(185, 46);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "Tổng quan";
            // 
            // tableStats
            // 
            tableStats.ColumnCount = 2;
            tableStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableStats.Controls.Add(card4, 1, 1);
            tableStats.Controls.Add(card3, 0, 1);
            tableStats.Controls.Add(card2, 1, 0);
            tableStats.Controls.Add(card1, 0, 0);
            tableStats.Location = new Point(20, 85);
            tableStats.Name = "tableStats";
            tableStats.RowCount = 2;
            tableStats.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableStats.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableStats.Size = new Size(860, 310);
            tableStats.TabIndex = 1;
            // 
            // card4 (Cảnh báo hết hàng)
            // 
            card4.BackColor = Color.White;
            card4.BorderStyle = BorderStyle.None;
            card4.Controls.Add(lblVal4);
            card4.Controls.Add(lblTitle4);
            card4.Dock = DockStyle.Fill;
            card4.Location = new Point(440, 165);
            card4.Margin = new Padding(12);
            card4.Name = "card4";
            card4.Size = new Size(408, 133);
            card4.TabIndex = 0;
            // 
            // lblVal4
            // 
            lblVal4.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblVal4.ForeColor = Color.FromArgb(30, 41, 59);
            lblVal4.Location = new Point(20, 55);
            lblVal4.Name = "lblVal4";
            lblVal4.Size = new Size(150, 60);
            lblVal4.TabIndex = 0;
            lblVal4.Text = "3";
            lblVal4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTitle4
            // 
            lblTitle4.Font = new Font("Segoe UI Semibold", 10.5F);
            lblTitle4.ForeColor = Color.FromArgb(100, 116, 139);
            lblTitle4.Location = new Point(20, 20);
            lblTitle4.Name = "lblTitle4";
            lblTitle4.Size = new Size(200, 30);
            lblTitle4.TabIndex = 1;
            lblTitle4.Text = "Cảnh báo hết hàng";
            lblTitle4.Click += lblTitle4_Click;
            // 
            // card3 (Số lượng khách)
            // 
            card3.BackColor = Color.White;
            card3.BorderStyle = BorderStyle.None;
            card3.Controls.Add(lblVal3);
            card3.Controls.Add(lblTitle3);
            card3.Dock = DockStyle.Fill;
            card3.Location = new Point(12, 165);
            card3.Margin = new Padding(12);
            card3.Name = "card3";
            card3.Size = new Size(408, 133);
            card3.TabIndex = 1;
            // 
            // lblVal3
            // 
            lblVal3.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblVal3.ForeColor = Color.FromArgb(30, 41, 59);
            lblVal3.Location = new Point(20, 55);
            lblVal3.Name = "lblVal3";
            lblVal3.Size = new Size(150, 60);
            lblVal3.TabIndex = 0;
            lblVal3.Text = "10";
            lblVal3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTitle3
            // 
            lblTitle3.Font = new Font("Segoe UI Semibold", 10.5F);
            lblTitle3.ForeColor = Color.FromArgb(100, 116, 139);
            lblTitle3.Location = new Point(20, 20);
            lblTitle3.Name = "lblTitle3";
            lblTitle3.Size = new Size(200, 30);
            lblTitle3.TabIndex = 1;
            lblTitle3.Text = "Số lượng khách";
            // 
            // card2 (Số lượng hàng)
            // 
            card2.BackColor = Color.White;
            card2.BorderStyle = BorderStyle.None;
            card2.Controls.Add(lblVal2);
            card2.Controls.Add(lblTitle2);
            card2.Dock = DockStyle.Fill;
            card2.Location = new Point(440, 12);
            card2.Margin = new Padding(12);
            card2.Name = "card2";
            card2.Size = new Size(408, 133);
            card2.TabIndex = 2;
            // 
            // lblVal2
            // 
            lblVal2.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblVal2.ForeColor = Color.FromArgb(30, 41, 59);
            lblVal2.Location = new Point(20, 55);
            lblVal2.Name = "lblVal2";
            lblVal2.Size = new Size(150, 60);
            lblVal2.TabIndex = 0;
            lblVal2.Text = "200";
            lblVal2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTitle2
            // 
            lblTitle2.Font = new Font("Segoe UI Semibold", 10.5F);
            lblTitle2.ForeColor = Color.FromArgb(100, 116, 139);
            lblTitle2.Location = new Point(20, 20);
            lblTitle2.Name = "lblTitle2";
            lblTitle2.Size = new Size(200, 30);
            lblTitle2.TabIndex = 1;
            lblTitle2.Text = "Số lượng hàng";
            // 
            // card1 (Số lượng mẫu)
            // 
            card1.BackColor = Color.White;
            card1.BorderStyle = BorderStyle.None;
            card1.Controls.Add(lblVal1);
            card1.Controls.Add(lblTitle1);
            card1.Dock = DockStyle.Fill;
            card1.Location = new Point(12, 12);
            card1.Margin = new Padding(12);
            card1.Name = "card1";
            card1.Size = new Size(408, 133);
            card1.TabIndex = 3;
            // 
            // lblVal1
            // 
            lblVal1.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblVal1.ForeColor = Color.FromArgb(30, 41, 59);
            lblVal1.Location = new Point(20, 55);
            lblVal1.Name = "lblVal1";
            lblVal1.Size = new Size(150, 60);
            lblVal1.TabIndex = 0;
            lblVal1.Text = "150";
            lblVal1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTitle1
            // 
            lblTitle1.Font = new Font("Segoe UI Semibold", 10.5F);
            lblTitle1.ForeColor = Color.FromArgb(100, 116, 139);
            lblTitle1.Location = new Point(20, 20);
            lblTitle1.Name = "lblTitle1";
            lblTitle1.Size = new Size(200, 30);
            lblTitle1.TabIndex = 1;
            lblTitle1.Text = "Số lượng mẫu";
            // 
            // panelHistory
            // 
            panelHistory.BackColor = Color.White;
            panelHistory.BorderStyle = BorderStyle.None;
            panelHistory.Controls.Add(flpHistory);
            panelHistory.Controls.Add(lblHistoryTitle);
            panelHistory.Location = new Point(32, 415);
            panelHistory.Name = "panelHistory";
            panelHistory.Size = new Size(836, 255);
            panelHistory.TabIndex = 0;
            // 
            // flpHistory
            // 
            flpHistory.AutoScroll = true;
            flpHistory.BackColor = Color.White;
            flpHistory.Dock = DockStyle.Fill;
            flpHistory.Location = new Point(0, 50);
            flpHistory.Name = "flpHistory";
            flpHistory.Padding = new Padding(20, 10, 20, 10);
            flpHistory.Size = new Size(836, 205);
            flpHistory.TabIndex = 0;
            // 
            // lblHistoryTitle
            // 
            lblHistoryTitle.Dock = DockStyle.Top;
            lblHistoryTitle.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            lblHistoryTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblHistoryTitle.Location = new Point(0, 0);
            lblHistoryTitle.Name = "lblHistoryTitle";
            lblHistoryTitle.Padding = new Padding(20, 15, 0, 0);
            lblHistoryTitle.Size = new Size(836, 50);
            lblHistoryTitle.TabIndex = 1;
            lblHistoryTitle.Text = "Hoạt động gần đây";
            // 
            // ViewTongQuan
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            Controls.Add(panelHistory);
            Controls.Add(tableStats);
            Controls.Add(lblTitle);
            Name = "ViewTongQuan";
            Size = new Size(900, 700);
            tableStats.ResumeLayout(false);
            card4.ResumeLayout(false);
            card3.ResumeLayout(false);
            card2.ResumeLayout(false);
            card1.ResumeLayout(false);
            panelHistory.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private TableLayoutPanel tableStats;
        private Panel card1;
        private Label lblVal1;
        private Label lblTitle1;
        private Panel card2;
        private Label lblVal2;
        private Label lblTitle2;
        private Panel card3;
        private Label lblVal3;
        private Label lblTitle3;
        private Panel card4;
        private Label lblVal4;
        private Label lblTitle4;
        private Panel panelHistory;
        private Label lblHistoryTitle;
        private FlowLayoutPanel flpHistory;
    }
}