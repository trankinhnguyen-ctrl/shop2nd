using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace dosi
{
    public partial class ViewTongQuan : UserControl
    {
        private string ConnectionString = "Data Source=QuanLyKho.db";

        public ViewTongQuan()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Load += ViewTongQuan_Load;
            this.SizeChanged += (s, e) => this.Invalidate(true);

            panelHistory.Paint += Panels_Paint;
            panelHistory.SizeChanged += (s, e) => panelHistory.Invalidate();

            SetupStatCards();

            flpHistory.FlowDirection = FlowDirection.TopDown;
            flpHistory.WrapContents = false;
            flpHistory.SizeChanged += (s, e) =>
            {
                int w = Math.Max(flpHistory.ClientSize.Width - flpHistory.Padding.Horizontal, 300);
                foreach (Control ctrl in flpHistory.Controls)
                {
                    if (ctrl is TheHoatDong card)
                        card.Width = w;
                }
            };
        }

        private void SetupStatCards()
        {
            var specs = new (Panel card, Label title, Color accent, Action<Graphics, Rectangle, Color> icon)[]
            {
                (card1, lblTitle1, Color.FromArgb(99,  102, 241), DrawIconBars),
                (card2, lblTitle2, Color.FromArgb(16,  185, 129), DrawIconBox),
                (card3, lblTitle3, Color.FromArgb(245, 158,  11), DrawIconPerson),
                (card4, lblTitle4, Color.FromArgb(239,  68,  68), DrawIconWarning),
            };

            foreach (var (card, title, accent, icon) in specs)
            {
                title.ForeColor = accent;
                var a = accent;
                var ic = icon;
                var p = card;
                p.Paint += (s, e) => DrawStatCard(e, p, a, ic);
                p.SizeChanged += (s, e) => p.Invalidate();
            }
        }

        private void DrawStatCard(PaintEventArgs e, Panel pnl, Color accent, Action<Graphics, Rectangle, Color> drawIcon)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            const int radius = 24;
            using var path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
            path.AddArc(new Rectangle(pnl.Width - radius - 1, 0, radius, radius), 270, 90);
            path.AddArc(new Rectangle(pnl.Width - radius - 1, pnl.Height - radius - 1, radius, radius), 0, 90);
            path.AddArc(new Rectangle(0, pnl.Height - radius - 1, radius, radius), 90, 90);
            path.CloseFigure();

            // Clear anti-alias fringe with parent background
            using (var bg = new SolidBrush(this.BackColor))
                g.FillRectangle(bg, pnl.ClientRectangle);

            // White card fill
            using (var white = new SolidBrush(Color.White))
                g.FillPath(white, path);

            // 5px colored top accent strip, clipped to rounded shape
            var state = g.Save();
            g.SetClip(path);
            using (var strip = new SolidBrush(accent))
                g.FillRectangle(strip, 0, 0, pnl.Width, 5);
            g.Restore(state);

            // Subtle border
            using (var border = new Pen(Color.FromArgb(226, 232, 240), 1))
                g.DrawPath(border, path);

            // Icon badge — rounded square, top-right
            const int badgeSize = 44;
            const int badgeRadius = 12;
            const int by = 18;
            int bx = pnl.Width - badgeSize - 18;

            using var badgePath = new GraphicsPath();
            badgePath.StartFigure();
            badgePath.AddArc(new Rectangle(bx, by, badgeRadius, badgeRadius), 180, 90);
            badgePath.AddArc(new Rectangle(bx + badgeSize - badgeRadius, by, badgeRadius, badgeRadius), 270, 90);
            badgePath.AddArc(new Rectangle(bx + badgeSize - badgeRadius, by + badgeSize - badgeRadius, badgeRadius, badgeRadius), 0, 90);
            badgePath.AddArc(new Rectangle(bx, by + badgeSize - badgeRadius, badgeRadius, badgeRadius), 90, 90);
            badgePath.CloseFigure();

            using (var badgeFill = new SolidBrush(Color.FromArgb(22, accent.R, accent.G, accent.B)))
                g.FillPath(badgeFill, badgePath);

            const int pad = 10;
            drawIcon(g, new Rectangle(bx + pad, by + pad, badgeSize - pad * 2, badgeSize - pad * 2), accent);
        }

        // 3 horizontal bars — represents a catalog / item list
        private static void DrawIconBars(Graphics g, Rectangle r, Color c)
        {
            using var brush = new SolidBrush(c);
            const float barH = 3f;
            float gap = (r.Height - 3 * barH) / 4f;
            for (int i = 0; i < 3; i++)
            {
                float y = r.Y + gap + i * (barH + gap);
                float w = i == 0 ? r.Width : r.Width * 0.70f;
                g.FillRectangle(brush, r.X, y, w, barH);
            }
        }

        // Box outline with lid line — represents stock/inventory
        private static void DrawIconBox(Graphics g, Rectangle r, Color c)
        {
            using var pen = new Pen(c, 2f) { LineJoin = LineJoin.Round };
            g.DrawRectangle(pen, r.X + 1, r.Y + 1, r.Width - 2, r.Height - 2);
            float lidY = r.Y + r.Height * 0.38f;
            g.DrawLine(pen, r.X + 1, lidY, r.X + r.Width - 1, lidY);
            g.DrawLine(pen, r.X + r.Width / 2f, r.Y + 1, r.X + r.Width / 2f, lidY);
        }

        // Circle head + ellipse body — represents a customer
        private static void DrawIconPerson(Graphics g, Rectangle r, Color c)
        {
            using var brush = new SolidBrush(c);
            float headD = r.Width * 0.50f;
            g.FillEllipse(brush, r.X + (r.Width - headD) / 2f, r.Y, headD, headD);
            float bw = r.Width * 0.85f;
            float bh = r.Height * 0.44f;
            g.FillEllipse(brush, r.X + (r.Width - bw) / 2f, r.Bottom - bh, bw, bh);
        }

        // Filled triangle with white exclamation — represents a warning
        private static void DrawIconWarning(Graphics g, Rectangle r, Color c)
        {
            using var brush = new SolidBrush(c);
            PointF[] tri = {
                new PointF(r.X + r.Width / 2f, r.Y),
                new PointF(r.Right, r.Bottom),
                new PointF(r.X, r.Bottom),
            };
            g.FillPolygon(brush, tri);

            using var wb = new SolidBrush(Color.White);
            float cx = r.X + r.Width / 2f - 1.5f;
            g.FillRectangle(wb, cx, r.Y + r.Height * 0.30f, 3f, r.Height * 0.33f);
            g.FillEllipse(wb, cx, r.Bottom - r.Height * 0.22f, 3f, 3f);
        }

        private void ViewTongQuan_Load(object? sender, EventArgs e)
        {
            LoadStats();
            LoadHistory();
        }

        // Hàm tự xử lý bo góc mịn màng bằng GDI+ mặc định của Windows
        private void Panels_Paint(object? sender, PaintEventArgs e)
        {
            Panel? pnl = sender as Panel;
            if (pnl == null) return;

            // Kích hoạt chế độ khử răng cưa cao cấp cho cả hình vẽ và chữ
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Tăng bán kính bo tròn lên 24 để góc mềm mại, hiện đại đúng chuẩn Figma
            int borderRadius = 24;

            // Tạo đường dẫn đồ họa bao quanh panel
            using (GraphicsPath path = new GraphicsPath())
            {
                path.StartFigure();
                path.AddArc(new Rectangle(0, 0, borderRadius, borderRadius), 180, 90);
                path.AddArc(new Rectangle(pnl.Width - borderRadius - 1, 0, borderRadius, borderRadius), 270, 90);
                path.AddArc(new Rectangle(pnl.Width - borderRadius - 1, pnl.Height - borderRadius - 1, borderRadius, borderRadius), 0, 90);
                path.AddArc(new Rectangle(0, pnl.Height - borderRadius - 1, borderRadius, borderRadius), 90, 90);
                path.CloseFigure();

                // Xóa nền cũ bị răng cưa của Windows Forms bằng cách vẽ đè màu nền cha lên trước
                using (SolidBrush backBrush = new SolidBrush(this.BackColor))
                {
                    e.Graphics.FillRectangle(backBrush, pnl.ClientRectangle);
                }

                // Đổ màu trắng tinh khiết bên trong vùng bo góc đã khử răng cưa
                using (SolidBrush cardBrush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillPath(cardBrush, path);
                }

                // Vẽ đường viền Border mỏng, màu xám Slate cực nhẹ (E2E8F0) bao quanh để tạo chiều sâu phẳng
                using (Pen borderPen = new Pen(Color.FromArgb(226, 232, 240), 1))
                {
                    e.Graphics.DrawPath(borderPen, path);
                }
            }
        }

        private void LoadStats()
        {
            using (var conn = new SqliteConnection(ConnectionString))
            {
                conn.Open();

                int mau = conn.ExecuteScalar<int>("SELECT COUNT(*) FROM SanPham");
                lblVal1.Text = mau.ToString();

                int tongHang = conn.ExecuteScalar<int>("SELECT SUM(so_luong_ton) FROM SanPham");
                lblVal2.Text = tongHang.ToString();

                int khach = conn.ExecuteScalar<int>("SELECT COUNT(*) FROM Khach");
                lblVal3.Text = khach.ToString();

                int hetHang = conn.ExecuteScalar<int>("SELECT COUNT(*) FROM SanPham WHERE so_luong_ton = 0");
                lblVal4.Text = hetHang.ToString();
            }
        }

        private void LoadHistory()
        {
            flpHistory.SuspendLayout();
            flpHistory.Controls.Clear();
            using (var conn = new SqliteConnection(ConnectionString))
            {
                conn.Open();
                string sql = @"
                    SELECT
                        k.ho_ten AS HoTen,
                        k.id AS KhachId,
                        nx.ngay_tao AS NgayTao,
                        SUM(nx.so_luong) AS TongSoLuong,
                        SUM(nx.so_luong * sp.gia_ban) AS TongTien
                    FROM NhapXuat nx
                    JOIN Khach k ON nx.KHACH_id = k.id
                    JOIN SanPham sp ON nx.SAPHAM_id = sp.id
                    LEFT JOIN HoaDon hd ON nx.hoadon_id = hd.id
                    WHERE nx.loai_giao_dich = 'Xuat'
                      AND (nx.hoadon_id IS NULL OR hd.TrangThai != 'DaChinhSua')
                    GROUP BY COALESCE(CAST(nx.hoadon_id AS TEXT), nx.KHACH_id || '_' || nx.ngay_tao)
                    ORDER BY nx.ngay_tao DESC
                    LIMIT 10";

                var invoices = conn.Query(sql).ToList();
                int cardWidth = Math.Max(flpHistory.ClientSize.Width - flpHistory.Padding.Horizontal, 300);

                foreach (var inv in invoices)
                {
                    int khachId = Convert.ToInt32(inv.KhachId);
                    int tongSoLuong = Convert.ToInt32(inv.TongSoLuong ?? 0);
                    decimal tongTien = Convert.ToDecimal(inv.TongTien ?? 0);

                    var card = new TheHoatDong();
                    card.Width = cardWidth;
                    card.LayDuLieu(
                        inv.HoTen?.ToString() ?? "",
                        inv.NgayTao?.ToString() ?? "",
                        tongSoLuong,
                        tongTien,
                        khachId);
                    card.OnSelect = () => (this.FindForm() as Form1)?.MoTrangKhachHang(khachId);
                    flpHistory.Controls.Add(card);
                }
            }
            flpHistory.ResumeLayout();
        }


        private void lblTitle4_Click(object sender, EventArgs e)
        {
        }

        private void lblHistoryTitle_Click(object sender, EventArgs e)
        {

        }
    }
}