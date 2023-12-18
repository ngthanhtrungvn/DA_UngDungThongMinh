using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class KhachHangDAO
    {
        readonly QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static KhachHangDAO instances;
        public static KhachHangDAO Instances
        {
            get
            {
                if (instances == null)
                    instances = new KhachHangDAO();
                return instances;
            }
            set
            {
                instances = value;
            }
        }
        public dynamic GetKhachHangs()
        {
            return Support.ToDataTable<KHACHHANG>((from kh in db.KHACHHANGs select kh).ToList());
        }
        public int Insert(string tenkh, bool gioiTinh, string diaChi, string sdt, int maLoaiKH)
        {
            try
            {
                db.KHACHHANGs.InsertOnSubmit(new KHACHHANG()
                {
                    TENKH = tenkh,
                    DIACHI = diaChi,
                    GIOITINH = gioiTinh,
                    SDT = sdt,
                    maloaikh = maLoaiKH
                });
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }

        }
        public int Update(string tenkh, bool gioiTinh, string diaChi, string sdt, int maloaikh, int maKH)
        {
            var kh = db.KHACHHANGs.FirstOrDefault(x => x.Makh == maKH);
            try
            {
                if (kh == null)
                    return -1;
                kh.TENKH = tenkh;
                kh.GIOITINH = gioiTinh;
                kh.DIACHI = diaChi;
                kh.SDT = sdt;
                kh.LOAIKH = db.LOAIKHs.Single(x => x.MALOAIKH == maloaikh);
                kh.maloaikh = maloaikh;
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }

        }
        public int Delete(int maKH)
        {
            try
            {
                var kh = db.KHACHHANGs.FirstOrDefault(x => x.Makh == maKH);
                if (kh == null || HoaDonDAO.Instances.IsCustomer(kh.Makh))
                    return -1;
                db.KHACHHANGs.DeleteOnSubmit(kh);
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }

        }
        public dynamic LayThongTinTheoMa(string maKH)
        {
            return db.KHACHHANGs.FirstOrDefault(x => x.Makh.Equals(maKH));
        }
        public bool IsTypeCustomer(int malkh)
        {
            return db.KHACHHANGs.FirstOrDefault(x => x.maloaikh == malkh) != null;
        }
    }
}
