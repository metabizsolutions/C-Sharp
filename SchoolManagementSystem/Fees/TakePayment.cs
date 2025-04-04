using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Fees
{
    public partial class TakePayment : DevExpress.XtraEditors.XtraUserControl
    {
        private static TakePayment _instance;

        public static TakePayment instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TakePayment();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        ObservableCollection<Move> move = new ObservableCollection<Move>();
        DataRow FocusRow = null;
        DataTable feedetails;
        public TakePayment()
        {
            InitializeComponent();
            var query = " SELECT invoice_id as ID,student.student_id as StdID,student.name as Name,class.name as Class,section.name as Section,due as Due, " +
                " (SELECT COUNT(installment_id) FROM fees_installment WHERE fees_installment.invoice_id = invoice.invoice_id ) AS installment "+
                " FROM invoice " +
                " inner join student on student.student_id = invoice.student_id " +
                " inner join parent on student.parent_id = parent.parent_id " +
                " left join class on class.class_id=student.class_id  " +
                " left join section on section.section_id=student.section_id " +
                " where 1 = 1 AND forward = 0 ";
            DataTable dt_std = fun.FetchDataTable(query);
            lookupedit_std.Properties.DataSource = dt_std;
            lookupedit_std.Properties.DisplayMember = "Name";
            lookupedit_std.Properties.ValueMember = "ID";
        }
        IncomeManagement IM;
        public TakePayment(DataRow row)
        {
            InitializeComponent();
            lookupedit_std.Visible = false;
            FocusRow = row;
            lbl_std_id.Text = FocusRow["StdID"].ToString();
            txtstd_name.Text = FocusRow["Name"].ToString();
            txtstd_roll.Text = FocusRow["roll"].ToString();
            txtstd_class.Text = FocusRow["class"].ToString();
            txtstd_section.Text = FocusRow["section"].ToString();
            lbl_parent_name.Text = FocusRow["Father"].ToString();
            lbl_parent_phone.Text = FocusRow["ParentPhone"].ToString();
            txtstd_total.Text = FocusRow["Amount"].ToString();
            txtstd_paid.Text = FocusRow["Paid"].ToString();
            txtstd_remaining.Text = FocusRow["Due"].ToString();

            


        }
        private void TakePayment_Load(object sender, System.EventArgs e)
        {
            loadcurrent();
        }
        void load_fee_details()
        {
            string query = "SELECT `previous`,`amount`, `Paid`, `Remaining`, `due` FROM `fees_installment` WHERE `invoice_id` ='" + FocusRow["ID"] + "'";
            feedetails = fun.FetchDataTable(query);
            gridFees_Details.DataSource = feedetails;

            query = "select * from invoice where invoice_id = '" + FocusRow["ID"] + "'";
            DataTable inv_dt = fun.FetchDataTable(query);
            if(inv_dt.Rows.Count > 0)
            {
                txtdue.Text = inv_dt.Rows[0]["due"].ToString();
                txtpaid.Text = inv_dt.Rows[0]["amount_paid"].ToString();
            }
        }
        void loadcurrent()
        {
            txtstd_name.Text = FocusRow["Name"].ToString();
            txtstd_roll.Text = FocusRow["roll"].ToString();
            txtstd_class.Text = FocusRow["class"].ToString();
            txtstd_section.Text = FocusRow["section"].ToString();
            txtstd_total.Text = FocusRow["Amount"].ToString();
            txtstd_paid.Text = FocusRow["Paid"].ToString();
            txtstd_remaining.Text = FocusRow["Due"].ToString();
            txtTotal.Text = FocusRow["Amount"].ToString();
            txtpaid.Text = FocusRow["Paid"].ToString();
            txtdue.Text = FocusRow["Due"].ToString();
            date = Convert.ToDateTime(string.IsNullOrEmpty(FocusRow["Date"].ToString()) ? DateTime.Now.ToString("yyyy-MM-dd") : FocusRow["Date"].ToString());
            picBoxStudent.Image = fun.get_image(@"\Images\Students\", lbl_std_id.Text + "_std", false, FocusRow["Gender"].ToString());
            IncomeManagement a = new IncomeManagement();
            move.Clear();
            move = a.Getcurrent();
            foreach (var m in move)
            {
                txtTotal.Text = m.Total;
                txtpaid.Text = m.Paid;
                txtdue.Text = m.Due;
                aa = m.Std;
                no = m.No;
                date = Convert.ToDateTime(string.IsNullOrEmpty(m.Date) ? DateTime.Now.ToString("yyyy-MM-dd") : m.Date);
            }
            txtPayment.Text = "Cash";
            string query = "select title, installment_id as ID,`Remaining` as amount,if(status = 1 , 'paid','unpaid') as `status`,due from fees_installment where status = 0 AND invoice_id = '"+ FocusRow["ID"].ToString() + "' order by installment_id asc";
            DataTable table = fun.FetchDataTable(query);
            if(Convert.ToInt32(txtdue.Text) > 0 && table.Rows.Count <= 0)
            {
                query = "select title, installment_id as ID,`Remaining` as amount,if(status = 1 , 'paid','unpaid') as `status`,due from fees_installment where status = 1 AND invoice_id = '" + FocusRow["ID"].ToString() + "' order by installment_id desc limit 1";
                table = fun.FetchDataTable(query);
            }
            if (table.Rows.Count > 0)
            {
                txtPaymentDrop.Text = "";
                txtPaymentDrop.Properties.DataSource = table;
                txtPaymentDrop.Properties.DisplayMember = "amount";
                txtPaymentDrop.Properties.ValueMember = "ID";

                txtPaymentDrop.EditValue = table.Rows[0]["ID"];

                txtPaymentDrop.Visible = true;
                lbl_ins_due.Visible = true;
                lbl_ins.Visible = true;
                txtins_due.Visible = true;
                
            }
            else
            {
                txtPaymentDrop.Visible = false;
                lbl_ins_due.Visible = false;
                lbl_ins.Visible = false;
                txtins_due.Visible = false;
            }


            table = fun.cashasset_list();
            if (table.Rows.Count > 0)
            {
                SpinnerCashAsset.Properties.DataSource = table;
                SpinnerCashAsset.Properties.DisplayMember = "title";
                SpinnerCashAsset.Properties.ValueMember = "cashasset_id";
            }

            txtDate.Text = DateTime.Now.ToShortDateString();
            FillGridHistoryTakePayment();
            load_fee_details();
            txtPayment.Text = "0";
        }

        string aa;
        string no;
        DateTime date;
        void FillGridHistoryTakePayment()
        {
            DateTime month = DateTime.Now;
            var a = aa;
            String query = "SELECT amount as Amount,cash_assets.title as Method,date as Date, transaction " +
                            " FROM payment LEFT JOIN cash_assets on cash_assets.cashasset_id = payment.cashasset_id " +
                            " WHERE  invoice_id = '" + FocusRow["ID"] + "'";
            DataTable table = fun.FetchDataTable(query);
            gridTakePayment.DataSource = table;
            gridView1.BestFitColumns();
        }

        private void btnTPAdd_Click(object sender, EventArgs e)
        {
            if (txtPayment.Text == "")
            {
                MessageBox.Show("Please Insert Payment", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtPayment.Text != "" || txtPaymentDrop.Text != "")
            {
                DialogResult a = MessageBox.Show("Are your sure you want to this payment", "Printing", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (a == DialogResult.Yes)
                {
                    txtPayment.Enabled = false;
                    int payment = 0;
                    int installment_id = 0;
                    var installAmount = txtPaymentDrop.Text.Trim();
                    if (installAmount != "" && installAmount != "[EditValue is null]")
                    {
                        installment_id = Convert.ToInt32(txtPaymentDrop.EditValue);
                        payment = Convert.ToInt32(txtPayment.Text);
                    }
                    else
                    {
                        payment = Convert.ToInt32(txtPayment.Text);
                    }
                    var cashasset_id = (SpinnerCashAsset.Text == "" || SpinnerCashAsset.Text == "[EditValue is null]") ? 0 : SpinnerCashAsset.EditValue;
                    string d = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd");// date.Year + "-" + date.Month + "-" + DateTime.Now.Day;
                    MySqlConnection con = new MySqlConnection(Login.constring);
                    con.Open();
                    try
                    {
                        var query = "";
                        if (installment_id > 0)
                        {
                            query = "SELECT installment_id FROM `fees_installment` WHERE invoice_id ='" + no + "' AND installment_id > '" + installment_id + "' ORDER BY installment_id ASC LIMIT 1";
                            object next_ins_id = fun.Execute_Scaler_string(query);
                            query = "UPDATE fees_installment SET `status` = 1,Paid=Paid+" + payment + ",Remaining=Remaining-" + payment + " WHERE installment_id = '" + installment_id + "'";
                            fun.ExecuteQuery(query);
                            if (next_ins_id != null)
                            {
                                query = "SELECT * FROM `fees_installment` WHERE installment_id = '" + installment_id + "'";
                                DataTable dt_ins = fun.FetchDataTable(query);
                                query = "UPDATE fees_installment SET `previous` = '" + dt_ins.Rows[0]["Remaining"] + "' WHERE installment_id = '" + next_ins_id + "';"; // adding previous
                                query += "UPDATE fees_installment SET Remaining=((amount+previous)-discount)-Paid WHERE installment_id = '" + next_ins_id + "';";// calculating Remaining
                                fun.ExecuteQuery(query);

                            }
                            query = "INSERT into payment(expense_category_id,title,payment_type,invoice_id,student_id,description,total,amount,dues,timestamp,date,user_id,sync,`transaction`, cashasset_id) VALUES " +
                                "('" + fun.GetExpenseID("Student Fee") + "','Monthly Fee','Income','" + no + "','" + aa + "',concat('(" + aa + ") ','" + txtstd_name.Text + "'),'" + txtdue.Text + "','" + payment + "','" + (Convert.ToInt32(txtdue.Text) - payment) + "','" + fun.time() + "','" + d + "','" + Login.CurrentUserID + "','0','" + txtTransaction.Text + "', '" + cashasset_id + "');";
                            query += "UPDATE invoice set amount_paid=(select sum(Paid) from fees_installment where invoice_id = '" + no + "') WHERE invoice_id='" + no + "';";
                            query += "UPDATE invoice set due=amount-amount_paid WHERE invoice_id='" + no + "';" +
                                        " update invoice set status = if(due<=0,'paid','unpaid') where `invoice_id` = '" + no + "';";
                            fun.ExecuteQuery(query);
                        }
                        else
                        {
                            int due = (Convert.ToInt32(txtdue.Text) - payment);
                            
                            query = "INSERT into payment(expense_category_id,title,payment_type,invoice_id,student_id,description,total,amount,dues,timestamp,date,user_id,sync,`transaction`, cashasset_id) VALUES " +
                                                        "('" + fun.GetExpenseID("Student Fee") + "','Monthly Fee','Income','" + no + "','" + aa + "',concat('(" + aa + ") ','" + txtstd_name.Text + "'),'" + txtdue.Text + "'," +
                                                        "'" + payment + "','" + due + "','" + fun.time() + "','" + d + "','" + Login.CurrentUserID + "','0','" + txtTransaction.Text + "', " +
                                                        "'" + cashasset_id + "');";
                            query += "UPDATE invoice set amount_paid=amount_paid+"+ payment + " WHERE invoice_id='" + no + "';";
                            fun.ExecuteQuery(query);
                            
                        }
                        update_connection_prev(Convert.ToInt32(no));

                        DataTable std_fee_info_dt = fun.invoice_grid_data(Convert.ToInt32(no));
                        FocusRow = std_fee_info_dt.Rows[0];

                        FillGridHistoryTakePayment();
                        load_fee_details();
                        fun.SendSMSFeePaid(aa.ToString(), payment.ToString(), txtTotal.Text);
                    }
                    catch (MySqlException ex)
                    {
                        SystemSounds.Hand.Play();
                        MessageBox.Show(ex.Message, "Error");
                        return;
                    }
                    con.Close();
                    
                    txtPayment.Text = "";
                    txtPayment.Enabled = false;
                    txtPaymentDrop.Enabled = false;
                    txtPayment.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Enter Payment", "Info");
                return;
            }
        }
        void update_connection_prev(int invoice_id)
        {
            string query = "update `invoice` set amount = (ifnull(previous_fee,0)+ifnull(current_fee,0)+ifnull(other_fee,0))-ifnull(fee_concession,0) where `invoice_id` = '" + invoice_id + "';";
            query += "update `invoice` set `due` = amount-amount_paid where `invoice_id` = '" + invoice_id + "';" +
                     " update invoice set status = if(due<=0,'paid','unpaid') where `invoice_id` = '" + invoice_id + "';";
            fun.Execute_Insert(query);
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            fun.print_receipt_cash(FocusRow);
        }

        private void txtPaymentDrop_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPaymentDrop.EditValue != null && txtPaymentDrop.EditValue.ToString() != "")
                txtins_due.Text = Convert.ToInt32(txtPaymentDrop.Text).ToString();
        }

        private void txtPayment_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void txtPayment_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPaymentDrop.EditValue != null && txtPayment.Text != "")
            {
                txtins_due.Text = txtPaymentDrop.Text.ToString();
                int ins_due = (Convert.ToInt32(txtPaymentDrop.Text) - Convert.ToInt32(txtPayment.Text));
                txtins_due.Text = ins_due.ToString();
            }
            else if (txtPayment.Text == "" && txtPaymentDrop.Visible) {
                txtins_due.Text = (Convert.ToInt32(txtPaymentDrop.Text)).ToString();
            }
        }

        private void lookupedit_std_EditValueChanged(object sender, EventArgs e)
        {
            string query = "SELECT invoice_id as ID,student.student_id as StdID,student.name as Name,title as Title,IFNULL(invoice.other_fee,0) AS Other_Fee,previous_fee as Previous,current_fee as Current,invoice.fee_concession as Concession,amount as Amount,amount_paid as Paid,due as Due,status as Status,date as Date ,class.name as Class,section.name as Section,remarks as Remarks,IFNULL(invoice.payment_details, invoice.description) AS Detail,due_date,number_month AS month, " +
                " (SELECT COUNT(installment_id) FROM fees_installment WHERE fees_installment.invoice_id = invoice.invoice_id ) AS installment,student.roll, " +
                " parent.name AS Father,parent.phone as ParentPhone " +
                " FROM invoice " +
                " inner join student on student.student_id = invoice.student_id " +
                " inner join parent on student.parent_id = parent.parent_id " +
                " left join class on class.class_id=student.class_id  " +
                " left join section on section.section_id=student.section_id " +
                " where 1 = 1 AND forward = 0 and invoice_id = '" + lookupedit_std.EditValue+"' ";
             DataTable table = fun.FetchDataTable(query);
            FocusRow = table.Rows[0];
            loadcurrent();
        }

        private void btn_print_fee_details_Click(object sender, EventArgs e)
        {
            fun.ShowGridPreview(gridFees_Details);
        }
    }
}
