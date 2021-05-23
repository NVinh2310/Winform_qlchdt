using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhuKienDienTu.DAO
{
    class DAO_KhachHang
    {
        private static DAO_KhachHang instance;
        public static DAO_KhachHang Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAO_KhachHang();
                return DAO_KhachHang.instance;
            }
            private set { instance = value; }
        }
        private DAO_KhachHang()
        { }
        public List<KhachHang> GetKhachHang()
        {
            List<KhachHang> khachhang = new List<KhachHang>();
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                foreach (var item in db.KhachHangs.ToList())
                {
                    khachhang.Add(new KhachHang()
                    {
                        MaKhachHang = item.MaKhachHang,
                        TenKhachHang = item.TenKhachHang,
                        DiaChi = item.DiaChi,
                        SoDienThoai = item.SoDienThoai
                    });

                }
            }
            return khachhang;
        }
        public List<KhachHang> GetListKH(int makh, string name)
        {
            QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities();
            List<KhachHang> data = new List<KhachHang>();
            if (makh == 0)
            {
                var list = db.KhachHangs.Where(p => p.TenKhachHang.Contains(name)).Select(p => p);
                data = list.ToList();
            }
            else
            {
                var list = db.KhachHangs.Where(p => p.MaKhachHang == makh && p.TenKhachHang.Contains(name)).Select(p => p);
                data = list.ToList();
            }
            return data;
        }
        public int GetMaKH(int ma, string name)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                try
                {
                    var data = from u in db.KhachHangs
                               where (u.MaKhachHang == ma && u.TenKhachHang == name)
                               select u;
                    foreach(var i in data)
                    {
                        return i.MaKhachHang;
                    }
                    return -1;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }
        internal int GetListKH(string ma, string name)
        {
            throw new NotImplementedException();
        }

        public List<KhachHang> GetKhachHangByName(string name)
        {
            List<KhachHang> kh = new List<KhachHang>();
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                var data = db.KhachHangs.Where(p => p.TenKhachHang.Contains(name)).ToList();
                foreach(var item in data)
                {
                    kh.Add(new KhachHang()
                    {
                        MaKhachHang = item.MaKhachHang,
                        TenKhachHang = item.TenKhachHang,
                        DiaChi = item.DiaChi,
                        SoDienThoai = item.SoDienThoai
                    });
                }
            }
            return kh;
        }
        public bool ThemKhachHang(KhachHang customer) 
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities()) 
            {
                try
                {
                    if (!CheckMaKH(customer.MaKhachHang))
                    {
                        return false;
                    }
                    else
                    {
                        db.KhachHangs.Add(customer);
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
        public bool CheckMaKH(int makh)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                try
                {
                    if ((db.KhachHangs.Where(p => p.MaKhachHang == makh).Count()) == 0)
                    {
                        return true;
                    }
                    else return false;
                }
                catch(Exception)
                {
                    return false;
                }
            }    
        }
        public bool SuaKhachHang(int makh, KhachHang customer /*string name*/) 
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                try
                {  
                    KhachHang kh = db.KhachHangs.Find(makh);
                    kh.MaKhachHang = makh;
                    kh.TenKhachHang = customer.TenKhachHang;
                    kh.DiaChi = customer.DiaChi;
                    kh.SoDienThoai = customer.SoDienThoai;
                    db.SaveChanges();
                 }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }
        public bool XoaKhachHang(int makh)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                try
                {
                    KhachHang kh = db.KhachHangs.Find(makh);
                    db.KhachHangs.Remove(kh);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }
        public KhachHang GetKhachHangByID(int id)
        {
            using(QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                return db.KhachHangs.Find(id);
            }    
        }
    }
}
