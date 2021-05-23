using QuanLyPhuKienDienTu.BLL;
using QuanLyPhuKienDienTu.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhuKienDienTu.View
{
    public partial class FormChiTietNhap : Form
    {
        public int MaHoaDon { get; set; }
        public FormChiTietNhap(int id)
        {
            InitializeComponent();
            MaHoaDon = id;
            LoadThuongHieu();
            LoadProduct();
        }

        private void LoadThuongHieu()
        {
            ThuongHieu thuongHieu = BLL_ThongTinNhap.Instance.ThongTinThuongHieu(MaHoaDon);
            lbName.Text = thuongHieu.TenThuongHieu;
            lbOrigin.Text = thuongHieu.XuatXu;
        }

        private void LoadProduct()
        {
            List<ChiTiet> sanPham = BLL_ThongTinNhap.Instance.ThongTinSanPham(MaHoaDon);

            for (int i = 1; i <= sanPham.Count; i++)
            {
                ListViewItem listViewItem = new ListViewItem(i.ToString()) { Tag = sanPham[i - 1].MaSanPham };
                listViewItem.SubItems.Add(sanPham[i - 1].TenSanPham);
                listViewItem.SubItems.Add(sanPham[i - 1].MauSac);
                listViewItem.SubItems.Add(sanPham[i - 1].SoLuong.ToString());
                listViewItem.SubItems.Add(sanPham[i - 1].Gia.ToString());
                listViewItem.SubItems.Add((sanPham[i - 1].SoLuong * sanPham[i - 1].Gia).ToString());

                lvDetail.Items.Add(listViewItem);
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
