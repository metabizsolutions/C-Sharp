using DevExpress.XtraEditors.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Class
{
    public partial class Subjects : DevExpress.XtraEditors.XtraUserControl
    {
        private static Subjects _instance;

        public static Subjects instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Subjects();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions fun = new CommonFunctions();

        public Subjects()
        {
            InitializeComponent();
            loadfunctions();

        }
        public void loadfunctions()
        {
            String examSQL = "SELECT teacher_id,`name`,designation,subject_code FROM teacher ORDER BY name DESC";
            DropdownTeacher.Properties.DataSource = fun.FetchDataTable(examSQL);
            DropdownTeacher.Properties.DisplayMember = "name";
            DropdownTeacher.Properties.ValueMember = "teacher_id";
            fillcomboClass();
            load_default_subjects();
            FillGridSubject();

        }
        void load_default_subjects()
		{
            string query = "select * from subject_default";
            DataTable sub_dt = fun.FetchDataTable(query);
            CCB_subjects.Properties.DataSource = sub_dt;
            CCB_subjects.Properties.DisplayMember = "name";
            CCB_subjects.Properties.ValueMember = "id";
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            if (add)
            {
                btnSAdd.Enabled = true;
            }
            gridView1.OptionsBehavior.Editable = false;
            if (Edit)
            {
                gridView1.OptionsBehavior.Editable = true;
            }
            BtnSDelete.Enabled = false;
            if (Delete)
                BtnSDelete.Enabled = true;
        }
        public void fillcomboClass()
        {
            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";
        }
        private void btnSAdd_Click(object sender, EventArgs e)
        {
            if (txtSection.EditValue == null || CCB_subjects.EditValue == null)
            {
                MessageBox.Show("Please select required fields", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var a = txtSection.EditValue.ToString();
            var info = a.Split(',');
            foreach (var sec in info)
            {
                string teacher = DropdownTeacher.EditValue == null ? "0" : DropdownTeacher.EditValue.ToString();
                string subject_fee = txt_SubjectFee.EditValue == null ? "0" : txt_SubjectFee.EditValue.ToString();
                fun.insert_subject_section(txtClass.EditValue.ToString(), sec, CCB_subjects.EditValue.ToString(), teacher, txtWorkLoad.Text, subject_fee);
            }
            FillGridSubject();
            empty();
        }

        private void empty()
        {
            CCB_subjects.EditValue = null;
            txtClass.EditValue = null;
            txtSection.EditValue = null;
            DropdownTeacher.EditValue = null;
        }
        public void FillGridSubject()
        {
            string query = "select ss.`sec_sub_id` AS ID,sub_de.`subject_code` AS `Subject Code`,sub_de.`name` AS `subject`,sec.name AS Section,cls.name AS Class, ss.work_load AS Work_Load,ss.`teacher_id` AS Teacher,ss.`subject_fee` as SubjectFee from `section_subject` as ss " +
                            " inner join section as sec on sec.`section_id` = ss.`section_id` " +
                            " inner join class as cls on cls.`class_id` = ss.`class_id` " +
                            " inner join `subject_default` as sub_de on sub_de.`id` = ss.`subject_id`";
            DataTable table = fun.FetchDataTable(query);
            gridView1.Columns.Clear();
            gridSubject.DataSource = table;
            gridView1.BestFitColumns();
            var col = gridView1.Columns["ID"];
            col.OptionsColumn.ReadOnly = true;
            var col2 = gridView1.Columns["Class"];
            col2.OptionsColumn.ReadOnly = true;
            var col1 = gridView1.Columns["Section"];
            col1.Group();
            gridView1.ExpandAllGroups();

            RepositoryItemSearchLookUpEdit TeacherCmbo = new RepositoryItemSearchLookUpEdit();
            String examSQL = "SELECT teacher_id,`name`,designation,subject_code FROM teacher ORDER BY name DESC";
            TeacherCmbo.DataSource = fun.FetchDataTable(examSQL);
            TeacherCmbo.DisplayMember = "name";
            TeacherCmbo.ValueMember = "teacher_id";
            gridView1.Columns["Teacher"].ColumnEdit = TeacherCmbo;

            if (Main_FD.SelectedSession != fun.GetDefaultSessionName())
            {
                btnSAdd.Enabled = false;
                BtnSDelete.Enabled = false;
                gridView1.OptionsBehavior.Editable = false;
            }
            else
            {
                btnSAdd.Enabled = true;
                BtnSDelete.Enabled = true;
                gridView1.OptionsBehavior.Editable = true;
            }
        }
        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            string query = "UPDATE section_subject set work_load='" + row["work_load"] + "',teacher_id='" + row["Teacher"] + "',subject_fee='" + row["SubjectFee"] + "' WHERE sec_sub_id='" + row["ID"] + "';";
            fun.ExecuteQuery(query);
        }
        private void BtnSDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Student result data related to this subject ", "Are You Sure to Delete!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    string subject_id = row["ID"].ToString();
                    String query = "DELETE from section_subject WHERE sec_sub_id='{0}'; ";
                    query = String.Format(query, subject_id);
                    fun.ExecuteQuery(query);
                    FillGridSubject();
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }
        private void Subjects_Enter(object sender, EventArgs e)
        {


        }

        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            string class_id = txtClass.EditValue == null ? "0" : txtClass.EditValue.ToString();
            if (!string.IsNullOrEmpty(class_id) && class_id != "0")
            {
                txtSection.Properties.DataSource = fun.GetAllSection_dt(class_id);
                txtSection.Properties.DisplayMember = "name";
                txtSection.Properties.ValueMember = "section_id";
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            old_subjects_load();
        }
        void old_subjects_load()
        {
            gridView1.Columns.Clear();
            string query = "select sub.subject_id,sub.name,cls.name as class,sec.name as section,`subject_code`,`work_load`,`subject_fee`,sub.teacher_id,sec.class_id,sub.section_id from `subject` as sub" +
                            " inner join section as sec on sec.`section_id` = sub.`section_id` " +
                            " inner join class as cls on cls.`class_id` = sec.`class_id` ";
            gridSubject.DataSource = fun.FetchDataTable(query);
            gridView1.BestFitColumns();
            var col2 = gridView1.Columns["class"];
            col2.OptionsColumn.ReadOnly = true;
            var col1 = gridView1.Columns["section"];
            col1.Group();
            gridView1.ExpandAllGroups();
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (CCB_subjects.EditValue == null)
            {
                MessageBox.Show("Select subject to change name");
                return;
            }
            if (MessageBox.Show("Are your Sure to update subjects name ", "Update!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string query = "";
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    DataRow r = gridView1.GetDataRow(i);
                    query = "UPDATE `subject` SET `name` = '" + CCB_subjects.Text + "' WHERE `subject_id` = '" + r["subject_id"] + "';";
                    fun.ExecuteQuery(query);
                }
                old_subjects_load();
            }

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string query = "select sub.subject_id,sub.name,sub.teacher_id,cls.class_id,sec.section_id,sub.`subject_code`,sub.`work_load`,sub.`subject_fee` from `subject` as sub" +
                            " inner join section as sec on sec.`section_id` = sub.`section_id` " +
                            " inner join class as cls on cls.`class_id` = sec.`class_id` order by sub.section_id ";
            DataTable dt = fun.FetchDataTable(query);
            object new_sub = 0;
            fun.loaderform(() =>
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string cls_id = string.IsNullOrEmpty(dr["class_id"].ToString())?"0": dr["class_id"].ToString();
                    string sec_id = string.IsNullOrEmpty(dr["section_id"].ToString()) ? "0" : dr["section_id"].ToString();
                    string sub_id = string.IsNullOrEmpty(dr["subject_id"].ToString()) ? "0" : dr["subject_id"].ToString();
                    string tech_id = string.IsNullOrEmpty(dr["teacher_id"].ToString()) ? "0" : dr["teacher_id"].ToString();
                    string workload = string.IsNullOrEmpty(dr["work_load"].ToString()) ? "0" : dr["work_load"].ToString();
                    string subject_fee = string.IsNullOrEmpty(dr["subject_fee"].ToString())?"0": dr["subject_fee"].ToString();
                    string name = string.IsNullOrEmpty(dr["name"].ToString())?"0": dr["name"].ToString();

                    string sub_query = "select ifnull(id,0) from subject_default where name = '" + name + "'";
                    new_sub = fun.Execute_Scaler_string(sub_query);
                    if (new_sub == null)
                    {
                        MessageBox.Show("Please add default subject against '" + name + "'", "info");
                        return;
                    }
                    query = "";
                    fun.insert_subject_section(cls_id, sec_id, new_sub.ToString(), tech_id, workload, subject_fee);
                    query += "UPDATE `fee_by_subject_teacher` SET `class_id` = '" + cls_id + "',`section_id` = '" + sec_id + "',`subject_id` = '" + new_sub + "' WHERE subject_id = '" + sub_id + "';";
                    query += "UPDATE `tbl_mark_subject` SET `class_id` = '" + cls_id + "',`section_id` = '" + sec_id + "',`subject_id` = '" + new_sub + "' WHERE `subject_id` = '" + sub_id + "';";
                    query += "UPDATE `mark` SET `class_id` = '" + cls_id + "',`section_id` = '" + sec_id + "',`subject_id` = '" + new_sub + "' WHERE `subject_id` = '" + sub_id + "';";
                    fun.ExecuteQuery(query);
                }
                MessageBox.Show("Subjects are transfered into new table you can use now", "Info");
            });
            FillGridSubject();
        }

        private void link_add_default_subjects_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (Default_subjects objfrom = new Default_subjects())
            {
                if (objfrom.ShowDialog() == DialogResult.Yes)
                    link_add_default_subjects.Enabled = false;
                else
                {
                    link_add_default_subjects.Enabled = true;
                    load_default_subjects();
                }

            }
        }
    }
}
