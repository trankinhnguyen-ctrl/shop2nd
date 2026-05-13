using System;
using System.Data.SQLite;
using System.IO;

namespace dosi
{
    internal class DatabaseService
    {
        private string dbPath;
        private string connectionString;

        public DatabaseService()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            dbPath = Path.Combine(baseDirectory, "QuanLyKho.db");
            connectionString = $"Data Source={dbPath};Version=3;";
        }

        public void InitializeDatabase()
        {
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
            }

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "DROP TABLE IF EXISTS NhapXuat; DROP TABLE IF EXISTS SanPham; DROP TABLE IF EXISTS Khach;";
                    command.ExecuteNonQuery();

                    string createSanPhamTable = @"
                        CREATE TABLE SanPham (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            ma_sp TEXT UNIQUE,
                            ten_sp TEXT,
                            mo_ta TEXT,
                            so_luong_ton INTEGER,
                            gia_von INTEGER,
                            gia_ban INTEGER,
                            hinh_anh TEXT
                        );";

                    string createKhachTable = @"
                        CREATE TABLE Khach (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            ho_ten TEXT,
                            so_dien_thoai TEXT,
                            dia_chi TEXT,
                            ghi_chu TEXT
                        );";

                    string createNhapXuatTable = @"
                        CREATE TABLE NhapXuat (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            SAPHAM_id INTEGER,
                            KHACH_id INTEGER,
                            loai_giao_dich TEXT,
                            so_luong INTEGER,
                            ngay_tao DATETIME,
                            ghi_chu TEXT,
                            FOREIGN KEY (SAPHAM_id) REFERENCES SanPham(id),
                            FOREIGN KEY (KHACH_id) REFERENCES Khach(id)
                        );";

                    command.CommandText = createSanPhamTable;
                    command.ExecuteNonQuery();

                    command.CommandText = createKhachTable;
                    command.ExecuteNonQuery();

                    command.CommandText = createNhapXuatTable;
                    command.ExecuteNonQuery();
                }
            }
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}