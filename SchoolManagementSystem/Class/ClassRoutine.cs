using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Class
{
    public partial class ClassRoutine : DevExpress.XtraEditors.XtraUserControl
    {
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions fun = new CommonFunctions();

        public ClassRoutine()
        {
            InitializeComponent();

            txteTime.Properties.Mask.MaskType = MaskType.DateTime;
            txteTime.Properties.Mask.EditMask = "HH:mm";
            txteTime.MaskBox.Mask.UseMaskAsDisplayFormat = true;
            txtsTime.Properties.Mask.MaskType = MaskType.DateTime;
            txtsTime.Properties.Mask.EditMask = "HH:mm";
            txtsTime.MaskBox.Mask.UseMaskAsDisplayFormat = true;
        }

        private void btnCrAdd_Click(object sender, EventArgs e)
        {
            var classID = fun.GetClassIDisSession(txtClass.Text, fun.GetDefaultSessionName());
            var sectionID = fun.GetSectionIDisClass(txtSection.Text, classID);

            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("INSERT into class_routine(class_id,subject_id,time_start,time_end,day,sync,section_id) VALUES('" + classID + "','" + fun.GetSubjectID(txtSubject.Text, sectionID) + "','" + txtsTime.Text + "','" + txteTime.Text + "','" + txtDay.Text + "','0','" + sectionID + "');", con);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
                return;
            }
            con.Close();
            FillGridClassRoutine();
            empty();
        }
        private void empty()
        {
            txtSubject.Text = "";
            txtDay.Text = "";
            txtClass.Text = "";
            txteTime.Text = "";
            txtsTime.Text = "";
        }

        public void FillGridClassRoutine()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdP = new MySqlCommand("SELECT class_routine_id as ID,class.name as Class,section.name as Section, subject.name as Subject,class_routine.day as Day,class_routine.time_start as StartingTime, class_routine.time_end as EndingTime FROM class_routine left join class on(class.class_id=class_routine.class_id) left join section on(section.section_id=class_routine.section_id) left join subject on subject.subject_id=class_routine.subject_id where 1 = 1", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridClassRoutine.DataSource = table;
            gridView1.BestFitColumns();
            var col = gridView1.Columns["ID"];
            col.OptionsColumn.ReadOnly = true;
            var col1 = gridView1.Columns["Class"];
            col1.Group();
            gridView1.ExpandAllGroups();
            con.Close();
            //RepositoryItemComboBox riCombo = new RepositoryItemComboBox();
            //allClass.Clear();
            //allClass = fun.GetAllSubject();
            //foreach (var allclass in allClass)
            //    riCombo.Items.Add(allclass.Name);
            //gridView1.Columns["Subject"].ColumnEdit = riCombo;
            riComboC = new RepositoryItemComboBox();
            //allClass.Clear();
            //allClass = fun.GetAllClass();
            //foreach (var allclass in allClass)
            //    riComboC.Items.Add(allclass.Name);
            //gridView1.Columns["Class"].ColumnEdit = riComboC;
            RepositoryItemComboBox riComboDay = new RepositoryItemComboBox();
            riComboDay.Items.Add("Monday");
            riComboDay.Items.Add("Tuesday");
            riComboDay.Items.Add("Wednesday");
            riComboDay.Items.Add("Thursday");
            riComboDay.Items.Add("Friday");
            riComboDay.Items.Add("Saturday");
            riComboDay.Items.Add("Sunday");
            gridView1.Columns["Day"].ColumnEdit = riComboDay;
            if (Main_FD.SelectedSession != fun.GetDefaultSessionName())
            {
                btnCrAdd.Enabled = false;
                btnCrDelete.Enabled = false;
                gridView1.OptionsBehavior.Editable = false;
            }
            else
            {
                btnCrAdd.Enabled = true;
                btnCrDelete.Enabled = true;
                gridView1.OptionsBehavior.Editable = true;
            }

        }
        RepositoryItemComboBox riComboC;
        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            var classID = fun.GetClassIDisSession(row[1].ToString(), fun.GetDefaultSessionName());
            var sectionID = fun.GetSectionIDisClass(row[2].ToString(), classID);
            var subjectID = fun.GetSubjectID(row[3].ToString(), sectionID);
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE class_routine set subject_id='" + subjectID + "',class_id='" + classID + "',day='" + row[4] + "',time_start='" + row[5] + "',time_end='" + row[6] + "' ,sync='0',section_id='" + sectionID + "'WHERE class_routine_id='" + row[0] + "';", con);
            cmd.ExecuteNonQuery();
            con.Close();
            FillGridClassRoutine();
        }
        private void btnCrDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to delete?", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    MySqlConnection con = new MySqlConnection(Login.constring);
                    con.Open();
                    MySqlCommand cmdM = new MySqlCommand("DELETE from class_routine WHERE class_routine_id='" + row[0] + "';", con);
                    cmdM.ExecuteNonQuery();
                    con.Close();
                    FillGridClassRoutine();
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }

        private void ClassRoutine_Enter(object sender, EventArgs e)
        {
            FillGridClassRoutine();
            txtClass.Properties.Items.Clear();
            allClass.Clear();
            allClass = fun.GetAllClassisSession(fun.GetDefaultSessionName());
            foreach (var allclass in allClass)
                txtClass.Properties.Items.Add(allclass.Name);
        }

        private void txtClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSection.Properties.Items.Clear();
            allClass.Clear();
            allClass = fun.GetAllSectionisClass(txtClass.Text);
            foreach (var allclass in allClass)
                txtSection.Properties.Items.Add(allclass.Name);
            txtSection.Text = "";
            txtSubject.Text = "";
        }

        private void txtSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSubject.Properties.Items.Clear();
            allClass.Clear();
            var classID = fun.GetClassIDisSession(txtClass.Text, fun.GetDefaultSessionName());
            var sectionID = fun.GetSectionIDisClass(txtSection.Text, classID);
            allClass = fun.GetAllSubject(sectionID);
            foreach (var allclass in allClass)
                txtSubject.Properties.Items.Add(allclass.Name);
        }

        private void gridView1_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName != "Section" && e.Column.FieldName != "Class" && e.Column.FieldName != "Subject")
                return;
            GridView gv = sender as GridView;
            if (e.Column.FieldName == "Section")
            {
                string classN = gv.GetRowCellValue(e.RowHandle, gv.Columns[1]).ToString();
                riComboC.Items.Clear();
                allClass.Clear();
                allClass = fun.GetAllSectionisClass(classN);
                foreach (var allclass in allClass)
                    riComboC.Items.Add(allclass.Name.ToString());
                gridView1.Columns["Section"].ColumnEdit = riComboC;
                // e.RepositoryItem = riComboC;
            }
            if (e.Column.FieldName == "Class")
            {
                riComboC.Items.Clear();
                allClass.Clear();
                allClass = fun.GetAllClassisSession(fun.GetDefaultSessionName());
                foreach (var allclass in allClass)
                    riComboC.Items.Add(allclass.Name.ToString());
                gridView1.Columns["Class"].ColumnEdit = riComboC;
                // e.RepositoryItem = riComboC;
            }
            if (e.Column.FieldName == "Subject")
            {
                riComboC.Items.Clear();
                allClass.Clear();
                string classN = gv.GetRowCellValue(e.RowHandle, gv.Columns["Class"]).ToString();
                string SectionN = gv.GetRowCellValue(e.RowHandle, gv.Columns["Section"]).ToString();
                var classID = fun.GetClassIDisSession(classN, fun.GetDefaultSessionName());
                var sectionID = fun.GetSectionIDisClass(SectionN, classID);
                allClass = fun.GetAllSubject(sectionID);
                foreach (var allclass in allClass)
                    riComboC.Items.Add(allclass.Name.ToString());
                gridView1.Columns["Subject"].ColumnEdit = riComboC;
                // e.RepositoryItem = riComboC;
            }
        }
    }
}
