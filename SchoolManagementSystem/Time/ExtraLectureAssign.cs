using DevExpress.LookAndFeel;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraReports.UI;
using SchoolManagementSystem.Time;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolManagementSystem.Class
{
    public partial class ExtraLectureAssign : DevExpress.XtraEditors.XtraUserControl
    {
        private static ExtraLectureAssign _instance;

        public static ExtraLectureAssign instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ExtraLectureAssign();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        int time_duration;

        string Time_Start;
        string Time_End;
        DataRow ActiveTimeTableRow;
        int ActiveSlotIndex;
        String ActiveSlot;
        int ActiveTableId = 0;
        int[] absent_array;

        public string[] DayOfWeekArray = new string[] {"Sunday", "Monday","Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"};
        public ExtraLectureAssign()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            allClass.Clear();
            allClass = fun.GetClassTiming();
            foreach (var allclass in allClass)
            {
                Time_Start = allclass.Name;
                Time_End = allclass.Salary;
            }

            time_duration = Convert.ToInt32(fun.GetSettings("time_duration"));

            txtDateManage.Text = DateTime.Now.ToString("yyyy-MM-dd");
            FillTimeTableNew();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnSave.Enabled = false;
            if (add)
            {
                btnSave.Enabled = true;
            }
            //bool Edit = fun.isAllow("Edit", "extra_lecture");
            //if (Edit)
            //{
            //    //gridView1.OptionsBehavior.Editable = true;
            //    //gridView1.OptionsBehavior.ReadOnly = false;
            //}
            //bool Delete = fun.isAllow("Delete", "extra_lecture");
            //if (Delete)
            //btnDelete.Enabled = true;
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            FillTimeTableNew();
            
        }

        void FillTimeTableNew() {
            
            string txtDate = Convert.ToDateTime(txtDateManage.Text).ToString("yyyy-MM-dd");
            absent_array = fun.GetTeacherAbsentArray(txtDate);
            int dayweek = (int)Convert.ToDateTime(txtDateManage.Text).DayOfWeek;
            string[] DaysArray = new string[1];
            DaysArray[0] = DayOfWeekArray[dayweek];
            DataTable table = new DataTable();

            //Setting table header
            DataColumn[] cols ={
                       new DataColumn("Class",typeof(String)),
                       new DataColumn("class_id",typeof(String)),
                       new DataColumn("Section",typeof(String)),
                       new DataColumn("section_id",typeof(String)),
                       new DataColumn("Days",typeof(String)),
                       new DataColumn("day_id",typeof(String)),
                   };
            table.Columns.AddRange(cols);
            ClassTime objClassTime = fun.GetClassTime();
            DateTime  time_start = DateTime.Today.Add(TimeSpan.Parse(objClassTime.min_time));
            DateTime  time_end = DateTime.Today.Add(TimeSpan.Parse(objClassTime.max_time));
            List<string> slots_array = new List<string>();
            string slotname = "";
            for (; time_start < time_end;)
            {
                slotname = time_start.ToString("HH:mm") + "-" + time_start.AddMinutes(time_duration).ToString("HH:mm");
                slots_array.Add(slotname);
                time_start = time_start.AddMinutes(time_duration);
                DataColumn[]  colSlot = {new DataColumn(slotname)};
                table.Columns.AddRange(colSlot);
            }
            int slots_count = slots_array.Count;
            
            string query = "SELECT class.class_id, class.name AS class, section.section_id,section.name AS section,section.time_start,section.time_end  " +
                            " FROM section " +
                            " INNER JOIN class ON class.class_id = section.class_id" +
                            " WHERE 1 = 1 ORDER BY section.class_id ASC, section.section_id ASC";
            DataTable SectionTable = fun.GetQueryTable(query);
            DataRow myRow;
            foreach (string day in DaysArray)
            {

                foreach (DataRow row in SectionTable.Rows)
                {
                    myRow = table.NewRow();
                    myRow["Class"] = row["class"].ToString();
                    myRow["class_id"] = row["class_id"].ToString();
                    myRow["Section"] = row["section"].ToString();
                    myRow["section_id"] = row["section_id"].ToString();
                    myRow["Days"] = day.Trim().ToString();
                    myRow["day_id"] = day.GetHashCode();
                    for (var i = 0; i < slots_count; i++)
                    {
                        myRow[slots_array[i]] = fun.GetSlotDataAbsent(row["class_id"].ToString(), row["section_id"].ToString(),day.ToString(),i, txtDate);
                    }
                    table.Rows.Add(myRow);                    
                }
            }

            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = table;
            var ColClassId = gridView1.Columns["class_id"];
            ColClassId.Visible = false;
            var ColSectionId = gridView1.Columns["section_id"];
            ColSectionId.Visible = false;
            var ColDayId = gridView1.Columns["day_id"];
            ColDayId.Visible = false;
            /*
            RepositoryItemMemoEdit rimemo = new RepositoryItemMemoEdit();
            gridView1.Columns[4].ColumnEdit = rimemo;
            gridView1.Columns[5].ColumnEdit = rimemo;
            gridView1.Columns[6].ColumnEdit = rimemo;
            gridView1.Columns[7].ColumnEdit = rimemo;
            gridView1.Columns[8].ColumnEdit = rimemo;
            gridView1.Columns[9].ColumnEdit = rimemo;
            gridView1.Columns[10].ColumnEdit = rimemo;
            gridView1.Columns[11].ColumnEdit = rimemo;
            */
            gridView1.OptionsView.RowAutoHeight = true;
            gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.RowHeight = 35;
        }
        private void btnClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            layoutControl1.Visible = false;
        }
        int tableCount = 0;
   
        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            string val = e.CellValue.ToString();
            if (val.Contains(":")) //Absent
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.White;
            }
            else if(val.Contains("<")) //Assign
            {
                e.Appearance.BackColor = Color.Blue;
                e.Appearance.ForeColor = Color.White;
            } 
            else if (val.Contains(">"))
            {
                e.Appearance.BackColor = Color.Green;
                e.Appearance.ForeColor = Color.White;
            }
        }

        void empty()
        {
            txtClass.Text = "";
            txtSection.Text = "";
            txtDay.Text = "";
            txtRoom.Text = "0";
            GridSubject.Properties.DataSource = new DataTable();
            GridTeacher.Properties.DataSource = new DataTable();
            GridTeacherAssign.Properties.DataSource = new DataTable();
        }

        
        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            GridView View = sender as GridView;
            string txtDate = Convert.ToDateTime(txtDateManage.Text).ToString("yyyy-MM-dd");

            int couunt = 0;
            empty();
            var hitInfo = gridView1.CalcHitInfo(e.Location);
            if (hitInfo.InRowCell)
            {
                int rowHandle = hitInfo.RowHandle;

                ActiveSlotIndex = hitInfo.Column.AbsoluteIndex - 6; //first 6 element are not slots
                ActiveSlot = hitInfo.Column.Caption;
                var s = DoRowDoubleClick(gridView1, e.Location);
                layoutControl1.Visible = true;
                layoutControlItem12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                ActiveTimeTableRow = s.Item1;
                object Val = s.Item2;
                txtClass.Text = ActiveTimeTableRow[0].ToString();
                txtSection.Text = ActiveTimeTableRow[2].ToString();
                txtDay.Text = ActiveTimeTableRow[4].ToString();

                DataTable SubjectTable = fun.GetAllSubject_by_section_dt(ActiveTimeTableRow["section_id"].ToString());
                GridSubject.Properties.DataSource = SubjectTable;
                GridSubject.Properties.DisplayMember = "name";
                GridSubject.Properties.ValueMember = "subject_id";

                string check_query = "SELECT * FROM time_table WHERE class_id = '" + ActiveTimeTableRow[1] + "' AND section_id = '" + ActiveTimeTableRow[3] + "' AND `day` = '" + ActiveTimeTableRow[4] + "' AND slot_id = '" + ActiveSlotIndex + "'";
                DataTable CheckData = fun.GetQueryTable(check_query);
                if (CheckData.Rows.Count > 0) //update query will be done
                {
                    txtRoom.Text = CheckData.Rows[0]["room"].ToString();
                    GridSubject.EditValue = CheckData.Rows[0]["subject_id"].ToString();
                    GridTeacher.EditValue = CheckData.Rows[0]["teacher_id"].ToString();
                    ActiveTableId = Convert.ToInt32(CheckData.Rows[0]["id"]);

                    //Assign teacher
                    string assignSQL = "SELECT teacher_id, `name`,subject_code FROM teacher "+
                                        " WHERE teacher_id NOT IN("+
                                        " SELECT teacher_id FROM time_table WHERE slot_id = {0} AND `day` = '{1}' UNION SELECT teacher_id FROM extra_lecture WHERE slate_id = {0} AND `date` = '{2}' "+
                                        " )ORDER BY subject_code = '{3}' DESC";
                    assignSQL = String.Format(assignSQL,ActiveSlotIndex, ActiveTimeTableRow[4], txtDate, GridSubject.Text.ToString());
                    GridTeacherAssign.Properties.DataSource = fun.GetQueryTable(assignSQL);
                    GridTeacherAssign.Properties.DisplayMember = "name";
                    GridTeacherAssign.Properties.ValueMember = "teacher_id";
                } 
            }
        }
        private void GridSubject_EditValueChanged(object sender, EventArgs e)
        {
            string subject_code = GridSubject.Text.ToString();
            string querySection = "SELECT teacher_id, name,subject_code FROM teacher ORDER BY subject_code = '" + subject_code + "' DESC";
            DataTable TeacherTable = fun.GetQueryTable(querySection);
            GridTeacher.Properties.DataSource = TeacherTable;
            GridTeacher.Properties.DisplayMember = "name";
            GridTeacher.Properties.ValueMember = "teacher_id";

            
        }
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to delete?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK && ActiveTableId > 0)
            {
                string query = "DELETE FROM time_table WHERE id =  " + ActiveTableId;
                fun.ExecuteQuery(query);
                empty();
                //FillTimeTableNew();
            }
        }

        public static Tuple<DataRow, object> DoRowDoubleClick(GridView view, Point pt)
        {
            GridHitInfo info = view.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                string colCaption = info.Column == null ? "N/A" : info.Column.GetCaption();
                object val = view.GetRowCellValue(info.RowHandle, info.Column);
                //  if (val != null)
                //     MessageBox.Show(string.Format("DoubleClick on row: {0}, column: {1}, value: {2}", info.RowHandle, colCaption, val));
                DataRow row = view.GetDataRow(info.RowHandle);
                return Tuple.Create(row, val);

            }
            return null;
        }

     
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GridTeacherAssign.EditValue) > 0) {
                MessageBox.Show("Select Assign Teacher First");
                return;
            }
            string subject_code = GridSubject.Text.ToString();
            string subject_id = GridSubject.EditValue.ToString();
            string teacher_id = GridTeacher.EditValue.ToString();
            string class_id = ActiveTimeTableRow[1].ToString();
            string section_id = ActiveTimeTableRow[3].ToString();
            string day = ActiveTimeTableRow[4].ToString();
            string myroom = txtRoom.Text;
            int slot_id = ActiveSlotIndex;
            string txtDate = Convert.ToDateTime(txtDateManage.Text).ToString("yyyy-MM-dd");
            string check_query = "SELECT * FROM extra_lecture WHERE slate_id = {0} AND `date` = '{1}' AND class_id = '{2}' AND section_id = '{3}' ";
            check_query = String.Format(check_query,slot_id, txtDate, class_id,section_id);
            string query = " SET slate = '"+ ActiveSlot + "', day = '"+day+"', `date` = '"+txtDate+"',class_id = '" + class_id + "', section_id = '" + section_id + "', slate_id = '" + slot_id + "', teacher_id = '" + teacher_id + "',extra_teacher_id='"+GridTeacherAssign.EditValue+"', subject_code = '" + subject_code + "', subject_id = '"+ subject_id + "'";
            DataTable CheckData = fun.GetQueryTable(check_query);
            if (CheckData.Rows.Count > 0) //update query will be done
            {
                query = "UPDATE extra_lecture " + query + " WHERE id=" + CheckData.Rows[0]["id"];
            } 
            else
            {
                query = "INSERT INTO extra_lecture " + query;
            }
            
            fun.ExecuteQuery(query);
            empty();
            MessageBox.Show("Slot Assign Successfully!");

            //FillTimeTableNew();
        }

        private void btnReportPrint_Click(object sender, EventArgs e)
        {
            gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.ColumnPanelRowHeight = 30;

            for (int i = 0; i < 7; i++)
            {
                GridColumn Column = gridView1.Columns[i + 1];
                Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }

            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            TimeTableReport report = new TimeTableReport();
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.GridControl = gridControl1;
            report.GridControl.Width = 100;
            report.LabAddress.Text = fun.GetSettings("address");
            report.LabTel.Text = fun.GetSettings("phone");
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);

        }

        private void btnFillSlots_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM time_table_slot";
            int slot_id = 0;
            fun.ExecuteQuery(query);
            ClassTime objClassTime = fun.GetClassTime();
            DateTime time_start = DateTime.Today.Add(TimeSpan.Parse(objClassTime.min_time));
            DateTime time_end = DateTime.Today.Add(TimeSpan.Parse(objClassTime.max_time));
            string slotname = "";
            for (; time_start < time_end;)
            {
                slotname = time_start.ToString("HH:mm") + "-" + time_start.AddMinutes(time_duration).ToString("HH:mm");
                query = "INSERT INTO time_table_slot(slot_id,slot) VALUES('" + slot_id+"','"+slotname+"')";
                fun.ExecuteQuery(query);
                time_start = time_start.AddMinutes(time_duration);
                slot_id++;
            }
            MessageBox.Show(slot_id+" Slots Successfully created");

        }
    }

}



