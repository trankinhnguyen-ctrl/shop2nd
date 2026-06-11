using System.Drawing.Drawing2D;

namespace dosi
{
    public partial class ChonVoucherForm : Form
    {
        public KhuyenMai? SelectedVoucher { get; private set; }

        private readonly List<KhuyenMai> _vouchers;

        public ChonVoucherForm(List<KhuyenMai> vouchers)
        {
            _vouchers = vouchers;
            InitializeComponent();
            lblTitle.Text = _vouchers.Count > 0
                ? $"Có {_vouchers.Count} khuyến mãi có thể áp dụng — click để chọn:"
                : "Không có khuyến mãi nào khả dụng.";
            LoadCards();
        }

        private void LoadCards()
        {
            int y = 8;
            foreach (var km in _vouchers)
            {
                var card = BuildCard(km, pnlScroll.Width - 24);
                card.Location = new Point(8, y);
                pnlScroll.Controls.Add(card);
                y += card.Height + 8;
            }
        }

        private Panel BuildCard(KhuyenMai km, int width)
        {
            Color accent = Color.FromArgb(99, 102, 241);

            bool hasValidity = !string.IsNullOrEmpty(km.NgayKetThuc) || km.SoLuongTon.HasValue;
            int cardHeight = hasValidity ? 82 : 68;

            var card = new Panel
            {
                Size = new Size(width, cardHeight),
                BackColor = Color.White,
                Cursor = Cursors.Hand,
                Tag = km
            };

            card.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using var path = RoundRect(new Rectangle(1, 1, card.Width - 3, card.Height - 3), 8);
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1f);
                g.DrawPath(pen, path);
            };

            Action pick = () =>
            {
                SelectedVoucher = km;
                DialogResult = DialogResult.OK;
                Close();
            };

            card.Click += (s, e) => pick();

            var badge = new Label
            {
                Text = km.MaKM,
                Location = new Point(12, 12),
                Size = new Size(82, 24),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Bold),
                ForeColor = accent,
                BackColor = Color.FromArgb(238, 242, 255)
            };
            badge.Click += (s, e) => pick();
            card.Controls.Add(badge);

            var lblName = new Label
            {
                Text = km.TenKM,
                Location = new Point(104, 10),
                Size = new Size(width - 116, 22),
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };
            lblName.Click += (s, e) => pick();
            card.Controls.Add(lblName);

            string giamText = km.LoaiGiamGia == 1
                ? $"Giảm {km.GiaTriGiam:N0}đ"
                : $"Giảm {km.GiaTriGiam:N0}%" + (km.GiamToiDa.HasValue ? $" (tối đa {km.GiamToiDa.Value:N0}đ)" : "");

            string condText = km.LoaiDieuKien switch
            {
                1 => $"  ·  Đơn từ {km.GiaTriDieuKien:N0}đ",
                2 => $"  ·  Từ {(int)(km.GiaTriDieuKien ?? 0)} sản phẩm",
                3 => $"  ·  Khách ≥ {(int)(km.GiaTriDieuKien ?? 0)} tháng",
                _ => ""
            };

            var lblDiscount = new Label
            {
                Text = giamText + condText,
                Location = new Point(104, 34),
                Size = new Size(width - 116, 20),
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.FromArgb(22, 163, 74),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };
            lblDiscount.Click += (s, e) => pick();
            card.Controls.Add(lblDiscount);

            if (hasValidity)
            {
                string parts = "";
                if (!string.IsNullOrEmpty(km.NgayKetThuc))
                    parts = $"HSD: {km.NgayKetThuc}";
                if (km.SoLuongTon.HasValue)
                    parts += (parts.Length > 0 ? "  ·  " : "") + $"Còn {km.SoLuongTon.Value} lượt";

                var lblValid = new Label
                {
                    Text = parts,
                    Location = new Point(104, 56),
                    Size = new Size(width - 116, 18),
                    Font = new Font("Segoe UI", 8f),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    TextAlign = ContentAlignment.MiddleLeft,
                    BackColor = Color.Transparent
                };
                lblValid.Click += (s, e) => pick();
                card.Controls.Add(lblValid);
            }

            return card;
        }

        private static GraphicsPath RoundRect(Rectangle r, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
