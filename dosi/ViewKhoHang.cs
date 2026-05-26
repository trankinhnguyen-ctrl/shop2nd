using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace dosi
{
    public partial class ViewKhoHang : UserControl
    {
        private string ConnectionString = "Data Source=QuanLyKho.db";

        public ViewKhoHang()
        {
            InitializeComponent();
            this.Load += ViewKhoHang_Load;
            this.Resize += (s, e) => { LoadSanPham(ThanhTimKiem.Text); };
            pic_HinhSP.Click += pic_HinhSP_Click;
            Tim_bt.Click += Tim_bt_Click;

            // Đăng ký các sự kiện tự vẽ giao diện hiện đại bằng GDI+
            panelMainContainer.Paint += PanelMainContainer_Paint;
            pic_HinhSP.Paint += Pic_HinhSP_Paint;

            // Xử lý vẽ bo góc mượt mà cho các nút bấm phẳng
            Tim_bt.Paint += Buttons_Paint;
            Them_bt.Paint += Buttons_Paint;

            // Xử lý làm mượt nền cho các ô nhập liệu (TextBox)
            txt_MaSP.BorderStyle = BorderStyle.None;
            txt_TenSP.BorderStyle = BorderStyle.None;
            txt_GiaBan.BorderStyle = BorderStyle.None;
            txtSL.BorderStyle = BorderStyle.None;
            ThanhTimKiem.BorderStyle = BorderStyle.None;

            // Gán padding giả lập thông qua gán đè vùng vẽ để chữ không dính sát viền
            SetPaddingForTextBox(txt_MaSP);
            SetPaddingForTextBox(txt_TenSP);
            SetPaddingForTextBox(txt_GiaBan);
            SetPaddingForTextBox(txtSL);
            SetPaddingForTextBox(ThanhTimKiem);
        }

        private void ViewKhoHang_Load(object? sender, EventArgs e)
        {
            LoadSanPham();
        }

        #region UI Custom Drawing (GDI+ No-Library)

        // 1. Tự vẽ khối nền trắng lớn bao quanh thông tin chi tiết (Bo góc 24 giống Figma)
        private void PanelMainContainer_Paint(object? sender, PaintEventArgs e)
        {
            Panel? pnl = sender as Panel;
            if (pnl == null) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            int radius = 24;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.StartFigure();
                path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
                path.AddArc(new Rectangle(pnl.Width - radius - 1, 0, radius, radius), 270, 90);
                path.AddArc(new Rectangle(pnl.Width - radius - 1, pnl.Height - radius - 1, radius, radius), 0, 90);
                path.AddArc(new Rectangle(0, pnl.Height - radius - 1, radius, radius), 90, 90);
                path.CloseFigure();

                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillPath(brush, path);
                }
                using (Pen pen = new Pen(Color.FromArgb(226, 232, 240), 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        // 2. Tự vẽ khung chứa ảnh nét đứt (Dash Border) và văn bản trung tâm
        private void Pic_HinhSP_Paint(object? sender, PaintEventArgs e)
        {
            PictureBox? pic = sender as PictureBox;
            if (pic == null) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            int radius = 16;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.StartFigure();
                path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
                path.AddArc(new Rectangle(pic.Width - radius - 1, 0, radius, radius), 270, 90);
                path.AddArc(new Rectangle(pic.Width - radius - 1, pic.Height - radius - 1, radius, radius), 0, 90);
                path.AddArc(new Rectangle(0, pic.Height - radius - 1, radius, radius), 90, 90);
                path.CloseFigure();

                // Nếu chưa chọn ảnh, vẽ đường nét đứt và chữ hướng dẫn y hệt Figma
                if (pic.Image == null)
                {
                    using (Pen dashPen = new Pen(Color.FromArgb(148, 163, 184), 1.5f))
                    {
                        dashPen.DashStyle = DashStyle.Dash;
                        dashPen.DashPattern = new float[] { 6, 4 };
                        e.Graphics.DrawPath(dashPen, path);
                    }

                    // Vẽ chuỗi văn bản trung tâm "Hình ảnh sản phẩm"
                    string text = "Hình ảnh sản phẩm";
                    Font textFont = new Font("Segoe UI", 10F, FontStyle.Regular);
                    Color textColor = Color.FromArgb(148, 163, 184);
                    Size textSize = TextRenderer.MeasureText(text, textFont);

                    e.Graphics.DrawString(text, textFont, new SolidBrush(textColor),
                        (pic.Width - textSize.Width) / 2, (pic.Height - textSize.Height) / 2 + 20);
                }
                else
                {
                    // Nếu đã có ảnh, ép khung ảnh cắt bo tròn theo đúng viền mềm mại
                    pic.Region = new Region(path);
                }
            }
        }

        // 3. Tự vẽ bo góc tròn mượt mà cho các nút bấm Flat Button
        private void Buttons_Paint(object? sender, PaintEventArgs e)
        {
            Button? btn = sender as Button;
            if (btn == null) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            int radius = 12; // Bo tròn góc nút bấm vừa vặn thanh thoát

            using (GraphicsPath path = new GraphicsPath())
            {
                path.StartFigure();
                path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
                path.AddArc(new Rectangle(btn.Width - radius - 1, 0, radius, radius), 270, 90);
                path.AddArc(new Rectangle(btn.Width - radius - 1, btn.Height - radius - 1, radius, radius), 0, 90);
                path.AddArc(new Rectangle(0, btn.Height - radius - 1, radius, radius), 90, 90);
                path.CloseFigure();

                btn.Region = new Region(path);

                using (SolidBrush brush = new SolidBrush(btn.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }

                // Vẽ lại text chữ của nút bấm căn giữa
                TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, btn.ClientRectangle, btn.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        // Hỗ trợ căn lề chữ bên trong TextBox khi bỏ BorderStyle
        private void SetPaddingForTextBox(TextBox textBox)
        {
            textBox.Location = new Point(textBox.Location.X, textBox.Location.Y + 4);
            textBox.Height = 32;
        }

        #endregion

        private void LamMoiForm()
        {
            txt_MaSP.Clear();
            txt_TenSP.Clear();
            txt_GiaBan.Clear();
            txtSL.Clear();
            pic_HinhSP.Image = null;
            pic_HinhSP.Tag = null;
            pic_HinhSP.Refresh(); // Buộc khung ảnh vẽ lại trạng thái nét đứt ban đầu
            txt_MaSP.Focus();
        }

        private void ThemSanPham()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_MaSP.Text)) return;

                string pathGoc = pic_HinhSP.Tag != null ? pic_HinhSP.Tag.ToString() : string.Empty;
                string pathLuuDB = string.Empty;

                if (!string.IsNullOrEmpty(pathGoc))
                {
                    pathLuuDB = CopyAnhVaoProject(pathGoc);
                }

                decimal.TryParse(txt_GiaBan.Text, out decimal gia);
                int.TryParse(txtSL.Text, out int sl);

                SanPham sp = new SanPham
                {
                    MaSP = txt_MaSP.Text,
                    TenSP = txt_TenSP.Text,
                    GiaBan = gia,
                    SoLuong = sl,
                    HinhAnh = pathLuuDB
                };

                sp.LuuVaoDatabase();
                MessageBox.Show("Thêm thành công!");
                LamMoiForm();
                LoadSanPham();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void LoadSanPham(string searchKey = "")
        {
            try
            {
                CardLayout.SuspendLayout();
                CardLayout.Controls.Clear();

                using (var conn = new SqliteConnection(ConnectionString))
                {
                    conn.Open();
                    string sql = "SELECT id, ma_sp AS MaSP, ten_sp AS TenSP, so_luong_ton AS SoLuong, hinh_anh AS HinhAnh, gia_ban AS GiaBan FROM SanPham";

                    if (!string.IsNullOrEmpty(searchKey))
                    {
                        sql += " WHERE ma_sp LIKE @key OR ten_sp LIKE @key";
                    }

                    var danhSach = conn.Query<SanPham>(sql, new { key = "%" + searchKey + "%" }).ToList();

                    foreach (var sp in danhSach)
                    {
                        TheSanPham card = new TheSanPham();
                        card.LayDuLieu(sp);

                        Action openEdit = () =>
                        {
                            SuaSanPhamForm frm = new SuaSanPhamForm(sp);
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                LoadSanPham();
                            }
                        };

                        card.OnSelect = openEdit;
                        CardLayout.Controls.Add(card);
                    }
                }
                CardLayout.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void Them_bt_Click(object sender, EventArgs e)
        {
            ThemSanPham();
        }

        private void Tim_bt_Click(object? sender, EventArgs e)
        {
            LoadSanPham(ThanhTimKiem.Text);
        }

        private void pic_HinhSP_Click(object? sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pic_HinhSP.Image = Image.FromFile(ofd.FileName);
                    pic_HinhSP.Tag = ofd.FileName;
                    pic_HinhSP.Refresh(); // Làm tươi lại khung hình để nạp vùng vẽ đồ họa mới
                }
            }
        }

        private string CopyAnhVaoProject(string pathGoc)
        {
            try
            {
                string folderDich = Path.Combine(Application.StartupPath, "Images");
                if (!Directory.Exists(folderDich)) Directory.CreateDirectory(folderDich);

                string tenFileMoi = Guid.NewGuid().ToString() + Path.GetExtension(pathGoc);
                string pathDich = Path.Combine(folderDich, tenFileMoi);

                File.Copy(pathGoc, pathDich, true);
                return Path.Combine("Images", tenFileMoi);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}