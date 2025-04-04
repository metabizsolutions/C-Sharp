using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace SchoolManagementSystem.Attendance
{
    public partial class ClassAttendance : DevExpress.XtraEditors.XtraUserControl
    {
        private static ClassAttendance _instance;
        public static ClassAttendance instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ClassAttendance();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions fun = new CommonFunctions();

        public ClassAttendance()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();
            load_classes();
        }
        void load_classes()
        {
            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";
        }
        private void FillGridClassAttendance()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            int a = fun.GetClassID(txtClass.Text);
            con.Open();
            var tempdate = DateTime.Now.Month + "-" + 1 + "-" + DateTime.Now.Year;
            DateTime date = Convert.ToDateTime(tempdate);
            int sun = 0;
            int total = 0;
            var query = "SELECT student.roll as Roll_No,concat( student.name,' / ',parent.name) as Student,class.name as Class,section.name as Section";
            for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
            {
                var str = "";
                var day = date.DayOfWeek;
                if (date <= DateTime.Now.Date)
                {
                    str = "ELSE 'A'";
                    if (day.ToString() == "Sunday")
                        sun++;
                    total = i;
                }
                if (day.ToString() == "Sunday")
                {
                    query += " ,MAX(CASE dayofmonth(date) WHEN '" + i + "' THEN ' ' ELSE ' ' END) AS '" + i + "'";

                }
                else
                    query += " ,MAX(CASE dayofmonth(date) WHEN '" + i + "' THEN 'P' " + str + " END) AS '" + i + "'";
                date = date.AddDays(1);
                //total = i;
            }
            total = total - sun;
            query += ",count(dayofmonth(date)) as 'Total_P'," + total + "-count(dayofmonth(date)) as 'Total_A' FROM student left join class on (class.class_id = student.class_id) left join section on section.section_id=student.section_id left join attendance on (attendance.student_id = student.student_id) and month(date) = '" + DateTime.Now.Month + "' and year(date)='" + DateTime.Now.Year + "' join parent on parent.parent_id=student.parent_id  " +
                        " where 1 = 1 group by student.student_id order by student.class_id,student.section_id";

            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridClassAttendance.DataSource = null;
            gridView1.Columns.Clear();
            gridClassAttendance.DataSource = CommonFunctions.AutoNumberedTable(table);
            gridView1.BestFitColumns();
            var col = gridView1.Columns["Class"];
            col.Group();
            var col1 = gridView1.Columns["Section"];
            col1.Group();
            GridColumn Column = gridView1.Columns["Sr#"];
            Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column2 = gridView1.Columns["Roll_No"];
            Column2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column3 = gridView1.Columns["Total_P"];
            Column3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            GridColumn Column4 = gridView1.Columns["Total_A"];
            Column4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            var col2 = gridView1.Columns["Sr#"];
            col2.Width = 60;
            gridView1.ExpandAllGroups();

            GridFormatRule gridFormatRuleP = new GridFormatRule();
            GridFormatRule gridFormatRuleA = new GridFormatRule();
            FormatConditionRuleDataBar formatConditionRuleDataBarP = new FormatConditionRuleDataBar();
            FormatConditionRuleDataBar formatConditionRuleDataBarA = new FormatConditionRuleDataBar();

            gridFormatRuleP.Column = gridView1.Columns["Total_P"];
            gridFormatRuleA.Column = gridView1.Columns["Total_A"];
            formatConditionRuleDataBarP.PredefinedName = "Green";
            formatConditionRuleDataBarP.MaximumType = FormatConditionValueType.Number;
            formatConditionRuleDataBarP.Maximum = total;
            formatConditionRuleDataBarP.MinimumType = FormatConditionValueType.Number;
            formatConditionRuleDataBarP.Minimum = 0;
            formatConditionRuleDataBarA.PredefinedName = "Coral";
            formatConditionRuleDataBarA.MaximumType = FormatConditionValueType.Number;
            formatConditionRuleDataBarA.Maximum = total;
            formatConditionRuleDataBarA.MinimumType = FormatConditionValueType.Number;
            formatConditionRuleDataBarA.Minimum = 0;

            gridFormatRuleP.Rule = formatConditionRuleDataBarP;
            gridFormatRuleA.Rule = formatConditionRuleDataBarA;
            gridView1.FormatRules.Add(gridFormatRuleP);
            gridView1.FormatRules.Add(gridFormatRuleA);

            con.Close();

        }
        DataTable table;
        private void btnCAFind_Click(object sender, EventArgs e)
        {
            var secQuery = "";
            if (txtMonth.Text == "" || txtYear.Text == "" || string.IsNullOrEmpty(txtClass.EditValue.ToString()) || string.IsNullOrEmpty(txtSection.EditValue.ToString()))
            {
                MessageBox.Show("Fill all fields....", "Info");
                return;
            }
            if (txtIgnoreDA.Checked == true)
            {
                secQuery = "and student.attendent_sms = '1'";

            }
            var classID = txtClass.EditValue.ToString();//fun.GetClassIDisSession(txtClass.Text, Main_FD.SelectedSession);
            var sectionID = txtSection.EditValue.ToString();//fun.GetSectionIDisClass(txtSection.Text, classID);
            var tempdate = txtMonth.Text + "-" + 1 + "-" + txtYear.Text;
            DateTime date = Convert.ToDateTime(tempdate);

            table = new DataTable();
            MySqlConnection con = new MySqlConnection(Login.constring);
            List<DateTime> dateHolidays = new List<DateTime>();
            List<string> WorkingDays = new List<string>();
            WorkingDays = fun.GetAllWorkingDaysS(Convert.ToInt32(sectionID));
            dateHolidays = fun.GetAllHolidaysAMonth(date,sectionID);
            int a = fun.GetClassID(txtClass.Text);
            con.Open();
            if (a != 0)
            {
                //int sun = 0;
                //int total = 0;
                var query = "";
                var str = "";
                query += "SELECT student.roll as Roll_No,student.name AS student,parent.name AS parent,parent.phone";
                int tDays = DateTime.DaysInMonth(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtMonth.Text));
                for (int i = 1; i <= tDays; i++)
                {
                    str = "";
                    var day = date.DayOfWeek;
                    if (date > DateTime.Now.Date)
                    {
                        //if (day.ToString() == "Sunday")
                        //   sun++;
                        // total = i;
                        query += ", '' as '" + i + "' ";
                    }
                    else if (dateHolidays.Contains(date))
                    {
                        if (day.ToString() == "Sunday")
                            query += " ,MAX(CASE WHEN dayofmonth(date)='" + i + "' and attendance.status='1' THEN 'P/S' ELSE 'S/H' END) AS '" + i + "'";
                        else
                            query += " ,MAX(CASE WHEN dayofmonth(date)='" + i + "' and attendance.status='1' THEN 'P/H' ELSE 'H' END) AS '" + i + "'";
                    }
                    else
                    if (!WorkingDays.Contains(day.ToString()))//== "Sunday" || day.ToString() == "Saturday")
                    {
                        query += " ,MAX(CASE dayofmonth(date) WHEN '" + i + "' THEN 'P/S' ELSE 'S' END) AS '" + i + "'";

                    }
                    else
                        query += ",MAX(CASE WHEN DAYOFMONTH(DATE)='" + i + "' THEN att_s.`abbr` ELSE 'A' END) AS '" + i + "' ";
                        //query += " ,MAX(CASE WHEN dayofmonth(date)='" + i + "' and attendance.status='1' THEN 'P' WHEN dayofmonth(date)='" + i + "' and attendance.status='2' THEN 'L' WHEN dayofmonth(date)='" + i + "' and attendance.status='3' THEN 'S/L' " + str + " END) AS '" + i + "'";

                    date = date.AddDays(1);

                }
                //   total = total - sun;
                query += ",'' as 'Total_P','' as 'Total_A','' as 'Total_L','' as 'Total_SL','' as 'Total_H' " +
                    " FROM student " +
                    " left join attendance on (attendance.student_id = student.student_id) and month(date) = '" + txtMonth.Text + "' and year(date)='" + txtYear.Text + "' " +
                    " LEFT JOIN `attendance_status` AS att_s ON att_s.`status` = attendance.status " +
                    " join parent on parent.parent_id=student.parent_id " +
                    " join class on(class.class_id=student.class_id) left join section on section.section_id=student.section_id where  student.passout != 1 AND student.class_id = '" + classID + "'and student.section_id='" + sectionID + "'  " + secQuery + " group by student.student_id; ";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);

                adp.Fill(table);
                int absent = 0;
                int Leave = 0;
                int present = 0;
                int SLeave = 0;
                int Holiday = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    absent = 0;
                    Leave = 0; SLeave = 0;
                    present = 0;
                    Holiday = 0;
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        var s = table.Rows[i][k].ToString();
                        if (table.Rows[i][k].ToString() == "A")
                        {
                            absent++;
                        }
                        else if (table.Rows[i][k].ToString() == "L")
                        {
                            Leave++;
                        }
                        else if (table.Rows[i][k].ToString() == "P")// || table.Rows[i][k].ToString() == "P/H")
                        {
                            present++;
                        }
                        else if (table.Rows[i][k].ToString() == "H")
                        {
                            Holiday++;
                        }
                        else if (table.Rows[i][k].ToString() == "S/L")
                        {
                            SLeave++;
                            present++;
                        }
                    }
                    table.Rows[i]["Total_A"] = absent;
                    table.Rows[i]["Total_L"] = Leave;
                    table.Rows[i]["Total_P"] = present;
                    table.Rows[i]["Total_SL"] = SLeave;
                    table.Rows[i]["Total_H"] = Holiday;
                }




                gridClassAttendance.DataSource = null;
                gridView1.Columns.Clear();
                gridClassAttendance.DataSource = CommonFunctions.AutoNumberedTable(table);
                gridView1.BestFitColumns();
                GridColumn Column = gridView1.Columns["Sr#"];
                //Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column2 = gridView1.Columns["Roll_No"];
                Column2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column3 = gridView1.Columns["Total_P"];
                Column3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column4 = gridView1.Columns["Total_A"];
                Column4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column5 = gridView1.Columns["Total_L"];
                Column5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                GridColumn Column6 = gridView1.Columns["Total_SL"];
                Column6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                con.Close();
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            showReport();
        }
        void showReport()
        {
            XtraMonthlyAttendanceReport report = new XtraMonthlyAttendanceReport();
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.LabAddress.Text = fun.GetSettings("address");
            report.LabMonth.Text = txtMonth.Text + "-" + txtYear.Text;
            report.labClass.Text = txtClass.Text;
            report.LabSection.Text = txtSection.Text;
            report.GridControl = gridClassAttendance;


            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }

        void PreviewPrintableComponent(IPrintable component, UserLookAndFeel lookAndFeel)
        {
            PrintableComponentLink link = new PrintableComponentLink()
            {
                PrintingSystemBase = new PrintingSystemBase(),
                Component = component,
                Landscape = true,
                PaperKind = PaperKind.A4,
                Margins = new Margins(20, 20, 20, 20)
            };
            link.CreateReportHeaderArea += link_CreateReportHeaderArea;
            link.ShowRibbonPreview(lookAndFeel);
        }

        private void link_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");

            string reportHeader = "\t\t\t\t" + school
                                + "\n\r\n\r                     " + "Session: " + Main_FD.SelectedSession + "     Class: " + txtClass.Text + "     Section: " + txtSection.Text
                                + "\n\r\t\t\t\t\t\t\t            Month: " + txtMonth.Text + "-" + txtYear.Text;
            e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Near);
            e.Graph.Font = new Font("Tahoma", 11, FontStyle.Regular);
            RectangleF rec = new RectangleF(0, 5, e.Graph.ClientPageSize.Width, 80);
            e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
            RectangleF recI = new RectangleF(10, 7, 70, 70);
            e.Graph.DrawImage(logo, recI, BorderSide.None, Color.Transparent);
        }
        private void ClassAttendance_Enter(object sender, EventArgs e)
        {
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            var tempdate = "";
            int tDays;
            if (txtMonth.Text == "")
            {
                tempdate = DateTime.Now.Month + "-" + 1 + "-" + DateTime.Now.Year;
                tDays = DateTime.DaysInMonth(Convert.ToInt32(DateTime.Now.Year), Convert.ToInt32(DateTime.Now.Month));

            }
            else
            {
                tempdate = txtMonth.Text + "-" + 1 + "-" + txtYear.Text;
                tDays = DateTime.DaysInMonth(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtMonth.Text));

            }
            DateTime date = Convert.ToDateTime(tempdate);
            GridView View = sender as GridView;
            for (int i = 1; i <= tDays; i++)
            {
                if (e.Column.FieldName == i.ToString())
                {
                    var day = date.DayOfWeek;
                    if (day.ToString() == "Sunday")
                    {
                        string present = View.GetRowCellDisplayText(e.RowHandle, View.Columns[i.ToString()]);
                        if (present != "")
                        {
                            e.Appearance.BackColor = Color.LightBlue;
                            e.Appearance.ForeColor = Color.DarkBlue;
                        }
                    }
                    else
                    {
                        string present = View.GetRowCellDisplayText(e.RowHandle, View.Columns[i.ToString()]);
                        if (present == "A")
                            e.Appearance.ForeColor = Color.Red;
                        if (present == "P" || present == "P/S")
                            e.Appearance.ForeColor = Color.Green;
                        if (present == "P/H" || present == "H")
                            e.Appearance.ForeColor = Color.Blue;
                        if (present == "L")
                            e.Appearance.ForeColor = Color.Orange;
                        if (present == "S/L")
                            e.Appearance.ForeColor = Color.OrangeRed;
                    }
                }
                date = date.AddDays(1);
            }
        }

        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtClass.EditValue.ToString()))
            {
                txtSection.Properties.DataSource = fun.GetAllSection_dt(txtClass.EditValue.ToString());
                txtSection.Properties.DisplayMember = "name";
                txtSection.Properties.ValueMember = "section_id";
            }
        }
    }
}
