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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DoAn.FRM;

namespace DoAn.UC
{
    public partial class Uc_import_employee : DevExpress.XtraEditors.XtraUserControl
    {
        readonly FrmMain frm;
        public Uc_import_employee(FrmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        //load data khi form khởi chạy
        private void Uc_import_employee_Load(object sender, EventArgs e)
        {
            //load danh sách hoá đơn chưa thanh toán
            gcImport.DataSource = NhapKhoBUS.Instances.GetNhapKhos( false);
            lkLinhKien.DataSource = LinhKienBUS.Instances.GetLinhKiens();
            lkLinhKien.DisplayMember = "TENLINHKIEN";
            lkLinhKien.ValueMember = "MALINHKIEN";
            gvImportDetail.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
            gvImport.IndicatorWidth = 50;
            gvImportDetail.IndicatorWidth = 50;
        }
        //xoá data gridview chi tiết nhập kho
        void ClearDataGVImportDetail()
        {
            gcImportDetail.DataSource = null;
            layoutGroupImportDetail.Enabled = false;
            txtTienPhaiTra.Text = "";

        }
        //đóng form nhập hàng
        private void BtnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm.CLOSE();
        }
        //gọi các chi tiết của 1 phiếu nhập có mã phiếu nhập truyền vào
        private void CallDataGVImportDetail(int mapn)
        {
            gcImportDetail.DataSource = ChiTietNKBUS.Instances.GetChiTietNKs( mapn);
            lkLinhKien.DataSource = LinhKienBUS.Instances.GetLinhKiens();
            lkLinhKien.DisplayMember = "TENLINHKIEN";
            lkLinhKien.ValueMember = "MALINHKIEN";
            layoutGroupImportDetail.Enabled = true;
            layoutGroupImportDetail.Text = "Chi tiết phiếu nhập " + mapn;
            txtTienPhaiTra.Text = Support.ConvertVND(gvImport.GetRowCellValue(gvImport.FocusedRowHandle, "tongtien").ToString());
        }
        //click 1 dòng trong gridview nhập kho
        private void GvImport_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.RowHandle > -1)
                CallDataGVImportDetail(int.Parse(gvImport.GetRowCellValue(e.RowHandle, "MAPN").ToString()));
        }
        //tạo 1 hoá đơn mới cho khách hàng       
        private void BtnCreate_Click(object sender, EventArgs e)
        {
            int i = NhapKhoBUS.Instances.Insert(frm.nv.MANV);
            if (i != -1)
            {
                XtraMessageBox.Show("Tạo phiếu nhập thành công.", "Thông báo");
                gcImport.DataSource = NhapKhoBUS.Instances.GetNhapKhos( false);
                gvImport.FocusedRowHandle = gvImport.RowCount - 1;
                CallDataGVImportDetail(NhapKhoBUS.Instances.LayHDVuaTao().MAPN);
            }
        }
        //huỷ 1 phiếu trong gridview nhập kho
        private void DestroyImport()
        {
            var mapn = gvImport.GetRowCellValue(gvImport.FocusedRowHandle, "MAPN");
            if (mapn != null)
            {
                if (XtraMessageBox.Show("Bạn chắc chắn huỷ phiếu nhập " + mapn.ToString() + "?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    int i = NhapKhoBUS.Instances.Delete(int.Parse(mapn.ToString()));
                    if (i != -1)
                    {
                        XtraMessageBox.Show("Huỷ phiếu nhập thành công " + mapn + ".", "Thông báo");
                        gcImport.DataSource = NhapKhoBUS.Instances.GetNhapKhos( false);
                        ClearDataGVImportDetail();
                    }
                    else
                        XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //sự kiện gọi hàm huỷ phiếu nhập
        private void BtnDestroy_Click(object sender, EventArgs e)
        {
            DestroyImport();
        }
        //click nút delete xoá 1 dòng trong chi tiết hoá đơn
        private void GcImportDetail_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gvImportDetail.State != GridState.Editing)
            {
                var mapn = gvImportDetail.GetRowCellValue(gvImportDetail.FocusedRowHandle, "MAPN");
                var malinhkien = gvImportDetail.GetRowCellValue(gvImportDetail.FocusedRowHandle, "MALINHKIEN");
                if (mapn != null)
                {
                    if (XtraMessageBox.Show("Bạn chắc chắn xoá linh kiện " + LinhKienBUS.Instances.TimTheoMa(int.Parse(malinhkien.ToString())).TENLINHKIEN + "?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        int i = ChiTietNKBUS.Instances.Delete(int.Parse(mapn.ToString()), int.Parse(malinhkien.ToString()));
                        if (i != -1)
                        {
                            XtraMessageBox.Show("Xoá thành công.", "Thông báo");
                            gcImportDetail.DataSource = ChiTietNKBUS.Instances.GetChiTietNKs( int.Parse(mapn.ToString()));
                            gcImport.DataSource = NhapKhoBUS.Instances.GetNhapKhos( false);
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        //ngăn không cho thao tác khi thêm sửa 1 dòng trong bảng ctnk khi dữ liệu sai
        private void GvImportDetail_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        //thêm sửa 1 dòng trong bảng chi tiết nhập kho
        private void GvImportDetail_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            string sErr = "";
            bool bVali = true;
            if (gvImportDetail.GetRowCellValue(e.RowHandle, "MALINHKIEN").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng chọn linh kiện.\n";
            }
            if (gvImportDetail.GetRowCellValue(e.RowHandle, "SOLUONG").ToString().Trim() == "")
            {
                bVali = false;
                sErr += "Vui lòng điền số lượng.\n";
            }
            if (gvImportDetail.GetRowCellValue(e.RowHandle, "DONGIA").ToString().Trim() == "")
            {
                bVali = false;
                sErr += "Vui lòng điền đơn giá.\n";

            }
            else
              if (int.Parse(gvImportDetail.GetRowCellValue(e.RowHandle, "DONGIA").ToString().Trim()) <= 0)
            {
                bVali = false;
                sErr += "Đơn giá phải lớn hơn 0.\n";
            }

            if (bVali)
            {
                //thêm mới
                if (e.RowHandle < 0)
                {

                    if (int.Parse(gvImportDetail.GetRowCellValue(e.RowHandle, "SOLUONG").ToString().Trim()) <= 0)
                    {
                        bVali = false;
                        sErr += "Số lượng phải lớn hơn 0.\n";
                    }

                    if (!bVali)
                    {
                        e.Valid = false;
                        XtraMessageBox.Show(sErr, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int i = ChiTietNKBUS.Instances.Insert(
                     int.Parse(gvImport.GetRowCellValue(gvImport.FocusedRowHandle, "MAPN").ToString()),
                     int.Parse(gvImportDetail.GetRowCellValue(e.RowHandle, "MALINHKIEN").ToString().Trim()),
                     int.Parse(gvImportDetail.GetRowCellValue(e.RowHandle, "SOLUONG").ToString().Trim()),
                     double.Parse(gvImportDetail.GetRowCellValue(e.RowHandle, "DONGIA").ToString().Trim()));
                    if (i != -1)
                        XtraMessageBox.Show("Thêm thành công", "Thông báo", DevExpress.Utils.DefaultBoolean.True);
                    int row = gvImport.FocusedRowHandle;
                    int mapn = int.Parse(gvImport.GetRowCellValue(row, "MAPN").ToString());
                    gcImport.DataSource = NhapKhoBUS.Instances.GetNhapKhos( false);
                    gvImport.FocusedRowHandle = row;
                    CallDataGVImportDetail(mapn);
                }
                //sửa 
                else
                {
                    ChiTietNKBUS.Instances.Update(
                                 int.Parse(gvImport.GetRowCellValue(gvImport.FocusedRowHandle, "MAPN").ToString()),
                                int.Parse(gvImportDetail.GetRowCellValue(e.RowHandle, "MALINHKIEN").ToString().Trim()),
                                int.Parse(gvImportDetail.GetRowCellValue(e.RowHandle, "SOLUONG").ToString().Trim()),
                                double.Parse(gvImportDetail.GetRowCellValue(e.RowHandle, "DONGIA").ToString().Trim()));
                    int row = gvImport.FocusedRowHandle;
                    int mapn = int.Parse(gvImport.GetRowCellValue(row, "MAPN").ToString());
                    gcImport.DataSource = NhapKhoBUS.Instances.GetNhapKhos( false);
                    gvImport.FocusedRowHandle = row;
                    CallDataGVImportDetail(mapn);

                }
            }
            else
            {
                e.Valid = false;
                XtraMessageBox.Show(sErr, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //thanh toán 1 phiếu nhập
        private void BtnThanhToan_Click(object sender, EventArgs e)
        {
            if (txtTienPhaiTra.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show("Mời bạn chọn phiếu nhập muốn thanh toán.", "Thông báo");
                return;
            }
            double tienPhaiTra = double.Parse(gvImport.GetRowCellValue(gvImport.FocusedRowHandle, "tongtien").ToString());

            if (tienPhaiTra == 0)
            {
                XtraMessageBox.Show("Phiếu nhập chưa có sản phẩm không cần thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int i = NhapKhoBUS.Instances.Update(int.Parse(gvImport.GetRowCellValue(gvImport.FocusedRowHandle, "MAPN").ToString()), true);
            if (i != -1)
            {
                XtraMessageBox.Show("Thanh toán thành công.", "Thông báo");
                gcImport.DataSource = NhapKhoBUS.Instances.GetNhapKhos( false);
                ClearDataGVImportDetail();
            }
        }
        //xoá 1 phiếu nhập bằng nút delete
        private void GcImport_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //DestroyImport();
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
