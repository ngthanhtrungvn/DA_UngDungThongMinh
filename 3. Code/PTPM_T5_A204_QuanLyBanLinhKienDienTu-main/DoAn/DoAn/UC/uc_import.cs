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
    public partial class Uc_import : DevExpress.XtraEditors.XtraUserControl
    {
        readonly FrmMain frm;
        dynamic lstDetailImport;
        public Uc_import(FrmMain frm)
        {
            InitializeComponent(); 
            this.frm = frm;
        }
        private void Uc_import_Load(object sender, EventArgs e)
        {
            gcImport.DataSource = NhapKhoBUS.Instances.GetNhapKhos(true);

            gvImport.IndicatorWidth = 50;
            gvImportDetail.IndicatorWidth = 50;
        }
        //đóng form
        private void BtnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm.CLOSE();
        }

        private void GvImport_MasterRowEmpty(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowEmptyEventArgs e)
        {
            var mapn = gvImport.GetRowCellValue(e.RowHandle, "MAPN");
            if (mapn != null)
                e.IsEmpty = ChiTietNKBUS.Instances.GetChiTietNKs(int.Parse(mapn.ToString())).Rows.Count == 0;
        }

        private void GvImport_MasterRowGetChildList(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs e)
        {
            var mapn = gvImport.GetRowCellValue(e.RowHandle, "MAPN");
            if (mapn != null)
            {
                e.ChildList = ChiTietNKBUS.Instances.GetChiTietNKs_(int.Parse(mapn.ToString()));
                gvImportDetail.ViewCaption = "Chi tiết phiếu nhập " + mapn;

            }
        }

        private void GvImport_MasterRowGetRelationCount(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 1;
        }

        private void GvImport_MasterRowGetRelationName(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "Chi tiết phiếu nhập";
        }
        private void DeleteImport()
        {
            int row = gvImport.FocusedRowHandle;
            if (row >= 0)
            {
                int mapn = int.Parse(gvImport.GetRowCellValue(row, "MAPN").ToString());
                if (XtraMessageBox.Show("Bạn chắc chắn xoá phiếu nhập " + mapn + "?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    int i = NhapKhoBUS.Instances.Delete(mapn);
                    if (i != -1)
                    {
                        XtraMessageBox.Show("Xoá phiếu nhập thành công.", "Thông báo");
                        gcImport.DataSource = NhapKhoBUS.Instances.GetNhapKhos(true);
                    }
                    else
                        XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteImport();
        }

        private void GcImport_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gvImport.State != GridState.Editing)
                DeleteImport();
        }

        private void BtnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int row = gvImport.FocusedRowHandle;
            if (row >= 0)
            {
                int mapn = int.Parse(gvImport.GetRowCellValue(row, "MAPN").ToString());
                lstDetailImport = ChiTietNKBUS.Instances.GetChiTietNKs__(mapn);
                dynamic nk = NhapKhoBUS.Instances.FindOrderCode(mapn);
                var rp = new rpImport
                {
                    DataSource = lstDetailImport
                };
                rp.lbNguoiLap.Value = frm.nv.TENNV;
                rp.lbCodeImport.Value = "BÁO CÁO PHIẾU NHẬP " + mapn;
                rp.lbDate.Value = nk.NGAYNHAP;
                rp.lbStaff.Value = nk.NHANVIEN.TENNV;
                rp.ShowPreviewDialog();
            }
        }

        private void GvImport_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;
            e.Info.DisplayText = (e.RowHandle + 1) + "";
        }

        private void GvImportDetail_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {

            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;
            e.Info.DisplayText = (e.RowHandle + 1) + "";
        }
    }
}
