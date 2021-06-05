using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhuKienDienTu.DAO
{
    class DAO_ThuongHieu
    {
        private static DAO_ThuongHieu instance;
        public static DAO_ThuongHieu Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAO_ThuongHieu();
                return DAO_ThuongHieu.instance;
            }
            private set { instance = value; }
        }
        public List<ThuongHieu> GetListThuongHieu()
        {
            List<ThuongHieu> loai = new List<ThuongHieu>();
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                foreach(ThuongHieu i in db.ThuongHieux.ToList())
                {
                    loai.Add(new ThuongHieu()
                    {
                        MaThuongHieu =i.MaThuongHieu,
                        TenThuongHieu =i.TenThuongHieu,
                        XuatXu = i.XuatXu
                    });
                }
                return loai;
            }
        }
        public List<ThuongHieu> GetListTH(int maTH, string name)
        {
            QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities();
            List<ThuongHieu> data = new List<ThuongHieu>();
            if (maTH == 0)
            {
                var list = db.ThuongHieux.Where(p => p.TenThuongHieu.Contains(name)).Select(p => p);
                data = list.ToList();
            }
            else
            {
                var list = db.ThuongHieux.Where(p => p.MaThuongHieu == maTH && p.TenThuongHieu.Contains(name)).Select(p => p);
                data = list.ToList();
            }
            return data;
        }
        public int GetMaTH(int ma, string name)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                try
                {
                    var data = from u in db.ThuongHieux
                               where (u.MaThuongHieu == ma && u.TenThuongHieu == name)
                               select u;
                    foreach (var i in data)
                    {
                        return i.MaThuongHieu;
                    }
                    return -1;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        public ThuongHieu ThongTinThuongHieu(int id)
        {
            ThuongHieu thuongHieu = new ThuongHieu();
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                var query = from hoadon in db.HoaDonNhaps
                            join thuonghieu in db.ThuongHieux on hoadon.MaThuongHieu equals thuonghieu.MaThuongHieu
                            where hoadon.MaHoaDonNhap == id
                            select new
                            {
                                TenThuongHieu = thuonghieu.TenThuongHieu,
                                XuatXu = thuonghieu.XuatXu
                            };

                foreach (var item in query)
                {
                    thuongHieu.TenThuongHieu = item.TenThuongHieu;
                    thuongHieu.XuatXu = item.XuatXu;
                }
            }
            return thuongHieu;
        }

        internal int GetListTH(string ma, string name)
        {
            throw new NotImplementedException();
        }

        public List<ThuongHieu> GetThuongHieuByName(string name)
        {
            List<ThuongHieu> TH = new List<ThuongHieu>();
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                var data = db.ThuongHieux.Where(p => p.TenThuongHieu.Contains(name)).ToList();
                foreach (var item in data)
                {
                    TH.Add(new ThuongHieu()
                    {
                        MaThuongHieu = item.MaThuongHieu,
                        TenThuongHieu = item.TenThuongHieu,
                        XuatXu =item.XuatXu
                    });
                }
            }
            return TH;
        }
        public bool ThemThuongHieu(ThuongHieu customer)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                try
                {
                    if (!CheckMaTH(customer.MaThuongHieu))
                    {
                        return false;
                    }
                    else
                    {
                        db.ThuongHieux.Add(customer);
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
        public bool CheckMaTH(int maTH)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                try
                {
                    if ((db.ThuongHieux.Where(p => p.MaThuongHieu == maTH).Count()) == 0)
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
        public bool SuaThuongHieu(int maTH, ThuongHieu customer /*string name*/)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                try
                {
                    ThuongHieu TH = db.ThuongHieux.Find(maTH);
                    TH.TenThuongHieu = customer.TenThuongHieu;
                    TH.XuatXu = customer.XuatXu;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }
        public bool XoaThuongHieu(int maTH)
        {
            using (QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                try
                {
                    ThuongHieu TH = db.ThuongHieux.Find(maTH);
                    db.ThuongHieux.Remove(TH);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }
        public ThuongHieu GetThuongHieu(int MaThuongHieu)
        {
            using(QuanLyPhuKienDienTuEntities db = new QuanLyPhuKienDienTuEntities())
            {
                return db.ThuongHieux.Find(MaThuongHieu);
            }    
        }
    }
}
