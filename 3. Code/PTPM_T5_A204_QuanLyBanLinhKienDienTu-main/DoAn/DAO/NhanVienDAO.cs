using System;
using System.Linq;
using DAO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class NhanVienDAO
    {
        readonly QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static NhanVienDAO instances;
        public static NhanVienDAO Instances
        {
            get{if (instances == null)
                    instances = new NhanVienDAO();
                return instances;}
            set{ instances = value;}
        }
        public dynamic GetNVs()
        {
            var lst = (from nv in db.NHANVIENs select nv).ToList();
            return Support.ToDataTable<NHANVIEN>(lst); ;
        }
        public int Insert(string tennv, string diachi, string sdt, bool gioiTinh, DateTime ngayVL
            , double luong, string hinhAnh, string taiKhoan, int maQuyen)
        {
            try
            {
                db.NHANVIENs.InsertOnSubmit(new NHANVIEN()
                {
                    TENNV = tennv,
                    DIACHI = diachi,
                    SDT = sdt,
                    GIOITINH = gioiTinh,
                    NGAYVL = ngayVL,
                    LUONG = luong,
                    HINHANH = hinhAnh,
                    taikhoan = taiKhoan,
                    MATKHAU = Support.EndCodeMD5("12345"),
                    maquyen = maQuyen
                });
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }

        }
        public int Update(string tennv, string diachi, string sdt, bool gioiTinh, DateTime ngayVL
            , double luong, string hinhAnh, int maQuyen, int manv)
        {
            var nv = db.NHANVIENs.FirstOrDefault(x => x.MANV == manv);

            try
            {
                if (nv == null)
                    return -1;
                nv.TENNV = tennv;
                nv.DIACHI = diachi;
                nv.SDT = sdt;
                nv.GIOITINH = gioiTinh;
                nv.NGAYVL = ngayVL;
                nv.LUONG = luong;
                nv.HINHANH = hinhAnh;
                nv.QUYEN = db.QUYENs.FirstOrDefault(x => x.maquyen == maQuyen);
                nv.maquyen = maQuyen;
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {

                return -1;
            }

        }
        public int Delete(int manv)
        {
            try
            {
                var nv = db.NHANVIENs.FirstOrDefault(x => x.MANV == manv);
                if (nv == null || HoaDonDAO.Instances.CheckIsStaffOrder(nv.MANV) || NhapKhoDAO.Instances.CheckIsStaffImport(manv))
                    return -1;
                db.NHANVIENs.DeleteOnSubmit(nv);
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public int ResetPass(int manv)
        {
            try
            {
                var nv = db.NHANVIENs.FirstOrDefault(x => x.MANV == manv);
              
                if (nv == null)
                    return -1;
                nv.MATKHAU = Support.EndCodeMD5("12345");
                db.SubmitChanges();
                return 1;
            }
            catch (Exception)
            {

                return -1;
            }

        }
        public NHANVIEN Login(string userName, string passWord, ref int errorCode)
        {
            passWord = Support.EndCodeMD5(passWord.Trim());
            NHANVIEN nv = null;
            try
            {
                nv = db.NHANVIENs.FirstOrDefault(x => x.taikhoan.Trim().Equals(userName.Trim()) && x.MATKHAU.Equals(passWord));
            }
            catch (SqlException ex)
            {
                errorCode = ex.ErrorCode;
            }
            return nv;
        }
        public int ChangePass(int manv, string oldPass, string newPass)
        {
            try
            {
                oldPass = Support.EndCodeMD5(oldPass);
                var nv = db.NHANVIENs.FirstOrDefault(x => x.MANV == manv && x.MATKHAU.Equals(oldPass));
                if (nv == null)
                    return -1;
                nv.MATKHAU = Support.EndCodeMD5(newPass);
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
