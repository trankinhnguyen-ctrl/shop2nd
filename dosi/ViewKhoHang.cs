using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
            ThanhTimKiem.TextChanged += (s, e) => LoadSanPham(ThanhTimKiem.Text);
            Them_bt.Click += Them_bt_Click;
            Them_bt.Cursor = Cursors.Hand;
            Tim_bt.Cursor = Cursors.Hand;

            panelMainContainer.Paint += PanelMainContainer_Paint;
            panelFields.Paint += PanelFields_Paint;
            pic_HinhSP.Paint += Pic_HinhSP_Paint;

            foreach (Control ctrl in new Control[] { txt_TenSP, txt_GiaBan, txt_GiaVon, txtSL, cbo_PhanLoai })
            {
                ctrl.Enter += (s, e) => panelFields.Invalidate();
                ctrl.Leave += (s, e) => panelFields.Invalidate();
            }

            txt_TenSP.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; FocusEndOf(txt_GiaBan); } };
            txt_GiaBan.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; FocusEndOf(txt_GiaVon); } };
            txt_GiaVon.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; FocusEndOf(txtSL); } };
            txtSL.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; cbo_PhanLoai.Focus(); } };

            Tim_bt.Paint += Buttons_Paint;
            Them_bt.Paint += Buttons_Paint;

            txt_TenSP.BorderStyle = BorderStyle.None;
            txt_GiaBan.BorderStyle = BorderStyle.None;
            txt_GiaVon.BorderStyle = BorderStyle.None;
            txtSL.BorderStyle = BorderStyle.None;
            ThanhTimKiem.BorderStyle = BorderStyle.None;

            SetPaddingForTextBox(txt_TenSP);
            SetPaddingForTextBox(txt_GiaBan);
            SetPaddingForTextBox(txt_GiaVon);
            SetPaddingForTextBox(txtSL);
            SetPaddingForTextBox(ThanhTimKiem);

            // Shift all panelFields controls right so the drawn left border is not clipped at x=0
            foreach (Control c in panelFields.Controls)
                c.Location = new Point(c.Location.X + 6, c.Location.Y);
        }

        private void ViewKhoHang_Load(object? sender, EventArgs e)
        {
            LoadPhanLoai();
            LoadSanPham();
        }

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

            // Category pills + refresh add-form dropdown
            int? comboCurrentId = (cbo_PhanLoai.SelectedItem as PhanLoaiItem)?.Id;
            cbo_PhanLoai.Items.Clear();

            try
            {
                using (var conn = new SqliteConnection(ConnectionString))
                {
                    conn.Open();
                    var list = conn.Query<PhanLoaiItem>("SELECT id AS Id, ten AS Ten, ma AS Ma FROM PhanLoai ORDER BY id").ToList();
                    for (int i = 0; i < list.Count; i++)
                    {
                        Color color = _pillColors[i % _pillColors.Length];
                        bool selected = _selectedPhanLoaiId == list[i].Id;

                        Button btn = CreatePillButton(list[i].Ten, color, selected);
                        int capturedId = list[i].Id;
                        btn.Click += (s, e) =>
                        {
                            _selectedPhanLoaiId = capturedId;
                            LoadPhanLoai();
                            LoadSanPham(ThanhTimKiem.Text);
                        };
                        flpCategory.Controls.Add(btn);

                        cbo_PhanLoai.Items.Add(list[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải phân loại: " + ex.Message);
            }

            // Restore or default combo selection
            PhanLoaiItem? toSelect = null;
            if (comboCurrentId.HasValue)
                toSelect = cbo_PhanLoai.Items.OfType<PhanLoaiItem>().FirstOrDefault(p => p.Id == comboCurrentId);
            if (cbo_PhanLoai.Items.Count > 0)
                cbo_PhanLoai.SelectedItem = toSelect ?? cbo_PhanLoai.Items[0];

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
            btn.BackColor = Color.White;
            btn.ForeColor = selected ? Color.White : color;
            btn.Cursor = Cursors.Hand;
            btn.Margin = new Padding(0, 4, 8, 4);
            btn.UseVisualStyleBackColor = false;

            btn.Paint += (s, e) =>
            {
                var b = (Button)s!;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Fill corners with parent bg so they blend in (no Region needed)
                Color parentBg = b.Parent?.BackColor ?? Color.FromArgb(241, 245, 249);
                using (var bgBrush = new SolidBrush(parentBg))
                    e.Graphics.FillRectangle(bgBrush, b.ClientRectangle);

                // Inset 1px so the 1.5px border's outer edge lands at y≈30.75, safely within 32px
                const int inset = 1;
                int diam = b.Height - inset * 2 - 1; // = 29
                using (var p = new GraphicsPath())
                {
                    p.AddArc(inset, inset, diam, diam, 90, 180);
                    p.AddArc(b.Width - inset - diam - 1, inset, diam, diam, 270, 180);
                    p.CloseFigure();

                    if (selected)
                    {
                        using (var brush = new SolidBrush(color))
                            e.Graphics.FillPath(brush, p);
                    }
                    else
                    {
                        using (var brush = new SolidBrush(Color.White))
                            e.Graphics.FillPath(brush, p);
                        using (var pen = new Pen(color, 1.5f))
                            e.Graphics.DrawPath(pen, p);
                    }
                }

                TextRenderer.DrawText(e.Graphics, b.Text, b.Font, b.ClientRectangle,
                    b.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };

            return btn;
        }

        private void ShowThemPhanLoaiDialog()
        {
            using Form dlg = new Form();
            dlg.Text = "Thêm phân loại";
            dlg.Size = new Size(340, 230);
            dlg.StartPosition = FormStartPosition.CenterParent;
            dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
            dlg.MaximizeBox = false;
            dlg.MinimizeBox = false;

            TextBox txtTen = new TextBox();
            txtTen.Font = new Font("Segoe UI", 10F);
            txtTen.Location = new Point(20, 20);
            txtTen.Size = new Size(285, 32);
            txtTen.PlaceholderText = "Tên phân loại... (VD: Áo Khoác)";
            dlg.Controls.Add(txtTen);

            Label lblMa = new Label();
            lblMa.Text = "Mã (viết tắt):";
            lblMa.Font = new Font("Segoe UI", 9F);
            lblMa.Location = new Point(20, 62);
            lblMa.AutoSize = true;
            dlg.Controls.Add(lblMa);

            TextBox txtMa = new TextBox();
            txtMa.Font = new Font("Segoe UI", 10F);
            txtMa.Location = new Point(20, 82);
            txtMa.Size = new Size(285, 32);
            txtMa.PlaceholderText = "VD: AK, QD, GD...";
            dlg.Controls.Add(txtMa);

            Button btnOK = new Button();
            btnOK.Text = "Thêm";
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(100, 132);
            btnOK.Size = new Size(90, 36);
            btnOK.BackColor = Color.FromArgb(99, 102, 241);
            btnOK.ForeColor = Color.White;
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.FlatAppearance.BorderSize = 0;
            dlg.Controls.Add(btnOK);

            Button btnCancel = new Button();
            btnCancel.Text = "Huỷ";
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(202, 132);
            btnCancel.Size = new Size(90, 36);
            dlg.Controls.Add(btnCancel);

            dlg.AcceptButton = btnOK;
            dlg.CancelButton = btnCancel;

            if (dlg.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(txtTen.Text))
            {
                try
                {
                    using (var conn = new SqliteConnection(ConnectionString))
                    {
                        conn.Open();
                        conn.Execute("INSERT INTO PhanLoai (ten, ma) VALUES (@ten, @ma)",
                            new { ten = txtTen.Text.Trim(), ma = txtMa.Text.Trim().ToUpper() });
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

        private void PanelFields_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            DrawInputContainer(e.Graphics, txt_TenSP);
            DrawInputContainer(e.Graphics, txt_GiaBan);
            DrawInputContainer(e.Graphics, txt_GiaVon);
            DrawInputContainer(e.Graphics, txtSL);
            DrawInputContainer(e.Graphics, cbo_PhanLoai);
        }

        private void DrawInputContainer(Graphics g, Control ctrl)
        {
            bool focused = ctrl.ContainsFocus;
            const int padX = 2, padYTop = 4, padYBottom = 6, radius = 10;

            Rectangle outer = new Rectangle(
                ctrl.Left - padX,
                ctrl.Top - padYTop,
                ctrl.Width + padX * 2,
                ctrl.Height + padYTop + padYBottom
            );

            using (GraphicsPath path = new GraphicsPath())
            {
                path.StartFigure();
                path.AddArc(new Rectangle(outer.X, outer.Y, radius, radius), 180, 90);
                path.AddArc(new Rectangle(outer.Right - radius - 1, outer.Y, radius, radius), 270, 90);
                path.AddArc(new Rectangle(outer.Right - radius - 1, outer.Bottom - radius - 1, radius, radius), 0, 90);
                path.AddArc(new Rectangle(outer.X, outer.Bottom - radius - 1, radius, radius), 90, 90);
                path.CloseFigure();

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(248, 250, 252)))
                    g.FillPath(brush, path);

                Color borderColor = focused ? Color.FromArgb(99, 102, 241) : Color.FromArgb(209, 213, 219);
                using (Pen pen = new Pen(borderColor, focused ? 2f : 1.5f))
                    g.DrawPath(pen, path);
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private void SetPaddingForTextBox(TextBox textBox)
        {
            textBox.Location = new Point(textBox.Location.X, textBox.Location.Y + 4);
            textBox.Height = 32;
            // EM_SETMARGINS: left=10px, right=6px
            SendMessage(textBox.Handle, 0xD3, (IntPtr)3, (IntPtr)((6 << 16) | 10));
        }

        #endregion

        private void FocusEndOf(TextBox txt)
        {
            txt.Focus();
            txt.BeginInvoke(new Action(() => { txt.SelectionStart = txt.Text.Length; txt.SelectionLength = 0; }));
        }

        private void LamMoiForm()
        {
            txt_TenSP.Clear();
            txt_GiaBan.Clear();
            txt_GiaVon.Clear();
            txtSL.Clear();
            pic_HinhSP.Image = null;
            pic_HinhSP.Tag = null;
            pic_HinhSP.Refresh();
            if (cbo_PhanLoai.Items.Count > 0)
                cbo_PhanLoai.SelectedIndex = 0;
            txt_TenSP.Focus();
        }

        private string GenerateMaSP(string maPhanLoai)
        {
            using (var conn = new SqliteConnection(ConnectionString))
            {
                conn.Open();
                var codes = conn.Query<string>(
                    "SELECT ma_sp FROM SanPham WHERE ma_sp LIKE @prefix",
                    new { prefix = maPhanLoai + "-%" }
                ).ToList();

                int maxNum = 0;
                foreach (var code in codes)
                {
                    if (code.Length > maPhanLoai.Length + 1)
                    {
                        string numPart = code.Substring(maPhanLoai.Length + 1);
                        if (int.TryParse(numPart, out int num) && num > maxNum)
                            maxNum = num;
                    }
                }

                return $"{maPhanLoai}-{(maxNum + 1):D3}";
            }
        }

        private void ThemSanPham()
        {
            try
            {
                var selectedPhanLoai = cbo_PhanLoai.SelectedItem as PhanLoaiItem;
                if (selectedPhanLoai == null || string.IsNullOrEmpty(selectedPhanLoai.Ma))
                {
                    MessageBox.Show("Vui lòng chọn phân loại hợp lệ!");
                    return;
                }

                string pathGoc = pic_HinhSP.Tag != null ? pic_HinhSP.Tag.ToString() ?? string.Empty : string.Empty;
                string pathLuuDB = string.Empty;

                if (!string.IsNullOrEmpty(pathGoc))
                    pathLuuDB = CopyAnhVaoProject(pathGoc);

                decimal.TryParse(txt_GiaBan.Text, out decimal gia);
                decimal.TryParse(txt_GiaVon.Text, out decimal giaVon);
                int.TryParse(txtSL.Text, out int sl);

                string maSP = GenerateMaSP(selectedPhanLoai.Ma);

                SanPham sp = new SanPham
                {
                    MaSP = maSP,
                    TenSP = txt_TenSP.Text,
                    GiaBan = gia,
                    GiaVon = giaVon,
                    SoLuong = sl,
                    HinhAnh = pathLuuDB,
                    PhanLoaiId = selectedPhanLoai.Id
                };

                sp.LuuVaoDatabase();
                MessageBox.Show($"Thêm thành công! Mã SP: {maSP}");
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
                    string sql = "SELECT id, ma_sp AS MaSP, ten_sp AS TenSP, so_luong_ton AS SoLuong, hinh_anh AS HinhAnh, gia_ban AS GiaBan, COALESCE(gia_von, 0) AS GiaVon, phanloai_id AS PhanLoaiId FROM SanPham WHERE 1=1";

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
