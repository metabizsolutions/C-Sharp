using DevExpress.LookAndFeel;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using MySql.Data.MySqlClient;
using SchoolManagementSystem.Fees;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Time
{
    public partial class FloorSheet : DevExpress.XtraEditors.XtraUserControl
    {
        //  ObservableCollection<AllClass> allClass;
        ObservableCollection<TeachingStaff> teachingStaff;
        ObservableCollection<FloorSheetItems> list;
        ObservableCollection<AllClass> allClass;
        CommonFunctions fun = new CommonFunctions();
        private static FloorSheet _instance;

        public static FloorSheet instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FloorSheet();
                return _instance;
            }
        }
        public FloorSheet()
        {
            InitializeComponent();
            loadfunctions();
        }
        DataTable slots = new DataTable();
        public void loadfunctions()
        {
            string query = "select * from time_table_slot";
            slots = fun.FetchDataTable(query);
            var qry = "Staff_Type in('Administration','Teaching','Visiting')";

            FillGridFloorSheet(DateTime.Now.ToString());
            query = "SELECT teacher_id,name,subject_code" +
                " FROM teacher where passout = 0 order by subject_code;";
            DataTable teachingStaff = fun.FetchDataTable(query);
            txtTeacher.Properties.DataSource = teachingStaff;
            txtTeacher.Properties.DisplayMember = "name";
            txtTeacher.Properties.ValueMember = "teacher_id";

            txtClass.Properties.Items.Clear();
            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";
        }
        private void TeacherProfile_Enter(object sender, EventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(slots.Rows.Count <= 0)
            {
                MessageBox.Show("Please Create Time Table slots before creating floor sheet","Error",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            int alreadyExit = 0;
            if (txtDateManage.Text != "")
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true);
                SplashScreenManager.Default.SetWaitFormCaption("Processing data...");
                string s = Convert.ToDateTime(txtDateManage.Text).ToString("yyyy-MM-dd");
                var Session = Main_FD.SelectedSession;

                var AllClass = fun.GetAllClassisSession(Session);
                MySqlConnection con = new MySqlConnection(Login.constring);
                
                foreach (var itemC in AllClass)
                {
                    var AllSection = fun.GetAllSectionisClass(itemC.Name);
                    foreach (var itemS in AllSection)
                    {
                        var classID = fun.GetClassIDisSession(itemC.Name, Session);
                        var sectionID = fun.GetSectionIDisClass(itemS.Name, classID);
                        con.Open();

                        string query = "SELECT * FROM floor_sheet WHERE class_id='" + classID + "'and section_id = '" + sectionID + "' and date='" + s + "';";

                        MySqlCommand cmd1 = new MySqlCommand(query, con);
                        MySqlDataReader reader1 = cmd1.ExecuteReader();
                        if (reader1.HasRows)
                        {
                            alreadyExit = 1;
                        }
                        con.Close();
                        if (alreadyExit == 0)
                        {
                            for (int i = 0; i < slots.Rows.Count; i++)
                            {
                                if (slots.Rows[i]["slot_type"].ToString() != "Assembly" && slots.Rows[i]["slot_type"].ToString() != "Break")
                                {
                                    query = "SELECT teacher_id FROM `time_table` WHERE `class_id` ='" + classID + "' and `section_id` ='" + sectionID + "' and `slot_id`= '" + slots.Rows[i]["slot_id"].ToString() + "'";
                                    object teacher_id = fun.Execute_Scaler_string(query);
                                    if (teacher_id != null)
                                    {

                                    }
                                    con.Open();
                                    try
                                    {
                                        MySqlCommand cmd = new MySqlCommand("INSERT into floor_sheet(class_id,section_id,teacher_id,late_time,slate,date) VALUES('" + classID + "','" + sectionID + "','"+teacher_id+"','0','" + slots.Rows[i]["slot_id"].ToString() + "','" + s + "');", con);
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch (MySqlException ex)
                                    {
                                        SystemSounds.Hand.Play();
                                        MessageBox.Show(ex.Message, "Error");
                                        return;
                                    }
                                    con.Close();
                                }
                            }
                        }
                        alreadyExit = 0;
                    }
                }
                FillGridFloorSheet(txtDateManage.Text);
                SplashScreenManager.CloseForm();
            }
            else
            {
                MessageBox.Show("Select Date");
            }
        }
        RepositoryItemSearchLookUpEdit riSearch;
        void FillGridFloorSheet(string date)
        {
            var query = "";
            query = "where date='" + Convert.ToDateTime(date).ToString("yyyy-MM-dd") + "'";
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdP = new MySqlCommand("SELECT floor_id as ID, class.name as Class,section.name as Section, slate as Slate,teacher.teacher_id as Teacher,late_time as `Late Time` , date as Date FROM floor_sheet join class on class.class_id=floor_sheet.class_id join section on section.section_id=floor_sheet.section_id left join teacher on teacher.teacher_id=floor_sheet.teacher_id " + query + "", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();
            var col = gridView1.Columns["ID"];
            col.OptionsColumn.ReadOnly = true;
            col.Visible = false;
            con.Close();
            teachingStaff = new ObservableCollection<TeachingStaff>();
            //var qry = " Staff_Type in('Administration','Teaching','Visiting') ";

            riSearch = new RepositoryItemSearchLookUpEdit();
            teachingStaff = fun.GetAllTeachingStaffWId(" 1 = 1 ");

            riSearch.DataSource = teachingStaff;
            riSearch.ValueMember = "ID";
            riSearch.DisplayMember = "Name";

            gridView1.Columns["Teacher"].ColumnEdit = riSearch;
            gridView1.OptionsBehavior.ReadOnly = false;

            riSearch = new RepositoryItemSearchLookUpEdit();
            riSearch.DataSource = slots;
            riSearch.ValueMember = "slot_id";
            riSearch.DisplayMember = "slot";
            gridView1.Columns["Slate"].ColumnEdit = riSearch;
            gridView1.Columns["Slate"].OptionsColumn.ReadOnly = true;

            gridView1.Columns["Class"].GroupIndex = 0;
            gridView1.Columns["Section"].GroupIndex = 1;

        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE floor_sheet set teacher_id='" + row[4].ToString() + "',late_time='" + row[5] + "' WHERE floor_id='" + row[0] + "';", con);
            cmd.ExecuteNonQuery();
            con.Close();
            // FillGridFloorSheet();
        }

        private void BtnPDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                string date = row[4].ToString();
                MySqlConnection con = new MySqlConnection(Login.constring);
                con.Open();
                MySqlCommand cmdM = new MySqlCommand("DELETE from floor_sheet WHERE floor_id='" + row[0] + "';", con);
                cmdM.ExecuteNonQuery();
                con.Close();
                FillGridFloorSheet(date);
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            if (txtFDate.Text == "" || txtTDate.Text == "")
            {
                MessageBox.Show("Select date rang!", "Info");
                return;
            }
            if (txtTeacher.Text == "[EditValue is null]")
            {
                var a = txtClass.Text;
                var classId = "";
                var info = a.Split(',');
                int count = info.Count();
                int j = 0;
                foreach (var clas in info)
                {
                    classId += fun.GetClassIDisSession(clas.ToString().Trim(), fun.GetDefaultSessionName());
                    if (j < count - 1)
                        classId += ",";
                    j++;
                }
                var query = "";
                if (txtClass.Text != "")
                    query += " and floor_sheet.class_id in (" + classId + ")";
                list = new ObservableCollection<FloorSheetItems>();

                XtraFloorSheetReport report = new XtraFloorSheetReport();
                MySqlConnection con = new MySqlConnection(Login.constring);
                MySqlCommand cmd4 = new MySqlCommand("SELECT max( class.name) as Class,max(section.name) as Section,count(section.name) as NoOfLecture,max( teacher.name )as Teacher,sum(late_time ) as `Late Time` ,  max(date) as Date FROM floor_sheet join class on class.class_id=floor_sheet.class_id join section on section.section_id=floor_sheet.section_id left join teacher on teacher.teacher_id=floor_sheet.teacher_id where date>='" + Convert.ToDateTime(txtFDate.Text).ToString("yyyy-MM-dd") + "' and date<='" + Convert.ToDateTime(txtTDate.Text).ToString("yyyy-MM-dd") + "'and teacher.name!='' " + query + "  group by section.section_id,teacher.teacher_id order by teacher.name", con);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd4);
                DataTable table = new DataTable();
                list.Clear();
                adp.Fill(table);
                for (int row = 0; row <= table.Rows.Count - 1; row++)
                {
                    FloorSheetItems cl;
                    if (table.Rows[row][3].ToString() != "")
                    {
                        if (row % 2 == 0)
                        {
                            cl = new FloorSheetItems
                            {
                                Teacher = (table.Rows[row][3].ToString()),
                                Section = (table.Rows[row][1].ToString()),
                                Count = int.Parse(table.Rows[row][2].ToString()),
                                LTime = int.Parse((table.Rows[row][4].ToString())),
                            };
                            list.Add(cl);
                        }
                        else
                        {
                            var item = list.FirstOrDefault(i => i.Teacher == table.Rows[row][3].ToString() && i.Section1 == null);
                            if (item != null)
                            {
                                item.Section1 = (table.Rows[row][1].ToString());
                                item.Count1 = int.Parse(table.Rows[row][2].ToString());
                                item.LTime1 = int.Parse(table.Rows[row][4].ToString());

                            }
                            else
                            {
                                cl = new FloorSheetItems
                                {
                                    Teacher = (table.Rows[row][3].ToString()),
                                    Section = (table.Rows[row][1].ToString()),
                                    Count = int.Parse(table.Rows[row][2].ToString()),
                                    LTime = int.Parse((table.Rows[row][4].ToString())),

                                };
                                list.Add(cl);
                            }

                        }
                    }

                }
                report.GridControl = list;
                Image logo = fun.Base64ToImage(Login.Logo);
                var school = fun.GetSettings("system_title");
                report.PicIogoBox.Image = logo;
                report.LabTitle.Text = school;
                report.LabAddress.Text = fun.GetSettings("address");
                report.LabTel.Text = "Floor Sheet Report";

                report.LabExam.Text = Convert.ToDateTime(txtFDate.Text).ToString("dd-MM-yyyy") + " To " + Convert.ToDateTime(txtTDate.Text).ToString("dd-MM-yyyy");


                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
            }
            else
            {
                TeacherFloorSheetReport report = new TeacherFloorSheetReport();
                gridView1.OptionsPrint.ExpandAllGroups = false;
                report.GridControl = gridControl1;
                Image logo = fun.Base64ToImage(Login.Logo);
                var school = fun.GetSettings("system_title");
                report.PicIogoBox.Image = logo;
                report.LabTitle.Text = school;
                report.LabAddress.Text = fun.GetSettings("address");
                report.LabTel.Text = fun.GetSettings("phone");
                report.labTeacher.Text = fun.GetTeacherName(int.Parse(txtTeacher.Text.Trim()));
                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);

            }
        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            gridView1.GroupSummary.Clear();
            if (txtFDate.Text == "" || txtTDate.Text == "")
            {
                MessageBox.Show("Select date rang!", "Info");
                return;
            }
            var classId = txtClass.EditValue;
            var query = "";
            if (txtTeacher.EditValue != null && !string.IsNullOrEmpty(txtTeacher.Text))
                query += " and teacher.teacher_id=" + txtTeacher.EditValue;
            if (txtClass.EditValue != null)
                query += " and floor_sheet.class_id in (" + classId + ")";

            list = new ObservableCollection<FloorSheetItems>();

            MySqlConnection con = new MySqlConnection(Login.constring);
            MySqlCommand cmd4 = new MySqlCommand("SELECT class.name as Class,section.name as Section,floor_sheet.slate as Slate, teacher.name as Teacher,late_time as `Late Time`,floor_sheet.date as Date " +
                                            " FROM floor_sheet join class on class.class_id=floor_sheet.class_id join section on section.section_id=floor_sheet.section_id left join teacher on teacher.teacher_id=floor_sheet.teacher_id " +
                                            " where date>='" + Convert.ToDateTime(txtFDate.Text).ToString("yyyy-MM-dd") + "' and date<='" + Convert.ToDateTime(txtTDate.Text).ToString("yyyy-MM-dd") + "'and teacher.name!='' " + query + 
                                            "  order by teacher.name", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd4);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();
            gridView1.OptionsBehavior.ReadOnly = true;
            GridGroupSummaryItem item = new GridGroupSummaryItem();
            item.FieldName = "Teacher";
            item.DisplayFormat = "Total={0}";
            item.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridView1.GroupSummary.Add(item);
            var col1 = gridView1.Columns["Teacher"];
            GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Teacher", "{0}");
            col1.Summary.Add(item1);
            con.Close();

        }

        private void btnDelFloorSheet_Click(object sender, EventArgs e)
        {
            if (txtDateManage.Text == "")
            {
                MessageBox.Show("please select the date First","Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Do you really want to delete this Floor Sheet Agaist " + txtDateManage.Text + " Date", "Confirmation Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string date = Convert.ToDateTime(txtDateManage.Text).ToString("yyyy-MM-dd");
                string query = "DELETE FROM `floor_sheet` WHERE `date` = '" + date + "'";
                fun.ExecuteQuery(query);
                MessageBox.Show("Data is Deleted successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPrintTable_Click(object sender, EventArgs e)
        {
            XtraReportHead report = new XtraReportHead();
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.labDate.Text = fun.CurrentDate();
            report.LabTel.Text = "Floor Sheet";

            report.GridControl = gridControl1;
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }
    }
}
