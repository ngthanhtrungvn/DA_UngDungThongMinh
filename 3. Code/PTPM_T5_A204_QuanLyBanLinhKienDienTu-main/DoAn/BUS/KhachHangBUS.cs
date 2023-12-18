using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class KhachHangBUS
    {
        private static KhachHangBUS instances;
        public static KhachHangBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new KhachHangBUS();
                return instances;
            }
            set
            {
                instances = value;
            }
        }
        public dynamic GetKhachHangs()
        {
            return KhachHangDAO.Instances.GetKhachHangs();
        }
        public int Insert(string tenkh, bool gioiTinh, string diaChi, string sdt, int maLoaiKH)
        {
            return KhachHangDAO.Instances.Insert(tenkh, gioiTinh, diaChi, sdt, maLoaiKH);
        }
        public int Update(string tenkh, bool gioiTinh, string diaChi, string sdt, int maloaikh, int maKH)
        {
            return KhachHangDAO.Instances.Update(tenkh, gioiTinh, diaChi, sdt, maloaikh, maKH);
        }
        public int Delete(int maKH)
        {
            return KhachHangDAO.Instances.Delete(maKH);
        }
        public dynamic LayThongTinTheoMa(string maKH)
        {
            return KhachHangDAO.Instances.LayThongTinTheoMa(maKH);
        }
        public bool IsTypeCustomer(int malkh)
        {
            return KhachHangDAO.Instances.IsTypeCustomer(malkh);
        }
    }
}
