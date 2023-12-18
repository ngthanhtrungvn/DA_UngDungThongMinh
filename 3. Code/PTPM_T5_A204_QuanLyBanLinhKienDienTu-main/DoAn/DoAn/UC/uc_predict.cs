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
using DevExpress.XtraCharts;
using DoAn.FRM;
using BUS;

namespace DoAn.UC
{
    public partial class Uc_predict : DevExpress.XtraEditors.XtraUserControl
    {
        readonly FrmMain frm;
        private readonly dynamic tbNextDay;
        private readonly dynamic tbNextMonth;

        public Uc_predict(FrmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
            tbNextDay = NoronNextDayBUS.Instances.LoadDataGC();
            gcPredictNextDay.DataSource = tbNextDay;
            tbNextMonth = NoronNextMonthBUS.Instances.LoadDataGC();
            gcPredictNextMonth.DataSource = tbNextMonth;
            gvPredictNextDay.IndicatorWidth = 50;
            gvPredictNextMonth.IndicatorWidth = 50;
            LoadChartPredictDay();
            LoadChartPredictMonth();
        }

        private void BtnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm.CLOSE();
        }

        private void BtnPredict_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
                XtraMessageBox.Show("Doanh thu ngày " + DateTime.Now.AddDays(1).ToShortDateString() + " là: " + Support.ConvertVND(NoronNextDayBUS.Instances.ReturnResult().ToString()), "Thông báo");
            else
                XtraMessageBox.Show("Doanh thu tháng " + DateTime.Now.AddMonths(1).Month + "/" + DateTime.Now.AddMonths(1).Year + " là: " + Support.ConvertVND(NoronNextMonthBUS.Instances.ReturnResult().ToString()), "Thông báo");

        }



        private void LoadChartPredictDay()
        {
            Series _seri = new Series("", ViewType.SwiftPlot);
            ChartTitle title = new ChartTitle
            {
                Text = "Doanh thu 30 ngày gần nhất."
            };
            chartNextDay.Titles.Add(title);
            chartNextDay.Series.Add(_seri);
            foreach (DataRow dr in tbNextDay.Rows)
                _seri.Points.Add(new SeriesPoint(dr[0].ToString(), dr[1].ToString().Equals("") ? "0" : dr[1].ToString()));
            foreach (Series series in chartNextDay.Series)
            {
                series.CrosshairLabelPattern = "{A:d}: {V:N0}";
            }
        }


        private void LoadChartPredictMonth()
        {
            Series _seri = new Series("", ViewType.SwiftPlot);
            ChartTitle title = new ChartTitle
            {
                Text = "Doanh thu 12 tháng gần nhất."
            };
            chartNextMonth.Titles.Add(title);
            chartNextMonth.Series.Add(_seri);
            foreach (DataRow dr in tbNextMonth.Rows)
                _seri.Points.Add(new SeriesPoint(dr[0].ToString(), dr[1].ToString().Equals("") ? "0" : dr[1].ToString()));
            foreach (Series series in chartNextMonth.Series)
            {
                series.CrosshairLabelPattern = "{A:Y}: {V:N0}";
            }
        }


        private void GvPredictNextDay_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;
            e.Info.DisplayText = (e.RowHandle + 1) + "";
        }

        private void GvPredictNextMonth_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;
            e.Info.DisplayText = (e.RowHandle + 1) + "";
        }

        private void ChartNextMonth_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
        {
            try
            {
                e.Item.Text = Support.ConvertVND(e.Item.Text);
            }
            catch (Exception )
            {

            }
        }

        private void ChartNextDay_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
        {
            try
            {
                e.Item.Text = Support.ConvertVND(e.Item.Text);
            }
            catch (Exception )
            {

            }
        }
    }
}
