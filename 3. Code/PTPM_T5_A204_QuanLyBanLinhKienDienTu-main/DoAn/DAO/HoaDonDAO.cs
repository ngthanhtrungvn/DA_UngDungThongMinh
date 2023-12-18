using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class HoaDonDAO
    {
        readonly QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static HoaDonDAO instances;
        public static HoaDonDAO Instances
        {
            get
            {
                if (instances == null)
                    instances = new HoaDonDAO();
                return instances;
            }

            set
            {
                instances = value;
            }
        }
        public dynamic GetHoaDons(bool isPay)
        {
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues,
                    db.HOADONs);
            if (isPay)
                return Support.ToDataTable<HOADON>((from hd in db.HOADONs select hd).ToList());
            else
                return Support.ToDataTable<HOADON>((from hd in db.HOADONs where hd.ispay == false select hd).ToList());
        }
        public int Insert(int manv, int makh, double giamGia)
        {
            try
            {
                db.HOADONs.InsertOnSubmit(new HOADON()
                {
                    ispay = false,
                    giamgia = giamGia,
                    MAKH = makh,
                    MANV = manv,
                    NGAYLAP = DateTime.Now,
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
        public int Delete(int mahd)
        {
            try
            {
                db.HOADONs.DeleteOnSubmit(db.HOADONs.FirstOrDefault(x => x.MAHD == mahd));
                db.SubmitChanges();
            }
            catch (Exception)
            {
                return -1;
            }
            return 1;
        }
        public int Update(int mahd, bool ispay)
        {
            try
            {
                var hd = db.HOADONs.FirstOrDefault(x => x.MAHD == mahd);
                hd.ispay = ispay;
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
            return db.HOADONs.OrderByDescending(x => x.MAHD).FirstOrDefault();
        }
        public dynamic FindOrderCode(int code)
        {
            return db.HOADONs.FirstOrDefault(x => x.MAHD == code);
        }
        public bool CheckIsStaffOrder(int manv)
        {
            return db.HOADONs.FirstOrDefault(x => x.MANV == manv) != null;
        }
        public bool IsCustomer(int makh)
        {
            return db.HOADONs.FirstOrDefault(x => x.MAKH == makh) != null;
        }
    }
}
