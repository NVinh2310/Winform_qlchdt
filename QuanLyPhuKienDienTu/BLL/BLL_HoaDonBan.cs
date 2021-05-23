using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhuKienDienTu.BLL
{
    class BLL_HoaDonBan
    {
        private static BLL_HoaDonBan instance;
        public static BLL_HoaDonBan Instance
        {
            get
            {
                if (instance == null)
                    instance = new BLL_HoaDonBan();
                return BLL_HoaDonBan.instance;
            }
            private set { instance = value; }
        }
        public int GetMaHoaDonMax()
        {
            int t = 0;
            foreach(HoaDonBan i in DAO.DAO_HoaDonBan.Instance.GetHoaDonBan())
            {
                t = i.MaHoaDonBan;
            }
            return t;
        }
        public void AddHoaDonBan(int MKH, DateTime NB)
        {
            DAO.DAO_HoaDonBan.Instance.AddHoaDonBan(MKH, NB);
        }
    }
}
