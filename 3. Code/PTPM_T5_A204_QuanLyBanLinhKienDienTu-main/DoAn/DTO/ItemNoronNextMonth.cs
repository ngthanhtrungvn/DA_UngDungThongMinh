using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
  public  class ItemNoronNextMonth
    {
        private string month;
        private double? revenue;
        private string convertRevenue;

        public string Month
        {
            get
            {
                return month;
            }

            set
            {
                month = value;
            }
        }

        public double? Revenue
        {
            get
            {
                return revenue;
            }

            set
            {
                revenue = value;
            }
        }

        public string ConvertRevenue
        {
            get
            {
                return convertRevenue;
            }

            set
            {
                convertRevenue = value;
            }
        }
    }
}
