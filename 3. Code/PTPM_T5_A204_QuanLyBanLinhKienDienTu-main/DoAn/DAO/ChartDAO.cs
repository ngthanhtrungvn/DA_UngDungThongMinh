using System;
using System.Data;
using System.Linq;

namespace DAO
{
    public class ChartDAO
    {
        readonly QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static ChartDAO instances;
        public static ChartDAO Instances
        {
            get
            {
                if (instances == null)
                    instances = new ChartDAO();
                return instances;
            }
            set { instances = value; }
        }
        //đơn hàng phiếu nhập trong tháng hiện tại
        public dynamic LoadOrderAndImportInMonthNow()
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("ten");
            tb.Columns.Add("soluong");
            DataRow dr = tb.NewRow();
            dr[0] = "Đơn hàng";
            dr[1] = db.HOADONs.Count(x => x.NGAYLAP.Month == DateTime.Now.Month && x.NGAYLAP.Year == DateTime.Now.Year);
            tb.Rows.Add(dr);
            dr = tb.NewRow();
            dr[0] = "Phiếu nhập";
            dr[1] = db.NHAPKHOs.Count(x => x.NGAYNHAP.Month == DateTime.Now.Month && x.NGAYNHAP.Year == DateTime.Now.Year);
            tb.Rows.Add(dr);
            return tb;
        }
        //top sản phẩm bán chạy nhất
        public dynamic LoadTopSelling()
        {
            return Support.ToDataTable(db.topSelling().OrderByDescending(x => x.soluong).ThenBy(x => x.TENLINHKIEN).ToList());
        }
        //load doanh thu năm hiện tại
        public dynamic LoadStatisticalYear()
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("ten");
            tb.Columns.Add("soluong");
            DataRow dr = tb.NewRow();
            dr[0] = "Tiền thu";
            dr[1] = db.HOADONs.Where(x => x.NGAYLAP.Year == DateTime.Now.Year).Sum(x => x.tongtien);
            tb.Rows.Add(dr);
            dr = tb.NewRow();
            dr[0] = "Tiền chi";
            dr[1] = db.NHAPKHOs.Where(x => x.NGAYNHAP.Year == DateTime.Now.Year).Sum(x => x.tongtien);
            tb.Rows.Add(dr);
            return tb;
        }
        //sản phẩm sắp hết hàng <=5
        public dynamic LoadProductNotStock()
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("ten");
            tb.Columns.Add("soluong");
            foreach (var item in db.LINHKIENs.Where(x => x.SOLUONGCON < 5))
            {
                DataRow dr = tb.NewRow();
                dr[0] = item.TENLINHKIEN;
                dr[1] = item.SOLUONGCON;
                tb.Rows.Add(dr);
            }
            return tb;
        }
    }
}
