using QuanLyPhuKienDienTu.DTO;
using QuanLyPhuKienDienTu.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhuKienDienTu
{
    public partial class PayForm : Form
    {
        public static KhachHang khachHang = new KhachHang(); 
        public List<ListViewItem> ListViewItems { get; set; }
        public  delegate void Mydel();
        public Mydel MD { get; set; }
        public PayForm(List<ListViewItem> m)
        {
            ListViewItems = m;
            InitializeComponent();
            LoadListView();

        }
        public void LoadListView()
        {
            Decimal TongGiaBan = 0;
            foreach (ListViewItem i in ListViewItems)
            { 
                listView1.Items.Add(i);
                TongGiaBan += Convert.ToDecimal(i.SubItems[3].Text);
            }
            textBoxPrice.Text = TongGiaBan.ToString();
        }

        public void LoadKhachHang(KhachHang i)
        {
            textBoxName.Text = i.TenKhachHang;
            textBoxPhone.Text = i.SoDienThoai;
            textBoxAddress.Text = i.DiaChi;
        }

        

        private void payButton_Click(object sender, EventArgs e)
        {
            //Add bill mới
            // foreach trong listview add bill info mới
            
            if(khachHang.TenKhachHang != null)
            {
                DateTime date = dateTimePicker1.Value;
                BLL.BLL_HoaDonBan.Instance.AddHoaDonBan(khachHang.MaKhachHang, date);
                int mhd = 0;
                mhd = BLL.BLL_HoaDonBan.Instance.GetMaHoaDonMax();

                foreach (ListViewItem i in listView1.Items)
                {
                    SanPham_View y = new SanPham_View();
                    y = (SanPham_View)listView1.Items[i.Index].Tag;
                    int slg = 0;
                    slg = Convert.ToInt32(listView1.Items[i.Index].SubItems[1].Text);

                    BLL.BLL_HoaDonBanChiTiet.Instance.AddHoaDonBanChiTiet(mhd, y.MaSanPham, slg, ""); 
                }
            }
            this.Close();
               
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            string sdt = textBoxPhone.Text;
           // MessageBox.Show(sdt);
            khachHang = BLL.BLL_KhachHang.Instance.GetKhachHangBySDT(textBoxPhone.Text);
            LoadKhachHang(khachHang);
            MessageBox.Show(khachHang.SoDienThoai);
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            FormKhachHang fKH = new FormKhachHang();
            fKH.ShowDialog();
            int mkhMax = BLL.BLL_KhachHang.Instance.GetMaKhachHangMax();
            khachHang = BLL.BLL_KhachHang.Instance.GetKhachHangByID(mkhMax);
            LoadKhachHang(khachHang);
            MessageBox.Show(khachHang.SoDienThoai);
        }
    }
}
