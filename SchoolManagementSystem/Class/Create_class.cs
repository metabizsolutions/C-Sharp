using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;

namespace SchoolManagementSystem.Class
{
    public partial class Create_class : DevExpress.XtraEditors.XtraForm
    {
        CommonFunctions fun = new CommonFunctions();
        public Create_class()
        {
            InitializeComponent();
            txtETime.Properties.Mask.MaskType = MaskType.DateTime;
            txtETime.Properties.Mask.EditMask = "HH:mm";
            txtETime.MaskBox.Mask.UseMaskAsDisplayFormat = true;
            txtSTime.Properties.Mask.MaskType = MaskType.DateTime;
            txtSTime.Properties.Mask.EditMask = "HH:mm";
            txtSTime.MaskBox.Mask.UseMaskAsDisplayFormat = true;
            loadingdata();
            load_degrees();
        }
        void loadingdata()
        {
            txtTeacher.Properties.DataSource = fun.GetAllTeacher_dt();
            txtTeacher.Properties.DisplayMember = "name";
            txtTeacher.Properties.ValueMember = "teacher_id";
        }
        void load_degrees()
        {
            comboDegree.Properties.DataSource = fun.degrees();
            comboDegree.Properties.DisplayMember = "name";
            comboDegree.Properties.ValueMember = "degree_id";
        }
        private void btnCAdd_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please Fill All Fields", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string query = "INSERT into class(name,degree_id,name_digit,session_id,teacher_id,time_start,time_end,sync) VALUES(" +
                " '" + txtName.Text + "','" + comboDegree.EditValue + "','" + txtdigit_Name.EditValue + "','" + fun.GetSessionID(fun.GetDefaultSessionName()) + "','" + txtTeacher.EditValue.ToString() + "','" + txtSTime.Text + "','" + txtETime.Text + "','0');";
            fun.Execute_Insert(query);
            empty();
        }
        private void empty()
        {
            txtName.Text = "";
            comboDegree.EditValue = null;
            txtdigit_Name.EditValue = null;
            txtSTime.Text = "";
            txtETime.Text = "";
            txtTeacher.Text = "";
        }
        private void lbllink_degree_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (Degrees de = new Degrees())
            {
                if (de.ShowDialog() == DialogResult.Yes) { }
                else
                    load_degrees();
            }
        }
    }
}
