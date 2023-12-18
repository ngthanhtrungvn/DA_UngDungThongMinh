
namespace DoAn.FRM
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.fluentDesignFormContainer1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer();
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.BtnHome = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.BtnManagerment = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.BtnProduct = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.BtnNhanVien = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.BtnKhachHang = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.BtnReceipt = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.BtnOrder = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.BtnStaffCustomer = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.BtnBanHang = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.BtnNhapHang = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.BtnCustomerOfStaff = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.BtnStatistical = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.BtnTurnover = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.BtnPredictNextDay = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.fluentDesignFormControl1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl();
            this.LbTieuDe = new DevExpress.XtraBars.BarHeaderItem();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::DoAn.FRM.WaitForm1), true, true);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.BtnChangePass = new DevExpress.XtraBars.BarButtonItem();
            this.BtnLogout = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.LbAccount = new DevExpress.XtraBars.BarHeaderItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.BtnBackup = new DevExpress.XtraBars.BarButtonItem();
            this.BtnRestore = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // fluentDesignFormContainer1
            // 
            this.fluentDesignFormContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fluentDesignFormContainer1.Location = new System.Drawing.Point(260, 53);
            this.fluentDesignFormContainer1.Name = "fluentDesignFormContainer1";
            this.fluentDesignFormContainer1.Size = new System.Drawing.Size(431, 394);
            this.fluentDesignFormContainer1.TabIndex = 0;
            // 
            // accordionControl1
            // 
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.BtnHome,
            this.BtnManagerment,
            this.BtnStaffCustomer,
            this.BtnStatistical});
            this.accordionControl1.Location = new System.Drawing.Point(0, 53);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            this.accordionControl1.Size = new System.Drawing.Size(260, 394);
            this.accordionControl1.TabIndex = 1;
            this.accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // BtnHome
            // 
            this.BtnHome.Name = "BtnHome";
            this.BtnHome.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.BtnHome.Text = "Trang chủ";
            this.BtnHome.Click += new System.EventHandler(this.BtnHome_Click);
            // 
            // BtnManagerment
            // 
            this.BtnManagerment.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.BtnProduct,
            this.BtnNhanVien,
            this.BtnKhachHang,
            this.BtnReceipt,
            this.BtnOrder});
            this.BtnManagerment.Name = "BtnManagerment";
            this.BtnManagerment.Text = "Quản lý";
            this.BtnManagerment.Click += new System.EventHandler(this.BtnManagerment_Click);
            // 
            // BtnProduct
            // 
            this.BtnProduct.Name = "BtnProduct";
            this.BtnProduct.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.BtnProduct.Text = "Linh kiện";
            this.BtnProduct.Click += new System.EventHandler(this.BtnProduct_Click);
            // 
            // BtnNhanVien
            // 
            this.BtnNhanVien.Name = "BtnNhanVien";
            this.BtnNhanVien.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.BtnNhanVien.Text = "Nhân viên";
            this.BtnNhanVien.Click += new System.EventHandler(this.BtnNhanVien_Click);
            // 
            // BtnKhachHang
            // 
            this.BtnKhachHang.Name = "BtnKhachHang";
            this.BtnKhachHang.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.BtnKhachHang.Text = "Khách hàng";
            this.BtnKhachHang.Click += new System.EventHandler(this.BtnKhachHang_Click);
            // 
            // BtnReceipt
            // 
            this.BtnReceipt.Name = "BtnReceipt";
            this.BtnReceipt.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.BtnReceipt.Text = "Phiếu nhập";
            this.BtnReceipt.Click += new System.EventHandler(this.BtnReceipt_Click);
            // 
            // BtnOrder
            // 
            this.BtnOrder.Name = "BtnOrder";
            this.BtnOrder.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.BtnOrder.Text = "Hoá đơn";
            this.BtnOrder.Click += new System.EventHandler(this.BtnOrder_Click);
            // 
            // BtnStaffCustomer
            // 
            this.BtnStaffCustomer.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.BtnBanHang,
            this.BtnNhapHang,
            this.BtnCustomerOfStaff});
            this.BtnStaffCustomer.Name = "BtnStaffCustomer";
            this.BtnStaffCustomer.Text = "Khách hàng";
            this.BtnStaffCustomer.Click += new System.EventHandler(this.BtnStaffCustomer_Click);
            // 
            // BtnBanHang
            // 
            this.BtnBanHang.Name = "BtnBanHang";
            this.BtnBanHang.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.BtnBanHang.Text = "Bán hàng";
            this.BtnBanHang.Click += new System.EventHandler(this.BtnBanHang_Click);
            // 
            // BtnNhapHang
            // 
            this.BtnNhapHang.Name = "BtnNhapHang";
            this.BtnNhapHang.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.BtnNhapHang.Text = "Nhập hàng";
            this.BtnNhapHang.Click += new System.EventHandler(this.BtnNhapHang_Click);
            // 
            // BtnCustomerOfStaff
            // 
            this.BtnCustomerOfStaff.Name = "BtnCustomerOfStaff";
            this.BtnCustomerOfStaff.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.BtnCustomerOfStaff.Text = "Quản lý khách hàng";
            this.BtnCustomerOfStaff.Click += new System.EventHandler(this.BtnCustomerOfStaff_Click);
            // 
            // BtnStatistical
            // 
            this.BtnStatistical.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.BtnTurnover,
            this.BtnPredictNextDay});
            this.BtnStatistical.Expanded = true;
            this.BtnStatistical.Name = "BtnStatistical";
            this.BtnStatistical.Text = "Thống kê - Dự đoán";
            // 
            // BtnTurnover
            // 
            this.BtnTurnover.Name = "BtnTurnover";
            this.BtnTurnover.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.BtnTurnover.Text = "Doanh thu";
            this.BtnTurnover.Click += new System.EventHandler(this.BtnTurnover_Click);
            // 
            // BtnPredictNextDay
            // 
            this.BtnPredictNextDay.Name = "BtnPredictNextDay";
            this.BtnPredictNextDay.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.BtnPredictNextDay.Text = "Dự báo doanh thu";
            this.BtnPredictNextDay.Click += new System.EventHandler(this.BtnPredictNextDay_Click);
            // 
            // fluentDesignFormControl1
            // 
            this.fluentDesignFormControl1.FluentDesignForm = this;
            this.fluentDesignFormControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.LbTieuDe});
            this.fluentDesignFormControl1.Location = new System.Drawing.Point(0, 0);
            this.fluentDesignFormControl1.Name = "fluentDesignFormControl1";
            this.fluentDesignFormControl1.Size = new System.Drawing.Size(691, 29);
            this.fluentDesignFormControl1.TabIndex = 2;
            this.fluentDesignFormControl1.TabStop = false;
            this.fluentDesignFormControl1.TitleItemLinks.Add(this.LbTieuDe);
            // 
            // LbTieuDe
            // 
            this.LbTieuDe.Caption = "LbTieuDe";
            this.LbTieuDe.Id = 0;
            this.LbTieuDe.Name = "LbTieuDe";
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.BtnChangePass,
            this.BtnBackup,
            this.BtnRestore,
            this.BtnLogout,
            this.LbAccount});
            this.barManager1.MaxItemId = 7;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.BtnChangePass),
            new DevExpress.XtraBars.LinkPersistInfo(this.BtnLogout)});
            this.bar1.Text = "Tools";
            // 
            // BtnChangePass
            // 
            this.BtnChangePass.Caption = "Đổi mật khẩu";
            this.BtnChangePass.Id = 0;
            this.BtnChangePass.Name = "BtnChangePass";
            this.BtnChangePass.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnChangePass_ItemClick);
            // 
            // BtnLogout
            // 
            this.BtnLogout.Caption = "Đăng xuất";
            this.BtnLogout.Id = 3;
            this.BtnLogout.Name = "BtnLogout";
            this.BtnLogout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnLogout_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.LbAccount)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // LbAccount
            // 
            this.LbAccount.Caption = "LbAccount";
            this.LbAccount.Id = 4;
            this.LbAccount.Name = "LbAccount";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 29);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(691, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 447);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(691, 26);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 53);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 394);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(691, 53);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 394);
            // 
            // BtnBackup
            // 
            this.BtnBackup.Id = 5;
            this.BtnBackup.Name = "BtnBackup";
            // 
            // BtnRestore
            // 
            this.BtnRestore.Id = 6;
            this.BtnRestore.Name = "BtnRestore";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 473);
            this.ControlContainer = this.fluentDesignFormContainer1;
            this.Controls.Add(this.fluentDesignFormContainer1);
            this.Controls.Add(this.accordionControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.fluentDesignFormControl1);
            this.FluentDesignFormControl = this.fluentDesignFormControl1;
            this.Name = "FrmMain";
            this.NavigationControl = this.accordionControl1;
            this.Text = "FrmMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer fluentDesignFormContainer1;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement BtnHome;
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl fluentDesignFormControl1;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem BtnChangePass;
        private DevExpress.XtraBars.BarButtonItem BtnBackup;
        private DevExpress.XtraBars.BarButtonItem BtnRestore;
        private DevExpress.XtraBars.BarButtonItem BtnLogout;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement BtnManagerment;
        private DevExpress.XtraBars.Navigation.AccordionControlElement BtnProduct;
        private DevExpress.XtraBars.Navigation.AccordionControlElement BtnNhanVien;
        private DevExpress.XtraBars.Navigation.AccordionControlElement BtnKhachHang;
        private DevExpress.XtraBars.Navigation.AccordionControlElement BtnReceipt;
        private DevExpress.XtraBars.Navigation.AccordionControlElement BtnOrder;
        private DevExpress.XtraBars.Navigation.AccordionControlElement BtnStaffCustomer;
        private DevExpress.XtraBars.Navigation.AccordionControlElement BtnBanHang;
        private DevExpress.XtraBars.Navigation.AccordionControlElement BtnNhapHang;
        private DevExpress.XtraBars.Navigation.AccordionControlElement BtnCustomerOfStaff;
        private DevExpress.XtraBars.Navigation.AccordionControlElement BtnStatistical;
        private DevExpress.XtraBars.Navigation.AccordionControlElement BtnTurnover;
        private DevExpress.XtraBars.Navigation.AccordionControlElement BtnPredictNextDay;
        private DevExpress.XtraBars.BarHeaderItem LbAccount;
        private DevExpress.XtraBars.BarHeaderItem LbTieuDe;
    }
}