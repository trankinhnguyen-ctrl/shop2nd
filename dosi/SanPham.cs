using Dapper;
using Microsoft.Data.Sqlite;


namespace dosi
{
    public class PhanLoaiItem
    {
        public int Id { get; set; }
        public string Ten { get; set; } = "";
        public string Ma { get; set; } = "";
        public override string ToString() => Ten;
    }

    public class SanPham
    {
        private static string ConnectionString = "Data Source=QuanLyKho.db";

        public int id { get; set; }
        public string MaSP { get; set; } = string.Empty;
        public string TenSP { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public decimal GiaBan { get; set; }
        public decimal GiaVon { get; set; }
        public string HinhAnh { get; set; } = string.Empty;
        public int PhanLoaiId { get; set; }

        public void LuuVaoDatabase()
        {
            using (var conn = new SqliteConnection(ConnectionString))
            {
                string sql = "INSERT INTO SanPham (ma_sp, ten_sp, so_luong_ton, gia_ban, gia_von, hinh_anh, phanloai_id) VALUES (@MaSP, @TenSP, @SoLuong, @GiaBan, @GiaVon, @HinhAnh, @PhanLoaiId)";
                conn.Open();
                conn.Execute(sql, this);
            }
        }

        public void CapNhatDatabase()
        {
            using (var conn = new SqliteConnection(ConnectionString))
            {
                string sql = "UPDATE SanPham SET ma_sp = @MaSP, ten_sp = @TenSP, so_luong_ton = @SoLuong, gia_ban = @GiaBan, gia_von = @GiaVon, hinh_anh = @HinhAnh, phanloai_id = @PhanLoaiId WHERE id = @id";
                conn.Open();
                conn.Execute(sql, this);
            }
        }

        public void XoaKhoiDatabase()
        {
            using (var conn = new SqliteConnection(ConnectionString))
            {
                string sql = "DELETE FROM SanPham WHERE id = @id";
                conn.Open();
                conn.Execute(sql, new { id = this.id });
            }
        }
    }
}