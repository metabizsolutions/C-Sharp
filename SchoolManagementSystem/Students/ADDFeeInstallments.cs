using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections.ObjectModel;
using DevExpress.XtraEditors.Repository;
using System.Collections;
using MySql.Data.MySqlClient;

namespace SchoolManagementSystem.Students
{
    public partial class ADDFeeInstallments : DevExpress.XtraEditors.XtraForm
    {
        DataRow dr = null;
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        ObservableCollection<AllStudent> allStudents;
        CommonFunctions fun = new CommonFunctions();
        object invoiceID = null;
        GridCheckMarksSelection gridCheckMarksSA;
        public ADDFeeInstallments()
        {
            InitializeComponent();
            fun.selectall_Controls(this);
            loadfunctions();
            GridStudentList.Properties.View.OptionsSelection.MultiSelect = true;
            GridStudentList.CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(gridLookUpEdit1_CustomDisplayText);
            GridStudentList.Properties.PopulateViewColumns();

            gridCheckMarksSA = new GridCheckMarksSelection(GridStudentList.Properties);
            gridCheckMarksSA.SelectionChanged += new GridCheckMarksSelection.SelectionChangedEventHandler(gridCheckMarks_SelectionChanged);
            GridStudentList.Properties.Tag = gridCheckMarksSA;
        }
        public void loadfunctions()
        {
            allClass.Clear();
            allClass = fun.GetAllClassisSession(fun.GetDefaultSessionName());
            txtClass.Properties.Items.Clear();
            foreach (var allclass in allClass)
                txtClass.Properties.Items.Add(allclass.Salary + " > " + allclass.Name);

            string sms = "SELECT student_id as id, student.name,student.roll as RollNo FROM student left join class on (class.class_id=student.class_id)  where student.section_id='0' AND student.passout != 1 and not student_id in (SELECT student_id FROM invoice WHERE class_id = '0')";
            DataTable Students = fun.FetchDataTable(sms);//GetAllStudentsisSection(int.Parse(val.Split('>')[0].Trim()), sms);

            GridStudentList.Text = "";
            GridStudentList.Properties.DataSource = Students;
            GridStudentList.Properties.DisplayMember = "name";
            GridStudentList.Properties.ValueMember = "id";
        }
        void gridCheckMarks_SelectionChanged(object sender, EventArgs e)
        {
            if (ActiveControl is GridLookUpEdit)
            {
                StringBuilder sb = new StringBuilder();
                foreach (DataRowView rv in (sender as GridCheckMarksSelection).Selection)
                {
                    if (sb.ToString().Length > 0) { sb.Append(", "); }
                    sb.Append(rv["name"].ToString());
                    //if (sb.ToString().Length == 1)
                    //{
                    string query = "SELECT 'Installment-' as Installment, `amount`, `Paid`, `Remaining`, if(`status`=1,'Paid','unpaid') as status,`due` as duedate FROM `fees_installment` WHERE " +
                        "`invoice_id` = (SELECT `invoice_id` FROM `change_class` WHERE `std_id` = '" + rv["id"] + "' order by id desc limit 1) order by `due`";
                    DataTable predata = fun.FetchDataTable(query);
                    int ins_count = 0;
                    int totalpaid = 0;
                    for (int i = 0; i < predata.Rows.Count; i++)
                    {
                        ins_count++;
                        predata.Rows[i]["Installment"] = ins_count;
                        totalpaid = totalpaid + Convert.ToInt32(predata.Rows[i]["Paid"]);
                    }
                    prev_stdgrid.DataSource = predata;
                    
                    
                    lblpaidamount.Text = totalpaid.ToString();
                    //}
                }
                (ActiveControl as GridLookUpEdit).Text = sb.ToString();
            }
        }

        void gridLookUpEdit1_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            GridCheckMarksSelection gridCheckMark = sender is GridLookUpEdit ? (sender as GridLookUpEdit).Properties.Tag as GridCheckMarksSelection : (sender as RepositoryItemGridLookUpEdit).Tag as GridCheckMarksSelection;
            if (gridCheckMark == null) return;
            foreach (DataRowView rv in gridCheckMark.Selection)
            {
                if (sb.ToString().Length > 0) { sb.Append(", "); }
                sb.Append(rv["name"].ToString());
            }
            e.DisplayText = sb.ToString();
        }


        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            txtSection.Properties.Items.Clear();
            var a = txtClass.Text;
            var info = a.Split(',');
            if (!string.IsNullOrEmpty(txtClass.Text))
            {
                allClass.Clear();
                allClass = fun.GetAllSectionisClass(int.Parse(txtClass.Text.Split('>')[0].Trim()));

                foreach (var allclass in allClass)
                    txtSection.Properties.Items.Add(allclass.Salary + " > " + allclass.Name);
                string query = "select annual_charges from class_fees where Class_id='" + txtClass.Text.Split('>')[0].Trim() + "'";
                object anuualfee = fun.Execute_Scaler_string(query);
                if (anuualfee != null)
                    txtAnualPakage.Text = anuualfee.ToString();
                else
                    MessageBox.Show("Please Set Annual Fee", "Fee Settings", MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
            }
        }
        private void txtSection_EditValueChanged(object sender, EventArgs e)
        {
            //txtStudent.Properties.Items.Clear();
            loadstudents();
        }
        void loadstudents()
        {
            var a = txtSection.Text;
            var info = a.Split(',');
            foreach (var val in info)
            {
                if (val != "")
                {
                    allClass.Clear();
                    //allStudents = new ObservableCollection<AllStudent>();
                    string sms = "SELECT student_id as id, student.name,student.roll as RollNo FROM student " +
                        " left join class on (class.class_id=student.class_id) " +
                        " where student.section_id='" + val.Split('>')[0].Trim() + "' " +
                        " AND student.passout != 1 and not student_id in (SELECT student_id FROM invoice WHERE class_id = '" + txtClass.Text.Split('>')[0].Trim() + "')";
                    DataTable Students = fun.FetchDataTable(sms);//GetAllStudentsisSection(int.Parse(val.Split('>')[0].Trim()), sms);

                    GridStudentList.Text = "";
                    GridStudentList.Properties.DataSource = Students;
                    GridStudentList.Properties.DisplayMember = "name";
                    GridStudentList.Properties.ValueMember = "id";
                    //foreach (var std in allStudents)
                    //    txtStudent.Properties.Items.Add(std.Name + " > " + std.Class);
                }
            }
        }
        int remainging = 0;
        ArrayList valueList;
        private void btnADD_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            MySqlTransaction tra = null;
            try
            {
                con.Open();
                tra = con.BeginTransaction();
                if (remainging <= 0)
                {
                    GridCheckMarksSelection gridCheckMark = GridStudentList.Properties.Tag as GridCheckMarksSelection;
                    valueList = gridCheckMark.Selection;
                    foreach (DataRowView rv in valueList)
                    {
                        var std = Convert.ToInt32(rv["id"]);
                        int alreadyExit = 0;
                        string query = "";
                        var c = fun.GetAllControls(panelinstallment, typeof(TextBox), "Ins");
                        query = "SELECT * FROM invoice WHERE student_id = '" + std + "' and class_id = '" + txtClass.Text.Split('>')[0].Trim() + "' ";
                        DataTable invoicedt = fun.FetchDataTable(query,con);
                        if (invoicedt.Rows.Count > 0)
                        {
                            alreadyExit = 1;
                        }
                        if (alreadyExit == 0)
                        {
                            query = "SELECT * FROM invoice WHERE student_id = '" + std + "' ORDER BY invoice_id DESC LIMIT 1";
                            DataTable pre_invoice = fun.FetchDataTable(query,con);
                            string previous = "0";
                            if (pre_invoice.Rows.Count > 0)
                                previous = pre_invoice.Rows[0]["due"].ToString();
                            query = "UPDATE invoice SET forward = 1 WHERE student_id = '" + std + "'";
                            fun.Execute_Insert(query,con,tra);
                            int totalfee = (Convert.ToInt32(previous) + Convert.ToInt32(string.IsNullOrEmpty(txtAnualPakage.Text) ? "0" : txtAnualPakage.Text));
                            //inserting invoice
                            query = "INSERT INTO invoice(student_id,class_id,section_id, title,description, previous_fee,current_fee, fee_concession,amount,amount_paid, due, creation_timestamp, `payment_method`,date,Status) VALUES" +
                            " (" + std + ",'" + txtClass.Text.Split('>')[0].Trim() + "',(SELECT section_id FROM student WHERE student_id = '" + std + "'),'Annual Fee','Tution Fee:0;Fee Concession:" + 0 + "','" + previous + "'," + txtAnualPakage.Text + "," + 0 + "," + totalfee + ",'0'," + totalfee + "," + fun.time() + ",'Installments','" + DateTime.Now.ToString("yyyy-MM-dd") + "','Unpaid')";
                            invoiceID = fun.Execute_Insert(query, con, tra);
                            // creating plus session fee
                            object session = fun.Execute_Scaler_string("SELECT `relation_id` FROM `student_fields_values` WHERE `student_id` =" + std + " and `field_id` = 9",con);
                            if (session == null)
                                query = "INSERT INTO `student_fields_values`(`student_id`, `field_id`, `value`) VALUES (" + std + ",9,'" + txtPlusSession.Text + "');";
                            else
                                query = "UPDATE `student_fields_values` SET value ='" + txtPlusSession.Text + "' WHERE `relation_id` = '" + session + "';";
                            fun.Execute_Insert(query, con, tra);
                            int installment = c.Count() + 1;
                            // Creating Board Fee
                            object partBoardFee = fun.Execute_Scaler_string("SELECT `relation_id` FROM `student_fields_values` WHERE `student_id` =" + std + " and `field_id` = 12",con);
                            if (partBoardFee == null)
                                query = "INSERT INTO `student_fields_values`(`student_id`, `field_id`, `value`) VALUES (" + std + ",12,'" + txtBoardFeePartI.Text + "');";
                            else
                                query = "UPDATE `student_fields_values` SET value='" + txtBoardFeePartI.Text + "' WHERE `relation_id` = '" + partBoardFee + "';";
                            fun.Execute_Insert(query, con, tra);
                            query = "update invoice set number_month = '" + installment + "' where invoice_id = '" + invoiceID + "'; ";

                            fun.Execute_Insert(query, con, tra);

                            string amount = "0";
                            string date = "";
                            if (Convert.ToInt32(lblpaidamount.Text) > 0)
                            {
                                amount = lblpaidamount.Text;
                                date = DateTime.Now.ToString("yyyy-MM-dd");
                                query = "INSERT INTO `fees_installment`(`invoice_id`, `amount`,Paid,Remaining, `due`, `status`) VALUES " +
                                        "(" + invoiceID + ",'" + amount + "','" + amount + "','0','" + Convert.ToDateTime(date).ToString("yyyy-MM-dd") + "',1);";
                                fun.Execute_Insert(query, con, tra);
                                query = "INSERT into payment(expense_category_id,title,payment_type,invoice_id,student_id,description,amount,dues,timestamp,date,user_id,sync,`transaction`, cashasset_id) VALUES('" + fun.GetExpenseID("Student Fee") + "','Monthly Fee','Income','" + invoiceID + "','" + std + "','Total:" + amount + "','" + amount + "','0','" + fun.GetTimestamp(DateTime.Now) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + Login.CurrentUserID + "','0','Session Fee', '1');";
                                fun.Execute_Insert(query, con, tra);
                                query += "UPDATE invoice set amount_paid='" + amount + "',due=amount-" + amount + ",status='Unpaid' WHERE invoice_id='" + invoiceID + "';";
                                fun.Execute_Insert(query, con, tra);
                            }
                            if (Convert.ToInt32(txtPlusSession.Text) > 0)
                            {
                                amount = txtPlusSession.Text;
                                date = DateTime.Now.ToString("yyyy-MM-dd");
                                query = "INSERT INTO `fees_installment`(title,`invoice_id`, `amount`,Paid,Remaining, `due`, `status`) VALUES " +
                                        "('Plus Session'," + invoiceID + ",'" + amount + "','" + amount + "','0','" + Convert.ToDateTime(date).ToString("yyyy-MM-dd") + "',1);";
                                fun.Execute_Insert(query, con, tra);
                                query = "INSERT into payment(expense_category_id,title,payment_type,invoice_id,student_id,description,amount,dues,timestamp,date,user_id,sync,`transaction`, cashasset_id) VALUES('" + fun.GetExpenseID("Student Fee") + "','Monthly Fee','Income','" + invoiceID + "','" + std + "','Total:" + amount + "','" + amount + "','0','" + fun.GetTimestamp(DateTime.Now) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + Login.CurrentUserID + "','0','Session Fee', '1');";
                                fun.Execute_Insert(query, con, tra);
                                query = "UPDATE invoice set amount_paid='" + amount + "',due=amount-" + amount + ",status='Unpaid' WHERE invoice_id='" + invoiceID + "';";
                                fun.Execute_Insert(query, con, tra);
                            }
                            int count = 0;
                            foreach (TextBox txt in c)
                            {
                                count++;
                                if (count > 1)
                                    previous = "0";
                                else if (string.IsNullOrEmpty(previous))
                                    previous = "0";
                                amount = string.IsNullOrEmpty(txt.Text) ? "0": txt.Text;
                                var d = fun.GetAllControls(panelinstallment, typeof(DateTimePicker), "dtp" + count).FirstOrDefault();
                                DateTimePicker pic = (DateTimePicker)d;
                                date = pic.Value.ToString("yyyy-MM-dd");
                                if (!string.IsNullOrEmpty(amount) && Convert.ToInt32(amount) > 0)
                                {
                                    int total_amt = Convert.ToInt32(previous) + Convert.ToInt32(amount);
                                    query = "INSERT INTO `fees_installment`(`invoice_id`,title,previous, `amount`,Remaining, `due`, `status`) VALUES " +
                                        "(" + invoiceID + ",'Installment_"+ count + "','" + previous + "','" + amount + "','" + total_amt + "','" + Convert.ToDateTime(date).ToString("yyyy-MM-dd") + "',0)";
                                    fun.Execute_Insert(query, con, tra);
                                }
                            }

                            if (Convert.ToInt32(txtBoardFeePartI.Text) > 0)
                            {
                                amount = txtBoardFeePartI.Text;
                                date = DateTime.Now.ToString("yyyy-MM-dd");
                                query = "INSERT INTO `fees_installment`(title,`invoice_id`, `amount`,Remaining, `due`, `status`) VALUES " +
                                    "('Board Fee'," + invoiceID + "," + amount + "," + amount + ",'" + Convert.ToDateTime(date).ToString("yyyy-MM-dd") + "',0)";

                                fun.Execute_Insert(query,con,tra);
                            }
                        }

                        //}
                    }
                    
                    MessageBox.Show("Installemnts Created Successfully");
                    tra.Commit();
                    empty();
                }
                else
                {
                    MessageBox.Show("Please fill installments properly");
                }
            }
            catch (Exception ex)
            {
                tra.Rollback();
                MessageBox.Show(ex.Message, "Installment Creation Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void empty()
        {
            txtPlusSession.Text = "0";
            txtBoardFeePartI.Text = "0";
            txtAnualPakage.Text = "0";
            panelinstallment.Controls.Clear();
            totalins = 0;
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            empty();
        }

        private void txtPlusSession_TextChanged(object sender, EventArgs e)
        {
            partIcollection();
        }

        void partIcollection()
        {
            int totalamout = 0;
            int totalinstallments = 0;

            var c = fun.GetAllControls(panelinstallment, typeof(TextBox), "Ins");
            totalinstallments = c.Count();

            remainging = Convert.ToInt32(string.IsNullOrEmpty(txtAnualPakage.Text) ? "0" : txtAnualPakage.Text);

            if (Convert.ToInt32(txtPlusSession.Text == "" ? "0" : txtPlusSession.Text) > 0)
                totalamout += Convert.ToInt32(txtPlusSession.Text);

            foreach (TextBox txt in c)
            {
                if (Convert.ToInt32(txt.Text == "" ? "0" : txt.Text) > 0)
                    totalamout += Convert.ToInt32(txt.Text);
            }

            if (Convert.ToInt32(txtBoardFeePartI.Text == "" ? "0" : txtBoardFeePartI.Text) > 0)
                totalamout += Convert.ToInt32(txtBoardFeePartI.Text);
            remainging = remainging - totalamout - Convert.ToInt32(lblpaidamount.Text);
            lblTotalAMountPartI.Text = remainging.ToString();
        }
        private void txtAnualPakage_TextChanged(object sender, EventArgs e)
        {
            remainging = Convert.ToInt32(string.IsNullOrEmpty(txtAnualPakage.Text) ? "0" : txtAnualPakage.Text);
            lblTotalAMountPartI.Text = remainging.ToString();
            partIcollection();

        }
        int totalins = 0;
        private void btncreateInstallment_Click(object sender, EventArgs e)
        {
            totalins++;
            if (totalins == 1)
                panelinstallment.Height = 30;
            else
                panelinstallment.Height += 30;
            Label lbl = new Label();
            lbl.AutoSize = false;
            lbl.Text = "Installment:" + totalins.ToString();
            lbl.Margin = new Padding(38, 6, 0, 0);
            lbl.BorderStyle = BorderStyle.Fixed3D;
            panelinstallment.Controls.Add(lbl);
            TextBox txt = new TextBox();
            panelinstallment.Controls.Add(txt);
            txt.TextChanged += txtPlusSession_TextChanged;
            txt.Size = new Size(100, 21);
            txt.Name = "Ins" + totalins;
            txt.Text = "0";
            DateTimePicker dtp = new DateTimePicker();
            panelinstallment.Controls.Add(dtp);
            dtp.Size = new Size(100, 21);
            dtp.Format = DateTimePickerFormat.Short;
            dtp.Name = "dtp" + totalins;
            panelinstallment.SetFlowBreak(dtp, true);
        }

        private void GridStudentList_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
