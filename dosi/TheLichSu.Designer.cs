namespace dosi
{
    partial class TheLichSu
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
            this.lblTenSP = new System.Windows.Forms.Label();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.lblThanhTien = new System.Windows.Forms.Label();
            this.lblNgayTao = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.lblTenSP.AutoSize = true;
            this.lblTenSP.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTenSP.Location = new System.Drawing.Point(12, 10);
            this.lblTenSP.Name = "lblTenSP";
            this.lblTenSP.Size = new System.Drawing.Size(120, 23);
            this.lblTenSP.Text = "Tên sản phẩm";

            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSoLuong.ForeColor = System.Drawing.Color.DimGray;
            this.lblSoLuong.Location = new System.Drawing.Point(12, 38);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(91, 20);
            this.lblSoLuong.Text = "Số lượng: 1";

            this.lblThanhTien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblThanhTien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblThanhTien.ForeColor = System.Drawing.Color.Crimson;
            this.lblThanhTien.Location = new System.Drawing.Point(220, 10);
            this.lblThanhTien.Name = "lblThanhTien";
            this.lblThanhTien.Size = new System.Drawing.Size(160, 23);
            this.lblThanhTien.Text = "0đ";
            this.lblThanhTien.TextAlign = System.Drawing.ContentAlignment.TopRight;

            this.lblNgayTao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNgayTao.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblNgayTao.ForeColor = System.Drawing.Color.Gray;
            this.lblNgayTao.Location = new System.Drawing.Point(220, 38);
            this.lblNgayTao.Name = "lblNgayTao";
            this.lblNgayTao.Size = new System.Drawing.Size(160, 20);
            this.lblNgayTao.Text = "2024-01-01 00:00";
            this.lblNgayTao.TextAlign = System.Drawing.ContentAlignment.TopRight;

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblNgayTao);
            this.Controls.Add(this.lblThanhTien);
            this.Controls.Add(this.lblSoLuong);
            this.Controls.Add(this.lblTenSP);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.Name = "TheLichSu";
            this.Size = new System.Drawing.Size(395, 70);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTenSP;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.Label lblThanhTien;
        private System.Windows.Forms.Label lblNgayTao;
    }
}