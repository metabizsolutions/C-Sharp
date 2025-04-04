using DevExpress.XtraEditors.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Fees
{
    public partial class FeeSetting : DevExpress.XtraEditors.XtraUserControl
    {
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions fun = new CommonFunctions();
        private static FeeSetting _instance;
        public static FeeSetting instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FeeSetting();
                return _instance;
            }
        }
        public FeeSetting()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            FillGridFeeSetting();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            gridView1.OptionsBehavior.Editable = false;
            if (Edit)
            {
                gridView1.OptionsBehavior.Editable = true;
            }
        }
        public void FillGridFeeSetting()
        {
            String sql = "SELECT fees_id as ID,class.class_id as ClassID,class.name as Class,admission as AdmissionFee,security_deposit as SecurityDeposit,monthly as MonthlyFee,annual_charges as Annual_Charges,exam_charges as ExamCharges,card_charges as CardCharges,late_fee as `Late_Fee` " +
                            " FROM class_fees right join class on(class.class_id=class_fees.class_id) " +
                            " where 1 = 1";
            String checkquery = sql + " AND fees_id IS NULL ";
            String insert_query = "";
            DataTable table = new DataTable();
            table = fun.FetchDataTable(checkquery);
            if (table.Rows.Count > 0) {
                foreach (DataRow row in table.Rows) {
                    insert_query = "INSERT into class_fees(Class_id,admission,security_deposit,monthly,annual_charges,sync,exam_charges,card_charges,late_fee) " +
                        "VALUES('"+row["ClassID"].ToString() +"','0','0','0','0','0','0','0','0');";
                    fun.ExecuteQuery(insert_query);
                }
            }

            table = fun.FetchDataTable(sql);
            gridFeeSetting.DataSource = table;
            gridView1.BestFitColumns();
            var col = gridView1.Columns["ID"];
            col.OptionsColumn.ReadOnly = true;
            var col2 = gridView1.Columns["ClassID"];
            col2.OptionsColumn.ReadOnly = true;
            this.gridView1.Columns[0].Visible = false;
            RepositoryItemComboBox riComboC = new RepositoryItemComboBox();
            allClass.Clear();
            allClass = fun.GetAllClassisSession(fun.GetDefaultSessionName());
            foreach (var allclass in allClass)
                riComboC.Items.Add(allclass.Name);
            gridView1.Columns["Class"].ColumnEdit = riComboC;
            if (Main_FD.SelectedSession != fun.GetDefaultSessionName())
            {
                gridView1.OptionsBehavior.Editable = false;
            }
            else
            {
                gridView1.OptionsBehavior.Editable = true;
            }
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            string query = "UPDATE class_fees set class_id='" + row["ClassID"] + "',admission='" + row["AdmissionFee"] + "',security_deposit='" + row["SecurityDeposit"] + "'," +
                "monthly='" + row["MonthlyFee"] + "' ,annual_charges='" + row["Annual_Charges"] + "',sync='0',exam_charges='" + row["ExamCharges"] + "',card_charges='" + row["CardCharges"] + "',late_fee = '" + row["Late_Fee"] + "' WHERE fees_id='" + row["ID"] + "';";
            fun.ExecuteQuery(query);
            FillGridFeeSetting();
        }
        
        private void FeeSetting_Enter(object sender, EventArgs e)
        {
            FillGridFeeSetting();
        }
    }
}
