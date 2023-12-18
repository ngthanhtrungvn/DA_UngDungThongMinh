using DAO;
using System;

namespace BUS
{
    public class NhanVienBUS
    {
        private static NhanVienBUS instances;
        public static NhanVienBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new NhanVienBUS();
                return instances;
            }
            set { instances = value; }
        }
        public dynamic GetNVs()
        {
            return NhanVienDAO.Instances.GetNVs();
        }
        public int Delete(int manv)
        {
            return NhanVienDAO.Instances.Delete(manv);
        }
        public dynamic Login(string userName, string passWord, ref int errorCode)
        {
            return NhanVienDAO.Instances.Login(userName, passWord, ref errorCode);
        }
        public int ChangePass(int manv, string oldPass, string newPass)
        {
            return NhanVienDAO.Instances.ChangePass(manv, oldPass, newPass);
        }
        public int Insert(string tennv, string diachi, string sdt, bool gioiTinh, DateTime ngayVL
            , double luong, string hinhAnh, string taiKhoan, int maQuyen)
        {
            return NhanVienDAO.Instances.Insert(tennv, diachi, sdt, gioiTinh, ngayVL
            , luong, hinhAnh, taiKhoan, maQuyen);
        }
        public int Update(string tennv, string diachi, string sdt, bool gioiTinh, DateTime ngayVL
            , double luong, string hinhAnh, int maQuyen, int manv)
        {
            return NhanVienDAO.Instances.Update(tennv, diachi, sdt, gioiTinh, ngayVL
           , luong, hinhAnh, maQuyen, manv);
        }
        public int ResetPass(int manv)
        {
            return NhanVienDAO.Instances.ResetPass(manv);
        }
    }
}
