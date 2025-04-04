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
    public partial class Create_section : DevExpress.XtraEditors.XtraForm
    {
        CommonFunctions fun = new CommonFunctions();
        public Create_section()
        {
            InitializeComponent();
            txtDay.Properties.Items.Clear();
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                txtDay.Properties.Items.Add(day);
            txtETime.Properties.Mask.MaskType = MaskType.DateTime;
            txtETime.Properties.Mask.EditMask = "HH:mm";
            txtETime.MaskBox.Mask.UseMaskAsDisplayFormat = true;
            txtSTime.Properties.Mask.MaskType = MaskType.DateTime;
            txtSTime.Properties.Mask.EditMask = "HH:mm";
            txtSTime.MaskBox.Mask.UseMaskAsDisplayFormat = true;
            load_default_subjects();
            loadingdata();
        }
        void loadingdata()
        {
            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";

            txtTeacher.Properties.DataSource = fun.GetAllTeacher_dt();
            txtTeacher.Properties.DisplayMember = "name";
            txtTeacher.Properties.ValueMember = "teacher_id";
        }
        void load_default_subjects()
        {
            string query = "select * from subject_default";
            DataTable sub_dt = fun.FetchDataTable(query);
            CCB_subjects.Properties.DataSource = sub_dt;
            CCB_subjects.Properties.DisplayMember = "name";
            CCB_subjects.Properties.ValueMember = "id";
        }
        private void btnSAdd_Click(object sender, EventArgs e)
        {
            if (txtClass.EditValue == null || txtName.Text == "" || txtTeacher.EditValue == null)
            {
                MessageBox.Show("Fill all fields!", "Info");
                return;
            }
            int class_id = Convert.ToInt32(txtClass.EditValue.ToString());
            string query = "INSERT into section(name,class_id,teacher_id,sync,time_start,time_end,days) VALUES('" + txtName.Text + "','" + class_id + "'," + txtTeacher.EditValue.ToString() + ",'0','" + txtSTime.Text + "','" + txtETime.Text + "','" + txtDay.Text.Trim() + "');";
            long secid = fun.Execute_Insert(query);
            string[] def_sub = CCB_subjects.EditValue.ToString().Split(',');
            for (int i = 0; i < def_sub.Length; i++)
            {
                fun.insert_subject_section(class_id.ToString(), secid.ToString(), def_sub[i].ToString(), "0", "0", "0");
            }
            empty();
        }
        private void empty()
        {
            txtName.Text = "";
            txtClass.Text = "";
            txtTeacher.Text = "";
            //section_sub.Clear();
            //lbltotsubjects.Text = "Subjects";
            //linkLabel1.Enabled = true;
        }

        private void link_add_default_subjects_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (Default_subjects objfrom = new Default_subjects())
            {
                if (objfrom.ShowDialog() == DialogResult.Yes)
                    link_add_default_subjects.Enabled = false;
                else
                {
                    link_add_default_subjects.Enabled = true;
                    load_default_subjects();
                }
            }
        }
    }
}
