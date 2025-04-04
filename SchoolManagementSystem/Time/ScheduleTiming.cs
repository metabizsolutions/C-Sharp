using Accounting.Automatic_Time_Table;
using DevExpress.LookAndFeel;
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
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Class
{
    public partial class ScheduleTiming : DevExpress.XtraEditors.XtraUserControl
    {
        private static ScheduleTiming _instance;

        public static ScheduleTiming instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ScheduleTiming();
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
        int ActiveTableId = 0;
        public enum DayOfWeek
        {
            Monday = 1,
            Tuesday = 2,
            Wednesday = 3,
            Thursday = 4,
            Friday = 5,
            Saturday = 6,
        }
        public ScheduleTiming()
        {
            InitializeComponent();
            InitializeTimePickerState();
            loadfunctions();
        }
        private void ScheduleTiming_Load(object sender, EventArgs e)
        {
            InitializeTimePickerState(); // Call again to ensure it's set correctly when the form loads
        }
        private void InitializeTimePickerState()
        {
            // Set initial state based on checkboxes
            timeSAS.Enabled = chkAssemblyTime.Checked;
            timeEA.Enabled = chkAssemblyTime.Checked;
            timeSB.Enabled = chkBreakTime.Checked;
            timeEB.Enabled = chkBreakTime.Checked;

            // If both checkboxes are unchecked, disable all time pickers
            if (!chkAssemblyTime.Checked && !chkBreakTime.Checked)
            {
                timeSAS.Enabled = false;
                timeEA.Enabled = false;
                timeSB.Enabled = false;
                timeEB.Enabled = false;
            }
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
            //Populate_Filter_Grid();
        }


        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnSave.Enabled = false;
            if (add)
                btnSave.Enabled = true;
            btnFillSlots.Enabled = false;
            gridView1.OptionsBehavior.Editable = false;
            if (Edit)
            {
                btnFillSlots.Enabled = true;
                gridView1.OptionsBehavior.Editable = true;
            }
            btnDelete.Enabled = false;
            if (Delete)
                btnDelete.Enabled = true;
        }
        public void Populate_Filter_Grid()
        {
            string query = "SELECT section.section_id, CONCAT(class.name , '-', section.name) AS section " +
                                    " FROM section " +
                                    " INNER JOIN class ON section.class_id = class.class_id " +
                                    " ORDER BY class.name ASC, section.name ASC ";
            DataTable table = fun.GetQueryTable(query);
            GridSection.Properties.DataSource = table;
            GridSection.Properties.DisplayMember = "section";
            GridSection.Properties.ValueMember = "section_id";
            GridSection.CheckAll();

            DataTable DayTable = new DataTable();
            DayTable.Columns.Add("day");
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                DataRow row = DayTable.NewRow();
                row["day"] = day;
                DayTable.Rows.Add(row);
            }

            GridDays.Properties.DataSource = DayTable;
            GridDays.Properties.DisplayMember = "day";
            GridDays.Properties.ValueMember = "day";
            GridDays.CheckAll();

        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            fun.ShowGridPreview(gridControl1);

        }

        void FillTimeTableNew()
        {
            string selectedSection = GridSection.EditValue.ToString();
            string SelectedDays = GridDays.EditValue.ToString();
            string[] DaysArray = SelectedDays.Split(',');
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
            DateTime time_start = DateTime.Today.Add(TimeSpan.Parse(objClassTime.min_time));
            DateTime time_end = DateTime.Today.Add(TimeSpan.Parse(objClassTime.max_time));
            List<string> slots_array = new List<string>();
            string slotname = "";
            for (; time_start < time_end;)
            {
                slotname = time_start.ToString("HH:mm") + "-" + time_start.AddMinutes(time_duration).ToString("HH:mm");
                slots_array.Add(slotname);
                time_start = time_start.AddMinutes(time_duration);
                DataColumn[] colSlot = { new DataColumn(slotname) };
                table.Columns.AddRange(colSlot);
            }
            int slots_count = slots_array.Count;

            string query = "SELECT class.class_id, class.name AS class, section.section_id,section.name AS section,section.time_start,section.time_end  " +
                            " FROM section " +
                            " INNER JOIN class ON class.class_id = section.class_id" +
                            " WHERE section.section_id IN(" + selectedSection + ") " +
                            " ORDER BY section.class_id ASC, section.section_id ASC";
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
                        myRow[slots_array[i]] = fun.GetSlotData(row["class_id"].ToString(), row["section_id"].ToString(), day.ToString(), i);
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
            int countCol = 0;
            if (tableCount == 1)
            {
                if (e.Column.FieldName == "Days")
                    return;
                var time = Time_Start;
                var tss = TimeSpan.Parse(time);
                var timeS = DateTime.Today.Add(tss);
                time = Time_End;
                tss = TimeSpan.Parse(time);
                var timeE = DateTime.Today.Add(tss);
                // timeE = timeE.AddHours(12);
                countCol = 0;

                for (; timeS < timeE;)
                {
                    if (countCol == 4)
                    {
                        string field = timeS.ToString("hh:mm tt") + "-" + timeS.AddMinutes(time_duration - 15).ToString("hh:mm tt");
                        string res = View.GetRowCellDisplayText(e.RowHandle, View.Columns[field]);
                        var date = "";

                        if (res.Contains('_'))
                        {
                            var val = res.Split('_')[1];
                            if (res.Split('_')[1] != "\n\r")
                                date = res.Split('_')[1].ToString();
                        }
                        string res2 = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Days"]);


                        if (e.Column.FieldName == field)//
                        {
                            if (date == "\n\rAbsent" && res2 == DateTime.Now.DayOfWeek.ToString())
                            {
                                e.Appearance.ForeColor = Color.Red;
                            }

                        }
                        timeS = timeS.AddMinutes(time_duration - 15);
                    }
                    else if (countCol > 4)
                    {
                        string field = timeS.ToString("hh:mm tt") + "-" + timeS.AddMinutes(time_duration - 5).ToString("hh:mm tt");
                        string res = View.GetRowCellDisplayText(e.RowHandle, View.Columns[field]);
                        var date = "";

                        if (res.Contains('_'))
                        {
                            var val = res.Split('_')[1];
                            if (res.Split('_')[1] != "\n\r")
                                date = res.Split('_')[1].ToString();
                        }
                        string res2 = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Days"]);


                        if (e.Column.FieldName == field)//
                        {
                            if (date == "\n\rAbsent" && res2 == DateTime.Now.DayOfWeek.ToString())
                            {
                                e.Appearance.ForeColor = Color.Red;
                            }

                        }
                        timeS = timeS.AddMinutes(time_duration - 5);
                    }
                    else
                    {
                        string field = timeS.ToString("hh:mm tt") + "-" + timeS.AddMinutes(time_duration).ToString("hh:mm tt");
                        string res = View.GetRowCellDisplayText(e.RowHandle, View.Columns[field]);
                        var date = "";

                        if (res.Contains('_'))
                        {
                            var val = res.Split('_')[1];
                            if (res.Split('_')[1] != "\n\r")
                                date = res.Split('_')[1].ToString();
                        }
                        string res2 = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Days"]);


                        if (e.Column.FieldName == field)//
                        {
                            if (date == "\n\rAbsent" && res2 == DateTime.Now.DayOfWeek.ToString())
                            {
                                e.Appearance.ForeColor = Color.Red;
                            }

                        }
                        timeS = timeS.AddMinutes(time_duration);
                    }
                    countCol++;


                }
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
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {

        }
        string col = "";
        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            GridView View = sender as GridView;
            DataRow dr = View.GetFocusedDataRow();
            col = View.FocusedColumn.ToString();
            if (dr["section_id"].ToString() == "")
            {
                //MessageBox.Show("Please Select row that have class and section Names", "Time Table Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int couunt = 0;
            empty();
            var hitInfo = gridView1.CalcHitInfo(e.Location);
            if (hitInfo.InRowCell)
            {
                int rowHandle = hitInfo.RowHandle;
                ActiveSlotIndex = hitInfo.Column.AbsoluteIndex - 6; //first 6 element are not slots
                var s = DoRowDoubleClick(gridView1, e.Location);
                layoutControl1.Visible = true;
                layoutControlItem12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                ActiveTimeTableRow = s.Item1;
                object Val = s.Item2;
                txtClass.Text = ActiveTimeTableRow["Class"].ToString();
                txtSection.Text = ActiveTimeTableRow["Section"].ToString();
                txtDay.Text = ActiveTimeTableRow["Days"].ToString();

                DataTable SubjectTable = fun.GetAllSubject_by_section_dt(ActiveTimeTableRow["section_id"].ToString());
                GridSubject.Properties.DataSource = SubjectTable;
                GridSubject.Properties.DisplayMember = "name";
                GridSubject.Properties.ValueMember = "subject_id";

                string subject_code = GridSubject.Text.ToString();
                DataTable TeacherTable = fun.GetAllTeacher_dt(" ORDER BY subject_code = '" + subject_code + "' DESC");
                GridTeacher.Properties.DataSource = TeacherTable;
                GridTeacher.Properties.DisplayMember = "name";
                GridTeacher.Properties.ValueMember = "teacher_id";

                string check_query = "SELECT * FROM time_table  as tt "+
                                        " join time_table_slot as tts on tts.slot_id = tt.slot_id " +
                                        " WHERE class_id = '" + ActiveTimeTableRow["class_id"] + "' AND section_id = '" + ActiveTimeTableRow["section_id"] + "' AND `day` = '" + ActiveTimeTableRow["Days"] + "' AND tts.slot= '" + col + "'";
                DataTable CheckData = fun.GetQueryTable(check_query);
                if (CheckData.Rows.Count > 0) //update query will be done
                {
                    txtRoom.Text = CheckData.Rows[0]["room"].ToString();
                    GridSubject.EditValue = CheckData.Rows[0]["subject_id"].ToString();
                    GridTeacher.EditValue = CheckData.Rows[0]["teacher_id"].ToString();
                    ActiveTableId = Convert.ToInt32(CheckData.Rows[0]["id"]);
                }
            }
        }
        private void GridSubject_EditValueChanged(object sender, EventArgs e)
        {
            string subject_code = GridSubject.Text.ToString();
            DataTable TeacherTable = fun.GetAllTeacher_dt(" ORDER BY subject_code = '" + subject_code + "' DESC");
            GridTeacher.Properties.DataSource = TeacherTable;
            GridTeacher.Properties.DisplayMember = "name";
            GridTeacher.Properties.ValueMember = "teacher_id";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to delete?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK && ActiveTableId > 0)
            {
                string query = "DELETE tt FROM time_table as tt " +
                               " join time_table_slot as tts on tts.slot_id = tt.slot_id " +
                               " where class_id = '" + ActiveTimeTableRow["class_id"] + "' AND section_id = '" + ActiveTimeTableRow["section_id"] + "' AND `day` = '" + ActiveTimeTableRow["Days"] + "' AND tts.slot= '" + col + "'";
                fun.ExecuteQuery(query);
                empty();
            }
        }
        static string colCaption = "";
        static int rownum = 0;
        public static Tuple<DataRow, object> DoRowDoubleClick(GridView view, Point pt)
        {
            GridHitInfo info = view.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                colCaption = info.Column == null ? "N/A" : info.Column.GetCaption();
                object val = view.GetRowCellValue(info.RowHandle, info.Column);
                rownum = info.RowHandle;
                //  if (val != null)
                //     MessageBox.Show(string.Format("DoubleClick on row: {0}, column: {1}, value: {2}", info.RowHandle, colCaption, val));
                DataRow row = view.GetDataRow(info.RowHandle);
                
                    return Tuple.Create(row, val);

            }
            return null;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if(GridSubject.EditValue == null)
            {
                MessageBox.Show("Please Select Subject from List","Subject List",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            if(GridTeacher.EditValue == null)
            {
                MessageBox.Show("Please Select Teacher from List", "Teacher List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string subject_code = GridSubject.Text.ToString();
            string subject_id = GridSubject.EditValue.ToString();
            string teacher_id = GridTeacher.EditValue.ToString();
            string class_id = ActiveTimeTableRow["class_id"].ToString();
            string section_id = ActiveTimeTableRow["section_id"].ToString();
            string day = ActiveTimeTableRow["Days"].ToString();
            string myroom = txtRoom.Text;
            object slotid = fun.Execute_Scaler_string("SELECT `slot_id` FROM `time_table_slot` WHERE `slot` = '" + colCaption + "'");
            ActiveSlotIndex = Convert.ToInt32(slotid);
            int slot_id = ActiveSlotIndex;
            string check_query = "SELECT * FROM time_table WHERE class_id = '" + class_id + "' AND section_id = '" + section_id + "' AND `day` = '" + day + "' AND slot_id = '" + slot_id + "'";
            string query = " SET class_id = '" + class_id + "', section_id = '" + section_id + "', `day` = '" + day + "',room='" + myroom + "', slot_id = '" + slot_id + "', teacher_id = '" + teacher_id + "', subject_code = '" + subject_code + "', subject_id = '" + subject_id + "'";
            DataTable CheckData = fun.GetQueryTable(check_query);
            if (CheckData.Rows.Count > 0) //update query will be done
            {
                query = "UPDATE time_table " + query + " WHERE id=" + CheckData.Rows[0]["id"];
            }
            else //insert query execute
            {
                query = "INSERT INTO time_table " + query;
            }
            fun.ExecuteQuery(query);

            gridView1.SetRowCellValue(rownum, colCaption, GridSubject.Text + " ( " + GridTeacher.Text + " ) ");
            empty();
            MessageBox.Show("Slot Added Successfully!");
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
            //string query = "DELETE FROM time_table_slot";
            //int slot_id = 0;
            //fun.ExecuteQuery(query);
            //ClassTime objClassTime = fun.GetClassTime();
            //DateTime time_start = DateTime.Today.Add(TimeSpan.Parse(objClassTime.min_time));
            //DateTime time_end = DateTime.Today.Add(TimeSpan.Parse(objClassTime.max_time));
            //string slotname = "";
            //for (; time_start < time_end;)
            //{
            //    slotname = time_start.ToString("HH:mm") + "-" + time_start.AddMinutes(time_duration).ToString("HH:mm");
            //    query = "INSERT INTO time_table_slot(slot_id,slot) VALUES('" + slot_id+"','"+slotname+"')";
            //    fun.ExecuteQuery(query);
            //    time_start = time_start.AddMinutes(time_duration);
            //    slot_id++;
            //}
            //MessageBox.Show(slot_id+" Slots Successfully created");

        }
        DataTable dtslt = new DataTable();
        public void timetableList()
        {
            //ifTeacherChange();
            try
            {
                gridControl1.DataSource = null;
                string SelectedDays = "Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday";
                string[] DaysArray = SelectedDays.Split(',');
                string query = "SELECT * FROM time_table_slot where not Slot_Start_Time = ''";

                dtslt = fun.FetchDataTable(query);

                query = "SELECT cl.class_id,cl.name as Class,sec.section_id,sec.name as section FROM section as sec " +
                                " join class as cl on cl.class_id = sec.class_id " +
                                " group by sec.section_id ";
                DataTable dt = fun.FetchDataTable(query);
                int count = dt.Rows.Count;
                DataRow myRow;
                myRow = dt.NewRow();
                dt.Columns.Add("Days");
                for (int i = 0; i < dtslt.Rows.Count; i++)
                {
                    string slottime = dtslt.Rows[i]["Slot_Start_Time"].ToString() + "-" + dtslt.Rows[i]["Slot_End_Time"].ToString();
                    dt.Columns.Add(slottime);
                    string slotname = dtslt.Rows[i]["slot_type"].ToString();
                    myRow[slottime] = slotname;
                }
                dt.Rows.InsertAt(myRow, 0);
                foreach (string day in DaysArray)
                {
                    for (int i = 1; i < count + 1; i++)
                    {
                        if (dt.Rows[i]["Days"].ToString() == "" && dt.Rows[i]["section"].ToString() != "")
                        {
                            dt.Rows[i]["Days"] = day.Trim().ToString();
                            for (int j = 0; j < dtslt.Rows.Count; j++)
                            {
                                string slotname = dtslt.Rows[j]["Slot_Start_Time"].ToString() + "-" + dtslt.Rows[j]["Slot_End_Time"].ToString();
                                query = "select * from time_table where class_id = " + dt.Rows[i]["class_id"].ToString() + " and section_id = " + dt.Rows[i]["section_id"].ToString() + ""
                                        + " and day = '" + day + "' and slot_id = " + dtslt.Rows[j]["slot_id"].ToString() + "";
                                DataTable table = new DataTable();
                                table = fun.FetchDataTable(query);
                                for (int k = 0; k < table.Rows.Count; k++)
                                {
                                    int techid = Convert.ToInt32(table.Rows[k]["teacher_id"]);
                                    object teachername = "";
                                    if (techid > 0)
                                        teachername = fun.Execute_Scaler_string("select name from teacher where teacher_id = " + table.Rows[k]["teacher_id"].ToString() + "");
                                    dt.Rows[i][slotname] = table.Rows[k]["subject_code"].ToString() + " ( " + teachername + " )";
                                }
                            }
                        }
                        else
                        {
                            myRow = dt.NewRow();

                            myRow["Class"] = dt.Rows[i]["class"].ToString();
                            myRow["Section"] = dt.Rows[i]["section"].ToString();
                            myRow["class_id"] = dt.Rows[i]["class_id"].ToString();
                            myRow["section_id"] = dt.Rows[i]["section_id"].ToString();
                            myRow["Days"] = day.Trim().ToString();


                            for (int j = 0; j < dtslt.Rows.Count; j++)
                            {
                                string slotname = dtslt.Rows[j]["Slot_Start_Time"].ToString() + "-" + dtslt.Rows[j]["Slot_End_Time"].ToString();
                                query = "select * from time_table where class_id = " + dt.Rows[i]["class_id"].ToString() + " and section_id = " + dt.Rows[i]["section_id"].ToString() + ""
                                        + " and day = '" + day + "' and slot_id = " + dtslt.Rows[j]["slot_id"].ToString() + "";
                                DataTable table = new DataTable();
                                table = fun.FetchDataTable(query);
                                for (int k = 0; k < table.Rows.Count; k++)
                                {
                                    int techid = Convert.ToInt32(table.Rows[k]["teacher_id"]);
                                    object teachername = "";
                                    if (techid > 0)
                                        teachername = fun.Execute_Scaler_string("select name from teacher where teacher_id = " + table.Rows[k]["teacher_id"].ToString() + "");
                                    myRow[slotname] = table.Rows[k]["subject_code"].ToString() + " ( " + teachername + " )";
                                }
                            }
                            dt.Rows.Add(myRow);
                        }
                    }
                }
                gridControl1.BeginUpdate();

                try
                {

                    gridView1.Columns.Clear();

                    gridControl1.DataSource = null;

                    gridControl1.DataSource = dt;

                }

                finally
                {
                    gridControl1.EndUpdate();

                }
                gridView1.Columns["class_id"].Visible = false;
                gridView1.Columns["section_id"].Visible = false;
                gridView1.Columns["section"].Group();
                gridView1.ExpandAllGroups();
            }
            catch (Exception e)
            {
                MessageBox.Show("Execute Query: " + e.ToString());
            }
        }
        private void btnTimeTableSlots_Click(object sender, EventArgs e)
        {
            if (txtLectures.Text == "")
            {
                MessageBox.Show("Please provide total lectures", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtlectime.Text == "")
            {
                MessageBox.Show("Please provide one lecture total time", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fun.loaderform(() =>
            {
                string query = "TRUNCATE time_table_slot";
                fun.Execute_Query(query);

                // **Only insert Assembly slot if checkbox is checked**
                if (chkAssemblyTime.Checked)
                {
                    query = "INSERT INTO time_table_slot(slot_type ,slot, Slot_Start_Time, Slot_End_Time) " +
                            "VALUES ('Assembly','" + timeSAS.Value.ToString("hh:mm tt") + "-" + timeEA.Value.ToString("hh:mm tt") + "'," +
                            "'" + timeSAS.Value.ToString("hh:mm tt") + "','" + timeEA.Value.ToString("hh:mm tt") + "')";
                    fun.Execute_Query(query);
                }

                int lecturetime = Convert.ToInt32(txtlectime.Text);
                DateTime LectureStarttime = timeSAS.Value;
                DateTime Lectureendtime = LectureStarttime.AddMinutes(lecturetime);

                for (int i = 0; i < Convert.ToInt32(txtLectures.Text); i++)
                {
                    if (i == 0)
                    {
                        LectureStarttime = timeEA.Value;
                        Lectureendtime = timeEA.Value.AddMinutes(lecturetime);
                    }
                    else
                    {
                        LectureStarttime = Lectureendtime;
                        Lectureendtime = LectureStarttime.AddMinutes(lecturetime);

                        if (LectureStarttime.ToString("hh:mm tt") == timeSB.Value.ToString("hh:mm tt"))
                        {
                            // **Only insert Break slot if checkbox is checked**
                            if (chkBreakTime.Checked)
                            {
                                query = "INSERT INTO time_table_slot(slot_type,slot, Slot_Start_Time, Slot_End_Time) " +
                                        "VALUES ('Break','" + timeSB.Value.ToString("hh:mm tt") + "-" + timeEB.Value.ToString("hh:mm tt") + "'," +
                                        "'" + timeSB.Value.ToString("hh:mm tt") + "','" + timeEB.Value.ToString("hh:mm tt") + "')";
                                fun.Execute_Query(query);
                            }

                            LectureStarttime = timeEB.Value;
                            Lectureendtime = LectureStarttime.AddMinutes(lecturetime);
                        }
                    }
                    query = "INSERT INTO time_table_slot(slot_type,slot, Slot_Start_Time, Slot_End_Time) " +
                            "VALUES ('Slot(" + (i + 1).ToString() + ")','" + LectureStarttime.ToString("hh:mm tt") + "-" + Lectureendtime.ToString("hh:mm tt") + "'," +
                            "'" + LectureStarttime.ToString("hh:mm tt") + "','" + Lectureendtime.ToString("hh:mm tt") + "')";
                    fun.Execute_Query(query);
                }

                timetableList(); // Refresh timetable
            });
        }


        private void btnEditSlot_Click(object sender, EventArgs e)
        {
            EditSlots slots = new EditSlots(this);
            slots.Show();
        }

        private void btn_load_tt_Click(object sender, EventArgs e)
        {
            fun.loaderform(timetableList);
        }
        private void chkAssemblyTime_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = chkAssemblyTime.Checked;

            if (assemblyTimeLabel != null)
                assemblyTimeLabel.Visible = isChecked;

            if (assemblyTimePicker != null)
                assemblyTimePicker.Visible = isChecked;

            // Disable/Enable time pickers based on checkbox state
            timeSAS.Enabled = isChecked;
            timeEA.Enabled = isChecked;

            DisableTimePickersIfBothUnchecked();
        }

        private void chkBreakTime_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = chkBreakTime.Checked;

            if (breakTimeLabel != null)
                breakTimeLabel.Visible = isChecked;

            if (breakTimePicker != null)
                breakTimePicker.Visible = isChecked;

            // Disable/Enable time pickers based on checkbox state
            timeSB.Enabled = isChecked;
            timeEB.Enabled = isChecked;

            DisableTimePickersIfBothUnchecked();
        }

        private void DisableTimePickersIfBothUnchecked()
        {
            // If both checkboxes are unchecked, disable all relevant time pickers
            if (!chkAssemblyTime.Checked && !chkBreakTime.Checked)
            {
                timeSAS.Enabled = false;
                timeEA.Enabled = false;
                timeSB.Enabled = false;
                timeEB.Enabled = false;
            }
        }



    }

}



