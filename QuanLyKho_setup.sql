-- ================================================================
--  SETUP DATA - Quan Ly Shop Do Si
--  Gia tri tinh toan dua tren gia SanPham thuc te trong DB.
--  Khong cham vao SanPham va PhanLoai.
-- ================================================================

-- ── Schema (bo qua neu da co) ────────────────────────────────────
CREATE TABLE IF NOT EXISTS KhuyenMai (
    id                  INTEGER PRIMARY KEY AUTOINCREMENT,
    ma_km               TEXT    UNIQUE NOT NULL,
    ten_km              TEXT    NOT NULL,
    so_luong_ton        INTEGER,
    ngay_bat_dau        TEXT,
    ngay_ket_thuc       TEXT,
    loai_dieu_kien      INTEGER DEFAULT 0,
    gia_tri_dieu_kien   NUMERIC,
    loai_giam_gia       INTEGER DEFAULT 1,
    gia_tri_giam        NUMERIC,
    giam_toi_da         NUMERIC,
    trang_thai          INTEGER DEFAULT 1
);
CREATE TABLE IF NOT EXISTS HoaDon_KhuyenMai (
    id              INTEGER PRIMARY KEY AUTOINCREMENT,
    hoadon_id       INTEGER NOT NULL,
    khuyenmai_id    INTEGER NOT NULL,
    so_tien_giam    NUMERIC NOT NULL
);

-- ── Xoa du lieu cu ───────────────────────────────────────────────
DELETE FROM HoaDon_KhuyenMai;
DELETE FROM NhapXuat;
DELETE FROM HoaDon;
DELETE FROM KhuyenMai;
DELETE FROM Khach;
DELETE FROM sqlite_sequence WHERE name IN (
    'HoaDon_KhuyenMai','NhapXuat','HoaDon','KhuyenMai','Khach'
);

-- ── KHACH HANG (id 1-10 sau reset) ──────────────────────────────
-- Kiem tra dieu kien voucher tinh den ngay 2026-06-07:
--   id 1-4  : >= 24 thang  → du ca THAN6T + VIP2NAM
--   id 5-8  : 7-17 thang   → du THAN6T, chua du VIP2NAM
--   id 9-10 : 1-5 thang    → chua du THAN6T
INSERT INTO Khach (ho_ten, so_dien_thoai, dia_chi, ghi_chu, ngay_tao) VALUES
('Nguyen Thi Lan',   '0901234567', '12 Le Loi, Q1, TP.HCM',         'Mua si thuong xuyen', '2022-01-15'),
('Tran Van Minh',    '0912345678', '45 Nguyen Hue, Q1, TP.HCM',      '',                    '2022-06-20'),
('Le Thi Hoa',       '0923456789', '78 Tran Phu, Q5, TP.HCM',        '',                    '2023-03-10'),
('Pham Quoc Bao',    '0934567890', '23 Vo Van Tan, Q3, TP.HCM',      'Mua si cuoi tuan',    '2023-09-25'),
('Hoang Thi Mai',    '0945678901', '56 CMT8, Q10, TP.HCM',           '',                    '2024-10-14'),
('Vu Duc Thanh',     '0956789012', '90 Dinh Tien Hoang, BT, TP.HCM', '',                    '2025-01-08'),
('Dang Thi Thu',     '0967890123', '15 Phan Xich Long, PN, TP.HCM',  '',                    '2025-03-05'),
('Bui Van Long',     '0978901234', '34 Ly Thuong Kiet, Q10, TP.HCM', '',                    '2025-08-20'),
('Dinh Thi Phuong',  '0989012345', '67 Nguyen Dinh Chieu, Q3',       '',                    '2026-01-10'),
('Ngo Van Tuan',     '0990123456', '11 Truong Chinh, TB, TP.HCM',    'Khach moi',           '2026-04-15');

-- ── KHUYEN MAI (id 1-7 sau reset) ───────────────────────────────
-- loai_dieu_kien: 0=Khong co  1=Tong tien>=  2=SL SP>=  3=Thang khach hang
-- loai_giam_gia:  1=Tien mat  2=Phan tram
INSERT INTO KhuyenMai
    (ma_km, ten_km, so_luong_ton, ngay_bat_dau, ngay_ket_thuc,
     loai_dieu_kien, gia_tri_dieu_kien, loai_giam_gia, gia_tri_giam, giam_toi_da, trang_thai)
VALUES
-- id=1
('CHAO50',    'Chao mung khach moi - Giam 50k',             100,  '2025-01-01', '2026-12-31', 0, NULL,    1, 50000,  NULL,   1),
-- id=2
('HOADON500', 'Don tu 500k - Giam 10% toi da 100k',          50,  '2025-01-01', '2026-12-31', 1, 500000,  2, 10,     100000, 1),
-- id=3
('HOADON1TR', 'Don tu 1 trieu - Giam 200k tien mat',        NULL, '2025-01-01', NULL,         1, 1000000, 1, 200000, NULL,   1),
-- id=4
('COMBO3SP',  'Mua tu 3 san pham - Giam 50k',               200,  '2025-01-01', '2026-06-30', 2, 3,       1, 50000,  NULL,   1),
-- id=5
('THAN6T',    'Khach than thiet 6 thang - Giam 8% toi da 150k', NULL, '2025-01-01', NULL,     3, 6,       2, 8,      150000, 1),
-- id=6
('VIP2NAM',   'Khach VIP 2 nam - Giam 15% toi da 300k',    NULL, '2025-01-01', NULL,         3, 24,      2, 15,     300000, 1),
-- id=7  (het han de test hien thi)
('TETHETHAO', 'Khuyen mai Tet 2025 - Da het han',           NULL, '2025-01-15', '2025-02-28', 0, NULL,    1, 100000, NULL,   0);

-- ── HOA DON + NHAP XUAT ──────────────────────────────────────────
-- Gia lay tu SanPham thuc te (xem ben duoi moi HD)
-- SP gia tham khao: id1=185k id3=320k id4=450k id6=280k id7=120k
-- id11=320k id12=450k id14=280k id16=90k id17=380k id18=200k
-- id20=300k id21=160k id22=220k id23=400k id24=200k id25=350k
-- id26=80k  id27=260k

-- HD1: Khach 1 (VIP 2 nam) | SP1 x3 + SP3 x1 | Tong=875k | Khong voucher
INSERT INTO HoaDon (ma_hd, KHACH_id, ngay_tao, tong_tien, tong_tien_giam, tien_phai_thanh, ghi_chu, TrangThai)
VALUES ('HD20250301001', 1, '2025-03-01 09:15:00', 875000, 0, 875000, '', 'HoanThanh');
INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, hoadon_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu)
VALUES (1, 1, last_insert_rowid(), 'Xuat', 3, '2025-03-01 09:15:00', 'Ban hang truc tiep');
INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, hoadon_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu)
VALUES (3, 1, (SELECT id FROM HoaDon WHERE ma_hd='HD20250301001'), 'Xuat', 1, '2025-03-01 09:15:00', 'Ban hang truc tiep');

-- HD2: Khach 3 (>24 thang) | SP1 x2 + SP6 x1 | Tong=650k | CHAO50 -50k = 600k
INSERT INTO HoaDon (ma_hd, KHACH_id, ngay_tao, tong_tien, tong_tien_giam, tien_phai_thanh, ghi_chu, TrangThai)
VALUES ('HD20250310002', 3, '2025-03-10 14:30:00', 650000, 50000, 600000, '', 'HoanThanh');
INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, hoadon_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu)
VALUES (1, 3, (SELECT id FROM HoaDon WHERE ma_hd='HD20250310002'), 'Xuat', 2, '2025-03-10 14:30:00', 'Ban hang truc tiep');
INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, hoadon_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu)
VALUES (6, 3, (SELECT id FROM HoaDon WHERE ma_hd='HD20250310002'), 'Xuat', 1, '2025-03-10 14:30:00', 'Ban hang truc tiep');

-- HD3: Khach 2 (>24 thang) | SP4 x2 + SP11 x1 | Tong=1220k | HOADON1TR -200k = 1020k
INSERT INTO HoaDon (ma_hd, KHACH_id, ngay_tao, tong_tien, tong_tien_giam, tien_phai_thanh, ghi_chu, TrangThai)
VALUES ('HD20250315003', 2, '2025-03-15 10:00:00', 1220000, 200000, 1020000, '', 'HoanThanh');
INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, hoadon_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu)
VALUES (4, 2, (SELECT id FROM HoaDon WHERE ma_hd='HD20250315003'), 'Xuat', 2, '2025-03-15 10:00:00', 'Ban hang truc tiep');
INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, hoadon_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu)
VALUES (11, 2, (SELECT id FROM HoaDon WHERE ma_hd='HD20250315003'), 'Xuat', 1, '2025-03-15 10:00:00', 'Ban hang truc tiep');

-- HD4: Khach 7 (15 thang - du THAN6T) | SP7 x2 + SP18 x1 | Tong=440k | Khong voucher
INSERT INTO HoaDon (ma_hd, KHACH_id, ngay_tao, tong_tien, tong_tien_giam, tien_phai_thanh, ghi_chu, TrangThai)
VALUES ('HD20250320004', 7, '2025-03-20 16:45:00', 440000, 0, 440000, '', 'HoanThanh');
INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, hoadon_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu)
VALUES (7, 7, (SELECT id FROM HoaDon WHERE ma_hd='HD20250320004'), 'Xuat', 2, '2025-03-20 16:45:00', 'Ban hang truc tiep');
INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, hoadon_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu)
VALUES (18, 7, (SELECT id FROM HoaDon WHERE ma_hd='HD20250320004'), 'Xuat', 1, '2025-03-20 16:45:00', 'Ban hang truc tiep');

-- HD5: Khach 5 (7 thang - du THAN6T) | SP21 x2 + SP20 x2 | Tong=920k | HOADON500 -10%=92k = 828k
INSERT INTO HoaDon (ma_hd, KHACH_id, ngay_tao, tong_tien, tong_tien_giam, tien_phai_thanh, ghi_chu, TrangThai)
VALUES ('HD20250401005', 5, '2025-04-01 11:20:00', 920000, 92000, 828000, '', 'HoanThanh');
INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, hoadon_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu)
VALUES (21, 5, (SELECT id FROM HoaDon WHERE ma_hd='HD20250401005'), 'Xuat', 2, '2025-04-01 11:20:00', 'Ban hang truc tiep');
INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, hoadon_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu)
VALUES (20, 5, (SELECT id FROM HoaDon WHERE ma_hd='HD20250401005'), 'Xuat', 2, '2025-04-01 11:20:00', 'Ban hang truc tiep');

-- HD6: Khach 1 (VIP 2 nam) | SP17 x3 + SP14 x2 | Tong=1700k | VIP2NAM -15%=255k = 1445k
INSERT INTO HoaDon (ma_hd, KHACH_id, ngay_tao, tong_tien, tong_tien_giam, tien_phai_thanh, ghi_chu, TrangThai)
VALUES ('HD20250410006', 1, '2025-04-10 09:00:00', 1700000, 255000, 1445000, '', 'HoanThanh');
INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, hoadon_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu)
VALUES (17, 1, (SELECT id FROM HoaDon WHERE ma_hd='HD20250410006'), 'Xuat', 3, '2025-04-10 09:00:00', 'Ban hang truc tiep');
INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, hoadon_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu)
VALUES (14, 1, (SELECT id FROM HoaDon WHERE ma_hd='HD20250410006'), 'Xuat', 2, '2025-04-10 09:00:00', 'Ban hang truc tiep');

-- HD7: Khach 4 (>24 thang) | SP26 x1 + SP16 x1 + SP22 x1 | Tong=390k | COMBO3SP (3 SP) -50k = 340k
INSERT INTO HoaDon (ma_hd, KHACH_id, ngay_tao, tong_tien, tong_tien_giam, tien_phai_thanh, ghi_chu, TrangThai)
VALUES ('HD20250415007', 4, '2025-04-15 13:30:00', 390000, 50000, 340000, '', 'HoanThanh');
INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, hoadon_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu)
VALUES (26, 4, (SELECT id FROM HoaDon WHERE ma_hd='HD20250415007'), 'Xuat', 1, '2025-04-15 13:30:00', 'Ban hang truc tiep');
INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, hoadon_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu)
VALUES (16, 4, (SELECT id FROM HoaDon WHERE ma_hd='HD20250415007'), 'Xuat', 1, '2025-04-15 13:30:00', 'Ban hang truc tiep');
INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, hoadon_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu)
VALUES (22, 4, (SELECT id FROM HoaDon WHERE ma_hd='HD20250415007'), 'Xuat', 1, '2025-04-15 13:30:00', 'Ban hang truc tiep');

-- HD8: Khach 6 (17 thang - du THAN6T) | SP23 x1 + SP24 x1 | Tong=600k | THAN6T -8%=48k = 552k
INSERT INTO HoaDon (ma_hd, KHACH_id, ngay_tao, tong_tien, tong_tien_giam, tien_phai_thanh, ghi_chu, TrangThai)
VALUES ('HD20250501008', 6, '2025-05-01 10:45:00', 600000, 48000, 552000, '', 'HoanThanh');
INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, hoadon_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu)
VALUES (23, 6, (SELECT id FROM HoaDon WHERE ma_hd='HD20250501008'), 'Xuat', 1, '2025-05-01 10:45:00', 'Ban hang truc tiep');
INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, hoadon_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu)
VALUES (24, 6, (SELECT id FROM HoaDon WHERE ma_hd='HD20250501008'), 'Xuat', 1, '2025-05-01 10:45:00', 'Ban hang truc tiep');

-- HD9: Khach 9 (5 thang - chua du THAN6T) | SP27 x2 | Tong=520k | Khong du dieu kien voucher
INSERT INTO HoaDon (ma_hd, KHACH_id, ngay_tao, tong_tien, tong_tien_giam, tien_phai_thanh, ghi_chu, TrangThai)
VALUES ('HD20250512009', 9, '2025-05-12 15:00:00', 520000, 0, 520000, '', 'HoanThanh');
INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, hoadon_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu)
VALUES (27, 9, (SELECT id FROM HoaDon WHERE ma_hd='HD20250512009'), 'Xuat', 2, '2025-05-12 15:00:00', 'Ban hang truc tiep');

-- HD10: Khach 1 (VIP 2 nam) | SP12 x3 + SP25 x2 | Tong=2050k | VIP2NAM -15%=307k > 300k cap -> -300k = 1750k
INSERT INTO HoaDon (ma_hd, KHACH_id, ngay_tao, tong_tien, tong_tien_giam, tien_phai_thanh, ghi_chu, TrangThai)
VALUES ('HD20250601010', 1, '2025-06-01 14:00:00', 2050000, 300000, 1750000, '', 'HoanThanh');
INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, hoadon_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu)
VALUES (12, 1, (SELECT id FROM HoaDon WHERE ma_hd='HD20250601010'), 'Xuat', 3, '2025-06-01 14:00:00', 'Ban hang truc tiep');
INSERT INTO NhapXuat (SAPHAM_id, KHACH_id, hoadon_id, loai_giao_dich, so_luong, ngay_tao, ghi_chu)
VALUES (25, 1, (SELECT id FROM HoaDon WHERE ma_hd='HD20250601010'), 'Xuat', 2, '2025-06-01 14:00:00', 'Ban hang truc tiep');

-- ── HOA DON KHUYEN MAI ───────────────────────────────────────────
INSERT INTO HoaDon_KhuyenMai (hoadon_id, khuyenmai_id, so_tien_giam)
SELECT h.id, k.id, 50000
FROM HoaDon h, KhuyenMai k WHERE h.ma_hd='HD20250310002' AND k.ma_km='CHAO50';

INSERT INTO HoaDon_KhuyenMai (hoadon_id, khuyenmai_id, so_tien_giam)
SELECT h.id, k.id, 200000
FROM HoaDon h, KhuyenMai k WHERE h.ma_hd='HD20250315003' AND k.ma_km='HOADON1TR';

INSERT INTO HoaDon_KhuyenMai (hoadon_id, khuyenmai_id, so_tien_giam)
SELECT h.id, k.id, 92000
FROM HoaDon h, KhuyenMai k WHERE h.ma_hd='HD20250401005' AND k.ma_km='HOADON500';

INSERT INTO HoaDon_KhuyenMai (hoadon_id, khuyenmai_id, so_tien_giam)
SELECT h.id, k.id, 255000
FROM HoaDon h, KhuyenMai k WHERE h.ma_hd='HD20250410006' AND k.ma_km='VIP2NAM';

INSERT INTO HoaDon_KhuyenMai (hoadon_id, khuyenmai_id, so_tien_giam)
SELECT h.id, k.id, 50000
FROM HoaDon h, KhuyenMai k WHERE h.ma_hd='HD20250415007' AND k.ma_km='COMBO3SP';

INSERT INTO HoaDon_KhuyenMai (hoadon_id, khuyenmai_id, so_tien_giam)
SELECT h.id, k.id, 48000
FROM HoaDon h, KhuyenMai k WHERE h.ma_hd='HD20250501008' AND k.ma_km='THAN6T';

INSERT INTO HoaDon_KhuyenMai (hoadon_id, khuyenmai_id, so_tien_giam)
SELECT h.id, k.id, 300000
FROM HoaDon h, KhuyenMai k WHERE h.ma_hd='HD20250601010' AND k.ma_km='VIP2NAM';
