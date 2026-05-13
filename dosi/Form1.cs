namespace dosi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đứa nào bấm vô là gay đó nha :v");
        }


        private void tabPage1_Click(object sender, EventArgs e)
        {

        }


        private void userControl11_Load(object sender, EventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void khoHang1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill; // Để UserControl lấp đầy panelMain
            panelMain.Controls.Clear();       // Xóa sạch cái cũ đang hiện
            panelMain.Controls.Add(userControl); // Thêm cái mới vào
            userControl.BringToFront();       // Đưa lên trên cùng để hiển thị
        }
        private void btnKhoHang_Click(object sender, EventArgs e)
        {
            // Gọi cái UserControl KhoHang bạn đã tạo (như thấy trong Solution Explorer)
            KhoHang uc = new KhoHang();
            addUserControl(uc);
        }
        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            // Tạo mới một instance của UserControl Khach_Hang
            Khach_Hang uc = new Khach_Hang();

            // Nạp nó vào cái PanelMain (cái thùng chứa bên phải Form1)
            addUserControl(uc);
        }
    }
}
