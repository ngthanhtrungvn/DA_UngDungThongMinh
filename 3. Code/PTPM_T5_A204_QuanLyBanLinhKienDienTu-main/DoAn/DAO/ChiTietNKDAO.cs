using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ChiTietNKDAO
    {
        readonly QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static ChiTietNKDAO instances;
        public static ChiTietNKDAO Instances
        {
            get
            {
                if (instances == null)
                    instances = new ChiTietNKDAO();
                return instances;
            }
            set { instances = value; }
        }

        public dynamic GetChiTietNKs(int mapn)
        {
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues,
                   db.CHITIETNKs);
            return Support.ToDataTable < CHITIETNK >( (from ctnk in db.CHITIETNKs where ctnk.MAPN == mapn select ctnk).ToList());
        }
        public dynamic GetChiTietNKs_(int mapn)
        {
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues,
                   db.CHITIETHDs);
            return (from ctnk in db.CHITIETNKs where ctnk.MAPN == mapn select ctnk).ToList();
        }
        public dynamic GetChiTietNKs__(int mapn)
        {
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues,
                   db.CHITIETHDs);
            return (from ctnk in db.CHITIETNKs 
                                                   join lk in db.LINHKIENs on ctnk.MALINHKIEN equals lk.MALINHKIEN where ctnk.MAPN == mapn
                                                   select new
                                                   {
                                                       ctnk.MAPN,
                                                       lk.TENLINHKIEN,
                                                       ctnk.SOLUONG,
                                                       ctnk.DONGIA,
                                                       ctnk.THANHTIEN
                                                   }).ToList();
        }
        public int Insert(int mapn, int malk, int soluong, double donGia)
        {
            var ctnk = db.CHITIETNKs.FirstOrDefault(x => x.MAPN == mapn && x.MALINHKIEN == malk);
            if (ctnk != null)
            {
                Update(mapn, malk, (soluong + (int)ctnk.SOLUONG), donGia);
                return 2;
            }
            else
            {
                db.CHITIETNKs.InsertOnSubmit(new CHITIETNK()
                {
                    MAPN = mapn,
                    MALINHKIEN = malk,
                    SOLUONG = soluong,
                    DONGIA = donGia,
                    THANHTIEN = 0
                });
                db.SubmitChanges();
                return 1;
            }
        }
        public int Update(int mapn, int malk, int soluong, double donGia)
        {
            if (soluong == 0)
            {
                Delete(mapn, malk);
                return 2;
            }
            else
            {
                var ctnk = db.CHITIETNKs.FirstOrDefault(x => x.MAPN == mapn && x.MALINHKIEN == malk);
                if (ctnk == null)
                    return -1;
                ctnk.SOLUONG = soluong;
                ctnk.DONGIA = donGia;
                db.SubmitChanges();
                return 1;
            }
        }
        public int Delete(int mapn, int malk)
        {
            var ctnk = db.CHITIETNKs.FirstOrDefault(x => x.MAPN == mapn && x.MALINHKIEN == malk);
            if (ctnk == null)
                return -1;
            db.CHITIETNKs.DeleteOnSubmit(ctnk);
            db.SubmitChanges();
            return 1;
        }
        public bool IsProduct(int malinhkien)
        {
            return db.CHITIETNKs.FirstOrDefault(x => x.MALINHKIEN == malinhkien) != null;
        }
    }
}
