using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class NhapKhoBUS
    {
        private static NhapKhoBUS instances;
        public static NhapKhoBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new NhapKhoBUS();
                return instances;
            }
            set
            {
                instances = value;
            }
        }
        public dynamic GetNhapKhos(bool isPay = true)
        {
            return NhapKhoDAO.Instances.GetNhapKhos(isPay);
        }
        public int Insert(int manv)
        {
            return NhapKhoDAO.Instances.Insert(manv);
        }
        public int Delete(int mapn)
        {
            return NhapKhoDAO.Instances.Delete(mapn);
        }
        public int Update(int mapn, bool ispay)
        {
            return NhapKhoDAO.Instances.Update(mapn, ispay);
        }
        public dynamic LayHDVuaTao()
        {
            return NhapKhoDAO.Instances.LayHDVuaTao();
        }
        public dynamic FindOrderCode(int code)
        {
            return NhapKhoDAO.Instances.FindOrderCode(code);
        }
        public bool CheckIsStaffImport(int manv)
        {
            return NhapKhoDAO.Instances.CheckIsStaffImport(manv);
        }
    }
}
