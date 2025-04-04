using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using MySql.Data.MySqlClient;
using SchoolManagementSystem.Exam;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace SchoolManagementSystem.Fees
{
    public partial class StudentFeeReport : DevExpress.XtraEditors.XtraUserControl
    {
        private static StudentFeeReport _instance;

        public static StudentFeeReport instance
        {
            get
            {
                if (_instance == null)
                    _instance = new StudentFeeReport();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();

        CommonFunctions fun = new CommonFunctions();
        ObservableCollection<StudentInfo> studentinfo;
        
        public StudentFeeReport()
        {
            InitializeComponent();
            loadfunctions();
        }
        object mgmt_feetype = null;
        public StudentFeeReport(int stdID,object FeesType)
        {
            InitializeComponent();
            loadfunctions();
            mgmt_feetype = FeesType;
            LP_std_ID.EditValue = stdID;
            btnRShow.PerformClick();

        }
        public void loadfunctions()
        {
            string str = "";
            studentinfo = new ObservableCollection<StudentInfo>();
            studentinfo = fun.GetAllStudentsWId_S_C_S(str);
            LP_std_ID.Text = "";
            LP_std_ID.Properties.DataSource = studentinfo;
            LP_std_ID.Properties.DisplayMember = "ID";
            LP_std_ID.Properties.ValueMember = "ID";

            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";
        }
        void section_list(string classid)
        {
            txtSection.Properties.DataSource = fun.GetAllSection_dt(classid);
            txtSection.Properties.DisplayMember = "name";
            txtSection.Properties.ValueMember = "section_id";
        }
        private void btnRShow_Click(object sender, System.EventArgs e)
        {
            result();
        }
        DataTable ResultTable;

        public void result()
        {
            ResultTable = new DataTable();
            if (string.IsNullOrEmpty(LP_std_ID.Text))
            {
                MessageBox.Show("Please select student from list", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var info = fun.GetStudentInfo(int.Parse(LP_std_ID.Text));
            int classID = int.Parse(info.Split('>')[1]);
            int sectionID = int.Parse(info.Split('>')[2]);
            int stdID = int.Parse(LP_std_ID.Text);
            
            MakeReport(stdID);
        }
        //public GridControl studentFeepackagedetails(int stdID)
        //{
        //    query = " SELECT fi.installment_id AS Id,fi.amount,fi.due,IF(fi.status = 1 ,'paid','unpaid') AS status FROM fees_installment as fi " +
        //                  " join invoice on invoice.invoice_id = fi.invoice_id " +
        //                  " WHERE invoice.student_id = '" + stdID + "' and invoice.forward = 0 ORDER BY fi.installment_id ASC " +

        //}
        public GridControl feedetails_IN_FeeManagement(int stdID, object feetype, GridControl gridStdReport, DevExpress.XtraGrid.Views.Grid.GridView gridView1)
        {
            GridControl gridStdReportNew = new GridControl();

            string feeremaining = "";
            int columns = 6;
            var query = "";
            if (Convert.ToBoolean(feetype) == true)
            { //yearly
                feeremaining = " ,@running_total:= @running_total + t.paid AS TotalPaid, (Total - @running_total) as Remaining";
                columns = 7;
                query = "SELECT date,Privious,Current, Concession, Total, t.paid " + feeremaining + ", Month FROM( " +
                               " SELECT payment.date as date,invoice.previous_fee as Privious,invoice.current_fee as Current,invoice.fee_concession as Concession, invoice.amount as Total, sum(payment.amount) as paid,DATE_FORMAT(payment.date,'%M-%Y') as Month FROM invoice " +
                               " left join payment on payment.student_id = invoice.student_id " +
                               " where invoice.student_id = '" + stdID + "' and invoice.forward = 0 GROUP BY payment.date ) t JOIN(SELECT @running_total:= 0) r ORDER BY t.`date`";
            }
            else
            {// monthly
                feeremaining = ", (Total - t.paid) as Remaining";
                query = "SELECT date,Privious,Current, Concession, Total, t.paid  " + feeremaining + "  FROM (  " +
                             " SELECT DATE_FORMAT(invoice.date,'%Y-%m') as date,invoice.previous_fee as Privious,invoice.current_fee as Current, " +
                             " invoice.fee_concession as Concession, invoice.Amount as Total, invoice.amount_paid as paid " +
                             " FROM invoice " +
                             " where invoice.student_id = '" + stdID + "') t ORDER BY t.`date`";
            }
            //var query = " SELECT Previous,other_fee,Current, Concession,Total, t.paid, @running_total:= @running_total + t.paid AS TotalPaid, (Total - @running_total) as Remaining,'' as status, date, Month "+
            //              " FROM(SELECT invoice.previous_fee as Previous, invoice.other_fee, IFNULL(invoice.current_fee, 0) as Current, "+
            //              " invoice.fee_concession as Concession, invoice.amount as Total, sum(payment.amount) as paid, "+
            //              " payment.date as date, DATE_FORMAT(payment.date,'%Y-%m') as Month " +
            //              " FROM invoice "+
            //              " join payment on payment.student_id = invoice.student_id and payment.invoice_id = invoice.invoice_id "+
            //              " where invoice.student_id = '" + stdID + "' and invoice.forward=0 GROUP BY payment.payment_id) " +
            //              " t JOIN(SELECT @running_total:= 0) r ORDER BY t.`date` asc; "; 
            DataTable table = fun.FetchDataTable(query);

            gridView1.Columns.Clear();
            gridStdReport.DataSource = null;
            gridStdReport.DataSource = table;

            gridStdReportNew.DataSource = null;
            gridStdReportNew.DataSource = table;


            gridView1.BestFitColumns();
            gridView1.GroupRowHeight = 25;
            var col2 = gridView1.Columns["date"];

            col2.Group();
            gridView1.ExpandAllGroups();
            for (int i = 0; i <= 5; i++)
            {
                GridColumn Column = gridView1.Columns[i];
                Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }


            gridStdReport.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            gridStdReport.LookAndFeel.UseDefaultLookAndFeel = false; // <<<<<<<<
            gridStdReportNew.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            gridStdReportNew.LookAndFeel.UseDefaultLookAndFeel = false; // <<<<<<<<
            gridView1.AppearancePrint.HeaderPanel.Options.UseBackColor = true;
            gridView1.AppearancePrint.HeaderPanel.BackColor = Color.White;
            gridView1.AppearancePrint.GroupRow.Options.UseBackColor = true;
            gridView1.AppearancePrint.GroupRow.BackColor = Color.LightGray;
            gridView1.AppearancePrint.GroupRow.ForeColor = Color.Black;
            gridView1.AppearancePrint.GroupRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            col2.Caption = " ";


            return gridStdReportNew;
        }
        public static GridControl feedetails(int stdID,object feetype)
        {
            DataTable table = instance.fun.fee_history(stdID,Convert.ToBoolean(feetype));
            GridControl gridControl = new GridControl();
            instance.gridView1.Columns.Clear();
            instance.gridStdReport.DataSource = null;
            instance.gridStdReport.DataSource = table;
            ColumnView view = (ColumnView)instance.gridStdReport.FocusedView;
            for (int i = 0; i < view.Columns.Count; i++)
            {
                view.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
            instance.gridView1.BestFitColumns();
            instance.gridView1.GroupRowHeight = 20;
            var col2 = instance.gridView1.Columns["Month"];
            col2.Group();
            instance.gridView1.ExpandAllGroups();

            instance.gridStdReport.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            instance.gridStdReport.LookAndFeel.UseDefaultLookAndFeel = false; 
            instance.gridView1.AppearancePrint.HeaderPanel.Options.UseBackColor = true;
            instance.gridView1.AppearancePrint.HeaderPanel.BackColor = Color.White;
            instance.gridView1.AppearancePrint.GroupRow.Options.UseBackColor = true;
            instance.gridView1.AppearancePrint.GroupRow.BackColor = Color.LightGray;
            instance.gridView1.AppearancePrint.GroupRow.ForeColor = Color.Black;
            instance.gridView1.AppearancePrint.GroupRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            col2.Caption = " ";
            return instance.gridStdReport;
        }
        public GridControl feedetails2(int stdID, object feetype, GridControl gridStdReport, DevExpress.XtraGrid.Views.Grid.GridView gridView1)
        {
            DataTable table = fun.fee_history(stdID, Convert.ToBoolean(feetype));
            table.Columns.Remove("date");
            GridControl gridStdReportNew = new GridControl();
            gridView1.Columns.Clear();
            gridStdReport.DataSource = null;
            gridStdReport.DataSource = table;
            gridStdReportNew.DataSource = null;
            gridStdReportNew.DataSource = table;

            ColumnView view = (ColumnView) gridStdReport.FocusedView;
            for (int i = 0; i < view.Columns.Count; i++)
            {
                view.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
            //gridView1.Columns["date"].Visible = false;
            gridView1.BestFitColumns();
            gridView1.GroupRowHeight = 20;
            var col2 = gridView1.Columns["Month"];
            col2.Group();
            gridView1.ExpandAllGroups();

            gridStdReport.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            gridStdReport.LookAndFeel.UseDefaultLookAndFeel = false;

            gridStdReportNew.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            gridStdReportNew.LookAndFeel.UseDefaultLookAndFeel = false;

            gridView1.AppearancePrint.HeaderPanel.Options.UseBackColor = true;
            gridView1.AppearancePrint.HeaderPanel.BackColor = Color.White;
            gridView1.AppearancePrint.GroupRow.Options.UseBackColor = true;
            gridView1.AppearancePrint.GroupRow.BackColor = Color.LightGray;
            gridView1.AppearancePrint.GroupRow.ForeColor = Color.Black;
            gridView1.AppearancePrint.GroupRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            col2.Caption = " ";
            return gridStdReportNew;
        }
        XtraStdReport result_card(int stdID)
        {
            GridControl mygridControl = new GridControl();
            if (mgmt_feetype != null)
                mygridControl = feedetails_IN_FeeManagement(stdID, mgmt_feetype, gridStdReport, gridView1);
            else
                mygridControl = feedetails2(stdID, toggle_feetype.EditValue, gridStdReport, gridView1);

            DataTable stdInfo = fun.GetStudentInfo_n(stdID);
            System.Drawing.Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            XtraStdReport report = new XtraStdReport();
            report.PicIogoBox.Image = logo;
            if (Login.Principal_Sign != "")
                report.picPrincipal_Sign.Image = fun.Base64ToImage(Login.Principal_Sign);
            report.LabTitle.Text = school;
            report.GridControl = mygridControl;
            report.GridControl.Width = 100;
            report.LabAddress.Text = fun.GetSettings("address");
            report.LabTel.Text = fun.GetSettings("phone");
            report.LabPrincipal.Text = "( " + fun.GetSettings("principal") + " )";
            report.exam_controler.Text = "( " + fun.GetSettings("controller_exam") + " )";
            if (stdInfo.Rows.Count > 0)
            {
                
                report.labName.Text = stdInfo.Rows[0]["Name"].ToString();
                report.LabFName.Text = stdInfo.Rows[0]["FatherName"].ToString();
                report.LabClass.Text = stdInfo.Rows[0]["Class"].ToString();
                report.LabSection.Text = stdInfo.Rows[0]["Section"].ToString();
                report.LabSAddress.Text = stdInfo.Rows[0]["Address"].ToString();
                report.labRoll.Text = stdInfo.Rows[0]["Roll"].ToString();
                report.LabCell.Text = stdInfo.Rows[0]["FatherPhone"].ToString();
                report.LabDob.Text = stdInfo.Rows[0]["Birthday"].ToString();
                report.LabGender.Text = stdInfo.Rows[0]["Gender"].ToString();
                report.PicStdBox.Image = fun.get_image(@"\Images\Students\", stdInfo.Rows[0]["SrNo"].ToString() + "_std", true, stdInfo.Rows[0]["Gender"].ToString());
            }
            report.xrChart1.Visible = false;
            report.xrLine1.Visible = false;
            report.exam_controler.Visible = false;
            report.LabController2.Visible = false;
            report.PicExam_Sign.Visible = false;
            documentViewer1.DocumentSource = report;

            report.CreateDocument();
            documentViewer1.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            return report;
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "fee_month")
            {
                var VAL = Convert.ToDateTime(e.Value).ToString("M-yyyy");
                if (VAL == "TOTAL")
                {
                    e.DisplayText = "";

                }
                else if (VAL.Split('-')[0].ToString() == "1")
                    e.DisplayText = "Jan - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "2")
                    e.DisplayText = "Feb - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "3")
                    e.DisplayText = "Mar - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "4")
                    e.DisplayText = "Apr - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "5")
                    e.DisplayText = "May - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "6")
                    e.DisplayText = "June - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "7")
                    e.DisplayText = "July - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "8")
                    e.DisplayText = "Aug - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "9")
                    e.DisplayText = "Sep - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "10")
                    e.DisplayText = "Oct - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "11")
                    e.DisplayText = "Nov - " + VAL.Split('-')[1].ToString();
                else if (VAL.Split('-')[0].ToString() == "12")
                    e.DisplayText = "Dec - " + VAL.Split('-')[1].ToString();


            }
        }

        public void HidePanel()
        {
            groupControl1.Visible = false;

        }

        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            if (txtClass.EditValue != null)
                section_list(txtClass.EditValue.ToString());
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtSection.EditValue != null) 
            {
                MakeReport(0);
            } else
            {
                MessageBox.Show("Select Section");
            }
            
        }
        List<XtraStdReport> reports = new List<XtraStdReport>();

        public void AppendReport(XtraStdReport report)
        {
            reports.Add(report);
        }
        private void MakeReport(int student_id)
        {
            reports.Clear();
            fun.loaderform(() =>
            {
                String where = "";
                if (student_id > 0) { where += " AND student.student_id = '" + student_id + "'"; }
                if (txtSection.EditValue != null) { where += " AND student.section_id = '" + txtSection.EditValue + "'"; }

                String query = "SELECT student.student_id, student.name FROM student WHERE student.passout != 1 " + where;
                DataTable table = fun.FetchDataTable(query);
                foreach (DataRow row in table.Rows)
                {
                    int.Parse(LP_std_ID.Text);
                    int std_id = int.Parse(row["student_id"].ToString());
                    XtraStdReport rep = result_card(std_id);
                    AppendReport(rep);
                }
            });
            documentViewer1.DocumentSource = CreateReport();
            documentViewer1.PrintingSystem.Document.AutoFitToPagesWidth = 1;
        }
        public XtraStdReport CreateReport()
        {
            XtraStdReport output = new XtraStdReport();
            foreach (var report in reports)
            {
                report.CreateDocument(false);
                output.Pages.AddRange(report.Pages);
            }
            if (output == null) output = new XtraStdReport();
            output.PrintingSystem.ContinuousPageNumbering = true;
            return output;
        }
    }
}
