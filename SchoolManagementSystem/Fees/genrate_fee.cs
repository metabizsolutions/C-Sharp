using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem.Fees
{
    public partial class genrate_fee : UserControl
    {
        private static genrate_fee _instance;

        public static genrate_fee instance
        {
            get
            {
                if (_instance == null)
                    _instance = new genrate_fee();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        public genrate_fee()
        {
            InitializeComponent();
            LoadOtherCat();
        }
        private void LoadOtherCat()
        {
            CmboOtherFees.Properties.Items.Clear();
            DataTable DTOther = fun.GetQueryTable("SELECT fee_cat_id,fees_title FROM fees_category");
            foreach (DataRow row in DTOther.Rows)
            {
                CmboOtherFees.Properties.Items.Add(row["fee_cat_id"] + " > " + row["fees_title"]);
            }
        }
        private void btnIG_Click(object sender, EventArgs e)
        {
            if (txtMonthGI.Text == "" || txtYearGI.Text == "")
            {
                MessageBox.Show("Select Month & Year", "Info");
                return;
            }
            if (txtStudent.Text == "")
            {
                MessageBox.Show("Student is not selected", "Info");
                return;
            }
            fun.loaderform(() =>
            {
                var m = "";

                var allmonth = txtMonthGI.Text.Split(',');
                var number_month = allmonth.Length;
                var start_month = allmonth.First().Trim();
                var end_month = allmonth.Last().Trim();
                //get integer value for month
                foreach (MonthOfYear mon in Enum.GetValues(typeof(MonthOfYear)))
                {
                    if (start_month == mon.ToString())
                    {
                        start_month = mon.GetHashCode().ToString();
                    }

                    if (end_month == mon.ToString())
                    {
                        end_month = mon.GetHashCode().ToString();
                    }
                }
                int alreadyExit = 0;
                int pDue = 0;
                // allStudents = new ObservableCollection<AllStudent>();
                var p = "";// var sms = "and student.fees_sms = '1'";
                           // allStudents = fun.GetAllStudentIsSession(fun.GetDefaultSessionName(), sms);
                var allStd = txtStudent.Text.Split(',');
                string otherfees_ids = "";
                DataTable TableOther;
                if (txtStudent.Text != "")
                {
                    string[] OtherFeesArray = CmboOtherFees.Text.Split(',');

                    int count = 0;
                    foreach (string str in OtherFeesArray)
                    {
                        OtherFeesArray[count] = str.Split('>')[0].Trim();
                        count++;
                    }
                    otherfees_ids = String.Join(",", OtherFeesArray);
                }
                //m = Convert.ToInt32(m) < 10 ? "0" + m : m;
                string date = txtYearGI.Text + "-" + start_month + "-1"; // need to be fix becuse of error comming when fee genrated in 3/31/2021 then date value comes 
                string end_date = (Convert.ToDateTime(date).AddMonths(1).AddDays(-1)).ToString("yyyy-MM-dd");//txtYearGI.Text + "-" + end_month + "-" + DateTime.Now.AddDays(-1).Day;
                DateTime Date = Convert.ToDateTime(date);
                var due_date = Convert.ToDateTime(DueDate.Value.ToString()).ToString("yyyy-MM-dd");
                string format_date = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
                String student_id, InsertQuery, InsertCat;
                int invoice_id;
                foreach (var std in allStd)
                {
                    student_id = std.Split('>')[0].Trim();
                    var query = "SELECT * FROM invoice WHERE student_id = '" + std.Split('>')[0].Trim() + "' and MONTH(date) = '" + start_month + "' and year(date) = '" + txtYearGI.Text + "'; ";
                    DataTable dt_inv =fun.FetchDataTable(query);
                    if (dt_inv.Rows.Count > 0)
                    {
                        alreadyExit = 1;
                    }


                    pDue = 0;
                    var classID = fun.GetClassIDisSession(std.Split('>')[2].Trim(), fun.GetDefaultSessionName());
                    int stdid = int.Parse(std.Split('>')[0].Trim());
                    int MFee = 0, AFee = 0, SDFee = 0, ACFee = 0, ECFee = 0, SCFee = 0;
                    int MFeeC = 0, OtherFees = 0;
                    int total = 0; var des = "";
                    int total_concession = 0;
                    Date = Date.AddMonths(-1);

                    if (checkTF.Checked == true)
                    {
                        MFee = fun.GetClassMonthlyFee(classID);
                        MFee = MFee * number_month;
                        des += ";Tution Fee:" + MFee;

                        MFeeC = fun.GetStudentFeeConcession(stdid);
                        total_concession = MFeeC * number_month;
                    }
                    if (checkAF.Checked == true)
                    {
                        total_concession += fun.GetStudentAFeeConcession(stdid);
                        AFee = fun.GetClassAdmissionFee(classID);
                        des += " ;Admission Fee:" + AFee;
                        MFee += AFee;
                    }
                    if (checkSF.Checked == true)
                    {
                        total_concession += fun.GetStudentSDFeeConcession(stdid);
                        SDFee = fun.GetClassSecurityDeposit(classID);
                        des += " ;Security Deposit:" + SDFee;
                        MFee += SDFee;
                    }
                    if (checkACF.Checked == true)
                    {
                        total_concession += fun.GetStudentACFeeConcession(stdid);
                        ACFee = fun.GetClassAnnualCharges(classID);
                        des += " ;Annual Charges:" + ACFee;
                        MFee += ACFee;
                    }
                    if (checkECF.Checked == true)
                    {
                        total_concession += fun.GetStudentECFeeConcession(stdid);
                        ECFee = fun.GetClassExamCharges(classID);
                        des += " ;Exam Charges:" + ECFee;
                        MFee += ECFee;
                    }
                    if (checkSCF.Checked == true)
                    {
                        SCFee = fun.GetClassCardCharges(classID);
                        des += " ;Student Card Charges:" + SCFee;
                        MFee += SCFee;
                    }
                    if (total_concession != 0)
                    {
                        des += ";Fee Concession:" + total_concession;
                    }
                    if (otherfees_ids != "")
                    {
                        query = "SELECT fcs.fees_cat_id,fcs.fees_val AS amount,fees_category.fees_title AS title " +
                                " FROM fees_category_student AS fcs " +
                                " INNER JOIN fees_category ON fees_category.fee_cat_id = fcs.fees_cat_id " +
                                " WHERE fcs.student_id = {0} AND fcs.fees_cat_id IN({1})";
                        query = string.Format(query, stdid, otherfees_ids);
                        TableOther = fun.GetQueryTable(query);
                        if (TableOther.Rows.Count > 0)
                        {
                            foreach (DataRow row in TableOther.Rows)
                            {
                                OtherFees += Convert.ToInt32(row["amount"]);
                                des += ";" + row["title"] + ":" + row["amount"];
                            }
                        }
                    }
                    if (txtOtherCharges.Text != "")
                    {
                        var res = txtOtherCharges.Text.Split(';');
                        foreach (var val in res)
                        {
                            if (val != "")
                            {
                                des += " ;" + val.Split(':')[0] + ":" + val.Split(':')[1];
                                OtherFees += int.Parse(val.Split(':')[1]);
                            }
                        }
                    }


                    if (alreadyExit == 0)
                    {
                        var query1 = "SELECT * FROM invoice WHERE student_id = '" + std.Split('>')[0].Trim() + "' AND forward = 0 ORDER BY invoice_id DESC LIMIT 1";
                        DataTable std_inv_dt = fun.FetchDataTable(query1);
                            foreach (DataRow reader2 in std_inv_dt.Rows)
                            {
                                var due = reader2["due"].ToString();
                                p = (due != null) ? due : "";
                                //update forward 
                                query = "UPDATE invoice SET forward = 1 WHERE invoice_id = '{0}'";
                                query = string.Format(query, reader2["invoice_id"].ToString());
                                fun.ExecuteQuery(query);

                            }
                            pDue = Convert.ToInt32(p);

                        if (pDue > 0)
                        {
                            des += " ;Arrears:" + pDue;
                        }
                        total += pDue + MFee + OtherFees - total_concession;
                        des = des.TrimStart(';');

                        InsertQuery = "INSERT into invoice(Student_id,class_id,section_id,title,description,other_fee,previous_fee,current_fee,fee_concession,amount,amount_paid,due,creation_timestamp,status,date,sync,to_date,number_month,due_date) VALUES('" + student_id + "','" + txtClass.Text.Split('>')[0].Trim() + "',(SELECT section_id FROM student WHERE student_id = '" + std.Split('>')[0].Trim() + "'),'Monthly Fee','" + des + "','" + OtherFees + "','" + pDue + "','" + MFee + "','" + total_concession + "','" + total + "','0','" + total + "','" + fun.GetTimestamp(Convert.ToDateTime(date)) + "','Unpaid','" + format_date + "','0','" + Convert.ToDateTime(end_date).ToString("yyyy-MM-dd") + "','" + number_month + "','" + due_date + "')";
                        invoice_id = fun.ExecuteInsert(InsertQuery);
                        if (otherfees_ids != "")
                        {
                            InsertCat = "INSERT INTO fees_cat_amount(invoice_id,amount,fee_cat_id) " +
                                                " SELECT '{0}' AS invoice_id, fees_val, fees_cat_id FROM fees_category_student WHERE fees_cat_id IN({1}) AND student_id = '{2}'";
                            InsertCat = String.Format(InsertCat, invoice_id, otherfees_ids, student_id);
                            fun.ExecuteQuery(InsertCat);
                        }

                    }
                    else
                    {
                        var query1 = "SELECT *," +
                        " (SELECT due FROM invoice WHERE student_id = '" + std.Split('>')[0].Trim() + "' AND forward = 1 ORDER BY invoice_id DESC LIMIT 1) as pre_due " +
                        " FROM invoice WHERE student_id = '" + std.Split('>')[0].Trim() + "' AND forward = 0 ORDER BY invoice_id DESC LIMIT 1";
                        DataTable std_inv_dt = fun.FetchDataTable(query1);
                        foreach (DataRow reader2 in std_inv_dt.Rows)
                        {
                                var due = reader2["pre_due"].ToString();
                                p = (!string.IsNullOrEmpty(due)) ? due : "0";
                                //update forward 
                                //query = "UPDATE invoice SET forward = 1 WHERE invoice_id = '{0}'";
                                //query = string.Format(query, reader2["invoice_id"].ToString());
                                //fun.ExecuteQuery(query);

                                pDue = Convert.ToInt32(p);
                                if (pDue > 0)
                                {
                                    des += " ;Arrears:" + pDue;
                                }
                                total += pDue + MFee + OtherFees - total_concession;
                                des = des.TrimStart(';');
                                InsertQuery = "update invoice set Student_id='" + student_id + "',class_id='" + txtClass.Text.Split('>')[0].Trim() + "',section_id=(SELECT section_id FROM student WHERE student_id = '" + std.Split('>')[0].Trim() + "'),title='Monthly Fee' " +
                                ",description='" + des + "',other_fee='" + OtherFees + "',previous_fee='" + pDue + "',current_fee='" + MFee + "',fee_concession='" + total_concession + "' " +
                                ",amount='" + total + "',due=(" + total + "-amount_paid),creation_timestamp='" + fun.GetTimestamp(Convert.ToDateTime(date)) + "',status='Unpaid'" +
                                ",date='" + format_date + "',to_date='" + Convert.ToDateTime(end_date).ToString("yyyy-MM-dd") + "',number_month='" + number_month + "',due_date='" + due_date + "' " +
                                " where invoice_id = '" + reader2["invoice_id"] + "'";
                                fun.ExecuteInsert(InsertQuery);
                                invoice_id = Convert.ToInt32(reader2["invoice_id"]);
                                if (otherfees_ids != "")
                                {
                                    InsertCat = "UPDATE fees_cat_amount AS fct," +
                                    " (SELECT fees_val, fees_cat_id FROM fees_category_student WHERE fees_cat_id IN({1}) AND student_id = '{2}') AS `src` " +
                                    " SET  fct.amount = src.fees_val,fct.fee_cat_id = src.fees_cat_id WHERE fct.invoice_id = '{0}'";
                                    InsertCat = String.Format(InsertCat, invoice_id, otherfees_ids, student_id);
                                    fun.ExecuteQuery(InsertCat);
                                }
                            }
                    }
                    alreadyExit = 0;

                }
                //Forward Passout students 
                var section_list = txtSection.Text;
                var info = section_list.Split(',');
                int section_id = 0;
                foreach (var val in info)
                {
                    section_id = int.Parse(val.Split('>')[0].Trim());
                    String forward_query = "UPDATE invoice SET forward = '1' " +
                        "WHERE forward != 1 AND student_id IN(SELECT IFNULL(student_id,0) AS student_id " +
                        "FROM student WHERE passout = 1 AND section_id  = '{0}')";
                    forward_query = string.Format(forward_query, section_id);
                    fun.ExecuteQuery(forward_query);
                }


                //merge common parents invoice 
                if (ChkMergeParent.Checked == true)
                {
                    String query_student = " SELECT * FROM ( " +
                                    " SELECT GROUP_CONCAT(student_id ORDER BY student_id ASC) AS student_id, GROUP_CONCAT(parent.parent_id) AS parent_id, parent.phone,COUNT(student_id) AS cnt " +
                                    " FROM student " +
                                    " INNER JOIN parent ON student.parent_id = parent.parent_id " +
                                    " GROUP BY parent.phone " +
                                    " ) AS tbl WHERE cnt > 1 ";
                    DataTable table = fun.GetQueryTable(query_student);
                    String student_ids, query_invoice, first_student, invoice_detail, query_update, query_delete;
                    DataTable table_invoice;
                    int invoice_amount, previous_fee, current_fee, fee_concession, other_fee;
                    foreach (DataRow row in table.Rows)
                    {
                        invoice_amount = 0;
                        previous_fee = 0;
                        current_fee = 0;
                        fee_concession = 0;
                        other_fee = 0;
                        invoice_detail = "";
                        student_ids = row["student_id"].ToString();
                        query_invoice = "SELECT invoice_id, invoice.previous_fee, invoice.current_fee, invoice.fee_concession,invoice.other_fee ,invoice.amount,student.name,student.student_id FROM invoice " +
                                        " INNER JOIN student ON student.student_id = invoice.student_id " +
                                        " WHERE invoice.student_id IN(" + student_ids + ") AND date = '" + format_date + "'";
                        table_invoice = fun.GetQueryTable(query_invoice);

                        if (table_invoice.Rows.Count > 0)
                        {
                            first_student = table_invoice.Rows[0]["student_id"].ToString();
                            foreach (DataRow invoice_row in table_invoice.Rows)
                            {
                                invoice_amount += Convert.ToInt32(invoice_row["amount"].ToString());
                                previous_fee += Convert.ToInt32(invoice_row["previous_fee"].ToString());
                                current_fee += Convert.ToInt32(invoice_row["current_fee"].ToString());
                                fee_concession += Convert.ToInt32(invoice_row["fee_concession"].ToString());
                                other_fee += Convert.ToInt32(invoice_row["other_fee"].ToString());
                                invoice_detail += invoice_row["name"].ToString() + ":" + invoice_row["amount"].ToString() + ";";
                            }
                            query_update = "UPDATE invoice SET payment_details = 'Merge Student Ids:" + invoice_detail + "', previous_fee ='" + previous_fee + "',current_fee = '" + current_fee + "',fee_concession='" + fee_concession + "',other_fee='" + other_fee + "', amount = '" + invoice_amount + "',due='" + invoice_amount + "',description = '" + invoice_detail + "' WHERE student_id = '" + first_student + "' AND DATE = '" + format_date + "'";
                            fun.ExecuteQuery(query_update);
                            query_delete = "DELETE FROM invoice WHERE student_id != '" + first_student + "' AND DATE = '" + format_date + "' AND student_id IN(" + student_ids + ")";
                            fun.ExecuteQuery(query_delete);
                        }
                    }
                }
                txtMonthGI.Text = "";
                txtYearGI.Text = "";
                txtClass.Text = "";
                txtSection.Text = "";
                txtStudent.Text = "";
                DueDate.Value = DateTime.Now;
            });
        }
    }
}
