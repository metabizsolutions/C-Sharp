using DevExpress.LookAndFeel;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SchoolManagementSystem.Exam
{
    public partial class ManageMarks : DevExpress.XtraEditors.XtraUserControl
    {
        private static ManageMarks _instance;

        public static ManageMarks instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ManageMarks();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass;
        CommonFunctions fun = new CommonFunctions();

        public ManageMarks()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            txtExam.Properties.DataSource = fun.GetAllExams_dt();
            txtExam.Properties.DisplayMember = "name";
            txtExam.Properties.ValueMember = "exam_id";

            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";

            string subject_wise = fun.GetSettings("Institute_Type");
            if (subject_wise == "Subject Wise Institute")
            {
                btnmanag_byteacher.Visible = true;
                gridLookUpEdit_teacher.Visible = true;
                labelControl3.Visible = true;
            }
            else
            {
                btnmanag_byteacher.Visible = false;
                gridLookUpEdit_teacher.Visible = false;
                labelControl3.Visible = false;
            }
        }
        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            if (txtClass.EditValue != null)
            {
                txtSection.Properties.DataSource = fun.GetAllSection_dt(txtClass.EditValue.ToString());
                txtSection.Properties.DisplayMember = "name";
                txtSection.Properties.ValueMember = "section_id";
            }
            else
            {
                txtSection.EditValue = null;
                txtSubject.EditValue = null;
            }
        }
        private void txtSection_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSection.EditValue != null)
            {
                txtSubject.Properties.DataSource = fun.GetAllSubject_dt(txtSection.EditValue.ToString());
                txtSubject.Properties.DisplayMember = "name";
                txtSubject.Properties.ValueMember = "subject_id";
            }
            else
                txtSubject.EditValue = null;
        }
        int alreadyExit = 0;
        int classID, examID, section, subjectID;
        private void btnMMAdd_Click(object sender, EventArgs e)
        {
            fun.loaderform(() => { FillGridExamManege(""); });
            //managemarks("");
            //new_manage_marks("");
        }
        void new_manage_marks(string where)
        {
            if (txtExam.EditValue == null || txtClass.EditValue == null || txtSection.EditValue == null || txtSubject.EditValue == null)
            {
                MessageBox.Show("Fill all fields...!!", "Info");
                return;
            }
            try
            {
                fun.loaderform(() =>
                {
                    classID = Convert.ToInt32(txtClass.EditValue);
                    examID = Convert.ToInt32(txtExam.EditValue);
                    section = Convert.ToInt32(txtSection.EditValue);
                    subjectID = Convert.ToInt32(txtSubject.EditValue);
                    string query = "SELECT std.`student_id`,m.`mark_obtained`,tms.`marks` AS total_marks,if(att.`status` is null,0,att.`status`) AS attendance  FROM  student  as `std`  " +
                                " LEFT JOIN `attendance` AS att ON att.`student_id` = std.`student_id` AND  att.`date` = CURDATE() " +
                                " LEFT JOIN mark AS m ON m.`student_id`= std.student_id AND m.exam_id = '" + examID + "' AND m.`subject_id` = '" + subjectID + "' " +
                                " LEFT JOIN `tbl_mark_subject` AS tms ON tms.subject_id = '" + subjectID + "' AND tms.exam_id = '" + examID + "' " +
                                " WHERE std.passout = 0 AND std.`section_id` = '" + section + "' AND m.`mark_obtained` IS NULL " + where + "; ";
                    DataTable std_dt = fun.FetchDataTable(query);
                    var q = "";
                    foreach (DataRow dr in std_dt.Rows)
                    {
                        if (string.IsNullOrEmpty(dr["mark_obtained"].ToString()))
                        {
                            int markobtained = -1;

                            //query = "SELECT if(status is null,0,status) FROM attendance WHERE student_id = " + dr["student_id"] + " and date = CurDate()";
                            //object Attendance =  == null ? 0 : dr["attendance"];
                            if (Convert.ToInt32(dr["attendance"]) == 1)
                                markobtained = 0;
                            int total = dr["total_marks"] == null ? 0 : Convert.ToInt32(dr["total_marks"]);
                            var totalSubject = (total == 0) ? 0 : total;
                            q += "INSERT into mark(student_id,subject_id,class_id,section_id,exam_id,mark_obtained,mark_total,sync,ondate) VALUES " +
                                " ('" + dr["student_id"] + "','" + subjectID + "','" + classID + "','" + section + "','" + examID + "','" + markobtained + "','" + totalSubject + "','0','" + DateTime.Now.ToString("yyyy-MM-dd") + "');";
                            q += "UPDATE `tbl_mark_subject` SET submit_on='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where subject_id='" + subjectID + "' and exam_id='" + examID + "';";

                        }

                    }
                    if (q != "")
                        fun.ExecuteQuery(q);
                    //FillGridExamManege(where);
                });
            }
            catch (Exception ex)
            {

            }
        }

        private void FillGridExamManege(string where)
        {
            if (string.IsNullOrEmpty(txtExam.EditValue.ToString()) || string.IsNullOrEmpty(txtClass.EditValue.ToString()) || string.IsNullOrEmpty(txtSection.EditValue.ToString()) || string.IsNullOrEmpty(txtSubject.EditValue.ToString()))
            {
                MessageBox.Show("Fill all fields...!!", "Info");
                return;
            }
            var classID = txtClass.EditValue;
            var sectionID = txtSection.EditValue;
            var examID = txtExam.EditValue;
            var subjectID = txtSubject.EditValue;
            string query = "SELECT std.student_id,m.mark_id AS ID,std.roll AS `Roll No`, CONCAT( std.name,' / ',p.name)  AS Student,m.mark_obtained AS `Obtained ("+ txtSubject.Text + ")`,tms.marks AS Total,COMMENT AS Remarks " +
                            " FROM student AS STD " +
                            " JOIN parent AS p ON p.parent_id = std.parent_id " +
                            " LEFT JOIN tbl_mark_subject AS tms ON tms.subject_id = '" + subjectID + "' AND tms.exam_id = '" + examID + "' AND tms.section_id = '" + sectionID + "' " +
                            " LEFT JOIN mark AS m ON m.`student_id` = std.`student_id` AND m.exam_id = tms.`exam_id` AND m.subject_id = tms.`subject_id`  AND m.section_id = tms.`section_id` " +
                            " WHERE std.section_id = '" + sectionID + "' AND std.passout = 0 ORDER BY std.roll; ";
            DataTable table = fun.FetchDataTable(query);
            if (table.Rows.Count > 0 && string.IsNullOrEmpty(table.Rows[0]["Total"].ToString()))
            {
                MessageBox.Show(" No Data Found  against this section and Exam Please Select That Exam in 'Subject Total Marks' and Try Again", "Manage Marks Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var q = "";
            foreach (DataRow dr in table.Rows)
            {
                if (string.IsNullOrEmpty(dr["Obtained (" + txtSubject.Text + ")"].ToString()))
                {
                    int markobtained = -1;

                    query = "SELECT if(status is null,0,status) FROM attendance WHERE student_id = " + dr["student_id"] + " and date = CurDate()";
                    object Attendance = fun.Execute_Scaler_string(query);
                    if (Convert.ToInt32(Attendance) == 1)
                        markobtained = 0;
                    int total = dr["Total"] == null ? 0 : Convert.ToInt32(dr["Total"]);
                    var totalSubject = (total == 0) ? 0 : total;
                    q = "INSERT into mark(student_id,subject_id,class_id,section_id,exam_id,mark_obtained,mark_total,sync,ondate) VALUES " +
                        " ('" + dr["student_id"] + "','" + subjectID + "','" + classID + "','" + sectionID + "','" + examID + "','" + markobtained + "','" + totalSubject + "','0','" + DateTime.Now.ToString("yyyy-MM-dd") + "');";
                    long mark_id = fun.Execute_Insert(q);
                    q = "UPDATE `tbl_mark_subject` SET submit_on='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where subject_id='" + subjectID + "' and section_id = '"+sectionID+"' and exam_id='" + examID + "';";
                    fun.ExecuteQuery(q);
                    dr["Obtained (" + txtSubject.Text + ")"] = markobtained;
                    dr["ID"] = mark_id;
                }

            }
            if(table.Rows.Count <= 0)
            {
                MessageBox.Show("No Record Please insert students in this section","No Data",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            gridManageMarks.DataSource = null;
            gridView1.Columns.Clear();
            gridManageMarks.DataSource = CommonFunctions.AutoNumberedTable(table);
            gridView1.IndicatorWidth = 30;
            gridView1.BestFitColumns();
            gridView1.Columns["student_id"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["student_id"].Visible = false;
            var col = gridView1.Columns["ID"];
            col.OptionsColumn.ReadOnly = true;
            col.Visible = false;
            var col3 = gridView1.Columns["Roll No"];
            col3.OptionsColumn.ReadOnly = true;
            col3.Width = 55;
            var col1 = gridView1.Columns["Student"];
            col1.OptionsColumn.ReadOnly = true;
            var col2 = gridView1.Columns["Total"];
            col2.OptionsColumn.ReadOnly = true;
            GridColumn Column = gridView1.Columns["Total"];
            Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column2 = gridView1.Columns[5];// obtain column index in gridview
            Column2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column3 = gridView1.Columns["Sr#"];
            Column3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column4 = gridView1.Columns["Roll No"];
            Column4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            //Thread thread = new Thread(t =>
            //{
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            var query = "UPDATE `tbl_mark_subject` SET `check_list`=1,test_not_deliver='0' where subject_id='" + txtSubject.EditValue + "' and class_id = '"+txtClass.EditValue+"' and section_id = '"+txtSection.EditValue+"' and exam_id='" + examID + "';";
            query += "UPDATE mark set mark_obtained='" + row["Obtained (" + txtSubject.Text + ")"] + "',comment='" + row["Remarks"] + "',sync='0',ondate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' WHERE mark_id='" + row["ID"] + "';";
            fun.ExecuteQuery(query);
            //})
            //{ IsBackground = true };
            //thread.Start();
        }

        private void gridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridColumn obtained = gridView1.Columns["Obtained (" + txtSubject.Text + ")"];
            GridColumn total = gridView1.Columns["Total"];

            int unitInObtained = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, obtained));
            int unitInTotal = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, total));
            if (unitInTotal < unitInObtained)
            {
                gridView1.SetColumnError(obtained, "The value should be less then total marks.");
                gridView1.SetColumnError(total, "The Marks on obtained should be less then this value.");
                gridView1.SetColumnError(null, "Invalid data");
                e.Valid = false;
                e.ErrorText = "'Mark In Obtained' and 'Marks In total' values are not consistent.";
            }
        }

        private void gridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void ManageMarks_Enter(object sender, EventArgs e)
        {
            //loadfunctions();
        }

        private void btnmanag_byteacher_Click(object sender, EventArgs e)
        {
            if (txtExam.EditValue == null || txtClass.EditValue == null || txtSection.EditValue == null || txtSubject.EditValue == null || gridLookUpEdit_teacher.EditValue == null)
            {
                MessageBox.Show("Fill all fields...!!", "Info");
                return;
            }
            else
            {
                classID = Convert.ToInt32(txtClass.EditValue);
                examID = Convert.ToInt32(txtExam.EditValue);
                section = Convert.ToInt32(txtSection.EditValue);
                subjectID = Convert.ToInt32(txtSubject.EditValue);

                //manag marks
                //managemarks(" and student.student_id in (SELECT student_id FROM fee_by_subject_teacher WHERE teacher_id ='" + gridLookUpEdit_teacher.EditValue + "')  ");
                fun.loaderform(() =>
                {
                    FillGridExamManege(" and student.student_id in (SELECT student_id FROM fee_by_subject_teacher WHERE teacher_id ='" + gridLookUpEdit_teacher.EditValue + "')  ");
                });
            }
        }

        private void btn_upload_excel_Click(object sender, EventArgs e)
        {
            if (txtExam.EditValue == null || txtClass.EditValue == null || txtSection.EditValue == null || txtSubject.EditValue == null)
            {
                MessageBox.Show("Fill all fields...!!", "Info");
                return;
            }
            classID = Convert.ToInt32(txtClass.EditValue);
            examID = Convert.ToInt32(txtExam.EditValue);
            section = Convert.ToInt32(txtSection.EditValue);
            subjectID = Convert.ToInt32(txtSubject.EditValue);
            string[] data = { classID.ToString(), examID.ToString(), section.ToString(), subjectID.ToString(), txtClass.Text, txtSection.Text, txtSubject.Text, txtExam.Text };
            using (upload_manage_marks mark = new upload_manage_marks(data))
            {
                if (mark.ShowDialog() == DialogResult.Yes)
                {

                }
                else
                {
                    FillGridExamManege("");
                    //managemarks("");
                }
            }
        }
        private void txtSubject_EditValueChanged(object sender, EventArgs e)
        {
            string query = "SELECT `teacher_id`,name FROM `teacher` WHERE `subject_code` ='" + txtSubject.Text + "'";
            gridLookUpEdit_teacher.Properties.DataSource = fun.FetchDataTable(query);
            gridLookUpEdit_teacher.Properties.DisplayMember = "name";
            gridLookUpEdit_teacher.Properties.ValueMember = "teacher_id";
        }

        private void btn_download_excel_Click(object sender, EventArgs e)
        {
            if (txtExam.EditValue == null || txtClass.EditValue == null || txtSection.EditValue == null || txtSubject.EditValue == null)
            {
                MessageBox.Show("Fill all fields...!!", "Info");
                return;
            }
            classID = Convert.ToInt32(txtClass.EditValue);
            examID = Convert.ToInt32(txtExam.EditValue);
            section = Convert.ToInt32(txtSection.EditValue);
            subjectID = Convert.ToInt32(txtSubject.EditValue);
            string query = "SELECT std.student_id,std.roll AS `Roll No`,cls.name as `class`,sec.name as `section`, CONCAT( std.name,' / ',p.name)  AS Student,m.mark_obtained AS `Obtain Marks`,tms.marks AS Total,COMMENT AS Remarks " +
                            " FROM student AS STD " +
                            " join class as cls on cls.class_id = STD.class_id " +
                            " join section as sec on sec.section_id = STD.section_id " +
                            " JOIN parent AS p ON p.parent_id = std.parent_id " +
                            " LEFT JOIN mark AS m ON m.`student_id` = std.`student_id` AND m.exam_id = '" + examID + "' AND m.subject_id = '" + subjectID + "' " +
                            " LEFT JOIN tbl_mark_subject AS tms ON tms.subject_id = '" + subjectID + "' AND tms.exam_id = '" + examID + "' " +
                            " WHERE std.section_id = '" + section + "' AND std.passout = 0 ORDER BY std.roll; ";
            DataTable table = fun.FetchDataTable(query);
            exportto_excel_grid.DataSource = table;
            gridView6.BestFitColumns();
            fun.exporttoexcel(exportto_excel_grid);
        }

        private void btnMMPrintP_Click(object sender, EventArgs e)
        {
            XtraManageMarks report = new XtraManageMarks();
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.LabAddress.Text = fun.GetSettings("address");
            report.GridControl = gridManageMarks;
            report.LabExam.Text = txtExam.Text;
            report.labClass.Text = txtClass.Text;
            report.LabSection.Text = txtSection.Text;
            report.LabSubject.Text = txtSubject.Text;
            report.LabDate.Text = DateTime.Now.ToShortDateString();
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }
        private void GridView1_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName != "Obtained") return;
            GridView gv = sender as GridView;
            if (e.Column.FieldName == "Obtained")
            {
                gv.SetRowCellValue(e.RowHandle, gv.Columns[""], null);
            }
        }
        private void gridManageMarks_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gridView1.MoveNext();
            }
        }

    }
}
