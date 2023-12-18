using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class QuyenDAO
    {
        readonly QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static QuyenDAO instances;
        public static QuyenDAO Instances
        {
            get
            {
                if (instances == null)
                    instances = new QuyenDAO();
                return instances;
            }
            set
            {
                instances = value;
            }
        }

        public dynamic GetQuyens()
        {
            return Support.ToDataTable<QUYEN>(( from q in db.QUYENs select q).ToList());
        }
        public int Insert(string tenQuyen)
        {
            try
            {
                db.QUYENs.InsertOnSubmit(new QUYEN()
                {
                    tenquyen = tenQuyen
                });
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }

        }
        public int Update(string tenQuyen, int maQuyen)
        {
            var q = db.QUYENs.FirstOrDefault(x => x.maquyen == maQuyen);
            try
            {
                if (q == null)
                    return -1;
                q.tenquyen = tenQuyen;
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }

        }
        public int Delete(int maQuyen)
        {
            try
            {
                var q = db.QUYENs.FirstOrDefault(x => x.maquyen == maQuyen);
                if (q == null)
                    return -1;
                db.QUYENs.DeleteOnSubmit(q);
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }

        }
    }
}
