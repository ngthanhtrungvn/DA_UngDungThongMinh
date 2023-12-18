using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class NhapKhoDAO
    {
        readonly QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static NhapKhoDAO instances;
        public static NhapKhoDAO Instances
        {
            get
            {
                if (instances == null)
                    instances = new NhapKhoDAO();
                return instances;
            }
            set
            {
                instances = value;
            }
        }
        public dynamic GetNhapKhos(bool isPay)
        {
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues,
                    db.NHAPKHOs);
            if (isPay)
                return Support.ToDataTable<NHAPKHO>((from nk in db.NHAPKHOs where nk.ispay == true select nk).ToList());
            else
                return Support.ToDataTable<NHAPKHO>((from nk in db.NHAPKHOs where nk.ispay == false select nk).ToList());
        }
        public int Insert(int manv)
        {
            try
            {
                db.NHAPKHOs.InsertOnSubmit(new NHAPKHO()
                {
                    ispay = false,
                    MANV = manv,
                    NGAYNHAP = DateTime.Now,
                    tongtien = 0
                });
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }

        }
        public int Delete(int mapn)
        {
            try
            {
                db.NHAPKHOs.DeleteOnSubmit(db.NHAPKHOs.FirstOrDefault(x => x.MAPN == mapn));
                db.SubmitChanges();
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        public int Update(int mapn, bool ispay)
        {
            try
            {
                var nk = db.NHAPKHOs.FirstOrDefault(x => x.MAPN == mapn);
                nk.ispay = ispay;
                db.SubmitChanges();
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        public dynamic LayHDVuaTao()
        {
            return db.NHAPKHOs.OrderByDescending(x => x.MAPN).FirstOrDefault();
        }
        public dynamic FindOrderCode(int code)
        {
            return db.NHAPKHOs.FirstOrDefault(x => x.MAPN == code);
        }
        public bool CheckIsStaffImport(int manv)
        {
            return db.NHAPKHOs.FirstOrDefault(x => x.MANV == manv) != null;
        }
    }
}
