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
        public delegate void MyDel();
        public MyDel del { get; set; }
        private int flagluu = 0;
        public FormThuongHieu()
        {
            InitializeComponent();
            
            loadDL();
        }
        private void FormThuongHieu_Load(object sender, EventArgs e)
        {
            
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

            
            dgvThuongHieu.Columns["HoaDonNhaps"].Visible = false;
            dgvThuongHieu.Columns["SanPhams"].Visible = false;

            
                

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
        
        private void btnXem_Click(object sender, EventArgs e)
        {
            try
            {
                groupBox1.Text = "Danh sách sản phẩm của thương hiệu";
                dgvThuongHieu.DataSource = DAO.DAO_SanPham.Instance.GetSanPhamByThuongHieu(Convert.ToInt32(txtMaTH.Text));
                dgvThuongHieu.Columns["MoTa"].Visible = false;
                dgvThuongHieu.Columns["MaSanPham"].Visible = false;
                dgvThuongHieu.Columns["MaThuongHieu"].Visible = false;
                dgvThuongHieu.Columns["MaLoai"].Visible = false;
                dgvThuongHieu.Columns["ChiTietHoaDonBans"].Visible = false;
                dgvThuongHieu.Columns["ChiTietHoaDonNhaps"].Visible = false;
                dgvThuongHieu.Columns["ThuongHieu"].Visible = false;
                dgvThuongHieu.Columns["Loai"].Visible = false;
                btnHuy.Enabled = true;
                btnXoa.Enabled = false;
                btnTimKiem.Enabled = false;
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXem.Enabled = false;
            }
            catch(Exception)
            {
                MessageBox.Show("Lỗi! vui lòng thủ lại");
            }
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
            groupBox1.Text = "Danh sách thuong hiệu";
            loadDL();
            btnTimKiem.Enabled = true;

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string name = txtSearch.Text;
            dgvThuongHieu.DataSource = BLL_ThuongHieu.Instance.GetThuongHieuByName(name);
            Process.InvisibleAttributes(dgvThuongHieu, new object[] { "MaThuongHieu" });
            btnHuy.Enabled = true;
        }

       

        private void txtMaTH_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            
            ThuongHieu th = BLL_ThuongHieu.Instance.GetThuongHieuByID(Convert.ToInt32(txtMaTH.Text));

            List<SanPham_View> sp = BLL_SanPham.Instance.GetSanPham_Views("All", "All",th.TenThuongHieu ,"All");
            if (sp.Count == 0)
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
            } 
            else
            {
                MessageBox.Show("Để Xóa Thương Hiệu Này, Cần xóa Sản phẩm và các hóa đơn có liên quan");
            }    
            loadDL();
        }

        private void FormThuongHieu_FormClosing(object sender, FormClosingEventArgs e)
        {
            del();
        }
    }
}

