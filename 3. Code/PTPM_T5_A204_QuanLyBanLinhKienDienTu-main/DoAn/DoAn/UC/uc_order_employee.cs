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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Controls;
using System.Globalization;
using DoAn.FRM;

namespace DoAn.UC
{
    public partial class Uc_order_employee : DevExpress.XtraEditors.XtraUserControl
    {
        readonly FrmMain frm;
        public Uc_order_employee(FrmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        //load form khi chạy lần đầu
        private void Uc_order_employee_Load(object sender, EventArgs e)
        {
            cbbKhachHang.Properties.DataSource = KhachHangBUS.Instances.GetKhachHangs();
            cbbKhachHang.Properties.DisplayMember = "TENKH";
            cbbKhachHang.Properties.ValueMember = "Makh";
            //load danh sách hoá đơn chưa thanh toán
            gcOrder.DataSource = HoaDonBUS.Instances.GetHoaDons( false);
            lkLinhKien.DataSource = LinhKienBUS.Instances.GetLinhKiens();
            lkLinhKien.DisplayMember = "TENLINHKIEN";
            lkLinhKien.ValueMember = "MALINHKIEN";
            gvOrderDetail.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
            gvOrder.IndicatorWidth = 50;
            gvOrderDetail.IndicatorWidth = 50;
        }
        //xoá data gridview chi tiết hoá đơn
        void ClearDataGVOrderDetail()
        {
            gcOrderDetail.DataSource = null;
            layoutGroupOrderDetail.Enabled = txtTienKhachDua.Enabled = false;
            txtTienKhachDua.Text = txtTienThua.Text = txtTienPhaiTra.Text = "";

        }
        //chọn khách hàng
        private void CbbKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            dynamic kh = KhachHangBUS.Instances.LayThongTinTheoMa(cbbKhachHang.GetColumnValue("Makh").ToString());
            txtGiamGia.Text = kh.LOAIKH.GIAMGIA.ToString().Trim();
            txtSDT.Text = kh.SDT.Trim();
            txtDiaChi.Text = kh.DIACHI.Trim();
        }
        //đóng user control bán hàng
        private void BtnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm.CLOSE();
        }
        //gọi các chi tiết của 1 hoá đơn có mã hoá đơn truyền vào
        private void CallDataGVOrderDetail(int mahd)
        {
            gcOrderDetail.DataSource = ChiTietHDBUS.Instances.GetChiTietHDs( mahd);
            lkLinhKien.DataSource = LinhKienBUS.Instances.GetLinhKiens();
            lkLinhKien.DisplayMember = "TENLINHKIEN";
            lkLinhKien.ValueMember = "MALINHKIEN";
            layoutGroupOrderDetail.Enabled = txtTienKhachDua.Enabled = true;
            layoutGroupOrderDetail.Text = "Chi tiết hoá đơn " + mahd;
            txtTienPhaiTra.Text = Support.ConvertVND(gvOrder.GetRowCellValue(gvOrder.FocusedRowHandle, "tongtien").ToString());
        }
        //click 1 dòng trong gridview hoá đơn
        private void GvOrder_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle > -1)
                CallDataGVOrderDetail(int.Parse(gvOrder.GetRowCellValue(e.RowHandle, "MAHD").ToString()));
        }
        //tạo 1 hoá đơn mới cho khách hàng       
        private void BtnCreateOrder_Click(object sender, EventArgs e)
        {
            var makh = cbbKhachHang.GetColumnValue("Makh");
            if (makh == null)
                XtraMessageBox.Show("Vui lòng chọn khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                int i = HoaDonBUS.Instances.Insert(frm.nv.MANV, int.Parse(makh.ToString()), double.Parse(txtGiamGia.Text));
                if (i != -1)
                {
                    XtraMessageBox.Show("Tạo hoá đơn thành công.", "Thông báo");
                    gcOrder.DataSource = HoaDonBUS.Instances.GetHoaDons( false);
                    gvOrder.FocusedRowHandle = gvOrder.RowCount - 1;
                    CallDataGVOrderDetail(HoaDonBUS.Instances.LayHDVuaTao().MAHD);
                }
            }
        }
        //huỷ 1 hoá đơn trong gridview hoá đơn
        private void DestroyOrder()
        {
            var mahd = gvOrder.GetRowCellValue(gvOrder.FocusedRowHandle, "MAHD");
            if (mahd != null)
            {
                if (XtraMessageBox.Show("Bạn chắc chắn huỷ hoá đơn " + mahd.ToString() + "?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    int i = HoaDonBUS.Instances.Delete(int.Parse(mahd.ToString()));
                    if (i != -1)
                    {
                        XtraMessageBox.Show("Huỷ hoá đơn thành công " + mahd + ".", "Thông báo");
                        gcOrder.DataSource = HoaDonBUS.Instances.GetHoaDons( false);
                        ClearDataGVOrderDetail();
                    }
                    else
                        XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //sự kiện gọi hàm huỷ hoá đơn
        private void BtnDestroy_Click(object sender, EventArgs e)
        {
            DestroyOrder();
        }
        //click nút delete xoá 1 dòng trong chi tiết hoá đơn
        private void GcOrderDetail_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gvOrderDetail.State != GridState.Editing)
            {
                var mahd = gvOrderDetail.GetRowCellValue(gvOrderDetail.FocusedRowHandle, "MAHD");
                var malinhkien = gvOrderDetail.GetRowCellValue(gvOrderDetail.FocusedRowHandle, "MALINHKIEN");
                if (mahd != null)
                {
                    if (XtraMessageBox.Show("Bạn chắc chắn xoá linh kiện " + LinhKienBUS.Instances.TimTheoMa(int.Parse(malinhkien.ToString())).TENLINHKIEN + "?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        int i = ChiTietHDBUS.Instances.Delete(int.Parse(mahd.ToString()), int.Parse(malinhkien.ToString()));
                        if (i != -1)
                        {
                            XtraMessageBox.Show("Xoá thành công.", "Thông báo");
                            gcOrderDetail.DataSource = ChiTietHDBUS.Instances.GetChiTietHDs( int.Parse(mahd.ToString()));
                            gcOrder.DataSource = HoaDonBUS.Instances.GetHoaDons( false);
                            lkLinhKien.DataSource = LinhKienBUS.Instances.GetLinhKiens();
                            lkLinhKien.DisplayMember = "TENLINHKIEN";
                            lkLinhKien.ValueMember = "MALINHKIEN";
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        //ngăn không cho thao tác khi thêm sửa 1 dòng trong bảng cthd khi dữ liệu sai
        private void GvOrderDetail_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        //thêm sửa 1 dòng trong bảng chi tiết hoá đơn
        private void GvOrderDetail_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            string sErr = "";
            bool bVali = true;
            if (gvOrderDetail.GetRowCellValue(e.RowHandle, "MALINHKIEN").ToString().Trim() == "")
            {
                bVali = false;
                sErr = "Vui lòng chọn linh kiện.\n";
            }
            if (gvOrderDetail.GetRowCellValue(e.RowHandle, "SOLUONG").ToString().Trim() == "")
            {
                bVali = false;
                sErr += "Vui lòng điền số lượng.\n";
            }
            if (bVali)
            {
                int? soLuong = int.Parse(gvOrderDetail.GetRowCellValue(e.RowHandle, "SOLUONG").ToString().Trim());
                dynamic lk = LinhKienBUS.Instances.TimTheoMa(int.Parse(gvOrderDetail.GetRowCellValue(e.RowHandle, "MALINHKIEN").ToString().Trim()));
                if (soLuong <= 0)
                {
                    bVali = false;
                    sErr += "Số lượng phải lớn hơn 0.\n";
                }
                //thêm mới
                if (e.RowHandle < 0)
                {                                
                    if (soLuong > lk.SOLUONGCON)
                    {
                        bVali = false;
                        sErr += "Không đủ hàng.\n";
                    }
                    if (!bVali)
                    {
                        e.Valid = false;
                        XtraMessageBox.Show(sErr, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int i = ChiTietHDBUS.Instances.Insert(
                     int.Parse(gvOrder.GetRowCellValue(gvOrder.FocusedRowHandle, "MAHD").ToString()),
                     int.Parse(gvOrderDetail.GetRowCellValue(e.RowHandle, "MALINHKIEN").ToString().Trim()),
                     int.Parse(gvOrderDetail.GetRowCellValue(e.RowHandle, "SOLUONG").ToString().Trim()));
                    if (i != -1)
                        XtraMessageBox.Show("Thêm thành công", "Thông báo", DevExpress.Utils.DefaultBoolean.True);
                    int row = gvOrder.FocusedRowHandle;
                    int mahd = int.Parse(gvOrder.GetRowCellValue(row, "MAHD").ToString());
                    gcOrder.DataSource = HoaDonBUS.Instances.GetHoaDons( false);
                    gvOrder.FocusedRowHandle = row;
                    CallDataGVOrderDetail(mahd);
                }
                //sửa 
                else
                {
                    dynamic cthd = ChiTietHDBUS.Instances.TimCTHDTheoMaHDMaLK(int.Parse(gvOrder.GetRowCellValue(gvOrder.FocusedRowHandle, "MAHD").ToString()), lk.MALINHKIEN);
                    int? soLuongCon = cthd.SOLUONG + lk.SOLUONGCON;
                    if (soLuong > soLuongCon)
                    {
                        bVali = false;
                        sErr += "Hết hàng.\n";
                    }
                    if (!bVali)
                    {
                        e.Valid = false;
                        XtraMessageBox.Show(sErr, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    ChiTietHDBUS.Instances.Update(
                                 int.Parse(gvOrder.GetRowCellValue(gvOrder.FocusedRowHandle, "MAHD").ToString()),
                                int.Parse(gvOrderDetail.GetRowCellValue(e.RowHandle, "MALINHKIEN").ToString().Trim()),
                                int.Parse(gvOrderDetail.GetRowCellValue(e.RowHandle, "SOLUONG").ToString().Trim()));
                    int row = gvOrder.FocusedRowHandle;
                    int mahd = int.Parse(gvOrder.GetRowCellValue(row, "MAHD").ToString());
                    gcOrder.DataSource = HoaDonBUS.Instances.GetHoaDons( false);
                    gvOrder.FocusedRowHandle = row;
                    CallDataGVOrderDetail(mahd);

                }
            }
            else
            {
                e.Valid = false;
                XtraMessageBox.Show(sErr, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //thanh toán 1 hoá đơn
        private void BtnThanhToan_Click(object sender, EventArgs e)
        {
            if (txtTienPhaiTra.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show("Mời bạn chọn hoá đơn muốn thanh toán.", "Thông báo");
                return;
            }
            double tienPhaiTra = double.Parse(gvOrder.GetRowCellValue(gvOrder.FocusedRowHandle, "tongtien").ToString());

            if (tienPhaiTra == 0)
            {
                XtraMessageBox.Show("Hoá đơn chưa có sản phẩm không cần thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtTienKhachDua.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show("Khách chưa đưa tiền.", "Thông báo");
                return;
            }
            double tienKhachDua = double.Parse(txtTienKhachDua.Text.Trim().Replace(",",""));

            if (tienPhaiTra > tienKhachDua)
            {
                XtraMessageBox.Show("Khách đưa không đủ tiền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int i = HoaDonBUS.Instances.Update(int.Parse(gvOrder.GetRowCellValue(gvOrder.FocusedRowHandle, "MAHD").ToString()), true);
            if (i != -1)
            {
                XtraMessageBox.Show("Thanh toán thành công.", "Thông báo");
                txtTienThua.Text = Support.ConvertVND((tienKhachDua - tienPhaiTra).ToString());
                gcOrder.DataSource = HoaDonBUS.Instances.GetHoaDons( false);
                ClearDataGVOrderDetail();
            }

        }
        //chuyển về kiểu tiền tệ khi nhập tiền vào textbox
        private void TxtTienKhachDua_KeyUp(object sender, KeyEventArgs e)
        {
            CultureInfo culture = new CultureInfo("en-US");
            decimal value;
            try
            {
                value = decimal.Parse(txtTienKhachDua.Text.Replace(",","."), NumberStyles.AllowThousands);

            }
            catch (Exception)
            {
                value = 0;
            }
            txtTienKhachDua.Text = String.Format(culture, "{0:N0}", value);
            txtTienKhachDua.Select(txtTienKhachDua.Text.Length, 0);
            decimal tienPhaiTra = decimal.Parse(gvOrder.GetRowCellValue(gvOrder.FocusedRowHandle, "tongtien").ToString());
            txtTienThua.Text = Support.ConvertVND((value - tienPhaiTra).ToString());
        }
        //không cho nhập chữ vào ô textbox
        private void TxtTienKhachDua_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        //xoá 1 hoá đơn bằng nút delete
        private void GcOrder_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //DestroyOrder();
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
