using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolManagementSystem.Discipline
{
    public partial class IndividualTeacherPoints : DevExpress.XtraEditors.XtraUserControl
    {
        private static IndividualTeacherPoints _instance;

        public static IndividualTeacherPoints instance
        {
            get
            {
                if (_instance == null)
                    _instance = new IndividualTeacherPoints();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();

        CommonFunctions fun = new CommonFunctions();
        public IndividualTeacherPoints()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            fun.DateFormat(txtDateFrom);
            fun.DateFormat(txtDateTO);
            txtTeacher.Properties.Items.Clear();
            allClass.Clear();
            allClass = fun.GetAllTeacherWId();
            foreach (var allclass in allClass)
                txtTeacher.Properties.Items.Add(allclass.Name);
        }
        private void FillGridStaffReport()
        {

            // DateTime date = txtDateTO.DateTime;
            var tempdate = txtDateTO.DateTime.Month + "-" + txtDateTO.DateTime.Day + "-" + txtDateTO.DateTime.Year;
            DateTime date = Convert.ToDateTime(tempdate);
            string from = Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd");
            string to = Convert.ToDateTime(txtDateTO.Text).ToString("yyyy-MM-dd");
            var query = "";
            if (txtSummery.Checked == false) { 
                int teacher_id = fun.GetTeacherID(txtTeacher.Text);
                query = "SELECT *,(attendance_time+dress+class_time+paper_submission+paper_days+course_plan) AS total FROM tbl_bonus_points WHERE on_date BETWEEN '{0}' AND '{1}'  AND teacher_id = '{2}' ORDER BY on_date DESC";
                query = string.Format(query, from, to, teacher_id);
            } 
            else
            {
                string where = "";
                string StaffType = txtStaffType.Text;
                if(StaffType != "")
                {
                    where = " and teacher.staff_type = '"+ StaffType + "' ";
                }
                query = "SELECT tbp.teacher_id,teacher.name, sum(attendance_time) as attendance_time,sum(dress) as dress,sum(class_time) as class_time,sum(paper_submission) as paper_submission,"+
                            " sum(paper_days) as paper_days,sum(course_plan) as course_plan,sum(attendance_time + dress + class_time + paper_submission + paper_days + course_plan) AS total "+
                            " FROM tbl_bonus_points as tbp "+
                            " inner join teacher on teacher.teacher_id = tbp.teacher_id "+
                            " WHERE tbp.on_date BETWEEN '{0}' AND '{1}' "+ where+
                            " group by  tbp.teacher_id";
                query = string.Format(query, from, to);
            }
            DataTable table = fun.FetchDataTable(query);
            gridIndividualTeacherAttendance.DataSource = null;
            gridView1.Columns.Clear();
            gridIndividualTeacherAttendance.DataSource = CommonFunctions.AutoNumberedTable(table);
            gridView1.BestFitColumns();

            
            GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "attendance_time", "{0}");
            GridColumnSummaryItem item2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "dress", "{0}");
            GridColumnSummaryItem item3 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "class_time", "{0}");
            GridColumnSummaryItem item4 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "paper_submission", "{0}");
            GridColumnSummaryItem item5 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "paper_days", "{0}");
            GridColumnSummaryItem item6 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "course_plan", "{0}");
            GridColumnSummaryItem item7 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "total", "{0}");

            gridView1.Columns["attendance_time"].Summary.Add(item1);
            gridView1.Columns["dress"].Summary.Add(item2);
            gridView1.Columns["class_time"].Summary.Add(item3);
            gridView1.Columns["paper_submission"].Summary.Add(item4);
            gridView1.Columns["paper_days"].Summary.Add(item5);
            gridView1.Columns["course_plan"].Summary.Add(item6);
            gridView1.Columns["total"].Summary.Add(item7);

        }

        private void btnfind_Click(object sender, EventArgs e)
        {
           
            
                if (txtTeacher.Text == "")
                {
                    MessageBox.Show("Must select teacher Name for individual teacher attendance....!!", "Info");
                    return;
                }
                FillGridStaffReport();
                showReport();
           
        }
        void showReport()
        {
            string info = "";
            allClass.Clear();
            allClass = fun.GetAllTeacherWId();
            foreach (var allclass in allClass)
            {
                if (allclass.Name == txtTeacher.Text)
                {
                    info = allclass.Salary;
                }
            }

         
            if (txtSummery.Checked == false)
            {
                XtraReportPerson report = new XtraReportPerson();
                Image logo = fun.Base64ToImage(Login.Logo);
                var school = fun.GetSettings("system_title");
                report.PicIogoBox.Image = logo;
                report.LabTitle.Text = school;
                report.labDate.Text = txtDateTO.Text + " to " + txtDateFrom.Text;

                report.LabAddress.Text = fun.GetSettings("address");
                report.labName.Text = txtTeacher.Text;
                report.LabNumber.Text = info.Split('>')[1];
                report.LabSAddress.Text = info.Split('>')[2];
                report.LabDes.Text = info.Split('>')[3];
                report.LabTel.Text = "Staff Earning Points Report";
                report.GridControl = gridIndividualTeacherAttendance;
                documentViewer1.DocumentSource = report;
                report.xrPanel1.Visible = true;
                report.CreateDocument();
                documentViewer1.PrintingSystem.Document.AutoFitToPagesWidth = 1;

            }
            else
            {
                XtraReportHead report = new XtraReportHead();
                Image logo = fun.Base64ToImage(Login.Logo);
                var school = fun.GetSettings("system_title");
                report.PicIogoBox.Image = logo;
                report.LabTitle.Text = school;
                report.labDate.Text = txtDateTO.Text + " to " + txtDateFrom.Text;
                report.LabTel.Text = "Staff Earning Points Report";
                report.GridControl = gridIndividualTeacherAttendance;
                documentViewer1.DocumentSource = report;
                report.CreateDocument();
                documentViewer1.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            }
            
            
            
        }

        private void txtSummery_CheckedChanged(object sender, EventArgs e)
        {
            if (txtSummery.Checked == false)
            {
                txtStaffType.Enabled = false;
                txtTeacher.Enabled = true;
            }
            else
            {
                txtStaffType.Enabled = true;
                txtTeacher.Enabled = false;
            }
        }
    }
}
