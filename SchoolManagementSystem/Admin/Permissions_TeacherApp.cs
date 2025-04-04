using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Admin
{
    public partial class Permissions_TeacherApp : DevExpress.XtraEditors.XtraForm
    {
        CommonFunctions fun = new CommonFunctions();
        ObservableCollection<AllValues> allValue;
        CheckEdit[] boxes;
        public Permissions_TeacherApp()
        {
            InitializeComponent();
            fun.selectall_Controls(this);
            allValue = new ObservableCollection<AllValues>();
            txtUserPermission.Properties.Items.Clear();
            allValue = fun.GetAllPermissionType("Teacher_App");
            if (allValue.Count == 0)
            {
                txtUserPermission.Properties.Items.Add("Coordinator");
                txtUserPermission.Properties.Items.Add("Teachers");
            }
            foreach (var val in allValue)
                txtUserPermission.Properties.Items.Add(val.Name);
            boxes = new CheckEdit[7];
            boxes[0] = teacher_attendance;
            boxes[1] = student_attendance;
            boxes[2] = manage_marks;
            boxes[3] = std_report;
            boxes[4] = timetable;
            boxes[5] = floor_sheet;
            boxes[6] = administer;

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var id = fun.GetPermissionID(txtUserPermission.Text.Trim(), "Teacher_App");
            if (id != "")
            {
                MessageBox.Show("Permission Type already exit.", "Info");
                return;
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    boxes[i].Checked = false;
                }
                MySqlConnection con = new MySqlConnection(Login.constring);
                con.Open();
                try
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT into tbl_permission(title,app_type) VALUES('" + txtUserPermission.Text + "','Teacher_App');", con);
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    SystemSounds.Hand.Play();
                    MessageBox.Show(ex.Message, "Error");
                    return;
                }
                con.Close();
                groupControl2.Visible = true;
                // FillGridClass();
                // empty();
            }


        }

        private void btnApply_Click(object sender, EventArgs e)
        {

            int alreadyexit = 0;
            var id = fun.GetPermissionID(txtUserPermission.Text.Trim(), "Teacher_App");

            MySqlConnection con = new MySqlConnection(Login.constring);

            if (id != "")
            {
                for (int i = 0; i < 7; i++)
                {
                    con.Open();
                    var query1 = "SELECT tbl_role.key,tbl_role.value FROM tbl_role   where tbl_role.permission_id='" + id + "' and tbl_role.key='" + boxes[i].Name + "'";
                    MySqlCommand cmd3 = new MySqlCommand(query1, con);
                    MySqlDataReader reader3 = cmd3.ExecuteReader();
                    if (reader3.HasRows)
                    {
                        alreadyexit = 1;
                    }
                    con.Close();
                    if (alreadyexit == 1)
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("UPDATE tbl_role set value= '" + (boxes[i].Checked == true ? 1 : 0) + "' WHERE `key`='" + boxes[i].Name + "' and permission_id='" + id + "' ;", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("INSERT into tbl_role(`key`,value,permission_id) VALUES('" + boxes[i].Name + "','" + (boxes[i].Checked == true ? 1 : 0) + "','" + id + "');", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    alreadyexit = 0;
                }
            }
            else
            {
                MessageBox.Show("Select Permission is not exit.", "Info");
                return;
            }
            for (int i = 0; i < 6; i++)
            {
                boxes[i].Checked = false;
            }
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                boxes[i].Checked = false;
            }
            var id = fun.GetPermissionID(txtUserPermission.Text.Trim(), "Teacher_App");

            MySqlConnection con = new MySqlConnection(Login.constring);
            if (id != "")
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT tbl_role.key as k,tbl_role.value as v FROM tbl_role   where tbl_role.permission_id='" + id + "'", con);
                MySqlDataReader reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        if (reader1["k"].ToString() == "teacher_attendance" && reader1["v"].ToString() == "True")
                            teacher_attendance.Checked = true;
                        if (reader1["k"].ToString() == "student_attendance" && reader1["v"].ToString() == "True")
                            student_attendance.Checked = true;
                        if (reader1["k"].ToString() == "manage_marks" && reader1["v"].ToString() == "True")
                            manage_marks.Checked = true;
                        if (reader1["k"].ToString() == "std_report" && reader1["v"].ToString() == "True")
                            std_report.Checked = true;
                        if (reader1["k"].ToString() == "timetable" && reader1["v"].ToString() == "True")
                            timetable.Checked = true;
                        if (reader1["k"].ToString() == "floor_sheet" && reader1["v"].ToString() == "True")
                            floor_sheet.Checked = true;
                        if (reader1["k"].ToString() == "administer" && reader1["v"].ToString() == "True")
                            administer.Checked = true;
                    }
                }
                con.Close();
                groupControl2.Visible = true;
            }
            else
            {
                MessageBox.Show("Select Permission is not exit.", "Info");
                return;
            }
        }

        private void std_report_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
