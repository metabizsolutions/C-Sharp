using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraReports.UI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolManagementSystem.Fees
{
    public partial class SalaryManagement : DevExpress.XtraEditors.XtraUserControl
    {
        private static SalaryManagement _instance;

        public static SalaryManagement instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SalaryManagement();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions CommonFunctions = new CommonFunctions();
        object objMonth, objYear, objMonthF, objYearF, objReport;
        int[] cat_ids;
        string[] cat_title;
        public SalaryManagement()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            barButGSalary.Enabled = false;
            if (add)
            {
                barButGSalary.Enabled = true;
            }
            gridView1.OptionsBehavior.Editable = false;
            if (Edit)
            {
                gridView1.OptionsBehavior.Editable = true;
            }
            //bool Delete = CommonFunctions.isAllow("Delete", "salary_management");
            //if (Delete)
            //    btnDelete.Enabled = true;
        }
        public void loadfunctions()
        {
            RetxtFMonth.Items.Clear();
            RetxtMonth.Items.Clear();
            foreach (MonthOfYear item in Enum.GetValues(typeof(MonthOfYear)))
            {
                var S = item.GetHashCode();
                if (DateTime.Now.Month >= item.GetHashCode())
                {
                    RetxtFMonth.Items.Add(item);
                    RetxtMonth.Items.Add(item);
                }
                else
                    break;

            }
            RetxtMonth.EditValueChanged += RetxtMonth_EditValueChanged;
            RetxtYear.EditValueChanged += RetxtYear_EditValueChanged;
            RetxtFMonth.EditValueChanged += RetxtFMonth_EditValueChanged;
            RetxtFYear.EditValueChanged += RetxtFYear_EditValueChanged;
            RetxtReports.EditValueChanged += RetxtReports_EditValueChanged;

            DataTable table = CommonFunctions.cashasset_list();
            if (table.Rows.Count > 0)
            {
                SpinnerCashAsset.DataSource = table;
                SpinnerCashAsset.DisplayMember = "title";
                SpinnerCashAsset.ValueMember = "cashasset_id";
            }
        }
        private void RetxtReports_EditValueChanged(object sender, EventArgs e)
        {
            objReport = (sender as ComboBoxEdit).EditValue;
        }
        private void RetxtFYear_EditValueChanged(object sender, EventArgs e)
        {
            objYearF = (sender as ComboBoxEdit).EditValue;
        }
        private void RetxtFMonth_EditValueChanged(object sender, EventArgs e)
        {
            objMonthF = (sender as ComboBoxEdit).EditValue;
        }
        private void RetxtYear_EditValueChanged(object sender, EventArgs e)
        {
            objYear = (sender as ComboBoxEdit).EditValue;

        }
        private void RetxtMonth_EditValueChanged(object sender, EventArgs e)
        {
            objMonth = (sender as ComboBoxEdit).EditValue;

        }

        static DataTable table;
        string Title = "";
        DataTable table2;
        private void barButGSalary_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (objMonth.ToString() == "" && objYear.ToString() == "")
            {
                MessageBox.Show("Select Month & Year", "Info");
                return;
            }

            int alreadyExit = 0;

            allClass = CommonFunctions.GetAllTeacher();
            string insert_query;
            int salary_id;
            string check_query = "SELECT * FROM salary_category";
            DataTable table = CommonFunctions.GetQueryTable(check_query);
            Boolean check_cat_flag = (table.Rows.Count) > 0 ? true : false;
            DateTime Date = CommonFunctions.GetDate(objYear.ToString(), objMonth.ToString());
            DateTime PreDate = Date.AddMonths(-1);
            string date = Date.ToString("yyyy-MM-dd");
            DateTime from_date = Convert.ToDateTime(DateFrom.EditValue);//.ToString("yyyy-MM-dd");
            DateTime to_date = Convert.ToDateTime(DateTo.EditValue);//.ToString("yyyy-MM-dd");

            int teacher_id;
            foreach (var allclass in allClass)
            {
                teacher_id = Convert.ToInt32(allclass.teacher_id);
                MySqlConnection con = new MySqlConnection(Login.constring);
                con.Open();
                var query = "SELECT * FROM salary WHERE teacher_id='" + teacher_id + "' and MONTH(date)='" + Date.Month + "' and year(date)='" + Date.Year + "';";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    alreadyExit = 1;
                }
                con.Close();
                int previous = 0;
                if (alreadyExit == 0)
                {

                    con.Open();
                    query = "SELECT * FROM salary WHERE teacher_id='" + teacher_id + "' and MONTH(date)='" + PreDate.Month + "' and year(date)='" + PreDate.Year + "';";
                    MySqlCommand cmd2 = new MySqlCommand(query, con);
                    MySqlDataReader reader2 = cmd2.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            previous = Convert.ToInt32(reader2["due"]);
                            query = "UPDATE salary SET forward = 1 WHERE salary_id = '" + reader2["salary_id"] + "'";
                            CommonFunctions.ExecuteQuery(query);
                        }
                    }
                    con.Close();
                    int addition = 0;
                    string cat_value;
                    string insert_cat_query = "";
                    var s = (allclass.Salary.ToString() == "") ? "0" : allclass.Salary.ToString();
                    int salary = Convert.ToInt32(s);
                    String[] insert_array = null;
                    // int M_Ded = SC / 100 * 5;
                    if (check_cat_flag)
                    {
                        check_query = "SELECT salary_cat_id,salary_val AS amount FROM salary_category_teacher WHERE teacher_id = '{0}'";
                        check_query = string.Format(check_query, teacher_id);
                        table = CommonFunctions.GetQueryTable(check_query);
                        insert_array = new String[table.Rows.Count];
                        if (table.Rows.Count > 0)
                        {
                            int count = 0;
                            int outint;
                            foreach (DataRow row in table.Rows)
                            {
                                cat_value = row["amount"].ToString();
                                if (cat_value.Contains("GS")) //Grand Salary
                                {
                                    cat_value = cat_value.Replace("GS", "");
                                    cat_value = (salary * Convert.ToDouble(cat_value)).ToString();
                                }
                                else if (cat_value.Contains("AC")) //Absent Count
                                {
                                    int leave_allowed = Convert.ToInt32(CommonFunctions.GetSettings("leave_allowed"));

                                    IDictionary<string, int> res_array = CommonFunctions.attendance_summary(teacher_id, from_date, to_date);
                                    int absent_count = Convert.ToInt32(res_array["absent"]);
                                    int sunday_count = Convert.ToInt32(res_array["sunday"]);
                                    int workingdays_count = Convert.ToInt32(res_array["working"]);
                                    if (absent_count < workingdays_count)
                                        absent_count = absent_count > leave_allowed ? absent_count - leave_allowed : 0;
                                    else
                                        absent_count = absent_count + sunday_count;

                                        cat_value = cat_value.Replace("AC", "");
                                    if (cat_value.Trim() == "-") //If no value then calculate from Salary day income
                                    {
                                        int days = Convert.ToInt32(to_date.Day);
                                        cat_value = (-Convert.ToDouble(salary) / days).ToString();

                                    }

                                    cat_value = (Convert.ToInt32(absent_count * Convert.ToDouble(cat_value))).ToString();

                                }
                                else if (cat_value.Contains("LM")) //Late Minute
                                {
                                    cat_value = cat_value.Replace("LM", "");
                                    IDictionary<string, int> res_array = CommonFunctions.attendance_summary(teacher_id, from_date, to_date);
                                    cat_value = (Convert.ToInt32(res_array["late_min"]) * (Convert.ToInt32(cat_value))).ToString();
                                }
                                else if (cat_value.Contains("EL")) //Extra Lecture, Missed Lecture
                                {
                                    IDictionary<string, int> res_array = CommonFunctions.lecture_summary(teacher_id, from_date, to_date);
                                    cat_value = cat_value.Replace("EL", "");
                                    cat_value = (Convert.ToInt32(res_array["extra_lecture"]) * (Convert.ToInt32(cat_value))).ToString();
                                }
                                else if (cat_value.Contains("ML")) //Missed Lecture
                                {
                                    IDictionary<string, int> res_array = CommonFunctions.lecture_summary(teacher_id, from_date, to_date);
                                    cat_value = cat_value.Replace("ML", "");
                                    cat_value = (Convert.ToInt32(res_array["missed_lecture"]) * (Convert.ToInt32(cat_value))).ToString();
                                }
                                else if (cat_value.Contains("TL")) //Missed Lecture
                                {
                                    IDictionary<string, int> res_array = CommonFunctions.lecture_summary(teacher_id, from_date, to_date);
                                    cat_value = cat_value.Replace("TL", "");
                                    cat_value = (Convert.ToInt32(res_array["total_lecture"]) * (Convert.ToInt32(cat_value))).ToString();
                                }


                                if (!int.TryParse(cat_value, out outint))
                                {
                                    outint = 0;
                                }

                                if (cat_value.Contains("TS")) //Total Salary
                                {
                                    //cat_value = cat_value.Replace("TS", "");
                                    insert_array[count++] = cat_value + "," + row["salary_cat_id"];
                                }
                                else
                                {
                                    addition += outint;
                                    insert_array[count++] = "('{0}','" + outint + "', '" + row["salary_cat_id"] + "')";
                                }


                            }

                        }
                    }
                    int total_salary = salary + addition;
                    if (insert_array != null)
                    {
                        for (int key = 0; key < insert_array.Length; ++key)
                        {
                            cat_value = insert_array[key];
                            String cat_id;
                            if (cat_value.Contains("TS")) //Grand Salary
                            {
                                cat_id = cat_value.Split(new Char[] { ',' })[1];
                                cat_value = cat_value.Split(new Char[] { ',' })[0];
                                cat_value = cat_value.Replace("TS", "");
                                cat_value = Convert.ToInt32(total_salary * Convert.ToDouble(cat_value)).ToString();
                                addition += Convert.ToInt32(cat_value);
                                insert_array[key] = "('{0}','" + cat_value + "', '" + cat_id + "')";
                            }
                        }
                    }
                    int due = salary + previous + addition;
                    insert_query = "INSERT into salary(teacher_id,previous,advance,current,paid,due,status,date,sync,addition,from_date,to_date) " +
                                            " VALUES('{0}','{1}','0','{2}','0','{3}','unpaid','{4}','0','{5}','{6}','{7}')";
                    insert_query = string.Format(insert_query, teacher_id, previous, salary, due, date, addition, from_date.ToString("yyyy-MM-dd"), to_date.ToString("yyyy-MM-dd"));
                    salary_id = CommonFunctions.ExecuteInsert(insert_query);
                    if (insert_array != null)
                    {
                        insert_cat_query = "INSERT INTO salary_cat_amount(salary_id,amount,salary_cat_id) " +
                                            " VALUES " + String.Join(",", insert_array);
                        insert_cat_query = string.Format(insert_cat_query, salary_id);
                        CommonFunctions.ExecuteQuery(insert_cat_query);
                    }
                }


                alreadyExit = 0;

            }

            FillGridSalaryManege();
        }
        private void FillGridSalaryManege()
        {

            DateTime month = CommonFunctions.GetDate(objYear.ToString(), objMonth.ToString());
            string query;
            string extra_col = "";

            //check if salary is forard
            string check_query = "SELECT * FROM salary WHERE MONTH(date)='" + month.Month + "' and Year(date)='" + month.Year + "'";
            DataTable checkTable = CommonFunctions.FetchDataTable(check_query);
            Boolean IsEditable = checkTable.Rows.Count > 0 ? (Convert.ToInt32(checkTable.Rows[0]["forward"]) == 1 ? false : true) : true;

            string query_cat = "SELECT salary_cat_id,salary_title FROM salary_category ORDER BY salary_cat_id ASC ";
            DataTable table_cat = CommonFunctions.GetQueryTable(query_cat);
            cat_ids = new int[table_cat.Rows.Count];
            int count = 0;
            int salary_cat_id;
            foreach (DataRow row in table_cat.Rows)
            {
                salary_cat_id = Convert.ToInt32(row["salary_cat_id"].ToString());
                cat_ids[count] = salary_cat_id;
                extra_col += ",(SELECT amount FROM salary_cat_amount WHERE salary_cat_id = '" + salary_cat_id + "' AND salary_id = salarys.ID) AS '" + row["salary_title"].ToString() + "'";
                count++;
            }

            query = "SELECT * " + extra_col + " FROM salarys where MONTH(date)='" + month.Month + "' and Year(date)='" + month.Year + "';";
            DataTable table = CommonFunctions.FetchDataTable(query);
            gridView1.Columns.Clear();
            gridSalary.DataSource = null;


            gridSalary.DataSource = CommonFunctions.AutoNumberedTable(table);
            gridView1.BestFitColumns();
            if (table.Rows.Count > 0)
            {
                var col = gridView1.Columns["ID"]; col.OptionsColumn.ReadOnly = true;
                var col2 = gridView1.Columns["Name"]; col2.OptionsColumn.ReadOnly = true;
                var col3 = gridView1.Columns["Staff_Type"]; col3.OptionsColumn.ReadOnly = true;
                var col4 = gridView1.Columns["Designation"]; col4.OptionsColumn.ReadOnly = true; col4.Visible = true;
                var col5 = gridView1.Columns["Subj"]; col5.OptionsColumn.ReadOnly = true; col5.Visible = true;
                var col6 = gridView1.Columns["A/C #"]; col6.OptionsColumn.ReadOnly = true;
                var col20 = gridView1.Columns["addition"]; col20.OptionsColumn.ReadOnly = true;
                var col26 = gridView1.Columns["Total"]; col26.OptionsColumn.ReadOnly = true;
                var col30 = gridView1.Columns["Status"]; col30.OptionsColumn.ReadOnly = true;
                var col31 = gridView1.Columns["Date"]; col31.OptionsColumn.ReadOnly = true;
                var col32 = gridView1.Columns["Due."]; col32.OptionsColumn.ReadOnly = true;
                gridView1.Columns["Paid"].OptionsColumn.ReadOnly = true;
                if (!IsEditable)
                {
                    gridView1.Columns["Salary"].OptionsColumn.ReadOnly = true;
                    gridView1.Columns["Advance"].OptionsColumn.ReadOnly = true;
                    if (table_cat.Rows.Count > 0)
                    {
                        foreach (DataRow row in table_cat.Rows)
                        {
                            gridView1.Columns[row["salary_title"].ToString()].OptionsColumn.ReadOnly = true;
                            gridView1.Columns[row["salary_title"].ToString()].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, row["salary_title"].ToString(), "{0}"));
                        }
                    }
                }
                gridView1.OptionsView.ShowFooter = true;
                gridView1.Columns["Arrears"].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Arrears", "{0}"));
                gridView1.Columns["Salary"].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Salary", "{0}"));
                gridView1.Columns["addition"].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "addition", "{0}"));
                gridView1.Columns["Total"].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Total", "{0}"));
                gridView1.Columns["Advance"].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Advance", "{0}"));
                gridView1.Columns["Due."].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Due.", "{0}"));
                gridView1.Columns["Paid"].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Paid", "{0}"));
            }
        }

        private void barButPaySlip_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow data = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            PaySlipNew report = new PaySlipNew();
            if (data != null)
            {
                ObservableCollection<PreviousFee> PfeeCard = new ObservableCollection<PreviousFee>();
                MySqlConnection con = new MySqlConnection(Login.constring);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM teacher where name='" + data[2].ToString() + "';", con);
                MySqlDataReader reader3 = cmd.ExecuteReader();
                if (reader3.HasRows)
                {
                    reader3.Read();

                    string teacher_id = (reader3["teacher_id"].ToString() == "") ? "-" : reader3["teacher_id"].ToString();
                    report.labEID.Text = teacher_id;
                    report.LabGender.Text = (reader3["sex"].ToString() == "") ? "-" : reader3["sex"].ToString();
                    report.LabFName.Text = (reader3["FName"].ToString() == "") ? "-" : reader3["FName"].ToString();
                    report.LabPhone.Text = (reader3["phone"].ToString() == "") ? "-" : reader3["phone"].ToString();
                    report.LabEAddress.Text = (reader3["address"].ToString() == "") ? "-" : reader3["address"].ToString();
                    report.LabStafftype.Text = (reader3["staff_type"].ToString() == "") ? "-" : reader3["staff_type"].ToString();
                    report.labAC.Text = (reader3["bank_account"].ToString() == "") ? "-" : reader3["bank_account"].ToString();
                }
                con.Close();


                string salary = data["Salary"].ToString();
                string previous = data["Arrears"].ToString();
                string advance = data["Advance"].ToString();
                string total = data["Total"].ToString();
                string paid = data["Paid"].ToString();
                string remaining = data["Due."].ToString();


                PfeeCard.Add(GetPaymentType(salary, "Salary"));
                PfeeCard.Add(GetPaymentType(previous, "Previous"));
                PfeeCard.Add(GetPaymentType(advance, "Advance"));
                //PfeeCard.Add(GetPaymentType(advance, "Remaining"));
                string query_other = "SELECT sc.salary_title,sca.amount  " +
                                           " FROM salary_cat_amount AS sca " +
                                           " LEFT JOIN  salary_category AS sc ON sc.salary_cat_id = sca.salary_cat_id " +
                                           " WHERE sca.salary_id = '{0}'";
                query_other = string.Format(query_other, data[1]);
                DataTable table_other = CommonFunctions.GetQueryTable(query_other);
                foreach (DataRow row in table_other.Rows)
                {
                    PfeeCard.Add(GetPaymentType(row["amount"].ToString(), row["salary_title"].ToString()));
                }
                PfeeCard.Add(GetPaymentType(total, "Total"));
                if (Convert.ToInt32(paid) > 0)
                {
                    PfeeCard.Add(GetPaymentType(paid, "Paid"));
                    PfeeCard.Add(GetPaymentType(remaining, "Remaining"));
                }


                Image logo = CommonFunctions.Base64ToImage(Login.Logo);
                var school = CommonFunctions.GetSettings("system_title");
                report.PicIogoBox.Image = logo;
                report.LabTitle.Text = school;
                report.LabAddress.Text = CommonFunctions.GetSettings("address");
                report.LabTel.Text = CommonFunctions.GetSettings("phone");
                report.labName.Text = data[2].ToString();
                report.LabDesi.Text = data[4].ToString();
                report.labSalaryMonth.Text = DateTime.Parse(data["date"].ToString()).ToString("M-yyyy");


                report.GridControl = PfeeCard;

                report.LabGrossSalary.Text = salary;
                report.LabNetSalary.Text = total;

                report.labDate.Text = DateTime.Now.ToString("dd-M-yyyy");
                String query = "SELECT * FROM salary WHERE salary_id = '{0}'";
                query = String.Format(query, data["ID"].ToString());
                DataTable table = CommonFunctions.FetchDataTable(query);
                if (table.Rows.Count > 0)
                {
                    report.LabDateFrom.Text = DateTime.Parse(table.Rows[0]["from_date"].ToString()).ToString("dd-M-yyyy");
                    report.LabDateTo.Text = DateTime.Parse(table.Rows[0]["to_date"].ToString()).ToString("dd-M-yyyy");
                    report.LabWorkingDate.Text = (DateTime.Parse(table.Rows[0]["to_date"].ToString()) - DateTime.Parse(table.Rows[0]["from_date"].ToString())).TotalDays.ToString();
                }


                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);

            }

        }

        private PreviousFee GetPaymentType(String amount, String title)
        {
            PreviousFee result = null;
            int val = Convert.ToInt32(amount);
            if (val > -1)
            {
                result = new PreviousFee { Amount = amount, Due = "", Method = title };
            }
            else
            {
                result = new PreviousFee { Amount = "", Due = (val * -1).ToString(), Method = title };
            }
            return result;
        }


        private void BtnFind_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FillGridSalaryManege();
        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            print();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            refresh_teacher_list();
        }
        private void refresh_teacher_list()
        {
            CmboTeacherName.Items.Clear();
            string query = "SELECT teacher.teacher_id, teacher.name,salary.salary_id,salary.due " +
                            " FROM salary " +
                            " INNER JOIN teacher ON teacher.teacher_id = salary.teacher_id " +
                            " WHERE salary.forward = 0 AND salary.due > 0";
            DataTable table = CommonFunctions.FetchDataTable(query);
            foreach (DataRow row in table.Rows)
            {
                CmboTeacherName.Items.Add(row["salary_id"] + ">" + row["name"] + ":" + row["due"]);
            }
        }
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtDatepicker.EditValue == null || CmboTeacher.EditValue == "" || SpinnerCash.EditValue == null)
            {
                MessageBox.Show("All Fields are required!");
                return;
            }
            var allTeacher = CmboTeacher.EditValue.ToString().Split(',');
            string date = Convert.ToDateTime(txtDatepicker.EditValue).ToString("yyyy-MM-dd");
            var cashasset_id = SpinnerCash.EditValue.ToString();
            string salary_id = "0";
            string query = "";
            string amount = "0";
            string paid = "0";
            int count = 0;
            string status = "unpaid";
            int remaining = 0;
            foreach (var teacher in allTeacher)
            {
                salary_id = teacher.Split('>').GetValue(0).ToString();
                amount = teacher.Split(':').GetValue(1).ToString();
                paid = allTeacher.Length == 1 ? txtpaid.EditValue.ToString() : amount;

                status = (Convert.ToInt32(amount) > Convert.ToInt32(paid)) ? "unpaid" : "paid";
                query = "INSERT INTO payment SET description=concat('(',{5},') ','"+teacher.Split('>',':').GetValue(1).ToString()+"'),expense_category_id = '" + CommonFunctions.GetExpenseID("Salary") + "' ,title = 'Salary Payment',payment_type = 'Expense',cashasset_id = '{0}', " +
                        " total = '{1}',amount = '{1}',dues = '0',`timestamp` = '{2}',`date` = '{3}',user_id = '{4}',student_id='{5}'; ";
                query = string.Format(query, cashasset_id, amount, CommonFunctions.time(), date, Login.CurrentUserID, salary_id);

                query += "UPDATE salary SET paid = paid+'{0}' ,due = due-'{0}', status = '{1}' WHERE salary_id = '{2}'";
                query = string.Format(query, paid, status, salary_id);
                CommonFunctions.ExecuteQuery(query);
                count++;
            }
            MessageBox.Show("Success total " + count);
            refresh_teacher_list();
            FillGridSalaryManege();
        }

        private void CmboTeacher_EditValueChanged(object sender, EventArgs e)
        {
            if (CmboTeacher.EditValue != "")
            {
                if (CmboTeacher.EditValue.ToString().Split(',').Length == 1)
                {
                    var amount = (CmboTeacher.EditValue.ToString().Split(':')).GetValue(1);
                    txtpaid.EditValue = amount;
                    txtpaid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    return;
                };

            }
            txtpaid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }



        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            var query = "";
            string salary_id = row[1].ToString();
            int salary = Convert.ToInt32(row[7]);
            int previous = Convert.ToInt32(row[8]);
            int addition = 0;
            if (cat_ids.Length > 0)
            {
                int count = 16;
                int amount = 0;
                foreach (int cat_id in cat_ids)
                {
                    amount = Convert.ToInt32(row[count]);
                    addition += amount;
                    query = "UPDATE salary_cat_amount SET amount = '{0}' WHERE salary_id = '{1}' AND salary_cat_id = '{2}'";
                    query = string.Format(query, amount, salary_id, cat_id);
                    CommonFunctions.ExecuteQuery(query);
                    count++;
                }
            }
            int advance = Convert.ToInt32(row[10]);
            int paid = Convert.ToInt32(row[12]);
            var due = salary + previous + addition - advance - paid;
            var status = (due <= 0) ? "paid" : "unpaid";
            query = "UPDATE salary set previous = '{0}',advance='{1}',addition='{2}',paid='{3}',due='{4}',status='{5}',current = '{6}' WHERE salary_id='{7}';";
            query = string.Format(query, previous, advance, addition, paid, due, status, salary, salary_id);
            CommonFunctions.ExecuteQuery(query);
            FillGridSalaryManege();

        }

        private void barButSelectReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {


                var col = gridView1.Columns["ID"];
                var col2 = gridView1.Columns["Name"];
                var col3 = gridView1.Columns["Staff_Type"];
                var col4 = gridView1.Columns["Designation"];
                var col5 = gridView1.Columns["Subj"];
                var col6 = gridView1.Columns["A/C #"];
                var col7 = gridView1.Columns["Salary"];
                var col14 = gridView1.Columns["Arrears"];
                var col15 = gridView1.Columns["addition"];
                var col26 = gridView1.Columns["Total"];
                var col27 = gridView1.Columns["Advance"];
                var col29 = gridView1.Columns["Paid"];
                var col30 = gridView1.Columns["Status"];
                var col31 = gridView1.Columns["Date"];
                var col32 = gridView1.Columns["Due."];
                var col33 = gridView1.Columns["Sr#"];
                col29.Summary.Clear();
                col27.Summary.Clear();
                col26.Summary.Clear();
                col7.Summary.Clear();
                col32.Summary.Clear();
                table2 = table;
                if (objReport.ToString() == "Bank Slip" && table != null)
                {

                    var s = table.AsEnumerable().Where(a => a.Field<string>("A/C #") != "");


                    gridSalary.DataSource = CommonFunctions.AutoNumberedTable(s.CopyToDataTable());
                    Title = "Account Holder Staff List";

                    col.Visible = false;
                    col2.Visible = true;
                    col3.Visible = false;
                    col4.Visible = true;
                    col5.Visible = true;
                    col6.Visible = true;
                    col7.Visible = true;
                    col14.Visible = false;
                    col15.Visible = false;
                    col26.Visible = true;
                    col27.Visible = true;
                    col29.Visible = false;
                    col30.Visible = false;
                    col31.Visible = false;
                    col32.Visible = false;
                    col33.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Salary", "{0}");
                    GridColumnSummaryItem item2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Arrears", "{0}");
                    GridColumnSummaryItem item4 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "addition.", "{0}");
                    GridColumnSummaryItem item5 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Total", "{0}");
                    GridColumnSummaryItem item6 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Advance", "{0}");
                    GridColumnSummaryItem item8 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Paid", "{0}");
                    GridColumnSummaryItem item3 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Due.", "{0}");
                    col29.Summary.Add(item8);
                    col27.Summary.Add(item6);
                    col26.Summary.Add(item5);
                    col7.Summary.Add(item1);
                    col32.Summary.Add(item3);
                    gridView1.BestFitColumns();
                }
                else
                    if (objReport.ToString() == "Permanent Teaching Staff" && table != null)
                {
                    var s = table.AsEnumerable().Where(a => a.Field<string>("Staff_Type") == "Teaching");
                    //gridSalary.DataSource = null;
                    //                gridView1.Columns.Clear();
                    gridSalary.DataSource = CommonFunctions.AutoNumberedTable(s.CopyToDataTable());
                    Title = "Salary Statement Permanent Teaching Staff";
                    col.Visible = false;
                    col2.Visible = true;
                    col3.Visible = false;
                    col4.Visible = true;
                    col5.Visible = true;
                    col6.Visible = true;
                    col7.Visible = true;
                    col14.Visible = true;
                    col15.Visible = true;
                    col26.Visible = true;
                    col27.Visible = true;
                    col29.Visible = true;
                    col30.Visible = false;
                    col31.Visible = false;
                    col32.Visible = true;
                    col33.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Salary", "{0}");
                    GridColumnSummaryItem item2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Arrears", "{0}");
                    GridColumnSummaryItem item4 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "addition.", "{0}");
                    GridColumnSummaryItem item5 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Total", "{0}");
                    GridColumnSummaryItem item6 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Advance", "{0}");
                    GridColumnSummaryItem item8 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Paid", "{0}");
                    GridColumnSummaryItem item3 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Due.", "{0}");
                    col29.Summary.Add(item8);
                    col27.Summary.Add(item6);
                    col26.Summary.Add(item5);
                    col7.Summary.Add(item1);
                    col32.Summary.Add(item3);
                    gridView1.BestFitColumns();
                }
                else
                    if (objReport.ToString() == "Visiting Teaching Staff" && table != null)
                {
                    var s = table.AsEnumerable().Where(a => a.Field<string>("Staff_Type") == "Visiting");
                    gridSalary.DataSource = CommonFunctions.AutoNumberedTable(s.CopyToDataTable());

                    Title = "Salary Statement Visiting Teaching Staff";

                    col.Visible = false;
                    col2.Visible = true;
                    col3.Visible = false;
                    col4.Visible = true;
                    col5.Visible = true;
                    col6.Visible = true;
                    col7.Visible = true;

                    col14.Visible = false;
                    col15.Visible = false;
                    col26.Visible = true;
                    col27.Visible = true;
                    col29.Visible = true;
                    col30.Visible = false;
                    col31.Visible = false;
                    col32.Visible = true;
                    col33.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Salary", "{0}");
                    GridColumnSummaryItem item2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Arrears", "{0}");
                    GridColumnSummaryItem item4 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "addition.", "{0}");
                    GridColumnSummaryItem item5 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Total", "{0}");
                    GridColumnSummaryItem item6 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Advance", "{0}");
                    GridColumnSummaryItem item8 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Paid", "{0}");
                    GridColumnSummaryItem item3 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Due.", "{0}");
                    col29.Summary.Add(item8);
                    col27.Summary.Add(item6);
                    col26.Summary.Add(item5);
                    col7.Summary.Add(item1);
                    col32.Summary.Add(item3);
                    gridView1.BestFitColumns();
                }
                else
                    if (objReport.ToString() == "Admin Staff" && table != null)
                {
                    var s = table.AsEnumerable().Where(a => a.Field<string>("Staff_Type") == "Administration");

                    gridSalary.DataSource = CommonFunctions.AutoNumberedTable(s.CopyToDataTable());
                    Title = "Salary Statement Administration Staff";

                    col.Visible = false;
                    col2.Visible = true;
                    col3.Visible = false;
                    col4.Visible = true;
                    col5.Visible = false;
                    col6.Visible = true;
                    col7.Visible = true;
                    col14.Visible = true;
                    col15.Visible = true;
                    col26.Visible = true;
                    col27.Visible = true;
                    col29.Visible = true;
                    col30.Visible = false;
                    col31.Visible = false;
                    col32.Visible = true;
                    col33.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Salary", "{0}");
                    GridColumnSummaryItem item2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Arrears", "{0}");
                    GridColumnSummaryItem item4 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "addition.", "{0}");
                    GridColumnSummaryItem item5 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Total", "{0}");
                    GridColumnSummaryItem item6 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Advance", "{0}");
                    GridColumnSummaryItem item8 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Paid", "{0}");
                    GridColumnSummaryItem item3 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Due.", "{0}");
                    col29.Summary.Add(item8);
                    col27.Summary.Add(item6);
                    col26.Summary.Add(item5);
                    col7.Summary.Add(item1);
                    col32.Summary.Add(item3); gridView1.BestFitColumns();
                }
                else
                    if (objReport.ToString() == "Non Teaching Staff" && table != null)
                {
                    var s = table.AsEnumerable().Where(a => a.Field<string>("Staff_Type") == "Class IV");

                    gridSalary.DataSource = CommonFunctions.AutoNumberedTable(s.CopyToDataTable());
                    Title = "Salary Statement Non Teaching Staff";
                    col.Visible = false;
                    col2.Visible = true;
                    col3.Visible = false;
                    col4.Visible = true;
                    col5.Visible = false;
                    col6.Visible = true;
                    col7.Visible = true;
                    col14.Visible = false;
                    col15.Visible = false;
                    col26.Visible = true;
                    col27.Visible = true;
                    col29.Visible = true;
                    col30.Visible = false;
                    col31.Visible = false;
                    col32.Visible = true;
                    col33.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Salary", "{0}");
                    GridColumnSummaryItem item2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Arrears", "{0}");
                    GridColumnSummaryItem item4 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "addition.", "{0}");
                    GridColumnSummaryItem item5 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Total", "{0}");
                    GridColumnSummaryItem item6 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Advance", "{0}");
                    GridColumnSummaryItem item8 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Paid", "{0}");
                    GridColumnSummaryItem item3 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Due.", "{0}");
                    col29.Summary.Add(item8);
                    col27.Summary.Add(item6);
                    col26.Summary.Add(item5);
                    col7.Summary.Add(item1);
                    col32.Summary.Add(item3);
                    gridView1.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Info");
            }
        }
        private void barButPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            print();

        }
        void print()
        {
            Image logo = CommonFunctions.Base64ToImage(Login.Logo);
            var school = CommonFunctions.GetSettings("system_title");
            SalaryStatmentPermanentTeacher report = new SalaryStatmentPermanentTeacher();
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.GridControl = gridSalary;
            report.LabAddress.Text = CommonFunctions.GetSettings("address");
            report.LabTel.Text = CommonFunctions.GetSettings("phone");
            report.LabReport.Text = Title;
            report.labDate.Text = objMonth.ToString() + " - " + objYear.ToString();


            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);

        }
        private void barEditItem1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                String Year = txtYear.EditValue.ToString();
                String Month = txtMonth.EditValue.ToString();
                if (Year != "" && Month != "")
                {
                    int year = Convert.ToInt32(Year);
                    int month = CommonFunctions.GetMonth(Month);
                    int last_day = DateTime.DaysInMonth(year, month);
                    string from_date = month + "/1/" + year;
                    string to_date = month + "/" + last_day + "/" + year;
                    DateFrom.EditValue = from_date;
                    DateTo.EditValue = to_date;

                }
            }
            catch (Exception ep) { }
        }


    }
}
