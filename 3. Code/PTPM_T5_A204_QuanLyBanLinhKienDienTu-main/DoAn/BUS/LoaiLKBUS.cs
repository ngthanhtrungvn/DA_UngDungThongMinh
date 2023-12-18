using DAO;

namespace BUS
{
    public class LoaiLKBUS
    {
        private static LoaiLKBUS instances;
        public static LoaiLKBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new LoaiLKBUS();
                return instances;
            }
            set { instances = value; }
        }
        public dynamic GetLoaiLKs()
        {
            return LoaiLKDAO.Instances.GetLoaiLKs();
        }
        public int Insert(string tenLoai)
        {
            return LoaiLKDAO.Instances.Insert(tenLoai);
        }
        public int Update(string tenLoai, int maLoai)
        {
            return LoaiLKDAO.Instances.Update(tenLoai, maLoai);
        }
        public int Delete(int maLoai)
        {
            return LoaiLKDAO.Instances.Delete(maLoai);
        }
    }
}
