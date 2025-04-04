using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace SchoolManagementSystem.Principal
{
    public partial class Scheduling : DevExpress.XtraEditors.XtraUserControl
    {
        public Scheduling()
        {
            InitializeComponent();
            schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Month;
            schedulerControl1.Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            schedulerControl1.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.Resource;
            appointmentsTableAdapter.Adapter.RowUpdated += Adapter_RowUpdated;
        }

        private void Adapter_RowUpdated(object sender, MySqlRowUpdatedEventArgs e)
        {
            if (e.Status == UpdateStatus.Continue && e.StatementType == StatementType.Insert)
            {
                int id = 0;
                //Select LAST_INSERT_ID() from appointmentste
                using (MySqlCommand cmd = new MySqlCommand("SELECT @@IDENTITY", appointmentsTableAdapter.Connection))
                {
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                e.Row["ID"] = id;
            }
        }

        private void Scheduling_Load(object sender, EventArgs e)
        {
            this.resourcesTableAdapter.Fill(this.tnsbay_schoolDataSet.resources);
            this.appointmentsTableAdapter.Fill(this.tnsbay_schoolDataSet.appointments);
        }

        private void schedulerStorage1_AppointmentsChanged(object sender, DevExpress.XtraScheduler.PersistentObjectsEventArgs e)
        {
            appointmentsTableAdapter.Update(tnsbay_schoolDataSet);
            tnsbay_schoolDataSet.AcceptChanges();
        }

        private void schedulerStorage1_AppointmentsDeleted(object sender, DevExpress.XtraScheduler.PersistentObjectsEventArgs e)
        {
            appointmentsTableAdapter.Update(tnsbay_schoolDataSet);
            tnsbay_schoolDataSet.AcceptChanges();
        }

        private void schedulerStorage1_AppointmentsInserted(object sender, DevExpress.XtraScheduler.PersistentObjectsEventArgs e)
        {
            appointmentsTableAdapter.Update(tnsbay_schoolDataSet);
            tnsbay_schoolDataSet.AcceptChanges();
        }

        CommonFunctions fun = new CommonFunctions();
        private void barButtonSendASMS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var sms = "";
            var final_sms = "";
            int present = 0;
            int total = 0;
            var mark = fun.GetSetting("principal_attendance_sms");
            string mark_template = mark[0].Name;
            mark_template = mark_template.Trim().Replace("[date]", DateTime.Now.ToString("dd-MM-yyyy"))
                               .Replace("[gender]", "Male");
            MySqlConnection con1 = new MySqlConnection(Login.constring);
            con1.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT class.class_id, class.name, concat( FORMAT(COUNT(attendance.student_id), 0),'/',FORMAT(count(*),0)) AS Present FROM student join class on class.class_id=student.class_id left JOIN attendance ON attendance.student_id= student.student_id and attendance.status= 1 and attendance.date= current_date() where student.sex= 'Male' GROUP by class.class_id", con1);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    sms = mark_template.Split('\\')[1];
                    final_sms += sms.Trim().Replace("[class]", reader1["name"].ToString())
                                                    .Replace("[summary]", reader1["Present"].ToString()) + "\n\r";
                    present += int.Parse(reader1["Present"].ToString().Split('/')[0]);
                    total += int.Parse(reader1["Present"].ToString().Split('/')[1]);

                }
            }
            con1.Close();
            sms = mark_template.Split('\\')[1];
            final_sms += sms.Trim().Replace("[class]", "Total")
                 .Replace("[summary]", present.ToString() + "/" + total.ToString());


            mark_template = mark_template.Split('\\')[0] + "\n\r" + final_sms;

            var phone = fun.GetSetting("student_absent_mobile");
            var tels = phone[0].Name.Split(',');
            foreach (var val in tels)
            {
                con1.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT into sms_que(mobile,sms,status) VALUES('" + val.Trim() + "','" + mark_template + "','" + 0 + "');", con1);
                cmd.ExecuteNonQuery();
                con1.Close();
            }
        }

        private void barButtonSendAGSMS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var sms = "";
            var final_sms = "";
            int present = 0, total = 0;
            var mark = fun.GetSetting("principal_attendance_sms");
            string mark_template = mark[0].Name;
            mark_template = mark_template.Trim().Replace("[date]", DateTime.Now.ToString("dd-MM-yyyy"))
                               .Replace("[gender]", "Grils");
            MySqlConnection con1 = new MySqlConnection(Login.constring);
            con1.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT class.class_id, class.name, concat( FORMAT(COUNT(attendance.student_id), 0),'/',FORMAT(count(*),0)) AS Present FROM student join class on class.class_id=student.class_id left JOIN attendance ON attendance.student_id= student.student_id and attendance.status= 1 and attendance.date= current_date() where student.sex= 'Female' GROUP by class.class_id", con1);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    sms = mark_template.Split('\\')[1];
                    final_sms += sms.Trim().Replace("[class]", reader1["name"].ToString())
                                                    .Replace("[summary]", reader1["Present"].ToString()) + "\n\r";
                    present += int.Parse(reader1["Present"].ToString().Split('/')[0]);
                    total += int.Parse(reader1["Present"].ToString().Split('/')[1]);
                }
            }
            con1.Close();
            sms = mark_template.Split('\\')[1];
            final_sms += sms.Trim().Replace("[class]", "Total")
                 .Replace("[summary]", present.ToString() + "/" + total.ToString());

            mark_template = mark_template.Split('\\')[0] + "\n\r" + final_sms;

            var phone = fun.GetSetting("student_absent_mobile");
            var tels = phone[0].Name.Split(',');
            foreach (var val in tels)
            {
                con1.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT into sms_que(mobile,sms,status) VALUES('" + val.Trim() + "','" + mark_template + "','" + 0 + "');", con1);
                cmd.ExecuteNonQuery();
                con1.Close();
            }

        }
    }
}


