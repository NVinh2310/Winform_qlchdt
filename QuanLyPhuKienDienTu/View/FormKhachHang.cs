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
    public partial class FormKhachHang : Form
    {
        private int flagluu = 0;
        public FormKhachHang()
        {
            InitializeComponent();
            loadDL();
        }
        
        public void loadDL()
        {
            dgvKhachHang.DataSource = BLL_KhachHang.Instance.GetKhachHang();
            Process.InvisibleAttributes(dgvKhachHang, new object[] { "MaKhachHang" });
            Process.InvisibleAttributes(dgvKhachHang, new object[] { "HoaDonBans" });
            var list = dgvKhachHang.DataSource;
            txtMaKH.DataBindings.Clear();
            txtMaKH.DataBindings.Add("Text", list, "MaKhachHang");
            txtTenKH.DataBindings.Clear();
            txtTenKH.DataBindings.Add("Text", list, "TenKhachHang");
            txtDiaChi.DataBindings.Clear();
            txtDiaChi.DataBindings.Add("Text", list, "DiaChi");
            txtSĐT.DataBindings.Clear();
            txtSĐT.DataBindings.Add("Text", list, "SoDienThoai");
        }
        public void DisEnl(bool e)
        {
            btnXem.Enabled = !e;
            btnThem.Enabled = !e;
            btnSua.Enabled = !e;
            btnXoa.Enabled = !e;
            btnLuu.Enabled = e;
            btnHuy.Enabled = e;
            txtMaKH.Enabled = e;
            txtTenKH.Enabled = e;
            txtDiaChi.Enabled = e;
            txtSĐT.Enabled = e;
        }
        private void clearData()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            txtSĐT.Text = "";
        }
        
        private void btnXem_Click(object sender, EventArgs e)
        {
            int mkh = Convert.ToInt32(dgvKhachHang.CurrentRow.Cells["MaKhachHang"].Value.ToString());
           dgvKhachHang.DataSource= DAO.DAO_HoaDonBanChiTiet.Instance.GetSP(mkh);
            btnHuy.Enabled = true;
            btnXem.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaKH.ReadOnly = true;
            flagluu = 0;
            clearData();
            DisEnl(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            flagluu = 1;
            //txtMaKH.Enabled = false;
            //DisEnl(true);
            btnXem.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled =true;
            txtMaKH.Enabled = false;
            txtTenKH.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSĐT.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection r = dgvKhachHang.SelectedRows;
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dr == DialogResult.Yes)
            {
                /*foreach (DataGridViewRow i in r)
                {*/
                    int makh = (int)dgvKhachHang.SelectedRows[0].Cells["MaKhachHang"].Value;
                if (BLL_KhachHang.Instance.XoaKhachHang(makh))
                {
                    MessageBox.Show("Xóa thành công !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xóa không thành công !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            loadDL();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
           if(flagluu == 0) 
            {
                if( Process.IsEmpty(txtTenKH.Text) || Process.IsEmpty(txtDiaChi.Text) || Process.IsEmpty(txtSĐT.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ các thông tin", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }    
               
                try
                {
                    KhachHang customer = new KhachHang()
                    {
                        
                        TenKhachHang = txtTenKH.Text,
                        DiaChi = txtDiaChi.Text,
                        SoDienThoai = txtSĐT.Text
                    };
                    if (BLL_KhachHang.Instance.ThemKhachHang(customer))
                    {
                        MessageBox.Show("Thêm thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Mã khách hàng bạn thêm vào đã có sẵn !", "Mời bạn nhập lại!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("Thêm thất bại!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                    }
                    loadDL();
                    DisEnl(false);
                }
                catch (Exception)
                {
                    return;
                }
            }
            else
            {
                DataGridViewSelectedRowCollection r = dgvKhachHang.SelectedRows;
                KhachHang kh = new KhachHang
                {
                   MaKhachHang = Convert.ToInt32(txtMaKH.Text),
                    TenKhachHang = txtTenKH.Text,
                    DiaChi = txtDiaChi.Text,
                    SoDienThoai = txtSĐT.Text
                };
                int ma = (int)dgvKhachHang.SelectedRows[0].Cells["MaKhachHang"].Value;
               if (BLL_KhachHang.Instance.SuaKhachHang(ma, kh))
                {
                    MessageBox.Show("Sửa thành công !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                    MessageBox.Show("Sửa không thành công !", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            loadDL();

        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            loadDL();
            DisEnl(false);
            return;
        }
        public void getrecord()
        {
            string name = txtSearch.Text;
            dgvKhachHang.DataSource = BLL_KhachHang.Instance.GetKhachHangByName(name);
            var data = dgvKhachHang.DataSource;
            txtMaKH.DataBindings.Clear();
            txtMaKH.DataBindings.Add("Text", data, "MaKhachHang");
            txtTenKH.DataBindings.Clear();
            txtTenKH.DataBindings.Add("Text", data, "TenKhachHang");
            txtDiaChi.DataBindings.Clear();
            txtDiaChi.DataBindings.Add("Text", data, "DiaChi");
            txtSĐT.DataBindings.Clear();
            txtSĐT.DataBindings.Add("Text", data, "SoDienThoai");
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string name = txtSearch.Text;
            if(Process.IsEmpty(name))
            {
                MessageBox.Show("Vui lòng nhập thông tin tìm kiếm!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            getrecord();
            Process.InvisibleAttributes(dgvKhachHang, new object[] { "MaKhachHang" });
            Process.InvisibleAttributes(dgvKhachHang, new object[] { "HoaDonBans" });
        }

        private void txtSĐT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("Chỉ được nhập số!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
            }
        }

        private void txtMaKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show(" Mã khách hàng chỉ được nhập số!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
            }
        }

        private void btnHuyTim_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            loadDL();
        }
    }
}
