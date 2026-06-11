using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace dosi
{
    public partial class Form1 : Form
    {
        private ReaLTaiizor.Controls.HopeButton? currentActiveButton;

        public Form1()
        {
            InitializeComponent();
            SetupButtonHoverEffects();
            panelShadow.Paint += PanelShadow_Paint;
            this.Load += (s, e) =>
            {
                try
                {
                    using var mc = new SqliteConnection("Data Source=QuanLyKho.db");
                    mc.Open();
                    mc.Execute("ALTER TABLE SanPham ADD COLUMN gia_von REAL NOT NULL DEFAULT 0");
                }
                catch { }
                KhoiPhucDonBiNgat();
                MoTrangTongQuan();
            };
        }

        private void KhoiPhucDonBiNgat()
        {
            try
            {
                using var conn = new SqliteConnection("Data Source=QuanLyKho.db");
                conn.Open();
                var stuck = conn.Query<string>("SELECT ma_hd FROM HoaDon WHERE TrangThai = 'DangChinhSua'").ToList();
                if (stuck.Count == 0) return;

                var result = MessageBox.Show(
                    $"Phát hiện {stuck.Count} đơn hàng chưa hoàn tất chỉnh sửa từ phiên trước.\n\n" +
                    "Nhấn Có để khôi phục tự động (hủy các chỉnh sửa chưa lưu).\n" +
                    "Nhấn Không để bỏ qua (chỉ chọn nếu bạn biết chắc điều này).",
                    "Khôi phục dữ liệu",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    foreach (string maHD in stuck)
                        RollbackStuckOrder(maHD, conn);
                }
            }
            catch { }
        }

        private static void RollbackStuckOrder(string maHoaDon, SqliteConnection conn)
        {
            try
            {
                using var trans = conn.BeginTransaction();
                var items = conn.Query(@"
                    SELECT nx.SAPHAM_id AS SpId, nx.so_luong AS SoLuong
                    FROM NhapXuat nx JOIN HoaDon hd ON nx.hoadon_id = hd.id
                    WHERE hd.ma_hd = @maHD AND nx.loai_giao_dich = 'Xuat'",
                    new { maHD = maHoaDon }, transaction: trans).ToList();

                foreach (dynamic item in items)
                {
                    conn.Execute(
                        "UPDATE SanPham SET so_luong_ton = MAX(0, so_luong_ton - @sl) WHERE id = @id",
                        new { sl = (int)(long)item.SoLuong, id = (long)item.SpId },
                        transaction: trans);
                }

                conn.Execute("UPDATE HoaDon SET TrangThai = 'HoanThanh' WHERE ma_hd = @maHD",
                    new { maHD = maHoaDon }, transaction: trans);

                trans.Commit();
            }
            catch { }
        }

        private bool OKToNavigateAway()
        {
            var gd = panelContent.Controls.OfType<ViewGiaoDich>().FirstOrDefault();
            if (gd != null && gd.DangChinhSua)
                return gd.XacNhanHuyChinhSua();
            return true;
        }

        private void PanelShadow_Paint(object? sender, PaintEventArgs e)
        {
            using var brush = new LinearGradientBrush(
                panelShadow.ClientRectangle,
                Color.FromArgb(18, 0, 0, 0),
                Color.Transparent,
                LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(brush, panelShadow.ClientRectangle);
        }

        private void SetupButtonHoverEffects()
        {
            ReaLTaiizor.Controls.HopeButton[] menus = { btnTongQuan, btnKhoHang, btnKhachHang, btnGiaoDich, btnPhanTich };
            foreach (var btn in menus)
            {
                btn.MouseEnter += (s, e) =>
                {
                    if (btn != currentActiveButton)
                    {
                        btn.PrimaryColor = Color.FromArgb(241, 245, 249);
                        btn.TextColor = Color.FromArgb(51, 65, 85);
                        btn.Invalidate();
                    }
                };
                btn.MouseLeave += (s, e) =>
                {
                    if (btn != currentActiveButton)
                    {
                        btn.PrimaryColor = Color.White;
                        btn.TextColor = Color.FromArgb(51, 65, 85);
                        btn.Invalidate();
                    }
                };
            }
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContent.Controls.Clear();
            panelContent.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void UpdateMenuUI(ReaLTaiizor.Controls.HopeButton activeBtn)
        {
            ReaLTaiizor.Controls.HopeButton[] menus = { btnTongQuan, btnKhoHang, btnKhachHang, btnGiaoDich, btnPhanTich };
            foreach (var btn in menus)
            {
                btn.PrimaryColor = Color.White;
                btn.TextColor = Color.FromArgb(51, 65, 85);
                btn.Invalidate();
            }

            activeBtn.PrimaryColor = Color.FromArgb(124, 58, 237);
            activeBtn.TextColor = Color.White;
            activeBtn.Invalidate();
            currentActiveButton = activeBtn;

            if (activeBtn == btnTongQuan)       lblPageTitle.Text = "Tổng quan";
            else if (activeBtn == btnKhoHang)   lblPageTitle.Text = "Kho hàng";
            else if (activeBtn == btnKhachHang) lblPageTitle.Text = "Khách hàng";
            else if (activeBtn == btnGiaoDich)  lblPageTitle.Text = "Giao dịch";
            else if (activeBtn == btnPhanTich)  lblPageTitle.Text = "Phân tích";
        }

        private void MoTrangTongQuan()
        {
            ViewTongQuan uc = new ViewTongQuan();
            addUserControl(uc);
            UpdateMenuUI(btnTongQuan);
        }

        private void btnTongQuan_Click(object sender, EventArgs e)
        {
            if (!OKToNavigateAway()) return;
            MoTrangTongQuan();
        }

        private void btnKhoHang_Click(object sender, EventArgs e)
        {
            if (!OKToNavigateAway()) return;
            ViewKhoHang uc = new ViewKhoHang();
            addUserControl(uc);
            UpdateMenuUI(btnKhoHang);
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            if (!OKToNavigateAway()) return;
            ViewKhachHang uc = new ViewKhachHang();
            addUserControl(uc);
            UpdateMenuUI(btnKhachHang);
        }

        public void MoTrangKhachHang(int khachId)
        {
            if (!OKToNavigateAway()) return;
            ViewKhachHang uc = new ViewKhachHang();
            addUserControl(uc);
            UpdateMenuUI(btnKhachHang);
            uc.ChonKhachHangTheoId(khachId);
        }

        private void btnGiaoDich_Click(object sender, EventArgs e)
        {
            if (!OKToNavigateAway()) return;
            ViewGiaoDich uc = new ViewGiaoDich();
            addUserControl(uc);
            UpdateMenuUI(btnGiaoDich);
        }

        public void MoTrangGiaoDichVoiEdit(EditOrderContext ctx)
        {
            if (!OKToNavigateAway()) return;
            var uc = new ViewGiaoDich();
            addUserControl(uc);
            UpdateMenuUI(btnGiaoDich);
            uc.LoadEditOrder(ctx);
        }

        private void btnPhanTich_Click(object sender, EventArgs e)
        {
            if (!OKToNavigateAway()) return;
            ViewPhanTich uc = new ViewPhanTich();
            addUserControl(uc);
            UpdateMenuUI(btnPhanTich);
        }

        private void picTongQuan_Click(object sender, EventArgs e)
        {

        }
    }
}
