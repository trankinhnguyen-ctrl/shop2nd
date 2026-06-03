using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace dosi
{
    public class TheHoaDon : UserControl
    {
        private Panel outerCard;
        private Panel headerPanel;
        private Label lblDate;
        private Label lblTotal;
        private Panel divider;
        private DataGridView dgv;

        public TheHoaDon()
        {
            outerCard = new Panel();
            headerPanel = new Panel();
            lblDate = new Label();
            lblTotal = new Label();
            divider = new Panel();
            dgv = new DataGridView();

            SetupDgv();
            SetupLayout();
        }

        private void SetupDgv()
        {
            var colTen = new DataGridViewTextBoxColumn
            {
                Name = "TenSP",
                HeaderText = "Tên sản phẩm",
                DataPropertyName = "TenSP"
            };
            var colSL = new DataGridViewTextBoxColumn
            {
                Name = "SoLuong",
                HeaderText = "SL",
                DataPropertyName = "SoLuong",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            var colGia = new DataGridViewTextBoxColumn
            {
                Name = "GiaBan",
                HeaderText = "Đơn giá",
                DataPropertyName = "GiaBan",
                DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            };
            var colTT = new DataGridViewTextBoxColumn
            {
                Name = "ThanhTien",
                HeaderText = "Thành tiền",
                DataPropertyName = "ThanhTien",
                DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            };

            dgv.AutoGenerateColumns = false;
            dgv.Columns.AddRange(colTen, colSL, colGia, colTT);
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.GridColor = Color.FromArgb(241, 245, 249);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 32;
            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 250, 252),
                ForeColor = Color.FromArgb(100, 116, 139),
                SelectionBackColor = Color.FromArgb(248, 250, 252),
                SelectionForeColor = Color.FromArgb(100, 116, 139),
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };
            dgv.RowTemplate.Height = 30;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(51, 65, 85);
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(238, 242, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(79, 70, 229);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.Dock = DockStyle.Fill;
            dgv.TabStop = false;

            dgv.DataBindingComplete += (s, e) =>
            {
                dgv.ClearSelection();
                dgv.CurrentCell = null;
            };
        }

        private void SetupLayout()
        {
            lblDate.Dock = DockStyle.Left;
            lblDate.Width = 220;
            lblDate.AutoSize = false;
            lblDate.TextAlign = ContentAlignment.MiddleLeft;
            lblDate.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            lblDate.ForeColor = Color.FromArgb(51, 65, 85);
            lblDate.BackColor = Color.Transparent;

            lblTotal.Dock = DockStyle.Fill;
            lblTotal.AutoSize = false;
            lblTotal.TextAlign = ContentAlignment.MiddleRight;
            lblTotal.Font = new Font("Segoe UI", 9.5f);
            lblTotal.ForeColor = Color.FromArgb(79, 70, 229);
            lblTotal.BackColor = Color.Transparent;

            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 50;
            headerPanel.BackColor = Color.FromArgb(248, 250, 252);
            headerPanel.Padding = new Padding(14, 0, 14, 0);
            headerPanel.Controls.Add(lblTotal);
            headerPanel.Controls.Add(lblDate);

            divider.Dock = DockStyle.Top;
            divider.Height = 1;
            divider.BackColor = Color.FromArgb(226, 232, 240);

            outerCard.Dock = DockStyle.Fill;
            outerCard.BackColor = Color.White;
            outerCard.BorderStyle = BorderStyle.FixedSingle;
            outerCard.Controls.Add(dgv);
            outerCard.Controls.Add(divider);
            outerCard.Controls.Add(headerPanel);

            this.BackColor = Color.Transparent;
            this.Margin = new Padding(0, 0, 0, 10);
            this.Controls.Add(outerCard);
        }

        public void LayDuLieu(string ngayTao, int soMon, decimal tongTien, List<dynamic> items)
        {
            if (DateTime.TryParse(ngayTao, out DateTime parsedDate))
                lblDate.Text = parsedDate.ToString("dd/MM/yyyy  HH:mm");
            else
                lblDate.Text = ngayTao;

            lblTotal.Text = $"{soMon} sản phẩm   •   {tongTien:N0}đ";

            var dt = new DataTable();
            dt.Columns.Add("TenSP", typeof(string));
            dt.Columns.Add("SoLuong", typeof(int));
            dt.Columns.Add("GiaBan", typeof(decimal));
            dt.Columns.Add("ThanhTien", typeof(decimal));

            foreach (dynamic item in items)
            {
                dt.Rows.Add(
                    item.TenSP?.ToString() ?? "",
                    Convert.ToInt32((object)(item.SoLuong ?? 0)),
                    Convert.ToDecimal((object)(item.GiaBan ?? 0)),
                    Convert.ToDecimal((object)(item.ThanhTien ?? 0)));
            }

            dgv.DataSource = dt;

            int dgvHeight = (items.Count * 30) + 32;
            this.Height = 50 + 1 + dgvHeight + 2;
        }
    }
}
