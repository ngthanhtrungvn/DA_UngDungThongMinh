using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class LoaiKHBUS
    {
        private static LoaiKHBUS instances;
        public static LoaiKHBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new LoaiKHBUS();
                return instances;
            }

            set
            {
                instances = value;
            }
        }
        public dynamic GetLoaiKHs()
        {
            return LoaiKHDAO.Instances.GetLoaiKHs();
        }
        public int Insert(string tenLoaiKH, double giamGia)
        {
            return LoaiKHDAO.Instances.Insert(tenLoaiKH, giamGia);
        }
        public int Update(string tenLoaiKH, double giamGia, int maLKH)
        {
            return LoaiKHDAO.Instances.Update(tenLoaiKH, giamGia, maLKH);
        }
        public int Delete(int maLoaiKH)
        {
            return LoaiKHDAO.Instances.Delete(maLoaiKH);
        }
    }
}
