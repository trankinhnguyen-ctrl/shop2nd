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
        private int? _selectedPhanLoaiId = null;

        private static readonly Color[] _pillColors = {
            Color.FromArgb(236, 72, 153),
            Color.FromArgb(245, 158, 11),
            Color.FromArgb(16, 185, 129),
            Color.FromArgb(239, 68, 68),
            Color.FromArgb(14, 165, 233),
            Color.FromArgb(168, 85, 247),
            Color.FromArgb(20, 184, 166),
            Color.FromArgb(234, 88, 12),
        };

        public ViewKhoHang()
        {
            InitializeComponent();
            this.Load += ViewKhoHang_Load;
            this.Resize += (s, e) => { LoadSanPham(ThanhTimKiem.Text); };
            pic_HinhSP.Click += pic_HinhSP_Click;
            Tim_bt.Click += Tim_bt_Click;

            panelMainContainer.Paint += PanelMainContainer_Paint;
            pic_HinhSP.Paint += Pic_HinhSP_Paint;

            Tim_bt.Paint += Buttons_Paint;
            Them_bt.Paint += Buttons_Paint;

            txt_MaSP.BorderStyle = BorderStyle.None;
            txt_TenSP.BorderStyle = BorderStyle.None;
            txt_GiaBan.BorderStyle = BorderStyle.None;
            txtSL.BorderStyle = BorderStyle.None;
            ThanhTimKiem.BorderStyle = BorderStyle.None;

            SetPaddingForTextBox(txt_MaSP);
            SetPaddingForTextBox(txt_TenSP);
            SetPaddingForTextBox(txt_GiaBan);
            SetPaddingForTextBox(txtSL);
            SetPaddingForTextBox(ThanhTimKiem);
        }

        private void ViewKhoHang_Load(object? sender, EventArgs e)
        {
            EnsureDatabase();
            LoadPhanLoai();
            LoadSanPham();
        }

        #region Database Migration

        private void EnsureDatabase()
        {
            using (var conn = new SqliteConnection(ConnectionString))
            {
                conn.Open();
                conn.Execute(@"CREATE TABLE IF NOT EXISTS PhanLoai (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL
                )");
                try { conn.Execute("ALTER TABLE SanPham ADD COLUMN phanloai_id INTEGER"); } catch { }
            }
        }

        #endregion

        #region Category Pills

        private void LoadPhanLoai()
        {
            flpCategory.SuspendLayout();
            flpCategory.Controls.Clear();

            // "Thêm phân loại" button - only in KhoHang
            Button btnThem = CreatePillButton("+ Thêm phân loại", Color.FromArgb(99, 102, 241), false);
            btnThem.Margin = new Padding(0, 0, 12, 0);
            btnThem.Click += (s, e) => ShowThemPhanLoaiDialog();
            flpCategory.Controls.Add(btnThem);

            // "Tất cả" pill
            bool allSelected = _selectedPhanLoaiId == null;
            Button btnAll = CreatePillButton("Tất cả", Color.FromArgb(99, 102, 241), allSelected);
            btnAll.Click += (s, e) =>
            {
                _selectedPhanLoaiId = null;
                LoadPhanLoai();
                LoadSanPham(ThanhTimKiem.Text);
            };
            flpCategory.Controls.Add(btnAll);

            // Category pills
            try
            {
                using (var conn = new SqliteConnection(ConnectionString))
                {
                    conn.Open();
                    var list = conn.Query("SELECT id, name AS Ten FROM PhanLoai ORDER BY id").ToList();
                    for (int i = 0; i < list.Count; i++)
                    {
                        int id = (int)(long)list[i].id;
                        string ten = (string)list[i].Ten;
                        Color color = _pillColors[i % _pillColors.Length];
                        bool selected = _selectedPhanLoaiId == id;

                        Button btn = CreatePillButton(ten, color, selected);
                        int capturedId = id;
                        btn.Click += (s, e) =>
                        {
                            _selectedPhanLoaiId = capturedId;
                            LoadPhanLoai();
                            LoadSanPham(ThanhTimKiem.Text);
                        };
                        flpCategory.Controls.Add(btn);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải phân loại: " + ex.Message);
            }

            flpCategory.ResumeLayout();
        }

        private Button CreatePillButton(string text, Color color, bool selected)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btn.Height = 32;
            btn.Width = TextRenderer.MeasureText(text, btn.Font).Width + 28;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = selected ? color : Color.White;
            btn.ForeColor = selected ? Color.White : color;
            btn.Cursor = Cursors.Hand;
            btn.Margin = new Padding(0, 0, 8, 0);
            btn.UseVisualStyleBackColor = false;

            int r = btn.Height / 2;
            using (var path = new GraphicsPath())
            {
                path.StartFigure();
                path.AddArc(0, 0, r * 2, r * 2, 180, 90);
                path.AddArc(btn.Width - r * 2 - 1, 0, r * 2, r * 2, 270, 90);
                path.AddArc(btn.Width - r * 2 - 1, btn.Height - r * 2 - 1, r * 2, r * 2, 0, 90);
                path.AddArc(0, btn.Height - r * 2 - 1, r * 2, r * 2, 90, 90);
                path.CloseFigure();
                btn.Region = new Region(path);
            }

            // WinForms FlatStyle borders don't follow Region clipping — draw only the outline
            if (!selected)
            {
                btn.Paint += (s, e) =>
                {
                    var b = (Button)s!;
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    int bR = b.Height / 2;
                    using (var p = new GraphicsPath())
                    {
                        p.StartFigure();
                        p.AddArc(0, 0, bR * 2, bR * 2, 180, 90);
                        p.AddArc(b.Width - bR * 2 - 1, 0, bR * 2, bR * 2, 270, 90);
                        p.AddArc(b.Width - bR * 2 - 1, b.Height - bR * 2 - 1, bR * 2, bR * 2, 0, 90);
                        p.AddArc(0, b.Height - bR * 2 - 1, bR * 2, bR * 2, 90, 90);
                        p.CloseFigure();
                        using (var pen = new Pen(color, 1.5f))
                            e.Graphics.DrawPath(pen, p);
                    }
                };
            }

            return btn;
        }

        private void ShowThemPhanLoaiDialog()
        {
            using Form dlg = new Form();
            dlg.Text = "Thêm phân loại";
            dlg.Size = new Size(340, 160);
            dlg.StartPosition = FormStartPosition.CenterParent;
            dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
            dlg.MaximizeBox = false;
            dlg.MinimizeBox = false;

            TextBox txt = new TextBox();
            txt.Font = new Font("Segoe UI", 10F);
            txt.Location = new Point(20, 20);
            txt.Size = new Size(285, 32);
            txt.PlaceholderText = "Tên phân loại...";
            dlg.Controls.Add(txt);

            Button btnOK = new Button();
            btnOK.Text = "Thêm";
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(100, 68);
            btnOK.Size = new Size(90, 36);
            btnOK.BackColor = Color.FromArgb(99, 102, 241);
            btnOK.ForeColor = Color.White;
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.FlatAppearance.BorderSize = 0;
            dlg.Controls.Add(btnOK);

            Button btnCancel = new Button();
            btnCancel.Text = "Huỷ";
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(202, 68);
            btnCancel.Size = new Size(90, 36);
            dlg.Controls.Add(btnCancel);

            dlg.AcceptButton = btnOK;
            dlg.CancelButton = btnCancel;

            if (dlg.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(txt.Text))
            {
                try
                {
                    using (var conn = new SqliteConnection(ConnectionString))
                    {
                        conn.Open();
                        conn.Execute("INSERT INTO PhanLoai (name) VALUES (@ten)", new { ten = txt.Text.Trim() });
                    }
                    LoadPhanLoai();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        #endregion

        #region UI Custom Drawing (GDI+ No-Library)

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

                if (pic.Image == null)
                {
                    using (Pen dashPen = new Pen(Color.FromArgb(148, 163, 184), 1.5f))
                    {
                        dashPen.DashStyle = DashStyle.Dash;
                        dashPen.DashPattern = new float[] { 6, 4 };
                        e.Graphics.DrawPath(dashPen, path);
                    }

                    string text = "Hình ảnh sản phẩm";
                    Font textFont = new Font("Segoe UI", 10F, FontStyle.Regular);
                    Color textColor = Color.FromArgb(148, 163, 184);
                    Size textSize = TextRenderer.MeasureText(text, textFont);

                    e.Graphics.DrawString(text, textFont, new SolidBrush(textColor),
                        (pic.Width - textSize.Width) / 2, (pic.Height - textSize.Height) / 2 + 20);
                }
                else
                {
                    pic.Region = new Region(path);
                }
            }
        }

        private void Buttons_Paint(object? sender, PaintEventArgs e)
        {
            Button? btn = sender as Button;
            if (btn == null) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            int radius = 12;

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

                TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, btn.ClientRectangle, btn.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

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
            pic_HinhSP.Refresh();
            txt_MaSP.Focus();
        }

        private void ThemSanPham()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_MaSP.Text)) return;

                string pathGoc = pic_HinhSP.Tag != null ? pic_HinhSP.Tag.ToString() ?? string.Empty : string.Empty;
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
                    string sql = "SELECT id, ma_sp AS MaSP, ten_sp AS TenSP, so_luong_ton AS SoLuong, hinh_anh AS HinhAnh, gia_ban AS GiaBan FROM SanPham WHERE 1=1";

                    if (!string.IsNullOrEmpty(searchKey))
                        sql += " AND (ma_sp LIKE @key OR ten_sp LIKE @key)";

                    if (_selectedPhanLoaiId.HasValue)
                        sql += " AND phanloai_id = @plId";

                    var danhSach = conn.Query<SanPham>(sql, new { key = "%" + searchKey + "%", plId = _selectedPhanLoaiId }).ToList();

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
                    pic_HinhSP.Refresh();
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
