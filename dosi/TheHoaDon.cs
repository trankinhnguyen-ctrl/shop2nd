using System;
using System.Collections.Generic;
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
        private Label lblStatus;
        private Button btnEdit;
        private Button btnPrint;
        private Panel divider;
        private DataGridView dgv;

        private string _maHoaDon = "";
        private Action<string>? _onEditClick;
        private Action<string>? _onPrintClick;

        public TheHoaDon()
        {
            outerCard = new Panel();
            headerPanel = new Panel();
            lblDate = new Label();
            lblTotal = new Label();
            lblStatus = new Label();
            btnEdit = new Button();
            btnPrint = new Button();
            divider = new Panel();
            dgv = new DataGridView();

            SetupDgv();
            SetupLayout();
        }

        private void SetupDgv()
        {
            var colTen = new DataGridViewTextBoxColumn { Name = "TenSP", HeaderText = "Tên sản phẩm", DataPropertyName = "TenSP" };
            var colSL = new DataGridViewTextBoxColumn
            {
                Name = "SoLuong", HeaderText = "SL", DataPropertyName = "SoLuong",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            var colGia = new DataGridViewTextBoxColumn
            {
                Name = "GiaBan", HeaderText = "Đơn giá", DataPropertyName = "GiaBan",
                DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            };
            var colTT = new DataGridViewTextBoxColumn
            {
                Name = "ThanhTien", HeaderText = "Thành tiền", DataPropertyName = "ThanhTien",
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
            dgv.DataBindingComplete += (s, e) => { dgv.ClearSelection(); dgv.CurrentCell = null; };
        }

        private void SetupLayout()
        {
            // btnEdit: Dock.Right — processed first (added last), docks to right edge
            btnEdit.Dock = DockStyle.Right;
            btnEdit.Width = 78;
            btnEdit.Text = "✎ Sửa";
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.FlatAppearance.BorderColor = Color.FromArgb(245, 158, 11);
            btnEdit.FlatAppearance.BorderSize = 1;
            btnEdit.BackColor = Color.FromArgb(255, 251, 235);
            btnEdit.ForeColor = Color.FromArgb(180, 83, 9);
            btnEdit.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btnEdit.Cursor = Cursors.Hand;
            btnEdit.Visible = false;
            btnEdit.Click += (s, e) => _onEditClick?.Invoke(_maHoaDon);

            var tt = new ToolTip();
            tt.SetToolTip(btnEdit, "Chỉnh sửa / Hoàn trả đơn hàng này");

            // btnPrint: Dock.Right — processed second, docks left of btnEdit
            btnPrint.Dock = DockStyle.Right;
            btnPrint.Width = 70;
            btnPrint.Text = "🖨 In";
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.FlatAppearance.BorderColor = Color.FromArgb(147, 197, 253);
            btnPrint.FlatAppearance.BorderSize = 1;
            btnPrint.BackColor = Color.FromArgb(239, 246, 255);
            btnPrint.ForeColor = Color.FromArgb(29, 78, 216);
            btnPrint.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btnPrint.Cursor = Cursors.Hand;
            btnPrint.Visible = false;
            btnPrint.Click += (s, e) => _onPrintClick?.Invoke(_maHoaDon);
            new ToolTip().SetToolTip(btnPrint, "In hóa đơn PDF");

            // lblDate: Dock.Left — processed second
            lblDate.Dock = DockStyle.Left;
            lblDate.Width = 190;
            lblDate.AutoSize = false;
            lblDate.TextAlign = ContentAlignment.MiddleLeft;
            lblDate.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            lblDate.ForeColor = Color.FromArgb(51, 65, 85);
            lblDate.BackColor = Color.Transparent;

            // lblStatus: Dock.Left — processed third
            lblStatus.Dock = DockStyle.Left;
            lblStatus.Width = 200;
            lblStatus.AutoSize = false;
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            lblStatus.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            lblStatus.BackColor = Color.Transparent;
            lblStatus.Visible = false;

            // lblTotal: Dock.Fill — processed last (fills remaining space)
            lblTotal.Dock = DockStyle.Fill;
            lblTotal.AutoSize = false;
            lblTotal.TextAlign = ContentAlignment.MiddleRight;
            lblTotal.Font = new Font("Segoe UI", 9.5f);
            lblTotal.ForeColor = Color.FromArgb(79, 70, 229);
            lblTotal.BackColor = Color.Transparent;

            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 50;
            headerPanel.BackColor = Color.FromArgb(248, 250, 252);
            headerPanel.Padding = new Padding(14, 0, 0, 0);
            // Reverse-processing order: btnEdit(R) → btnPrint(R left of edit) → lblDate(L) → lblStatus(L) → lblTotal(Fill)
            headerPanel.Controls.Add(lblTotal);
            headerPanel.Controls.Add(lblStatus);
            headerPanel.Controls.Add(lblDate);
            headerPanel.Controls.Add(btnPrint);
            headerPanel.Controls.Add(btnEdit);

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

        public void LayDuLieu(string ngayTao, int soMon, decimal tongTien, List<dynamic> items,
            string maHoaDon = "", string trangThai = "HoanThanh", string maHoaDonMoi = "",
            Action<string>? onEditClick = null, Action<string>? onPrintClick = null)
        {
            _maHoaDon = maHoaDon;
            _onEditClick = onEditClick;
            _onPrintClick = onPrintClick;
            btnPrint.Visible = !string.IsNullOrEmpty(maHoaDon) && onPrintClick != null;

            if (DateTime.TryParse(ngayTao, out DateTime parsedDate))
                lblDate.Text = parsedDate.ToString("dd/MM/yyyy  HH:mm");
            else
                lblDate.Text = ngayTao;

            lblTotal.Text = $"{soMon} sản phẩm   •   {tongTien:N0}đ";

            // Status badge
            switch (trangThai)
            {
                case "DangChinhSua":
                    lblStatus.Text = "🔄 Đang chỉnh sửa";
                    lblStatus.ForeColor = Color.FromArgb(180, 83, 9);
                    lblStatus.Visible = true;
                    btnEdit.Visible = !string.IsNullOrEmpty(maHoaDon);
                    btnEdit.Text = "✎ Tiếp tục";
                    new ToolTip().SetToolTip(btnEdit, "Tiếp tục chỉnh sửa đơn hàng này");
                    break;
                case "DaChinhSua":
                    lblStatus.Text = string.IsNullOrEmpty(maHoaDonMoi)
                        ? "✏ Đã chỉnh sửa"
                        : $"✏ Đã chỉnh sửa → {maHoaDonMoi}";
                    lblStatus.ForeColor = Color.FromArgb(100, 116, 139);
                    lblStatus.Font = new Font("Segoe UI", 8.5f, FontStyle.Italic);
                    lblStatus.Visible = true;
                    btnEdit.Visible = false;
                    break;
                default: // HoanThanh
                    lblStatus.Visible = false;
                    btnEdit.Visible = !string.IsNullOrEmpty(maHoaDon);
                    btnEdit.Text = "✎ Sửa";
                    break;
            }

            var dt = new System.Data.DataTable();
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
