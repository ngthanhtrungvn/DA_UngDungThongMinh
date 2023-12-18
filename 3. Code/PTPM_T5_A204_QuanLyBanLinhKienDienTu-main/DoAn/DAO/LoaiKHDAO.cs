using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class LoaiKHDAO
    {
        readonly QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static LoaiKHDAO instances;
        public static LoaiKHDAO Instances
        {
            get
            {
                if (instances == null)
                    instances = new LoaiKHDAO();
                return instances;
            }

            set
            {
                instances = value;
            }
        }
        public dynamic GetLoaiKHs()
        {
            return Support.ToDataTable<LOAIKH>((from lkh in db.LOAIKHs select lkh).ToList());
        }
        public int Insert(string tenLoaiKH, double giamGia)
        {
            try
            {
                db.LOAIKHs.InsertOnSubmit(new LOAIKH()
                {
                    TENLOAIKH = tenLoaiKH,
                    GIAMGIA = giamGia
                });
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }

        }
        public int Update(string tenLoaiKH, double giamGia, int maLKH)
        {
            var lkh = db.LOAIKHs.FirstOrDefault(x => x.MALOAIKH == maLKH);

            try
            {
                if (lkh == null)
                    return -1;
                lkh.TENLOAIKH = tenLoaiKH;
                lkh.GIAMGIA = giamGia;
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {

                return -1;
            }

        }
        public int Delete(int maLoaiKH)
        {
            try
            {
                var lkh = db.LOAIKHs.FirstOrDefault(x => x.MALOAIKH == maLoaiKH);
                if (lkh == null || KhachHangDAO.Instances.IsTypeCustomer(lkh.MALOAIKH))
                    return -1;
                db.LOAIKHs.DeleteOnSubmit(lkh);
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
