using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhuKienDienTu.DTO
{
    public class Process
    {
        public static bool IsEmpty(string text)
        {
            if (text == "")
                return true;
            return false;
        }

        public static string ToPhone(string text)
        {
            if (IsEmpty(text))
            {
                Exception e = new Exception("Vui lòng nhập đầy đủ thông tin");
                throw e;
            }
            if (!isNumber(text))
            {
                Exception e = new Exception("So dien thoai khong hop le");
                throw e;
            }
            return text;
        }

        public static bool isNumber(string text)
        {
            foreach (char c in text)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        public static void InvisibleAttributes(DataGridView dataView, object[] parameters = null)
        {
            foreach (string item in parameters)
            {
                dataView.Columns[item].Visible = false;
            }
        }

        public static string FormatDate(DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy");
        }
    }
}
