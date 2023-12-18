using DAO;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class NoronNextMonthBUS
    {
        private static NoronNextMonthBUS instances;
        public static NoronNextMonthBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new NoronNextMonthBUS();
                return instances;
            }
        }
        public dynamic LoadDataGC()
        {
            return NoronNextMonthDAO.Instances.LoadDataGC();
        }
        public double ReturnResult()
        {
            return NoronNextMonthDAO.Instances.ReturnResult();
        }
    }
}
