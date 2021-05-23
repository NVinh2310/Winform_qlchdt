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
    public partial class FormThuongHieu : Form
    {
        
        private int flagluu = 0;
        public FormThuongHieu()
        {
            InitializeComponent();
            LoadThuongHieu();
            loadDL();
        }
        private void FormThuongHieu_Load(object sender, EventArgs e)
        {
            LoadThuongHieu();
            loadDL();
        }
        public void loadDL()
        {
            dgvThuongHieu.DataSource = BLL.BLL_ThuongHieu.Instance.GetListThuongHieu();
            
            txtMaTH.DataBindings.Clear();
            txtMaTH.DataBindings.Add("Text", dgvThuongHieu.DataSource, "MaThuongHieu");
            txtTenTH.DataBindings.Clear();
            txtTenTH.DataBindings.Add("Text", dgvThuongHieu.DataSource, "TenThuongHieu");
            txtXuatXu.DataBindings.Clear();
            txtXuatXu.DataBindings.Add("Text", dgvThuongHieu.DataSource, "XuatXu");
        }
        public void DisEnl(bool e)
        {
            btnXem.Enabled = !e;
            btnThem.Enabled = !e;
            btnSua.Enabled = !e;
            btnXoa.Enabled = !e;
            btnLuu.Enabled = e;
            btnHuy.Enabled = e;
            txtMaTH.Enabled = e;
            txtTenTH.Enabled = e;
            txtXuatXu.Enabled = e;
        }
        private void clearData()
        {
            txtMaTH.Text = "";
            txtTenTH.Text = "";
            txtXuatXu.Text = "";
        }
        public void LoadThuongHieu()
        {
            dgvThuongHieu.DataSource = BLL_ThuongHieu.Instance.GetListThuongHieu();
            Process.InvisibleAttributes(dgvThuongHieu, new object[] { "MaThuongHieu" });
        }
        private void btnXem_Click(object sender, EventArgs e)
        {
            
            dgvThuongHieu.DataSource = DAO.DAO_SanPham.Instance.GetSanPhamByThuongHieu(Convert.ToInt32(txtMaTH.Text));
            btnHuy.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaTH.ReadOnly = true;
            flagluu = 0;
            clearData();
            DisEnl(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            flagluu = 1;
            //txtMaTH.Enabled = false;
            //DisEnl(true);
            btnXem.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            txtMaTH.Enabled = false;
            txtTenTH.Enabled = true;
            txtXuatXu.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection r = dgvThuongHieu.SelectedRows;
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa Thương hiệu này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                foreach (DataGridViewRow i in r)
                {
                    int maTH = (int)i.Cells["MaThuongHieu"].Value;
                    if (BLL_ThuongHieu.Instance.XoaThuongHieu(maTH))
                    {
                        MessageBox.Show("Xóa thành công !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Xóa THông thành công !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            FormThuongHieu_Load(sender, e);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (flagluu == 0)
            {

                /*if (!BLL_ThuongHieu.Instance.CheckMaTH(TH.MaThuongHieu))
                {
                    MessageBox.Show("Mã THách hàng bạn thêm vào đã có sẵn !", "Mời bạn nhập lại!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }*/
                try
                {
                    ThuongHieu customer = new ThuongHieu()
                    {
                        TenThuongHieu = txtTenTH.Text,
                        XuatXu = txtXuatXu.Text,
                    };
                    if (BLL_ThuongHieu.Instance.ThemThuongHieu(customer))
                    {
                        MessageBox.Show("Thêm thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Thêm thất bại!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    FormThuongHieu_Load(sender, e);
                    DisEnl(false);
                }
                catch (Exception)
                {
                    return;
                }
            }
            else
            {
                DataGridViewSelectedRowCollection r = dgvThuongHieu.SelectedRows;
                ThuongHieu TH = new ThuongHieu
                {
                    MaThuongHieu = Convert.ToInt32(txtMaTH.Text),
                    TenThuongHieu = txtTenTH.Text,
                    XuatXu = txtXuatXu.Text,
                };
                int ma = (int)dgvThuongHieu.SelectedRows[0].Cells["MaThuongHieu"].Value;
                if (BLL_ThuongHieu.Instance.SuaThuongHieu(ma, TH))
                {
                    MessageBox.Show("Sửa thành công !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                    MessageBox.Show("Sửa THông thành công !", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            FormThuongHieu_Load(sender, e);

        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnXem_Click(sender, e);
            DisEnl(false);
            dgvThuongHieu.DataSource = BLL.BLL_ThuongHieu.Instance.GetListThuongHieu();

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string name = txtSearch.Text;
            dgvThuongHieu.DataSource = BLL_ThuongHieu.Instance.GetThuongHieuByName(name);
            Process.InvisibleAttributes(dgvThuongHieu, new object[] { "MaThuongHieu" });
        }

       

        private void txtMaTH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show(" Mã Thương Hiệu chỉ được nhập số!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
            }
        }

        
    }
}

