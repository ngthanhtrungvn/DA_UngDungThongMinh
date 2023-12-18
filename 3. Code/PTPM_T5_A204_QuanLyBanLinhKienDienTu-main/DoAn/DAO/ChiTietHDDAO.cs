using System.Linq;

namespace DAO
{
    public class ChiTietHDDAO
    {
        readonly QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static ChiTietHDDAO instances;
        public static ChiTietHDDAO Instances
        {
            get
            {
                if (instances == null)
                    instances = new ChiTietHDDAO();
                return instances;
            }
            set { instances = value; }
        }
        public dynamic GetChiTietHDs(int mahd)
        {
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues,
                   db.CHITIETHDs);
            return Support.ToDataTable<CHITIETHD>((from cthd in db.CHITIETHDs where cthd.MAHD == mahd select cthd).ToList());
        }
        public dynamic GetChiTietHDs_(int mahd)
        {
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues,
                   db.CHITIETHDs);
            return (from cthd in db.CHITIETHDs where cthd.MAHD == mahd select cthd).ToList();
        }
        public dynamic GetChiTietNKs__(int mahd)
        {
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues,
                   db.CHITIETHDs);
            return (from ctnk in db.CHITIETHDs
                    join lk in db.LINHKIENs on ctnk.MALINHKIEN equals lk.MALINHKIEN
                    where ctnk.MAHD == mahd
                    select new
                    {
                        ctnk.MAHD,
                        lk.TENLINHKIEN,
                        ctnk.SOLUONG,
                        ctnk.DONGIA,
                        ctnk.THANHTIEN
                    }).ToList();
        }
        public int Insert(int mahd, int malk, int soluong)
        {
            var cthd = db.CHITIETHDs.FirstOrDefault(x => x.MAHD == mahd && x.MALINHKIEN == malk);
            if (cthd != null)
            {
                Update(mahd, malk, (soluong + (int)cthd.SOLUONG));
                return 2;
            }
            else
            {
                db.CHITIETHDs.InsertOnSubmit(new CHITIETHD()
                {
                    MAHD = mahd,
                    MALINHKIEN = malk,
                    SOLUONG = soluong,
                    DONGIA = LinhKienDAO.Instances.TimTheoMa(malk).DONGIA,
                    THANHTIEN = 0
                });
                db.SubmitChanges();
                return 1;
            }
        }
        public int Update(int mahd, int malk, int soluong)
        {
            if (soluong == 0)
            {
                Delete(mahd, malk);
                return 2;
            }
            else
            {
                var cthd = db.CHITIETHDs.FirstOrDefault(x => x.MAHD == mahd && x.MALINHKIEN == malk);
                if (cthd == null)
                    return -1;
                cthd.SOLUONG = soluong;
                cthd.DONGIA = LinhKienDAO.Instances.TimTheoMa(malk).DONGIA;
                db.SubmitChanges();
                return 1;
            }
        }
        public int Delete(int mahd, int malk)
        {
            var cthd = db.CHITIETHDs.FirstOrDefault(x => x.MAHD == mahd && x.MALINHKIEN == malk);
            if (cthd == null)
                return -1;
            db.CHITIETHDs.DeleteOnSubmit(cthd);
            db.SubmitChanges();
            return 1;
        }
        public dynamic TimCTHDTheoMaHDMaLK(int mahd, int malk)
        {
            return db.CHITIETHDs.FirstOrDefault(x => x.MAHD == mahd && x.MALINHKIEN == malk);
        }
        public bool IsProduct(int malinhkien)
        {
            return db.CHITIETHDs.FirstOrDefault(x => x.MALINHKIEN == malinhkien) != null;
        }
    }
}
