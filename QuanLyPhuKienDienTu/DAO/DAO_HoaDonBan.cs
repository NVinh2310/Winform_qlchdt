using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhuKienDienTu.DAO
{
    class DAO_HoaDonBan
    {
        private static DAO_HoaDonBan instance;
        public static DAO_HoaDonBan Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAO_HoaDonBan();
                return DAO_HoaDonBan.instance;
            }
            private set { instance = value; }
        }
        public List<HoaDonBan> GetHoaDonBan()
        {
            List<HoaDonBan> list = new List<HoaDonBan>();
            using(QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                foreach(HoaDonBan i in db.HoaDonBans.ToList())
                {
                    list.Add(i);
                }
                return list;
            }    
        }
        public void AddHoaDonBan(int MKH, DateTime NB)
        {
            using(QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                HoaDonBan i = new HoaDonBan(){
                    MaKhachHang= MKH,
                    NgayBan = NB
                };
                db.HoaDonBans.Add(i);
                db.SaveChanges();
            }    
        }

    }
}
