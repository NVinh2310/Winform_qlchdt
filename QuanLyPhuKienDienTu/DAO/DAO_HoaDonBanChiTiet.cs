using QuanLyPhuKienDienTu.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhuKienDienTu.DAO
{
    class DAO_HoaDonBanChiTiet
    {
        private static DAO_HoaDonBanChiTiet instance;
        public static DAO_HoaDonBanChiTiet Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAO_HoaDonBanChiTiet();
                return DAO_HoaDonBanChiTiet.instance;
            }
            private set { instance = value; }
        }
        public void AddHoaDonBanChiTiet(int MHD, int MSP, int SL, string GC)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                ChiTietHoaDonBan i = new ChiTietHoaDonBan()
                {
                    MaHoaDonBan = MHD,
                    MaSanPham = MSP,
                    SoLuongBan = SL,
                    GhiChu = GC
                };
                db.ChiTietHoaDonBans.Add(i);
                db.SaveChanges();
            }
        }

        public void BaoHanhSanPham(int MHDCT, string GC)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                ChiTietHoaDonBan i = db.ChiTietHoaDonBans.Find(MHDCT);
                i.GhiChu = GC;
                db.SaveChanges();
            }    
        }
        // Lấy danh sách sản phẩm khách hàng đã mua 
        public List<BaoHanh> GetSP(int idkh)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                var list = from cthdb in db.ChiTietHoaDonBans
                           join hdb in db.HoaDonBans on cthdb.MaHoaDonBan equals hdb.MaHoaDonBan
                           join sp in db.SanPhams on cthdb.MaSanPham equals sp.MaSanPham
                           join kh in db.KhachHangs on hdb.MaKhachHang equals kh.MaKhachHang
                           where kh.MaKhachHang == idkh

                           select new BaoHanh 
                           {
                               MaHoaDonBanChiTiet = cthdb.MaHoaDonBanChiTiet,
                               MaSanPham = sp.MaSanPham,
                               TenSanPham = sp.TenSanPham,
                               NgayBan = hdb.NgayBan,
                               SoLuong = cthdb.SoLuongBan
                           };
                
                return list.ToList();
            }
        }



        public ChiTietHoaDonBan GetHoaDonBanChiTiet(int mhdbct)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                return db.ChiTietHoaDonBans.Find(mhdbct);
            }
        }


        //public string GetGhiChu(int idkh, )
        //public void UpdateSLgHoaDonBan(int MHD, int MSP, int SL)

    }
}
