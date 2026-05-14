using Dapper;
using System.Data;
using System.Data.SQLite;

namespace dosi
{
    public partial class ViewKhachHang : UserControl
    {
        private string ConnectionString = "Data Source=QuanLyKho.db";

        public ViewKhachHang()
        {
            InitializeComponent();
            this.Load += ViewKhachHang_Load;
            txtSearch.TextChanged += TxtSearch_TextChanged;
        }

        private void ViewKhachHang_Load(object? sender, EventArgs e)
        {
            LoadDanhSachKhachHang();
        }

        private void LoadDanhSachKhachHang(string search = "")
        {
            try
            {
                listKhachHang.SuspendLayout();
                listKhachHang.Controls.Clear();

                using (var conn = new SQLiteConnection(ConnectionString))
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
                        card.Margin = new Padding(0, 0, 0, 5);

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
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void HienThiChiTiet(KhachHang kh)
        {
            lblHoTen.Text = kh.HoTen;
            lblPhone.Text = kh.SoDienThoai;
            lblAddress.Text = kh.DiaChi;

            CapNhatThongKe(kh.id);
            LoadLichSuMuaHang(kh.id);
        }

        private void CapNhatThongKe(int khachId)
        {
            try
            {
                using (var conn = new SQLiteConnection(ConnectionString))
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
                        lblStatValue1.Text = (stats.DonHang ?? 0).ToString();

                        decimal tongChi = stats.TongChi != null ? Convert.ToDecimal(stats.TongChi) : 0;
                        lblStatValue2.Text = tongChi.ToString("N0") + "đ";

                        if (stats.NgayDau != null)
                        {
                            DateTime ngayDau = DateTime.Parse(stats.NgayDau.ToString());
                            double nam = (DateTime.Now - ngayDau).TotalDays / 365.0;
                            lblStatValue3.Text = nam.ToString("F1");
                        }
                        else
                        {
                            lblStatValue3.Text = "0.0";
                        }
                    }
                }
            }
            catch
            {
                lblStatValue1.Text = "0";
                lblStatValue2.Text = "0đ";
                lblStatValue3.Text = "0.0";
            }
        }

        private void LoadLichSuMuaHang(int khachHangId)
        {
            try
            {
                using (var conn = new SQLiteConnection(ConnectionString))
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
                MessageBox.Show("Lỗi lịch sử: " + ex.Message);
            }
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            LoadDanhSachKhachHang(txtSearch.Text);
        }

        private void lblStatValue1_Click(object sender, EventArgs e)
        {

        }

        private void lblPhone_Click(object sender, EventArgs e)
        {

        }
    }
}