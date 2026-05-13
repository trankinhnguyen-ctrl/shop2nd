using System.Drawing;
using System.IO;

namespace dosi
{
    public partial class TheSanPham : UserControl
    {
        public TheSanPham()
        {
            InitializeComponent();
        }

        public void LayDuLieu(SanPham sp)
        {
            TenSP.Text = sp.TenSP;
            MaSP.Text = "Mã: " + sp.MaSP;
            SL.Text = "Kho: " + sp.SoLuong;

            string placeholderPath = Path.Combine(Application.StartupPath, "Images", "item_placeholder.png");
            string targetPath = string.IsNullOrEmpty(sp.HinhAnh)
                ? placeholderPath
                : Path.Combine(Application.StartupPath, sp.HinhAnh);

            if (!File.Exists(targetPath))
            {
                targetPath = placeholderPath;
            }

            if (File.Exists(targetPath))
            {
                using (FileStream fs = new FileStream(targetPath, FileMode.Open, FileAccess.Read))
                {
                    HinhAnh.Image = Image.FromStream(fs);
                }
            }
            else
            {
                HinhAnh.Image = null;
            }
        }
    }
}