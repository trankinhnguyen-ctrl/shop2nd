using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace dosi
{
    public partial class ViewKhachHang : UserControl
    {
        private string ConnectionString = "Data Source=QuanLyKho.db";
        private KhachHang? _khachHangHienTai;

        public ViewKhachHang()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            this.Load += ViewKhachHang_Load;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            panelHeader.Paint += KhốiGiaoDien_Paint;
            panelStat1.Paint += KhốiGiaoDien_Paint;
            panelStat2.Paint += KhốiGiaoDien_Paint;
            panelStat3.Paint += KhốiGiaoDien_Paint;
            panelStat4.Paint += KhốiGiaoDien_Paint;

            picAvatar.Paint += PicAvatar_Paint;

            picChinhSua.Click += (s, e) =>
            {
                if (_khachHangHienTai == null) return;
                using var form = new SuaKhachHangForm(_khachHangHienTai);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadDanhSachKhachHang(txtSearch.Text);
                    HienThiChiTiet(_khachHangHienTai);
                }
            };
        }

        private void ViewKhachHang_Load(object? sender, EventArgs e)
        {
            LoadDanhSachKhachHang();
        }

        private void KhốiGiaoDien_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is Panel panel)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int radius = 12;

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.StartFigure();
                    path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
                    path.AddArc(new Rectangle(panel.Width - radius - 1, 0, radius, radius), 270, 90);
                    path.AddArc(new Rectangle(panel.Width - radius - 1, panel.Height - radius - 1, radius, radius), 0, 90);
                    path.AddArc(new Rectangle(0, panel.Height - radius - 1, radius, radius), 90, 90);
                    path.CloseFigure();

                    panel.Region = new Region(path);

                    using (Pen borderPen = new Pen(Color.FromArgb(226, 232, 240), 1f))
                    {
                        e.Graphics.DrawPath(borderPen, path);
                    }
                }
            }
        }

        private void PicAvatar_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, picAvatar.Width - 1, picAvatar.Height - 1);
                picAvatar.Region = new Region(path);

                using (Pen circlePen = new Pen(Color.FromArgb(226, 232, 240), 1.5f))
                {
                    e.Graphics.DrawPath(circlePen, path);
                }
            }
        }

        private void LoadDanhSachKhachHang(string search = "")
        {
            try
            {
                listKhachHang.SuspendLayout();
                listKhachHang.Controls.Clear();

                using (var conn = new SqliteConnection(ConnectionString))
                {
                    conn.Open();
                    string sql = "SELECT id, ho_ten AS HoTen, so_dien_thoai AS SoDienThoai, dia_chi AS DiaChi, ghi_chu AS GhiChu FROM Khach";

                    if (!string.IsNullOrEmpty(search))
                    {
                        sql += " WHERE ho_ten LIKE @key OR so_dien_thoai LIKE @key";
                    }

                    var danhSach = conn.Query<KhachHang>(sql, new { key = "%" + search + "%" }).ToList();

                    foreach (var kh in danhSach)
                    {
                        TheKhachHang card = new TheKhachHang();
                        card.LayDuLieu(kh);

                        card.Width = listKhachHang.Width > 50 ? listKhachHang.Width - 25 : 320;
                        card.Margin = new Padding(0, 0, 0, 8);

                        card.Click += (s, e) => HienThiChiTiet(kh);
                        foreach (Control child in card.Controls)
                        {
                            child.Click += (s, e) => HienThiChiTiet(kh);
                        }

                        listKhachHang.Controls.Add(card);
                    }
                }
                listKhachHang.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách khách hàng: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienThiChiTiet(KhachHang kh)
        {
            _khachHangHienTai = kh;
            lblHoTen.Text = kh.HoTen;
            lblPhone.Text = kh.SoDienThoai;
            lblAddress.Text = string.IsNullOrEmpty(kh.DiaChi) ? "Chưa cập nhật địa chỉ" : kh.DiaChi;
            lblGhiChu.Text = string.IsNullOrEmpty(kh.GhiChu) ? "" : " " + kh.GhiChu;

            CapNhatThongKe(kh.id);
            LoadHoaDonKhachHang(kh.id);
        }

        private void CapNhatThongKe(int khachId)
        {
            try
            {
                using (var conn = new SqliteConnection(ConnectionString))
                {
                    conn.Open();

                    int donHangCoHD = conn.ExecuteScalar<int>(
                        "SELECT COUNT(DISTINCT hoadon_id) FROM NhapXuat WHERE KHACH_id = @id AND loai_giao_dich = 'Xuat' AND hoadon_id IS NOT NULL",
                        new { id = khachId });

                    int donHangCu = conn.ExecuteScalar<int>(
                        "SELECT COUNT(DISTINCT ngay_tao) FROM NhapXuat WHERE KHACH_id = @id AND loai_giao_dich = 'Xuat' AND hoadon_id IS NULL",
                        new { id = khachId });

                    lblStatValue1.Text = (donHangCoHD + donHangCu).ToString("N0");

                    string sql = @"
                        SELECT
                            COALESCE(SUM(nx.so_luong), 0) AS SoHang,
                            COALESCE(SUM(nx.so_luong * sp.gia_ban), 0) AS TongChi,
                            MIN(nx.ngay_tao) AS NgayDau
                        FROM NhapXuat nx
                        JOIN SanPham sp ON nx.SAPHAM_id = sp.id
                        WHERE nx.KHACH_id = @id AND nx.loai_giao_dich = 'Xuat'";

                    var stats = conn.QueryFirstOrDefault(sql, new { id = khachId });

                    if (stats != null)
                    {
                        lblStatValue2.Text = (stats.SoHang ?? 0).ToString("N0");

                        decimal tongChi = stats.TongChi != null ? Convert.ToDecimal(stats.TongChi) : 0;
                        lblStatValue3.Text = tongChi.ToString("N0") + "đ";

                        if (stats.NgayDau != null)
                        {
                            DateTime ngayDau = DateTime.Parse(stats.NgayDau.ToString());
                            double nam = (DateTime.Now - ngayDau).TotalDays / 365.0;
                            lblStatValue4.Text = nam.ToString("F1") + " năm";
                        }
                        else
                        {
                            lblStatValue4.Text = "0.0 năm";
                        }
                    }
                }
            }
            catch
            {
                lblStatValue1.Text = "0";
                lblStatValue2.Text = "0";
                lblStatValue3.Text = "0đ";
                lblStatValue4.Text = "0.0 năm";
            }
        }

        private void LoadHoaDonKhachHang(int khachHangId)
        {
            flpInvoices.SuspendLayout();
            flpInvoices.Controls.Clear();

            try
            {
                using (var conn = new SqliteConnection(ConnectionString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT
                            nx.hoadon_id,
                            nx.ngay_tao,
                            sp.ten_sp AS TenSP,
                            nx.so_luong AS SoLuong,
                            sp.gia_ban AS GiaBan,
                            (nx.so_luong * sp.gia_ban) AS ThanhTien
                        FROM NhapXuat nx
                        JOIN SanPham sp ON nx.SAPHAM_id = sp.id
                        WHERE nx.KHACH_id = @id AND nx.loai_giao_dich = 'Xuat'
                        ORDER BY nx.ngay_tao DESC";

                    var rows = conn.Query(sql, new { id = khachHangId }).ToList();

                    var groups = rows
                        .GroupBy(r => r.hoadon_id != null ? $"hd_{r.hoadon_id}" : $"dt_{r.ngay_tao}")
                        .OrderByDescending(g => g.First().ngay_tao?.ToString() ?? "")
                        .ToList();

                    int cardWidth = Math.Max(flpInvoices.ClientSize.Width - 16, 300);

                    foreach (var group in groups)
                    {
                        var items = group.ToList();
                        string ngayTao = items[0].ngay_tao?.ToString() ?? "";

                        int soMon = 0;
                        decimal tongHoaDon = 0;
                        foreach (dynamic item in items)
                        {
                            soMon += Convert.ToInt32((object)(item.SoLuong ?? 0));
                            tongHoaDon += Convert.ToDecimal((object)(item.ThanhTien ?? 0));
                        }

                        var card = new TheHoaDon();
                        card.Width = cardWidth;
                        card.LayDuLieu(ngayTao, soMon, tongHoaDon, items);
                        flpInvoices.Controls.Add(card);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử mua hàng: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            flpInvoices.ResumeLayout();
        }

        public void ChonKhachHangTheoId(int id)
        {
            foreach (Control ctrl in listKhachHang.Controls)
            {
                if (ctrl is TheKhachHang card && card.Data?.id == id)
                {
                    HienThiChiTiet(card.Data);
                    listKhachHang.ScrollControlIntoView(card);
                    break;
                }
            }
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            LoadDanhSachKhachHang(txtSearch.Text);
        }

        private void mainSplit_SplitterMoved(object sender, SplitterEventArgs e)
        {
            foreach (Control ctrl in listKhachHang.Controls)
            {
                if (ctrl is UserControl card)
                    card.Width = listKhachHang.Width > 50 ? listKhachHang.Width - 25 : 320;
            }

            int invoiceWidth = Math.Max(flpInvoices.ClientSize.Width - 16, 300);
            foreach (Control ctrl in flpInvoices.Controls)
            {
                if (ctrl is TheHoaDon hoaDon)
                    hoaDon.Width = invoiceWidth;
            }
        }

        private void panelHeader_Click(object sender, EventArgs e)
        {

        }
    }
}
