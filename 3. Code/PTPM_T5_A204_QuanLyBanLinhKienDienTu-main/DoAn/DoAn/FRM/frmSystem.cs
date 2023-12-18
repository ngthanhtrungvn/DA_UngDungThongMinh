using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace DoAn.FRM
{
    public partial class FrmSystem : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FrmSystem()
        {
            InitializeComponent();
            lbStatus.Caption = "";
        }
        void OpenForm(Type typeForm)
        {
            foreach (Form frm in MdiChildren)
            {
                if (frm.GetType() == typeForm)
                {
                    frm.Activate();
                    return;
                }
            }
            Form f = (Form)Activator.CreateInstance(typeForm, this);
            f.MdiParent = this;
            f.Show();
        }
        public void SetStatus(string status, Color cl)
        {
            lbStatus.Caption = status;
            lbStatus.ItemAppearance.Normal.ForeColor = cl;
        }
        private void BtnConnect_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmConnect));
        }
        private void BtnLogin_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmLogin));
        }
        private void FrmSystem_Load(object sender, EventArgs e)
        {
            OpenForm(typeof(FrmLogin));
        }
        public void Show_()
        {
            this.Show();
        }
    }
}