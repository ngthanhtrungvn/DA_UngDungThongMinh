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
using DevExpress.XtraPrinting.Native;
using System.IO;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraPrinting;
using DoAn.FRM;
using BUS;

namespace DoAn.UC
{
    public partial class Uc_product : DevExpress.XtraEditors.XtraUserControl
    {
        readonly FrmMain frm;
        readonly ImageCollection images = new ImageCollection();
        OpenFileDialog open;
        public Uc_product(FrmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        private void Uc_product_Load(object sender, EventArgs e)
        {
            gcProduct.DataSource = LinhKienBUS.Instances.GetLinhKiens();
            lkLoaiLK.DataSource = LoaiLKBUS.Instances.GetLoaiLKs();
            lkLoaiLK.DisplayMember = "TENLOAI";
            lkLoaiLK.ValueMember = "MALOAI";
            gvProduct.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gcTypeProduct.DataSource = LoaiLKBUS.Instances.GetLoaiLKs();
            gvTypeProduct.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvProduct.IndicatorWidth = 30;
            gvTypeProduct.IndicatorWidth = 30;
        }
        private void BtnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm.CLOSE();
        }
        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                DataRow dr = gvProduct.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá linh kiện " + dr["TENLINHKIEN"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int i = LinhKienBUS.Instances.Delete(int.Parse(dr["MALINHKIEN"].ToString()));
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Xoá thành công.", "Thông báo");
                            gcProduct.DataSource = LinhKienBUS.Instances.GetLinhKiens();
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                DataRow dr = gvTypeProduct.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá loại linh kiện " + dr["TENLOAI"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int i = LoaiLKBUS.Instances.Delete(int.Parse(dr["MALOAI"].ToString()));
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Xoá thành công", "Thông báo");
                            gcTypeProduct.DataSource = LoaiLKBUS.Instances.GetLoaiLKs();
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void GvProduct_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        private void ImgHinhAnh_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(open.FileName);
                if (!File.Exists("../../Images/" + open.SafeFileName))
                {
                    pictureBox1.Image.Save("../../Images/" + open.SafeFileName);
                }
                try
                {
                    int i = gvProduct.GetSelectedRows()[0];
                    int j = LinhKienBUS.Instances.Update(
                           gvProduct.GetRowCellValue(i, "TENLINHKIEN").ToString().Trim()
                            , int.Parse(gvProduct.GetRowCellValue(i, "MALOAI").ToString().Trim())
                            , gvProduct.GetRowCellValue(i, "HANGSX").ToString().Trim()
                            , double.Parse(gvProduct.GetRowCellValue(i, "DONGIA").ToString().Trim())
                            , open.SafeFileName
                            , int.Parse(gvProduct.GetRowCellValue(i, "SOLUONGCON").ToString().Trim())
                        , int.Parse(gvProduct.GetRowCellValue(i, "MALINHKIEN").ToString().Trim()));
                    if (j == 1)
                        gcProduct.DataSource = LinhKienBUS.Instances.GetLinhKiens();
                    else
                        XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    open = null;
                }
                catch (Exception)
                {
                }
            }
        }
        private void GvProduct_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "HINHANH")
            {
                try
                {
                    if (e.RowHandle >= 0)
                    {
                        Image img = Image.FromFile("../../Images/" + gvProduct.GetRowCellValue(e.RowHandle, "HINHANH").ToString());
                        images.Images.Clear();
                        images.Images.Add(img);
                    }
                }
                catch (Exception)
                {
                    Image img = Image.FromFile("../../Images/loadImg.png");
                    images.Images.Clear();
                    images.Images.Add(img);
                }
                imgHinhAnh.Images = images;
            }
        }
        private void GcProduct_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gvProduct.State != GridState.Editing)
            {
                DataRow dr = gvProduct.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá linh kiện " + dr["TENLINHKIEN"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int i = LinhKienBUS.Instances.Delete(int.Parse(dr["MALINHKIEN"].ToString()));
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Xoá thành công", "Thông báo");
                            gcProduct.DataSource = LinhKienBUS.Instances.GetLinhKiens();
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void GvProduct_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            string sErr = "";
            bool bVali = true;
            if (gvProduct.GetRowCellValue(e.RowHandle, "TENLINHKIEN").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng điền tên linh kiện.\n";
            }
            if (gvProduct.GetRowCellValue(e.RowHandle, "MALOAI").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng chọn loại linh kiện.\n";
            }
            if (gvProduct.GetRowCellValue(e.RowHandle, "HANGSX").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng điền tên hãng sản xuất.\n";
            }
            if (gvProduct.GetRowCellValue(e.RowHandle, "DONGIA").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng điền đơn giá.\n";
            }
            if (bVali)
            {
                //thêm mới
                if (e.RowHandle < 0)
                {
                    try
                    {
                        int i = LinhKienBUS.Instances.Insert(
                              gvProduct.GetRowCellValue(e.RowHandle, "TENLINHKIEN").ToString().Trim()
                               , int.Parse(gvProduct.GetRowCellValue(e.RowHandle, "MALOAI").ToString().Trim())
                               , gvProduct.GetRowCellValue(e.RowHandle, "HANGSX").ToString().Trim()
                               , double.Parse(gvProduct.GetRowCellValue(e.RowHandle, "DONGIA").ToString().Trim())
                               , open == null || open.SafeFileName == null ? gvProduct.GetRowCellValue(e.RowHandle, "HINHANH").ToString() : open.SafeFileName
                               , 0);
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Thêm thành công", "Thông báo", DefaultBoolean.True);
                            gcProduct.DataSource = LinhKienBUS.Instances.GetLinhKiens();
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception)
                    {
                    }
                    open = null;
                }
                //sửa 
                else
                {
                    try
                    {
                        int i = LinhKienBUS.Instances.Update(
                               gvProduct.GetRowCellValue(e.RowHandle, "TENLINHKIEN").ToString().Trim()
                                , int.Parse(gvProduct.GetRowCellValue(e.RowHandle, "MALOAI").ToString().Trim())
                                , gvProduct.GetRowCellValue(e.RowHandle, "HANGSX").ToString().Trim()
                                , double.Parse(gvProduct.GetRowCellValue(e.RowHandle, "DONGIA").ToString().Trim())
                                , open == null || open.SafeFileName == null ? gvProduct.GetRowCellValue(e.RowHandle, "HINHANH").ToString() : open.SafeFileName
                                , 0
                            , int.Parse(gvProduct.GetRowCellValue(e.RowHandle, "MALINHKIEN").ToString().Trim()));
                        if (i == 1)
                            gcProduct.DataSource = LinhKienBUS.Instances.GetLinhKiens();
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception)
                    {
                    }
                    open = null;
                }
            }
            else
            {
                e.Valid = false;
                XtraMessageBox.Show(sErr, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GcTypeProduct_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gvProduct.State != GridState.Editing)
            {
                DataRow dr = gvTypeProduct.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá loại linh kiện " + dr["TENLOAI"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int i = LoaiLKBUS.Instances.Delete(int.Parse(dr["MALOAI"].ToString()));
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Xoá thành công", "Thông báo");
                            gcTypeProduct.DataSource = LoaiLKBUS.Instances.GetLoaiLKs();
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        //hàm ngăn không cho chuyển dòng khác khi insert hoặc update sai dữ liệu bảng loại linh kiện
        private void GvTypeProduct_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        //thêm sửa bảng loại linh kiện
        private void GvTypeProduct_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            string sErr = "";
            bool bVali = true;
            if (gvTypeProduct.GetRowCellValue(e.RowHandle, "TENLOAI").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng điền tên loại linh kiện.\n";
            }
            if (bVali)
            {
                //thêm mới
                if (e.RowHandle < 0)
                {
                    try
                    {
                        int i = LoaiLKBUS.Instances.Insert(gvTypeProduct.GetRowCellValue(e.RowHandle, "TENLOAI").ToString().Trim());
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Thêm thành công", "Thông báo", DefaultBoolean.True);
                            gcTypeProduct.DataSource = LoaiLKBUS.Instances.GetLoaiLKs();
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
                        int i = LoaiLKBUS.Instances.Update(gvTypeProduct.GetRowCellValue(e.RowHandle, "TENLOAI").ToString().Trim(), int.Parse(gvTypeProduct.GetRowCellValue(e.RowHandle, "MALOAI").ToString().Trim()));
                        if (i == 1)
                            gcTypeProduct.DataSource = LoaiLKBUS.Instances.GetLoaiLKs();
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
        //xuất ra file excel linh kiện hoặc loại linh kiện
        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xls",
                Title = "Xuất ra file excel"
            };
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string str = "linh kiện";
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                    gvProduct.ExportToXls(sf.FileName);
                else
                {
                    gvTypeProduct.ExportToXls(sf.FileName);
                    str = "loại linh kiện";
                }
                XtraMessageBox.Show("Xuất file excel " + str + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        //xuất ra file Word linh kiện hoặc loại linh kiện
        private void BtnWord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog
            {
                Filter = "Word Files (*.docx)|*.docx",
                Title = "Xuất ra file word"
            };
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string str = "linh kiện";
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                    gvProduct.ExportToDocx(sf.FileName);
                else
                {
                    gvTypeProduct.ExportToDocx(sf.FileName);
                    str = "loại linh kiện";
                }
                XtraMessageBox.Show("Xuất file word " + str + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        //xuất ra file Pdf linh kiện hoặc loại linh kiện
        private void BtnPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog
            {
                Filter = "Pdf Files (*.pdf)|*.pdf",
                Title = "Xuất ra file pdf"
            };
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string str = "linh kiện";
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                    gvProduct.ExportToPdf(sf.FileName);
                else
                {
                    gvTypeProduct.ExportToPdf(sf.FileName);
                    str = "loại linh kiện";
                }
                XtraMessageBox.Show("Xuất file pdf " + str + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        private void GvProduct_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;
            e.Info.DisplayText = (e.RowHandle + 1) + "";
        }
        private void GvTypeProduct_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;
            e.Info.DisplayText = (e.RowHandle + 1) + "";
        }
    }
}
