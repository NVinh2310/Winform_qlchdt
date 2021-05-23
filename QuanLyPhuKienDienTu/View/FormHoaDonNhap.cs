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
    public partial class FormHoaDonNhap : Form
    {
        public FormHoaDonNhap()
        {
            InitializeComponent();
            LoadData();
            SetCBB();
        }

        private void LoadData()
        {
            dataGridView.DataSource = BLL_ThongTinNhap.Instance.HoaDonNhap();
            Process.InvisibleAttributes(dataGridView, new object[] { "MaHoaDonNhap" });
        }

        private void SetCBB()
        {
            cbbDate.Items.AddRange(new string[] { "Ngày", "Tháng", "Năm" });
            cbbDate.SelectedIndex = 0;

            cbbSort.Items.AddRange(new string[] { "Tên", "Ngày", "Số lượng", "Giá bán" });
            cbbSort.SelectedIndex = 0;
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnSearchDate_Click(object sender, EventArgs e)
        {
            DateTime dateTime = dateTimePicker1.Value;
            string type = cbbDate.Text;

            switch (type)
            {
                case "Ngày":
                    dataGridView.DataSource = BLL_ThongTinNhap.Instance.TimTheoNgay(dateTime);
                    break;
                case "Tháng":
                    dataGridView.DataSource = BLL_ThongTinNhap.Instance.TimTheoThang(dateTime);
                    break;
                case "Năm":
                    dataGridView.DataSource = BLL_ThongTinNhap.Instance.TimTheoNam(dateTime);
                    break;
                default:
                    MessageBox.Show("Không hợp lệ");
                    break;
            }
        }

        private void btnSearchName_Click(object sender, EventArgs e)
        {
            string name = txbName.Text;

            if (Process.IsEmpty(name))
            {
                MessageBox.Show("Vui lòng nhập tên muốn tìm");
                return;
            }

            dataGridView.DataSource = BLL_ThongTinNhap.Instance.TimTheoTen(name);
            Process.InvisibleAttributes(dataGridView, new object[] { "MaHoaDonNhap" });
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            List<ThongTinNhap> list = new List<ThongTinNhap>();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                ThongTinNhap info = row.DataBoundItem as ThongTinNhap;
                list.Add(info);
            }

            string type = cbbSort.Text;
            switch (type)
            {
                case "Tên":
                    dataGridView.DataSource = BLL_ThongTinNhap.Instance.SapXepTheoTen(list);
                    break;
                case "Ngày":
                    dataGridView.DataSource = BLL_ThongTinNhap.Instance.SapXepTheoNgay(list);
                    break;
                case "Số lượng":
                    dataGridView.DataSource = BLL_ThongTinNhap.Instance.SapXepTheoSoLuong(list);
                    break;
                case "Giá bán":
                    dataGridView.DataSource = BLL_ThongTinNhap.Instance.SapXepTheoGiaBan(list);
                    break;
                default:
                    MessageBox.Show("Không hợp lệ");
                    break;
            }
        }

        private void dataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int idHoaDon = (int)dataGridView.SelectedRows[0].Cells["MaHoaDonNhap"].Value;

            FormChiTietNhap form = new FormChiTietNhap(idHoaDon);
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
