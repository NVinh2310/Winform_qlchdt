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

namespace QuanLyPhuKienDienTu
{
    public partial class FormDangNhap : Form
    {
        public FormDangNhap()
        {
            InitializeComponent();
        }

        public void Reset() {
            textBoxUsername.Text = "";
            textBoxPassword.Text = "";
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;

            if (Process.IsEmpty(username) || Process.IsEmpty(password)) {
                MessageBox.Show("Vui lòng nhập tài khoản trước khi đăng nhập !");
                return;
            }

            try
            {
                //Đăng nhập thành công
                if (BLL_TaiKhoan.Instance.DangNhap(username, password))
                {
                    int state = BLL_NhanVien.Instance.TrangThaiNhanVien(username);

                    switch (state)
                    {
                        case 0:
                            //Đây là nhân viên
                            MainForm form1 = new MainForm(false);
                            form1.del = new MainForm.MyDel(Reset);
                            this.Hide();
                            form1.ShowDialog();
                            this.Show();
                            break;
                        case 1:
                            //Đây là quản lý
                            MainForm form2 = new MainForm(true);
                            form2.del = new MainForm.MyDel(Reset);
                            this.Hide();
                            form2.ShowDialog();
                            this.Show();
                            break;
                        default:
                            break;
                    }
                }
                //Đăng nhập thất bại
                else
                {
                    MessageBox.Show("Đăng nhập thất bại");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi");
            }
        }

        private void buttonChangePassWord_Click(object sender, EventArgs e)
        {
            FormDoiMatKhau form = new FormDoiMatKhau();
            form.del = new FormDoiMatKhau.MyDel(Reset);
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}
