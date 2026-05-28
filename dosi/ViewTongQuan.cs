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

            Panel[] cards = { card1, card2, card3, card4, panelHistory };
            foreach (var p in cards)
            {
                p.Paint += Panels_Paint;
                p.SizeChanged += (s, e) => (s as Panel)?.Invalidate();
            }
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
            flpHistory.Controls.Clear();
            using (var conn = new SqliteConnection(ConnectionString))
            {
                conn.Open();
                string sql = @"
                    SELECT k.ho_ten, nx.ngay_tao, s.ten_sp 
                    FROM NhapXuat nx 
                    JOIN Khach k ON nx.KHACH_id = k.id 
                    JOIN SanPham s ON nx.SAPHAM_id = s.id 
                    WHERE nx.loai_giao_dich = 'Xuat' 
                    ORDER BY nx.ngay_tao DESC LIMIT 10";

                var activities = conn.Query(sql).ToList();

                foreach (var act in activities)
                {
                    Label lbl = new Label();
                    lbl.AutoSize = false;
                    lbl.Size = new Size(flpHistory.Width - 40, 35);
                    lbl.Text = "  •  " + act.ho_ten + " đã mua " + act.ten_sp + " (" + act.ngay_tao + ")";
                    lbl.Font = new Font("Segoe UI", 10F);
                    lbl.ForeColor = Color.FromArgb(71, 85, 105);
                    lbl.TextAlign = ContentAlignment.MiddleLeft;

                    flpHistory.Controls.Add(lbl);
                }
            }
        }

        private void lblTitle4_Click(object sender, EventArgs e)
        {
        }

        private void lblHistoryTitle_Click(object sender, EventArgs e)
        {

        }
    }
}