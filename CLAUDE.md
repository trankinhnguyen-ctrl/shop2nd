# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

A Windows Forms desktop application for managing a second-hand/wholesale clothing shop ("Quản Lý Shop Đồ Sỉ"). Built with .NET 8.0-windows, SQLite, Dapper, and ReaLTaiizor UI controls.

## Build & Run Commands

```powershell
# Build
dotnet build dosi/dosi.csproj

# Run
dotnet run --project dosi/dosi.csproj

# Build release
dotnet publish dosi/dosi.csproj -c Release
```

No test project exists in this repo.

## Architecture

### Shell + Navigation
`Form1` (`Quan Ly Shop Do Si.cs`) is the main window. It hosts a left sidebar with five `ReaLTaiizor.Controls.HopeButton` menu items. Clicking a button calls `OKToNavigateAway()` first — which prompts the user if an order edit is in progress — then clears `panelContent` and loads the corresponding `UserControl` via `addUserControl()`.

Two deep-link entry points exist on `Form1` for cross-view navigation:
- `MoTrangKhachHang(khachId)` — opens `ViewKhachHang` and selects a specific customer
- `MoTrangGiaoDichVoiEdit(EditOrderContext)` — opens `ViewGiaoDich` pre-loaded with an existing order for editing

### Startup Lifecycle
On `Form1.Load`, before anything else:
1. `RunMigrations()` — adds `TrangThai` and `MaHoaDonMoi` columns to `HoaDon` with `ALTER TABLE … ADD COLUMN` wrapped in individual try/catch (idempotent).
2. `KhoiPhucDonBiNgat()` — detects any `HoaDon` rows stuck in `TrangThai = 'DangChinhSua'` from a crashed session and offers to roll them back.

### Views (UserControls loaded into panelContent)
| File | Purpose |
|---|---|
| `ViewTongQuan.cs` | Dashboard — 4 stat cards + last 10 transactions as `TheHoatDong` cards; clicking an activity card deep-links to that customer in `ViewKhachHang` |
| `ViewKhoHang.cs` | Inventory — add products with image upload; search/browse as `TheSanPham` cards; click a card to open `SuaSanPhamForm` for edit/delete |
| `ViewKhachHang.cs` | Customer list — `TheKhachHang` card list on left, detail panel on right showing stats and purchase history as `TheHoaDon` cards in a scrollable panel |
| `ViewGiaoDich.cs` | Point-of-sale — `FlowLayoutPanel` of `TheSanPham` cards, in-memory `DataTable` cart (`gioHang`), phone lookup auto-fills customer info, transactional checkout; also hosts the order-edit flow |
| `ViewPhanTich.cs` | Analytics — date-range filter, 3 KPI cards (revenue / orders / items), daily revenue line chart (WinForms Charting), top-5 products and customers in `DataGridView`s; clicking a chart point opens `ChiTietForm` |

### Data Models
Both `SanPham` and `KhachHang` are active-record style: each has `LuuVaoDatabase()`, `CapNhatDatabase()`, and `XoaKhoiDatabase()` methods that open their own connection and call Dapper directly. The `ConnectionString` is hardcoded as `"Data Source=QuanLyKho.db"` (relative path, resolves to the build output directory at runtime).

### Database Schema (SQLite — `QuanLyKho.db`)
- `SanPham` — products: `id, ma_sp, ten_sp, so_luong_ton, gia_ban, hinh_anh`
- `Khach` — customers: `id, ho_ten, so_dien_thoai, dia_chi, ghi_chu`
- `HoaDon` — invoice headers: `id, ma_hd, ngay_tao, tong_tien, TrangThai ('HoanThanh'|'DangChinhSua'), MaHoaDonMoi`
- `NhapXuat` — transaction line items: `id, SAPHAM_id (FK→SanPham), KHACH_id (FK→Khach), hoadon_id (FK→HoaDon), loai_giao_dich ('Xuat'), so_luong, ngay_tao, ghi_chu`

The DB file lives in the build output folder (`bin/Debug/net8.0-windows/QuanLyKho.db`). Product images are copied to `bin/Debug/net8.0-windows/Images/` with a GUID filename; the relative path `Images/<guid>.ext` is stored in `SanPham.HinhAnh`.

### Order Edit Flow
When the user clicks "✎ Sửa" on a `TheHoaDon` card in `ViewKhachHang`:
1. `TheHoaDon` fires `_onEditClick(maHoaDon)`.
2. The caller builds an `EditOrderContext` (holds `MaHoaDonGoc`, customer info, and a `List<EditOrderItem>`).
3. `Form1.MoTrangGiaoDichVoiEdit(ctx)` opens `ViewGiaoDich` and calls `LoadEditOrder(ctx)`, which marks the original `HoaDon` as `'DangChinhSua'`, restores stock, and pre-fills the cart.
4. On checkout, the original invoice is cancelled (`TrangThai='DaChinhSua'`, `MaHoaDonMoi` set to new `ma_hd`) and a new `HoaDon` is created.
5. `OKToNavigateAway()` blocks navigation away from `ViewGiaoDich` while `DangChinhSua` is true.

### Dialogs
- `ChiTietForm` — modal dialog; opened by clicking a data point on `ViewPhanTich`'s chart; shows all invoices for that day grouped by `hoadon_id`, each rendered as a card with a `DataGridView` of line items.
- `SuaSanPhamForm` — edit/delete a product; opened from `ViewKhoHang`.
- `SuaKhachHangForm` — edit a customer; opened from `ViewKhachHang`.

### UI Rendering Pattern
All rounded corners, card borders, and flat button styles are drawn manually using GDI+ `GraphicsPath` in `Paint` event handlers — there is no WinForms standard border styling used. Double-buffering (`ControlStyles.OptimizedDoubleBuffer`) is applied on views that render many cards. This pattern is used consistently across all views and card controls.

### Reusable Card UserControls
- `TheSanPham` — product card; exposes `OnSelect` (`Action?`) delegate and `LayDuLieu(SanPham)` to populate
- `TheKhachHang` — customer card; exposes `Data` property and `LayDuLieu(KhachHang)`
- `TheLichSu` — transaction history card
- `TheHoatDong` — activity row card used in `ViewTongQuan`; exposes `KhachId` and `OnSelect` delegate; hover state propagates through all child controls
- `TheHoaDon` — invoice card used in `ViewKhachHang`; shows a collapsible `DataGridView` of line items + header with date, total, status badge, and optional "✎ Sửa" button; supports `trangThai` values `HoanThanh`, `DangChinhSua`, `DaChinhSua`

### Checkout Flow (`ViewGiaoDich`)
Cart state is an in-memory `DataTable` (`gioHang`) with a computed column `ThanhTien = SoLuong * DonGia`. Checkout runs inside a single SQLite transaction: upserts the customer in `Khach`, inserts a `HoaDon` header row, inserts one `NhapXuat` row per cart item (with `hoadon_id`), and decrements `SanPham.so_luong_ton` with an optimistic check (`WHERE so_luong_ton >= @sl`). Rolls back the entire transaction if any item fails.

## Naming Conventions
- Model properties: PascalCase Vietnamese (`HoTen`, `SoDienThoai`, `GiaBan`)
- DB columns: snake_case Vietnamese (`ho_ten`, `so_dien_thoai`, `gia_ban`); newer columns (post-migration) use PascalCase (`TrangThai`, `MaHoaDonMoi`)
- Dapper parameter mapping relies on column aliases in SQL (e.g., `ho_ten AS HoTen`) to match model properties
- UI methods and variables: Vietnamese (`LamMoiForm`, `ThemVaoGioHang`, `TinhTongTien`)
