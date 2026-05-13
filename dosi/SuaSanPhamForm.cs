using System.Drawing;
using System.IO;

namespace dosi
{
    public partial class SuaSanPhamForm : Form
    {
        private SanPham _sp;

        public SuaSanPhamForm(SanPham sp)
        {
            InitializeComponent();
            _sp = sp;
            HienThiDuLieu();
        }

        private void HienThiDuLieu()
        {
            txt_MaSP.Text = _sp.MaSP;
            txt_TenSP.Text = _sp.TenSP;
            txt_SoLuong.Text = _sp.SoLuong.ToString();

            if (!string.IsNullOrEmpty(_sp.HinhAnh))
            {
                string fullPath = Path.Combine(Application.StartupPath, _sp.HinhAnh);
                if (File.Exists(fullPath))
                {
                    using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                    {
                        pic_HinhSP.Image = Image.FromStream(fs);
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
            _sp.MaSP = txt_MaSP.Text;
            _sp.TenSP = txt_TenSP.Text;
            _sp.SoLuong = int.TryParse(txt_SoLuong.Text, out int sl) ? sl : 0;

            if (pic_HinhSP.Tag?.ToString() is string path)
            {
                _sp.HinhAnh = CopyAnhVaoProject(path);
            }

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
    }
}