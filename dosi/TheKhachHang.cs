namespace dosi
{
    public partial class TheKhachHang : UserControl
    {
        public KhachHang? Data { get; private set; }

        public TheKhachHang()
        {
            InitializeComponent();
        }

        public void LayDuLieu(KhachHang kh)
        {
            Data = kh;
            lblTen.Text = kh.HoTen;
            lblSdt.Text = kh.SoDienThoai;
        }

        private void TheKhachHang_Load(object sender, EventArgs e)
        {

        }
    }
}