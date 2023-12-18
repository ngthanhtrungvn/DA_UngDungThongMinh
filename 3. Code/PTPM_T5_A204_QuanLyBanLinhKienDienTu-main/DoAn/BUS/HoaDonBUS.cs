using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class HoaDonBUS
    {
        private static HoaDonBUS instances;
        public static HoaDonBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new HoaDonBUS();
                return instances;
            }
            set
            {
                instances = value;
            }
        }
        public dynamic GetHoaDons(bool isPay = true)
        {
            return HoaDonDAO.Instances.GetHoaDons(isPay);
        }
        public int Insert(int manv, int makh, double giamGia)
        {
            return HoaDonDAO.Instances.Insert(manv, makh, giamGia);
        }
        public int Delete(int mahd)
        {
            return HoaDonDAO.Instances.Delete(mahd);
        }
        public int Update(int mahd, bool ispay)
        {
            return HoaDonDAO.Instances.Update(mahd, ispay);
        }
        public dynamic LayHDVuaTao()
        {
            return HoaDonDAO.Instances.LayHDVuaTao();
        }
        public dynamic FindOrderCode(int code)
        {
            return HoaDonDAO.Instances.FindOrderCode(code);
        }
        public bool CheckIsStaffOrder(int manv)
        {
            return HoaDonDAO.Instances.CheckIsStaffOrder(manv);
        }
        public bool IsCustomer(int makh)
        {
            return HoaDonDAO.Instances.IsCustomer(makh);
        }
    }
}
