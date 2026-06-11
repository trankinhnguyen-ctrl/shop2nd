using Dapper;
using Microsoft.Data.Sqlite;
using System.Drawing;
using System.IO;

namespace dosi
{
    public partial class SuaSanPhamForm : Form
    {
        private SanPham _sp;
        private string ConnectionString = "Data Source=QuanLyKho.db";

        public SuaSanPhamForm(SanPham sp)
        {
            InitializeComponent();
            _sp = sp;
            HienThiDuLieu();
        }

        private void HienThiDuLieu()
        {
            lbl_MaSPValue.Text = _sp.MaSP;
            txt_TenSP.Text = _sp.TenSP;
            txt_GiaBan.Text = _sp.GiaBan.ToString("N0");
            txt_GiaVon.Text = _sp.GiaVon.ToString("N0");
            txt_SoLuong.Text = _sp.SoLuong.ToString();

            if (!string.IsNullOrEmpty(_sp.HinhAnh))
            {
                string fullPath = Path.Combine(Application.StartupPath, _sp.HinhAnh);
                if (File.Exists(fullPath))
                {
                    try
                    {
                        using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                        {
                            pic_HinhSP.Image = Image.FromStream(fs);
                        }
                    }
                    catch
                    {
                        pic_HinhSP.Image = null;
                    }
                }
            }
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
                return _sp.HinhAnh;
            }
        }

        private void btn_Luu_Click(object? sender, EventArgs e)
        {
            _sp.TenSP = txt_TenSP.Text;

            string giaText = txt_GiaBan.Text.Replace(",", "").Replace(".", "");
            _sp.GiaBan = decimal.TryParse(giaText, out decimal gia) ? gia : 0;

            string giaVonText = txt_GiaVon.Text.Replace(",", "").Replace(".", "");
            _sp.GiaVon = decimal.TryParse(giaVonText, out decimal giaVon) ? giaVon : 0;

            _sp.SoLuong = int.TryParse(txt_SoLuong.Text, out int sl) ? sl : 0;

            if (pic_HinhSP.Tag?.ToString() is string path)
            {
                _sp.HinhAnh = CopyAnhVaoProject(path);
            }

            _sp.PhanLoaiId = (cbo_PhanLoai.SelectedItem as PhanLoaiItem)?.Id ?? 0;

            _sp.CapNhatDatabase();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Xoa_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show("Xác nhận xóa sản phẩm?", "Cảnh báo", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                _sp.XoaKhoiDatabase();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void SuaSanPhamForm_Load(object sender, EventArgs e)
        {
            LoadPhanLoaiDropdown();
        }

        private void LoadPhanLoaiDropdown()
        {
            cbo_PhanLoai.Items.Clear();
            try
            {
                using var conn = new SqliteConnection(ConnectionString);
                conn.Open();
                var list = conn.Query<PhanLoaiItem>("SELECT id AS Id, ten AS Ten, ma AS Ma FROM PhanLoai ORDER BY id").ToList();
                foreach (var item in list)
                    cbo_PhanLoai.Items.Add(item);
            }
            catch { }

            PhanLoaiItem? toSelect = cbo_PhanLoai.Items.OfType<PhanLoaiItem>().FirstOrDefault(p => p.Id == _sp.PhanLoaiId);
            if (cbo_PhanLoai.Items.Count > 0)
                cbo_PhanLoai.SelectedItem = toSelect ?? cbo_PhanLoai.Items[0];
        }
    }
}