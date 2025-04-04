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
    public partial class ActivityStudent : DevExpress.XtraEditors.XtraUserControl
    {
        private static ActivityStudent _instance;

        public static ActivityStudent instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ActivityStudent();
                return _instance;
            }
        }
        public ActivityStudent()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            //Fill student spinner
            ObservableCollection<StudentInfo> studentinfo = new ObservableCollection<StudentInfo>();
            studentinfo = fun.GetAllStudentsWId_S_C_S("");
            searchLookUpEdit1.Text = "";
            searchLookUpEdit1.Properties.DataSource = studentinfo;
            searchLookUpEdit1.Properties.DisplayMember = "Name";
            searchLookUpEdit1.Properties.ValueMember = "ID";

            //fill category spinner
            DataTable table = fun.ActivityCategory_list();
            if (table.Rows.Count > 0)
            {
                SpinnerCategory.Properties.DataSource = table;
                SpinnerCategory.Properties.DisplayMember = "title";
                SpinnerCategory.Properties.ValueMember = "id";
            }

            FillGridClass();
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
        private void btnCAdd_Click(object sender, System.EventArgs e)
        {
            int ondate = fun.time();
            string date = fun.CurrentDate();
            string title = txtTitle.Text;
            string detail = txtDetail.Text;


            if (title == "" || searchLookUpEdit1.Text == "[EditValue is null]" || SpinnerCategory.Text == "[EditValue is null]")
            {
                MessageBox.Show("Student, Category,Title are required fields!");
                return;
            }
            var student_id = searchLookUpEdit1.EditValue;
            var cat_id = SpinnerCategory.EditValue;

            String query = "INSERT INTO activity_log SET student_id = '{0}', title = '{1}',detail = '{2}',activity_cat_id = '{3}',ondate = '{4}', date = '{5}',user_by = '{6}'";
            query = String.Format(query, student_id, title, detail, cat_id, ondate, date, Login.CurrentUserID);
            fun.ExecuteInsert(query);
            FillGridClass();
            empty();
        }
        private void empty()
        {
            txtTitle.Text = "";
        }

        public void FillGridClass()
        {

            string query_cat = "SELECT al.activity_log_id AS id, student.student_id,student.roll,student.name,al.title,al.date,al.detail  " +
                                " FROM activity_log AS al " +
                                " INNER JOIN student ON student.student_id = al.student_id ";
            DataTable table = fun.GetQueryTable(query_cat);

            gridClass.DataSource = null;
            gridClass.DataSource = table;
            gridView1.BestFitColumns();

        }


        private void BtnPDelete_Click(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (MessageBox.Show("Do You Really want to delete?", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string log_id = row["id"].ToString();
                string query_delete = "DELETE FROM activity_log WHERE activity_log_id = '{0}'";
                query_delete = string.Format(query_delete, log_id);
                fun.ExecuteQuery(query_delete);
                MessageBox.Show("Your record deleted successfully! ");
                FillGridClass();
            }



        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions fun = new CommonFunctions();


        private void gridView1_RowUpdated_1(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            int disable = Convert.ToBoolean(row["disable"]) ? 1 : 0;
            string query = "UPDATE activity_category SET title = '{0}', `disable` = '{1}' WHERE activity_cat_id = '{2}'";
            query = string.Format(query, row["title"], disable, row["activity_cat_id"]);
            fun.ExecuteQuery(query);
        }
    }
}
