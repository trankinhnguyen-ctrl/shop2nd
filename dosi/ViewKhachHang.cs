using Dapper;
using Microsoft.Data.Sqlite;
using System;
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

            // Kích hoạt Double-Buffered chống nhấp nháy màn hình khi render giao diện phức tạp
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            this.Load += ViewKhachHang_Load;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            // Đăng ký sự kiện vẽ bo góc mịn cho các khối giao diện
            panelHeader.Paint += KhốiGiaoDien_Paint;
            panelStat1.Paint += KhốiGiaoDien_Paint;
            panelStat2.Paint += KhốiGiaoDien_Paint;
            panelStat3.Paint += KhốiGiaoDien_Paint;

            // Đăng ký sự kiện cắt picAvatar thành hình tròn
            picAvatar.Paint += PicAvatar_Paint;

            // Định hình lại phong cách phẳng (Flat Design) cho DataGridView lịch sử mua hàng
            StyleDataGridView();

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

        // 1. Tự động vẽ bo góc 12px mượt mà cho các bảng khối thông tin và thẻ thống kê
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

                    // Cắt vùng hiển thị của Panel theo khung bo góc
                    panel.Region = new Region(path);

                    // Vẽ viền mảnh tinh tế bao quanh các khối nền trắng
                    using (Pen borderPen = new Pen(Color.FromArgb(226, 232, 240), 1f))
                    {
                        e.Graphics.DrawPath(borderPen, path);
                    }
                }
            }
        }

        // 2. Cắt bo ảnh đại diện thành vòng tròn (Circle Avatar) chuẩn UI/UX hiện đại
        private void PicAvatar_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, picAvatar.Width - 1, picAvatar.Height - 1);
                picAvatar.Region = new Region(path);

                // Thêm một vòng viền mảnh bọc ngoài ảnh đại diện tạo chiều sâu
                using (Pen circlePen = new Pen(Color.FromArgb(226, 232, 240), 1.5f))
                {
                    e.Graphics.DrawPath(circlePen, path);
                }
            }
        }

        // 3. Làm phẳng bảng dữ liệu DataGridView theo chuẩn thiết kế Figma thanh lịch
        private void StyleDataGridView()
        {
            dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252); // Màu dòng xen kẽ dịu nhẹ
            dgvHistory.BackgroundColor = Color.White;
            dgvHistory.GridColor = Color.FromArgb(241, 245, 249);
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.MultiSelect = false;
            dgvHistory.AllowUserToAddRows = false; // Tắt hàng trống dư thừa dưới cùng
            dgvHistory.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // Tắt đường kẻ dọc ẩn bớt chi tiết thừa
            dgvHistory.EnableHeadersVisualStyles = false;

            // Cấu hình thanh tiêu đề cột phẳng
            dgvHistory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(100, 116, 139); // Chữ xám Slate tinh tế
            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            dgvHistory.ColumnHeadersHeight = 38;

            // Cấu hình các dòng dữ liệu hiển thị thông thoáng
            dgvHistory.RowTemplate.Height = 35;
            dgvHistory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(238, 242, 255); // Màu xanh Indigo nhạt khi chọn dòng
            dgvHistory.DefaultCellStyle.SelectionForeColor = Color.FromArgb(79, 70, 229);
            dgvHistory.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvHistory.DefaultCellStyle.ForeColor = Color.FromArgb(51, 65, 85);
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

                        // Tự động kéo dãn độ rộng Card vừa vặn theo thanh cuộn
                        card.Width = listKhachHang.Width > 50 ? listKhachHang.Width - 25 : 320;
                        card.Margin = new Padding(0, 0, 0, 8); // Khoảng cách giãn giữa các thẻ gọn gàng hơn

                        // Đăng ký sự kiện click chọn thẻ đồng bộ
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
            lblGhiChu.Text = string.IsNullOrEmpty(kh.GhiChu) ? "" : "📝 " + kh.GhiChu;

            CapNhatThongKe(kh.id);
            LoadLichSuMuaHang(kh.id);
        }

        private void CapNhatThongKe(int khachId)
        {
            try
            {
                using (var conn = new SqliteConnection(ConnectionString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            COUNT(*) as DonHang, 
                            SUM(nx.so_luong * sp.gia_ban) as TongChi,
                            MIN(nx.ngay_tao) as NgayDau
                        FROM NhapXuat nx
                        JOIN SanPham sp ON nx.SAPHAM_id = sp.id
                        WHERE nx.KHACH_id = @id AND nx.loai_giao_dich = 'Xuat'";

                    var stats = conn.QueryFirstOrDefault(sql, new { id = khachId });

                    if (stats != null)
                    {
                        lblStatValue1.Text = (stats.DonHang ?? 0).ToString("N0");

                        decimal tongChi = stats.TongChi != null ? Convert.ToDecimal(stats.TongChi) : 0;
                        lblStatValue2.Text = tongChi.ToString("N0") + "đ";

                        if (stats.NgayDau != null)
                        {
                            DateTime ngayDau = DateTime.Parse(stats.NgayDau.ToString());
                            double nam = (DateTime.Now - ngayDau).TotalDays / 365.0;

                            // Định dạng hiển thị thâm niên ví dụ: "1.5 năm" hoặc "0.2 năm"
                            lblStatValue3.Text = nam.ToString("F1") + " năm";
                        }
                        else
                        {
                            lblStatValue3.Text = "0.0 năm";
                        }
                    }
                }
            }
            catch
            {
                lblStatValue1.Text = "0";
                lblStatValue2.Text = "0đ";
                lblStatValue3.Text = "0.0 năm";
            }
        }

        private void LoadLichSuMuaHang(int khachHangId)
        {
            try
            {
                using (var conn = new SqliteConnection(ConnectionString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            nx.ngay_tao AS [Ngày mua], 
                            sp.ten_sp AS [Tên sản phẩm], 
                            nx.so_luong AS [SL], 
                            sp.gia_ban AS [Đơn giá], 
                            (nx.so_luong * sp.gia_ban) AS [Thành tiền]
                        FROM NhapXuat nx
                        JOIN SanPham sp ON nx.SAPHAM_id = sp.id
                        WHERE nx.KHACH_id = @id AND nx.loai_giao_dich = 'Xuat'
                        ORDER BY nx.ngay_tao DESC";

                    var data = conn.ExecuteReader(sql, new { id = khachHangId });
                    DataTable dt = new DataTable();
                    dt.Load(data);
                    dgvHistory.DataSource = dt;

                    if (dgvHistory.Columns.Count > 0)
                    {
                        dgvHistory.Columns["Đơn giá"].DefaultCellStyle.Format = "N0";
                        dgvHistory.Columns["Thành tiền"].DefaultCellStyle.Format = "N0";
                        dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử mua hàng: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            LoadDanhSachKhachHang(txtSearch.Text);
        }

        private void mainSplit_SplitterMoved(object sender, SplitterEventArgs e)
        {
            // Tự động tính toán lại chiều rộng của toàn bộ thẻ trong danh sách khi thanh chia Layout dịch chuyển
            foreach (Control ctrl in listKhachHang.Controls)
            {
                if (ctrl is UserControl card)
                {
                    card.Width = listKhachHang.Width > 50 ? listKhachHang.Width - 25 : 320;
                }
            }
        }

        private void panelHeader_Click(object sender, EventArgs e)
        {

        }
    }
}