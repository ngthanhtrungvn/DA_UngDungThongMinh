using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
   public class ItemNoronNextDay
    {
        private DateTime date;
        private double? revenue;
        private string convertRevenue;

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
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
