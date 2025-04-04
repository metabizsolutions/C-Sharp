using System;
using System.Collections.ObjectModel;

namespace SchoolManagementSystem.Principal
{
    public partial class Appointments : DevExpress.XtraEditors.XtraForm
    {
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        ObservableCollection<StudentInfo> studentinfo = new ObservableCollection<StudentInfo>();
        CommonFunctions fun = new CommonFunctions();
        public Appointments()
        {
            InitializeComponent();
            fun.DateFormat(txtDateS);
            load();
        }
        void load()
        {
            txtType.Properties.Items.Clear();
            txtType.Properties.Items.Add("Urgent");
            txtType.Properties.Items.Add("Short Term");
            txtType.Properties.Items.Add("Long Term");
            if (PDashboard.edit == 1)
                if (PDashboard.RowID != 0)
                {
                    txtsubject.Text = PDashboard.subject;
                    txtAssigned.Text = PDashboard.assigned;
                    txtDes.Text = PDashboard.des;
                    txtType.Text = PDashboard.type;
                    var status = (PDashboard.status == "1") ? checkStatus.Checked = true : checkStatus.Checked = false;
                    txtDateS.DateTime = PDashboard.dateS;
                    txtDateE.DateTime = PDashboard.dateE;
                }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            var result = "0";
            var type = txtType.Text;
            if (type == "Urgent")
                result = "1";
            else if (type == "Short Term")
                result = "2";
            else if (type == "Long Term")
                result = "3";
            var status = (checkStatus.Checked == true) ? "1" : "0";
            var query = "";
            if (PDashboard.edit == 0)
                query = "INSERT into appointments(Subject, Description, CustomField1, ResourceId, StartDate, EndDate, Status) VALUES('" + txtsubject.Text + "', '" + txtDes.Text + "', '" + txtAssigned.Text + "', '" + int.Parse(result) + "', '" + Convert.ToDateTime(txtDateS.Text).ToString("yyyy-MM-dd HH:MM") + "', '" + Convert.ToDateTime(txtDateE.Text == "" ? DateTime.Now.ToString() : txtDateE.Text).ToString("yyyy-MM-dd HH:MM") + "', '" + status + "'); ";
            else
                query = "UPDATE appointments set StartDate='" + Convert.ToDateTime(txtDateS.Text).ToString("yyyy-MM-dd HH:MM") + "',EndDate='" + Convert.ToDateTime(txtDateE.Text).ToString("yyyy-MM-dd HH:MM") + "',Subject='" + txtsubject.Text + "',Description='" + txtDes.Text + "',ResourceId='" + result + "',CustomField1='" + txtAssigned.Text + "',Status='" + status + "' WHERE ID='" + PDashboard.RowID + "';";
            SqlFunctions.SqlExecuteNonQuery(query);
            this.Close();
        }
    }
}