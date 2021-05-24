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

        public KhachHang khachHang2 { get; set; }

        public ThuongHieu thuongHieu { get; set; }

        static decimal TongTienHoaDonBan ;
        static decimal TongTienHoaDonNhap;

        public MainForm()
        {
            InitializeComponent();
            setComboBox();
            show();
               
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
            int t = 1;
            foreach(KhachHang i in BLL.BLL_KhachHang.Instance.GetKhachHang())
            {
                
                TB_cbbSDT.Items.Add(new CBBItem
                {
                    Value = t,
                    Text= i.SoDienThoai
                });
                t++;
                TB_cbbSDT.AutoCompleteCustomSource.Add(i.SoDienThoai);
            }
            t = 1;
            foreach (KhachHang i in BLL.BLL_KhachHang.Instance.GetKhachHang())
            {

                TBH_txtSDT.Items.Add(new CBBItem
                {
                    Value = t,
                    Text = i.SoDienThoai
                });
                t++;
                TBH_txtSDT.AutoCompleteCustomSource.Add(i.SoDienThoai);
            }

            TB_cbbTH.SelectedIndex = 0;
            TB_cbbGia.SelectedIndex = 0;
            
            
        }

        // Load DataGridView trong tab Bán Hàng
        public void show()
        {
            TB_dgvSanPham.DataSource = BLL.BLL_SanPham.Instance.GetSanPham_Views("All", "All", "All", "All");
            SetShow(TB_dgvSanPham);
            LoadSanPhamView();
        }

        // Ẩn các column không cần thiết
        public void SetShow(DataGridView i)
        {
            i.Columns["MoTa"].Visible = false;
            i.Columns["MaSanPham"].Visible = false;
            i.Columns["MauSac"].Visible = false;
            i.Columns["ThoiLuongBaoHanh"].Visible = false;
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
            TB_dgvSanPham.Columns["GiaNhap"].Visible = false;
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

            try
            {
                int MSP = Convert.ToInt32(TB_dgvSanPham.CurrentRow.Cells["MaSanPham"].Value.ToString());
                
                int index = check(MSP, TB_lvGioHang);
                SanPham_View i = BLL.BLL_SanPham.Instance.GetSanPhamByID(MSP);
                
                int SLgThem = Convert.ToInt32(numericSoLuongBan.Value);

                if (index != -100)
                {

                    

                    int SLgTruoc = Convert.ToInt32(TB_lvGioHang.Items[index].SubItems[1].Text);

                    int SLgSau = SLgTruoc + SLgThem;


                    
                    TB_lvGioHang.Items[index].SubItems[1].Text = SLgSau.ToString();
                    TB_lvGioHang.Items[index].SubItems[3].Text = ((SLgSau * i.GiaBan).ToString());

                    TongTienHoaDonBan += (SLgThem * i.GiaBan);
                    TB_txtTongTien.Text = TongTienHoaDonBan.ToString();

                }
                else
                {
                    ListViewItem listView = new ListViewItem(i.TenSanPham);

                    listView.Tag = i;

                    listView.SubItems.Add(numericSoLuongBan.Value.ToString());
                    listView.SubItems.Add(i.GiaBan.ToString());
                    listView.SubItems.Add((i.GiaBan * SLgThem).ToString());

                    TongTienHoaDonBan += (i.GiaBan * SLgThem);

                    TB_lvGioHang.Items.Add(listView);
                    TB_txtTongTien.Text = TongTienHoaDonBan.ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hệ thống chưa có sản phẩm nào, vui lòng thêm sản phẩm mới rồi thử lại ♥♥♥");
            }

        }

        // Xóa sản phẩm khỏi listView Bán
        public void DeleteListViewBan()
        {

           
                string tensp = TB_lvGioHang.SelectedItems[0].SubItems[0].Text;
                int SLgXoa = Convert.ToInt32(numericSoLuongBan.Value);
                int SLgTruoc = Convert.ToInt32(TB_lvGioHang.SelectedItems[0].SubItems[1].Text);
                int SLgSau = SLgTruoc - SLgXoa;

                if (SLgSau < 1)
                {
                    TongTienHoaDonBan  -= Convert.ToDecimal(TB_lvGioHang.SelectedItems[0].SubItems[3].Text);
                    TB_lvGioHang.SelectedItems[0].Remove();
                    TB_txtTongTien.Text = TongTienHoaDonBan.ToString();
                    
                }
                else
                {
                    Decimal GiaBan = Convert.ToDecimal(TB_lvGioHang.SelectedItems[0].SubItems[2].Text);
                    TB_lvGioHang.SelectedItems[0].SubItems[1].Text = SLgSau.ToString();
                    TB_lvGioHang.SelectedItems[0].SubItems[3].Text = ((SLgSau * GiaBan).ToString());
                    TongTienHoaDonBan -= (GiaBan * SLgXoa);
                    TB_txtTongTien.Text = TongTienHoaDonBan.ToString();
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
            SetShow(TB_dgvSanPham);
            LoadSanPhamView();
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            try
            {
                int a = Convert.ToInt32(TB_dgvSanPham.CurrentRow.Cells["MaSanPham"].Value.ToString());
                int index = check(a, TB_lvGioHang);
                int slgtonkho = Convert.ToInt32(TB_dgvSanPham.CurrentRow.Cells["SoLuongTonKho"].Value);


                if (index != -100)
                {
                    int slgban = Convert.ToInt32(TB_lvGioHang.Items[index].SubItems[1].Text);
                    if (slgtonkho >= slgban + numericSoLuongBan.Value)
                    {
                        AddListViewBan();
                    }
                    else
                        MessageBox.Show("Số lượng trong kho ít hơn số lượng sản phẩm được chọn, vui lòng kiểm tra lại!");
                }
                else
                {
                    if (slgtonkho >= numericSoLuongBan.Value)
                    {
                        AddListViewBan();
                    }
                    else
                        MessageBox.Show("Số lượng trong kho ít hơn số lượng sản phẩm được chọn, vui lòng kiểm tra lại!");
                }
            } 
            catch(Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại sau!");
            }
            
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
            try
            {

                if (khachHang != null && TB_lvGioHang.Items.Count != 0)
                {
                    DateTime date = dateTimePickerNgayBan.Value;
                    BLL.BLL_HoaDonBan.Instance.AddHoaDonBan(khachHang.MaKhachHang, date);
                    int mhd = 0;
                    mhd = BLL.BLL_HoaDonBan.Instance.GetMaHoaDonMax();

                    foreach (ListViewItem i in TB_lvGioHang.Items)
                    {
                        SanPham_View y = new SanPham_View();
                        y = (SanPham_View)TB_lvGioHang.Items[i.Index].Tag;
                        
                        int slg = Convert.ToInt32(TB_lvGioHang.Items[i.Index].SubItems[1].Text);
                        int slgSauGiaoDich = y.SoLuongTonKho - slg;

                        BLL.BLL_HoaDonBanChiTiet.Instance.AddHoaDonBanChiTiet(mhd, y.MaSanPham, slg, "");
                        BLL.BLL_SanPham.Instance.SanPhamSauGiaoDich(y.MaSanPham, slgSauGiaoDich);
                        show();
                    }
                    TB_lvGioHang.Items.Clear();

                    MessageBox.Show("     ♥♥♥   \nThanh toán thành công ");
                }
                else
                    MessageBox.Show("       Đã xảy ra lỗi!\n Hãy chọn đầy đủ sản phẩm và khách hàng rồi thử lại ♥♥♥!");
            }
            catch(Exception )
            {
                MessageBox.Show("       Đã xảy ra lỗi!\n Hãy chọn đầy đủ sản phẩm và khách hàng rồi thử lại ♥♥♥!");
            }
            
        }

        private void TB_ButtonTimSDT_Click(object sender, EventArgs e)
        {
            try
            {

                khachHang = BLL.BLL_KhachHang.Instance.GetKhachHangBySDT(TB_cbbSDT.Text);
                if (khachHang != null)
                    LoadKhachHang(khachHang);
                else
                    MessageBox.Show("Khách hàng chưa tồn tại!\nVui lòng thêm mới khách hàng");
            }
            catch(Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại");
            }
        }

        private void TB_ButtonKHMoi_Click(object sender, EventArgs e)
        {
            try
            {
                FormKhachHang fKH = new FormKhachHang();
                fKH.ShowDialog();
                int mkhMax = BLL.BLL_KhachHang.Instance.GetMaKhachHangMax();
                khachHang = BLL.BLL_KhachHang.Instance.GetKhachHangByID(mkhMax);
                LoadKhachHang(khachHang);
            }
            catch(Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại ");
            }
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
            try
            {
                khachHang2 = BLL.BLL_KhachHang.Instance.GetKhachHangBySDT(TBH_txtSDT.Text);
                if (khachHang2!= null)
                {
                    LoadKhachHang2(khachHang2);
                    TBH_dgvSanPham.DataSource = DAO.DAO_HoaDonBanChiTiet.Instance.GetSP(khachHang2.MaKhachHang);
                    TBH_dgvSanPham_CellMouseClick(sender, e);
                }    
                else
                {
                    MessageBox.Show("Khách hàng chưa tồn tại, vui lòng thêm mới ♥♥♥");
                }    
            }
            catch(Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại");
            }
        }

        // Tìm Hóa đơn theo ngày
        private void TBH_buttonTimHD_Click(object sender, EventArgs e)
        {
            try
            {
                if(khachHang2!= null)
                {
                    TBH_dgvSanPham.DataSource = BLL.BLL_HoaDonBanChiTiet.Instance.GetSPKhachDaMuaTheoNgay(khachHang2.MaKhachHang, TBH_dtpNgayMua.Value);
                    TBH_dgvSanPham_CellMouseClick(sender, e);
                }    
                else
                {
                    MessageBox.Show("Vui lòng chọn khách hàng trước! ♥♥♥");
                }    
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại");
            }
        }

        // Lưu thông tin bảo hành
        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                int mhdct = Convert.ToInt32(TBH_dgvSanPham.CurrentRow.Cells["MaHoaDonBanChiTiet"].Value.ToString());
                string gc = TBH_rtxtGhiChu.Text;
                BLL.BLL_HoaDonBanChiTiet.Instance.BaoHanhSanPham(mhdct, gc);
                MessageBox.Show("Đã Lưu thành công");
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại");
            }
            
        }
        // Hủy thông tin bảo hành
        private void TBH_buttonHuy_Click(object sender, EventArgs e)
        {
            LoadSanPhamView2();
        }

        // Chọn sản phẩm cần bảo hành
        private void TBH_dgvSanPham_CellMouseClick(object sender, EventArgs e)
        {
            try
            {
                LoadSanPhamView2();
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại");
            }
            
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
            try
            {
                int a = Convert.ToInt32(TN_dgvTH.CurrentRow.Cells["MaSanPham"].Value);
                int index = check(a, TN_listViewNhap);
                SanPham_View i = BLL.BLL_SanPham.Instance.GetSanPhamByID(a);
                
                int SoLuongNhap = Convert.ToInt32(numericUpDownNhap.Value);

                if (index != -100)
                {

                    

                    int SLgTruoc = Convert.ToInt32(TN_listViewNhap.Items[index].SubItems[1].Text);

                   int SLgSau = SLgTruoc + Convert.ToInt32(numericUpDownNhap.Value);


                    Decimal GiaNhap = Convert.ToDecimal(TN_listViewNhap.Items[index].SubItems[2].Text);
                    TN_listViewNhap.Items[index].SubItems[1].Text = SLgSau.ToString();
                    TN_listViewNhap.Items[index].SubItems[3].Text = ((SLgSau * GiaNhap).ToString());
                    TongTienHoaDonNhap  += (SoLuongNhap * GiaNhap);
                    TN_txtTongGia.Text = TongTienHoaDonNhap.ToString();
                }
                else
                {
                    ListViewItem listView = new ListViewItem(i.TenSanPham);

                    listView.Tag = i;

                    listView.SubItems.Add(numericUpDownNhap.Value.ToString());
                    listView.SubItems.Add(i.GiaNhap.ToString());
                    listView.SubItems.Add((i.GiaNhap * SoLuongNhap).ToString());

                    TongTienHoaDonNhap += (SoLuongNhap * i.GiaNhap);
                    TN_txtTongGia.Text = TongTienHoaDonNhap.ToString();

                    TN_listViewNhap.Items.Add(listView);
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Thương hiệu chưa có sản phẩm nào, vui lòng thêm sản phẩm mới rồi thử lại ♥♥♥");
            }


        }

        // Xóa sản phẩm vào list view nhập
        public void DeleteListViewNhap()
        {
            string tensp = TN_listViewNhap.SelectedItems[0].SubItems[0].Text;
            int SlgXoa = Convert.ToInt32(numericUpDownNhap.Value);
            int SLgTruoc = Convert.ToInt32(TN_listViewNhap.SelectedItems[0].SubItems[1].Text);
            int SLgSau = SLgTruoc - SlgXoa;
            if (SLgSau < 1)
            {
                 TongTienHoaDonNhap -= Convert.ToDecimal(TN_listViewNhap.SelectedItems[0].SubItems[3].Text);
                TN_listViewNhap.SelectedItems[0].Remove();
                TN_txtTongGia.Text = TongTienHoaDonNhap.ToString();
            }
            else
            {
                Decimal GiaBan = Convert.ToDecimal(TN_listViewNhap.SelectedItems[0].SubItems[2].Text);
                TN_listViewNhap.SelectedItems[0].SubItems[1].Text = SLgSau.ToString();
                TN_listViewNhap.SelectedItems[0].SubItems[3].Text = ((SLgSau * GiaBan).ToString());
                TongTienHoaDonNhap -=  (SlgXoa * GiaBan);
                TN_txtTongGia.Text = TongTienHoaDonNhap.ToString();
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
        private void buttonTimTH_Click(object sender, EventArgs e)
        {
            try
            {

                thuongHieu = BLL.BLL_ThuongHieu.Instance.GetThuongHieuByTen(TN_cbbTimTH.Text);
                if (thuongHieu != null)
                {
                    LoadThuongHieu(thuongHieu);
                    TN_dgvTH.DataSource = BLL.BLL_SanPham.Instance.GetSanPham_Views("All", "All", thuongHieu.TenThuongHieu, "All");
                    LoadSanPhamView3();
                    SetShow(TN_dgvTH);
                }    
                else
                {
                    MessageBox.Show("Thương hiệu chưa tồn tại, vui lòng thêm thương hiệu mới rồi thử lại ♥♥♥");
                }    

            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại");
            }
            
        }

        private void buttonThemMoi_Click(object sender, EventArgs e)
        {
            try
            {
                buttonQuayLai_Click(sender, e);
                FormThuongHieu fTH = new FormThuongHieu();
                fTH.ShowDialog();
                int max = BLL.BLL_ThuongHieu.Instance.GetListThuongHieu().Count;
                thuongHieu = BLL.BLL_ThuongHieu.Instance.GetThuongHieuByID(max);;
                if (thuongHieu != null)
                {
                    LoadThuongHieu(thuongHieu);
                    TN_dgvTH.DataSource = BLL.BLL_SanPham.Instance.GetSanPham_Views("All", "All", thuongHieu.TenThuongHieu, "All");
                    LoadSanPhamView3();
                    SetShow(TN_dgvTH);
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi, vui lòng thêm thương hiệu mới rồi thử lại ♥♥♥");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại");
            }
        }

        private void buttonQuayLai_Click(object sender, EventArgs e)
        {
            try
            {
                TN_txtTenTH.Text = "";
                TN_txtXuatXu.Text = "";
                TN_txtMaTH.Text = "";
                TN_dgvTH.DataSource = null;
                TN_txtTongGia.Text = "";
                TN_listViewNhap.Items.Clear();
                thuongHieu = null;
                TongTienHoaDonNhap = 0;

            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại");
            }
        }

        private void TN_buttonThem_Click(object sender, EventArgs e)
        {
            AddListViewNhap();
        }

        private void TN_buttonXoa_Click(object sender, EventArgs e)
        {
            if (TN_listViewNhap.Items.Count > 0&& TN_listViewNhap.SelectedItems.Count >0)
                DeleteListViewNhap();
        }

        private void TN_buttonThemSPMoi_Click(object sender, EventArgs e)
        {
            FormSanPham fSP = new FormSanPham();
            fSP.ShowDialog();
            buttonTimTH_Click(sender, e);
        }

        private void TN_buttonThanhToan_Click(object sender, EventArgs e)
        {
            try 
            {
                if(thuongHieu != null && TN_listViewNhap.Items.Count>0)
                {
                    DateTime date = TN_dtpNhap.Value;
                    int MHD = Convert.ToInt32(thuongHieu.MaThuongHieu);
                    BLL.BLL_HoaDonNhap.Instance.AddHoaDonNhap(MHD, date);
                    int mhd = 0;
                    mhd = BLL.BLL_HoaDonNhap.Instance.GetMaHoaDonNhapMax();

                    foreach (ListViewItem i in TN_listViewNhap.Items)
                    {
                        SanPham_View y = new SanPham_View();
                        y = (SanPham_View)TN_listViewNhap.Items[i.Index].Tag;
                        
                        int slg = Convert.ToInt32(TN_listViewNhap.Items[i.Index].SubItems[1].Text);
                        int slgSauGiaoDich = y.SoLuongTonKho + slg;
                        BLL.BLL_HoaDonNhapChiTiet.Instance.AddHoaDonNhapChiTiet(mhd, y.MaSanPham, slg);
                        BLL.BLL_SanPham.Instance.SanPhamSauGiaoDich(y.MaSanPham, slgSauGiaoDich);
                    }
                    MessageBox.Show("Thanh toán thành công! ♥♥♥");
                    buttonQuayLai_Click(sender, e);
                }    
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi , vui lòng kiểm tra giỏ hàng rồi thử lại");
                }    
            }
            catch(Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi , vui lòng thử lại");
            }
            

        }

        #endregion

        #endregion


    }
}
