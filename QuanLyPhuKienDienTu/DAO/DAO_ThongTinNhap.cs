using QuanLyPhuKienDienTu.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhuKienDienTu.DAO
{
    class DAO_ThongTinNhap
    {
        private static DAO_ThongTinNhap instance;
        public static DAO_ThongTinNhap Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAO_ThongTinNhap();
                return DAO_ThongTinNhap.instance;
            }
            private set { instance = value; }
        }

        private DAO_ThongTinNhap() { }

        public List<ThongTinNhap> HoaDonNhap()
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                var list = from hdn in db.HoaDonNhaps
                            join cthdn in db.ChiTietHoaDonNhaps on hdn.MaHoaDonNhap equals cthdn.MaHoaDonNhap
                            join sp in db.SanPhams on cthdn.MaSanPham equals sp.MaSanPham
                            join th in db.ThuongHieux on hdn.MaThuongHieu equals th.MaThuongHieu
                            group new
                            {
                                TongSoLuong = cthdn.SoLuongNhap,
                                TongTien = sp.GiaNhap
                            } by new
                            {
                                MaHoaDon = hdn.MaHoaDonNhap,
                                TenThuongHieu = th.TenThuongHieu,
                                Ngay = hdn.NgayNhap
                            } into g
                            select new ThongTinNhap
                            {
                                MaHoaDonNhap = g.Key.MaHoaDon,
                                TenThuongHieu = g.Key.TenThuongHieu,
                                NgayNhap = g.Key.Ngay,
                                TongSoLuong = g.Sum(x => x.TongSoLuong),
                                TongTien = Math.Round(g.Sum(x => x.TongTien))
                            };

                return list.ToList();
            }
        }

        

        public List<ChiTiet> ThongTinSanPham(int id)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                var data = from cthd in db.ChiTietHoaDonNhaps
                            join sp in db.SanPhams on cthd.MaSanPham equals sp.MaSanPham
                            where cthd.MaHoaDonNhap == id
                            select new ChiTiet
                            {
                                MaSanPham = sp.MaSanPham,
                                TenSanPham = sp.TenSanPham,
                                MauSac = sp.MauSac,
                                SoLuong = cthd.SoLuongNhap,
                                Gia = sp.GiaNhap
                            };

                return data.ToList();
            }
        }
    }
}
