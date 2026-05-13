using Dapper;
using System.Data.SQLite;

namespace dosi
{
    public class SanPham
    {
        private static string ConnectionString = "Data Source=QuanLyKho.db";

        public int id { get; set; }
        public string MaSP { get; set; } = string.Empty;
        public string TenSP { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public string HinhAnh { get; set; } = string.Empty;

        public void LuuVaoDatabase()
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                string sql = "INSERT INTO SanPham (ma_sp, ten_sp, so_luong_ton, hinh_anh) VALUES (@MaSP, @TenSP, @SoLuong, @HinhAnh)";
                conn.Open();
                conn.Execute(sql, this);
            }
        }

        public void CapNhatDatabase()
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                string sql = "UPDATE SanPham SET ma_sp = @MaSP, ten_sp = @TenSP, so_luong_ton = @SoLuong, hinh_anh = @HinhAnh WHERE id = @id";
                conn.Open();
                conn.Execute(sql, this);
            }
        }

        public void XoaKhoiDatabase()
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                string sql = "DELETE FROM SanPham WHERE id = @id";
                conn.Open();
                conn.Execute(sql, new { id = this.id });
            }
        }
    }
}