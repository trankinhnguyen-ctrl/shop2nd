using System.Data.SQLite;
using Dapper;

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
        }

        private void ViewKhoHang_Load(object sender, EventArgs e)
        {
            LoadSanPham();
        }

        private void LamMoiForm()
        {
            txt_MaSP.Clear();
            txt_TenSP.Clear();
            txt_Size.Clear();
            txtSL.Clear();
            pic_HinhSP.Image = null;
            pic_HinhSP.Tag = null;
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

                SanPham sp = new SanPham
                {
                    MaSP = txt_MaSP.Text,
                    TenSP = txt_TenSP.Text,
                    SoLuong = int.TryParse(txtSL.Text, out int sl) ? sl : 0,
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

                using (var conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    string sql = "SELECT id, ma_sp AS MaSP, ten_sp AS TenSP, so_luong_ton AS SoLuong, hinh_anh AS HinhAnh FROM SanPham";

                    if (!string.IsNullOrEmpty(searchKey) && searchKey != "Tìm Sản Phẩm")
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

                        card.Click += (s, ev) => openEdit();

                        foreach (Control child in card.Controls)
                        {
                            child.Click += (s, ev) => openEdit();
                        }

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

        private void Tim_bt_Click(object sender, EventArgs e)
        {
            LoadSanPham(ThanhTimKiem.Text);
        }

        private void pic_HinhSP_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pic_HinhSP.Image = Image.FromFile(ofd.FileName);
                    pic_HinhSP.Tag = ofd.FileName;
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