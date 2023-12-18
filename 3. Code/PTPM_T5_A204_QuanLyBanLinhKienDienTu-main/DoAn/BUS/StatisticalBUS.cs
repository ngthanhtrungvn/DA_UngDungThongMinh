using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace BUS
{
    public class StatisticalBUS
    {
        private QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static StatisticalBUS instance;
        public static StatisticalBUS Instance
        {
            get
            {
                if (instance == null)
                    return new StatisticalBUS();
                return instance;
            }

            set
            {
                instance = value;
            }
        }
        public double? TinhTienChi(DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            return StatisticalDAO.Instance.TinhTienChi(dateTimeFrom, dateTimeTo);
        }
        public double? TinhTienThu(DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            return StatisticalDAO.Instance.TinhTienThu(dateTimeFrom, dateTimeTo);
        }
        public dynamic LoadDetailStatistical(DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            return StatisticalDAO.Instance.LoadDetailStatistical(dateTimeFrom, dateTimeTo);
        }
    }
}
