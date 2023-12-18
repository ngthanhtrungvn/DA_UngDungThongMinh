using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class StatisticalDAO
    {
        private readonly QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        private static StatisticalDAO instance;
        public static StatisticalDAO Instance
        {
            get
            {
                if (instance == null)
                    return new StatisticalDAO();
                return instance;
            }

            set
            {
                instance = value;
            }
        }
        public double? TinhTienChi(DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            double? sumMoney = db.NHAPKHOs.Where(x => x.ispay == true && x.NGAYNHAP.CompareTo(dateTimeFrom) >= 0 && x.NGAYNHAP.CompareTo(dateTimeTo) <= 0).Sum(x => x.tongtien) ?? 0;
            return sumMoney;
        }
        public double? TinhTienThu(DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            double? sumMoney = db.HOADONs.Where(x => x.ispay == true && x.NGAYLAP.CompareTo(dateTimeFrom) >= 0 && x.NGAYLAP.CompareTo(dateTimeTo) <= 0).Sum(x => x.tongtien) ?? 0;
            return sumMoney;
        }
        public dynamic LoadDetailStatistical( DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("ngay");
            tb.Columns.Add("thu");
            tb.Columns.Add("chi");
            tb.Columns.Add("loinhuan");
            var lstOrder = db.HOADONs.Where(x => x.ispay == true && x.NGAYLAP.CompareTo(dateTimeFrom) >= 0 && x.NGAYLAP.CompareTo(dateTimeTo) <= 0).ToList();
            var lstImport = db.NHAPKHOs.Where(x => x.ispay == true && x.NGAYNHAP.CompareTo(dateTimeFrom) >= 0 && x.NGAYNHAP.CompareTo(dateTimeTo) <= 0).ToList();
            for (DateTime date = dateTimeFrom; date.CompareTo(dateTimeTo) <= 0; date = date.AddDays(1))
            {
                var lstOrderTemp = lstOrder.Where(x => DateTime.Parse(x.NGAYLAP.ToShortDateString()).CompareTo(DateTime.Parse(date.ToShortDateString())) == 0).ToList();
                var lstImportTemp = lstImport.Where(x => DateTime.Parse(x.NGAYNHAP.ToShortDateString()).CompareTo(DateTime.Parse(date.ToShortDateString())) == 0).ToList();
                if (lstOrderTemp.Count != 0 || lstImportTemp.Count != 0)
                {

                    DataRow dr = tb.NewRow();
                    var sumOrder = lstOrderTemp.Sum(x => x.tongtien);
                    var sumImport = lstImportTemp.Sum(x => x.tongtien);
                    dr[0] = date.ToShortDateString();
                    dr[1] = Support.ConvertVND(sumOrder.ToString());
                    dr[2] = Support.ConvertVND(sumImport.ToString());
                    dr[3] = Support.ConvertVND((sumOrder - sumImport).ToString());
                    tb.Rows.Add(dr);
                }
            }
            return tb;
        }
    }
}
