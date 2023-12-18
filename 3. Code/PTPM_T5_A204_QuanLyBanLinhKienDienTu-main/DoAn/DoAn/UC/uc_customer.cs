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
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraReports.UI;
using DoAn.FRM;
using BUS;

namespace DoAn.UC
{
    public partial class Uc_customer : XtraUserControl
    {
        readonly FrmMain frm;
        public Uc_customer(FrmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        private void Uc_customer_Load(object sender, EventArgs e)
        {
            gcCustomer.DataSource = KhachHangBUS.Instances.GetKhachHangs();
            lkLoaiKH.DataSource = LoaiKHBUS.Instances.GetLoaiKHs();
            lkLoaiKH.DisplayMember = "TENLOAIKH";
            lkLoaiKH.ValueMember = "MALOAIKH";
            gvCustomer.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
            gcTypeCustomer.DataSource = LoaiKHBUS.Instances.GetLoaiKHs();
            gvTypeCustomer.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
            gvCustomer.IndicatorWidth = 50;
            gvTypeCustomer.IndicatorWidth = 50;
        }
        private void BtnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm.CLOSE();
        }
        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                DataRow dr = gvCustomer.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá khách hàng " + dr["TENKH"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        int i = KhachHangBUS.Instances.Delete(int.Parse(dr["MAKH"].ToString()));
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Xoá thành công.", "Thông báo");
                            gcCustomer.DataSource = KhachHangBUS.Instances.GetKhachHangs();
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                DataRow dr = gvTypeCustomer.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá loại khách hàng " + dr["TENLOAIKH"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        int i = LoaiKHBUS.Instances.Delete(int.Parse(dr["MALOAIKH"].ToString()));
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Xoá thành công", "Thông báo");
                            gcTypeCustomer.DataSource = LoaiKHBUS.Instances.GetLoaiKHs();
                            lkLoaiKH.DataSource = LoaiKHBUS.Instances.GetLoaiKHs();
                            lkLoaiKH.DisplayMember = "TENLOAIKH";
                            lkLoaiKH.ValueMember = "MALOAIKH";
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xls",
                Title = "Xuất ra file excel"
            };
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string str = "khách hàng";
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                    gvCustomer.ExportToXls(sf.FileName);
                else
                {
                    gvTypeCustomer.ExportToXls(sf.FileName);
                    str = "loại khách hàng";
                }
                XtraMessageBox.Show("Xuất file excel " + str + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        private void BtnWord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog
            {
                Filter = "Word Files (*.docx)|*.docx",
                Title = "Xuất ra file word"
            };
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string str = "khách hàng";
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                    gvCustomer.ExportToDocx(sf.FileName);
                else
                {
                    gvTypeCustomer.ExportToDocx(sf.FileName);
                    str = "loại khách hàng";
                }
                XtraMessageBox.Show("Xuất file word " + str + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        private void BtnPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog
            {
                Filter = "Pdf Files (*.pdf)|*.pdf",
                Title = "Xuất ra file pdf"
            };
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string str = "khách hàng";
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                    gvCustomer.ExportToPdf(sf.FileName);
                else
                {
                    gvTypeCustomer.ExportToPdf(sf.FileName);
                    str = "loại khách hàng";
                }
                XtraMessageBox.Show("Xuất file pdf " + str + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        private void GcCustomer_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gvCustomer.State != GridState.Editing)
            {
                DataRow dr = gvCustomer.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá khách hàng " + dr["TENKH"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        int i = KhachHangBUS.Instances.Delete(int.Parse(dr["MAKH"].ToString()));
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Xoá thành công", "Thông báo");
                            gcCustomer.DataSource = KhachHangBUS.Instances.GetKhachHangs();
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }
        private void GvCustomer_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;

        }
        private void GvCustomer_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            string sErr = "";
            bool bVali = true;
            if (gvCustomer.GetRowCellValue(e.RowHandle, "TENKH").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng điền tên khách hàng.\n";
            }
            if (gvCustomer.GetRowCellValue(e.RowHandle, "DIACHI").ToString().Trim() == "")
            {
                bVali = false;
                sErr += "Vui lòng điền địa chỉ.\n";
            }
            if (gvCustomer.GetRowCellValue(e.RowHandle, "SDT").ToString().Trim() == "")
            {
                bVali = false;
                sErr += "Vui lòng điền số điện thoại.\n";
            }
            if (gvCustomer.GetRowCellValue(e.RowHandle, "maloaikh").ToString().Trim() == "")
            {
                bVali = false;
                sErr += "Vui lòng chọn loại khách hàng.\n";
            }
            if (bVali)
            {
                //thêm mới
                if (e.RowHandle < 0)
                {
                    try
                    {
                        int i = KhachHangBUS.Instances.Insert(gvCustomer.GetRowCellValue(e.RowHandle, "TENKH").ToString().Trim()
                             , gvCustomer.GetRowCellValue(e.RowHandle, "GIOITINH") != null && gvCustomer.GetRowCellValue(e.RowHandle, "GIOITINH").ToString() != "" && bool.Parse(gvCustomer.GetRowCellValue(e.RowHandle, "GIOITINH").ToString().Trim())
                             , gvCustomer.GetRowCellValue(e.RowHandle, "DIACHI").ToString()
                             , gvCustomer.GetRowCellValue(e.RowHandle, "SDT").ToString()
                             , int.Parse(gvCustomer.GetRowCellValue(e.RowHandle, "maloaikh").ToString().Trim()));
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Thêm thành công.", "Thông báo", DevExpress.Utils.DefaultBoolean.True);
                            gcCustomer.DataSource = KhachHangBUS.Instances.GetKhachHangs();
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception)
                    {
                    }
                }
                //sửa 
                else
                {
                    try
                    {
                        int i = KhachHangBUS.Instances.Update(gvCustomer.GetRowCellValue(e.RowHandle, "TENKH").ToString().Trim()
                             , bool.Parse(gvCustomer.GetRowCellValue(e.RowHandle, "GIOITINH").ToString().Trim())
                             , gvCustomer.GetRowCellValue(e.RowHandle, "DIACHI").ToString()
                             , gvCustomer.GetRowCellValue(e.RowHandle, "SDT").ToString()
                             , int.Parse(gvCustomer.GetRowCellValue(e.RowHandle, "maloaikh").ToString().Trim())
                             , int.Parse(gvCustomer.GetRowCellValue(e.RowHandle, "Makh").ToString().Trim()));
                        if (i == 1)
                            gcCustomer.DataSource = KhachHangBUS.Instances.GetKhachHangs();
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception)
                    {
                    }

                }
            }
            else
            {
                e.Valid = false;
                XtraMessageBox.Show(sErr, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GvCustomer_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;
            e.Info.DisplayText = (e.RowHandle + 1) + "";
        }
        private void GcTypeCustomer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gvCustomer.State != GridState.Editing)
            {
                DataRow dr = gvTypeCustomer.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá loại khách hàng " + dr["TENLOAIKH"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        int i = LoaiKHBUS.Instances.Delete(int.Parse(dr["MALOAIKH"].ToString()));
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Xoá thành công", "Thông báo");
                            gcTypeCustomer.DataSource = LoaiKHBUS.Instances.GetLoaiKHs();
                            lkLoaiKH.DataSource = LoaiKHBUS.Instances.GetLoaiKHs();
                            lkLoaiKH.DisplayMember = "TENLOAIKH";
                            lkLoaiKH.ValueMember = "MALOAIKH";
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }
        private void GvTypeCustomer_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;

        }
        private void GvTypeCustomer_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            string sErr = "";
            bool bVali = true;
            if (gvTypeCustomer.GetRowCellValue(e.RowHandle, "TENLOAIKH").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng điền tên loại.\n";
            }
            if (gvTypeCustomer.GetRowCellValue(e.RowHandle, "GIAMGIA").ToString().Trim() == "")
            {
                bVali = false;
                sErr += "Vui lòng điền giảm giá.\n";
            }
            if (bVali)
            {

                //thêm mới
                if (e.RowHandle < 0)
                {
                    try
                    {
                        int i = LoaiKHBUS.Instances.Insert(gvTypeCustomer.GetRowCellValue(e.RowHandle, "TENLOAIKH").ToString().Trim()
                              , double.Parse(gvTypeCustomer.GetRowCellValue(e.RowHandle, "GIAMGIA").ToString().Trim()));
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Thêm thành công", "Thông báo", DefaultBoolean.True);
                            gcTypeCustomer.DataSource = LoaiKHBUS.Instances.GetLoaiKHs();
                            lkLoaiKH.DataSource = LoaiKHBUS.Instances.GetLoaiKHs();
                            lkLoaiKH.DisplayMember = "TENLOAIKH";
                            lkLoaiKH.ValueMember = "MALOAIKH";
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    catch (Exception)
                    {

                    }
                }
                //sửa 
                else
                {
                    try
                    {
                        int i = LoaiKHBUS.Instances.Update(gvTypeCustomer.GetRowCellValue(e.RowHandle, "TENLOAIKH").ToString().Trim()
                             , double.Parse(gvTypeCustomer.GetRowCellValue(e.RowHandle, "GIAMGIA").ToString().Trim())
                             , int.Parse(gvTypeCustomer.GetRowCellValue(e.RowHandle, "MALOAIKH").ToString().Trim()));
                        if (i == 1)
                        {
                            gcTypeCustomer.DataSource = LoaiKHBUS.Instances.GetLoaiKHs();
                            lkLoaiKH.DataSource = LoaiKHBUS.Instances.GetLoaiKHs();
                            lkLoaiKH.DisplayMember = "TENLOAIKH";
                            lkLoaiKH.ValueMember = "MALOAIKH";
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception)
                    {
                    }

                }
            }
            else
            {
                e.Valid = false;
                XtraMessageBox.Show(sErr, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GvTypeCustomer_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;
            e.Info.DisplayText = (e.RowHandle + 1) + "";
        }
    }
}
