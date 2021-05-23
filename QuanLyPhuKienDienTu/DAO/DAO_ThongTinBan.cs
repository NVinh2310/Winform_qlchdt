using QuanLyPhuKienDienTu.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhuKienDienTu.DAO
{
    class DAO_ThongTinBan
    {
        private static DAO_ThongTinBan instance;
        public static DAO_ThongTinBan Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAO_ThongTinBan();
                return DAO_ThongTinBan.instance;
            }
            private set { instance = value; }
        }

        private DAO_ThongTinBan() { }

        public List<ThongTinBan> HoaDonBan()
        {
            List<ThongTinBan> data = new List<ThongTinBan>();

            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                var query = from hdb in db.HoaDonBans
                           join cthdb in db.ChiTietHoaDonBans on hdb.MaHoaDonBan equals cthdb.MaHoaDonBan
                           join kh in db.KhachHangs on hdb.MaKhachHang equals kh.MaKhachHang
                           join sp in db.SanPhams on cthdb.MaSanPham equals sp.MaSanPham
                           group new
                           {
                               TongSoLuong = cthdb.SoLuongBan,
                               TongTien = sp.GiaBan
                           } by new
                           {
                               MaHoaDon = hdb.MaHoaDonBan,
                               TenKhach = kh.TenKhachHang,
                               Ngay = hdb.NgayBan
                           } into g
                           select new
                           {
                               MaHoaDon = g.Key.MaHoaDon,
                               TenKhach = g.Key.TenKhach,
                               Ngay = g.Key.Ngay,
                               TongSoLuong = g.Sum(x => x.TongSoLuong),
                               TongTien = g.Sum(x => x.TongTien)
                           };
                foreach(var item in query.ToList())
                {
                    data.Add(new ThongTinBan()
                    {
                        MaHoaDonBan = item.MaHoaDon,
                        TenKhachHang = item.TenKhach,
                        NgayBan = item.Ngay,
                        TongSoLuong = item.TongSoLuong,
                        TongGiaBan = item.TongTien
                    });
                }
            }
            return data;
        }

        //Lấy thông tin khách hàng bằng mã hóa đơn bán
        public KhachHang ThongTinKhachHang(int id)
        {
            KhachHang khachHang = new KhachHang();

            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                var query = from hoadon in db.HoaDonBans
                            join khachhang in db.KhachHangs on hoadon.MaKhachHang equals khachhang.MaKhachHang
                            where hoadon.MaHoaDonBan == id
                            select new { 
                                TenKhachHang = khachhang.TenKhachHang,
                                DiaChi = khachhang.DiaChi,
                                SoDienThoai = khachhang.SoDienThoai
                            };
                foreach (var item in query)
                {
                    khachHang.TenKhachHang = item.TenKhachHang;
                    khachHang.DiaChi = item.DiaChi;
                    khachHang.SoDienThoai = item.SoDienThoai;
                }
            }

            return khachHang;
        }

        //Lấy thông tin sản phẩm bằng mã hóa đơn bán
        public List<ChiTiet> ThongTinSanPham(int id)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                var data = from cthd in db.ChiTietHoaDonBans
                            join sp in db.SanPhams on cthd.MaSanPham equals sp.MaSanPham
                            where cthd.MaHoaDonBan == id
                            select new ChiTiet
                            {
                                MaSanPham = sp.MaSanPham,
                                TenSanPham = sp.TenSanPham,
                                MauSac = sp.MauSac,
                                SoLuong = cthd.SoLuongBan,
                                Gia = sp.GiaBan
                            };

                return data.ToList();
            }
        }
    }
}
