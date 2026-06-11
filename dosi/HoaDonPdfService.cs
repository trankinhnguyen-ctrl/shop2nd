using Dapper;
using Microsoft.Data.Sqlite;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace dosi
{
    public record HoaDonPdfItem(string TenSP, int SoLuong, decimal GiaBan, decimal ThanhTien);

    public record HoaDonPdfData(
        string MaHoaDon,
        string NgayTao,
        string TenKhach,
        string SoDienThoai,
        string DiaChi,
        List<HoaDonPdfItem> Items,
        decimal TongTien,
        decimal TienGiam = 0
    );

    public static class HoaDonPdfService
    {
        private static readonly string ConnectionString = "Data Source=QuanLyKho.db";

        public static void InHoaDonTheoMa(string maHoaDon)
        {
            try
            {
                using var conn = new SqliteConnection(ConnectionString);
                conn.Open();

                var header = conn.QueryFirstOrDefault(@"
                    SELECT hd.ma_hd, hd.ngay_tao, hd.tong_tien,
                           COALESCE(hd.tong_tien_giam, 0) AS TienGiam,
                           k.ho_ten AS TenKhach, k.so_dien_thoai AS SDT,
                           COALESCE(k.dia_chi, '') AS DiaChi
                    FROM HoaDon hd
                    JOIN Khach k ON hd.KHACH_id = k.id
                    WHERE hd.ma_hd = @ma", new { ma = maHoaDon });

                if (header == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var rows = conn.Query(@"
                    SELECT sp.ten_sp AS TenSP, nx.so_luong AS SoLuong, sp.gia_ban AS GiaBan,
                           (nx.so_luong * sp.gia_ban) AS ThanhTien
                    FROM NhapXuat nx
                    JOIN SanPham sp ON nx.SAPHAM_id = sp.id
                    JOIN HoaDon hd ON nx.hoadon_id = hd.id
                    WHERE hd.ma_hd = @ma AND nx.loai_giao_dich = 'Xuat'",
                    new { ma = maHoaDon }).ToList();

                var data = new HoaDonPdfData(
                    MaHoaDon: header.ma_hd?.ToString() ?? maHoaDon,
                    NgayTao: header.ngay_tao?.ToString() ?? "",
                    TenKhach: header.TenKhach?.ToString() ?? "",
                    SoDienThoai: header.SDT?.ToString() ?? "",
                    DiaChi: header.DiaChi?.ToString() ?? "",
                    Items: rows.Select(r => new HoaDonPdfItem(
                        TenSP: r.TenSP?.ToString() ?? "",
                        SoLuong: Convert.ToInt32((object)r.SoLuong),
                        GiaBan: Convert.ToDecimal((object)r.GiaBan),
                        ThanhTien: Convert.ToDecimal((object)r.ThanhTien)
                    )).ToList(),
                    TongTien: Convert.ToDecimal((object)header.tong_tien),
                    TienGiam: Convert.ToDecimal((object)header.TienGiam)
                );

                InHoaDon(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void InHoaDon(HoaDonPdfData data)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            string tempPath = Path.Combine(Path.GetTempPath(), $"HoaDon_{data.MaHoaDon}.pdf");

            try
            {
                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A5);
                        page.Margin(1.5f, Unit.Centimetre);
                        page.DefaultTextStyle(s => s.FontFamily("Segoe UI").FontSize(10));

                        page.Content().Column(col =>
                        {
                            // ── Tiêu đề ──────────────────────────────────────
                            col.Item().AlignCenter().Text("HÓA ĐƠN BÁN HÀNG")
                                .FontSize(18).Bold().FontColor("#4F46E5");

                            col.Item().AlignCenter().Text("2Hand Store")
                                .FontSize(11).FontColor("#64748B");

                            col.Item().PaddingVertical(8).Height(1).Background("#E2E8F0");

                            // ── Thông tin hóa đơn ─────────────────────────────
                            col.Item().PaddingVertical(6).Row(row =>
                            {
                                row.RelativeItem().Text($"Mã HĐ: {data.MaHoaDon}").Bold();
                                row.RelativeItem().AlignRight().Text(FormatDate(data.NgayTao))
                                    .FontColor("#64748B");
                            });

                            // ── Thông tin khách hàng ──────────────────────────
                            col.Item().Background("#F8FAFC").Border(1).BorderColor("#E2E8F0")
                                .Padding(10).Column(inner =>
                                {
                                    inner.Item().Text($"Khách hàng:  {data.TenKhach}").Bold();
                                    inner.Item().Text($"SĐT:  {data.SoDienThoai}").FontColor("#475569");
                                    if (!string.IsNullOrWhiteSpace(data.DiaChi))
                                        inner.Item().Text($"Địa chỉ:  {data.DiaChi}").FontColor("#475569");
                                });

                            col.Item().PaddingVertical(10).Height(0);

                            // ── Bảng sản phẩm ─────────────────────────────────
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(c =>
                                {
                                    c.RelativeColumn(4);
                                    c.RelativeColumn(1);
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                });

                                table.Header(h =>
                                {
                                    static IContainer HeaderCell(IContainer c) =>
                                        c.Background("#EEF2FF").Padding(6);

                                    h.Cell().Element(HeaderCell).Text("Tên sản phẩm").Bold().FontSize(9);
                                    h.Cell().Element(HeaderCell).AlignCenter().Text("SL").Bold().FontSize(9);
                                    h.Cell().Element(HeaderCell).AlignRight().Text("Đơn giá").Bold().FontSize(9);
                                    h.Cell().Element(HeaderCell).AlignRight().Text("Thành tiền").Bold().FontSize(9);
                                });

                                bool alt = false;
                                foreach (var item in data.Items)
                                {
                                    string bg = alt ? "#F8FAFC" : "#FFFFFF";
                                    alt = !alt;

                                    table.Cell().Background(bg).Padding(5).Text(item.TenSP).FontSize(9);
                                    table.Cell().Background(bg).Padding(5).AlignCenter()
                                        .Text(item.SoLuong.ToString()).FontSize(9);
                                    table.Cell().Background(bg).Padding(5).AlignRight()
                                        .Text(item.GiaBan.ToString("N0") + "đ").FontSize(9);
                                    table.Cell().Background(bg).Padding(5).AlignRight()
                                        .Text(item.ThanhTien.ToString("N0") + "đ").FontSize(9);
                                }
                            });

                            col.Item().PaddingVertical(6).Height(1).Background("#E2E8F0");

                            // ── Tổng tiền ─────────────────────────────────────
                            col.Item().PaddingTop(4).AlignRight().Column(totals =>
                            {
                                if (data.TienGiam > 0)
                                {
                                    totals.Item().Text($"Tổng tiền hàng:   {data.TongTien:N0}đ")
                                        .FontColor("#64748B");
                                    totals.Item().Text($"Giảm giá:   -{data.TienGiam:N0}đ")
                                        .FontColor("#16A34A");
                                    totals.Item().PaddingVertical(4).Height(1).Background("#E2E8F0");
                                    totals.Item().Text($"Phải thanh toán:   {(data.TongTien - data.TienGiam):N0}đ")
                                        .Bold().FontSize(13).FontColor("#4F46E5");
                                }
                                else
                                {
                                    totals.Item().Text($"Tổng cộng:   {data.TongTien:N0}đ")
                                        .Bold().FontSize(13).FontColor("#4F46E5");
                                }
                            });

                            col.Item().PaddingTop(24).AlignCenter()
                                .Text("Cảm ơn quý khách hàng!").Italic().FontColor("#94A3B8");
                        });
                    });
                }).GeneratePdf(tempPath);

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = tempPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo PDF: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string FormatDate(string ngayTao)
        {
            if (DateTime.TryParse(ngayTao, out DateTime dt))
                return dt.ToString("dd/MM/yyyy  HH:mm");
            return ngayTao;
        }
    }
}
