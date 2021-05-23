using QuanLyPhuKienDienTu.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhuKienDienTu.BLL
{
    class BLL_ThuongHieu
    {
        private static BLL_ThuongHieu instance;
        public static BLL_ThuongHieu Instance
        {
            get
            {
                if (instance == null)
                    instance = new BLL_ThuongHieu();
                return BLL_ThuongHieu.instance;
            }
            private set { instance = value; }
        }
        public List<ThuongHieu> GetListThuongHieu()
        {
            return DAO.DAO_ThuongHieu.Instance.GetListThuongHieu();
        }

        public List<ThuongHieu> GetListTH(int maTH, string name)
        {
            return DAO_ThuongHieu.Instance.GetListTH(maTH, name);
        }
        public List<ThuongHieu> GetThuongHieuByName(string name)
        {
            return DAO_ThuongHieu.Instance.GetThuongHieuByName(name);
        }
        public bool ThemThuongHieu(ThuongHieu customer)
        {
            return DAO_ThuongHieu.Instance.ThemThuongHieu(customer);
        }
        public bool CheckMaTH(int maTH)
        {
            return DAO_ThuongHieu.Instance.CheckMaTH(maTH);
        }
        public bool SuaThuongHieu(int maTH, ThuongHieu customer)
        {
            //int maTH = DAO_ThuongHieu.Instance.GetMaTH(ma,name );
            return DAO_ThuongHieu.Instance.SuaThuongHieu(maTH, customer);
        }
        public bool XoaThuongHieu(int maTH)
        {
            return DAO_ThuongHieu.Instance.XoaThuongHieu(maTH);
        }
        public int GetMaThuongHieuMax()
        {
            int t = 0;
            foreach (ThuongHieu i in BLL_ThuongHieu.Instance.GetListThuongHieu())
            {
                t = i.MaThuongHieu;
            }
            return t;
        }

        public string GetTenThuongHieuByID(int MaThuongHieu)
        {
            return DAO.DAO_ThuongHieu.Instance.GetThuongHieu(MaThuongHieu).TenThuongHieu;
        }
        public ThuongHieu GetThuongHieuByID(int MaThuongHieu)
        {
            foreach(ThuongHieu i in BLL.BLL_ThuongHieu.Instance.GetListThuongHieu())
            {
                if (i.MaThuongHieu == MaThuongHieu)
                    return i;
            }    return new ThuongHieu();
        }
        public ThuongHieu GetThuongHieuByTen(string th)
        {
            foreach (ThuongHieu i in BLL.BLL_ThuongHieu.Instance.GetListThuongHieu())
            {
                if (String.Compare(i.TenThuongHieu, th)==0)
                    return i;
            }
            return new ThuongHieu();
        }

    }
}
