using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using MySql.Data.MySqlClient;
using SchoolManagementSystem.Students;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Fees
{
    public partial class IncomeManagement : DevExpress.XtraEditors.XtraUserControl
    {
        private static IncomeManagement _instance;

        public static IncomeManagement instance
        {
            get
            {
                if (_instance == null)
                    _instance = new IncomeManagement();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        ObservableCollection<AllStudent> allStudents;
        CommonFunctions fun = new CommonFunctions();
        ObservableCollection<Move> move = new ObservableCollection<Move>();
        object objDate;
        public enum MonthOfYear
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12,
        }
        public IncomeManagement()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            bool is_yearly = fun.GetSettings("has_yearly_fee") == "1" ? true : false;//true for yearly false monthly
            toggle_feetype.EditValue = is_yearly;

            txtMonthGI.Properties.Items.Clear();
            FromDate.EditValueChanged += TxtDate_EditValueChanged;
            foreach (MonthOfYear item in Enum.GetValues(typeof(MonthOfYear)))
            {
                var S = item.GetHashCode();
                txtMonthGI.Properties.Items.Add(item);
            }
            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";
            LoadOtherCat();

            FillGridIncomeManage();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            barButInvoiceGen.Enabled = false;
            gridView1.OptionsBehavior.Editable = false;
            btn_subjectFee.Enabled = false;
            btnAnnualGenrateFee.Enabled = false;
            if (add)
            {
                barButInvoiceGen.Enabled = true;
                btnAnnualGenrateFee.Enabled = true;
                btn_subjectFee.Enabled = true;
                gridView1.OptionsBehavior.Editable = true;
            }
            btn_delete_invoice.Enabled = false;
            if (Delete)
                btn_delete_invoice.Enabled = true;
            string subject_wise = fun.GetSettings("Institute_Type");
            if (subject_wise == "Subject Wise Institute")
                btn_subjectFee.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            else
                btn_subjectFee.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            if (txtSection.EditValue != null && !string.IsNullOrEmpty(txtSection.EditValue.ToString()))
            {
                txtStudent.Properties.DataSource = fun.GetAllStudentsinSection(txtSection.EditValue.ToString(), " and student.free_student != 1 ");
                txtStudent.Properties.DisplayMember = "name";
                txtStudent.Properties.ValueMember = "student_id";
            }
        }
        private void TxtDate_EditValueChanged(object sender, EventArgs e)
        {
            objDate = (sender as DateEdit).EditValue;
        }

        bool has_col_chak = false;
        private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(e.RowHandle);
            GridView view = sender as GridView;
            if (view == null) return;
            string query = "";
            if (!has_col_chak)
            {
                if (e.Column.FieldName == "Concession" && Convert.ToBoolean(toggle_feetype.EditValue) == false)
                {
                    Decimal discount = Convert.ToDecimal(e.Value);
                    query = "update `invoice` set `fee_concession` = '" + discount + "' where `invoice_id` = '" + dr["ID"] + "';";
                    fun.Execute_Insert(query);
                    update_connection_prev(dr);
                    return;
                }
                if (e.Column.FieldName == "late_fee" && Convert.ToBoolean(toggle_feetype.EditValue) == false)
                {
                    Decimal late_fee = Convert.ToDecimal(e.Value);
                    query = "update `invoice` set `late_fee` = '" + late_fee + "' where `invoice_id` = '" + dr["ID"] + "';";
                    fun.Execute_Insert(query);
                    update_connection_prev(dr);
                    return;
                }
                if (e.Column.FieldName == "Previous")
                {
                    int previous = 0;
                    query = "select * from invoice where invoice_id = '" + dr["ID"] + "';";
                    DataTable inv_details = fun.FetchDataTable(query);
                    try
                    {
                        if (inv_details.Rows[0]["previous_fee"] != null && Convert.ToInt32(inv_details.Rows[0]["previous_fee"]) > 0 && toggle_feetype.EditValue.ToString() == "True")
                        {
                            MessageBox.Show("Previous amount already added against this student Now You cannot change that amount");
                            previous = Convert.ToInt32(inv_details.Rows[0]["previous_fee"]);
                            return;
                        }
                        else
                            previous = Convert.ToInt32(e.Value);
                    } catch(Exception ep) { }
                    if (Convert.ToInt32(dr["installment"]) > 0)
                    {
                        string ins_query = "SELECT title,installment_id,IFNULL(Remaining,0) AS amount,due,status FROM fees_installment WHERE invoice_id = '" + dr["ID"] + "' AND title NOT IN ('Plus Session','Board Fee') ORDER BY installment_id ASC limit 1";
                        DataTable table = fun.FetchDataTable(ins_query);
                        if (table.Rows.Count > 0 && table.Rows[0]["status"].ToString() == "False")
                        {
                            query = "UPDATE `fees_installment` SET `previous` = '" + previous + "' WHERE `installment_id` = '" + table.Rows[0]["installment_id"] + "';";
                            query += "UPDATE `fees_installment` SET `Remaining` = ((ifnull(amount,0)+ifnull(previous,0))-ifnull(discount,0))-ifnull(paid,0) WHERE `installment_id` = '" + table.Rows[0]["installment_id"] + "';";
                            query += "update `invoice` set `previous_fee` = '" + previous + "' where `invoice_id` = '" + dr["ID"] + "';";
                            fun.Execute_Insert(query);

                            update_connection_prev(dr);
                        }
                        else
                        {
                            MessageBox.Show("First Installment is paid of that student  or no Installment data against this Student");
                            return;
                        }

                    }
                    else
                    {
                        query = "update `invoice` set `previous_fee` = '" + previous + "' where `invoice_id` = '" + dr["ID"] + "';";
                        fun.Execute_Insert(query);
                        update_connection_prev(dr);
                    }
                    return;
                }
            }
        }
        bool update_connection_prev(DataRow dr)
        {
            string query = "update `invoice` set amount = (ifnull(previous_fee,0)+ifnull(current_fee,0)+ifnull(other_fee,0)+ifnull(late_fee,0))-ifnull(fee_concession,0) where `invoice_id` = '" + dr["ID"] + "';";
            query += "update `invoice` set `due` = amount-amount_paid where `invoice_id` = '" + dr["ID"] + "';";
            fun.Execute_Insert(query);
            query = " update invoice set status = if(due<=0,'paid','unpaid') where `invoice_id` = '" + dr["ID"] + "';";
            fun.Execute_Insert(query);

            query = "select * from `invoice` where `invoice_id` = '" + dr["ID"] + "';";
            DataTable dt = fun.FetchDataTable(query);
            if (dt.Rows.Count > 0)
            {
                has_col_chak = true;
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Previous", dt.Rows[0]["previous_fee"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Concession", dt.Rows[0]["fee_concession"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Current", dt.Rows[0]["current_fee"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Other_Fee", dt.Rows[0]["other_fee"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "late_Fee", dt.Rows[0]["late_fee"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Amount", dt.Rows[0]["amount"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Paid", dt.Rows[0]["amount_paid"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Due", dt.Rows[0]["due"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Status", dt.Rows[0]["status"]);
            }
            has_col_chak = false;
            return has_col_chak;
        }
        #region Not used for snap rellport

        ObservableCollection<FeeCard> feeCard = new ObservableCollection<FeeCard>();

        void FeeCard(DataRow data)
        {
            //feeCard.Clear();
            //PfeeCard.Clear();
            //MySqlConnection con = new MySqlConnection(Login.constring);
            //DateTime date = Convert.ToDateTime(data[11]);
            //con.Open();
            //MySqlCommand cmd = new MySqlCommand("SELECT student_id as stdid,amount as Amount,method as Method,date as Date,dues as Due FROM payment where student_id='" + row[12] + "'&& month(date)='" + date.Month + "';", con);
            //MySqlDataReader reader3 = cmd.ExecuteReader();
            //if (reader3.HasRows)
            //{
            //    while (reader3.Read())
            //    {
            //        PreviousFee dd = new PreviousFee
            //        {
            //            StdID = (reader3["stdid"].ToString() == "") ? "-" : reader3["stdid"].ToString(),
            //            Amount = (reader3["Amount"].ToString() == "") ? "-" : reader3["Amount"].ToString(),
            //            Method = (reader3["Method"].ToString() == "") ? "-" : reader3["Method"].ToString(),
            //            Date = (reader3["Date"].ToString() == "") ? "-" : reader3["Date"].ToString(),
            //            Due = (reader3["Due"].ToString() == "") ? "-" : reader3["Due"].ToString(),
            //        };
            //        PfeeCard.Add(dd);
            //    }
            //}
            //else
            //{
            //    PreviousFee dd = new PreviousFee
            //    {
            //        StdID = "-",
            //        Amount = "-",
            //        Method = "-",
            //        Date = "-",
            //        Due = "-"
            //    };
            //    PfeeCard.Add(dd);
            //}
            //   snapControl1.Document.DataSources.Clear();
            //   snapControl1.Document.DataSources.Add(new DataSourceInfo("Previous", PfeeCard));
            //  con.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSetting("system_title");
            FeeCard d = new FeeCard
            {
                ID = data[0].ToString(),
                StdID = data[12].ToString(),
                StdName = data[1].ToString(),
                StdClass = data[13].ToString(),
                Title = data[2].ToString(),
                Description = data[3].ToString(),
                Previous = Convert.ToInt32(data[4].ToString()),
                Current = Convert.ToInt32(data[5]),
                Concession = Convert.ToInt32(data[6]),
                Amount = Convert.ToInt32(data[7]),
                Paid = Convert.ToInt32(data[8]),
                Due = Convert.ToInt32(data[9]),
                Date = data[11].ToString(),
                Status = data[10].ToString(),
                logo = logo,
                School = school[0].Name
            };
            feeCard.Add(d);
            //  snapControl1.Document.DataSources.Add(new DataSourceInfo("Fee", feeCard));

        }
        #endregion

        object grid_ID;

        public void grid_cutomization()
        {
            int fees_previous_edit = Convert.ToInt32(fun.GetSettings("fees_previous_edit"));
            #region column properites from database
            string query = "Select ID from grids where GridName = 'Fee_Mgmt'";
            grid_ID = fun.Execute_Scaler_string(query);
            if (Convert.ToInt32(grid_ID) <= 0)
            {
                query = "INSERT INTO `grids`(`GridName`) VALUES ('Fee_Mgmt')";
                grid_ID = fun.Execute_Insert(query);
            }
            ColumnView view = (ColumnView)gridInvoiceManage.FocusedView;
            view.ClearColumnsFilter();
            for (int i = 0; i < view.Columns.Count; i++)
            {
                string name = view.Columns[i].FieldName;
                string select_col_query = "Select * from grids_columns where Grid_ID = '" + grid_ID + "' and Column_Name = '" + name + "'";
                DataTable grid_Column = fun.FetchDataTable(select_col_query);
                if (grid_Column.Rows.Count <= 0)
                {
                    query = "INSERT INTO `grids_columns`(`Grid_ID`, `Column_Name`, `Width`, `order`, `Visible`) VALUES ('" + grid_ID + "','" + name + "','" + view.Columns[i].Width + "','" + i + "','" + (view.Columns[i].Visible == true ? 1 : 0) + "')";
                    fun.Execute_Query(query);
                    grid_Column = fun.FetchDataTable(select_col_query);
                }
                bool visibility = Convert.ToInt32(grid_Column.Rows[0]["Visible"]) == 1 ? true : false;
                view.Columns[i].VisibleIndex = Convert.ToInt32(grid_Column.Rows[0]["order"]);
                view.Columns[i].Visible = visibility;
                view.Columns[i].Width = Convert.ToInt32(grid_Column.Rows[0]["Width"]);

                view.Columns[i].OptionsColumn.ReadOnly = true;

                bool isyearly = Convert.ToBoolean(toggle_feetype.EditValue);
                if(name == "Previous")
                {
                    view.Columns[i].OptionsColumn.ReadOnly = fees_previous_edit == 1 ? false : true;
                }
                if (!isyearly) /// this for yearly fee installment fee concession should be added in installment report
                {
                    if (name == "Concession" || name == "late_fee")
                        view.Columns[i].OptionsColumn.ReadOnly = false;
                }
                if (name == "Title" || name == "Remarks" || name == "ID")
                    view.Columns[i].Visible = false;

                if (name == "Other_Fee" || name == "late_fee" || name == "Previous" || name == "Current" || name == "Concession" || name == "Amount" || name == "Due" || name == "Paid")
                {
                    gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, name, gridView1.Columns[name]);
                    gridView1.Columns[name].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, name, "{0}"));
                }
            }
            #endregion column properties

            var col3 = gridView1.Columns["Class"];
            col3.Group(); col3.Visible = false;
            gridView1.Columns["Section"].Group(); //col4.Visible = false;
            gridView1.Columns["type"].Group();
            gridView1.ExpandAllGroups();
        }
        public void FillGridIncomeManage()
        {
            gridView1.GroupSummarySortInfo.Clear();
            gridInvoiceManage.DataSource = null;
            gridView1.Columns.Clear();
            gridView1.GroupSummary.Clear();
            DataTable dt = fun.invoice_grid_data();



            foreach (DataRow dr in dt.Rows)
            {
                int late_fee_fine = fun.GetClass_lateFee(Convert.ToInt32(dr["class_id"]));
                if (late_fee_fine > 0)
                {
                    if (Convert.ToDateTime(dr["due_date"].ToString()).Date < DateTime.Now.Date && !dr["Detail"].ToString().Contains("Late_Fee_Fine") && Convert.ToDouble(dr["Due"]) > 0)
                    {
                        string des = "; Late_Fee_Fine: "+late_fee_fine;
                        string query = "UPDATE `invoice` SET " +
                            " `description` = CONCAT(description,'" + des + "'),`amount` = amount+" + late_fee_fine+ ",`due` = due+" + late_fee_fine + ",`late_fee` = " + late_fee_fine + ",status = 'unpaid' " +
                            " WHERE `invoice_id` = '"+dr["ID"]+"'; ";
                        fun.ExecuteQuery(query);
                    }
                }
            }
            gridInvoiceManage.DataSource = dt;
            gridView1.BestFitColumns();


            gridView1.OptionsView.ShowFooter = true;
            DataTable dtt = fun.fee_categories();
            foreach (DataRow row in dtt.Rows)
            {
                gridView1.Columns[row["fees_title"].ToString()].Visible = false;
            }
            GridColumn colCounter = gridView1.Columns.AddVisible("SrNo#");
            colCounter.VisibleIndex = 0;
            colCounter.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            colCounter.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            gridView1.CustomUnboundColumnData += (sender, e) =>
            {
                GridView v = sender as GridView;
                if (e.Column.FieldName == "SrNo#" && e.IsGetData)
                {
                    e.Value = v.GetRowHandle(e.ListSourceRowIndex) + 1;
                }
            };

            grid_cutomization();
        }
        public ObservableCollection<Move> Getcurrent()
        {
            move.Clear();
            Move d = new Move
            {
                Total = total,
                Paid = paid,
                Due = due,
                Std = stdid,
                No = number,
                Date = date
            };
            move.Add(d);
            return move;
        }
        static string total;
        static string paid;
        static string due;
        static string stdid;
        static string number;
        static string date;

        public static DataRow row;
        private void barButTakePayment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            row = null;
            row = gridView1.GetFocusedDataRow();
            if (row != null)
            {
                int row_handle = gridView1.FocusedRowHandle;
                total = row["Amount"].ToString(); paid = row["Paid"].ToString(); due = row["Due"].ToString(); stdid = row["StdID"].ToString(); number = row["ID"].ToString(); date = row["Date"].ToString();
                using (Take takePayment = new Take(row))
                {
                    takePayment.Text = "Take Payment";
                    if (takePayment.ShowDialog() == DialogResult.OK)
                    {

                    }
                    else
                    {
                        ColumnView view = (ColumnView)gridInvoiceManage.FocusedView;
                        DataTable dt = fun.invoice_grid_data(Convert.ToInt32(number));//(" and SrNo = '" + dr["SrNo"] + "'");
                        gridView1.BeginUpdate();
                        for (int i = 0; i < view.Columns.Count; i++)
                        {
                            string name = view.Columns[i].FieldName;
                            try
                            {
                                if (name != "due_date" && name != "SrNo#")//name == "paid_date" || name == "Date" || 
                                { 
                                    string val = dt.Rows[0][name].ToString();
                                    gridView1.SetRowCellValue(row_handle, name, val);
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        gridView1.EndUpdate();
                    }
                }
            }
        }

        public void UpdateDataGrid()
        {
            gridInvoiceManage.DataSource = fun.invoice_grid_data();
            gridInvoiceManage.Refresh();
            gridView1.ExpandAllGroups();
        }
        public static DataRow stdRow { get; set; }
        private void barButFeeRecipt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int margin = Convert.ToInt32(fun.GetSettings("fees_receipt_margin"));
            ReportPrintTool printTool;
            MessageBoxManager.Yes = "Print All";
            MessageBoxManager.No = "Print Selected";
            MessageBoxManager.Cancel = "Cancle";
            if (margin == 4)
            {
                MasterFeeReciptReport4 MReport;
                MasterFeeReciptReport4 output = new MasterFeeReciptReport4();
                MessageBoxManager.Register();
                DialogResult a = MessageBox.Show("Please Select option for print....", "Printing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                fun.loaderform(() =>
                {
                    if (a != DialogResult.Cancel)
                    {
                        if (a == DialogResult.Yes)
                        {
                            for (int i = 0; i < gridView1.DataRowCount; i++)
                            {
                                DataRow row2 = gridView1.GetDataRow(i);
                                stdRow = row2;
                                MReport = new MasterFeeReciptReport4(stdRow);
                                MReport.Landscape = true;
                                MReport.CreateDocument(false);
                                output.Pages.AddRange(MReport.Pages);
                                output.PrintingSystem.ContinuousPageNumbering = true;
                            }
                        }
                        else if (a == DialogResult.No)
                        {
                            DataRow row2 = gridView1.GetFocusedDataRow();
                            stdRow = row2;
                            MReport = new MasterFeeReciptReport4(stdRow);
                            MReport.Landscape = true;
                            MReport.CreateDocument(false);
                            output.Pages.AddRange(MReport.Pages);
                            output.PrintingSystem.ContinuousPageNumbering = true;
                        }

                        //if (output == null) output = new MasterFeeReciptReport4();
                        printTool = new ReportPrintTool(output);

                        printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
                    }
                });
                MessageBoxManager.Unregister();
            }
            else
            {
                MasterFeeReciptReport MReport;
                MasterFeeReciptReport output = new MasterFeeReciptReport();
                MessageBoxManager.Register();
                DialogResult a = MessageBox.Show("Please Select option for print....", "Printing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                fun.loaderform(() =>
                {
                    if (a != DialogResult.Cancel)
                    {
                        if (a == DialogResult.Yes)
                        {
                            for (int i = 0; i < gridView1.DataRowCount; i++)
                            {
                                DataRow row2 = gridView1.GetDataRow(i);
                                stdRow = row2;
                                MReport = new MasterFeeReciptReport(stdRow);
                                MReport.CreateDocument(false);
                                output.Pages.AddRange(MReport.Pages);
                                output.PrintingSystem.ContinuousPageNumbering = true;
                            }
                        }
                        else if (a == DialogResult.No)
                        {
                            DataRow row2 = gridView1.GetFocusedDataRow();
                            stdRow = row2;
                            MReport = new MasterFeeReciptReport(stdRow);
                            MReport.CreateDocument(false);
                            output.Pages.AddRange(MReport.Pages);
                            output.PrintingSystem.ContinuousPageNumbering = true;
                        }
                        printTool = new ReportPrintTool(output);

                        printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
                    }
                });
                MessageBoxManager.Unregister();
            }
        }
        private void barButFeeReciptBank_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
                stdRow = row;
            MasterFeeReciptReportBank MReport = new MasterFeeReciptReportBank();

            ReportPrintTool printTool = new ReportPrintTool(MReport);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }
        ObservableCollection<PreviousFee> PfeeCard;
        public FeeRecipt58 showReport(bool showDetail)
        {
            DataRow data = stdRow;

            //FeeRecipt report = new FeeRecipt();
            FeeRecipt58 report = new FeeRecipt58();
            if (data != null)
            {
                PfeeCard = new ObservableCollection<PreviousFee>();
                MySqlConnection con = new MySqlConnection(Login.constring);
                DateTime date = Convert.ToDateTime(data["Date"]);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT student_id as stdid,amount as Amount,cash_assets.title as Method,date as Date,dues as Due,transaction " +
                    " FROM payment " +
                    " LEFT JOIN cash_assets ON cash_assets.cashasset_id = payment.cashasset_id " +
                    " WHERE invoice_id = '" + data["ID"].ToString() + "';", con);
                MySqlDataReader reader3 = cmd.ExecuteReader();
                if (reader3.HasRows)
                {

                    while (reader3.Read())
                    {
                        PreviousFee dd = new PreviousFee
                        {
                            StdID = (reader3["stdid"].ToString() == "") ? "-" : reader3["stdid"].ToString(),
                            Amount = (reader3["Amount"].ToString() == "") ? "-" : reader3["Amount"].ToString(),
                            Method = (reader3["Method"].ToString() == "") ? "-" : reader3["Method"].ToString(),
                            Date = (reader3["Date"].ToString() == "") ? "-" : Convert.ToDateTime(reader3["Date"]).ToString("dd-M-yyyy"),
                            Due = (reader3["transaction"].ToString() == "") ? "-" : reader3["transaction"].ToString(),
                        };
                        PfeeCard.Add(dd);
                    }
                }

                con.Close();
                report.GridControl = PfeeCard;
                Image logo = fun.Base64ToImage(Login.Logo);
                var school = fun.GetSettings("system_title");
                report.PicIogoBox.Image = logo;
                report.LabTitle.Text = school;
                report.labChallanN.Text = data["ID"].ToString();
                report.LabAddress.Text = fun.GetSettings("address");
                report.LabTel.Text = fun.GetSettings("phone");
                report.LabReport.Text = "  Student Copy";
                report.LabSID.Text = data["StdID"].ToString() + "-" + data["roll"].ToString();
                report.LabSName.Text = data["Name"].ToString();
                report.LabSClass.Text = data["Class"].ToString();
                report.LabDate.Text = Convert.ToDateTime(data["Date"]).ToString(" M-yyyy");
                report.labPre.Text = data["Previous"].ToString();
                report.LabCur.Text = data["Current"].ToString();
                report.LabConce.Text = data["Concession"].ToString();
                report.LabAmount.Text = data["Amount"].ToString();
                report.LabPaid.Text = data["Paid"].ToString();
                report.LabDue.Text = data["Due"].ToString();
                report.LabelDueDate.Text = data["due_date"].ToString();

                var amount = data["Amount"].ToString();
                if (Convert.ToInt32(data["installment"].ToString()) > 0)
                {
                    string ins_query = "SELECT title,installment_id,IFNULL(Remaining,0) AS amount,due FROM fees_installment WHERE invoice_id = '" + data["ID"].ToString() + "' AND status = 0 ORDER BY installment_id ASC limit 1";
                    DataTable table = fun.FetchDataTable(ins_query);
                    if (Convert.ToInt32(data["Due"]) > 0 && table.Rows.Count <= 0)
                    {
                        ins_query = "select title, installment_id,IFNULL(Remaining,0) as amount,due from fees_installment where status = 1 AND invoice_id = '" + data["ID"].ToString() + "' order by installment_id desc limit 1";
                        table = fun.FetchDataTable(ins_query);
                    }
                    amount = table.Rows.Count > 0 ? table.Rows[0]["amount"].ToString() : "0";
                    report.LabelAmountTitle.Text = table.Rows.Count > 0 ? table.Rows[0]["title"].ToString() : "Installment";
                    report.LabDate.Text = table.Rows.Count > 0 ? Convert.ToDateTime(table.Rows[0]["due"]).ToString("MMM/yyyy") : "";
                    report.LabelDueDate.Text = table.Rows.Count > 0 ? Convert.ToDateTime(table.Rows[0]["due"]).ToString("yyyy-MM-dd") : "";

                }


                report.LabelinvoiceAmount.Text = amount;
                report.LabelAmountWord.Text = fun.NumberToWords(int.Parse(amount)) + " Only.";
                report.LabelMerge.Text = (data["Detail"].ToString());
                report.LabelOther.Text = data["Other_Fee"].ToString();

                report.LabAccountName.Text = "A/N : " + fun.GetSettings("account_name");
                report.LabAccountN.Text = "A/C : " + fun.GetSettings("account_number") + "  ";
                report.LabelNotes.Text = (fun.GetSettings("fee_note")).Replace(";", Environment.NewLine);
                //report.ReportPrintOptions.PrintOnEmptyDataSource = false;
                if (showDetail) // for bank
                {
                    if (fun.GetSettings("bank_receipt_detail") == "0")
                    {
                        report.DetailFlag = true;
                    }
                }
            }
            return report;
        }
        public FeeRecipt_Bank showReportBank(bool showDetail)
        {
            DataRow data = stdRow;
            FeeRecipt_Bank report = new FeeRecipt_Bank();
            if (data != null)
            {
                PfeeCard = new ObservableCollection<PreviousFee>();
                MySqlConnection con = new MySqlConnection(Login.constring);
                DateTime date = Convert.ToDateTime(data[12]);

                string current_amt = string.IsNullOrEmpty(data["Current"].ToString()) ? "0" : data["Current"].ToString();
                string discount_chk = string.IsNullOrEmpty(data["Concession"].ToString()) ? "0" : data["Concession"].ToString();
                int current = Convert.ToInt32(current_amt) - Convert.ToInt32(discount_chk); //Convert.ToInt32(data[6]) - Convert.ToInt32(data[7]) - Convert.ToInt32(data[9]);
                PreviousFee dd;
                String query = "SELECT fc.fees_title,fca.amount FROM fees_cat_amount AS fca " +
                                    " LEFT JOIN fees_category AS fc ON fc.fee_cat_id = fca.fee_cat_id " +
                                    " WHERE fca.invoice_id = '{0}'";
                query = String.Format(query, data["ID"].ToString());
                DataTable table = fun.FetchDataTable(query);
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        dd = new PreviousFee { Amount = row["amount"].ToString(), Method = row["fees_title"].ToString() };
                        PfeeCard.Add(dd);
                    }
                }
                var amount = data[10].ToString();
                if (Convert.ToInt32(data["installment"].ToString()) > 0)
                {

                }
                else
                {
                    dd = new PreviousFee { Amount = current.ToString(), Method = "Current Fee" };
                    PfeeCard.Add(dd);
                    dd = new PreviousFee { Amount = data["Previous"].ToString(), Method = "Previous" };
                    PfeeCard.Add(dd);
                }

                report.GridControl = PfeeCard;
                Image logo = fun.Base64ToImage(Login.Logo);
                var school = fun.GetSettings("system_title");
                report.PicIogoBox.Image = logo;
                report.LabTitle.Text = school;
                report.labChallanN.Text = data[0].ToString();
                report.LabAddress.Text = fun.GetSettings("account_name");
                report.LabTel.Text = fun.GetSettings("account_number");
                report.LabReport.Text = "  Student Copy";
                report.LabStudentId.Text = data[1].ToString();
                report.LabRoll.Text = data[20].ToString();
                report.LabFName.Text = data["Father"].ToString();
                report.LabSName.Text = data[2].ToString();
                report.LabSClass.Text = data[13].ToString();
                report.LabSection.Text = data["Section"].ToString();
                report.LabDate.Text = Convert.ToDateTime(data[12]).ToString("MMM/yyyy");
                if (Convert.ToInt32(data["installment"].ToString()) > 0)
                {
                    string ins_query = "SELECT title,installment_id,IFNULL(Remaining,0) AS total,amount,due,Previous FROM fees_installment WHERE invoice_id = '" + data["ID"].ToString() + "' AND status = 0 ORDER BY installment_id ASC limit 1";
                    table = fun.FetchDataTable(ins_query);
                    if (Convert.ToInt32(data["Due"]) > 0 && table.Rows.Count <= 0)
                    {
                        ins_query = "select title, installment_id,IFNULL(Remaining,0) AS total,amount,due,Previous from fees_installment where status = 1 AND invoice_id = '" + data["ID"].ToString() + "' order by installment_id desc limit 1";
                        table = fun.FetchDataTable(ins_query);
                    }
                    amount = table.Rows.Count > 0 ? table.Rows[0]["total"].ToString() : "0";
                    report.LabelAmountTitle.Text = table.Rows.Count > 0 ? table.Rows[0]["title"].ToString() : "Installment";
                    report.LabDate.Text = table.Rows.Count > 0 ? Convert.ToDateTime(table.Rows[0]["due"]).ToString("MMM/yyyy") : "";
                    report.LabelDueDate.Text = table.Rows.Count > 0 ? Convert.ToDateTime(table.Rows[0]["due"]).ToString("yyyy-MM-dd") : "";
                    //Details Values
                    string current_ins = table.Rows.Count > 0 ? table.Rows[0]["amount"].ToString() : "0";
                    dd = new PreviousFee { Amount = current_ins, Method = "Current Fee" };
                    PfeeCard.Add(dd);
                    dd = new PreviousFee { Amount = table.Rows[0]["Previous"].ToString(), Method = "Previous" };
                    PfeeCard.Add(dd);
                }
                else
                {
                    string duedate = string.IsNullOrEmpty(data["due_date"].ToString()) ? "0001-01-01" : data["due_date"].ToString() == "0000-00-00" ? "0001-01-01" : data["due_date"].ToString();
                    report.LabelDueDate.Text = (Convert.ToDateTime(duedate)).ToString("dd-MMM-yyyy");
                }

                report.LabelinvoiceAmount.Text = amount;
                report.LabelAmountWord.Text = fun.NumberToWords(int.Parse(amount)) + " Only.";
                report.LabelIssueDate.Text = fun.CurrentDate();
                /*int number_month = Convert.ToInt32(data[18].ToString());
                int start_month = Convert.ToInt32(Convert.ToDateTime(data[12]).Month);
                String[] month_array = { "Jan", "Feb", "March", "Apr", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec" };
                String[] FeeMontharray = new String[number_month];
                for (int i = 0; i < number_month; i++)
                {
                    int month = ((start_month + i) - 1);
                    if (month <= 11)
                        FeeMontharray[i] = month_array[start_month + i - 1];
                    else
                    {
                        month = month - 11;
                        FeeMontharray[i] = month_array[month];
                    }
                }
                String FeeMonthStr = string.Join(",", FeeMontharray);

                report.LabDate.Text = FeeMonthStr + "(" + Convert.ToDateTime(data[12]).Year + ")";*/

                //report.LabAccountName.Text = fun.GetSettings("account_name");
                //report.LabAccountN.Text = fun.GetSettings("account_number") + "  ";
                report.LabelNotes.Text = (fun.GetSettings("fee_note")).Replace(";", Environment.NewLine);
                //report.ReportPrintOptions.PrintOnEmptyDataSource = false;
                if (showDetail) // for bank
                {
                    if (fun.GetSettings("bank_receipt_detail") == "0")
                    {
                        report.DetailFlag = true;
                    }
                }
            }
            return report;
        }
        public FeeRecipt_Cashold showReportCash(DataRow data, bool showDetail)
        {
            FeeRecipt_Cashold report = new FeeRecipt_Cashold();
            if (data != null)
            {
                PfeeCard = new ObservableCollection<PreviousFee>();
                MySqlConnection con = new MySqlConnection(Login.constring);
                DateTime date = Convert.ToDateTime(data["Date"].ToString() == "" ? DateTime.Now.ToShortDateString() : data["Date"].ToString());
                PreviousFee dd;
                string Detail = data["Detail"].ToString();
                String query;
                DataTable table;
                if (Detail.Contains("Merge Student"))
                {
                    Detail = Detail.Replace("Merge Student Ids:", "");
                    Detail = Detail.TrimEnd(';');
                    String[] str_array = Detail.Split(new char[] { ';' });
                    if (str_array.Length > 0)
                    {
                        foreach (string str in str_array)
                        {
                            dd = new PreviousFee { Amount = str.Split(new char[] { ':' })[1].ToString(), Method = str.Split(new char[':'])[0].ToString() };
                            PfeeCard.Add(dd);
                        }
                    }
                }
                else
                {
                    string current_amt = string.IsNullOrEmpty(data["Current"].ToString()) ? "0" : data["Current"].ToString();
                    string discount_chk = string.IsNullOrEmpty(data["Concession"].ToString()) ? "0" : data["Concession"].ToString();
                    int current = Convert.ToInt32(current_amt) - Convert.ToInt32(discount_chk);//Convert.ToInt32(data[6]) - Convert.ToInt32(data[7]) - Convert.ToInt32(data[9]);
                    dd = new PreviousFee { Amount = current.ToString(), Method = "Current Fee" };
                    PfeeCard.Add(dd);
                    dd = new PreviousFee { Amount = data["Previous"].ToString(), Method = "Previous" };
                    PfeeCard.Add(dd);

                    query = "SELECT fc.fees_title,fca.amount FROM fees_cat_amount AS fca " +
                                        " LEFT JOIN fees_category AS fc ON fc.fee_cat_id = fca.fee_cat_id " +
                                        " WHERE fca.invoice_id = '" + data["ID"].ToString() + "'";
                    table = fun.FetchDataTable(query);
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            dd = new PreviousFee { Amount = row["amount"].ToString(), Method = row["fees_title"].ToString() };
                            PfeeCard.Add(dd);
                        }
                    }
                }
                report.GridControl = PfeeCard;
                Image logo = fun.Base64ToImage(Login.Logo);
                var school = fun.GetSettings("system_title");
                report.PicIogoBox.Image = logo;
                report.LabTitle.Text = school;
                report.labChallanN.Text = data["ID"].ToString();
                report.LabAddress.Text = fun.GetSettings("account_name");
                report.LabTel.Text = fun.GetSettings("account_number");
                report.LabReport.Text = "  Student Copy";
                report.LabStudentId.Text = data["StdID"].ToString();
                report.LabRoll.Text = data["Roll"].ToString();
                report.LabFName.Text = data["Father"].ToString();
                report.LabSName.Text = data["Name"].ToString();
                report.LabSClass.Text = data["Class"].ToString();
                report.LabSection.Text = data["Section"].ToString();
                report.LabDate.Text = Convert.ToDateTime(data["Date"].ToString() == "" ? DateTime.Now.ToShortDateString() : data["Date"].ToString()).ToString(" M-yyyy");
                var amount = data["Amount"].ToString();

                if (Convert.ToInt32(data["installment"].ToString()) > 0)
                {
                    string ins_query = "SELECT title,installment_id,IFNULL(Remaining,0) AS total,amount,due,previous FROM fees_installment WHERE invoice_id = '" + data["ID"].ToString() + "' AND status = 0 ORDER BY installment_id ASC limit 1";
                    table = fun.FetchDataTable(ins_query);
                    if (Convert.ToInt32(data["Due"]) > 0 && table.Rows.Count <= 0)
                    {
                        ins_query = "select title, installment_id,IFNULL(Remaining,0) AS total,amount,due,previous from fees_installment where status = 1 AND invoice_id = '" + data["ID"].ToString() + "' order by installment_id desc limit 1";
                        table = fun.FetchDataTable(ins_query);
                    }
                    amount = table.Rows.Count > 0 ? table.Rows[0]["total"].ToString() : "0";
                    report.LabelAmountTitle.Text = table.Rows.Count > 0 ? table.Rows[0]["title"].ToString() : "Installment";
                    report.LabDate.Text = table.Rows.Count > 0 ? Convert.ToDateTime(table.Rows[0]["due"]).ToString("MMM/yyyy") : "";
                    report.LabelDueDate.Text = table.Rows.Count > 0 ? Convert.ToDateTime(table.Rows[0]["due"]).ToString("yyyy-MM-dd") : "";
                    //Details Values
                    PfeeCard.Clear();
                    string current_ins = table.Rows.Count > 0 ? table.Rows[0]["amount"].ToString() : "0";
                    dd = new PreviousFee { Amount = current_ins, Method = "Current Fee" };
                    PfeeCard.Add(dd);
                    dd = new PreviousFee { Amount = table.Rows[0]["Previous"].ToString(), Method = "Previous" };
                    PfeeCard.Add(dd);
                }
                else
                {
                    string duedate = string.IsNullOrEmpty(data["due_date"].ToString()) ? "0001-01-01" : data["due_date"].ToString() == "0000-00-00" ? "0001-01-01" : data["due_date"].ToString();
                    report.LabelDueDate.Text = (Convert.ToDateTime(duedate)).ToString("dd-MMM-yyyy");

                }
                report.LabelinvoiceAmount.Text = amount;
                report.LabelAmountWord.Text = fun.NumberToWords(int.Parse(amount)) + " Only.";

                report.LabelIssueDate.Text = fun.CurrentDate();
                /*int number_month = Convert.ToInt32(data["Month"].ToString() == "" ? "0" : data["Month"].ToString());
                int start_month = Convert.ToInt32(Convert.ToDateTime(data["Date"].ToString() == "" ? DateTime.Now.ToShortDateString() : data["Date"].ToString()).Month);
                String[] month_array = { "Jan", "Feb", "March", "Apr", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec" };
                String[] FeeMontharray = new String[number_month];
                for (int i = 0; i < number_month; i++)
                {
                    int month = ((start_month + i) - 1);
                    if (month <= 11)
                        FeeMontharray[i] = month_array[start_month + i - 1];
                    else
                    {
                        month = month - 11;
                        FeeMontharray[i] = month_array[month];
                    }
                }
                String FeeMonthStr = string.Join(",", FeeMontharray);*/



                report.LabelNotes.Text = (fun.GetSettings("fee_note")).Replace(";", Environment.NewLine);
                //report.ReportPrintOptions.PrintOnEmptyDataSource = false;
                if (showDetail) // for bank
                {
                    if (fun.GetSettings("bank_receipt_detail") == "0")
                    {
                        report.DetailFlag = true;
                    }
                }
            }
            return report;
        }

        private void barButInvoiceGen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            groupControlGI.Visible = true;
            LoadOtherCat();
        }
        private void btnIG_Click(object sender, EventArgs e)
        {
            if (txtMonthGI.Text == "" || txtYearGI.Text == "")
            {
                MessageBox.Show("Select Month & Year", "Info");
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
                int student_id;
                string student_name, InsertQuery, InsertCat;
                int invoice_id;
                string[] all_std = txtStudent.EditValue.ToString().Split(',');
                foreach (var std in all_std)
                {
                    student_id = Convert.ToInt32(std.Trim());
                    var query = "select * from student where student_id = '" + student_id + "'";
                    DataTable std_info = fun.FetchDataTable(query);
                    student_name = std_info.Rows[0]["name"].ToString();
                    MySqlConnection con = new MySqlConnection(Login.constring);
                    con.Open();
                    query = "SELECT * FROM invoice WHERE student_id = '" + student_id + "' and MONTH(date) = '" + start_month + "' and year(date) = '" + txtYearGI.Text + "'; ";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataReader reader1 = cmd.ExecuteReader();
                    if (reader1.HasRows)
                    {
                        alreadyExit = 1;
                    }

                    con.Close();

                    pDue = 0;
                    var classID = Convert.ToInt32(std_info.Rows[0]["class_id"].ToString());//fun.GetClassIDisSession(std.Split('>')[2].Trim(), fun.GetDefaultSessionName());
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

                        MFeeC = fun.GetStudentFeeConcession(student_id);
                        total_concession = MFeeC * number_month;
                    }
                    if (checkAF.Checked == true)
                    {
                        total_concession += fun.GetStudentAFeeConcession(student_id);
                        AFee = fun.GetClassAdmissionFee(classID);
                        des += " ;Admission Fee:" + AFee;
                        MFee += AFee;
                    }
                    if (checkSF.Checked == true)
                    {
                        total_concession += fun.GetStudentSDFeeConcession(student_id);
                        SDFee = fun.GetClassSecurityDeposit(classID);
                        des += " ;Security Deposit:" + SDFee;
                        MFee += SDFee;
                    }
                    if (checkACF.Checked == true)
                    {
                        total_concession += fun.GetStudentACFeeConcession(student_id);
                        ACFee = fun.GetClassAnnualCharges(classID);
                        des += " ;Annual Charges:" + ACFee;
                        MFee += ACFee;
                    }
                    if (checkECF.Checked == true)
                    {
                        total_concession += fun.GetStudentECFeeConcession(student_id);
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
                        query = string.Format(query, student_id, otherfees_ids);
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
                        con.Open();
                        var query1 = "SELECT * FROM invoice WHERE student_id = '" + student_id + "' AND forward = 0 ORDER BY invoice_id DESC LIMIT 1";
                        MySqlCommand cmd2 = new MySqlCommand(query1, con);
                        MySqlDataReader reader2 = cmd2.ExecuteReader();
                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                var due = reader2["due"].ToString();
                                p = (due != null) ? due : "";
                                //update forward 
                                query = "UPDATE invoice SET forward = 1 WHERE invoice_id = '{0}'";
                                query = string.Format(query, reader2["invoice_id"].ToString());
                                fun.ExecuteQuery(query);
                            }
                            pDue = Convert.ToInt32(p);
                        }
                        con.Close();
                        int advance_paid = 0;
                        if (pDue > 0) // remaining
                            des += " ;Arrears:" + pDue;
                        else if (pDue < 0) // Advanced Paid
                        {
                            advance_paid = -(pDue);
                            pDue = 0;
                            des += " :Advance paid:" + advance_paid;

                        }
                        total += ((pDue + MFee + OtherFees) - advance_paid) - total_concession;
                        des = des.TrimStart(';');

                        InsertQuery = "INSERT into invoice(Student_id,class_id,section_id,title,description,other_fee,previous_fee,current_fee,fee_concession,amount,amount_paid,due,creation_timestamp,status,date,sync,to_date,number_month,due_date) VALUES('" + student_id.ToString() + "','" + classID.ToString() + "',(SELECT section_id FROM student WHERE student_id = '" + student_id.ToString() + "'),'Monthly Fee','" + des + "','" + OtherFees + "','" + pDue + "','" + MFee + "','" + total_concession + "','" + total + "','0','" + total + "','" + fun.time() + "','Unpaid','" + format_date + "','0','" + Convert.ToDateTime(end_date).ToString("yyyy-MM-dd") + "','" + number_month + "','" + due_date + "')";
                        invoice_id = fun.ExecuteInsert(InsertQuery);
                        if (otherfees_ids != "")
                        {
                            InsertCat = "INSERT INTO fees_cat_amount(invoice_id,amount,fee_cat_id) " +
                                                " SELECT '{0}' AS invoice_id, fees_val, fees_cat_id FROM fees_category_student WHERE fees_cat_id IN({1}) AND student_id = '{2}'";
                            InsertCat = String.Format(InsertCat, invoice_id, otherfees_ids, student_id);
                            fun.ExecuteQuery(InsertCat);
                        }
                        if (advance_paid > 0)
                        {
                            query = "INSERT into payment(expense_category_id,title,payment_type,invoice_id,student_id,description,total,amount,dues,timestamp,date,user_id,sync, cashasset_id) VALUES " +
                                                      "('" + fun.GetExpenseID("Student Fee") + "','Monthly Advance Fee','Income','" + invoice_id + "','" + student_id + "',concat('(" + student_id + ") ','" + student_name + "'),'" + total + "'," +
                                                      "'" + advance_paid + "','" + (total - advance_paid) + "','" + fun.time() + "','" + date + "','" + Login.CurrentUserID + "','0','1');";
                            query = "UPDATE invoice set previous_fee=" + (-1*advance_paid) + " WHERE invoice_id='" + invoice_id + "';";
                            query += "update `invoice` set amount = (ifnull(previous_fee,0)+ifnull(current_fee,0)+ifnull(other_fee,0))-ifnull(fee_concession,0) where `invoice_id` = '" + invoice_id + "';";
                            query += "update `invoice` set `due` = amount-amount_paid where `invoice_id` = '" + invoice_id + "';";
                            fun.Execute_Insert(query);
                        }
                        query = " update invoice set status = if(due<=0,'paid','unpaid') where `invoice_id` = '" + invoice_id + "';";
                        fun.Execute_Insert(query);
                    }
                    else
                    {
                        con.Open();
                        var query1 = "SELECT *," +
                        " (SELECT due FROM invoice WHERE student_id = '" + student_id + "' AND forward = 1 ORDER BY invoice_id DESC LIMIT 1) as pre_due " +
                        " FROM invoice WHERE student_id = '" + student_id + "' AND forward = 0 ORDER BY invoice_id DESC LIMIT 1";
                        MySqlCommand cmd2 = new MySqlCommand(query1, con);
                        MySqlDataReader reader2 = cmd2.ExecuteReader();
                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                var due = reader2["pre_due"].ToString();
                                p = (!string.IsNullOrEmpty(due)) ? due : "0";
                                //update forward 
                                //query = "UPDATE invoice SET forward = 1 WHERE invoice_id = '{0}'";
                                //query = string.Format(query, reader2["invoice_id"].ToString());
                                //fun.ExecuteQuery(query);
                                pDue = Convert.ToInt32(p);
                                int advance_paid = 0;
                                if (pDue > 0) // remaining
                                    des += " ;Arrears:" + pDue;
                                else if (pDue < 0) // Advanced Paid
                                {
                                    advance_paid = -(pDue);
                                    pDue = 0;
                                    des += " :Advance paid:" + advance_paid;
                                }
                                total += ((pDue + MFee + OtherFees) - advance_paid) - total_concession;
                                des = des.TrimStart(';');
                                InsertQuery = "update invoice set Student_id='" + student_id + "',class_id='" + classID + "',section_id=(SELECT section_id FROM student WHERE student_id = '" + student_id + "'),title='Monthly Fee' " +
                                ",description='" + des + "',other_fee='" + OtherFees + "',previous_fee='" + pDue + "',current_fee='" + MFee + "',fee_concession='" + total_concession + "' " +
                                ",amount='" + total + "',due=(" + total + "-amount_paid),creation_timestamp='" + fun.time() + "',status='Unpaid'" +
                                ",date='" + format_date + "',to_date='" + Convert.ToDateTime(end_date).ToString("yyyy-MM-dd") + "',number_month='" + number_month + "',due_date='" + due_date + "',payment_details=NULL " +
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
                                if (advance_paid > 0)
                                {
                                    query = "INSERT into payment(expense_category_id,title,payment_type,invoice_id,student_id,description,total,amount,dues,timestamp,date,user_id,sync, cashasset_id) VALUES " +
                                                              "('" + fun.GetExpenseID("Student Fee") + "','Monthly Advance Fee','Income','" + invoice_id + "','" + student_id + "',concat('(" + student_id + ") ','" + student_name + "'),'" + total + "'," +
                                                              "'" + advance_paid + "','" + (total - advance_paid) + "','" + fun.time() + "','" + date + "','" + Login.CurrentUserID + "','0','1');";
                                    query = "UPDATE invoice set amount_paid=amount_paid+" + advance_paid + " WHERE invoice_id='" + invoice_id + "';";
                                    query += "update `invoice` set amount = (ifnull(previous_fee,0)+ifnull(current_fee,0)+ifnull(other_fee,0))-ifnull(fee_concession,0) where `invoice_id` = '" + invoice_id + "';";
                                    query += "update `invoice` set `due` = amount-amount_paid where `invoice_id` = '" + invoice_id + "';";
                                    fun.Execute_Insert(query);
                                }
                                query = " update invoice set status = if(due<=0,'paid','unpaid') where `invoice_id` = '" + invoice_id + "';";
                                fun.Execute_Insert(query);
                            }

                        }
                        con.Close();
                    }
                    alreadyExit = 0;

                }
                //Forward Passout students 
                var section_list = txtSection.EditValue.ToString();
                var info = section_list.Split(',');
                int section_id = 0;
                foreach (var val in info)
                {
                    section_id = int.Parse(val.Trim());
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
                txtClass.EditValue = null;
                txtSection.EditValue = null;
                txtStudent.EditValue = null;
                DueDate.Value = DateTime.Now;
                FillGridIncomeManage();
            });
        }
        private void btnHideGI_Click(object sender, EventArgs e)
        {
            groupControlGI.Visible = false;
        }

        private void barButFind_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.GroupSummarySortInfo.Clear();
            String from = Convert.ToDateTime(FromDateCont.EditValue).ToString("yyyy-MM-dd");
            String to = Convert.ToDateTime(ToDateCont.EditValue).ToString("yyyy-MM-dd");
            string query = "SELECT student.student_id as StdID,student.name as Name,class.name as Class ,payment.amount as Amount,payment.date as Date, cash_assets.title as method,parent.name as Parent,parent.phone,payment.transaction " +
                            " FROM payment " +
                            " INNER JOIN student on(student.student_id = payment.student_id) " +
                            " join parent on parent.parent_id = student.parent_id " +
                            " LEFT JOIN class on class.class_id=student.class_id " +
                            " LEFT JOIN cash_assets on cash_assets.cashasset_id = payment.cashasset_id " +
                            " WHERE date >= '" + from + "' AND date <= '" + to + "'";
            DataTable table = fun.FetchDataTable(query);
            gridInvoiceManage.DataSource = null;
            gridView1.Columns.Clear();
            gridInvoiceManage.DataSource = table;
            gridView1.BestFitColumns();
            var col1 = gridView1.Columns["Name"]; col1.OptionsColumn.ReadOnly = true;
            var col2 = gridView1.Columns["Amount"]; col2.OptionsColumn.ReadOnly = true;
            var col3 = gridView1.Columns["Date"]; col3.OptionsColumn.ReadOnly = true;

            var col44 = gridView1.Columns["StdID"];
            col44.Visible = false;
            var col5 = gridView1.Columns["Class"];
            col5.Group();
            gridView1.ExpandAllGroups();
            GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Amount", "{0}");
            col2.Summary.Add(item1);

            gridView1.OptionsView.ShowFooter = true;
            gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Amount", gridView1.Columns["Amount"]);
        }


        private List<FeeRecipt_Cash> reportscash = new List<FeeRecipt_Cash>();
        public void AppendReportCash(FeeRecipt_Cash re)
        {
            reportscash.Add(re);
        }
        public FeeRecipt_Cash showReportCash(bool showDetail)
        {
            DataRow data = stdRow;

            //FeeRecipt report = new FeeRecipt();
            FeeRecipt_Cash report = new FeeRecipt_Cash();
            if (data != null)
            {
                PfeeCard = new ObservableCollection<PreviousFee>();
                MySqlConnection con = new MySqlConnection(Login.constring);
                DateTime date = Convert.ToDateTime(data["Date"]);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT student_id as stdid,amount as Amount,cash_assets.title as Method,date as Date,dues as Due,transaction " +
                    " FROM payment " +
                    " LEFT JOIN cash_assets ON cash_assets.cashasset_id = payment.cashasset_id " +
                    " WHERE invoice_id = '" + data["ID"].ToString() + "';", con);
                MySqlDataReader reader3 = cmd.ExecuteReader();
                if (reader3.HasRows)
                {

                    while (reader3.Read())
                    {
                        PreviousFee dd = new PreviousFee
                        {
                            StdID = (reader3["stdid"].ToString() == "") ? "-" : reader3["stdid"].ToString(),
                            Amount = (reader3["Amount"].ToString() == "") ? "-" : reader3["Amount"].ToString(),
                            Method = (reader3["Method"].ToString() == "") ? "-" : reader3["Method"].ToString(),
                            Date = (reader3["Date"].ToString() == "") ? "-" : Convert.ToDateTime(reader3["Date"]).ToString("dd-M-yyyy"),
                            Due = (reader3["transaction"].ToString() == "") ? "-" : reader3["transaction"].ToString(),
                        };
                        PfeeCard.Add(dd);
                    }
                }

                con.Close();
                report.GridControl = PfeeCard;
                Image logo = fun.Base64ToImage(Login.Logo);
                var school = fun.GetSettings("system_title");
                report.PicIogoBox.Image = logo;
                report.LabTitle.Text = school;
                report.labChallanN.Text = data[0].ToString();
                report.LabAddress.Text = fun.GetSettings("address");
                report.LabTel.Text = fun.GetSettings("phone");
                report.LabReport.Text = "  Student Copy";
                report.LabSID.Text = data[1].ToString() + "-" + data[20].ToString();
                report.LabSName.Text = data[2].ToString();
                report.LabSClass.Text = data[13].ToString();
                report.LabDate.Text = Convert.ToDateTime(data["Date"]).ToString(" M-yyyy");
                report.labPre.Text = data[5].ToString();
                report.LabCur.Text = data[6].ToString();
                report.LabConce.Text = data[7].ToString();
                report.LabAmount.Text = data[8].ToString();
                report.LabPaid.Text = data[9].ToString();
                report.LabDue.Text = data[10].ToString();
                report.LabelDueDate.Text = data[17].ToString();

                var amount = data[10].ToString();
                if (Convert.ToInt32(data["installment"].ToString()) > 0)
                {
                    string query = "SELECT IFNULL(amount,0) AS amount,due FROM fees_installment WHERE invoice_id = '{0}' AND status = 0 ORDER BY installment_id ASC limit 1";
                    query = string.Format(query, data["ID"].ToString());
                    DataTable table = fun.FetchDataTable(query);
                    amount = table.Rows.Count > 0 ? table.Rows[0]["amount"].ToString() : "0";
                    report.LabelAmountTitle.Text = "Installment: ";
                    if (table.Rows.Count > 0)
                    {
                        report.LabelDueDate.Text = Convert.ToDateTime(table.Rows[0]["due"].ToString()).ToString(" M-yyyy");
                    }
                }


                report.LabelinvoiceAmount.Text = amount;
                report.LabelAmountWord.Text = fun.NumberToWords(int.Parse(amount)) + " Only.";
                report.LabelMerge.Text = (data[16].ToString());
                report.LabelOther.Text = data[4].ToString();

                int number_month = Convert.ToInt32(data["month"].ToString());
                int start_month = Convert.ToInt32(Convert.ToDateTime(data["Date"]).Month);
                String[] month_array = { "Jan", "Feb", "March", "Apr", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec" };
                String[] FeeMontharray = new String[number_month];
                for (int i = 0; i < number_month; i++)
                {
                    FeeMontharray[i] = month_array[start_month + i - 1];
                }
                String FeeMonthStr = string.Join(",", FeeMontharray);

                report.LabDate.Text = FeeMonthStr + "(" + Convert.ToDateTime(data["Date"]).Year + ")";

                report.LabAccountName.Text = "A/N : " + fun.GetSettings("account_name");
                report.LabAccountN.Text = "A/C : " + fun.GetSettings("account_number") + "  ";
                report.LabelNotes.Text = (fun.GetSettings("fee_note")).Replace(";", Environment.NewLine);
                //report.ReportPrintOptions.PrintOnEmptyDataSource = false;
                if (showDetail) // for bank
                {
                    if (fun.GetSettings("bank_receipt_detail") == "0")
                    {
                        report.DetailFlag = true;
                    }
                }
            }
            return report;
        }
        public FeeRecipt_Cashold CreateReportCash()
        {
            int margin = Convert.ToInt32(fun.GetSettings("fees_receipt_margin"));
            //FeeRecipt output = new FeeRecipt();
            FeeRecipt_Cashold output = new FeeRecipt_Cashold();
            foreach (var rep in reportscash)
            {
                rep.Margins = new System.Drawing.Printing.Margins(margin, margin, margin, margin);
                rep.CreateDocument(false);
                output.Pages.AddRange(rep.Pages);
            }
            if (output == null) output = new FeeRecipt_Cashold();// new FeeRecipt();
            output.PrintingSystem.ContinuousPageNumbering = true;
            return output;
        }

        /// </summary>
        public void print_receipt(DataRow row)
        {
            reports.Clear();
            stdRow = row;
            AppendReport(showReport(false));
            ReportPrintTool printTool = new ReportPrintTool(CreateReport());
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
            FillGridIncomeManage();
        }
        private List<FeeRecipt58> reports = new List<FeeRecipt58>();
        public void AppendReport(FeeRecipt58 re)
        {
            reports.Add(re);
        }
        public FeeRecipt58 CreateReport()
        {
            int margin = Convert.ToInt32(fun.GetSettings("fees_receipt_margin"));
            //FeeRecipt output = new FeeRecipt();
            FeeRecipt58 output = new FeeRecipt58();
            foreach (var rep in reports)
            {
                rep.Margins = new System.Drawing.Printing.Margins(margin, margin, margin, margin);
                rep.CreateDocument(false);
                output.Pages.AddRange(rep.Pages);
            }
            if (output == null) output = new FeeRecipt58();// new FeeRecipt();
            output.PrintingSystem.ContinuousPageNumbering = true;
            return output;
        }

        private void barBtnPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FeeReport report = new FeeReport();
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.LabAddress.Text = fun.GetSettings("address");
            report.LabTel.Text = fun.GetSettings("phone");

            report.GridControl = gridInvoiceManage;
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int margin = Convert.ToInt32(fun.GetSettings("fees_receipt_margin"));
            ReportPrintTool printTool;
            MessageBoxManager.Yes = "Print All";
            MessageBoxManager.No = "Print Selected";
            MessageBoxManager.Cancel = "Cancle";
            MessageBoxManager.Register();

            DialogResult a = MessageBox.Show("Please Select option for print....", "Printing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            fun.loaderform(() =>
            {
                if (a != DialogResult.Cancel)
                {
                    FeeRecipt_single MReport;
                    FeeRecipt_single output = new FeeRecipt_single();
                    int total_row = gridView1.DataRowCount;
                    DataRow row1;
                    if (a == DialogResult.Yes)
                    {
                        for (int i = 0; i < total_row; i = i + 1)
                        {
                            row1 = i < total_row ? gridView1.GetDataRow(i) : null;
                            MReport = new FeeRecipt_single(row1);
                            MReport.CreateDocument(false);
                            output.Pages.AddRange(MReport.Pages);
                            output.PrintingSystem.ContinuousPageNumbering = true;
                        }
                    }
                    else if (a == DialogResult.No)
                    {
                        DataRow row = gridView1.GetFocusedDataRow();
                        row1 = gridView1.GetFocusedDataRow();
                        MReport = new FeeRecipt_single(row1);
                        MReport.CreateDocument(false);
                        output.Pages.AddRange(MReport.Pages);
                        output.PrintingSystem.ContinuousPageNumbering = true;
                    }
                    printTool = new ReportPrintTool(output);

                    printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                    printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
                }
            });
            // }
            MessageBoxManager.Unregister();
            //}
            /*reports.Clear();
            MessageBoxManager.Yes = "Print All";
            MessageBoxManager.No = "Print Selected";
            MessageBoxManager.Cancel = "Cancle";
            MessageBoxManager.Register();
            DialogResult a = MessageBox.Show("Please Select option for print....", "Printing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            fun.loaderform(() =>
            {
                if (a != DialogResult.Cancel)
                {
                    if (a == DialogResult.Yes)
                    {
                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {
                            DataRow newrow = gridView1.GetDataRow(i);
                            stdRow = newrow;
                            AppendReport(showReport(false));
                        }
                    }
                    else if (a == DialogResult.No)
                    {
                        DataRow row = gridView1.GetFocusedDataRow();
                        stdRow = row;
                        AppendReport(showReport(false));
                    }
                    ReportPrintTool printTool = new ReportPrintTool(CreateReport());

                    printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                    printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
                }
            });
            MessageBoxManager.Unregister();*/

            //FillGridIncomeManage();
        }

        private void btnInstallment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            row = null;
            row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            InstallmentPayment ObjInstallment = new InstallmentPayment(row);
            ObjInstallment.ShowDialog();
            UpdateDataGrid();
        }

        private void btnBulkInstallment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow[] invoice_array = new DataRow[gridView1.DataRowCount];            // Declare int array of zeros
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                invoice_array[i] = gridView1.GetDataRow(i);
            }
            InstallmentPaymentBulk ObjInstallment = new InstallmentPaymentBulk(invoice_array);
            ObjInstallment.ShowDialog();
            UpdateDataGrid();
        }

        private void barButtonItem3_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fun.loaderform(() =>
            {
                FillGridIncomeManage();
            });
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int margin = Convert.ToInt32(fun.GetSettings("fees_receipt_margin"));
            ReportPrintTool printTool;
            MessageBoxManager.Yes = "Print All";
            MessageBoxManager.No = "Print Selected";
            MessageBoxManager.Cancel = "Cancle";
            MessageBoxManager.Register();
            if (margin == 4)
            {
                DialogResult a = MessageBox.Show("Please Select option for print....", "Printing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                fun.loaderform(() =>
                {
                    if (a != DialogResult.Cancel)
                    {
                        MasterFeeReciptCash4 MReport;
                        MasterFeeReciptCash4 output = new MasterFeeReciptCash4(null, null, null, null);
                        int total_row = gridView1.DataRowCount;
                        DataRow row1, row2, row3, row4;
                        if (a == DialogResult.Yes)
                        {

                            for (int i = 0; i < total_row; i = i + 4)
                            {
                                row1 = i < total_row ? gridView1.GetDataRow(i) : null;
                                row2 = i + 1 < total_row ? gridView1.GetDataRow(i + 1) : null;
                                row3 = i + 2 < total_row ? gridView1.GetDataRow(i + 2) : null;
                                row4 = i + 3 < total_row ? gridView1.GetDataRow(i + 3) : null;
                                MReport = new MasterFeeReciptCash4(row1, row2, row3, row4);
                                MReport.Landscape = true;
                                MReport.CreateDocument(false);
                                output.Pages.AddRange(MReport.Pages);
                                output.PrintingSystem.ContinuousPageNumbering = true;
                            }
                        }
                        else if (a == DialogResult.No)
                        {
                            DataRow row = gridView1.GetFocusedDataRow();
                            row1 = gridView1.GetFocusedDataRow();
                            row2 = gridView1.GetFocusedDataRow();
                            row3 = gridView1.GetFocusedDataRow();
                            row4 = gridView1.GetFocusedDataRow();
                            MReport = new MasterFeeReciptCash4(row1, row2, row3, row4);
                            MReport.Landscape = true;
                            MReport.CreateDocument(false);
                            output.Pages.AddRange(MReport.Pages);
                            output.PrintingSystem.ContinuousPageNumbering = true;
                        }
                        if (output == null) output = new MasterFeeReciptCash4(null, null, null, null);
                        printTool = new ReportPrintTool(output);

                        //printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
                    }
                });
            }
            else
            {
                DialogResult a = MessageBox.Show("Please Select option for print....", "Printing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                fun.loaderform(() =>
                {
                    if (a != DialogResult.Cancel)
                    {
                        MasterFeeReciptCash3 MReport;
                        MasterFeeReciptCash3 output = new MasterFeeReciptCash3();
                        int total_row = gridView1.DataRowCount;
                        DataRow row1, row2, row3;
                        if (a == DialogResult.Yes)
                        {
                            for (int i = 0; i < total_row; i = i + 3)
                            {
                                row1 = i < total_row ? gridView1.GetDataRow(i) : null;
                                row2 = i + 1 < total_row ? gridView1.GetDataRow(i + 1) : null;
                                row3 = i + 2 < total_row ? gridView1.GetDataRow(i + 2) : null;
                                MReport = new MasterFeeReciptCash3(row1, row2, row3);
                                MReport.CreateDocument(false);
                                output.Pages.AddRange(MReport.Pages);
                                output.PrintingSystem.ContinuousPageNumbering = true;
                            }
                        }
                        else if (a == DialogResult.No)
                        {
                            DataRow row = gridView1.GetFocusedDataRow();
                            row1 = gridView1.GetFocusedDataRow();
                            row2 = gridView1.GetFocusedDataRow();
                            row3 = gridView1.GetFocusedDataRow();
                            MReport = new MasterFeeReciptCash3(row1, row2, row3);
                            MReport.CreateDocument(false);
                            output.Pages.AddRange(MReport.Pages);
                            output.PrintingSystem.ContinuousPageNumbering = true;
                        }
                        //if (output == null) output = new MasterFeeReciptCash3();
                        printTool = new ReportPrintTool(output);

                        //printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
                    }
                });
            }
            MessageBoxManager.Unregister();
        }

        private void btnFeeDetails_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            StudentFeeReport std = new StudentFeeReport(Convert.ToInt32(dr["StdID"]), toggle_feetype.EditValue);
            FeeDetails_STD form = new FeeDetails_STD(std, Convert.ToInt32(dr["StdID"]));
            form.Show();
        }

        private void gridView1_DragObjectDrop(object sender, DragObjectDropEventArgs e)
        {
            GridColumn cln = e.DragObject as GridColumn;
            ColumnView view = (ColumnView)gridInvoiceManage.FocusedView;
            view.ClearColumnsFilter();
            if (cln != null)
            {
                if (cln.Visible)
                {
                    for (int i = 0; i < view.Columns.Count; i++)
                    {
                        string name = view.Columns[i].FieldName;
                        int order = view.Columns[i].VisibleIndex;
                        if (view.Columns[i].Visible || name == cln.FieldName)
                        {
                            string query = "UPDATE `grids_columns` SET `order`=" + order + ",`Visible`='1' WHERE `Grid_ID` ='" + grid_ID + "' and `Column_Name` = '" + name + "'";
                            fun.ExecuteInsert(query);
                        }

                    }
                }
                else
                {
                    string name = cln.FieldName;
                    string query = "UPDATE `grids_columns` SET `Visible`='0' WHERE `Grid_ID` ='" + grid_ID + "' and `Column_Name` = '" + name + "'";
                    fun.ExecuteInsert(query);
                }
            }
        }

        private void gridView1_ColumnWidthChanged(object sender, ColumnEventArgs e)
        {
            ColumnView view = (ColumnView)gridInvoiceManage.FocusedView;
            view.ClearColumnsFilter();
            for (int i = 0; i < view.Columns.Count; i++)
            {
                string name = view.Columns[i].FieldName;
                int width = view.Columns[i].VisibleWidth;
                string query = "UPDATE `grids_columns` SET `Width`=" + width + " WHERE `Grid_ID` ='" + grid_ID + "' and `Column_Name` = '" + name + "'";
                fun.ExecuteInsert(query);
            }
        }

        private void btnAnnualGenrateFee_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (ADDFeeInstallments insform = new ADDFeeInstallments())
            {
                if (insform.ShowDialog() == DialogResult.OK)
                {

                }
                if (insform.CloseBox)
                {
                    FillGridIncomeManage();
                }
            }
        }

        private void btn_subjectFee_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (subject_fees insform = new subject_fees())
            {
                if (insform.ShowDialog() == DialogResult.OK)
                {

                }
                else
                {
                    FillGridIncomeManage();
                }
            }
        }
        private void toggle_feetype_EditValueChanged(object sender, EventArgs e)
        {
            if (toggle_feetype.EditValue.ToString() == "True")
                toggle_feetype.Caption = "Yearly";
            else
                toggle_feetype.Caption = "Monthly";
        }
        private void double_receipt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int margin = Convert.ToInt32(fun.GetSettings("fees_receipt_margin"));
            ReportPrintTool printTool;
            MessageBoxManager.Yes = "Print All";
            MessageBoxManager.No = "Print Selected";
            MessageBoxManager.Cancel = "Cancle";
            MessageBoxManager.Register();

            DialogResult a = MessageBox.Show("Please Select option for print....", "Printing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            fun.loaderform(() =>
            {
                if (a != DialogResult.Cancel)
                {
                    FeeRecipt_double MReport;
                    FeeRecipt_double output = new FeeRecipt_double();
                    int total_row = gridView1.DataRowCount;
                    DataRow row1, row2, row3, row4;
                    if (a == DialogResult.Yes)
                    {
                        for (int i = 0; i < total_row; i = i + 2)
                        {
                            row1 = i < total_row ? gridView1.GetDataRow(i) : null;
                            row2 = i < total_row ? gridView1.GetDataRow(i) : null;
                            row3 = i + 1 < total_row ? gridView1.GetDataRow(i + 1) : null;
                            row4 = i + 1 < total_row ? gridView1.GetDataRow(i + 1) : null;

                            MReport = new FeeRecipt_double(row1, row2, row3, row4);
                            MReport.CreateDocument(false);
                            output.Pages.AddRange(MReport.Pages);
                            output.PrintingSystem.ContinuousPageNumbering = true;
                        }
                    }
                    else if (a == DialogResult.No)
                    {
                        DataRow row = gridView1.GetFocusedDataRow();
                        row1 = gridView1.GetFocusedDataRow();
                        row2 = gridView1.GetFocusedDataRow();
                        row3 = null;
                        row4 = null;
                        MReport = new FeeRecipt_double(row1, row2, row3, row4);
                        MReport.CreateDocument(false);
                        output.Pages.AddRange(MReport.Pages);
                        output.PrintingSystem.ContinuousPageNumbering = true;
                    }
                    printTool = new ReportPrintTool(output);

                    printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                    printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
                }
            });
            // }
            MessageBoxManager.Unregister();
            //}
        }

        private void btn_receipt_template_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //fee
            //Fee_Receipt_Template frt = new Fee_Receipt_Template();
            //frt.Show();
        }

        private void btn_delete_invoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            int invoice_id = Convert.ToInt32(dr["ID"].ToString());
            string query = "select * from invoice where invoice_id = '" + invoice_id + "'";
            DataTable inv_dt = fun.FetchDataTable(query);
            if (inv_dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(inv_dt.Rows[0]["amount_paid"]) > 0)
                {
                    MessageBox.Show("This invoice can not be delete now because some amount is paid", "Invoice Deleting Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    using (invoice_delete_from de = new invoice_delete_from())
                    {
                        if (de.ShowDialog() == DialogResult.Yes) { }
                        else if (de.is_delete == true)
                        {
                            query = "SELECT * FROM invoice WHERE student_id = '" + inv_dt.Rows[0]["student_id"] + "' AND forward = 1 ORDER BY invoice_id DESC LIMIT 1";
                            DataTable prev_invoice_std = fun.FetchDataTable(query);


                            query = "INSERT INTO `invoice_deleted` (`invoice_id`,`student_id`,`class_id`,`section_id`,`title`,`description`,`previous_fee`,`current_fee`,`fee_concession`,`amount`,`amount_paid`,`due`,`creation_timestamp`,`payment_timestamp`,`payment_method`,`payment_details`,`status`,`date`,`forward`,`sync`,`remarks`,`other_fee`,`due_date`,`to_date`,`number_month`,fo_month,late_fee) " +
                                " SELECT * FROM invoice WHERE invoice_id = '" + invoice_id + "'; ";
                            if (prev_invoice_std.Rows.Count > 0)
                                query += "UPDATE invoice SET forward = 0 WHERE invoice_id = '" + prev_invoice_std.Rows[0]["invoice_id"] + "';";
                            query += "delete from invoice where invoice_id = '" + invoice_id + "';";
                            query += "update `invoice_deleted` set `reason` = '" + de.txt_description.Text + "',session = '" + Login.session_name + "' where `invoice_id` = '" + invoice_id + "';";
                            fun.ExecuteQuery(query);
                            int row_id = gridView1.FocusedRowHandle;
                            gridView1.DeleteRow(row_id);
                        }
                    }

                }
            }
        }
    }

}
