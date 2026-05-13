using Dapper;
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

        private void ViewKhachHang_Load(object sender, EventArgs e)
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

            lblStatValue1.Text = "0";
            lblStatValue2.Text = "0đ";
            lblStatValue3.Text = "1.0";

            LoadLichSuMuaHang(kh.id);
        }

        private void LoadLichSuMuaHang(int khachHangId)
        {
            dgvHistory.DataSource = null;
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadDanhSachKhachHang(txtSearch.Text);
        }
    }
}