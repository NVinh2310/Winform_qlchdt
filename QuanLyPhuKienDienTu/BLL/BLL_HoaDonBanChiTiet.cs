using QuanLyPhuKienDienTu.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhuKienDienTu.BLL
{
    class BLL_HoaDonBanChiTiet
    {
        private static BLL_HoaDonBanChiTiet instance;
        public static BLL_HoaDonBanChiTiet Instance
        {
            get
            {
                if (instance == null)
                    instance = new BLL_HoaDonBanChiTiet();
                return BLL_HoaDonBanChiTiet.instance;
            }
            private set { instance = value; }
        }
        
        public void AddHoaDonBanChiTiet(int MHD, int MSP, int SL, string GC)
        {
            DAO.DAO_HoaDonBanChiTiet.Instance.AddHoaDonBanChiTiet(MHD, MSP, SL, GC);
        }
        public void BaoHanhSanPham(int MHDCT, string GC)
        {
            DAO.DAO_HoaDonBanChiTiet.Instance.BaoHanhSanPham(MHDCT, GC);
        }
        public ChiTietHoaDonBan GetHoaDonBanChiTiet(int mhdbct)
        {
            return DAO.DAO_HoaDonBanChiTiet.Instance.GetHoaDonBanChiTiet(mhdbct);
        }

        public List<BaoHanh> GetSPKhachDaMua(int idkh)
        {
            
           return DAO.DAO_HoaDonBanChiTiet.Instance.GetSP(idkh);
            
        }
        public List<BaoHanh> GetSPKhachDaMuaTheoNgay(int idkh, DateTime date)
        {

            List<BaoHanh> list = new List<BaoHanh>();
            foreach(BaoHanh i in GetSPKhachDaMua(idkh))
            {
                if(date.DayOfYear == i.NgayBan.DayOfYear && date.Year ==i.NgayBan.Year)
                list.Add(i);
            }
            return list;

        }


    }
}
