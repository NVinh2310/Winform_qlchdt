using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyPhuKienDienTu.DAO;

namespace QuanLyPhuKienDienTu.BLL
{
    class BLL_KhachHang
    {
        private static BLL_KhachHang instance;
        public static BLL_KhachHang Instance
        {
            get
            {
                if (instance == null)
                    instance = new BLL_KhachHang();
                return BLL_KhachHang.instance;
            }
            private set { instance = value; }
        }

        private BLL_KhachHang() { }

        public List<KhachHang> GetKhachHang()
        {
            return DAO_KhachHang.Instance.GetKhachHang();
        }
        public List<KhachHang> GetListKH(int makh, string name)
        {
            return DAO_KhachHang.Instance.GetListKH(makh, name);
        }
        public List<KhachHang> GetKhachHangByName(string name)
        {
            return DAO_KhachHang.Instance.GetKhachHangByName(name);
        }
        public bool ThemKhachHang(KhachHang customer)
        {
            return DAO_KhachHang.Instance.ThemKhachHang(customer);
        }
        public bool CheckMaKH(int makh)
        {
            return DAO_KhachHang.Instance.CheckMaKH(makh);
        }
        public bool SuaKhachHang(int makh, KhachHang customer)
        {
            //int makh = DAO_KhachHang.Instance.GetMaKH(ma,name );
            return DAO_KhachHang.Instance.SuaKhachHang(makh, customer);
        }
        public bool XoaKhachHang(int makh)
        {
            return DAO_KhachHang.Instance.XoaKhachHang(makh);
        }
        public int GetMaKhachHangMax()
        {
            int t = 0;
            foreach (KhachHang i in DAO.DAO_KhachHang.Instance.GetKhachHang())
            {
                t = i.MaKhachHang;
            }
            return t;
        }

        public KhachHang GetKhachHangBySDT(string sdt)
        {
            
            foreach (KhachHang i in DAO.DAO_KhachHang.Instance.GetKhachHang())
            {
                if (String.Compare(sdt, i.SoDienThoai, true) == 0)
                
                return i; 
            }
            return null;
        }

        public KhachHang GetKhachHangByID(int id)
        {
            return DAO.DAO_KhachHang.Instance.GetKhachHangByID(id);
        }
    }
}
