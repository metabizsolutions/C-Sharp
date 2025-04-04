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
    public partial class SalaryCategory : DevExpress.XtraEditors.XtraUserControl
    {
        private static SalaryCategory _instance;
        public static SalaryCategory instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SalaryCategory();
                return _instance;
            }
        }

        public SalaryCategory()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            FillGridClass(); 
        }
        private void btnCAdd_Click(object sender, System.EventArgs e)
        {
            String query = "INSERT INTO salary_category SET salary_title = '{0}',default_val = '{1}'";
            if(txtTitle.Text == "")
            {
                MessageBox.Show("Title is required field!");
                return;
            }
            query = String.Format(query, txtTitle.Text, txtDefaultVal.Text);
            int id = fun.ExecuteInsert(query);

            query = "INSERT INTO salary_category_teacher(salary_cat_id,salary_val,teacher_id) SELECT '{0}' AS cat_id,'{1}' AS fees_val,teacher_id FROM teacher WHERE passout = 0";
            query = string.Format(query, id, txtDefaultVal.Text);
            fun.ExecuteQuery(query);
            MessageBox.Show("Your record created successfully! ");
            FillGridClass();
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
        public void FillGridClass()
        {
            string extra_col = "";
            string query = "";
            string query_cat = "SELECT salary_cat_id,salary_title FROM salary_category ORDER BY salary_cat_id ASC ";
            DataTable table_cat = fun.GetQueryTable(query_cat);
            cat_ids = new int[table_cat.Rows.Count];
            string[] cat_title = new string[table_cat.Rows.Count];
            int count = 0;
            foreach (DataRow row in table_cat.Rows)
            {
                cat_ids[count] = Convert.ToInt32(row["salary_cat_id"].ToString());
                cat_title[count] = row["salary_title"].ToString();
                extra_col += ",(select fct.salary_val from salary_category_teacher as fct where fct.teacher_id = teacher.teacher_id and fct.salary_cat_id = '"+row["salary_cat_id"].ToString() +"') AS '"+row["salary_title"].ToString()+"'";
                count++;
            }
            query = "SELECT teacher_id,staff_type,teacher.name AS teacher, salary,sex AS gender "+ extra_col+
                    " FROM teacher WHERE passout = 0 ";
        
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

            gridView1.Columns["teacher_id"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["staff_type"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["teacher"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["salary"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["gender"].OptionsColumn.ReadOnly = true;

            gridView1.Columns["staff_type"].Group();
            gridView1.ExpandAllGroups();
            foreach (string str in cat_title)
            {
                try
                {
                    gridView1.Columns[str].OptionsColumn.ReadOnly = false;
                }
                catch(Exception e) { }
            }
            
        }
        
        private void BtnPDelete_Click(object sender, EventArgs e)
        {
            int colindex = gridView1.FocusedColumn.AbsoluteIndex;
            
            if(colindex - 5 >= 0)
            {
                if (MessageBox.Show("Do You Really want to delete?", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int cat_id = cat_ids[colindex - 5];
                    string query_delete = "DELETE FROM salary_category WHERE salary_cat_id = '{0}'";
                    query_delete = string.Format(query_delete, cat_id);
                    fun.ExecuteQuery(query_delete);

                    query_delete = "DELETE FROM salary_category_teacher WHERE salary_cat_id = '{0}'";
                    query_delete = string.Format(query_delete, cat_id);
                    fun.ExecuteQuery(query_delete);
                    MessageBox.Show("Your record deleted successfully! ");
                    FillGridClass();
                }
            }
            else
            {
                MessageBox.Show("Select Category Column to delete ");
            }
            
            
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions fun = new CommonFunctions();
        private void gridView1_RowUpdated_1(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            int index = 5;
            string check_query, upsert_query;
            string upsert_query_col = " salary_cat_id = '{0}',salary_val = '{1}',teacher_id = '{2}'";
            DataTable CheckTable;
            foreach (int cat_id in cat_ids)
            {
                check_query = "SELECT * FROM salary_category_teacher WHERE teacher_id = '{0}' AND salary_cat_id = '{1}'";
                check_query = string.Format(check_query,row[0],cat_id);
                CheckTable = fun.GetQueryTable(check_query);

                if(CheckTable.Rows.Count > 0) // Update
                {
                    upsert_query = "UPDATE salary_category_teacher SET " + string.Format(upsert_query_col, cat_id,row[index], row[0]) +" WHERE id="+ CheckTable.Rows[0]["id"].ToString();
                }
                else //insert
                {
                    upsert_query = "INSERT INTO salary_category_teacher SET " + string.Format(upsert_query_col, cat_id, row[index], row[0]);
                }
                fun.ExecuteQuery(upsert_query);
                index++;
            }

        }
    }
}
