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

        // 1. Tự vẽ bo góc 16px và viền mảnh nhẹ (Figma Light Border) cho Thẻ sản phẩm
        private void TheSanPham_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            int radius = 16;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.StartFigure();
                path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
                path.AddArc(new Rectangle(this.Width - radius - 1, 0, radius, radius), 270, 90);
                path.AddArc(new Rectangle(this.Width - radius - 1, this.Height - radius - 1, radius, radius), 0, 90);
                path.AddArc(new Rectangle(0, this.Height - radius - 1, radius, radius), 90, 90);
                path.CloseFigure();

                // Cắt vùng hiển thị của UserControl theo hình bo góc
                this.Region = new Region(path);

                // Vẽ đường viền mảnh tinh tế bao quanh thẻ
                using (Pen borderPen = new Pen(Color.FromArgb(226, 232, 240), 1.5f))
                {
                    e.Graphics.DrawPath(borderPen, path);
                }
            }
        }

        // 2. Bo tròn 2 góc trên của ảnh để khớp hoàn toàn với độ cong của thẻ sản phẩm
        private void HinhAnh_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            int radius = 16;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.StartFigure();
                path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
                path.AddArc(new Rectangle(HinhAnh.Width - radius - 1, 0, radius, radius), 270, 90);
                path.AddLine(HinhAnh.Width - 1, HinhAnh.Height, 0, HinhAnh.Height);
                path.CloseFigure();

                HinhAnh.Region = new Region(path);
            }
        }

        public void LayDuLieu(SanPham sp)
        {
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