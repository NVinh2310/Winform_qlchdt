using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhuKienDienTu.BLL
{
    class BLL_HoaDonNhapChiTiet
    {
        private static BLL_HoaDonNhapChiTiet instance;
        public static BLL_HoaDonNhapChiTiet Instance
        {
            get
            {
                if (instance == null)
                    instance = new BLL_HoaDonNhapChiTiet();
                return BLL_HoaDonNhapChiTiet.instance;
            }
            private set { instance = value; }
        }
        public void AddHoaDonNhapChiTiet(int MHD, int MSP, int SL)
        {
            DAO.DAO_HoaDonNhapChiTiet.Instance.AddHoaDonNhapChiTiet(MHD, MSP, SL);
        }
    }
}
