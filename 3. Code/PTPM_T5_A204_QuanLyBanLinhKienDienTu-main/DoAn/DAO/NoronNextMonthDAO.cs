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
    public class NoronNextMonthDAO
    {
        private readonly QL_LinhKienDBDataContext db = new QL_LinhKienDBDataContext();
        readonly ActivationNetwork network = new ActivationNetwork(new SigmoidFunction(), 1, 4, 1);
        private List<ItemNoronNextMonth> lstRevenue;
        private static NoronNextMonthDAO instances;
        public static NoronNextMonthDAO Instances
        {
            get
            {
                if (instances == null)
                    instances = new NoronNextMonthDAO();
                return instances;
            }
        }
        public dynamic LoadDataGC()
        {
            lstRevenue = new List<ItemNoronNextMonth>();
            DateTime months12Ago = DateTime.Now.AddMonths(-11);
            for (DateTime date = months12Ago; date.CompareTo(DateTime.Now) <= 0; date = date.AddMonths(1))
            {
                var total = db.HOADONs.Where(x => x.NGAYLAP.Month == date.Month && x.NGAYLAP.Year == date.Year && x.ispay == true).Sum(x => x.tongtien) ?? 0;
                lstRevenue.Add(new ItemNoronNextMonth()
                {
                    Month = date.Month + "/" + date.Year,
                    Revenue = total,
                    ConvertRevenue = Support.ConvertVND(total.ToString())
                });
            }
                    Train();
            return Support.ToDataTable<ItemNoronNextMonth>(lstRevenue);
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
        //chuẩn hoá dữ liệu về [0,1] của lstStaticalDay
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
            dataTable.Columns.Add("Month", typeof(DateTime));
            dataTable.Columns.Add("Total", typeof(double));
            DateTime months12Ago = DateTime.Now.AddMonths(-11);
            for (DateTime date = months12Ago; date.CompareTo(DateTime.Now) <= 0; date = date.AddMonths(1))
            {
                var total = db.HOADONs.Where(x => x.NGAYLAP.Month == date.Month && x.NGAYLAP.Year == date.Year && x.ispay == true).Sum(x => x.tongtien) ?? 0;
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

        //trả kết quả cuối cùng
        public double ReturnResult()
        {
            double min = FindMin();
            double max = FindMax();
            double lastMonthTotalRevenue = db.HOADONs.Where(x => x.NGAYLAP.CompareTo(DateTime.Now) == 0 && x.ispay == true).Sum(x => x.tongtien) ?? 0;
            double result = network.Compute(new double[] { lastMonthTotalRevenue })[0] * (max - min) + min;
            return Math.Round(result, 6);
        }

        double[][] ConvertDataTableToInputArray(DataTable dataTable)
        {
            // Chuyển đổi DataTable thành mảng 2D double[][]
            double[][] inputs = new double[dataTable.Rows.Count - 1][];

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

            for (int i = 0; i < dataTable.Rows.Count - 1; i++)
            {
                double totalAmount = 0;
                // Lấy giá trị từ cột "Total"
                if (i != dataTable.Rows.Count - 1)
                    totalAmount = DataNormalization(Convert.ToDouble(dataTable.Rows[i + 1]["Total"]));

                // Thêm giá trị vào mảng đầu ra
                outputs[i] = new double[] { totalAmount };
            }

            return outputs;
        }

    }
}
