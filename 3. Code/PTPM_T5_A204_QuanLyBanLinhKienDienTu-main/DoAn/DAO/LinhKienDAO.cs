using System;
using System.Linq;

namespace DAO
{
    public class LinhKienDAO
    {
        readonly QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static LinhKienDAO instances;
        public static LinhKienDAO Instances
        {
            get
            {
                if (instances == null)
                    instances = new LinhKienDAO();
                return instances;
            }
            set { instances = value; }
        }
        public dynamic GetLinhKiens()
        {
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, db.LINHKIENs);
            return Support.ToDataTable < LINHKIEN >(( from lk in db.LINHKIENs select lk).ToList());
        }
        public int Insert(string tenLK, int maLoai, string hangSX, double donGia, string hinhAnh, int soLuongCon)
        {
            try
            {
                db.LINHKIENs.InsertOnSubmit(new LINHKIEN()
                {
                    TENLINHKIEN = tenLK,
                    MALOAI = maLoai,
                    HANGSX = hangSX,
                    DONGIA = donGia,
                    HINHANH = hinhAnh,
                    SOLUONGCON = soLuongCon
                });
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }

        }
        public int Update(string tenLK, int maLoai, string hangSX, double donGia, string hinhAnh, int soLuongCon, int maLK)
        {
            var lk = db.LINHKIENs.FirstOrDefault(x => x.MALINHKIEN == maLK);
            try
            {
                if (lk == null)
                    return -1;
                lk.TENLINHKIEN = tenLK;
                lk.LOAILINHKIEN = db.LOAILINHKIENs.Single(x => x.MALOAI == maLoai);
                lk.MALOAI = maLoai;
                lk.HANGSX = hangSX;
                lk.DONGIA = donGia;
                lk.HINHANH = hinhAnh;
                lk.SOLUONGCON = soLuongCon;
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }

        }
        public int Delete(int maLK)
        {
            try
            {
                var lk = db.LINHKIENs.FirstOrDefault(x => x.MALINHKIEN == maLK);
                if (lk == null || ChiTietHDDAO.Instances.IsProduct(lk.MALINHKIEN) || ChiTietNKDAO.Instances.IsProduct(lk.MALINHKIEN))
                    return -1;
                db.LINHKIENs.DeleteOnSubmit(lk);
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public dynamic TimTheoMa(int malk)
        {
            return db.LINHKIENs.FirstOrDefault(x => x.MALINHKIEN == malk);
        }
        public bool IsTypeProduct(int maloai)
        {
            return db.LINHKIENs.FirstOrDefault(x => x.MALOAI == maloai) != null;
        }
    }
}
