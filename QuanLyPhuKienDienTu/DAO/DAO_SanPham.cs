using QuanLyPhuKienDienTu.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhuKienDienTu.DAO
{
    class DAO_SanPham
    {
        private static DAO_SanPham instance;
        public static DAO_SanPham Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAO_SanPham();
                return DAO_SanPham.instance;
            }
            private set { instance = value; }
        }
        private DAO_SanPham()
        {

        }
        public List<SanPham> GetSanPham()
        {
            List<SanPham> sp = new List<SanPham>();
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                foreach (var item in db.SanPhams.ToList())
                {
                    sp.Add(new SanPham()
                    {
                        MaSanPham = item.MaSanPham,
                        MaThuongHieu = item.MaThuongHieu,
                        MaLoai = item.MaLoai,
                        TenSanPham = item.TenSanPham,
                        MauSac = item.MauSac,
                        MoTa = item.MoTa,
                        GiaBan = item.GiaBan,
                        GiaNhap = item.GiaNhap,
                        SoLuongTonKho = item.SoLuongTonKho,
                        ThoiLuongBaoHanh = item.ThoiLuongBaoHanh
                    });
                }
            }
            return sp;
        }
        // tra ve danh sach tat ca cac san pham theo masp va namesp
        public List<SanPham> GetListSP(int masp, string namesp)
        {
            List<SanPham> data = new List<SanPham>();
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                if(masp == 0)
                {
                    var list = db.SanPhams.Where(p => p.TenSanPham.Contains(namesp)).Select(p => p);
                    data = list.ToList();
                }
                else
                {
                    var list = db.SanPhams.Where(p => p.MaSanPham == masp && p.TenSanPham.Contains(namesp)).Select(p => p);
                    data = list.ToList();
                }
            }
            return data;
        }
        public List<SanPham_View> GetSanPhamByName(string name)
        {
            List< SanPham_View> sp = new List<SanPham_View>();
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                var data = db.SanPhams.Where(p => p.TenSanPham.Contains(name)).ToList();
                foreach (var item in data)
                {
                    sp.Add(new SanPham_View()
                    {
                        MaSanPham = item.MaSanPham,
                        TenThuongHieu = item.ThuongHieu.TenThuongHieu,
                        TenLoai = item.Loai.TenLoai,
                        TenSanPham = item.TenSanPham,
                        MauSac = item.MauSac,
                        MoTa = item.MoTa,
                        GiaBan = item.GiaBan,
                        GiaNhap = item.GiaNhap,
                        SoLuongTonKho = item.SoLuongTonKho,
                        ThoiLuongBaoHanh = item.ThoiLuongBaoHanh
                    });
                }
            }
            return sp;
        }
        public List<SanPham> GetSanPhamByThuongHieu(int maTH)
        {
            List<SanPham> sp = new List<SanPham>();
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                var data = db.SanPhams.Where(p => p.MaThuongHieu == maTH).ToList();
                foreach (var item in data)
                {
                    sp.Add(new SanPham()
                    {
                        MaSanPham = item.MaSanPham,
                        MaThuongHieu = item.MaThuongHieu,
                        MaLoai = item.MaLoai,
                        TenSanPham = item.TenSanPham,
                        MauSac = item.MauSac,
                        MoTa = item.MoTa,
                        GiaBan = item.GiaBan,
                        GiaNhap = item.GiaNhap,
                        SoLuongTonKho = item.SoLuongTonKho,
                        ThoiLuongBaoHanh = item.ThoiLuongBaoHanh
                    });
                }
            }
            return sp;

        }
        public bool CheckMaSP(int masp)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                try
                {
                    if ((db.SanPhams.Where(p => p.MaSanPham == masp).Count()) == 0)
                    {
                        return true;
                    }
                    else return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public bool ThemSanPham(SanPham s)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                try
                {
                    if (!CheckMaSP(s.MaSanPham))
                    {
                        return false;
                    }
                    else
                    {
                        db.SanPhams.Add(s);
                        db.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }
        public bool SuaSanPham(int masp, SanPham product) 
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                try
                {
                    SanPham sp = db.SanPhams.Find(masp);
                    sp.MaSanPham = masp;
                    sp.TenSanPham = product.TenSanPham;
                    sp.MaThuongHieu = product.MaThuongHieu;
                    sp.MaLoai = product.MaLoai;
                    sp.MauSac = product.MauSac;
                    sp.MoTa = product.MoTa;
                    sp.GiaBan = product.GiaBan;
                    sp.GiaNhap = product.GiaNhap;
                    sp.SoLuongTonKho = product.SoLuongTonKho;
                    sp.ThoiLuongBaoHanh = product.ThoiLuongBaoHanh;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public bool XoaSanPham(int masp)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                try
                {
                    SanPham sp = db.SanPhams.Find(masp);
                    db.SanPhams.Remove(sp);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }
        public List<SanPham_View> GetSanPhamView()
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                var list = (from i in db.SanPhams select new SanPham_View 
                { 
                    MaSanPham =i.MaSanPham,
                    TenSanPham = i.TenSanPham,
                    TenLoai=i.Loai.TenLoai, 
                    TenThuongHieu =i.ThuongHieu.TenThuongHieu,
                    SoLuongTonKho = i.SoLuongTonKho,
                    MauSac =i.MauSac,
                    MoTa =i.MoTa,
                    ThoiLuongBaoHanh=i.ThoiLuongBaoHanh,
                    GiaBan= i.GiaBan,
                    GiaNhap = i.GiaNhap
                });
                return list.ToList();
            }
        }

        public bool SanPhamSauGiaoDich(int masp, int slgSauGiaoDich)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                try
                {
                    SanPham sp = db.SanPhams.Find(masp);
                    sp.SoLuongTonKho = slgSauGiaoDich;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }
        public List<SanPham_View> GetSanPhamViewByThuongHieu(int maTH)
        {
            List<SanPham_View> sp = new List<SanPham_View>();
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                var data = db.SanPhams.Where(p => p.MaThuongHieu == maTH).ToList();
                foreach (var item in data)
                {
                    sp.Add(new SanPham_View()
                    {
                        MaSanPham = item.MaSanPham,
                        TenThuongHieu = item.ThuongHieu.TenThuongHieu,
                        TenLoai = item.Loai.TenLoai,
                        TenSanPham = item.TenSanPham,
                        MauSac = item.MauSac,
                        MoTa = item.MoTa,
                        GiaBan = item.GiaBan,
                        GiaNhap = item.GiaNhap,
                        SoLuongTonKho = item.SoLuongTonKho,
                        ThoiLuongBaoHanh = item.ThoiLuongBaoHanh
                    });
                }
            }
            return sp;

        }

    }
}
