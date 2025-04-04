using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem.Fees
{
    public partial class subject_fees : Form
    {
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        ObservableCollection<AllStudent> allStudents;
        CommonFunctions fun = new CommonFunctions();
        public subject_fees()
        {
            InitializeComponent();
            fun.selectall_Controls(this);
            txtMonthGI.Properties.Items.Clear();
            foreach (MonthOfYear item in Enum.GetValues(typeof(MonthOfYear)))
            {
                var S = item.GetHashCode();
                txtMonthGI.Properties.Items.Add(item);
            }
            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";
        }
        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            if(txtClass.EditValue != null && !string.IsNullOrEmpty(txtClass.EditValue.ToString()))
            {
                txtSection.Properties.DataSource = fun.GetAllSection_dt(txtClass.EditValue.ToString());
                txtSection.Properties.DisplayMember = "name";
                txtSection.Properties.ValueMember = "section_id";
            }
        }
        private void txtSection_EditValueChanged(object sender, EventArgs e)
        {
            if(txtSection.EditValue != null && !string.IsNullOrEmpty(txtSection.EditValue.ToString()))
            {
                var sms = " AND student.passout != 1 and student.student_id in(SELECT student_id FROM `fee_by_subject_teacher` WHERE `student_id` = student.student_id group by student.student_id) ";
                txtStudent.Properties.DataSource = fun.GetAllStudentsinSection(txtSection.EditValue.ToString(), sms);
                txtStudent.Properties.DisplayMember = "name";
                txtStudent.Properties.ValueMember = "student_id";
            }
        }
        private void btnIG_Click(object sender, EventArgs e)
        {
            if (txtMonthGI.Text == "" || txtYearGI.Text == "")
            {
                MessageBox.Show("Select Month & Year", "Info");
                return;
            }
            fun.loaderform(() =>
            {
                var allmonth = txtMonthGI.Text.Split(',');
                var number_month = allmonth.Length;
                var start_month = allmonth.First().Trim();
                var end_month = allmonth.Last().Trim();
                //get integer value for month
                foreach (MonthOfYear mon in Enum.GetValues(typeof(MonthOfYear)))
                {
                    if (start_month == mon.ToString())
                    {
                        start_month = mon.GetHashCode().ToString();
                    }

                    if (end_month == mon.ToString())
                    {
                        end_month = mon.GetHashCode().ToString();
                    }
                }
                int alreadyExit = 0;
                int pDue = 0;
                var allStd = txtStudent.EditValue.ToString().Split(',');
                String student_id, InsertQuery;
                long invoice_id;
                string date = txtYearGI.Text + "-" + start_month + "-" + DateTime.Now.Day;
                string end_date = txtYearGI.Text + "-" + end_month + "-" + DateTime.Now.Day;
                DateTime Date = Convert.ToDateTime(date);
                foreach (var std in allStd)
                {
                    student_id = std;
                    var query = "SELECT * FROM invoice WHERE student_id = '" + std + "' and MONTH(date) = '" + start_month + "' and year(date) = '" + txtYearGI.Text + "'; ";
                    DataTable chackstd = fun.FetchDataTable(query);
                    if (chackstd.Rows.Count > 0)
                    {
                        alreadyExit = 1;
                    }

                    if (alreadyExit == 0)
                    {
                        pDue = 0;
                        var p = "";
                        var classID = fun.get_student_class_id(std);
                        int stdid = int.Parse(std);
                        int total = 0; var des = "";
                        int total_concession = 0;
                        Date = Date.AddMonths(-1);
                        var query1 = "SELECT * FROM invoice WHERE student_id = '" + std + "' AND forward = 0 ORDER BY invoice_id DESC LIMIT 1";
                        DataTable chackstd1 = fun.FetchDataTable(query1);
                        if (chackstd1.Rows.Count > 0)
                        {
                            foreach (DataRow reader2 in chackstd1.Rows)
                            {
                                var due = reader2["due"].ToString();
                                p = (due != null) ? due : "";
                                //update forward 
                                query = "UPDATE invoice SET forward = 1 WHERE invoice_id = '{0}'";
                                query = string.Format(query, reader2["invoice_id"].ToString());
                                fun.ExecuteQuery(query);

                            }
                            pDue = Convert.ToInt32(p);
                        }
                        //subject  by fee calculation needed
                        int MFee = 0;

                        string format_date = DateTime.Now.ToString("yyyy-MM-dd");
                        query = "select *,sub.name as subject from fee_by_subject_teacher as fbst " +
                            " join subject_default as sub on sub.id = fbst.subject_id " +
                            " where fbst.student_id = '" + student_id + "'";
                        DataTable dtsubjects = fun.FetchDataTable(query);
                        for (int i = 0; i < dtsubjects.Rows.Count; i++)
                        {
                            total_concession += Convert.ToInt32(dtsubjects.Rows[i]["concession"]);
                            int Fee = Convert.ToInt32(dtsubjects.Rows[i]["amount"]);
                            des += dtsubjects.Rows[i]["subject"] + ": " + (Fee - Convert.ToInt32(dtsubjects.Rows[i]["concession"])) + ",";
                            MFee += Fee;
                        }
                        total = (pDue + MFee) - total_concession;
                        InsertQuery = "INSERT into invoice(Student_id,class_id,section_id,title,description,previous_fee,current_fee,fee_concession,amount,amount_paid,due,creation_timestamp,status,date,sync,to_date,number_month) " +
                            "VALUES('" + student_id + "',(SELECT class_id FROM student WHERE student_id = '" + student_id + "'),(SELECT section_id FROM student WHERE student_id = '" + student_id + "'),'Monthly Fee','" + des + "','" + pDue + "','" + MFee + "','" + total_concession + "','" + total + "','0','" + total + "','" + fun.GetTimestamp(Convert.ToDateTime(date)) + "','Unpaid','" + format_date + "','0','" + Convert.ToDateTime(end_date).ToString("yyyy-MM-dd") + "','" + number_month + "')";
                        invoice_id = fun.Execute_Insert(InsertQuery);
                    }
                    alreadyExit = 0;

                    //Forward Passout students 
                    var section_list = txtSection.EditValue.ToString();
                    var info = section_list.Split(',');
                    int section_id = 0;
                    foreach (var val in info)
                    {
                        section_id = int.Parse(val);
                        String forward_query = "UPDATE invoice SET forward = '1' " +
                            "WHERE forward != 1 AND student_id IN(SELECT IFNULL(student_id,0) AS student_id " +
                            "FROM student WHERE passout = 1 AND section_id  = '{0}')";
                        forward_query = string.Format(forward_query, section_id);
                        fun.ExecuteQuery(forward_query);
                    }
                }
            });

        }
    }
}
