using Dapper;
using Microsoft.Data.Sqlite;

namespace dosi
{
    public class KhuyenMai
    {
        private static string ConnectionString = "Data Source=QuanLyKho.db";

        public int id { get; set; }
        public string MaKM { get; set; } = string.Empty;
        public string TenKM { get; set; } = string.Empty;
        public int? SoLuongTon { get; set; }
        public string? NgayBatDau { get; set; }
        public string? NgayKetThuc { get; set; }
        // 0: Không có, 1: Tổng tiền min, 2: SL SP min, 3: Khách lâu năm (tháng)
        public int LoaiDieuKien { get; set; } = 0;
        public decimal? GiaTriDieuKien { get; set; }
        // 1: Giảm tiền mặt, 2: Giảm %
        public int LoaiGiamGia { get; set; } = 1;
        public decimal GiaTriGiam { get; set; }
        public decimal? GiamToiDa { get; set; }
        public int TrangThai { get; set; } = 1;

        public override string ToString() => $"{MaKM}  –  {TenKM}";

        public bool ConHieuLuc()
        {
            if (TrangThai != 1) return false;
            if (SoLuongTon.HasValue && SoLuongTon.Value <= 0) return false;

            var now = DateTime.Now;
            if (!string.IsNullOrEmpty(NgayBatDau) && DateTime.TryParse(NgayBatDau, out var batDau) && now < batDau)
                return false;
            if (!string.IsNullOrEmpty(NgayKetThuc) && DateTime.TryParse(NgayKetThuc, out var ketThuc) && now > ketThuc)
                return false;

            return true;
        }

        // tongTienGoc: original cart total before this voucher
        // tongSoLuong: total items in cart
        // ngayTaoKhach: ISO date string of customer creation date (nullable)
        public decimal TinhTienGiam(decimal tongTienGoc, int tongSoLuong, string? ngayTaoKhach)
        {
            if (LoaiDieuKien == 1 && (!GiaTriDieuKien.HasValue || tongTienGoc < GiaTriDieuKien.Value))
                return 0;

            if (LoaiDieuKien == 2 && (!GiaTriDieuKien.HasValue || tongSoLuong < (int)GiaTriDieuKien.Value))
                return 0;

            if (LoaiDieuKien == 3)
            {
                if (!GiaTriDieuKien.HasValue || string.IsNullOrEmpty(ngayTaoKhach)) return 0;
                if (!DateTime.TryParse(ngayTaoKhach, out var ngayTao)) return 0;
                int soThang = (DateTime.Now.Year - ngayTao.Year) * 12 + DateTime.Now.Month - ngayTao.Month;
                if (soThang < (int)GiaTriDieuKien.Value) return 0;
            }

            decimal giam;
            if (LoaiGiamGia == 1)
            {
                giam = GiaTriGiam;
            }
            else
            {
                giam = Math.Round(tongTienGoc * GiaTriGiam / 100m, 0);
                if (GiamToiDa.HasValue && giam > GiamToiDa.Value)
                    giam = GiamToiDa.Value;
            }

            return Math.Min(giam, tongTienGoc);
        }

        public void LuuVaoDatabase()
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            conn.Execute(@"INSERT INTO KhuyenMai
                (ma_km, ten_km, so_luong_ton, ngay_bat_dau, ngay_ket_thuc,
                 loai_dieu_kien, gia_tri_dieu_kien, loai_giam_gia, gia_tri_giam, giam_toi_da, trang_thai)
                VALUES
                (@MaKM, @TenKM, @SoLuongTon, @NgayBatDau, @NgayKetThuc,
                 @LoaiDieuKien, @GiaTriDieuKien, @LoaiGiamGia, @GiaTriGiam, @GiamToiDa, @TrangThai)", this);
        }

        public void CapNhatDatabase()
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            conn.Execute(@"UPDATE KhuyenMai SET
                ma_km=@MaKM, ten_km=@TenKM, so_luong_ton=@SoLuongTon,
                ngay_bat_dau=@NgayBatDau, ngay_ket_thuc=@NgayKetThuc,
                loai_dieu_kien=@LoaiDieuKien, gia_tri_dieu_kien=@GiaTriDieuKien,
                loai_giam_gia=@LoaiGiamGia, gia_tri_giam=@GiaTriGiam,
                giam_toi_da=@GiamToiDa, trang_thai=@TrangThai
                WHERE id=@id", this);
        }

        public void XoaKhoiDatabase()
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            conn.Execute("DELETE FROM KhuyenMai WHERE id=@id", new { id });
        }

        public static List<KhuyenMai> LayDanhSach(bool chiHieuLuc = false)
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            var list = conn.Query<KhuyenMai>(@"SELECT
                id, ma_km AS MaKM, ten_km AS TenKM, so_luong_ton AS SoLuongTon,
                ngay_bat_dau AS NgayBatDau, ngay_ket_thuc AS NgayKetThuc,
                loai_dieu_kien AS LoaiDieuKien, gia_tri_dieu_kien AS GiaTriDieuKien,
                loai_giam_gia AS LoaiGiamGia, gia_tri_giam AS GiaTriGiam,
                giam_toi_da AS GiamToiDa, trang_thai AS TrangThai
                FROM KhuyenMai ORDER BY id").ToList();
            return chiHieuLuc ? list.Where(k => k.ConHieuLuc()).ToList() : list;
        }

        public static string SinhMaKM()
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            int count = conn.ExecuteScalar<int>("SELECT COUNT(*) FROM KhuyenMai");
            return "KM" + (count + 1).ToString("D3");
        }
    }

    public class HoaDonKhuyenMai
    {
        public int id { get; set; }
        public long HoaDonId { get; set; }
        public int KhuyenMaiId { get; set; }
        public decimal SoTienGiam { get; set; }
    }
}
