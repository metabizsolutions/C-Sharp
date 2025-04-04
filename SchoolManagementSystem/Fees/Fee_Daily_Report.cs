using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace SchoolManagementSystem.Fees
{
    public partial class Fee_Daily_Report : UserControl
    {
        private static Fee_Daily_Report _instance;

        public static Fee_Daily_Report instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Fee_Daily_Report();
                return _instance;
            }
        }
        public Fee_Daily_Report()
        {
            InitializeComponent();
        }
        CommonFunctions fun = new CommonFunctions();
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridView1.GroupSummarySortInfo.Clear();
            String from = FromDateCont.Value.ToString("yyyy-MM-dd");
            String to = ToDateCont.Value.ToString("yyyy-MM-dd");
            string query = "SELECT student.student_id as StdID,student.roll,student.name as Name,class.name as Class,section.name as section, payment.Total,payment.amount as Amount,payment.dues,payment.date as Date, cash_assets.title as method,parent.name as Parent,parent.phone,payment.transaction,a.name as Receive_By,invoice_id as ID " +
                            " FROM payment " +
                            " INNER JOIN student on(student.student_id = payment.student_id) " +
                            " join parent on parent.parent_id = student.parent_id " +
                            " join admin as a on a.admin_id = payment.user_id " +
                            " LEFT JOIN class on class.class_id=student.class_id " +
                            " LEFT JOIN section on section.section_id=student.section_id " +
                            " LEFT JOIN cash_assets on cash_assets.cashasset_id = payment.cashasset_id " +
                            " WHERE date >= '" + from + "' AND date <= '" + to + "'";
            DataTable table = fun.FetchDataTable(query);
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = table;
            //Serial No
            GridColumn colCounter = gridView1.Columns.AddVisible("SrNo#");
            colCounter.VisibleIndex = 0;
            colCounter.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            colCounter.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            gridView1.CustomUnboundColumnData += GridView1_CustomUnboundColumnData;
            //End serial No
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

        private void GridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "SrNo#" && e.IsGetData)
                e.Value = view.GetRowHandle(e.ListSourceRowIndex) + 1;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            fun.ShowGridPreview(gridControl1);
        }
    }
}
