using DAO;

namespace BUS
{
    public class LinhKienBUS
    {
        private static LinhKienBUS instances;
        public static LinhKienBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new LinhKienBUS();
                return instances;
            }
            set { instances = value; }
        }
        public dynamic GetLinhKiens()
        {
            return LinhKienDAO.Instances.GetLinhKiens();
        }
        public int Insert(string tenLK, int maLoai, string hangSX, double donGia, string hinhAnh, int soLuongCon)
        {
            return LinhKienDAO.Instances.Insert(tenLK, maLoai, hangSX, donGia, hinhAnh, soLuongCon);
        }
        public int Update(string tenLK, int maLoai, string hangSX, double donGia, string hinhAnh, int soLuongCon, int maLK)
        {
            return LinhKienDAO.Instances.Update(tenLK, maLoai, hangSX, donGia, hinhAnh, soLuongCon, maLK);
        }
        public int Delete(int maLK)
        {
            return LinhKienDAO.Instances.Delete(maLK);
        }
        public dynamic TimTheoMa(int malk)
        {
            return LinhKienDAO.Instances.TimTheoMa(malk);
        }
        public bool IsTypeProduct(int maloai)
        {
            return LinhKienDAO.Instances.IsTypeProduct(maloai);
        }
    }
}
