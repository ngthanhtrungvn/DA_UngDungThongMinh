using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BUS;

namespace DoAn.FRM
{
    public partial class FrmConnect : DevExpress.XtraEditors.XtraForm
    {
        private string connectionString;
        private readonly FrmSystem frm; 
        public FrmConnect(FrmSystem frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        private void BtnTestconnect_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            connectionString = String.Format("server={0}; database={1}; Integrated Security = False;uid={2};pwd={3}", cbbServer.Text.Trim(), cbbDatabase.Text, txtUsername.Text, txtPassword.Text);
            if (Support.TestConnection(connectionString))
            {
                splashScreenManager1.CloseWaitForm();
                XtraMessageBox.Show("Kết nối thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                frm.SetStatus("Test kết nối thành công.",Color.Yellow);
            }
            else
            {
                splashScreenManager1.CloseWaitForm();
                XtraMessageBox.Show("Kết nối thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                frm.SetStatus("Test kết nối thất bại.",Color.Red);
            }
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
        private void CbbDatabase_MouseDown(object sender, MouseEventArgs e)
        {
            if (ValidateTextBox(txtUsername) || ValidateTextBox(txtPassword))            
                return;
            splashScreenManager1.ShowWaitForm();
            DataTable tb = Support.GetDBName(cbbServer.Text, txtUsername.Text.Trim(), txtPassword.Text);
            if (tb.Rows.Count == 0)
                frm.SetStatus("Tài khoản mật khẩu không hợp lệ.",Color.Red);
            else
                frm.SetStatus("", Color.Red);
            cbbDatabase.Properties.DataSource =tb;
            cbbDatabase.Properties.DisplayMember = "name";
            splashScreenManager1.CloseWaitForm();
        }
        private void CbbServer_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            cbbServer.Properties.Items.Clear();
            DataTable tb = Support.GetServerName();
            foreach (DataRow r in tb.Rows)
                cbbServer.Properties.Items.Add(r[0].ToString());
            splashScreenManager1.CloseWaitForm();
        }
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            connectionString = String.Format("server={0}; database={1}; Integrated Security = False;uid={2};pwd={3}", cbbServer.Text.Trim(), cbbDatabase.Text, txtUsername.Text, txtPassword.Text);
            splashScreenManager1.ShowWaitForm();
            if (Support.SaveConnection(connectionString))
            {
                frm.SetStatus("Lưu thành công.Vui lòng khởi động lại ứng dụng.",Color.Yellow);
                splashScreenManager1.CloseWaitForm();
                if (XtraMessageBox.Show("Lưu thành công.Vui lòng khởi động lại ứng dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                   Application.Restart();
                }
            }
            else
            {
                splashScreenManager1.CloseWaitForm();
                frm.SetStatus("Có lỗi xảy ra.",Color.Red);
                XtraMessageBox.Show("Có lỗi xảy ra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}