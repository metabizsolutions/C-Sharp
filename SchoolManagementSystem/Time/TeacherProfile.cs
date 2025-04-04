using DevExpress.LookAndFeel;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraReports.UI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Time
{
    public partial class TeacherProfile : DevExpress.XtraEditors.XtraUserControl
    {
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();

        CommonFunctions fun = new CommonFunctions();
        string Time_Start;
        string Time_End;
        int time_duration;

        private static TeacherProfile _instance;
        public static TeacherProfile instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TeacherProfile();
                return _instance;
            }
        }
        public TeacherProfile()
        {
            InitializeComponent();
            

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
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnSave.Enabled = false;
            if (add)
            {
                btnSave.Enabled = true;
            }
            gridView1.OptionsBehavior.Editable = false;
            if (Edit)
            {
                gridView1.OptionsBehavior.Editable = true;
            }
            BtnPDelete.Enabled = false;
            if (Delete)
                BtnPDelete.Enabled = true;
        }
        private void TeacherProfile_Enter(object sender, EventArgs e)
        {
            allClass.Clear();
            txtLecture.Properties.Items.Clear();
            allClass.Clear();
            allClass = fun.GetAllTeacherWId();
            foreach (var allclass in allClass)
                txtLecture.Properties.Items.Add(allclass.Name);
            allClass.Clear();
            allClass = fun.GetAllClassisSession(fun.GetDefaultSessionName());
            txtClass.Properties.Items.Clear();
            foreach (var allclass in allClass)
                txtClass.Properties.Items.Add(allclass.Name);
            allClass.Clear();
            allClass = fun.GetAllSubjects();
            txtSubject.Properties.Items.Clear();
            foreach (var allclass in allClass)
                txtSubject.Properties.Items.Add(allclass.Name);
            txtSubject.Properties.Items.Add("PracticalBio");
            txtSubject.Properties.Items.Add("PracticalPhy");
            txtSubject.Properties.Items.Add("PracticalChem");
            txtSubject.Properties.Items.Add("Animation");
            FillGridTeacherProfile();
        }

        private void txtLecture_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSubject.Text = fun.GetTeachersSubject(txtLecture.Text);
        }
        private void txtClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSection.Properties.Items.Clear();
            allClass.Clear();
            allClass = fun.GetAllSectionisClass(txtClass.Text);
            foreach (var allclass in allClass)
                txtSection.Properties.Items.Add(allclass.Name);
            txtSection.Text = "";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            try
            {
                var classID = fun.GetClassIDisSession(txtClass.Text, Main_FD.SelectedSession);
                MySqlCommand cmd = new MySqlCommand("INSERT into time_teacher(teacher_id,subject,section_id,no_lecture) VALUES('" + fun.GetTeacherID(txtLecture.Text) + "','" + txtSubject.Text + "','" + fun.GetSectionIDisClass(txtSection.Text, classID) + "','" + txtNoLect.Text + "');", con);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Error");
                return;
            }
            con.Close();
            FillGridTeacherProfile();
            empty();
        }
        private void empty()
        {
            txtLecture.Text = "";
            txtSubject.Text = "";
            txtClass.Text = "";
            txtSection.Text = "";
            txtNoLect.Text = "";
        }
        RepositoryItemComboBox riCombo;
        void FillGridTeacherProfile()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdP = new MySqlCommand("SELECT time_teacher.id as ID, teacher.name as Teacher,time_teacher.subject as Subject,class.name as Class,section.name as Section,time_teacher.no_lecture as `No of Lecture`,time_teacher.days as Days  FROM time_teacher join section on section.section_id=time_teacher.section_id join teacher on teacher.teacher_id=time_teacher.teacher_id join class on class.class_id=section.class_id", con);
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
            riCombo = new RepositoryItemComboBox();
            allClass.Clear();
            allClass = fun.GetAllTeacherWId();
            foreach (var allclass in allClass)
                riCombo.Items.Add(allclass.Name);
            gridView1.Columns["Teacher"].ColumnEdit = riCombo;

            riCombo.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(_riCombo_EditValueChangeding);
            RepositoryItemCheckedComboBoxEdit riComboDay = new RepositoryItemCheckedComboBoxEdit();

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                riComboDay.Items.Add(day);
            gridView1.Columns["Days"].ColumnEdit = riComboDay;
            var col1 = gridView1.Columns["Teacher"];
            //  col1.Group();
            var col3 = gridView1.Columns["Class"];
            col3.Group();
            var col2 = gridView1.Columns["Section"];
            col2.Group();
            var col4 = gridView1.Columns["Days"];
            col4.Visible = false;
            var col5 = gridView1.Columns["No of Lecture"];
            gridView1.ExpandAllGroups();
            gridView1.GroupSummary.Clear();
            GridColumnSummaryItem item2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "No of Lecture", "{0}");
            col5.Summary.Add(item2);

            GridGroupSummaryItem item = new GridGroupSummaryItem();
            item.FieldName = "No of Lecture";
            item.DisplayFormat = "Count={0}";
            item.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.GroupSummary.Add(item);

        }
        string oldTeacher;
        string newTeacher;
        private void _riCombo_EditValueChangeding(object sender, ChangingEventArgs e)
        {
            newTeacher = e.NewValue.ToString();
            oldTeacher = e.OldValue.ToString();
        }



        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            MySqlConnection con = new MySqlConnection(Login.constring);

            var classID = fun.GetClassIDisSession(row[3].ToString(), Main_FD.SelectedSession);
            var sectionID = fun.GetSectionIDisClass(row[4].ToString(), classID);
            var NewTeacherID = fun.GetTeacherID(newTeacher);
            var OldTeacherID = fun.GetTeacherID(oldTeacher);
            con.Open();
            var day = row[6];
            MySqlCommand cmd = new MySqlCommand("UPDATE time_teacher set teacher_id='" + fun.GetTeacherID(row[1].ToString()) + "',no_lecture='" + row[5] + "',days='" + day + "' WHERE id='" + row[0] + "';", con);
            cmd.ExecuteNonQuery();
            con.Close();

            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM time_table where teacher_id='" + OldTeacherID + "' and section_id='" + sectionID + "'", con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    MySqlConnection con3 = new MySqlConnection(Login.constring);
                    con3.Open();

                    MySqlCommand cmd3 = new MySqlCommand("SELECT * FROM time_table where teacher_id='" + NewTeacherID + "' and day='" + reader1["day"].ToString() + "' and slate='" + reader1["slate"].ToString() + "'", con3);
                    MySqlDataReader reader3 = cmd3.ExecuteReader();
                    if (!reader3.HasRows)
                    {
                        con3.Close();
                        con3.Open();
                        var query = "update time_table set teacher_id='" + NewTeacherID + "' where teacher_id='" + OldTeacherID + "' and day='" + reader1["day"].ToString() + "' and slate='" + reader1["slate"].ToString() + "'";
                        MySqlCommand cmd2 = new MySqlCommand(query, con3);
                        cmd2.ExecuteNonQuery();
                        con3.Close();
                    }
                    con3.Close();
                }
            }
            con.Close();

            con.Open();
            MySqlCommand cmd5 = new MySqlCommand("SELECT * FROM time_table where teacher_id='" + OldTeacherID + "' and section_id='" + sectionID + "'", con);
            MySqlDataReader reader5 = cmd5.ExecuteReader();
            if (reader5.HasRows)
            {
                while (reader5.Read())
                {
                    MySqlConnection con3 = new MySqlConnection(Login.constring);
                    con3.Open();

                    MySqlCommand cmd3 = new MySqlCommand("SELECT * FROM time_table where section_id='" + sectionID + "' and day='" + reader1["day"].ToString() + "'and teacher_id='0'", con3);
                    MySqlDataReader reader3 = cmd3.ExecuteReader();
                    if (reader3.HasRows)
                    {
                        MySqlConnection con4 = new MySqlConnection(Login.constring);
                        con4.Open();
                        var query = "update time_table set teacher_id='" + NewTeacherID + "' where teacher_id='0' and day='" + reader3["day"].ToString() + "' and slate='" + reader3["slate"].ToString() + "'";
                        MySqlCommand cmd2 = new MySqlCommand(query, con4);
                        cmd2.ExecuteNonQuery();
                        con4.Close();
                    }
                    con3.Close();
                }
            }
            con.Close();


            // FillGridTeacherProfile();
        }

        private void BtnPDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to delete?", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    MySqlConnection con = new MySqlConnection(Login.constring);
                    con.Open();
                    MySqlCommand cmdM = new MySqlCommand("DELETE from time_teacher WHERE id='" + row[0] + "';", con);
                    cmdM.ExecuteNonQuery();
                    con.Close();
                    FillGridTeacherProfile();
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }
        //  string teacher;
        private void gridControl1_MouseDown(object sender, MouseEventArgs e)
        {
            //GridView View = sender as GridView;
            //var hitInfo = gridView1.CalcHitInfo(e.Location);
            //if (hitInfo.InRowCell)
            //{
            //    int rowHandle = hitInfo.RowHandle;
            //    GridColumn column = hitInfo.Column;
            //    var s = DoRowDoubleClick(gridView1, e.Location);

            //    DataRow row = s.Item1;
            //    object Val = s.Item2;
            //    teacher = s.Item2.ToString();
            //}
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            TeacherProfileReport report = new TeacherProfileReport();
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
    }
}
