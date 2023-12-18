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
    public class NoronNextDayBUS
    {
        private static NoronNextDayBUS instances;
        public static NoronNextDayBUS Instances
        {
            get
            {
                if (instances == null)
                    instances = new NoronNextDayBUS();
                return instances;
            }
        }

        public dynamic LoadDataGC()
        {
            return NoronNextDayDAO.Instances.LoadDataGC();        
        }
        public double ReturnResult()
        {
            return NoronNextDayDAO.Instances.ReturnResult();
        }

    }
}
