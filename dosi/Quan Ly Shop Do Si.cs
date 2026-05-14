using System;
using System.Windows.Forms;

namespace dosi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ViewKhoHang uc = new ViewKhoHang();
            addUserControl(uc);
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void btnKhoHang_Click(object sender, EventArgs e)
        {
            ViewKhoHang uc = new ViewKhoHang();
            addUserControl(uc);
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            ViewKhachHang uc = new ViewKhachHang();
            addUserControl(uc);
        }

        private void btnGiaoDich_Click(object sender, EventArgs e)
        {
            ViewGiaoDich uc = new ViewGiaoDich();
            addUserControl(uc);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đứa nào bấm vô là gay đó nha :v");
        }
    }
}