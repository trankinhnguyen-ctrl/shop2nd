using System;
using System.Drawing;
using System.Windows.Forms;

namespace dosi
{
    public class TheHoatDong : UserControl
    {
        private Panel borderPanel;
        private Panel contentPanel;
        private TableLayoutPanel table;
        private Label lblCustomer;
        private Label lblTotal;
        private Label lblTime;
        private Label lblItems;

        public int KhachId { get; private set; }
        public Action? OnSelect { get; set; }

        public TheHoatDong()
        {
            lblCustomer = new Label();
            lblTotal = new Label();
            lblTime = new Label();
            lblItems = new Label();
            table = new TableLayoutPanel();
            contentPanel = new Panel();
            borderPanel = new Panel();
            SetupControls();
            SetupLayout();
        }

        private void SetupControls()
        {
            lblCustomer.AutoSize = false;
            lblCustomer.Dock = DockStyle.Fill;
            lblCustomer.TextAlign = ContentAlignment.MiddleLeft;
            lblCustomer.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            lblCustomer.ForeColor = Color.FromArgb(30, 41, 59);
            lblCustomer.BackColor = Color.White;

            lblTotal.AutoSize = false;
            lblTotal.Dock = DockStyle.Fill;
            lblTotal.TextAlign = ContentAlignment.MiddleRight;
            lblTotal.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(79, 70, 229);
            lblTotal.BackColor = Color.White;

            lblTime.AutoSize = false;
            lblTime.Dock = DockStyle.Fill;
            lblTime.TextAlign = ContentAlignment.MiddleLeft;
            lblTime.Font = new Font("Segoe UI", 9f);
            lblTime.ForeColor = Color.FromArgb(100, 116, 139);
            lblTime.BackColor = Color.White;

            lblItems.AutoSize = false;
            lblItems.Dock = DockStyle.Fill;
            lblItems.TextAlign = ContentAlignment.MiddleRight;
            lblItems.Font = new Font("Segoe UI", 9f);
            lblItems.ForeColor = Color.FromArgb(100, 116, 139);
            lblItems.BackColor = Color.White;
        }

        private void SetupLayout()
        {
            table.Dock = DockStyle.Fill;
            table.RowCount = 2;
            table.ColumnCount = 2;
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 55f));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 45f));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55f));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45f));
            table.BackColor = Color.White;
            table.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            table.Margin = new Padding(0);
            table.Padding = new Padding(0);
            table.Controls.Add(lblCustomer, 0, 0);
            table.Controls.Add(lblTotal, 1, 0);
            table.Controls.Add(lblTime, 0, 1);
            table.Controls.Add(lblItems, 1, 1);

            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.White;
            contentPanel.Padding = new Padding(16, 10, 16, 10);
            contentPanel.Controls.Add(table);

            borderPanel.Dock = DockStyle.Fill;
            borderPanel.BackColor = Color.FromArgb(226, 232, 240);
            borderPanel.Padding = new Padding(1);
            borderPanel.Controls.Add(contentPanel);

            this.Height = 76;
            this.BackColor = Color.Transparent;
            this.Margin = new Padding(0, 0, 0, 10);
            this.Cursor = Cursors.Hand;
            this.Controls.Add(borderPanel);

            EventHandler fireSelect = (s, e) => OnSelect?.Invoke();
            Control[] clickTargets = { this, borderPanel, contentPanel, table, lblCustomer, lblTotal, lblTime, lblItems };
            foreach (var c in clickTargets)
                c.Click += fireSelect;

            EventHandler enterHandler = (s, e) => ApplyHover(true);
            EventHandler leaveHandler = (s, e) =>
            {
                if (!ClientRectangle.Contains(PointToClient(Cursor.Position)))
                    ApplyHover(false);
            };
            foreach (var c in clickTargets)
            {
                c.MouseEnter += enterHandler;
                c.MouseLeave += leaveHandler;
            }
        }

        private void ApplyHover(bool on)
        {
            Color bg = on ? Color.FromArgb(245, 243, 255) : Color.White;
            contentPanel.BackColor = bg;
            table.BackColor = bg;
            lblCustomer.BackColor = bg;
            lblTotal.BackColor = bg;
            lblTime.BackColor = bg;
            lblItems.BackColor = bg;
        }

        public void LayDuLieu(string hoTen, string ngayTao, int tongSoLuong, decimal tongTien, int khachId)
        {
            KhachId = khachId;
            lblCustomer.Text = hoTen;
            if (DateTime.TryParse(ngayTao, out DateTime dt))
                lblTime.Text = dt.ToString("dd/MM/yyyy  HH:mm");
            else
                lblTime.Text = ngayTao;
            lblItems.Text = $"Mua {tongSoLuong} sản phẩm";
            lblTotal.Text = tongTien.ToString("N0") + "đ";
        }
    }
}
