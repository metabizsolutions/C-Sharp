using System;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;
using System.Data;

namespace SchoolManagementSystem.Fees
{
    public partial class Unpaid_SMS : UserControl
    {
        private static Unpaid_SMS _instance;

        public static Unpaid_SMS instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Unpaid_SMS();
                return _instance;
            }
        }
        public Unpaid_SMS()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnSend.Enabled = false;
            if (add)
                btnSend.Enabled = true;
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        public void loadfunctions()
        {
            gridControl1.DataSource = null;

            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";
        }
        CommonFunctions fun = new CommonFunctions();
        ObservableCollection<AllStudent> allStudents;
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtStudent.EditValue == null || string.IsNullOrEmpty(txtStudent.EditValue.ToString()))
            {
                MessageBox.Show("Please select date before send sms", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataTable stds_dt = new DataTable();
            fun.loaderform(() =>
            {
                var allStd = txtStudent.EditValue.ToString().Split(',');
                string query = "SELECT inv.student_id,std.Name,std.roll,cls.name as Class,inv.description,inv.amount,inv.amount_paid,inv.due,inv.due_date FROM student as std " +
                                    " join class as cls on cls.class_id = std.class_id " +
                                    " join invoice as inv on inv.student_id = std.student_id " +
                                    " WHERE std.student_id in (" + txtStudent.EditValue.ToString() + ") and inv.forward = 0 ";
                stds_dt = fun.FetchDataTable(query);
                foreach(DataRow dr in stds_dt.Rows)
                {
                    var p = "";
                        p = dr["due"].ToString() == "" ? "0" : dr["due"].ToString();
                        if (p != "0")
                            fun.SendSMSFeeUNPaid(Convert.ToInt32(dr["student_id"]), p, dr["due_date"].ToString(), dr["description"].ToString());
                }
                gridControl1.DataSource = stds_dt;
                gridView1.Columns["Class"].Group();
                gridView1.ExpandAllGroups();
            });
            MessageBox.Show("Task Completed Successfully. Count: " + stds_dt.Rows.Count, "Info");
        }

        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            if (txtClass.EditValue != null && !string.IsNullOrEmpty(txtClass.EditValue.ToString()))
            {
                txtSection.Properties.DataSource = fun.GetAllSection_dt(txtClass.EditValue.ToString());
                txtSection.Properties.DisplayMember = "name";
                txtSection.Properties.ValueMember = "section_id";
            }
        }

        private void txtSection_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSection.Text != null && !string.IsNullOrEmpty(txtSection.Text))
            {
                var a = txtSection.EditValue.ToString();
                load_students();
            }
        }

        void load_students()
        {
            if (txtSection.EditValue != null && !string.IsNullOrEmpty(txtSection.EditValue.ToString()))
            {
                string selection = "";
                if (cbo_smstype.Text == "Half Paid")
                    selection = " and amount_paid > 0 having (amount-amount_paid) >0";
                else if (cbo_smstype.Text == "UnPaid")
                    selection = " and amount_paid = 0 ";
                else
                    selection = "  and inv.due > 0 ";

                string query = "SELECT inv.student_id,std.Name,std.roll,cls.name as Class,inv.description,inv.amount,inv.amount_paid,inv.due FROM student as std " +
                                " join class as cls on cls.class_id = std.class_id " +
                                " join invoice as inv on inv.student_id = std.student_id " +
                                " WHERE std.section_id in (" + txtSection.EditValue + ") and MONTH(date) = '" + dtp_month.Value.Month + "' and year(date) = '" + dtp_month.Value.Year + "' AND std.fees_sms = '1' and amount > 0 " + selection + " ";
                DataTable dtstd = fun.FetchDataTable(query);
                txtStudent.Properties.DataSource = dtstd;
                txtStudent.Properties.DisplayMember = "Name";
                txtStudent.Properties.ValueMember = "student_id";
            }
        }
    }
}
