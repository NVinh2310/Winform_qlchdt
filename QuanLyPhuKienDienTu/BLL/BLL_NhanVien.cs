using QuanLyPhuKienDienTu.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhuKienDienTu.BLL
{
    class BLL_NhanVien
    {
        private static BLL_NhanVien instance;
        public static BLL_NhanVien Instance
        {
            get
            {
                if (instance == null)
                    instance = new BLL_NhanVien();
                return BLL_NhanVien.instance;
            }
            private set { instance = value; }
        }

        private BLL_NhanVien() { }

        public List<NhanVien> GetNhanVien()
        {
            return DAO_NhanVien.Instance.GetNhanVien();
        }

        public bool XoaNhanVien(int id)
        {
            return DAO_NhanVien.Instance.XoaNhanVien(id);
        }

        public List<NhanVien> GetNhanVienChuaCoTK()
        {
            return DAO_NhanVien.Instance.GetNhanVienChuaCoTK();
        }

        public int TrangThaiNhanVien(string username)
        {
            return DAO_NhanVien.Instance.TrangThaiNhanVien(username);
        }

        public bool ThemNhanVien(NhanVien nhanVien)
        {
            return DAO_NhanVien.Instance.ThemNhanVien(nhanVien);
        }

        public bool CapNhatNhanVien(NhanVien nhanVien)
        {
            return DAO_NhanVien.Instance.CapNhatNhanVien(nhanVien);
        }

        public List<NhanVien> GetNhanVienTheoTen(string name)
        {
            return DAO_NhanVien.Instance.GetNhanVienTheoTen(name);
        }
    }
}
