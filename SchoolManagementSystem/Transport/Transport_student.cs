using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using DevExpress.LookAndFeel;
using DevExpress.XtraGrid;

namespace SchoolManagementSystem.Transport
{
    public partial class Transport_student : UserControl
    {
        private static Transport_student _instance;

        public static Transport_student instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Transport_student();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        public Transport_student()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            loadTra_Stops();
            loadgrid();
            loadstudents();
        }
        public void loadTra_Stops()
        {
            string query = "select id,name from transport_stops";
            DataTable stops = fun.FetchDataTable(query);
            ddlstops.Properties.DataSource = stops;
            ddlstops.Properties.DisplayMember = "name";
            ddlstops.Properties.ValueMember = "id";
        }
        public void loadstudents()

        {
            string query = "select student_id as id,roll,std.name,sec.name as section from student as std" +
                " join section as sec on sec.section_id = std.section_id " +
                "  where not student_id in (select student_id from transport_student where status=0)";
            DataTable std = fun.FetchDataTable(query);
            ddlstudents.Properties.DataSource = std;
            ddlstudents.Properties.DisplayMember = "name";
            ddlstudents.Properties.ValueMember = "id";
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlstudents.Text))
            {
                MessageBox.Show("please select student", "Error");
                return;
            }
            if (ddlstops.EditValue == null)
            {
                MessageBox.Show("please select student", "Error");
                return;
            }
            if (txtTransport_charges.Value <= 0)
            {
                MessageBox.Show("please Insert Charges", "Error");
                return;
            }
            fun.loaderform(() =>
            {
                string query = "INSERT INTO `transport_student`(`student_id`, `stop_id`, `charges`, `Date`) VALUES('" + ddlstudents.EditValue + "','" + ddlstops.EditValue + "','" + txtTransport_charges.Value + "','" + DateTime.Now + "')";
                int ts_id = fun.ExecuteInsert(query);
                query = "INSERT INTO `transport_fee`(`ts_id`,`due`,previous, `creation_date`) VALUES ('" + ts_id + "','" + (Convert.ToInt32(txtTransport_charges.Value)+Convert.ToInt32(txtprevious.Value)) + "','" + txtprevious.Value + "','" + month.Value.ToString("yyyy-MM-dd") + "')";
                fun.Execute_Query(query);
                loadgrid();
                loadstudents();
            });
        }
        public void loadgrid()
        {
            string query = "SELECT tf.id,std.student_id,std.name,std.roll,tss.name as stop,ts.`charges`,tf.previous,tf.discount,(ts.`charges`+tf.previous-tf.discount) as Total,tf.paid,tf.due, " +
                            " if (tf.status = 1,'Paid','UnPaid') as status FROM `transport_student` as ts " +
                            " left join student as std on std.student_id = ts.student_id " +
                            " left join transport_stops as tss on tss.id = ts.stop_id " +
                            " left join transport_fee as tf on tf.ts_id = ts.id" +
                            " where DATE_FORMAT(`creation_date`, '%m/%Y') = '" + month.Value.ToString("MM/yyyy") + "'";
            DataTable dt = fun.FetchDataTable(query);
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridView1.GroupSummary.Clear();
            //string selectmonth = month.Value.ToString("MM/yyyy");
            //string currentMonth = DateTime.Now.ToString("MM/yyyy");
            //if ((dt.Rows.Count <= 0) && (selectmonth == currentMonth))
            //    btn_create_fee.Enabled = true;
            //else
            //    btn_create_fee.Enabled = false;
            gridControl1.DataSource = dt;

            for (int i = 0; i < gridView1.Columns.Count; i++)
                gridView1.Columns[i].OptionsColumn.ReadOnly = true;

            gridView1.Columns["charges"].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "charges", "{0}"));
            gridView1.Columns["previous"].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "previous", "{0}"));
            gridView1.Columns["discount"].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "discount", "{0}"));
            gridView1.Columns["Total"].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Total", "{0}"));
            gridView1.Columns["paid"].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "paid", "{0}"));
            gridView1.Columns["due"].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "due", "{0}"));
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            string query = "UPDATE `transport_fee` SET `status`='1',`paid`=paid+'" + dr["paid"] + "',`discount`='" + dr["discount"] + "',`due`=due-'" + dr["paid"] + "',`paiddate`='" + fun.CurrentDate() + "' WHERE `id`='" + dr["id"] + "'";
            fun.Execute_Query(query);
            loadgrid();
        }

        public TraFee showReport()
        {
            DataRow data = stdRow;

            TraFee report = new TraFee();
            if (data != null)
            {
                Image logo = fun.Base64ToImage(Login.Logo);
                var school = fun.GetSettings("system_title");
                report.PicIogoBox.Image = logo;
                report.LabTitle.Text = school;
                report.LabAddress.Text = fun.GetSettings("address");
                report.LabTel.Text = fun.GetSettings("phone");
                report.LabSID.Text = data["student_id"].ToString();
                report.LabSName.Text = data["name"].ToString();
                report.LabStop.Text = data["stop"].ToString();
                report.LabDate.Text = DateTime.Now.ToString("dd-M-yyyy");
                report.labtra_fee.Text = data["charges"].ToString();
                report.labprevious.Text = data["previous"].ToString();
                report.labtotal.Text = data["Total"].ToString();
            }
            return report;
        }

        private void month_ValueChanged(object sender, EventArgs e)
        {
            loadgrid();
        }

        private void btn_create_fee_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM `transport_student` as ts WHERE ts.`status` = 0 and not id in (select ts_id from transport_fee where DATE_FORMAT(`creation_date`, '%m-%Y') >= DATE_FORMAT('" + month.Value.ToString("yyyy-MM-dd") + "', '%m-%Y') group by ts_id)";
            DataTable dt = fun.FetchDataTable(query);
            foreach (DataRow dr in dt.Rows)
            {
                query = "select due from transport_fee where ts_id = '" + dr["id"] + "' and forward = 0";
                object due = fun.Execute_Scaler_string(query);
                int totaldue = Convert.ToInt32(due) + Convert.ToInt32(dr["charges"]);
                query = "UPDATE `transport_fee` SET `forward`=1 WHERE `ts_id`='" + dr["id"] + "';";
                query += "INSERT INTO `transport_fee`(`ts_id`,previous,`due`, `creation_date`) VALUES ('" + dr["id"] + "','" + due + "','" + totaldue + "','" + month.Value.ToString("yyyy-MM-dd") + "')";
                fun.Execute_Query(query);
            }
            loadgrid();
        }

        private void btnpaid_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr == null)
            {
                MessageBox.Show("Please Selet student");
                return;
            }
            using (payment pay = new payment(dr))
            {
                if (pay.ShowDialog() == DialogResult.Yes)
                {

                }
                else
                    loadgrid();
            }
        }
        public static DataRow stdRow { get; set; }
        private void btn_receipt_Click(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
                stdRow = row;
            TransportFee MReport = new TransportFee();

            ReportPrintTool printTool = new ReportPrintTool(MReport);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }
    }
}
