using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;

namespace SchoolManagementSystem.Fees
{
    public partial class Subject_wise_fee_details : UserControl
    {
        private static Subject_wise_fee_details _instance;
        CommonFunctions fun = new CommonFunctions();
        public static Subject_wise_fee_details instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Subject_wise_fee_details();
                return _instance;
            }
        }
        public Subject_wise_fee_details()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            teacherlist();
        }
        void teacherlist()
        {
            string query = "select teacher_id as ID,name as Name,subject_code from teacher";

            GridteacherList.Properties.DataSource = fun.FetchDataTable(query);
            GridteacherList.Properties.DisplayMember = "Name";
            GridteacherList.Properties.ValueMember = "ID";
        }
        private void btn_Teacher_Click(object sender, EventArgs e)
        {
            load_teacherData();
            isinstitute = false;
        }

        void load_teacherData()
        {
            if (string.IsNullOrEmpty(dtp_Month.Text.Trim()))
            {
                MessageBox.Show("Select Month");
                return;
            }
            if (GridteacherList.EditValue == null)
            {
                MessageBox.Show("Select teacher");
                return;
            }
            fun.loaderform(() =>
            {
                
                string query = "set @teacher_total:=0;set @teacher_paid:=0;set @institute_amount:=0;SELECT student_id,Student,Class,Total,Paid,due,Salary AS 'teach%',teacher_id," +
                                " @teacher_total := student_amount + teacher_amount AS TeacherTotal," +
                                " @teacher_paid := FORMAT(((@teacher_total * (Paid / Total * 100) / 100) * Salary / 100),0) AS Teacher_Paid," +
                                " FORMAT(((@teacher_total * Salary/100) - @teacher_paid),0) AS Teacher_Due," +
                                " @institute_amount := FORMAT((@teacher_total - (@teacher_total * Salary/100)),0) AS InstituteAmount," +
                                " (case when @teacher_paid > 0 then (FORMAT((@teacher_total - @teacher_paid),0) - @institute_amount) else @institute_amount end) AS InstituteDue,`date` " +
                                " FROM " +
                                " (SELECT std.student_id, std.name AS Student, cls.name AS Class, inv.amount AS Total, inv.amount_paid AS Paid, inv.due, tech.Salary, fbst.teacher_id," +
                                " (fbst.amount - fbst.concession) AS student_amount, inv.previous_fee, " +
                                " FORMAT((inv.previous_fee * ((fbst.amount - fbst.concession) / (inv.amount - inv.previous_fee) * 100) / 100), 0) AS teacher_amount,inv.date " +
                                " FROM `student` AS STD " +
                                " JOIN fee_by_subject_teacher AS fbst ON fbst.student_id = std.student_id and fbst.section_id = std.section_id " +
                                " JOIN teacher AS tech ON tech.teacher_id = fbst.teacher_id " +
                                " JOIN class AS cls ON cls.class_id = std.class_id " +
                                " JOIN invoice AS inv ON inv.student_id = std.student_id " +
                                " WHERE DATE_FORMAT(inv.date,'%Y-%m') = '" + dtp_Month.Text.Trim() + "' and std.passout =0 AND fbst.teacher_id = '" + GridteacherList.EditValue.ToString() + "') AS tb";

                gridControl1.BeginUpdate();
                try
                {
                    gridView1.Columns.Clear();
                    gridControl1.DataSource = null;
                    gridControl1.DataSource = fun.FetchDataTable(query);
                }
                finally
                {
                    gridControl1.EndUpdate();
                }

                foreach (GridColumn col in gridView1.Columns)
                {
                    string c = col.FieldName;
                    if (c != "teach%")
                        gridView1.Columns[c].OptionsColumn.ReadOnly = true;
                    if (c != "Class" && c != "Student" && c != "student_id" && c != "teach%")
                    {
                        GridGroupSummaryItem item4 = new GridGroupSummaryItem();
                        item4.FieldName = c;
                        item4.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        item4.DisplayFormat = "{0:0.##}";
                        item4.ShowInGroupColumnFooter = gridView1.Columns[c];
                        gridView1.GroupSummary.Add(item4);

                        GridColumnSummaryItem item = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, c, "{0}");
                        gridView1.Columns[c].Summary.Clear();
                        gridView1.Columns[c].Summary.Add(item);
                    }
                }
            });
        }

        bool isinstitute = false;
        private void btn_Institute_Click(object sender, EventArgs e)
        {
            load_instituedata();
            isinstitute = true;
        }
         
        void load_instituedata()
        {
            if (string.IsNullOrEmpty(dtp_Month.Text.Trim()))
            {
                MessageBox.Show("Select Month");
                return;
            }
            fun.loaderform(() =>
            {
                string query = "SET @teacher_total=0;SET @teacher_paid=0;SET @institute_amount=0; " +
                " SELECT teacher_id, teacher, Salary AS 'teach%', SUM(@teacher_total:= (student_amount + teacher_amount)) AS TeacherTotal, " +
                " SUM(@teacher_paid:= FORMAT(((@teacher_total * (Paid / Total * 100) / 100) * Salary / 100), 0)) AS Teacher_Paid, " +
                " SUM(FORMAT(((@teacher_total * Salary / 100) - @teacher_paid), 0)) AS Teacher_Due, " +
                " SUM(@institute_amount:= FORMAT((@teacher_total - (@teacher_total * Salary / 100)), 0)) AS InstituteAmount, " +
                " SUM(CASE WHEN @teacher_paid > 0 THEN(FORMAT((@teacher_total - @teacher_paid), 0) - @institute_amount) ELSE @institute_amount END) AS InstituteDue " +
                " FROM " +
                " (SELECT std.student_id, std.name AS Student, cls.name AS Class, inv.amount AS Total, inv.amount_paid AS Paid, inv.due, tech.Salary, fbst.teacher_id,tech.name AS teacher, " +
                " (fbst.amount - fbst.concession) AS student_amount, inv.previous_fee, FORMAT((inv.previous_fee * ((fbst.amount - fbst.concession) / (inv.amount - inv.previous_fee) * 100) / 100), 0) AS teacher_amount " +
                " FROM `student` AS STD " +
                " JOIN fee_by_subject_teacher AS fbst ON fbst.student_id = std.student_id and fbst.section_id = std.section_id " +
                " JOIN teacher AS tech ON tech.teacher_id = fbst.teacher_id " +
                " JOIN class AS cls ON cls.class_id = std.class_id " +
                " JOIN invoice AS inv ON inv.student_id = std.student_id " +
                " WHERE DATE_FORMAT(inv.date,'%Y-%m') = '" + dtp_Month.Text.Trim() + "' and STD.passout =0 ) AS tb GROUP BY tb.teacher_id";
                gridControl1.BeginUpdate();
                try
                {
                    gridView1.Columns.Clear();
                    gridControl1.DataSource = null;
                    gridControl1.DataSource = fun.FetchDataTable(query);
                }
                finally
                {
                    gridControl1.EndUpdate();
                }

                foreach (GridColumn col in gridView1.Columns)
                {
                    string c = col.FieldName;
                    if (c != "teach%")
                        gridView1.Columns[c].OptionsColumn.ReadOnly = true;
                    if (c != "Teacher" && c != "teacher_id" && c != "teach%")
                    {
                        GridGroupSummaryItem item4 = new GridGroupSummaryItem();
                        item4.FieldName = c;
                        item4.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        item4.DisplayFormat = "{0:0.##}";
                        item4.ShowInGroupColumnFooter = gridView1.Columns[c];
                        gridView1.GroupSummary.Add(item4);

                        GridColumnSummaryItem item = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, c, "{0}");
                        gridView1.Columns[c].Summary.Clear();
                        gridView1.Columns[c].Summary.Add(item);
                    }
                }

            });
        }
        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            string query = "UPDATE `teacher` SET `salary`='"+dr["teach%"] +"' WHERE teacher_id = '" + dr["teacher_id"] + "'";
            fun.ExecuteQuery(query);
            if (isinstitute)
                load_instituedata();
            else
                load_teacherData();
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            fun.exporttoexcel(gridControl1);
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            fun.ShowGridPreview(gridControl1);
        }
    }
}
