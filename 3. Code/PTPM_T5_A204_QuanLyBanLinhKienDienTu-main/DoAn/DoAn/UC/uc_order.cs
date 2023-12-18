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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using DoAn.FRM;
using BUS;
using GUI.Report;

namespace DoAn.UC
{
    public partial class Uc_order : DevExpress.XtraEditors.XtraUserControl
    {
        dynamic lstDetailOrder;
        readonly FrmMain frm;
        public Uc_order(FrmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        //load data khi form khởi chạy
        private void Uc_order_Load(object sender, EventArgs e)
        {
            gcOrder.DataSource = HoaDonBUS.Instances.GetHoaDons( true);
            gvOrder.IndicatorWidth = 50;
            gvOrderDetail.IndicatorWidth = 50;
        }
        //đóng form hoá đơn
        private void BtnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm.CLOSE();
        }

        private void GvOrder_MasterRowEmpty(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowEmptyEventArgs e)
        {
            var mahd = gvOrder.GetRowCellValue(e.RowHandle, "MAHD");
            if (mahd != null)
                e.IsEmpty = ChiTietHDBUS.Instances.GetChiTietHDs(int.Parse(mahd.ToString())).Rows.Count == 0;
        }

        private void GvOrder_MasterRowGetChildList(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs e)
        {
            var mahd = gvOrder.GetRowCellValue(e.RowHandle, "MAHD");
            if (mahd != null)
            {
                e.ChildList = ChiTietHDBUS.Instances.GetChiTietHDs_(int.Parse(mahd.ToString()));

                gvOrderDetail.ViewCaption = "Chi tiết hoá đơn " + mahd;

            }
        }

        private void GvOrder_MasterRowGetRelationCount(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 1;
        }

        private void GvOrder_MasterRowGetRelationName(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "Chi tiết hoá đơn";
        }
        private void DeleteOrder()
        {
            int row = gvOrder.FocusedRowHandle;
            if (row >= 0)
            {
                int mahd = int.Parse(gvOrder.GetRowCellValue(row, "MAHD").ToString());
                if (XtraMessageBox.Show("Bạn chắc chắn xoá hoá đơn " + mahd + "?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    int i = HoaDonBUS.Instances.Delete(mahd);
                    if (i != -1)
                    {
                        XtraMessageBox.Show("Xoá hoá đơn thành công.", "Thông báo");
                        gcOrder.DataSource = HoaDonBUS.Instances.GetHoaDons( true);
                    }
                    else
                        XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteOrder();
        }



        private void GcOrder_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gvOrder.State != GridState.Editing)
                DeleteOrder();
        }

        private void BtnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            int row = gvOrder.FocusedRowHandle;
            if (row >= 0)
            {
                int mahd = int.Parse(gvOrder.GetRowCellValue(row, "MAHD").ToString());
                lstDetailOrder = ChiTietHDBUS.Instances.GetChiTietHDs__(mahd);
                dynamic hd = HoaDonBUS.Instances.FindOrderCode(mahd);
                var rp = new rpOrder
                {
                    DataSource = lstDetailOrder
                };
                rp.lbNguoiLap.Value = frm.nv.TENNV;
                rp.lbCodeOrder.Value = "BÁO CÁO HOÁ ĐƠN " + mahd;
                rp.lbCustomer.Value = hd.KHACHHANG.TENKH;
                rp.lbDate.Value = hd.NGAYLAP;
                rp.lbSale.Value = hd.giamgia;
                rp.lbStaff.Value = hd.NHANVIEN.TENNV;
                rp.lbTienPhaiTra.Value = hd.tongtien;
                rp.ShowPreviewDialog();

            }
        }

        private void GvOrder_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;
            e.Info.DisplayText = (e.RowHandle + 1) + "";
        }

        private void GvOrderDetail_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;
            e.Info.DisplayText = (e.RowHandle + 1) + "";
        }


    }
}
