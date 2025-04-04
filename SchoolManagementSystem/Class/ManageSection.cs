using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using MySql.Data.MySqlClient;
using DevExpress.XtraGrid.Views.Base;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Class
{
    public partial class ManageSection : DevExpress.XtraEditors.XtraUserControl
    {
        private static ManageSection _instance;

        public static ManageSection instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ManageSection();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions fun = new CommonFunctions();
        public ManageSection()
        {
            InitializeComponent();
            loadfunctions();
            
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnSAdd.Enabled = false;
            gridView1.OptionsBehavior.Editable = false;
            BtnSDelete.Enabled = false;
            if (add)
            {
                btnSAdd.Enabled = true;
            }
            if (Edit)
            {
                gridView1.OptionsBehavior.Editable = true;
            }
            if (Delete)
                BtnSDelete.Enabled = true;
        }
        public void loadfunctions()
        {
            FillGridSections();
        }
        private void btnSAdd_Click(object sender, EventArgs e)
        {
            using (Create_section de = new Create_section())
            {
                if (de.ShowDialog() == DialogResult.Yes) { }
                else
                    FillGridSections();
            }
        }

        public void FillGridSections()
        {
            string query = "SELECT sec.section_id as ID,sec.name as Section,sec.class_id,sec.time_start as TimeStart,sec.time_end as TimeEnd,c.`name` as Class, sec.teacher_id as Teacher,sec.days as Days,created_date " +
                " FROM section as sec " +
                " inner join class as c on c.class_id = sec.class_id " +
                "order by sec.class_id,sec.section_id asc;";
            DataTable table = fun.FetchDataTable(query);
            gridSection.DataSource = table;
            ColumnView view = (ColumnView)gridSection.FocusedView;
            view.ClearColumnsFilter();
            for (int i = 0; i < view.Columns.Count; i++)
            {
                string c = view.Columns[i].FieldName;
                view.Columns[i].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                if (c == "ID" || c == "Class" || c== "class_id")
                {
                    view.Columns[i].OptionsColumn.ReadOnly = true;
                }
                if (c == "Teacher")
                {
                    RepositoryItemSearchLookUpEdit reCombo = new RepositoryItemSearchLookUpEdit();
                    reCombo.DataSource = fun.GetAllTeacher_dt();
                    reCombo.ValueMember = "teacher_id";
                    reCombo.DisplayMember = "name";
                    gridView1.Columns["Teacher"].ColumnEdit = reCombo;
                }
                if (c == "Days")
                {
                    RepositoryItemCheckedComboBoxEdit riCheckbo = new RepositoryItemCheckedComboBoxEdit();
                    foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                        riCheckbo.Items.Add(day);
                    gridView1.Columns["Days"].ColumnEdit = riCheckbo;
                }
            }
            gridView1.Columns["class_id"].Group();
            gridView1.Columns["class_id"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            gridView1.BestFitColumns();
            gridView1.ExpandAllGroups();

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
            string query = "UPDATE section set name='" + row["Section"] + "',time_start='" + row["TimeStart"] + "',time_end='" + row["TimeEnd"] + "',teacher_id='" + row["Teacher"] + "',sync='0',days='" + row["Days"] + "' WHERE section_id='" + row["ID"] + "';";
            fun.ExecuteQuery(query);
        }
        private void BtnSDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                string query = "select * from student where section_id = '" + row["ID"] + "'";
                DataTable dt = fun.FetchDataTable(query);
                if (dt.Rows.Count <= 0)
                {
                    if (MessageBox.Show("Are you sure to delete?", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        query = "DELETE from section WHERE section_id='" + row["ID"] + "';";
                        fun.ExecuteQuery(query);
                        FillGridSections();
                    }
                }
                else
                {
                    MessageBox.Show("You Cannot delete this section bcz some students already have in this section", "Section info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }

        private void ManageSection_Enter(object sender, EventArgs e)
        {

        }
    }
}
