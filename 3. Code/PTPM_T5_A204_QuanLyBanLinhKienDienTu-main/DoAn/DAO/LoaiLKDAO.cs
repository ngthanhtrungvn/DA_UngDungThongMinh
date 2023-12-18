using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class LoaiLKDAO
    {
        readonly QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static LoaiLKDAO instances;
        public static LoaiLKDAO Instances
        {
            get
            {
                if (instances == null)
                    instances = new LoaiLKDAO();
                return instances;
            }
            set { instances = value; }
        }
        public dynamic GetLoaiLKs()
        {
            return Support.ToDataTable<LOAILINHKIEN>( (from llk in db.LOAILINHKIENs select llk).ToList());
        }
        public int Insert(string tenLoai)
        {
            try
            {
                db.LOAILINHKIENs.InsertOnSubmit(new LOAILINHKIEN()
                {
                    TENLOAI = tenLoai
                });
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public int Update(string tenLoai, int maLoai)
        {
            var llk = db.LOAILINHKIENs.FirstOrDefault(x => x.MALOAI == maLoai);
            try
            {
                if (llk == null)
                    return -1;
                llk.TENLOAI = tenLoai;
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public int Delete(int maLoai)
        {
            try
            {
                var llk = db.LOAILINHKIENs.FirstOrDefault(x => x.MALOAI == maLoai);
                if (llk == null || LinhKienDAO.Instances.IsTypeProduct(llk.MALOAI))
                    return -1;
                db.LOAILINHKIENs.DeleteOnSubmit(llk);
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
