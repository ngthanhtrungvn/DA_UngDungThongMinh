using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BUS;

namespace DoAn.FRM
{
    public partial class FrmLogin : DevExpress.XtraEditors.XtraForm
    {
        private readonly FrmSystem frm;
        public FrmLogin(FrmSystem frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Remember))
            {
                string[] arrStr = Properties.Settings.Default.Remember.Split('-');
                txtUsername.Text = arrStr[0];
                txtPassword.Text = arrStr[1];
                ckbRemember.Checked = true;
            }
            else
                ckbRemember.Checked = false;
        }
        private bool ValidateTextBox(TextEdit txt)
        {
            if (txt.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show(txt.Tag + " không được rỗng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt.Focus();
                return true;
            }
            return false;
        }
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (ValidateTextBox(txtUsername) || ValidateTextBox(txtPassword))
                return;
            splashScreenManager1.ShowWaitForm();
            int errorCode = 0;
            var nv = NhanVienBUS.Instances.Login(txtUsername.Text, txtPassword.Text,ref errorCode);
            if (errorCode.ToString().Equals("-2146232060"))
            {
                splashScreenManager1.CloseWaitForm();
                XtraMessageBox.Show("Lỗi kết nối server.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                frm.SetStatus("Lỗi kết nối server.", Color.Red);
            }
            else
            if (nv == null)
            {
                splashScreenManager1.CloseWaitForm();
                XtraMessageBox.Show("Sai tài khoản hoặc mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                frm.SetStatus("Sai tài khoản hoặc mật khẩu", Color.Red);
            }
            else
            {
                if (ckbRemember.Checked)
                    Properties.Settings.Default.Remember = txtUsername.Text.Trim() + "-" + txtPassword.Text.Trim();
                else
                    Properties.Settings.Default.Remember = "";
                Properties.Settings.Default.Save();
                frm.Hide();
                splashScreenManager1.CloseWaitForm();
                new FrmMain(frm, nv).Show();
            }
        }
    }
}