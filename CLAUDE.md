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
`Form1` (`Quan Ly Shop Do Si.cs`) is the main window. It hosts a left sidebar with four `ReaLTaiizor.Controls.HopeButton` menu items. Clicking a button clears `panelMain` and loads the corresponding `UserControl` via `addUserControl()`.

### Views (UserControls loaded into panelMain)
| File | Purpose |
|---|---|
| `ViewTongQuan.cs` | Dashboard — 4 stat cards (product types, total stock, customers, out-of-stock count) + last 10 transactions |
| `ViewKhoHang.cs` | Inventory — add products with image upload; search/browse as `TheSanPham` cards; click a card to open `SuaSanPhamForm` for edit/delete |
| `ViewKhachHang.cs` | Customer list — `TheKhachHang` card list on left, detail panel on right showing stats and purchase history in a `DataGridView` |
| `ViewGiaoDich.cs` | Point-of-sale — `FlowLayoutPanel` of `TheSanPham` cards, in-memory `DataTable` cart (`gioHang`), phone lookup auto-fills customer info, transactional checkout |

### Data Models
Both `SanPham` and `KhachHang` are active-record style: each has `LuuVaoDatabase()`, `CapNhatDatabase()`, and `XoaKhoiDatabase()` methods that open their own connection and call Dapper directly. The `ConnectionString` is hardcoded as `"Data Source=QuanLyKho.db"` (relative path, resolves to the build output directory at runtime).

### Database Schema (SQLite — `QuanLyKho.db`)
- `SanPham` — products: `id, ma_sp, ten_sp, so_luong_ton, gia_ban, hinh_anh`
- `Khach` — customers: `id, ho_ten, so_dien_thoai, dia_chi, ghi_chu`
- `NhapXuat` — transactions: `id, SAPHAM_id (FK), KHACH_id (FK), loai_giao_dich ('Xuat'), so_luong, ngay_tao, ghi_chu`

The DB file lives in the build output folder (`bin/Debug/net8.0-windows/QuanLyKho.db`). Product images are copied to `bin/Debug/net8.0-windows/Images/` with a GUID filename; the relative path `Images/<guid>.ext` is stored in `SanPham.HinhAnh`.

### UI Rendering Pattern
All rounded corners, card borders, and flat button styles are drawn manually using GDI+ `GraphicsPath` in `Paint` event handlers — there is no WinForms standard border styling used. Double-buffering (`ControlStyles.OptimizedDoubleBuffer`) is applied on views that render many cards. This pattern is used consistently across all views and card controls.

### Reusable Card UserControls
- `TheSanPham` — product card; exposes `OnSelect` (`Action?`) delegate and `LayDuLieu(SanPham)` to populate
- `TheKhachHang` — customer card; exposes `Data` property and `LayDuLieu(KhachHang)`
- `TheLichSu` — transaction history card

### Checkout Flow (`ViewGiaoDich`)
Cart state is an in-memory `DataTable` (`gioHang`) with a computed column `ThanhTien = SoLuong * DonGia`. Checkout runs inside a single SQLite transaction: upserts the customer in `Khach`, inserts one `NhapXuat` row per cart item, and decrements `SanPham.so_luong_ton` with an optimistic check (`WHERE so_luong_ton >= @sl`). Rolls back the entire transaction if any item fails.

## Naming Conventions
- Model properties: PascalCase Vietnamese (`HoTen`, `SoDienThoai`, `GiaBan`)
- DB columns: snake_case Vietnamese (`ho_ten`, `so_dien_thoai`, `gia_ban`)
- Dapper parameter mapping relies on column aliases in SQL (e.g., `ho_ten AS HoTen`) to match model properties
- UI methods and variables: Vietnamese (`LamMoiForm`, `ThemVaoGioHang`, `TinhTongTien`)
