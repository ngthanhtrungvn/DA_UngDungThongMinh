using DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class ChartBUS
    {
        private static ChartBUS instances;
        public static ChartBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new ChartBUS();
                return instances;
            }
            set { instances = value; }
        }
        //đơn hàng phiếu nhập trong tháng hiện tại
        public dynamic LoadOrderAndImportInMonthNow()
        {
            return ChartDAO.Instances.LoadOrderAndImportInMonthNow();
        }
        //top sản phẩm bán chạy nhất
        public dynamic LoadTopSelling()
        {
            return ChartDAO.Instances.LoadTopSelling();
        }
        //load doanh thu năm hiện tại
        public dynamic LoadStatisticalYear()
        {
            return ChartDAO.Instances.LoadStatisticalYear();
        }
        //sản phẩm sắp hết hàng <=5
        public dynamic LoadProductNotStock()
        {
            return ChartDAO.Instances.LoadProductNotStock();
        }
    }
}
