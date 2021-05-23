using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhuKienDienTu.DAO
{
    class DAO_HoaDonNhapChiTiet
    {
        private static DAO_HoaDonNhapChiTiet instance;
        public static DAO_HoaDonNhapChiTiet Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAO_HoaDonNhapChiTiet();
                return DAO_HoaDonNhapChiTiet.instance;
            }
            private set { instance = value; }
        }
        public void AddHoaDonNhapChiTiet(int MHD, int MSP, int SL)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                ChiTietHoaDonNhap i = new ChiTietHoaDonNhap()
                {
                    MaHoaDonNhap = MHD,
                    MaSanPham = MSP,
                    SoLuongNhap = SL
                };
                db.ChiTietHoaDonNhaps.Add(i);
                db.SaveChanges();
            }
        }
    }
}
