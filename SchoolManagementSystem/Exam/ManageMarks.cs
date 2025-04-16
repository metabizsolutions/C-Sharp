using DevExpress.LookAndFeel;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
        private List<int> selectedSubjectIds = new List<int>();
        private bool isSingleSubjectMode = true;

        // Class to hold subject data
        private class SubjectItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name; // Display Name in CheckedListBox
        }

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

            txtExamTime.Properties.Mask.EditMask = "HH:mm";
            txtExamTime.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;

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

            txtSubject.Visible = true;
            checkedListBoxSubjects.Visible = false;
            simpleButtonToggleMode.Text = "Switch to Multi-Subject Mode";
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
                checkedListBoxSubjects.Items.Clear();
            }
        }

        private void txtSection_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSection.EditValue != null)
            {
                DataTable subjectsDt = fun.GetAllSubject_dt(txtSection.EditValue.ToString());
                txtSubject.Properties.DataSource = subjectsDt;
                txtSubject.Properties.DisplayMember = "name";
                txtSubject.Properties.ValueMember = "subject_id";

                checkedListBoxSubjects.Items.Clear();
                foreach (DataRow row in subjectsDt.Rows)
                {
                    checkedListBoxSubjects.Items.Add(new SubjectItem
                    {
                        Id = Convert.ToInt32(row["subject_id"]),
                        Name = row["name"].ToString()
                    }, false);
                }
            }
            else
            {
                txtSubject.EditValue = null;
                checkedListBoxSubjects.Items.Clear();
            }
        }

        private void simpleButtonToggleMode_Click(object sender, EventArgs e)
        {
            isSingleSubjectMode = !isSingleSubjectMode;
            if (isSingleSubjectMode)
            {
                txtSubject.Visible = true;
                checkedListBoxSubjects.Visible = false;
                simpleButtonToggleMode.Text = "Switch to Multi-Subject Mode";
            }
            else
            {
                txtSubject.Visible = false;
                checkedListBoxSubjects.Visible = true;
                simpleButtonToggleMode.Text = "Switch to Single Subject Mode";
            }
        }

        int alreadyExit = 0;
        int classID, examID, section;

        private void btnMMAdd_Click(object sender, EventArgs e)
        {
            fun.loaderform(() => { FillGridExamManage(""); });
        }

        void new_manage_marks(string where)
        {
            if (txtExam.EditValue == null || txtClass.EditValue == null || txtSection.EditValue == null ||
                (isSingleSubjectMode && txtSubject.EditValue == null) || (!isSingleSubjectMode && checkedListBoxSubjects.CheckedItems.Count == 0))
            {
                MessageBox.Show("Fill all fields including at least one subject...!!", "Info");
                return;
            }

            try
            {
                fun.loaderform(() =>
                {
                    classID = Convert.ToInt32(txtClass.EditValue);
                    examID = Convert.ToInt32(txtExam.EditValue);
                    section = Convert.ToInt32(txtSection.EditValue);
                    string examTime = txtExamTime.Text;

                    selectedSubjectIds.Clear();
                    if (isSingleSubjectMode)
                    {
                        selectedSubjectIds.Add(Convert.ToInt32(txtSubject.EditValue));
                    }
                    else
                    {
                        foreach (SubjectItem item in checkedListBoxSubjects.CheckedItems)
                        {
                            selectedSubjectIds.Add(item.Id);
                        }
                    }

                    string query = "";
                    foreach (int subjectID in selectedSubjectIds)
                    {
                        query = "SELECT std.`student_id`, m.`mark_obtained`, tms.`marks` AS total_marks, IF(att.`status` IS NULL, 0, att.`status`) AS attendance " +
                                "FROM student AS std " +
                                "LEFT JOIN `attendance` AS att ON att.`student_id` = std.`student_id` AND att.`date` = CURDATE() " +
                                "LEFT JOIN mark AS m ON m.`student_id` = std.student_id AND m.exam_id = '" + examID + "' AND m.`subject_id` = '" + subjectID + "' " +
                                "LEFT JOIN `tbl_mark_subject` AS tms ON tms.subject_id = '" + subjectID + "' AND tms.exam_id = '" + examID + "' " +
                                "WHERE std.passout = 0 AND std.`section_id` = '" + section + "' AND m.`mark_obtained` IS NULL " + where + "; ";

                        DataTable std_dt = fun.FetchDataTable(query);
                        string q = "";
                        foreach (DataRow dr in std_dt.Rows)
                        {
                            if (string.IsNullOrEmpty(dr["mark_obtained"].ToString()))
                            {
                                int markobtained = -1;
                                if (Convert.ToInt32(dr["attendance"]) == 1)
                                    markobtained = 0;
                                int total = dr["total_marks"] == null ? 0 : Convert.ToInt32(dr["total_marks"]);
                                var totalSubject = (total == 0) ? 0 : total;
                                q += "INSERT INTO mark(student_id, subject_id, class_id, section_id, exam_id, mark_obtained, mark_total, sync, ondate) VALUES " +
                                     "('" + dr["student_id"] + "','" + subjectID + "','" + classID + "','" + section + "','" + examID + "','" + markobtained + "','" + totalSubject + "','0','" + DateTime.Now.ToString("yyyy-MM-dd") + "');";
                                q += "UPDATE `tbl_mark_subject` SET submit_on='" + DateTime.Now.ToString("yyyy-MM-dd") + "' WHERE subject_id='" + subjectID + "' AND exam_id='" + examID + "';";
                            }
                        }
                        if (q != "")
                            fun.ExecuteQuery(q);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }

        private void FillGridExamManage(string where)
        {
            if (txtExam.EditValue == null || txtClass.EditValue == null || txtSection.EditValue == null ||
                (isSingleSubjectMode && txtSubject.EditValue == null) || (!isSingleSubjectMode && checkedListBoxSubjects.CheckedItems.Count == 0))
            {
                MessageBox.Show("Fill all fields including at least one subject...!!", "Info");
                return;
            }

            classID = Convert.ToInt32(txtClass.EditValue);
            section = Convert.ToInt32(txtSection.EditValue);
            examID = Convert.ToInt32(txtExam.EditValue);

            selectedSubjectIds.Clear();
            if (isSingleSubjectMode)
            {
                selectedSubjectIds.Add(Convert.ToInt32(txtSubject.EditValue));
            }
            else
            {
                foreach (SubjectItem item in checkedListBoxSubjects.CheckedItems)
                {
                    selectedSubjectIds.Add(item.Id);
                }
            }

            DataTable combinedTable = new DataTable();
            combinedTable.Columns.Add("student_id", typeof(int));
            combinedTable.Columns.Add("mark_id", typeof(long));
            combinedTable.Columns.Add("Roll No", typeof(string));
            combinedTable.Columns.Add("Student", typeof(string));
            foreach (int subjectID in selectedSubjectIds)
            {
                string subjectName = fun.Execute_Scaler_string($"SELECT name FROM subject WHERE subject_id = '{subjectID}'") as string;
                combinedTable.Columns.Add($"Obtained ({subjectName})", typeof(int));
            }
            combinedTable.Columns.Add("Total", typeof(int));
            combinedTable.Columns.Add("Remarks", typeof(string));

            foreach (int subjectID in selectedSubjectIds)
            {
                string query = "SELECT std.student_id, m.mark_id AS ID, std.roll AS `Roll No`, CONCAT(std.name, ' / ', p.name) AS Student, " +
                               "m.mark_obtained AS `Obtained`, tms.marks AS Total, m.COMMENT AS Remarks " +
                               "FROM student AS std " +
                               "JOIN parent AS p ON p.parent_id = std.parent_id " +
                               "LEFT JOIN tbl_mark_subject AS tms ON tms.subject_id = '" + subjectID + "' AND tms.exam_id = '" + examID + "' AND tms.section_id = '" + section + "' " +
                               "LEFT JOIN mark AS m ON m.`student_id` = std.`student_id` AND m.exam_id = tms.`exam_id` AND m.subject_id = tms.`subject_id` AND m.section_id = tms.`section_id` " +
                               "WHERE std.section_id = '" + section + "' AND std.passout = 0 ORDER BY std.roll;";

                DataTable table = fun.FetchDataTable(query);
                if (table.Rows.Count > 0 && string.IsNullOrEmpty(table.Rows[0]["Total"].ToString()))
                {
                    MessageBox.Show("No Data Found for this section and exam. Please configure 'Subject Total Marks' and try again.", "Manage Marks Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (DataRow dr in table.Rows)
                {
                    DataRow[] existingRows = combinedTable.Select($"student_id = '{dr["student_id"]}'");
                    DataRow existingRow = existingRows.Length > 0 ? existingRows[0] : combinedTable.NewRow();
                    if (existingRows.Length == 0)
                    {
                        existingRow["student_id"] = dr["student_id"];
                        existingRow["mark_id"] = dr["ID"];
                        existingRow["Roll No"] = dr["Roll No"];
                        existingRow["Student"] = dr["Student"];
                        combinedTable.Rows.Add(existingRow);
                    }
                    string subjectName = fun.Execute_Scaler_string($"SELECT name FROM subject WHERE subject_id = '{subjectID}'") as string;
                    existingRow[$"Obtained ({subjectName})"] = dr["Obtained"] == DBNull.Value ? (object)null : Convert.ToInt32(dr["Obtained"]);
                    existingRow["Total"] = dr["Total"];
                    existingRow["Remarks"] = dr["Remarks"];
                }
            }

            if (combinedTable.Rows.Count <= 0)
            {
                MessageBox.Show("No Record. Please insert students in this section.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            gridManageMarks.DataSource = null;
            gridView1.Columns.Clear();
            gridManageMarks.DataSource = CommonFunctions.AutoNumberedTable(combinedTable);
            gridView1.IndicatorWidth = 30;
            gridView1.BestFitColumns();
            gridView1.Columns["student_id"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["student_id"].Visible = false;
            gridView1.Columns["mark_id"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["mark_id"].Visible = false;
            gridView1.Columns["Roll No"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["Roll No"].Width = 55;
            gridView1.Columns["Student"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["Total"].OptionsColumn.ReadOnly = true;

            foreach (GridColumn col in gridView1.Columns)
            {
                col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            string query = "";
            foreach (int subjectID in selectedSubjectIds)
            {
                string subjectName = fun.Execute_Scaler_string($"SELECT name FROM subject WHERE subject_id = '{subjectID}'") as string;
                query += $"UPDATE `tbl_mark_subject` SET `check_list`=1, test_not_deliver='0' WHERE subject_id='{subjectID}' AND class_id='{txtClass.EditValue}' AND section_id='{txtSection.EditValue}' AND exam_id='{examID}';";
                query += $"UPDATE mark SET mark_obtained='{row[$"Obtained ({subjectName})"]}', comment='{row["Remarks"]}', sync='0', ondate='{DateTime.Now.ToString("yyyy-MM-dd")}' WHERE mark_id='{row["mark_id"]}';";
            }
            fun.ExecuteQuery(query);
        }

        private void gridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            foreach (int subjectID in selectedSubjectIds)
            {
                string subjectName = fun.Execute_Scaler_string($"SELECT name FROM subject WHERE subject_id = '{subjectID}'") as string;
                GridColumn obtained = gridView1.Columns[$"Obtained ({subjectName})"];
                GridColumn total = gridView1.Columns["Total"];

                object obtainedValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, obtained);
                object totalValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, total);

                int unitInObtained = obtainedValue == null || obtainedValue == DBNull.Value ? 0 : Convert.ToInt32(obtainedValue);
                int unitInTotal = totalValue == null || totalValue == DBNull.Value ? 0 : Convert.ToInt32(totalValue);
                if (unitInTotal < unitInObtained)
                {
                    gridView1.SetColumnError(obtained, "The value should be less than total marks.");
                    gridView1.SetColumnError(total, "The Marks on obtained should be less than this value.");
                    gridView1.SetColumnError(null, "Invalid data");
                    e.Valid = false;
                    e.ErrorText = "'Mark In Obtained' and 'Marks In Total' values are not consistent.";
                }
            }
        }

        private void gridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void btnmanag_byteacher_Click(object sender, EventArgs e)
        {
            if (txtExam.EditValue == null || txtClass.EditValue == null || txtSection.EditValue == null ||
                (isSingleSubjectMode && txtSubject.EditValue == null) || (!isSingleSubjectMode && checkedListBoxSubjects.CheckedItems.Count == 0) ||
                gridLookUpEdit_teacher.EditValue == null)
            {
                MessageBox.Show("Fill all fields including at least one subject...!!", "Info");
                return;
            }
            classID = Convert.ToInt32(txtClass.EditValue);
            examID = Convert.ToInt32(txtExam.EditValue);
            section = Convert.ToInt32(txtSection.EditValue);
            fun.loaderform(() =>
            {
                FillGridExamManage(" AND student.student_id IN (SELECT student_id FROM fee_by_subject_teacher WHERE teacher_id ='" + gridLookUpEdit_teacher.EditValue + "') ");
            });
        }

        private void btn_upload_excel_Click(object sender, EventArgs e)
        {
            if (txtExam.EditValue == null || txtClass.EditValue == null || txtSection.EditValue == null ||
                (isSingleSubjectMode && txtSubject.EditValue == null) || (!isSingleSubjectMode && checkedListBoxSubjects.CheckedItems.Count == 0))
            {
                MessageBox.Show("Fill all fields including at least one subject...!!", "Info");
                return;
            }
            classID = Convert.ToInt32(txtClass.EditValue);
            examID = Convert.ToInt32(txtExam.EditValue);
            section = Convert.ToInt32(txtSection.EditValue);
            selectedSubjectIds.Clear();
            if (isSingleSubjectMode)
            {
                selectedSubjectIds.Add(Convert.ToInt32(txtSubject.EditValue));
            }
            else
            {
                foreach (SubjectItem item in checkedListBoxSubjects.CheckedItems)
                {
                    selectedSubjectIds.Add(item.Id);
                }
            }
            string subjectIds = string.Join(",", selectedSubjectIds);
            string[] data = { classID.ToString(), examID.ToString(), section.ToString(), subjectIds, txtClass.Text, txtSection.Text, isSingleSubjectMode ? txtSubject.Text : checkedListBoxSubjects.Text, txtExam.Text };
            using (upload_manage_marks mark = new upload_manage_marks(data))
            {
                if (mark.ShowDialog() == DialogResult.Yes)
                {
                }
                else
                {
                    FillGridExamManage("");
                }
            }
        }

        private void txtSubject_EditValueChanged(object sender, EventArgs e)
        {
            if (isSingleSubjectMode && txtSubject.EditValue != null)
            {
                string query = "SELECT `teacher_id`, name FROM `teacher` WHERE `subject_code` ='" + txtSubject.Text + "'";
                gridLookUpEdit_teacher.Properties.DataSource = fun.FetchDataTable(query);
                gridLookUpEdit_teacher.Properties.DisplayMember = "name";
                gridLookUpEdit_teacher.Properties.ValueMember = "teacher_id";
            }
        }

        private void btn_download_excel_Click(object sender, EventArgs e)
        {
            if (txtExam.EditValue == null || txtClass.EditValue == null || txtSection.EditValue == null ||
                (isSingleSubjectMode && txtSubject.EditValue == null) || (!isSingleSubjectMode && checkedListBoxSubjects.CheckedItems.Count == 0))
            {
                MessageBox.Show("Fill all fields including at least one subject...!!", "Info");
                return;
            }
            classID = Convert.ToInt32(txtClass.EditValue);
            examID = Convert.ToInt32(txtExam.EditValue);
            section = Convert.ToInt32(txtSection.EditValue);
            selectedSubjectIds.Clear();
            if (isSingleSubjectMode)
            {
                selectedSubjectIds.Add(Convert.ToInt32(txtSubject.EditValue));
            }
            else
            {
                foreach (SubjectItem item in checkedListBoxSubjects.CheckedItems)
                {
                    selectedSubjectIds.Add(item.Id);
                }
            }

            DataTable exportTable = new DataTable();
            exportTable.Columns.Add("student_id", typeof(int));
            exportTable.Columns.Add("Roll No", typeof(string));
            exportTable.Columns.Add("class", typeof(string));
            exportTable.Columns.Add("section", typeof(string));
            exportTable.Columns.Add("Student", typeof(string));
            foreach (int subjectID in selectedSubjectIds)
            {
                string subjectName = fun.Execute_Scaler_string($"SELECT name FROM subject WHERE subject_id = '{subjectID}'") as string;
                exportTable.Columns.Add($"Obtain Marks ({subjectName})", typeof(int));
            }
            exportTable.Columns.Add("Total", typeof(int));
            exportTable.Columns.Add("Remarks", typeof(string));

            foreach (int subjectID in selectedSubjectIds)
            {
                string query = "SELECT std.student_id, std.roll AS `Roll No`, cls.name AS `class`, sec.name AS `section`, CONCAT(std.name, ' / ', p.name) AS Student, " +
                               "m.mark_obtained AS `Obtain Marks`, tms.marks AS Total, m.COMMENT AS Remarks " +
                               "FROM student AS std " +
                               "JOIN class AS cls ON cls.class_id = std.class_id " +
                               "JOIN section AS sec ON sec.section_id = std.section_id " +
                               "JOIN parent AS p ON p.parent_id = std.parent_id " +
                               "LEFT JOIN mark AS m ON m.`student_id` = std.`student_id` AND m.exam_id = '" + examID + "' AND m.subject_id = '" + subjectID + "' " +
                               "LEFT JOIN `tbl_mark_subject` AS tms ON tms.subject_id = '" + subjectID + "' AND tms.exam_id = '" + examID + "' " +
                               "WHERE std.section_id = '" + section + "' AND std.passout = 0 ORDER BY std.roll;";
                DataTable table = fun.FetchDataTable(query);
                foreach (DataRow dr in table.Rows)
                {
                    DataRow[] existingRows = exportTable.Select($"student_id = '{dr["student_id"]}'");
                    DataRow existingRow = existingRows.Length > 0 ? existingRows[0] : exportTable.NewRow();
                    if (existingRows.Length == 0)
                    {
                        existingRow["student_id"] = dr["student_id"];
                        existingRow["Roll No"] = dr["Roll No"];
                        existingRow["class"] = dr["class"];
                        existingRow["section"] = dr["section"];
                        existingRow["Student"] = dr["Student"];
                        exportTable.Rows.Add(existingRow);
                    }
                    string subjectName = fun.Execute_Scaler_string($"SELECT name FROM subject WHERE subject_id = '{subjectID}'") as string; existingRow[$"Obtain Marks ({subjectName})"] = dr["Obtain Marks"] == DBNull.Value ? (object)null : Convert.ToInt32(dr["Obtain Marks"]);
                    existingRow["Total"] = dr["Total"];
                    existingRow["Remarks"] = dr["Remarks"];
                }
            }

            exportto_excel_grid.DataSource = exportTable;
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
            report.LabSubject.Text = isSingleSubjectMode ? txtSubject.Text : checkedListBoxSubjects.Text;
            report.LabDate.Text = DateTime.Now.ToShortDateString();
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }

        private void GridView1_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (!e.Column.FieldName.StartsWith("Obtained")) return;
            GridView gv = sender as GridView;
            gv.SetRowCellValue(e.RowHandle, gv.Columns[e.Column.FieldName], null);
        }

        private void gridManageMarks_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gridView1.MoveNext();
            }
        }

        private void ManageMarks_Enter(object sender, EventArgs e)
        {
            // loadfunctions();
        }
    }
}