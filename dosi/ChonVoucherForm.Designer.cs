namespace dosi
{
    partial class ChonVoucherForm
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
            lblTitle = new Label();
            pnlScroll = new Panel();
            btnClose = new Button();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(51, 65, 85);
            lblTitle.Location = new Point(16, 16);
            lblTitle.Name = "lblTitle";
            lblTitle.TabIndex = 0;
            lblTitle.Text = "...";
            //
            // pnlScroll
            //
            pnlScroll.AutoScroll = true;
            pnlScroll.BackColor = Color.FromArgb(248, 250, 252);
            pnlScroll.Location = new Point(12, 46);
            pnlScroll.Name = "pnlScroll";
            pnlScroll.Size = new Size(436, 408);
            pnlScroll.TabIndex = 1;
            //
            // btnClose
            //
            btnClose.BackColor = Color.FromArgb(241, 245, 249);
            btnClose.Cursor = Cursors.Hand;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 9.5F);
            btnClose.ForeColor = Color.FromArgb(51, 65, 85);
            btnClose.Location = new Point(308, 462);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(140, 34);
            btnClose.TabIndex = 2;
            btnClose.Text = "Đóng";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.FlatAppearance.BorderSize = 1;
            btnClose.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            //
            // ChonVoucherForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(460, 500);
            Controls.Add(btnClose);
            Controls.Add(pnlScroll);
            Controls.Add(lblTitle);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "ChonVoucherForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chọn khuyến mãi áp dụng";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Panel pnlScroll;
        private Button btnClose;
    }
}
