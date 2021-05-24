using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyPhuKienDienTu.BLL;
using QuanLyPhuKienDienTu.DTO;

namespace QuanLyPhuKienDienTu.View
{
    public partial class FormSanPham : Form
    {
        private int flagluu = 0;
        public FormSanPham()
        {
            InitializeComponent();
            LoadSanPham();
            SetCBBThuongHieu();
            SetCBBLoai();
        }
        public void LoadSanPham()
        {
            dgvSanPham.DataSource = BLL_SanPham.Instance.GetSanPham_Views("","","","");
            Process.InvisibleAttributes(dgvSanPham, new object[] { "MaSanPham" });
        }
        public void LoadDL()
        { 
            
            dgvSanPham.DataSource = DAO.DAO_SanPham.Instance.GetSanPhamView();
            var list = dgvSanPham.DataSource;
            txtMaSP.DataBindings.Clear();
            txtMaSP.DataBindings.Add("Text", list, "MaSanPham");  
            cbbMaTH.DataBindings.Clear();
            cbbMaTH.DataBindings.Add("Text", list, "TenThuongHieu");
            cbbMaL.DataBindings.Clear();
            cbbMaL.DataBindings.Add("Text", list, "TenLoai");
            txtTenSP.DataBindings.Clear();
            txtTenSP.DataBindings.Add("Text", list, "TenSanPham");
            txtMauSac.DataBindings.Clear();
            txtMauSac.DataBindings.Add("Text", list, "MauSac");
            txtMoTa.DataBindings.Clear();
            txtMoTa.DataBindings.Add("Text", list, "MoTa");
            txtGiaBan.DataBindings.Clear();
            txtGiaBan.DataBindings.Add("Text", list, "GiaBan");
            txtGiaNhap.DataBindings.Clear();
            txtGiaNhap.DataBindings.Add("Text", list, "GiaNhap");
            txtSL.DataBindings.Clear();
            txtSL.DataBindings.Add("Text", list, "SoLuongTonKho");
            txtBaoHanh.DataBindings.Clear();
            txtBaoHanh.DataBindings.Add("Text", list, "ThoiLuongBaoHanh");
            //dtBaoHanh.DataBindings.Add("Value", list, "ThoiLuongBaoHanh");
            //dtBaoHanh.Format = DateTimePickerFormat.Custom;
            // dtBaoHanh.CustomFormat = "MM/dd/yyyy";
           
        }
        public void SetCBBThuongHieu()
        {
            List<ThuongHieu> list = BLL_SanPham.Instance.GetThuongHieu();
            if(cbbMaTH.Items != null)
            {
                cbbMaTH.Items.Clear();
            }
            cbbMaTH.Items.Add(new CBBItem { Value = 0, Text = "All" });
            foreach (ThuongHieu item in list)
            {
                cbbMaTH.Items.Add(new CBBItem
                {
                    Value = item.MaThuongHieu,
                    Text = item.TenThuongHieu
                });
            }
            cbbMaTH.SelectedIndex = 0;
        }
        public void SetCBBLoai()
        {
            List<Loai> data = BLL_SanPham.Instance.GetLoai();
            if(cbbMaL.Items != null)
            {
                cbbMaL.Items.Clear();
            }
            cbbMaL.Items.Add(new CBBItem { Value = 0, Text = "All" });
            foreach (Loai item in data)
            {
                cbbMaL.Items.Add(new CBBItem
                {
                    Value = item.MaLoai,
                    Text = item.TenLoai
                });
            }
            cbbMaL.SelectedIndex = 0;
        }
        public void ClearData() 
        {
            txtMaSP.Text = "";
            SetCBBThuongHieu();
            SetCBBLoai();
            txtTenSP.Text = "";
            txtMauSac.Text = "";
            txtMoTa.Text = "";
            txtGiaBan.Text = "";
            txtGiaNhap.Text = "";
            txtSL.Text = "";
            txtBaoHanh.Text = "";
        }
        public void DisEnl(bool e)
        {
            btnXem.Enabled = !e;
            btnThem.Enabled = !e;
            btnSua.Enabled = !e;
            btnXoa.Enabled = !e;
            btnLuu.Enabled = e;
            btnHuy.Enabled = e;
            txtMaSP.Enabled = e;
            cbbMaTH.Enabled = e;
            cbbMaL.Enabled = e;
            txtTenSP.Enabled = e;
            txtTenSP.Enabled = e;
            txtMauSac.Enabled = e;
            txtMoTa.Enabled = e;
            txtGiaBan.Enabled = e;
            txtGiaNhap.Enabled = e;
            txtSL.Enabled = e;
            txtBaoHanh.Enabled = e;
        }
        

        private void btnXem_Click(object sender, EventArgs e)
        {
            LoadSanPham();
            LoadDL();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaSP.ReadOnly = true; 
            flagluu = 0;
            ClearData(); 
            DisEnl(true);

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            flagluu = 1;
            btnXem.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            txtMaSP.Enabled = false;
            cbbMaTH.Enabled = true;
            cbbMaL.Enabled = true;
            txtTenSP.Enabled = true;
            txtTenSP.Enabled = true;
            txtMauSac.Enabled = true;
            txtMoTa.Enabled = true;
            txtGiaBan.Enabled = true;
            txtGiaNhap.Enabled = true;
            txtSL.Enabled = true;
            txtBaoHanh.Enabled = true;
            //DisEnl(true);
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection r = dgvSanPham.SelectedRows;
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                foreach (DataGridViewRow i in r)
                {
                    int masp = (int)i.Cells["MaSanPham"].Value;
                    if (BLL_SanPham.Instance.XoaSanPham(masp))
                    {
                        MessageBox.Show("Xóa thành công !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Xóa không thành công !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            LoadDL();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(flagluu == 0)
            {
                if(
                    Process.IsEmpty(cbbMaTH.Text) ||
                    Process.IsEmpty(cbbMaL.Text) ||
                    Process.IsEmpty(txtTenSP.Text) ||
                    Process.IsEmpty(txtMauSac.Text) ||
                    Process.IsEmpty(txtMoTa.Text) ||
                    Process.IsEmpty(txtGiaBan.Text) ||
                    Process.IsEmpty(txtGiaNhap.Text) ||
                    Process.IsEmpty(txtSL.Text) ||
                    Process.IsEmpty(txtBaoHanh.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }    
                try
                {
                    SanPham sp = new SanPham()
                    {
                        MaThuongHieu = ((CBBItem)cbbMaTH.SelectedItem).Value,
                        MaLoai = ((CBBItem)cbbMaL.SelectedItem).Value,  
                        TenSanPham = txtTenSP.Text,
                        MauSac = txtMauSac.Text,
                        MoTa = txtMoTa.Text,
                        GiaBan = Convert.ToDecimal(txtGiaBan.Text),
                        GiaNhap = Convert.ToDecimal(txtGiaNhap.Text),
                        SoLuongTonKho = Convert.ToInt32(txtSL.Text),
                        ThoiLuongBaoHanh = Convert.ToInt32(txtBaoHanh.Text)
                    };
                    /*if(Process.IsEmpty(sp.TenSanPham
                     * {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);    
                    }    */
                    if(BLL_SanPham.Instance.ThemSanPham(sp))
                    {
                        MessageBox.Show("Thêm thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        
                        MessageBox.Show("Thêm thất bại!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadDL();
                   // DisEnl(false);
                }
                catch (Exception)
                {
                    return;
                }

            }
            else
            {
                //ham sửa
                SanPham sp = new SanPham
                {
                    MaSanPham = Convert.ToInt32(txtMaSP.Text),
                    MaThuongHieu = ((CBBItem)cbbMaTH.SelectedItem).Value,
                    MaLoai = ((CBBItem)cbbMaL.SelectedItem).Value,
                    TenSanPham = txtTenSP.Text,
                    MauSac = txtMauSac.Text,
                    MoTa = txtMoTa.Text,
                    GiaBan = Convert.ToDecimal(txtGiaBan.Text),
                    GiaNhap = Convert.ToDecimal(txtGiaNhap.Text),
                    SoLuongTonKho = Convert.ToInt32(txtSL.Text),
                    ThoiLuongBaoHanh = Convert.ToInt32(txtBaoHanh.Text)
                };
                int ma = (int)dgvSanPham.SelectedRows[0].Cells["MaSanPham"].Value;
                if (BLL_SanPham.Instance.SuaSanPham(ma, sp))
                {
                     MessageBox.Show("Sửa thành công !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                     MessageBox.Show("Sửa không thành công !", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadDL();
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            LoadDL();
            DisEnl(false);
            return;
        }
        public void getrecord()
        {
            string name = txtTimKiem.Text;
            dgvSanPham.DataSource = BLL_SanPham.Instance.GetSanPhamByName(name);
            var data = dgvSanPham.DataSource;
            txtMaSP.DataBindings.Clear();
            txtMaSP.DataBindings.Add("Text", data, "MaSanPham");
            cbbMaTH.DataBindings.Clear();
            cbbMaTH.DataBindings.Add("Text", data, "TenThuongHieu");
            cbbMaL.DataBindings.Clear();
            cbbMaL.DataBindings.Add("Text", data, "TenLoai");
            txtTenSP.DataBindings.Clear();
            txtTenSP.DataBindings.Add("Text", data, "TenSanPham");
            txtMauSac.DataBindings.Clear();
            txtMauSac.DataBindings.Add("Text", data, "MauSac");
            txtMoTa.DataBindings.Clear();
            txtMoTa.DataBindings.Add("Text", data, "MoTa");
            txtGiaBan.DataBindings.Clear();
            txtGiaBan.DataBindings.Add("Text", data, "GiaBan");
            txtGiaNhap.DataBindings.Clear();
            txtGiaNhap.DataBindings.Add("Text", data, "GiaNhap");
            txtSL.DataBindings.Clear();
            txtSL.DataBindings.Add("Text", data, "SoLuongTonKho");
            txtBaoHanh.DataBindings.Clear();
            txtBaoHanh.DataBindings.Add("Text", data, "ThoiLuongBaoHanh");
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string name = txtTimKiem.Text;
            if(Process.IsEmpty(name))
            {
                MessageBox.Show("Vui lòng nhập thông tin tìm kiếm!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                getrecord();
                //dgvSanPham.DataSource = BLL_SanPham.Instance.GetSanPhamByName(name);
                Process.InvisibleAttributes(dgvSanPham, new object[] { "MaSanPham" });
        }

        private void txtMaSP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show(" Mã sản phẩm chỉ được nhập số!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
            }
        }

        private void txtGiaBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show(" Chỉ được nhập số!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
            }
        }

        private void txtGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("Chỉ được nhập số!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
            }
        }

        private void txtSL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("Chỉ được nhập số!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
            }
        }

        private void txtBaoHanh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show(" Thời lượng bảo hành chỉ được nhập số!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
            }
        }
    }
}

