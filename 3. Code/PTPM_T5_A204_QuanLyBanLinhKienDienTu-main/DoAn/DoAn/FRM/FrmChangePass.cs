using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BUS;

namespace DoAn.FRM
{
    public partial class FrmChangePass : DevExpress.XtraEditors.XtraForm
    {
        readonly dynamic nv;
        readonly FrmMain frm;
        public FrmChangePass(FrmMain frm, dynamic nv)
        {
            InitializeComponent();
            this.nv = nv;
            this.frm = frm;
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        bool ValidateTextBox(TextEdit txt)
        {
            if (txt.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show(txt.Tag + " không được rỗng.", "Thông báo");
                txt.Focus();
                return true;
            }
            return false;
        }
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (ValidateTextBox(txtOldPass) || ValidateTextBox(txtNewPass))
                return;
            if (txtNewPass.Text.Length < 5)
            {
                XtraMessageBox.Show("Mật khẩu từ 5 kí tự trở lên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNewPass.Focus();
                return;
            }
            if(!txtNewPass.Text.Equals(txtConfirmPass.Text))
            {
                XtraMessageBox.Show("Xác nhận mật khẩu không giống nhau.", "Thông báo",MessageBoxButtons.OK ,MessageBoxIcon.Error);
                txtConfirmPass.Focus();
                return;
            }
            int i = NhanVienBUS.Instances.ChangePass(nv.MANV, txtOldPass.Text, txtNewPass.Text);
            if (i == -1)
            { 
                XtraMessageBox.Show("Mật khẩu cũ không chính xác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOldPass.Focus();
            }
            else
            {
                XtraMessageBox.Show("Đổi mật khẩu thành công.Vui lòng đăng nhập lại", "Thông báo");
                frm.Logout(1);
            }
        }
    }
}