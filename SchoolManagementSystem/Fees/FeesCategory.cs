using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Class
{
    public partial class FeesCategory : DevExpress.XtraEditors.XtraUserControl
    {
        private static FeesCategory _instance;

        public static FeesCategory instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FeesCategory();
                return _instance;
            }
        }
        public FeesCategory()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";
            FillGridClass("");
        }
        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtClass.EditValue.ToString()))
            {
                txtSection.Properties.DataSource = fun.GetAllSection_dt(txtClass.EditValue.ToString());
                txtSection.Properties.DisplayMember = "name";
                txtSection.Properties.ValueMember = "section_id";
            }
        }
        private void btnCAdd_Click(object sender, System.EventArgs e)
        {
            if (txtTitle.Text == "")
            {
                MessageBox.Show("Title is required field!");
                return;
            }
            if (string.IsNullOrEmpty(txtClass.EditValue.ToString()) || string.IsNullOrEmpty(txtSection.EditValue.ToString()))
            {
                MessageBox.Show("Class and section are required field!");
                return;
            }
            string query = "select * from fees_category where fees_title = '"+ txtTitle.Text + "'";
            DataTable dt_fee_cat = fun.FetchDataTable(query);
            int id = 0;
            if (dt_fee_cat.Rows.Count <= 0)
            {
                query = "INSERT INTO fees_category SET fees_title = '" + txtTitle.Text + "',default_val = '" + txtDefaultVal.Text + "'";
                id = fun.ExecuteInsert(query);
            }
            else
                id = Convert.ToInt32(dt_fee_cat.Rows[0]["fee_cat_id"]);
            string allsections = txtSection.EditValue.ToString();
            string[] sec_arr = allsections.Split(',');
            string std_cat_query = "";
            foreach (var sec_id in sec_arr)
            {
                query = "select * from student where section_id = '"+sec_id+"'";
                dt_fee_cat = fun.FetchDataTable(query);
                foreach (DataRow dr in dt_fee_cat.Rows)
                {
                    query = "select * from fees_category_student where fees_cat_id = '" + id + "' and student_id = '" + dr["student_id"] + "' ";
                    DataTable dt_std_cat = fun.FetchDataTable(query);
                    if (dt_std_cat.Rows.Count <= 0)
                        std_cat_query += "INSERT INTO fees_category_student(fees_cat_id,fees_val,student_id) Values ('" + id + "','" + txtDefaultVal.Text + "','" + dr["student_id"] + "'); ";
                    else
                        std_cat_query += "UPDATE `fees_category_student` SET `fees_val` = '" + txtDefaultVal.Text + "' WHERE `id` = '" + dt_std_cat.Rows[0]["id"] + "';";
                }
            }
            fun.ExecuteQuery(std_cat_query);
            FillGridClass(allsections);
            empty();
        }
        private void empty()
        {
            txtTitle.Text = "";
            txtDefaultVal.Text = "0.0";
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnCAdd.Enabled = false;
            if (add)
            {
                btnCAdd.Enabled = true;
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
        int[] cat_ids;
        public void FillGridClass(string section_ids)
        {
            string extra_col = "";
            string query = "";
            string query_cat = "SELECT fee_cat_id,fees_title FROM fees_category ORDER BY fee_cat_id ASC ";
            DataTable table_cat = fun.GetQueryTable(query_cat);
            cat_ids = new int[table_cat.Rows.Count];
            string[] cat_title = new string[table_cat.Rows.Count];
            int count = 0;
            foreach (DataRow row in table_cat.Rows)
            {
                cat_ids[count] = Convert.ToInt32(row["fee_cat_id"].ToString());
                cat_title[count] = row["fees_title"].ToString();
                extra_col += ",(select fct.fees_val from fees_category_student as fct where fct.student_id = student.student_id and fct.fees_cat_id = '" + row["fee_cat_id"].ToString() + "') AS '" + row["fees_title"].ToString() + "'";
                count++;
            }
            string where = "";
            if (!string.IsNullOrEmpty(section_ids))
                where = "and student.section_id in (" + section_ids + ")";
            query = "SELECT student_id,roll,student.name AS student,class.name AS class, section.name AS section " + extra_col +
                " FROM student " +
                " inner join class on class.class_id = student.class_id " +
                " inner join section on section.section_id = student.section_id " +
                " WHERE passout = 0 "+where+"; ";

            DataTable table = fun.GetQueryTable(query);
            gridView1.ClearGrouping();
            gridClass.BeginUpdate();
            try
            {
                gridView1.Columns.Clear();
                gridClass.DataSource = null;
                gridClass.DataSource = table;
            }
            finally
            {
                gridClass.EndUpdate();
            }

            gridView1.BestFitColumns();

            gridView1.Columns["student_id"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["roll"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["student"].OptionsColumn.ReadOnly = true;

            gridView1.Columns["class"].Group();
            gridView1.Columns["section"].Group();
            gridView1.ExpandAllGroups();
            foreach (string str in cat_title)
            {
                try
                {
                    gridView1.Columns[str].OptionsColumn.ReadOnly = false;
                }
                catch (Exception e)
                {

                }
            }

        }

        private void BtnPDelete_Click(object sender, EventArgs e)
        {
            int colindex = gridView1.FocusedColumn.AbsoluteIndex;

            if (colindex - 5 >= 0)
            {
                int cat_id = cat_ids[colindex - 5];
                string query = "select * from fees_category_student where fees_cat_id = '"+ cat_id + "'";
                DataTable dt_fee_cat = fun.FetchDataTable(query);
                if (dt_fee_cat.Rows.Count <= 0)
                {
                    if (MessageBox.Show("Do You Really want to delete?", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string query_delete = "DELETE FROM fees_category WHERE fee_cat_id = '{0}';";
                        query_delete += "DELETE FROM fees_category_student WHERE fees_cat_id = '{0}';";
                        query_delete = string.Format(query_delete, cat_id);
                        fun.ExecuteQuery(query_delete);

                        gridView1.DeleteRow(gridView1.FocusedRowHandle);
                    }
                }
                else
                    MessageBox.Show("Selected Category has been used .... Now you cannot Delete this Category", "Delete Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Select Category Column to delete ","Delete Category",MessageBoxButtons.OK,MessageBoxIcon.Information);


        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions fun = new CommonFunctions();
        private void ManageClass_Enter(object sender, EventArgs e)
        {
            /*
            allClass.Clear();
            txtTeacher.Properties.Items.Clear();
            allClass = fun.GetAllTeacher();
            foreach (var allclass in allClass)
                txtTeacher.Properties.Items.Add(allclass.Name);

            FillGridClass();
            */
        }

        private void gridView1_RowUpdated_1(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            int index = 5;
            string check_query, upsert_query;
            string upsert_query_col = " fees_cat_id = '{0}',fees_val = '{1}',student_id = '{2}'";
            DataTable CheckTable;
            foreach (int cat_id in cat_ids)
            {
                check_query = "SELECT * FROM fees_category_student WHERE student_id = '{0}' AND fees_cat_id = '{1}'";
                check_query = string.Format(check_query, row[0], cat_id);
                CheckTable = fun.GetQueryTable(check_query);

                if (CheckTable.Rows.Count > 0) // Update
                {
                    upsert_query = "UPDATE fees_category_student SET " + string.Format(upsert_query_col, cat_id, row[index], row[0]) + " WHERE id=" + CheckTable.Rows[0]["id"].ToString();
                }
                else //insert
                {
                    upsert_query = "INSERT INTO fees_category_student SET " + string.Format(upsert_query_col, cat_id, row[index], row[0]);
                }
                fun.ExecuteQuery(upsert_query);
                index++;
            }

        }

        private void txtSection_Leave(object sender, EventArgs e)
        {
            string section_ids = txtSection.EditValue.ToString();
            FillGridClass(section_ids);
        }

        private void txtSection_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
