using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using System.Data.SQLite;

namespace dosi
{
    public class KhachHang
    {
        private static string ConnectionString = "Data Source=QuanLyKho.db";

        public int id { get; set; }
        public string HoTen { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public string GhiChu { get; set; } = string.Empty;

        public void LuuVaoDatabase()
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                string sql = "INSERT INTO Khach (ho_ten, so_dien_thoai, dia_chi, ghi_chu) VALUES (@HoTen, @SoDienThoai, @DiaChi, @GhiChu)";
                conn.Open();
                conn.Execute(sql, this);
            }
        }

        public void CapNhatDatabase()
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                string sql = "UPDATE Khach SET ho_ten = @HoTen, so_dien_thoai = @SoDienThoai, dia_chi = @DiaChi, ghi_chu = @GhiChu WHERE id = @id";
                conn.Open();
                conn.Execute(sql, this);
            }
        }

        public void XoaKhoiDatabase()
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                string sql = "DELETE FROM Khach WHERE id = @id";
                conn.Open();
                conn.Execute(sql, new { id = this.id });
            }
        }
    }
}