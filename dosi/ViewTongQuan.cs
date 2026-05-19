using Dapper;
using System.Data.SQLite;

namespace dosi
{
    public partial class ViewTongQuan : UserControl
    {
        private string ConnectionString = "Data Source=QuanLyKho.db";

        public ViewTongQuan()
        {
            InitializeComponent();
            this.Load += ViewTongQuan_Load;
        }

        private void ViewTongQuan_Load(object? sender, EventArgs e)
        {
            LoadStats();
            LoadHistory();
        }

        private void LoadStats()
        {
            using (var conn = new SQLiteConnection(ConnectionString))
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
            using (var conn = new SQLiteConnection(ConnectionString))
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
                    lbl.Size = new Size(flpHistory.Width - 30, 40);
                    lbl.Text = "• " + act.ho_ten + " đã mua " + act.ten_sp + " (" + act.ngay_tao + ")";
                    lbl.Font = new Font("Segoe UI", 9F);
                    flpHistory.Controls.Add(lbl);
                }
            }
        }

        private void lblTitle4_Click(object sender, EventArgs e)
        {

        }
    }
}