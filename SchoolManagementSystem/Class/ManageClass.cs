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
    public partial class ManageClass : DevExpress.XtraEditors.XtraUserControl
    {
        private static ManageClass _instance;

        public static ManageClass instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ManageClass();
                return _instance;
            }
        }
        public ManageClass()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            FillGridClass();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnCAdd.Enabled = false;
            gridView1.OptionsBehavior.Editable = false;
            BtnPDelete.Enabled = false;
            if (add)
            {
                btnCAdd.Enabled = true;
            }
            if (Edit)
            {
                gridView1.OptionsBehavior.Editable = true;
            }
            if (Delete)
                BtnPDelete.Enabled = true;
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions fun = new CommonFunctions();

        private void ManageClass_Enter(object sender, EventArgs e)
        {
            
        }

        public void FillGridClass()
        {
            string query = "SELECT * from classes";
            DataTable table = fun.FetchDataTable(query);
            gridClass.DataSource = table;
            gridView1.BestFitColumns();
            var col = gridView1.Columns["Class_id"];
            col.OptionsColumn.ReadOnly = true;
            RepositoryItemComboBox riCombo = new RepositoryItemComboBox();
            allClass.Clear();
            allClass = fun.GetAllTeacher();
            foreach (var allclass in allClass)
                riCombo.Items.Add(allclass.Name);
            gridView1.Columns["Teacher Name"].ColumnEdit = riCombo;
            RepositoryItemLookUpEdit degreecom = new RepositoryItemLookUpEdit();
            degreecom.DataSource = fun.degrees();
            
            degreecom.DisplayMember = "name";
            degreecom.ValueMember = "degree_id";
            gridView1.Columns["degree_id"].ColumnEdit = degreecom;

            string sessionname = fun.GetDefaultSessionName();
            if (Main_FD.SelectedSession != sessionname)
            {
                btnCAdd.Enabled = false;
                BtnPDelete.Enabled = false;
                gridView1.OptionsBehavior.Editable = false;
            }
            else
            {
                btnCAdd.Enabled = true;
                BtnPDelete.Enabled = true;
                gridView1.OptionsBehavior.Editable = true;
            }
        }
        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row["degree_id"] == null)
            {
                MessageBox.Show("Please Select Degree");
                return;
            }
            if (row["name_digit"] == null)
            {
                MessageBox.Show("Please Write Name In Digit ");
                return;
            }
            string query = "UPDATE class set name='" + row["Name"] + "',degree_id='" + row["degree_id"] + "',name_digit='" + row["name_digit"] + "',teacher_id='" + fun.GetTeacherID(row["Teacher Name"].ToString()) + "',time_start='" + row["Time_start"] + "' ,time_end='" + row["Time_end"] + "' ,sync='0' WHERE class_id='" + row["Class_id"] + "';";
            query += "UPDATE `section` SET time_start='" + row["Time_start"] + "' ,time_end='" + row["Time_end"] + "' WHERE class_id='" + row["Class_id"] + "'";
            fun.ExecuteQuery(query);
            FillGridClass();
        }

        private void BtnPDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                string query = "select * from student where class_id = '" + row["Class_id"] + "';";
                DataTable dt = fun.FetchDataTable(query);
                if (dt.Rows.Count <= 0)
                {
                    if (MessageBox.Show("Are you sure to delete?", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        query = "DELETE from class WHERE class_id='" + row["Class_id"] + "';";
                        fun.ExecuteQuery(query);
                        FillGridClass();
                    }
                }
                else
                    MessageBox.Show("this class have some students in it so you cannot delete this class now", "class Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }

        private void btnCAdd_Click(object sender, EventArgs e)
        {
            using (Create_class de = new Create_class())
            {
                if (de.ShowDialog() == DialogResult.Yes) { }
                else
                    FillGridClass();
            }
        }
    }
}
