using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace SchoolManagementSystem.Principal
{
    public partial class VisitorAppointment : DevExpress.XtraEditors.XtraForm
    {
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        ObservableCollection<StudentInfo> studentinfo = new ObservableCollection<StudentInfo>();
        CommonFunctions fun = new CommonFunctions();
        string appType;
        public VisitorAppointment()
        {
            InitializeComponent();
            fun.DateFormat(txtDate);
            load(); fillgridView();
            radioGroup1.SelectedIndex = 0;
            FillComboIssue();
        }
        void load()
        {
            string str = "";
            studentinfo = fun.GetAllStudentsWId_S_C_S(str);
            txtSID.Text = "0";
            txtSID.Properties.DataSource = studentinfo;
            txtSID.Properties.DisplayMember = "ID";
            txtSID.Properties.ValueMember = "ID";
        }
        void FillComboIssue()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdP = new MySqlCommand("SELECT subject FROM waiting_list group by subject; ", con);
            MySqlDataReader reader1 = cmdP.ExecuteReader();
            if (reader1.HasRows)
                while (reader1.Read())
                    txtSubject.Properties.Items.Add(reader1["subject"].ToString().Trim());
            con.Close();
        }
        private void txtSID_EditValueChanged(object sender, System.EventArgs e)
        {
            var stdInfo = fun.GetStudentInfo(txtSID.Text.ToString());
            if (stdInfo != "")
            {
                var std = stdInfo.Split('>');
                txtstudent.Text = std[13];
                txtRoll.Text = std[3];
                txtclass.Text = std[4];
                txtsection.Text = std[5];
                txtVisitorName.Text = std[0];
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            var query = "INSERT into waiting_list(visitor_name,subject,description,date,student_id,`check`,type) VALUES('" + txtVisitorName.Text + "','" + txtSubject.Text + "','" + txtDes.Text + "','" + txtDate.DateTime.ToShortDateString() + "','" + (txtSID.Text == "" ? "0" : txtSID.Text) + "','0','" + appType + "');";
            MySqlCommand cmd1 = new MySqlCommand(query, con);
            cmd1.ExecuteNonQuery();
            con.Close();
            Notification n = new Notification
            {
                header = "Mr. " + txtVisitorName.Text + " waiting outside!",
                body = "For " + txtSubject.Text + " " + txtDes.Text
            };
            PDashboard.notifi.Enqueue(n);

            this.Close();
        }

        private void radioGroup1_EditValueChanged(object sender, System.EventArgs e)
        {

            appType = radioGroup1.EditValue.ToString();
        }
        RepositoryItemSearchLookUpEdit riSearch;
        RepositoryItemComboBox riComboC;

        void fillgridView()
        {
            var query = "SELECT id,visitor_name as Visitor,subject as Issue,description as Description,date as Date,student_id as Student,`check` as Status,`type` as Type FROM waiting_list;";
            DataTable table = SqlFunctions.SqlExecuteDataAdapter(query);
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = table;
            var col = gridView1.Columns["id"];
            col.OptionsColumn.ReadOnly = true;
            col.Visible = false;
            riSearch = new RepositoryItemSearchLookUpEdit();
            string str = "";
            studentinfo = fun.GetAllStudentsWId_S_C_S(str);

            riSearch.DataSource = studentinfo;
            riSearch.ValueMember = "ID";
            riSearch.DisplayMember = "Name";
            gridView1.Columns["Student"].ColumnEdit = riSearch;

            var col0 = gridView1.Columns["Date"];
            col0.DisplayFormat.FormatType = FormatType.DateTime;
            col0.DisplayFormat.FormatString = "M/d/yyyy";

            riComboC = new RepositoryItemComboBox();
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdP = new MySqlCommand("SELECT subject FROM waiting_list group by subject; ", con);
            MySqlDataReader reader1 = cmdP.ExecuteReader();
            if (reader1.HasRows)
                while (reader1.Read())
                    riComboC.Items.Add(reader1["subject"].ToString().Trim());
            con.Close();
            gridView1.Columns["Issue"].ColumnEdit = riComboC;

            var col1 = gridView1.Columns["Type"];
            col1.Group();
            gridView1.ExpandAllGroups();


        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            var query = "UPDATE `waiting_list` SET `visitor_name`='" + row[1] + "', `subject`='" + row[2] + "', `description`='" + row[3] + "', `date`='" + row[4] + "', `student_id`='" + row[5] + "', `check`='" + (row[6].ToString() == "True" ? "1" : "0") + "', `type`='" + row[7] + "' WHERE `id`='" + row[0] + "';";
            SqlFunctions.SqlExecuteNonQuery(query);
        }
    }
}