using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhuKienDienTu.DAO
{
    class DAO_HoaDonNhap
    {
        private static DAO_HoaDonNhap instance;
        public static DAO_HoaDonNhap Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAO_HoaDonNhap();
                return DAO_HoaDonNhap.instance;
            }
            private set { instance = value; }
        }
        public void AddHoaDonNhap(int MTH, DateTime NN)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                HoaDonNhap i = new HoaDonNhap()
                {
                    MaThuongHieu = MTH,
                    NgayNhap = NN
                };
                db.HoaDonNhaps.Add(i);
                db.SaveChanges();
            }
        }
        public List<HoaDonNhap> GetHoaDonNhap()
        {
            List<HoaDonNhap> list = new List<HoaDonNhap>();
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                foreach (HoaDonNhap i in db.HoaDonNhaps.ToList())
                {
                    list.Add(i);
                }
                return list;
            }
        }
    }
}
