using System;
using System.Windows.Forms;

namespace dosi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += (s, e) => MoTrangTongQuan();
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void MoTrangTongQuan()
        {
            ViewTongQuan uc = new ViewTongQuan();
            addUserControl(uc);
        }

        private void btnTongQuan_Click(object sender, EventArgs e)
        {
            MoTrangTongQuan();
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
    }
}