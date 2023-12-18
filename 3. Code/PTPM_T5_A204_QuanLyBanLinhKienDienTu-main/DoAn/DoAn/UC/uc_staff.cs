using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Controls;
using System.IO;
using DoAn.FRM;
using BUS;

namespace DoAn.UC
{
    public partial class Uc_staff : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly FrmMain frm;
        private readonly ImageCollection images = new ImageCollection(); //{ ImageSize=new Size(20, 20) };
        private OpenFileDialog open;
        public Uc_staff(FrmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm.CLOSE();
        }
        private void Uc_staff_Load(object sender, EventArgs e)
        {
            gcStaff.DataSource = NhanVienBUS.Instances.GetNVs();
            lkQuyen.DataSource = QuyenBUS.Instances.GetQuyens();
            lkQuyen.DisplayMember = "tenquyen";
            lkQuyen.ValueMember = "maquyen";

            gcRole.DataSource = QuyenBUS.Instances.GetQuyens();

            gvStaff.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
            gvRole.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvRole.IndicatorWidth = 50;
            gvStaff.IndicatorWidth = 50;
        }
        private void BtnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //nhân viên
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                DataRow dr = gvStaff.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá nhân viên " + dr["TENNV"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int i = NhanVienBUS.Instances.Delete(int.Parse(dr["MANV"].ToString()));
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Xoá thành công", "Thông báo");
                            gcStaff.DataSource = NhanVienBUS.Instances.GetNVs();
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            //quyền
            else
            {
                DataRow dr = gvRole.GetFocusedDataRow();
                if (dr != null)
                {
                    if (dr["tenquyen"].ToString().ToLower().Equals("admin") || dr["tenquyen"].ToString().ToLower().Equals("nhân viên"))
                        return;
                    if (XtraMessageBox.Show("Bạn có muốn xoá quyền " + dr["tenquyen"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        QuyenBUS.Instances.Delete(int.Parse(dr["maquyen"].ToString()));
                        XtraMessageBox.Show("Xoá thành công", "Thông báo");
                        gcRole.DataSource = QuyenBUS.Instances.GetQuyens();
                    }
                }
            }

        }
        private void BtnReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                DataRow dr = gvStaff.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn reset mật khẩu nhân viên " + dr["TENNV"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        int manv = int.Parse(dr["MANV"].ToString());
                        int i = NhanVienBUS.Instances.ResetPass(manv);
                        if (i == 1)
                        {
                            if (manv == frm.nv.MANV)
                            {
                                XtraMessageBox.Show("Reset mật khẩu thành công. Mật khẩu mới là 12345", "Thông báo");
                                XtraMessageBox.Show("Vui lòng đăng nhập lại.", "Thông báo");
                                frm.Logout(1);
                            }
                            else
                            {
                                XtraMessageBox.Show("Reset mật khẩu thành công. Mật khẩu mới là 12345", "Thông báo");
                                gcStaff.DataSource = NhanVienBUS.Instances.GetNVs();
                            }
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        //load hình ảnh
        private void GvStaff_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "HINHANH")
            {
                try
                {
                    if (e.RowHandle >= 0)
                    {
                        Image img = Image.FromFile("../../Images/" + gvStaff.GetDataRow(e.RowHandle)["HINHANH"].ToString());
                        images.Images.Clear();
                        images.Images.Add(img);
                    }
                }
                catch (Exception)
                {
                    Image img = Image.FromFile("../../Images/loadImg.png");
                    images.Images.Clear();
                    //    images.ImageSize = new Size(100, 100);
                    images.Images.Add(img);
                }
                imgHinhAnh.Images = images;
            }
        }
        //thay đổi hình ảnh nhân viên
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
                    DataRow dr = gvStaff.GetFocusedDataRow();
                    int i = NhanVienBUS.Instances.Update(dr["TENNV"].ToString().Trim(), dr["DIACHI"].ToString().Trim()
                     , dr["SDT"].ToString().Trim(), bool.Parse(dr["GIOITINH"].ToString().Trim()), DateTime.Parse(dr["NGAYVL"].ToString().Trim())
                     , double.Parse(dr["LUONG"].ToString().Trim()), open.SafeFileName
                     , int.Parse(dr["maquyen"].ToString().Trim()), int.Parse(dr["MANV"].ToString().Trim()));
                    if (i == 1)
                        gcStaff.DataSource = NhanVienBUS.Instances.GetNVs();
                    else
                        XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    open = null;
                }
                catch (Exception)
                {
                }
            }


        }
        //phím delete xoá nhân viên
        private void GcStaff_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gvStaff.State != GridState.Editing)
            {
                DataRow dr = gvStaff.GetFocusedDataRow();
                if (dr != null)
                {
                    if (XtraMessageBox.Show("Bạn có muốn xoá nhân viên " + dr["TENNV"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int manv = int.Parse(dr["MANV"].ToString());
                        if (frm.nv.MANV == manv)
                        {
                            XtraMessageBox.Show("Tài khoản đang đăng nhập không được phép xoá.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        int i = NhanVienBUS.Instances.Delete(manv);
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Xoá thành công", "Thông báo");
                            gcStaff.DataSource = NhanVienBUS.Instances.GetNVs();
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        //ngăn ko cho chuyển dòng khi sai dữ liệu nhân viên
        private void GvStaff_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        //thêm sửa dữ liệu nhân viên
        private void GvStaff_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            string sErr = "";
            bool bVali = true;
            if (gvStaff.GetRowCellValue(e.RowHandle, "TENNV").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng điền tên nhân viên.\n";
            }
            if (gvStaff.GetRowCellValue(e.RowHandle, "DIACHI").ToString() == "")
            {
                bVali = false;
                sErr += "Vui lòng điền địa chỉ.\n";
            }
            if (gvStaff.GetRowCellValue(e.RowHandle, "SDT").ToString() == "")
            {
                bVali = false;
                sErr += "Vui lòng điền số điện thoại.\n";
            }
            if (gvStaff.GetRowCellValue(e.RowHandle, "NGAYVL").ToString() == "")
            {
                bVali = false;
                sErr += "Vui lòng điền ngày vào làm.\n";
            }
            if (gvStaff.GetRowCellValue(e.RowHandle, "LUONG").ToString() == "")
            {
                bVali = false;
                sErr += "Vui lòng điền lương.\n";
            }
            if (gvStaff.GetRowCellValue(e.RowHandle, "maquyen").ToString() == "")
            {
                bVali = false;
                sErr += "Vui lòng chọn quyền.";
            }
            if (bVali)
            {
                if (gvStaff.GetRowCellValue(e.RowHandle, "taikhoan").ToString() == "")
                {
                    bVali = false;
                    sErr += "Vui lòng điền tài khoản.\n";
                }
                if (!bVali)
                {
                    e.Valid = false;

                    XtraMessageBox.Show(sErr, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //thêm mới
                if (e.RowHandle < 0)
                {
                    try
                    {
                        int i = NhanVienBUS.Instances.Insert(gvStaff.GetRowCellValue(e.RowHandle, "TENNV").ToString().Trim()
                              , gvStaff.GetRowCellValue(e.RowHandle, "DIACHI").ToString().Trim()
                           , gvStaff.GetRowCellValue(e.RowHandle, "SDT").ToString().Trim()
                           , gvStaff.GetRowCellValue(e.RowHandle, "GIOITINH") != null && gvStaff.GetRowCellValue(e.RowHandle, "GIOITINH").ToString() != "" && bool.Parse(gvStaff.GetRowCellValue(e.RowHandle, "GIOITINH").ToString().Trim())
                           , DateTime.Parse(gvStaff.GetRowCellValue(e.RowHandle, "NGAYVL").ToString().Trim())
                           , double.Parse(gvStaff.GetRowCellValue(e.RowHandle, "LUONG").ToString().Trim())
                           , open == null || open.SafeFileName == null ? gvStaff.GetRowCellValue(e.RowHandle, "HINHANH").ToString() : open.SafeFileName
                           , gvStaff.GetRowCellValue(e.RowHandle, "taikhoan").ToString().Trim()
                           , int.Parse(gvStaff.GetRowCellValue(e.RowHandle, "maquyen").ToString().Trim()));
                        open = null;
                        if (i == 1)
                            XtraMessageBox.Show("Thêm thành công. Mật khẩu mặc định là 12345", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    catch (Exception)
                    {

                    }
                    gcStaff.DataSource = NhanVienBUS.Instances.GetNVs();
                }
                //sửa 
                else
                {
                    int i = -1;
                    try
                    {
                        i = NhanVienBUS.Instances.Update(gvStaff.GetRowCellValue(e.RowHandle, "TENNV").ToString().Trim()
                                 , gvStaff.GetRowCellValue(e.RowHandle, "DIACHI").ToString().Trim()
                              , gvStaff.GetRowCellValue(e.RowHandle, "SDT").ToString().Trim()
                              , bool.Parse(gvStaff.GetRowCellValue(e.RowHandle, "GIOITINH").ToString().Trim())
                              , DateTime.Parse(gvStaff.GetRowCellValue(e.RowHandle, "NGAYVL").ToString().Trim())
                              , double.Parse(gvStaff.GetRowCellValue(e.RowHandle, "LUONG").ToString().Trim())
                              , open == null || open.SafeFileName == null ? gvStaff.GetRowCellValue(e.RowHandle, "HINHANH").ToString() : open.SafeFileName
                              , int.Parse(gvStaff.GetRowCellValue(e.RowHandle, "maquyen").ToString().Trim())
                              , int.Parse(gvStaff.GetRowCellValue(e.RowHandle, "MANV").ToString().Trim()));
                        open = null;
                    }
                    catch (Exception)
                    {
                    }
                    if (i == 1)
                        gcStaff.DataSource = NhanVienBUS.Instances.GetNVs();
                    else
                        XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {

                e.Valid = false;

                XtraMessageBox.Show(sErr, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //ngăn ko cho chuyển dòng khi sai dữ liệu trong bảng quyền
        private void GvRole_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        //phím delete xoá quyền
        private void GcRole_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gvStaff.State != GridState.Editing)
            {
                DataRow dr = gvRole.GetFocusedDataRow();
                if (dr != null)
                {
                    if (dr["tenquyen"].ToString().ToLower().Equals("admin") || dr["tenquyen"].ToString().ToLower().Equals("nhân viên"))
                        return;
                    if (XtraMessageBox.Show("Bạn có muốn xoá quyền " + dr["tenquyen"].ToString() + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int i = QuyenBUS.Instances.Delete(int.Parse(dr["maquyen"].ToString()));
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Xoá thành công.", "Thông báo");
                            gcRole.DataSource = QuyenBUS.Instances.GetQuyens();
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        //thêm sửa dữ liệu quyền
        private void GvRole_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            string sErr = "";
            bool bVali = true;
            if (gvRole.GetRowCellValue(e.RowHandle, "tenquyen").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng điền tên quyền.\n";
            }
            if (bVali)
            {
                //thêm mới
                if (e.RowHandle < 0)
                {
                    try
                    {
                        int i = QuyenBUS.Instances.Insert(gvRole.GetRowCellValue(e.RowHandle, "tenquyen").ToString().Trim());
                        if (i == 1)
                        {
                            XtraMessageBox.Show("Thêm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            gcRole.DataSource = QuyenBUS.Instances.GetQuyens();
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception)
                    {
                        XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                //sửa 
                else
                {
                    try
                    {
                        int i = QuyenBUS.Instances.Update(gvRole.GetRowCellValue(e.RowHandle, "tenquyen").ToString().Trim(), int.Parse(gvRole.GetRowCellValue(e.RowHandle, "maquyen").ToString().Trim()));
                        if (i == 1)
                            gcRole.DataSource = QuyenBUS.Instances.GetQuyens();
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
        //xuất ra file excel nhân viện hoặc quyền
        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xls",
                Title = "Xuất ra file excel"
            };
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string str = "nhân viên";
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                    gvStaff.ExportToXls(sf.FileName);
                else
                {
                    gvRole.ExportToXls(sf.FileName);
                    str = "quyền";
                }
                XtraMessageBox.Show("Xuất file excel " + str + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        //xuất ra file word nhân viện hoặc quyền
        private void BtnWord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog
            {
                Filter = "Word Files (*.docx)|*.docx",
                Title = "Xuất ra file word"
            };
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string str = "nhân viên";
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                    gvStaff.ExportToDocx(sf.FileName);
                else
                {
                    gvRole.ExportToDocx(sf.FileName);
                    str = "quyền";
                }
                XtraMessageBox.Show("Xuất file word " + str + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        //xuất ra file Pdf nhân viện hoặc quyền
        private void BtnPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog
            {
                Filter = "Pdf Files (*.pdf)|*.pdf",
                Title = "Xuất ra file pdf"
            };
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string str = "nhân viên";
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                    gvStaff.ExportToPdf(sf.FileName);
                else
                {
                    gvRole.ExportToPdf(sf.FileName);
                    str = "quyền";
                }
                XtraMessageBox.Show("Xuất file pdf " + str + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        private void GvRole_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;
            e.Info.DisplayText = (e.RowHandle + 1) + "";
        }
        private void GvStaff_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!e.Info.IsRowIndicator || e.RowHandle < 0)
                return;
            e.Info.DisplayText = (e.RowHandle + 1) + "";
        }
    }
}
