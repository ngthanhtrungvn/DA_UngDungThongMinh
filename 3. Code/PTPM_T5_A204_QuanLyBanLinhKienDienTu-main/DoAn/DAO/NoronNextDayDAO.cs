using Accord.Neuro;
using Accord.Neuro.Learning;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class NoronNextDayDAO
    {
        private readonly QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        readonly ActivationNetwork network = new ActivationNetwork(new SigmoidFunction(), 1, 4, 1);
        private List<ItemNoronNextDay> lstRevenue;
        private static NoronNextDayDAO instances;
        public static NoronNextDayDAO Instances
        {
            get
            {
                if (instances == null)
                    instances = new NoronNextDayDAO();
                return instances;
            }
        }
        public dynamic LoadDataGC()
        {
            lstRevenue = new List<ItemNoronNextDay>();
            DateTime days30Ago = DateTime.Now.AddDays(-29);
            for (DateTime date = days30Ago; date.CompareTo(DateTime.Now) <= 0; date = date.AddDays(1))
            {
                var total = db.HOADONs.Where(x => x.NGAYLAP.CompareTo(date) == 0 && x.ispay == true).Sum(x => x.tongtien) ?? 0;
                lstRevenue.Add(new ItemNoronNextDay()
                {
                    Date = date,
                    Revenue = total,
                    ConvertRevenue = Support.ConvertVND(total.ToString())
                });
            }
            Train();
            return Support.ToDataTable<ItemNoronNextDay>(lstRevenue);
        }
        //tìm doanh thu lớn nhất
        private double FindMax()
        {
            return Math.Round(lstRevenue.Max(x => x.Revenue) ?? 0, 6);
        }
        //tìm doanh thu nhỏ nhất
        private double FindMin()
        {
            return Math.Round(lstRevenue.Min(x => x.Revenue) ?? 0, 6);

        }
        //chuẩn hoá dữ liệu về [min,max] để đưa dữ liệu về từ 0-1
        private double DataNormalization(double x)
        {
            double min = FindMin();
            double max = FindMax();
            double result = (x - min) / (max - min);
            return Math.Round(result, 6);
        }

        //train dữ liệu 
        private void Train()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Day", typeof(DateTime));
            dataTable.Columns.Add("Total", typeof(double));
            DateTime days30Ago = DateTime.Now.AddDays(-29);
            for (DateTime date = days30Ago; date.CompareTo(DateTime.Now) <= 0; date = date.AddDays(1))
            {
                var total = db.HOADONs.Where(x => x.NGAYLAP.CompareTo(date) == 0 && x.ispay == true).Sum(x => x.tongtien) ?? 0;
                dataTable.Rows.Add(date, total);
            }
            double[][] inputs = ConvertDataTableToInputArray(dataTable);
            double[][] outputs = ConvertDataTableToOutputArray(dataTable);
            
            BackPropagationLearning teacher = new BackPropagationLearning(network);
            int m = 0;
            while (m++ < 1000)
            {
                teacher.RunEpoch(inputs, outputs);
            }
        }
         double[][] ConvertDataTableToInputArray(DataTable dataTable)
        {
            // Chuyển đổi DataTable thành mảng 2D double[][]
            double[][] inputs = new double[dataTable.Rows.Count - 1][];
            // lấy tới 29
            for (int i = 0; i < dataTable.Rows.Count - 1; i++)
            {
                // Lấy giá trị từ cột "Total"
                double totalAmount = DataNormalization(Convert.ToDouble(dataTable.Rows[i]["Total"]));

                // Thêm giá trị vào mảng đầu vào
                inputs[i] = new double[] { totalAmount };
            }

            return inputs;
        }
         double[][] ConvertDataTableToOutputArray(DataTable dataTable)
        {
            double[][] outputs = new double[dataTable.Rows.Count - 1][];
            // từ 2-30
            for (int i = 0; i < dataTable.Rows.Count - 1; i++)
            {
                double totalAmount = 0;
                // Lấy giá trị từ cột "Total"
                totalAmount = DataNormalization(Convert.ToDouble(dataTable.Rows[i + 1]["Total"]));

                // Thêm giá trị vào mảng đầu ra
                outputs[i] = new double[] { totalAmount };
            }

            return outputs;
        }

        //trả kết quả cuối cùng
        public double ReturnResult()
        {
            double min = FindMin();
            double max = FindMax();
            double lastDayTotalRevenue = db.HOADONs.Where(x => x.NGAYLAP.CompareTo(DateTime.Now) == 0 && x.ispay == true).Sum(x => x.tongtien) ?? 0;
            double result = network.Compute(new double[] { lastDayTotalRevenue })[0] * (max - min) + min;
            return Math.Round(result, 6);
        }
    }
}

