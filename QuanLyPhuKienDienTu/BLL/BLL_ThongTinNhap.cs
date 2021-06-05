using QuanLyPhuKienDienTu.DAO;
using QuanLyPhuKienDienTu.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhuKienDienTu.BLL
{
    class BLL_ThongTinNhap
    {
        private static BLL_ThongTinNhap instance;
        public static BLL_ThongTinNhap Instance
        {
            get
            {
                if (instance == null)
                    instance = new BLL_ThongTinNhap();
                return BLL_ThongTinNhap.instance;
            }
            private set { instance = value; }
        }

        private BLL_ThongTinNhap() { }

        public List<ThongTinNhap> HoaDonNhap()
        {
            return DAO_ThongTinNhap.Instance.HoaDonNhap();
        }

        public List<ThongTinNhap> TimTheoNgay(DateTime datetime)
        {
            List<ThongTinNhap> result = new List<ThongTinNhap>();
            string formatDate = datetime.ToString("dd-MM-yyyy");

            foreach (ThongTinNhap item in HoaDonNhap())
            {
                if ((item.NgayNhap.DayOfYear == datetime.DayOfYear) &&
                    (item.NgayNhap.Year == datetime.Year))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public List<ThongTinNhap> TimTheoThang(DateTime dateTime)
        {
            List<ThongTinNhap> result = new List<ThongTinNhap>();

            foreach (ThongTinNhap item in HoaDonNhap())
            {
                if (item.NgayNhap.Month == dateTime.Month &&
                    (item.NgayNhap.Year == dateTime.Year))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public List<ThongTinNhap> TimTheoNam(DateTime dateTime)
        {
            List<ThongTinNhap> result = new List<ThongTinNhap>();

            foreach (ThongTinNhap item in HoaDonNhap())
            {
                if (item.NgayNhap.Year == dateTime.Year)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public List<ThongTinNhap> TimTheoTen(string name)
        {
            List<ThongTinNhap> result = new List<ThongTinNhap>();

            foreach (ThongTinNhap item in HoaDonNhap())
            {
                if (item.TenThuongHieu.Contains(name))
                {
                    result.Add(item);
                }
            }

            return result;
        }
        public List<ThongTinNhap> SapXepTheoTen(List<ThongTinNhap> data)
        {
            List<ThongTinNhap> result = data;

            result.Sort(delegate (ThongTinNhap tt1, ThongTinNhap tt2)
            {
                return (tt1.TenThuongHieu.CompareTo(tt2.TenThuongHieu));
            });

            return result;
        }

        public List<ThongTinNhap> SapXepTheoNgay(List<ThongTinNhap> data)
        {
            List<ThongTinNhap> result = data;

            result.Sort(delegate (ThongTinNhap tt1, ThongTinNhap tt2)
            {
                return (tt1.NgayNhap.CompareTo(tt2.NgayNhap));
            });

            return result;
        }

        public List<ThongTinNhap> SapXepTheoSoLuong(List<ThongTinNhap> data)
        {
            List<ThongTinNhap> result = data;

            result.Sort(delegate (ThongTinNhap tt1, ThongTinNhap tt2)
            {
                return (tt1.TongSoLuong.CompareTo(tt2.TongSoLuong));
            });

            return result;
        }

        public List<ThongTinNhap> SapXepTheoGiaBan(List<ThongTinNhap> data)
        {
            List<ThongTinNhap> result = data;

            result.Sort(delegate (ThongTinNhap tt1, ThongTinNhap tt2)
            {
                return (tt1.TongTien.CompareTo(tt2.TongTien));
            });

            return result;
        }

        

        public List<ChiTiet> ThongTinSanPham(int id)
        {
            return DAO_ThongTinNhap.Instance.ThongTinSanPham(id);
        }
    }
}
