using System.Collections.Generic;

namespace dosi
{
    public class EditOrderContext
    {
        public string MaHoaDonGoc { get; set; } = "";
        public string TenKhachHang { get; set; } = "";
        public string SoDienThoai { get; set; } = "";
        public string DiaChi { get; set; } = "";
        public List<EditOrderItem> Items { get; set; } = new();
    }

    public class EditOrderItem
    {
        public string MaSP { get; set; } = "";
        public string TenSP { get; set; } = "";
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
    }
}
