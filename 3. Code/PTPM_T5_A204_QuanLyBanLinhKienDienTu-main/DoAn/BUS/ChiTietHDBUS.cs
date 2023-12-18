using DAO;

namespace BUS
{
    public class ChiTietHDBUS
    {
        private static ChiTietHDBUS instances;
        public static ChiTietHDBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new ChiTietHDBUS();
                return instances;
            }
            set { instances = value; }
        }
        public dynamic GetChiTietHDs(int mahd)
        {
            return ChiTietHDDAO.Instances.GetChiTietHDs(mahd);
        }
        public dynamic GetChiTietHDs_(int mahd)
        {
            return ChiTietHDDAO.Instances.GetChiTietHDs_(mahd);
        }
        public dynamic GetChiTietHDs__(int mahd)
        {
            return ChiTietHDDAO.Instances.GetChiTietNKs__(mahd);
        }
        public int Insert(int mahd, int malk, int soluong)
        {
            return ChiTietHDDAO.Instances.Insert(mahd, malk, soluong);
        }
        public int Update(int mahd, int malk, int soluong)
        {
            return ChiTietHDDAO.Instances.Update(mahd, malk, soluong);
        }
        public int Delete(int mahd, int malk)
        {
            return ChiTietHDDAO.Instances.Delete(mahd, malk);
        }
        public dynamic TimCTHDTheoMaHDMaLK(int mahd, int malk)
        {
            return ChiTietHDDAO.Instances.TimCTHDTheoMaHDMaLK(mahd, malk);
        }
        public bool IsProduct(int malinhkien)
        {
            return ChiTietHDDAO.Instances.IsProduct(malinhkien);
        }
    }
}
