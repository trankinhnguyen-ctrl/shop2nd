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
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(170, 41);
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
            tableStats.Location = new Point(20, 80);
            tableStats.Name = "tableStats";
            tableStats.RowCount = 2;
            tableStats.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableStats.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableStats.Size = new Size(860, 300);
            tableStats.TabIndex = 1;
            // 
            // card4
            // 
            card4.BackColor = Color.White;
            card4.BorderStyle = BorderStyle.FixedSingle;
            card4.Controls.Add(lblVal4);
            card4.Controls.Add(lblTitle4);
            card4.Dock = DockStyle.Fill;
            card4.Location = new Point(440, 160);
            card4.Margin = new Padding(10);
            card4.Name = "card4";
            card4.Size = new Size(410, 130);
            card4.TabIndex = 0;
            // 
            // lblVal4
            // 
            lblVal4.Dock = DockStyle.Fill;
            lblVal4.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblVal4.ForeColor = Color.Crimson;
            lblVal4.Location = new Point(0, 45);
            lblVal4.Name = "lblVal4";
            lblVal4.Size = new Size(408, 83);
            lblVal4.TabIndex = 0;
            lblVal4.Text = "0";
            lblVal4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitle4
            // 
            lblTitle4.Dock = DockStyle.Top;
            lblTitle4.Font = new Font("Segoe UI", 10F);
            lblTitle4.ForeColor = Color.Gray;
            lblTitle4.Location = new Point(0, 0);
            lblTitle4.Name = "lblTitle4";
            lblTitle4.Padding = new Padding(10, 10, 0, 0);
            lblTitle4.Size = new Size(408, 45);
            lblTitle4.TabIndex = 1;
            lblTitle4.Text = "Cảnh báo hết hàng";
            lblTitle4.Click += lblTitle4_Click;
            // 
            // card3
            // 
            card3.BackColor = Color.White;
            card3.BorderStyle = BorderStyle.FixedSingle;
            card3.Controls.Add(lblVal3);
            card3.Controls.Add(lblTitle3);
            card3.Dock = DockStyle.Fill;
            card3.Location = new Point(10, 160);
            card3.Margin = new Padding(10);
            card3.Name = "card3";
            card3.Size = new Size(410, 130);
            card3.TabIndex = 1;
            // 
            // lblVal3
            // 
            lblVal3.Dock = DockStyle.Fill;
            lblVal3.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblVal3.Location = new Point(0, 45);
            lblVal3.Name = "lblVal3";
            lblVal3.Size = new Size(408, 83);
            lblVal3.TabIndex = 0;
            lblVal3.Text = "0";
            lblVal3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitle3
            // 
            lblTitle3.Dock = DockStyle.Top;
            lblTitle3.Font = new Font("Segoe UI", 10F);
            lblTitle3.ForeColor = Color.Gray;
            lblTitle3.Location = new Point(0, 0);
            lblTitle3.Name = "lblTitle3";
            lblTitle3.Padding = new Padding(10, 10, 0, 0);
            lblTitle3.Size = new Size(408, 45);
            lblTitle3.TabIndex = 1;
            lblTitle3.Text = "Số lượng khách";
            // 
            // card2
            // 
            card2.BackColor = Color.White;
            card2.BorderStyle = BorderStyle.FixedSingle;
            card2.Controls.Add(lblVal2);
            card2.Controls.Add(lblTitle2);
            card2.Dock = DockStyle.Fill;
            card2.Location = new Point(440, 10);
            card2.Margin = new Padding(10);
            card2.Name = "card2";
            card2.Size = new Size(410, 130);
            card2.TabIndex = 2;
            // 
            // lblVal2
            // 
            lblVal2.Dock = DockStyle.Fill;
            lblVal2.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblVal2.Location = new Point(0, 39);
            lblVal2.Name = "lblVal2";
            lblVal2.Size = new Size(408, 89);
            lblVal2.TabIndex = 0;
            lblVal2.Text = "0";
            lblVal2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitle2
            // 
            lblTitle2.Dock = DockStyle.Top;
            lblTitle2.Font = new Font("Segoe UI", 10F);
            lblTitle2.ForeColor = Color.Gray;
            lblTitle2.Location = new Point(0, 0);
            lblTitle2.Name = "lblTitle2";
            lblTitle2.Padding = new Padding(10, 10, 0, 0);
            lblTitle2.Size = new Size(408, 39);
            lblTitle2.TabIndex = 1;
            lblTitle2.Text = "Số lượng hàng";
            // 
            // card1
            // 
            card1.BackColor = Color.White;
            card1.BorderStyle = BorderStyle.FixedSingle;
            card1.Controls.Add(lblVal1);
            card1.Controls.Add(lblTitle1);
            card1.Dock = DockStyle.Fill;
            card1.Location = new Point(10, 10);
            card1.Margin = new Padding(10);
            card1.Name = "card1";
            card1.Size = new Size(410, 130);
            card1.TabIndex = 3;
            // 
            // lblVal1
            // 
            lblVal1.Dock = DockStyle.Fill;
            lblVal1.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblVal1.Location = new Point(0, 39);
            lblVal1.Name = "lblVal1";
            lblVal1.Size = new Size(408, 89);
            lblVal1.TabIndex = 0;
            lblVal1.Text = "0";
            lblVal1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitle1
            // 
            lblTitle1.Dock = DockStyle.Top;
            lblTitle1.Font = new Font("Segoe UI", 10F);
            lblTitle1.ForeColor = Color.Gray;
            lblTitle1.Location = new Point(0, 0);
            lblTitle1.Name = "lblTitle1";
            lblTitle1.Padding = new Padding(10, 10, 0, 0);
            lblTitle1.Size = new Size(408, 39);
            lblTitle1.TabIndex = 1;
            lblTitle1.Text = "Số lượng mẫu";
            // 
            // panelHistory
            // 
            panelHistory.BackColor = Color.White;
            panelHistory.BorderStyle = BorderStyle.FixedSingle;
            panelHistory.Controls.Add(flpHistory);
            panelHistory.Controls.Add(lblHistoryTitle);
            panelHistory.Location = new Point(30, 400);
            panelHistory.Name = "panelHistory";
            panelHistory.Size = new Size(840, 270);
            panelHistory.TabIndex = 0;
            // 
            // flpHistory
            // 
            flpHistory.AutoScroll = true;
            flpHistory.Dock = DockStyle.Fill;
            flpHistory.Location = new Point(0, 45);
            flpHistory.Name = "flpHistory";
            flpHistory.Padding = new Padding(10);
            flpHistory.Size = new Size(838, 223);
            flpHistory.TabIndex = 0;
            // 
            // lblHistoryTitle
            // 
            lblHistoryTitle.Dock = DockStyle.Top;
            lblHistoryTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblHistoryTitle.Location = new Point(0, 0);
            lblHistoryTitle.Name = "lblHistoryTitle";
            lblHistoryTitle.Padding = new Padding(15, 15, 0, 0);
            lblHistoryTitle.Size = new Size(838, 45);
            lblHistoryTitle.TabIndex = 1;
            lblHistoryTitle.Text = "Lịch sử khách hàng gần đây";
            // 
            // ViewTongQuan
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
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