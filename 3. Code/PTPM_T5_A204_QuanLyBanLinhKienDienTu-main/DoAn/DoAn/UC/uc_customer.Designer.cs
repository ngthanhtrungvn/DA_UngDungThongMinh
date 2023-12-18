namespace DoAn.UC
{
    partial class Uc_customer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Uc_customer));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnXoa = new DevExpress.XtraBars.BarButtonItem();
            this.btnExcel = new DevExpress.XtraBars.BarButtonItem();
            this.btnWord = new DevExpress.XtraBars.BarButtonItem();
            this.btnPdf = new DevExpress.XtraBars.BarButtonItem();
            this.btnClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.gcCustomer = new DevExpress.XtraGrid.GridControl();
            this.gvCustomer = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkLoaiKH = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.gcTypeCustomer = new DevExpress.XtraGrid.GridControl();
            this.gvTypeCustomer = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiKH)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTypeCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTypeCustomer)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnClose,
            this.btnDelete,
            this.btnXoa,
            this.btnExcel,
            this.btnWord,
            this.btnPdf});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 10;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnXoa),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExcel, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnWord, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnPdf, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnClose, true)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // btnXoa
            // 
            this.btnXoa.Caption = "Xoá";
            this.btnXoa.Id = 5;
            this.btnXoa.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnXoa.ImageOptions.Image")));
            this.btnXoa.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnXoa.ImageOptions.LargeImage")));
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnXoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnXoa_ItemClick);
            // 
            // btnExcel
            // 
            this.btnExcel.Caption = "Xuất excel";
            this.btnExcel.Id = 6;
            this.btnExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.ImageOptions.Image")));
            this.btnExcel.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnExcel.ImageOptions.LargeImage")));
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnExcel_ItemClick);
            // 
            // btnWord
            // 
            this.btnWord.Caption = "Xuất word";
            this.btnWord.Id = 7;
            this.btnWord.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnWord.ImageOptions.Image")));
            this.btnWord.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnWord.ImageOptions.LargeImage")));
            this.btnWord.Name = "btnWord";
            this.btnWord.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnWord.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnWord_ItemClick);
            // 
            // btnPdf
            // 
            this.btnPdf.Caption = "Xuất pdf";
            this.btnPdf.Id = 8;
            this.btnPdf.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPdf.ImageOptions.Image")));
            this.btnPdf.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnPdf.ImageOptions.LargeImage")));
            this.btnPdf.Name = "btnPdf";
            this.btnPdf.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnPdf.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnPdf_ItemClick);
            // 
            // btnClose
            // 
            this.btnClose.Caption = "Đóng";
            this.btnClose.Id = 3;
            this.btnClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.ImageOptions.Image")));
            this.btnClose.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnClose.ImageOptions.LargeImage")));
            this.btnClose.Name = "btnClose";
            this.btnClose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(634, 28);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 350);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.barDockControlBottom.Size = new System.Drawing.Size(634, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 28);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 322);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(634, 28);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 322);
            // 
            // btnDelete
            // 
            this.btnDelete.Caption = "Xoá";
            this.btnDelete.Id = 4;
            this.btnDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageOptions.Image")));
            this.btnDelete.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageOptions.LargeImage")));
            this.btnDelete.Name = "btnDelete";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 28);
            this.xtraTabControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(634, 322);
            this.xtraTabControl1.TabIndex = 14;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.gcCustomer);
            this.xtraTabPage1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(629, 296);
            this.xtraTabPage1.Text = "Khách hàng";
            // 
            // gcCustomer
            // 
            this.gcCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCustomer.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gcCustomer.Location = new System.Drawing.Point(0, 0);
            this.gcCustomer.MainView = this.gvCustomer;
            this.gcCustomer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gcCustomer.MenuManager = this.barManager1;
            this.gcCustomer.Name = "gcCustomer";
            this.gcCustomer.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkLoaiKH});
            this.gcCustomer.Size = new System.Drawing.Size(629, 296);
            this.gcCustomer.TabIndex = 10;
            this.gcCustomer.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCustomer});
            this.gcCustomer.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.GcCustomer_ProcessGridKey);
            // 
            // gvCustomer
            // 
            this.gvCustomer.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gvCustomer.DetailHeight = 239;
            this.gvCustomer.FixedLineWidth = 1;
            this.gvCustomer.GridControl = this.gcCustomer;
            this.gvCustomer.Name = "gvCustomer";
            this.gvCustomer.OptionsDetail.EnableMasterViewMode = false;
            this.gvCustomer.OptionsFind.AlwaysVisible = true;
            this.gvCustomer.OptionsFind.Condition = DevExpress.Data.Filtering.FilterCondition.Contains;
            this.gvCustomer.OptionsFind.FindDelay = 100;
            this.gvCustomer.OptionsFind.FindNullPrompt = "Tìm kiếm tại đây...";
            this.gvCustomer.OptionsFind.SearchInPreview = true;
            this.gvCustomer.OptionsFind.ShowFindButton = false;
            this.gvCustomer.OptionsView.ShowGroupPanel = false;
            this.gvCustomer.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.GvCustomer_CustomDrawRowIndicator);
            this.gvCustomer.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.GvCustomer_InvalidRowException);
            this.gvCustomer.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.GvCustomer_ValidateRow);
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên khách hàng";
            this.gridColumn2.FieldName = "TENKH";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Giới tính";
            this.gridColumn3.FieldName = "GIOITINH";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Địa chỉ";
            this.gridColumn4.FieldName = "DIACHI";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Số điện thoại";
            this.gridColumn5.FieldName = "SDT";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Loại khách hàng";
            this.gridColumn6.ColumnEdit = this.lkLoaiKH;
            this.gridColumn6.FieldName = "maloaikh";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            // 
            // lkLoaiKH
            // 
            this.lkLoaiKH.AutoHeight = false;
            this.lkLoaiKH.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkLoaiKH.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MALOAIKH", "Mã loại khách hàng"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TENLOAIKH", "Tên loại khách hàng"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GIAMGIA", "Giảm giá")});
            this.lkLoaiKH.Name = "lkLoaiKH";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.gcTypeCustomer);
            this.xtraTabPage2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(629, 298);
            this.xtraTabPage2.Text = "Loại khách hàng";
            // 
            // gcTypeCustomer
            // 
            this.gcTypeCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTypeCustomer.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gcTypeCustomer.Location = new System.Drawing.Point(0, 0);
            this.gcTypeCustomer.MainView = this.gvTypeCustomer;
            this.gcTypeCustomer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gcTypeCustomer.MenuManager = this.barManager1;
            this.gcTypeCustomer.Name = "gcTypeCustomer";
            this.gcTypeCustomer.Size = new System.Drawing.Size(629, 298);
            this.gcTypeCustomer.TabIndex = 0;
            this.gcTypeCustomer.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTypeCustomer});
            this.gcTypeCustomer.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.GcTypeCustomer_PreviewKeyDown);
            // 
            // gvTypeCustomer
            // 
            this.gvTypeCustomer.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn8,
            this.gridColumn9});
            this.gvTypeCustomer.DetailHeight = 239;
            this.gvTypeCustomer.FixedLineWidth = 1;
            this.gvTypeCustomer.GridControl = this.gcTypeCustomer;
            this.gvTypeCustomer.Name = "gvTypeCustomer";
            this.gvTypeCustomer.OptionsDetail.EnableMasterViewMode = false;
            this.gvTypeCustomer.OptionsFind.AlwaysVisible = true;
            this.gvTypeCustomer.OptionsFind.Condition = DevExpress.Data.Filtering.FilterCondition.Contains;
            this.gvTypeCustomer.OptionsFind.FindDelay = 100;
            this.gvTypeCustomer.OptionsFind.FindNullPrompt = "Tìm kiếm tại đây...";
            this.gvTypeCustomer.OptionsFind.SearchInPreview = true;
            this.gvTypeCustomer.OptionsFind.ShowFindButton = false;
            this.gvTypeCustomer.OptionsView.ShowGroupPanel = false;
            this.gvTypeCustomer.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.GvTypeCustomer_CustomDrawRowIndicator);
            this.gvTypeCustomer.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.GvTypeCustomer_InvalidRowException);
            this.gvTypeCustomer.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.GvTypeCustomer_ValidateRow);
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Tên loại khách hàng";
            this.gridColumn8.FieldName = "TENLOAIKH";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Giảm giá";
            this.gridColumn9.FieldName = "GIAMGIA";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 1;
            // 
            // Uc_customer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Uc_customer";
            this.Size = new System.Drawing.Size(634, 350);
            this.Tag = "Danh sách khách hàng";
            this.Load += new System.EventHandler(this.Uc_customer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiKH)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTypeCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTypeCustomer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnClose;
        private DevExpress.XtraBars.BarButtonItem btnDelete;
        private DevExpress.XtraBars.BarButtonItem btnXoa;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraGrid.GridControl gcCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCustomer;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkLoaiKH;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraGrid.GridControl gcTypeCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTypeCustomer;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraBars.BarButtonItem btnExcel;
        private DevExpress.XtraBars.BarButtonItem btnWord;
        private DevExpress.XtraBars.BarButtonItem btnPdf;
    }
}
