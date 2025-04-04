using DevExpress.LookAndFeel;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraReports.UI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace SchoolManagementSystem.Exam
{
    public partial class FeeInstallmentReports : DevExpress.XtraEditors.XtraUserControl
    {
        private static FeeInstallmentReports _instance;

        public static FeeInstallmentReports instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FeeInstallmentReports();
                return _instance;
            }
        }
        ObservableCollection<CheckListItems> list = new ObservableCollection<CheckListItems>();

        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        ObservableCollection<TeachingStaff> allStaff = new ObservableCollection<TeachingStaff>();

        CommonFunctions fun = new CommonFunctions();

        public FeeInstallmentReports()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {

            allClass.Clear();
            allClass = fun.GetAllClassisSession(fun.GetDefaultSessionName());
            txtclass.Properties.Items.Clear();
            foreach (var allclass in allClass)
                txtclass.Properties.Items.Add(allclass.Salary + ">" + allclass.Name);

            loadinstallment();

        }
        void loadinstallment()
        {

            //string query = "SELECT IFNULL(MAX(cnt),0) AS installment FROM (SELECT COUNT(*) AS cnt FROM fees_installment GROUP BY invoice_id) AS tbl";
            //DataTable dt = fun.FetchDataTable(query);
            //for (int i = 1; i <= dt.Rows.Count; i++)
            //    chb_ins_list.Properties.Items.Add("Installment " + i);
        }
        private void btnCRShow_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSection.Text))
            {
                MessageBox.Show("Please select section From List", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fee_installments();
            //FillGridTotalMarks();
        }
        private void fee_installments()
        {
            string[] sections = txtSection.Text.Split(',');
            int countsec = 0;
            string allsection = "";
            foreach (string str in sections)
            {
                //sections[count] = str.Split('>')[0].Trim();
                if (countsec == 0)
                    allsection = str.Split('>')[0].Trim();
                else
                    allsection += "," + str.Split('>')[0].Trim();
                countsec++;
            }

            string query = "SELECT fi.`invoice_id`,fi.installment_id,std.student_id,fi.title,class.name AS Class,section.name AS Section,std.name AS NAME," +
                            " fi.`previous`,fi.`amount`,fi.discount,Paid,Remaining,fi.`due`AS DueDate, IF(fi.status = 0, 'Unpaid', 'Paid') AS STATUS, grouproyling AS GroupRoyling," +
                            " (IF(grouproyling > 0, fi.Paid - grouproyling, 0)) AS CollageAmount " +
                            " FROM `fees_installment` AS fi" +
                            " JOIN invoice ON invoice.`invoice_id` = fi.`invoice_id`" +
                            " JOIN student AS STD ON std.student_id = invoice.student_id" +
                            " JOIN class ON std.class_id = class.class_id" +
                            " JOIN section ON section.section_id = std.section_id " +
                            " WHERE invoice.`forward` =0 and std.section_id in ("+ allsection + ")  ORDER BY fi.`installment_id` ASC";
            DataTable dt = fun.FetchDataTable(query);
            gridCheckList.DataSource = dt;
            foreach (GridColumn col in gridView1.Columns)
            {
                string c = col.FieldName;
                gridView1.Columns[c].OptionsColumn.ReadOnly = true;
                if (c == "invoice_id" || c == "installment_id")
                    gridView1.Columns[c].Visible = false;

                if (c == "amount" || c == "paid" || c == "Remaining" || c == "previous" )
                {
                    GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, c, "Sum={0}");
                    gridView1.Columns[c].Summary.Clear();
                    gridView1.Columns[c].Summary.Add(item1);
                }
                if(c == "discount" || c == "DueDate")
                    gridView1.Columns[c].OptionsColumn.ReadOnly = false;

            }
            gridView1.Columns["student_id"].Group();
            gridView1.ExpandAllGroups();
        }
        private void FillGridTotalMarks()
        {
            string[] sections = txtSection.Text.Split(',');
            int countsec = 0;
            string allsection = "";
            foreach (string str in sections)
            {
                //sections[count] = str.Split('>')[0].Trim();
                if (countsec == 0)
                    allsection = str.Split('>')[0].Trim();
                else
                    allsection += "," + str.Split('>')[0].Trim();
                countsec++;
            }
            //Fees categories
            string extra_col = "";
            string query_cat = "SELECT fee_cat_id,fees_title FROM fees_category ORDER BY fee_cat_id ASC ";
            DataTable table_cat = fun.GetQueryTable(query_cat);
            foreach (DataRow row in table_cat.Rows)
            {
                extra_col += ",(SELECT IFNULL(amount,0) FROM fees_cat_amount AS fca WHERE fca.invoice_id = invoice.invoice_id AND fca.fee_cat_id = '" + row["fee_cat_id"].ToString() + "') AS '" + row["fees_title"].ToString() + "'";
            }
            string query = "SELECT '' AS SN,invoice_id AS ID,student.student_id AS StdID,student.roll,class.name AS Class,section.name AS Section,student.name AS `Name`,parent.name AS Father,amount AS TotalPackage,amount_paid AS TotalPaid,due AS Remaining " + extra_col +
                         " FROM invoice " +
                         " LEFT JOIN student on student.student_id = invoice.student_id " +
                         " LEFT JOIN parent on parent.parent_id = student.student_id " +
                         " LEFT JOIN class on class.class_id=student.class_id  " +
                         " LEFT JOIN section on section.section_id=student.section_id " +
                         " WHERE 1 = 1 AND forward = 0 and student.section_id in(" + allsection + ")";

            DataTable table = fun.FetchDataTable(query);
            query_cat = "SELECT IFNULL(MAX(cnt),0) AS installment FROM (SELECT COUNT(*) AS cnt FROM fees_installment GROUP BY invoice_id) AS tbl";
            table_cat = fun.GetQueryTable(query_cat);
            int total_install = Convert.ToInt32(table_cat.Rows[0]["installment"]);
            for (int i = 1; i <= total_install; i++)
            {
                table.Columns.Add("Installment " + i, typeof(int));
                table.Columns["Installment " + i].DefaultValue = "0";
                table.Columns.Add("status " + i, typeof(string));
            }
            DataTable mytable = new DataTable();
            if (total_install > 0)
            {
                int count = 0, cat_count = 0;
                foreach (DataRow row in table.Rows)
                {
                    table.Rows[count]["SN"] = count + 1;
                    if (total_install > 0)
                    {
                        query_cat = "SELECT amount,status FROM fees_installment WHERE invoice_id = '" + row["ID"] + "' ORDER BY due ASC";
                        table_cat = fun.FetchDataTable(query_cat);
                        cat_count = 1;
                        for (int i = 1; i <= total_install; i++)
                        {
                            table.Rows[count]["Installment " + i] = 0;
                            table.Rows[count]["status " + i] = "NA";
                        }
                        foreach (DataRow cat_row in table_cat.Rows)
                        {
                            table.Rows[count]["Installment " + cat_count] = cat_row["amount"];
                            table.Rows[count]["status " + cat_count] = Convert.ToInt32(cat_row["status"]) == 1 ? "Paid" : "Unpaid";

                            cat_count++;
                        }
                    }
                    count++;
                }

            }
            gridCheckList.DataSource = table;
            foreach (GridColumn col in gridView1.Columns)
            {
                string c = col.FieldName;
                if (c == "TotalPackage" && c == "TotalPaid" && c == "Remaining")
                {
                    GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, c, "Sum={0}");
                    gridView1.Columns[c].Summary.Clear();
                    gridView1.Columns[c].Summary.Add(item1);
                }
                if (c.Contains("Installment"))
                {
                    GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, c, "Sum={0}");
                    gridView1.Columns[c].Summary.Clear();
                    gridView1.Columns[c].Summary.Add(item1);
                    string[] ins_num = c.Split(' ');
                    string paid = table.AsEnumerable()
                                    .Where(y => y.Field<string>("status " + ins_num[1]) == "Paid")
                                    .Sum(x => x.Field<int>(c))
                                    .ToString();
                    GridColumnSummaryItem item2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, c, "Paid=" + paid);
                    gridView1.Columns[c].Summary.Add(item2);
                    string unpaid = table.AsEnumerable()
                                    .Where(y => y.Field<string>("status " + ins_num[1]) == "Unpaid")
                                    .Sum(x => x.Field<int>(c))
                                    .ToString();
                    GridColumnSummaryItem item3 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, c, "Due=" + unpaid);
                    gridView1.Columns[c].Summary.Add(item3);
                }
            }

            gridView1.Columns["ID"].Visible = false;
        }

        private void btnPrint_Click(object sender, System.EventArgs e)
        {
            fun.ShowGridPreview(gridView1.GridControl);
        }

        private void txtclass_EditValueChanged(object sender, EventArgs e)
        {
            txtSection.Properties.Items.Clear();
            var a = txtclass.Text;
            var info = a.Split(',');
            foreach (var val in info)
            {
                if (val != "")
                {
                    allClass.Clear();
                    allClass = fun.GetAllSectionisClass(int.Parse(val.Split('>')[0].Trim()));
                    foreach (var allclass in allClass)
                        txtSection.Properties.Items.Add(allclass.Salary + " > " + allclass.Name);
                }
            }
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(e.RowHandle);

        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(e.RowHandle);
            if (e.Column.FieldName == "discount")
            {
                Decimal discount = Convert.ToDecimal(e.Value);
                if(Convert.ToInt32(dr["Paid"]) > 0)
                {
                    MessageBox.Show("This installment already paid", "discount on installment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (discount > 0 && discount <= Convert.ToDecimal(dr["Remaining"]))
                {
                    string query = "UPDATE `fees_installment` SET `discount` =" + discount + " WHERE `installment_id` = '"+dr["installment_id"]+"'; ";
                    query += "UPDATE `fees_installment` SET `Remaining` = ((ifnull(amount,0)+ifnull(previous,0))-ifnull(discount,0))-ifnull(paid,0) WHERE `installment_id` = '" + dr["installment_id"] + "';";
                    fun.ExecuteQuery(query);

                    query = "update `invoice` set `fee_concession` = (SELECT SUM(discount) FROM `fees_installment` WHERE invoice_id = '" + dr["invoice_id"] + "') where `invoice_id` = '" + dr["invoice_id"]+"';";
                    fun.ExecuteQuery(query);
                    query = "update `invoice` set amount = (ifnull(previous_fee,0)+ifnull(current_fee,0)+ifnull(other_fee,0))-ifnull(fee_concession,0) where `invoice_id` = '" + dr["invoice_id"] + "';";
                    query += "update `invoice` set `due` = ifnull(amount,0)-ifnull(amount_paid,0) where `invoice_id` = '" + dr["ID"] + "';";
                    fun.ExecuteQuery(query);
                    dr["Remaining"] = Convert.ToInt32(dr["Remaining"]) - discount;
                    // fee_installments();
                }
                else
                    MessageBox.Show("Discount has been give against this installment already","discount on installment",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else if (e.Column.FieldName == "DueDate")
            {
                string query = "UPDATE `fees_installment` SET `due` ='" + Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd") + "' WHERE `installment_id` = '" + dr["installment_id"] + "'; ";
                fun.ExecuteQuery(query);
            }
        }
    }
}
