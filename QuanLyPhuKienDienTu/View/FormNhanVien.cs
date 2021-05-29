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
    public partial class FormNhanVien : Form
    {
        public delegate void MyDel();
        public MyDel del { get; set; }
        bool addFlag = false;
        bool editFlag = false;
        public FormNhanVien()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            LoadNhanVien();
            AddBinding();
            DisableTextBox();
            TurnOffFlag();
            ReleaseInput();
        }

        private void LoadNhanVien()
        {
            dataViewStaff.DataSource = BLL_NhanVien.Instance.GetNhanVien();
            // Process.InvisibleAttributes(dataViewStaff, new object[] { "IDAccount" });
        }
        private void AddBinding()
        {
            textBoxID.DataBindings.Add("Text", dataViewStaff.DataSource, "MaNhanVien");
            textBoxName.DataBindings.Add("Text", dataViewStaff.DataSource, "TenNhanVien");
            textBoxPhone.DataBindings.Add("Text", dataViewStaff.DataSource, "SoDienThoai");
            textBoxAddress.DataBindings.Add("Text", dataViewStaff.DataSource, "DiaChi");
            cbbState.DataBindings.Add("Text", dataViewStaff.DataSource, "TrangThai");
        }

        private void DisableTextBox()
        {
            textBoxID.Enabled = false;
            textBoxName.Enabled = false;
            textBoxPhone.Enabled = false;
            textBoxAddress.Enabled = false;
            cbbState.Enabled = false;
            saveButton.Enabled = false;
            cancelButton.Enabled = false;
        }

        private void TurnOffFlag()
        {
            addFlag = false;
            editFlag = false;
        }

        private void ClearBinding()
        {
            textBoxID.DataBindings.Clear();
            textBoxName.DataBindings.Clear();
            textBoxPhone.DataBindings.Clear();
            textBoxAddress.DataBindings.Clear();
            cbbState.DataBindings.Clear();
        }
        private void EnableTextBox()
        {
            textBoxName.Enabled = true;
            textBoxPhone.Enabled = true;
            textBoxAddress.Enabled = true;
            cbbState.Enabled = true;
        }
        private void ReleaseInput()
        {
            textBoxID.Text = "";
            textBoxName.Text = "";
            textBoxPhone.Text = "";
            textBoxAddress.Text = "";
        }

        private void SetCBB()
        {
            cbbState.Items.AddRange(new string[] { "Quản lý", "Nhân viên" });
            cbbState.SelectedIndex = 0;
        }

        private void ClearCBB()
        {
            cbbState.Items.Clear();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            addFlag = true;
            saveButton.Enabled = true;
            cancelButton.Enabled = true;

            ClearBinding();
            ClearCBB();
            EnableTextBox();

            ReleaseInput();
            SetCBB();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            editFlag = true;

            saveButton.Enabled = true;
            cancelButton.Enabled = true;

            ClearBinding();
            ClearCBB();
            EnableTextBox();
            SetCBB();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa thông tin này ?", "Cảnh báo",
                   MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {
                ClearBinding();
                DeleteNhanVien();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (addFlag)
                ThemNhanVien();
            if (editFlag)
                UpdateStaff();
        }

        private void DeleteNhanVien()
        {
            try
            {
                int id = Convert.ToInt32(textBoxID.Text);

                bool delAccount = BLL_TaiKhoan.Instance.XoaTaiKhoanVoiIDNhanVien(id);

                if (BLL_NhanVien.Instance.XoaNhanVien(id) && delAccount)
                {
                    MessageBox.Show("Xóa thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void ThemNhanVien()
        {
            try
            {
                string name = textBoxName.Text;
                string phone = Process.ToPhone(textBoxPhone.Text);
                string address = textBoxAddress.Text;
                int state;

                if (Process.IsEmpty(name) || Process.IsEmpty(address))
                {
                    MessageBox.Show("Vui lòng điền thông tin đầy đủ", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (cbbState.Text.Equals("Quản lý"))
                {
                    state = 1;
                }
                else if (cbbState.Text.Equals("Nhân viên"))
                {
                    state = 0;
                }
                else
                {
                    Exception e = new Exception("Thông tin loại nhân viên bị lỗi");
                    throw e;
                }

                NhanVien nhanVien = new NhanVien()
                {
                    TenNhanVien = name,
                    SoDienThoai = phone,
                    DiaChi = address,
                    TrangThai = state
                };

                if (BLL_NhanVien.Instance.ThemNhanVien(nhanVien))
                {
                    MessageBox.Show("Thêm thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException error)
            {
                MessageBox.Show(error.Message);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void UpdateStaff()
        {
            try
            {
                int id = Convert.ToInt32(textBoxID.Text);
                string name = textBoxName.Text;
                string phone = Process.ToPhone(textBoxPhone.Text);
                string address = textBoxAddress.Text;
                int state;

                if (Process.IsEmpty(name) || Process.IsEmpty(address))
                {
                    MessageBox.Show("Vui lòng điền thông tin đầy đủ", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (cbbState.Text.Equals("Quản lý"))
                {
                    state = 1;
                }
                else if (cbbState.Text.Equals("Nhân viên"))
                {
                    state = 0;
                }
                else
                {
                    Exception e = new Exception("Thông tin loại nhân viên bị lỗi");
                    throw e;
                }

                NhanVien nhanVien = new NhanVien()
                {
                    MaNhanVien = id,
                    TenNhanVien = name,
                    SoDienThoai = phone,
                    DiaChi = address,
                    TrangThai = state
                };

                if (BLL_NhanVien.Instance.CapNhatNhanVien(nhanVien))
                {
                    MessageBox.Show("Sửa thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException error)
            {
                MessageBox.Show(error.Message);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            del();
            this.Close();
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            string name = textBoxFindStaff.Text;
            if (name.Equals(""))
            {
                return;
            }
            dataViewStaff.DataSource = BLL_NhanVien.Instance.GetNhanVienTheoTen(name);
        }

        private void cancleButton_Click(object sender, EventArgs e)
        {
            LoadNhanVien();
        }

        private void FormNhanVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            del();
        }
    }
}
