using QuanLyPhuKienDienTu.BLL;
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
    public partial class MainForm : Form
    {
        public KhachHang khachHang { get; set; }
        public List<SanPham> listBanSanPham { get; set; }
        public List<SanPham> listNhapSanPham { get; set; }

        public MainForm()
        {
            InitializeComponent();
            setComboBox();
            show();
            LoadSanPhamView();
            foreach(KhachHang i in BLL.BLL_KhachHang.Instance.GetKhachHang())
            {
                
            }    
        }

        // Tab Bán Hàng
        #region TabBanHang
        #region Method
        // Set ComboBox trong tab Bán hàng
        private void setComboBox() 
        {
            TB_cbbLoai.Items.Add(new CBBItem
            {
                Value = 0,
                Text = "All"
            });
            TB_cbbTH.Items.Add(new CBBItem
            {
                Value = 0,
                Text = "All"
            });
            

            foreach (Loai i in BLL.BLL_Loai.Instance.GetListLoai())
            {
                TB_cbbLoai.Items.Add(new CBBItem
                {
                    Value = i.MaLoai,
                    Text = i.TenLoai
                });
            }
            TB_cbbLoai.SelectedIndex = 0;

            foreach (ThuongHieu i in BLL.BLL_ThuongHieu.Instance.GetListThuongHieu())
            {
                TB_cbbTH.Items.Add(new CBBItem
                {
                    Value = i.MaThuongHieu,
                    Text = i.TenThuongHieu
                });
                
            }

            foreach (ThuongHieu i in BLL.BLL_ThuongHieu.Instance.GetListThuongHieu())
            {
                TN_cbbTimTH.Items.Add(new CBBItem
                {
                    Value = i.MaThuongHieu,
                    Text = i.TenThuongHieu
                });
                TN_cbbTimTH.AutoCompleteCustomSource.Add(i.TenThuongHieu);
            }

            TB_cbbTH.SelectedIndex = 0;
            TB_cbbGia.SelectedIndex = 0;
            
            
        }

        // Load DataGridView trong tab Bán Hàng
        public void show()
        {
            TB_dgvSanPham.DataSource = BLL.BLL_SanPham.Instance.GetSanPham_Views("All", "All", "All", "All");
            SetShow();
        }

        // Ẩn các column không cần thiết
        public void SetShow()
        {
            TB_dgvSanPham.Columns["MoTa"].Visible = false;
            TB_dgvSanPham.Columns["MaSanPham"].Visible = false;
            TB_dgvSanPham.Columns["MauSac"].Visible = false;
            TB_dgvSanPham.Columns["ThoiLuongBaoHanh"].Visible = false;
        }


        // Load thông tin khách hàng
        public void LoadKhachHang(KhachHang i)
        {
            TB_cbbTenKH.Text = i.TenKhachHang;
            TB_cbbSDT.Text = i.SoDienThoai;
            TB_cbbDiaChiKH.Text = i.DiaChi;
        }

        // Load Thông tin sản phẩm
        public void LoadSanPhamView()
        {
            TB_txtTenSP.DataBindings.Clear();
            TB_txtTenSP.DataBindings.Add(new Binding("Text", TB_dgvSanPham.DataSource, "TenSanPham"));

            TB_txtTenTH.DataBindings.Clear();
            TB_txtTenTH.DataBindings.Add(new Binding("Text", TB_dgvSanPham.DataSource, "TenThuongHieu"));

            TB_txtTenLoai.DataBindings.Clear();
            TB_txtTenLoai.DataBindings.Add(new Binding("Text", TB_dgvSanPham.DataSource, "TenLoai"));

            TB_txtMauSac.DataBindings.Clear();
            TB_txtMauSac.DataBindings.Add(new Binding("Text", TB_dgvSanPham.DataSource, "MauSac"));

            TB_txtThoiLuongBH.DataBindings.Clear();
            TB_txtThoiLuongBH.DataBindings.Add(new Binding("Text", TB_dgvSanPham.DataSource, "ThoiLuongBaoHanh"));

            TB_txtGiaBan.DataBindings.Clear();
            TB_txtGiaBan.DataBindings.Add(new Binding("Text", TB_dgvSanPham.DataSource, "GiaBan"));

            TB_txtSoLuongTonKho.DataBindings.Clear();
            TB_txtSoLuongTonKho.DataBindings.Add(new Binding("Text", TB_dgvSanPham.DataSource, "SoLuongTonKho"));

            TB_rtxtMoTa.DataBindings.Clear();
            TB_rtxtMoTa.DataBindings.Add(new Binding("Text", TB_dgvSanPham.DataSource, "MoTa"));
        }

        // Check sản phẩm đã có trong listView chưa, nếu có trả về vị trí item đã tồn tại
        public int check(int MSP, ListView list)
        {
            foreach (ListViewItem i in list.Items)
            {
                if (i.Text == BLL.BLL_SanPham.Instance.GetSanPhamByID(MSP).TenSanPham)
                    return i.Index;
            }
            return -100;
        }

        // Thêm sản phẩm vào listView Bán
        public void AddListViewBan()
        {

            int a = Convert.ToInt32(TB_dgvSanPham.CurrentRow.Cells["MaSanPham"].Value.ToString());
            int index = check(a, TB_lvGioHang);
            SanPham_View i = BLL.BLL_SanPham.Instance.GetSanPhamByID(a);
            decimal TongTienHoaDon = 0;
            int SoLuongBan = Convert.ToInt32(numericSoLuongBan.Value);

            if (index != -100)
            {

                int SLgSau = 0;

                int SLgTruoc = Convert.ToInt32(TB_lvGioHang.Items[index].SubItems[1].Text);

                SLgSau = SLgTruoc + Convert.ToInt32(numericSoLuongBan.Value);


                Decimal GiaBan = Convert.ToDecimal(TB_lvGioHang.Items[index].SubItems[2].Text);
                TB_lvGioHang.Items[index].SubItems[1].Text = SLgSau.ToString();
                TB_lvGioHang.Items[index].SubItems[3].Text = ((SLgSau * GiaBan).ToString());
                TongTienHoaDon = Convert.ToDecimal(TB_txtTongTien.Text) + (SLgSau * GiaBan);
                TB_txtTongTien.Text = TongTienHoaDon.ToString();
            }
            else
            {
                ListViewItem listView = new ListViewItem(i.TenSanPham);

                listView.Tag = i;

                listView.SubItems.Add(numericSoLuongBan.Value.ToString());
                listView.SubItems.Add(i.GiaBan.ToString());
                listView.SubItems.Add((i.GiaBan * SoLuongBan).ToString());

                TongTienHoaDon += (Decimal)(i.GiaBan * SoLuongBan);

                TB_lvGioHang.Items.Add(listView);
                TB_txtTongTien.Text = TongTienHoaDon.ToString();
            }

        }

        // Xóa sản phẩm khỏi listView Bán
        public void DeleteListViewBan()
        {
            string tensp = TB_lvGioHang.SelectedItems[0].SubItems[0].Text;
            int SLgSau = 0;
            int SLgTruoc = Convert.ToInt32(TB_lvGioHang.SelectedItems[0].SubItems[1].Text);
            SLgSau = SLgTruoc - Convert.ToInt32(numericSoLuongBan.Value);
            if (SLgSau < 1)
            {
                Decimal TongTienHoaDon = Convert.ToDecimal(TB_txtTongTien.Text) - Convert.ToDecimal(TB_lvGioHang.SelectedItems[0].SubItems[3].Text);
                TB_lvGioHang.SelectedItems[0].Remove();
                TB_txtTongTien.Text = TongTienHoaDon.ToString();
            }
            else
            {
                Decimal GiaBan = Convert.ToDecimal(TB_lvGioHang.SelectedItems[0].SubItems[2].Text);
                TB_lvGioHang.SelectedItems[0].SubItems[1].Text = SLgSau.ToString();
                TB_lvGioHang.SelectedItems[0].SubItems[3].Text = ((SLgSau * GiaBan).ToString());
                Decimal TongTienHoaDon = Convert.ToDecimal(TB_txtTongTien.Text) - (SLgSau * GiaBan);
                TB_txtTongTien.Text = TongTienHoaDon.ToString();
            }

        }


        #endregion
        #region Event
        private void buttonTimSP_Click(object sender, EventArgs e)
        {
            string loai = TB_cbbLoai.SelectedItem.ToString();
            string thuonghieu = TB_cbbTH.SelectedItem.ToString();
            string gia = TB_cbbGia.SelectedItem.ToString();

            TB_dgvSanPham.DataSource = BLL.BLL_SanPham.Instance.GetSanPham_Views(TB_txtTimSP.Text, loai, thuonghieu, gia);
            SetShow();
            LoadSanPhamView();
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            AddListViewBan();
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            if (TB_lvGioHang.Items.Count != 0 && TB_lvGioHang.SelectedItems.Count != 0)
            {
                DeleteListViewBan();
            }
        }

        private void payButton_Click(object sender, EventArgs e)
        {

            if (khachHang.TenKhachHang != null)
            {
                DateTime date = dateTimePickerNgayBan.Value;
                BLL.BLL_HoaDonBan.Instance.AddHoaDonBan(khachHang.MaKhachHang, date);
                int mhd = 0;
                mhd = BLL.BLL_HoaDonBan.Instance.GetMaHoaDonMax();

                foreach (ListViewItem i in TB_lvGioHang.Items)
                {
                    SanPham_View y = new SanPham_View();
                    y = (SanPham_View)TB_lvGioHang.Items[i.Index].Tag;
                    int slg = 0;
                    slg = Convert.ToInt32(TB_lvGioHang.Items[i.Index].SubItems[1].Text);

                    BLL.BLL_HoaDonBanChiTiet.Instance.AddHoaDonBanChiTiet(mhd, y.MaSanPham, slg, "");
                }
            }

        }

        private void TB_ButtonTimSDT_Click(object sender, EventArgs e)
        {
            khachHang = BLL.BLL_KhachHang.Instance.GetKhachHangBySDT(TB_cbbSDT.Text);
            LoadKhachHang(khachHang);
        }

        private void TB_ButtonKHMoi_Click(object sender, EventArgs e)
        {
            FormKhachHang fKH = new FormKhachHang();
            fKH.ShowDialog();
            int mkhMax = BLL.BLL_KhachHang.Instance.GetMaKhachHangMax();
            khachHang = BLL.BLL_KhachHang.Instance.GetKhachHangByID(mkhMax);
            LoadKhachHang(khachHang);
        }
        #endregion
        #endregion

        // Tab Bảo Hành
        #region TabBaoHanh
        #region Method

        //Load Thông tin sản phẩm
        public void LoadSanPhamView2()
        {
            int id = Convert.ToInt32(TBH_dgvSanPham.CurrentRow.Cells["MaSanPham"].Value.ToString());
            SanPham_View i = BLL.BLL_SanPham.Instance.GetSanPhamByID(id);
            TBH_txtTenSP.Text = i.TenSanPham;
            TBH_txtSLMua.Text = TBH_dgvSanPham.CurrentRow.Cells["SoLuong"].Value.ToString();
            TBH_txtMau.Text = i.MauSac;
            TBH_txtTenTH.Text = i.TenThuongHieu;
            TBH_txtTLBH.Text = i.ThoiLuongBaoHanh.ToString();
            TBH_txtGia.Text = i.GiaBan.ToString();
            TBH_txtLoai.Text = i.TenLoai;
            DateTime HHBH = ((DateTime)TBH_dgvSanPham.CurrentRow.Cells["NgayBan"].Value).AddDays(i.ThoiLuongBaoHanh);
            TBH_txtHHBH.Text = HHBH.ToString();
            ChiTietHoaDonBan y = BLL.BLL_HoaDonBanChiTiet.Instance.GetHoaDonBanChiTiet((int)TBH_dgvSanPham.CurrentRow.Cells["MaHoaDonBanChiTiet"].Value);
            if (y.GhiChu == "")
            {
                TBH_rtxtGhiChu.Text = "- Ngày bảo hành:" + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "\n";
            }
            else
                TBH_rtxtGhiChu.Text = y.GhiChu + "\n- Ngày bảo hành:" + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "\n";

        }

        // Load thông tin khách hàng
        public void LoadKhachHang2(KhachHang i)
        {
            TBH_txtTenKH.Text = i.TenKhachHang;
            TBH_txtSDT.Text = i.SoDienThoai;
            TBH_txtDiaChiKH.Text = i.DiaChi;
        }
        #endregion
        #region Event
        // Tìm Khách hàng bằng số điện thoại
        private void TBH_buttonTimSDT_Click(object sender, EventArgs e)
        {
            khachHang = BLL.BLL_KhachHang.Instance.GetKhachHangBySDT(TBH_txtSDT.Text);
            LoadKhachHang2(khachHang);
            TBH_dgvSanPham.DataSource = DAO.DAO_HoaDonBanChiTiet.Instance.GetSP(khachHang.MaKhachHang);
        }

        // Tìm Hóa đơn theo ngày
        private void TBH_buttonTimHD_Click(object sender, EventArgs e)
        {

            MessageBox.Show(khachHang.TenKhachHang);
            TBH_dgvSanPham.DataSource = BLL.BLL_HoaDonBanChiTiet.Instance.GetSPKhachDaMuaTheoNgay(khachHang.MaKhachHang, TBH_dtpNgayMua.Value);
        }

        // Lưu thông tin bảo hành
        private void saveButton_Click(object sender, EventArgs e)
        {
            int mhdct = Convert.ToInt32(TBH_dgvSanPham.CurrentRow.Cells["MaHoaDonBanChiTiet"].Value.ToString());
            string gc = TBH_rtxtGhiChu.Text;
            BLL.BLL_HoaDonBanChiTiet.Instance.BaoHanhSanPham(mhdct, gc);
        }
        // Hủy thông tin bảo hành
        private void TBH_buttonHuy_Click(object sender, EventArgs e)
        {
            LoadSanPhamView2();
        }

        // Chọn sản phẩm cần bảo hành
        private void TBH_dgvSanPham_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            LoadSanPhamView2();
        }
        #endregion
        #endregion

        //Tab Nhập Hàng
        #region TabNhapHang
        #region Method

        //Load thông tin sản phẩm chi tiết
        public void LoadSanPhamView3()
        {
            TN_txtTenSP.DataBindings.Clear();
            TN_txtTenSP.DataBindings.Add(new Binding("Text", TN_dgvTH.DataSource, "TenSanPham"));

            TN_txtTH.DataBindings.Clear();
            TN_txtTH.DataBindings.Add(new Binding("Text", TN_dgvTH.DataSource, "TenThuongHieu"));

            TN_txtLoai.DataBindings.Clear();
            TN_txtLoai.DataBindings.Add(new Binding("Text", TN_dgvTH.DataSource, "TenLoai"));

            TN_txtMauSac.DataBindings.Clear();
            TN_txtMauSac.DataBindings.Add(new Binding("Text", TN_dgvTH.DataSource, "MauSac"));

            TN_txtBaoHanh.DataBindings.Clear();
            TN_txtBaoHanh.DataBindings.Add(new Binding("Text", TN_dgvTH.DataSource, "ThoiLuongBaoHanh"));

            TN_txtGiaBan.DataBindings.Clear();
            TN_txtGiaBan.DataBindings.Add(new Binding("Text", TN_dgvTH.DataSource, "GiaBan"));

            TN_txtSL.DataBindings.Clear();
            TN_txtSL.DataBindings.Add(new Binding("Text", TN_dgvTH.DataSource, "SoLuongTonKho"));

            TN_rtxtMoTa.DataBindings.Clear();
            TN_rtxtMoTa.DataBindings.Add(new Binding("Text", TN_dgvTH.DataSource, "MoTa"));

            TN_txtGiaNhap.DataBindings.Clear();
            TN_txtGiaNhap.DataBindings.Add(new Binding("Text", TN_dgvTH.DataSource, "GiaNhap"));
        }

        // Thêm sản phẩm vào list view nhập
        public void AddListViewNhap()
        {
            int a = Convert.ToInt32(TN_dgvTH.CurrentRow.Cells["MaSanPham"].Value);
            int index = check(a, TN_listViewNhap);
            SanPham_View i = BLL.BLL_SanPham.Instance.GetSanPhamByID(a);
            decimal TongTienHoaDon = 0;
            int SoLuongNhap = Convert.ToInt32(numericUpDownNhap.Value);

            if (index != -100)
            {

                int SLgSau = 0;

                int SLgTruoc = Convert.ToInt32(TN_listViewNhap.Items[index].SubItems[1].Text);

                SLgSau = SLgTruoc + Convert.ToInt32(numericUpDownNhap.Value);


                Decimal GiaNhap = Convert.ToDecimal(TN_listViewNhap.Items[index].SubItems[2].Text);
                TN_listViewNhap.Items[index].SubItems[1].Text = SLgSau.ToString();
                TN_listViewNhap.Items[index].SubItems[3].Text = ((SLgSau * GiaNhap).ToString());
                TongTienHoaDon = Convert.ToDecimal(TN_txtTongGia.Text) + (SLgSau * GiaNhap);
                TN_txtTongGia.Text = TongTienHoaDon.ToString();
            }
            else
            {
                ListViewItem listView = new ListViewItem(i.TenSanPham);

                listView.Tag = i;

                listView.SubItems.Add(numericUpDownNhap.Value.ToString());
                listView.SubItems.Add(i.GiaBan.ToString());
                listView.SubItems.Add((i.GiaBan * SoLuongNhap).ToString());

                TongTienHoaDon += (Decimal)(i.GiaBan * SoLuongNhap);

                TN_listViewNhap.Items.Add(listView);
                TN_txtTongGia.Text = TongTienHoaDon.ToString();
            }

        }

        // Xóa sản phẩm vào list view nhập
        public void DeleteListViewNhap()
        {
            string tensp = TN_listViewNhap.SelectedItems[0].SubItems[0].Text;
            int SLgSau = 0;
            int SLgTruoc = Convert.ToInt32(TN_listViewNhap.SelectedItems[0].SubItems[1].Text);
            SLgSau = SLgTruoc - Convert.ToInt32(numericUpDownNhap.Value);
            if (SLgSau < 1)
            {
                Decimal TongTienHoaDon = Convert.ToDecimal(TN_txtTongGia.Text) - Convert.ToDecimal(TN_listViewNhap.SelectedItems[0].SubItems[3].Text);
                TN_listViewNhap.SelectedItems[0].Remove();
                TN_txtTongGia.Text = TongTienHoaDon.ToString();
            }
            else
            {
                Decimal GiaBan = Convert.ToDecimal(TN_listViewNhap.SelectedItems[0].SubItems[2].Text);
                TN_listViewNhap.SelectedItems[0].SubItems[1].Text = SLgSau.ToString();
                TN_listViewNhap.SelectedItems[0].SubItems[3].Text = ((SLgSau * GiaBan).ToString());
                Decimal TongTienHoaDon = Convert.ToDecimal(TN_txtTongGia.Text) - (SLgSau * GiaBan);
                TN_txtTongGia.Text = TongTienHoaDon.ToString();
            }

        }
        // Load thông tin thương hiệu
        public void LoadThuongHieu(ThuongHieu i)
        {
            TN_txtMaTH.Text = i.MaThuongHieu.ToString();
            TN_txtTenTH.Text = i.TenThuongHieu;
            TN_txtXuatXu.Text = i.XuatXu;
        }

        #endregion

        #region Event


        #endregion

        #endregion

        private void buttonTimTH_Click(object sender, EventArgs e)
        {
            ThuongHieu i = BLL.BLL_ThuongHieu.Instance.GetThuongHieuByTen(TN_cbbTimTH.Text);
            LoadThuongHieu(i);
            TN_dgvTH.DataSource = BLL.BLL_SanPham.Instance.GetSanPham_Views("All", "All", TN_txtTenTH.Text, "All");
            LoadSanPhamView3();
        }

        private void buttonThemMoi_Click(object sender, EventArgs e)
        {
            FormThuongHieu fTH = new FormThuongHieu();
            fTH.ShowDialog();
            int max = BLL.BLL_ThuongHieu.Instance.GetListThuongHieu().Count;
            ThuongHieu i = BLL.BLL_ThuongHieu.Instance.GetThuongHieuByID(max);
            LoadThuongHieu(i);
        }

        private void buttonXemSP_Click(object sender, EventArgs e)
        {
            TN_dgvTH.DataSource = BLL.BLL_SanPham.Instance.GetSanPham_Views("All", "All", TN_txtTenTH.Text, "All");
            LoadSanPhamView3();
        }

        private void buttonQuayLai_Click(object sender, EventArgs e)
        {
            TN_txtTenTH.Text = "";
            TN_txtXuatXu.Text = "";
            TN_txtMaTH.Text = "";
            TN_dgvTH.DataSource = null;
            TN_txtTongGia.Text = "";
            TN_listViewNhap.Items.Clear();
        }

        private void TN_buttonThem_Click(object sender, EventArgs e)
        {
            AddListViewNhap();
            
        }

        private void TN_buttonXoa_Click(object sender, EventArgs e)
        {
            if(TN_listViewNhap.Items.Count >0)
            DeleteListViewNhap();
        }

        private void TN_buttonThemSPMoi_Click(object sender, EventArgs e)
        {
            FormSanPham fSP = new FormSanPham();
            fSP.ShowDialog();
            buttonXemSP_Click(sender, e);
        }

        private void TN_buttonThanhToan_Click(object sender, EventArgs e)
        {
            
                DateTime date = TN_dtpNhap.Value;
                int MHD = Convert.ToInt32(TN_txtMaTH.Text);
                BLL.BLL_HoaDonNhap.Instance.AddHoaDonNhap(MHD, date);
                int mhd = 0;
                mhd = BLL.BLL_HoaDonNhap.Instance.GetMaHoaDonNhapMax();

                foreach (ListViewItem i in TN_listViewNhap.Items)
                {
                    SanPham_View y = new SanPham_View();
                    y = (SanPham_View)TN_listViewNhap.Items[i.Index].Tag;
                    int slg = 0;
                    slg = Convert.ToInt32(TN_listViewNhap.Items[i.Index].SubItems[1].Text);

                    BLL.BLL_HoaDonNhapChiTiet.Instance.AddHoaDonNhapChiTiet(mhd, y.MaSanPham, slg);
                }
            
        }
    }
}
