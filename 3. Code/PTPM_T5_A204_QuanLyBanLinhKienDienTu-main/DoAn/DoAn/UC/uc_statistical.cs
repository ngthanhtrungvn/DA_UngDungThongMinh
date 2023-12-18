using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BUS;
using DoAn.FRM;
using GUI.Report;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Parameters;
namespace DoAn.UC
{
    public partial class Uc_statistical : DevExpress.XtraEditors.XtraUserControl
    {
        readonly FrmMain frm;
        public Uc_statistical(FrmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
            gvStatistical.IndicatorWidth = 50;
            dateFrom.DateTime = DateTime.Now;
            dateTo.DateTime = DateTime.Now;
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm.CLOSE();
        }

        private void BtnThongKe_Click(object sender, EventArgs e)
        {
            if(DateTime.Parse(dateFrom.DateTime.ToShortDateString()).CompareTo(DateTime.Parse(dateTo.DateTime.ToShortDateString())) >0)
            {
                XtraMessageBox.Show("Ngày tìm không hợp lệ.", "Thông báo");
                return;
            }
            var sumStatistic = StatisticalBUS.Instance.TinhTienThu(dateFrom.DateTime, dateTo.DateTime);
            var sumSpend = StatisticalBUS.Instance.TinhTienChi(dateFrom.DateTime, dateTo.DateTime);
            txtSumStatistic.Text = Support.ConvertVND(sumStatistic.ToString());
            txtSumSpend.Text = Support.ConvertVND(sumSpend.ToString());
            txtProfit.Text = Support.ConvertVND((sumStatistic - sumSpend).ToString());
            gcStatistical.DataSource = StatisticalBUS.Instance.LoadDetailStatistical( dateFrom.DateTime, dateTo.DateTime);

        }

        private void BtnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable tb = StatisticalBUS.Instance.LoadDetailStatistical(dateFrom.DateTime, dateTo.DateTime);
            if (tb == null || tb.Rows.Count == 0)
                return;
            var rp = new rpStatistical
            {
                DataSource = tb
            };
            rp.lbNguoiLap.Value = frm.nv.TENNV;
            rp.ShowPreviewDialog();
        }

        private void GvStatistical_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;
            e.Info.DisplayText = (e.RowHandle + 1) + "";
        }
    }
}
