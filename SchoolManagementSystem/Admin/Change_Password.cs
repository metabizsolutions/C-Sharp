using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;

namespace SchoolManagementSystem.Admin
{
    public partial class Change_Password : XtraForm
    {
        private readonly Main_FD objMain_FD;
        bool isform = false;
        public Change_Password(Main_FD Mainpage)
        {
            InitializeComponent();
            fun.selectall_Controls(this);
            objMain_FD = Mainpage;
            isform = true;
        }
        private void Password_Leave(object sender, EventArgs e)
        {
            DataTable dt = fun.FetchDataTable("SELECT * from admin where admin_id = " + Login.CurrentUserID + "");
            if (String.Compare(Convert.ToString(OldPassword.Text), Convert.ToString(dt.Rows[0]["Password"])) == 0)
            {
                lblfisrterror.Text = "Password Match Successfully";
                lblfisrterror.ForeColor = Color.Green;
                lblfisrterror.Visible = true;
                NewPassword.Enabled = true;
            }
            else
            {
                lblfisrterror.Text = "Please Write Your Current Password Correctly";
                lblfisrterror.ForeColor = Color.Red;
                lblfisrterror.Visible = true;
                NewPassword.Enabled = false;
            }

        }
        CommonFunctions fun = new CommonFunctions();
        private void BtnChangePass_Click(object sender, EventArgs e)
        {
            if (OldPassword.Text == "")
            {
                MessageBox.Show("Please write your Current Password", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (NewPassword.Text == "")
            {
                MessageBox.Show("Please write your New Password", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable dt = fun.FetchDataTable("SELECT * from admin where admin_id = " + Login.CurrentUserID + "");
            if (String.Compare(Convert.ToString(OldPassword.Text), Convert.ToString(dt.Rows[0]["Password"])) == 0)
            {
                string query = "UPDATE admin SET password='" + NewPassword.Text + "' WHERE admin_id = " + Login.CurrentUserID + "";
                fun.ExecuteQuery(query);
                lblfisrterror.Text = "Password Matched and Updated Successfully";
                lblfisrterror.ForeColor = Color.Green;
                lblfisrterror.Visible = true;
            }
            else
            {
                lblfisrterror.Text = "Please Write Your Current Password Correctly";
                lblfisrterror.ForeColor = Color.Red;
                lblfisrterror.Visible = true;
                return;
            }

            OldPassword.Text = "";
            NewPassword.Text = "";
            lblfisrterror.Visible = false;
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(10);
            }
            this.Close();
        }

        private void NewPassword_EditValueChanged(object sender, EventArgs e)
        {
            BtnChangePass.Enabled = true;
        }

        private void Change_Password_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
