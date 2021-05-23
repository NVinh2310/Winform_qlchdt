using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhuKienDienTu.BLL
{
    class BLL_HoaDonNhap
    {
        private static BLL_HoaDonNhap instance;
        public static BLL_HoaDonNhap Instance
        {
            get
            {
                if (instance == null)
                    instance = new BLL_HoaDonNhap();
                return BLL_HoaDonNhap.instance;
            }
            private set { instance = value; }
        }
        public void AddHoaDonNhap(int MTH,DateTime NN)
        {
            DAO.DAO_HoaDonNhap.Instance.AddHoaDonNhap(MTH, NN);
        }
        public int GetMaHoaDonNhapMax()
        {
            int t = 0;
            foreach (HoaDonNhap i in DAO.DAO_HoaDonNhap.Instance.GetHoaDonNhap())
            {
                t = i.MaHoaDonNhap;
            }
            return t;
        }
    }
}
