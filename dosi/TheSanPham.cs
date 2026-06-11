using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace dosi
{
    public partial class TheSanPham : UserControl
    {
        public Action? OnSelect { get; set; }
        private bool _isOutOfStock = false;

        public TheSanPham()
        {
            InitializeComponent();

            // Kích hoạt double-buffered để khi cuộn danh sách card không bị nhấp nháy
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            // Ủy quyền sự kiện click từ thẻ và tất cả các control con
            this.Click += (s, e) => OnSelect?.Invoke();
            foreach (Control child in this.Controls)
            {
                child.Click += (s, e) => OnSelect?.Invoke();
            }

            // Đăng ký sự kiện vẽ giao diện bo góc mượt mà cho thẻ
            this.Paint += TheSanPham_Paint;

            // Đăng ký sự kiện vẽ bo góc trên cho PictureBox để ảnh không bị tràn ra ngoài góc bo
            HinhAnh.Paint += HinhAnh_Paint;
        }

        private const int RADIUS = 16;

        // 1. Set rounded Region + draw bottom-half border (top is covered by HinhAnh child)
        private void TheSanPham_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath regionPath = new GraphicsPath())
            {
                regionPath.StartFigure();
                regionPath.AddArc(new Rectangle(0, 0, RADIUS, RADIUS), 180, 90);
                regionPath.AddArc(new Rectangle(this.Width - RADIUS - 1, 0, RADIUS, RADIUS), 270, 90);
                regionPath.AddArc(new Rectangle(this.Width - RADIUS - 1, this.Height - RADIUS - 1, RADIUS, RADIUS), 0, 90);
                regionPath.AddArc(new Rectangle(0, this.Height - RADIUS - 1, RADIUS, RADIUS), 90, 90);
                regionPath.CloseFigure();
                this.Region = new Region(regionPath);
            }

            DrawCardBorder(e.Graphics);
        }

        // 2. Round top corners of image + draw top-half border on top of the image
        private void HinhAnh_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath clipPath = new GraphicsPath())
            {
                clipPath.StartFigure();
                clipPath.AddArc(new Rectangle(0, 0, RADIUS, RADIUS), 180, 90);
                clipPath.AddArc(new Rectangle(HinhAnh.Width - RADIUS - 1, 0, RADIUS, RADIUS), 270, 90);
                clipPath.AddLine(HinhAnh.Width - 1, HinhAnh.Height, 0, HinhAnh.Height);
                clipPath.CloseFigure();
                HinhAnh.Region = new Region(clipPath);
            }

            // Translate to card coordinate space and draw the border; the HinhAnh bounds
            // naturally clip the drawing to just the top portion of the card.
            e.Graphics.TranslateTransform(-HinhAnh.Left, -HinhAnh.Top);
            DrawCardBorder(e.Graphics);
            e.Graphics.ResetTransform();
        }

        // Draws the full card border path inset by half pen-width so the stroke stays inside the Region.
        private void DrawCardBorder(Graphics g)
        {
            Color borderColor = _isOutOfStock ? Color.FromArgb(239, 68, 68) : Color.FromArgb(226, 232, 240);
            float borderWidth = _isOutOfStock ? 5f : 1.5f;
            float inset = borderWidth / 2f;
            int W = this.Width;
            int H = this.Height;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.StartFigure();
                path.AddArc(new RectangleF(inset, inset, RADIUS, RADIUS), 180, 90);
                path.AddArc(new RectangleF(W - RADIUS - 1 - inset, inset, RADIUS, RADIUS), 270, 90);
                path.AddArc(new RectangleF(W - RADIUS - 1 - inset, H - RADIUS - 1 - inset, RADIUS, RADIUS), 0, 90);
                path.AddArc(new RectangleF(inset, H - RADIUS - 1 - inset, RADIUS, RADIUS), 90, 90);
                path.CloseFigure();

                using (Pen pen = new Pen(borderColor, borderWidth))
                    g.DrawPath(pen, path);
            }
        }

        public void LayDuLieu(SanPham sp)
        {
            _isOutOfStock = sp.SoLuong <= 0;
            TenSP.Text = sp.TenSP;
            MaSP.Text = sp.MaSP;
            SL.Text = "Tồn: " + sp.SoLuong;
            GiaBan.Text = sp.GiaBan.ToString("N0") + "đ";

            if (string.IsNullOrEmpty(sp.HinhAnh))
            {
                HinhAnh.Image = null;
                return;
            }

            string path = Path.Combine(Application.StartupPath, sp.HinhAnh);
            if (File.Exists(path))
            {
                try
                {
                    // Đọc ảnh qua FileStream bảo đảm không làm lock cứng file ở thư mục Images
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        HinhAnh.Image = Image.FromStream(fs);
                    }
                }
                catch
                {
                    HinhAnh.Image = null;
                }
            }
            else
            {
                HinhAnh.Image = null;
            }
        }
    }
}