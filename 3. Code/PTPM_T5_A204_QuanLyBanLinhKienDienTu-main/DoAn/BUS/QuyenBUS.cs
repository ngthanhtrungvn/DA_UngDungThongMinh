using DAO;
using DevExpress.Xpo;
using System;

namespace BUS
{
    public class QuyenBUS 
    {
        private static QuyenBUS instances;
        public static QuyenBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new QuyenBUS();
                return instances;
            }
            set
            {
                instances = value;
            }
        }
        public dynamic GetQuyens()
        {
            return QuyenDAO.Instances.GetQuyens();
        }
        public int Insert(string tenQuyen)
        {
            return QuyenDAO.Instances.Insert(tenQuyen);
        }
        public int Update(string tenQuyen, int maQuyen)
        {
            return QuyenDAO.Instances.Update(tenQuyen, maQuyen);
        }
        public int Delete(int maQuyen)
        {
            return QuyenDAO.Instances.Delete(maQuyen);
        }
    }

}