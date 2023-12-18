using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace BUS
{
    public class ChiTietNKBUS
    {
        private static ChiTietNKBUS instances;
        public static ChiTietNKBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new ChiTietNKBUS();
                return instances;
            }
            set { instances = value; }
        }

        public dynamic GetChiTietNKs(int mapn)
        {
            return ChiTietNKDAO.Instances.GetChiTietNKs(mapn);
        }
        public dynamic GetChiTietNKs_(int mapn)
        {
            return ChiTietNKDAO.Instances.GetChiTietNKs_(mapn);
        }
        public dynamic GetChiTietNKs__(int mapn)
        {
            return ChiTietNKDAO.Instances.GetChiTietNKs__(mapn);
        }
    
        public int Insert(int mapn, int malk, int soluong, double donGia)
        {
            return ChiTietNKDAO.Instances.Insert(mapn, malk, soluong, donGia);
        }
        public int Update(int mapn, int malk, int soluong, double donGia)
        {
            return ChiTietNKDAO.Instances.Update(mapn, malk, soluong, donGia);
        }
        public int Delete(int mapn, int malk)
        {
            return ChiTietNKDAO.Instances.Delete(mapn, malk);
        }
        public bool IsProduct(int malinhkien)
        {
            return ChiTietNKDAO.Instances.IsProduct(malinhkien);
        }
    }
}
