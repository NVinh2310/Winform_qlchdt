using QuanLyPhuKienDienTu.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhuKienDienTu.DAO
{
    class DAO_NhanVien
    {
        private static DAO_NhanVien instance;
        public static DAO_NhanVien Instance
        {
            get 
            {
                if (instance == null)
                    instance = new DAO_NhanVien();
                return DAO_NhanVien.instance;
            }
            private set { instance = value; }
        }

        private DAO_NhanVien() { }

        public List<NhanVien> GetNhanVien()
        {
            List<NhanVien> data = new List<NhanVien>();
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                foreach (var item in db.NhanViens.ToList())
                {
                    data.Add(new NhanVien
                    {
                        MaNhanVien = item.MaNhanVien,
                        TenNhanVien = item.TenNhanVien,
                        TrangThai = item.TrangThai,
                        DiaChi = item.DiaChi,
                        SoDienThoai = item.SoDienThoai
                    });
                }
            }
            return data;
        }

        public int TrangThaiNhanVien(string username)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                try
                {
                    var data = from nhanvien in db.NhanViens
                               from taikhoan in db.TaiKhoans
                               where (nhanvien.MaNhanVien == taikhoan.MaNhanVien && 
                                        taikhoan.Username == username)
                               select new
                               {
                                   State = nhanvien.TrangThai
                               };

                    foreach (var item in data)
                    {
                        return item.State;
                    }
                    return -1;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        public List<NhanVien> GetNhanVienChuaCoTK()
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                var data = from nhanvien in db.NhanViens
                           where !(from taikhoan in db.TaiKhoans select taikhoan.MaNhanVien)
                           .Contains(nhanvien.MaNhanVien)
                           select nhanvien;
                return data.ToList();
            } 
        }

        public bool ThemNhanVien(NhanVien nhanVien)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                try
                {
                    db.NhanViens.Add(nhanVien);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public bool CapNhatNhanVien(NhanVien info)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                try
                {
                    NhanVien nhanVien = db.NhanViens.Find(info.MaNhanVien);
                    nhanVien.TenNhanVien = info.TenNhanVien;
                    nhanVien.DiaChi = info.DiaChi;
                    nhanVien.SoDienThoai = info.SoDienThoai;
                    nhanVien.TrangThai = info.TrangThai;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
                return true;
            }
        }

        public bool XoaNhanVien(int id)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                try
                {
                    NhanVien nhanVien = db.NhanViens.Find(id);
                    db.NhanViens.Remove(nhanVien);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public List<NhanVien> GetNhanVienTheoTen(string name)
        {
            List<NhanVien> result = new List<NhanVien>();
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                var data = db.NhanViens.Where(p => p.TenNhanVien.Contains(name)).ToList();
                foreach (var item in data)
                {
                    result.Add(new NhanVien()
                    {
                        MaNhanVien = item.MaNhanVien,
                        TenNhanVien = item.TenNhanVien,
                        DiaChi = item.DiaChi,
                        SoDienThoai = item.SoDienThoai,
                        TrangThai = item.TrangThai
                    });
                }
            }
            return result;
        }
    }
}
