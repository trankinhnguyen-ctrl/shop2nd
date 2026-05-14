using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dosi
{
    public partial class TheLichSu : UserControl
    {
        public TheLichSu()
        {
            InitializeComponent();
        }

        public void LayDuLieu(dynamic data)
        {
            lblTenSP.Text = data.ten_sp;
            lblSoLuong.Text = "Số lượng: " + data.so_luong;
            lblThanhTien.Text = string.Format("{0:N0}đ", data.thanh_tien);
            lblNgayTao.Text = data.ngay_tao;
        }
    }
}