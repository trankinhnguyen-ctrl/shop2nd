namespace dosi
{
    public partial class TheSanPham : UserControl
    {
        public Action? OnSelect { get; set; }

        public TheSanPham()
        {
            InitializeComponent();

            this.Click += (s, e) => OnSelect?.Invoke();
            foreach (Control child in this.Controls)
            {
                child.Click += (s, e) => OnSelect?.Invoke();
            }
        }

        public void LayDuLieu(SanPham sp)
        {
            TenSP.Text = sp.TenSP;
            MaSP.Text = "Mã: " + sp.MaSP;
            SL.Text = "Kho: " + sp.SoLuong;
            GiaBan.Text = sp.GiaBan.ToString("N0") + "đ";

            string path = Path.Combine(Application.StartupPath, sp.HinhAnh);
            if (!string.IsNullOrEmpty(sp.HinhAnh) && File.Exists(path))
            {
                try
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        HinhAnh.Image = Image.FromStream(fs);
                    }
                }
                catch
                {
                    HinhAnh.Image = null;
                }
            }
            else
            {
                HinhAnh.Image = null;
            }
        }
    }
}