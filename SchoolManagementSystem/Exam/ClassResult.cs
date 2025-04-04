using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace SchoolManagementSystem.Exam
{
    public partial class ClassResult : DevExpress.XtraEditors.XtraUserControl
    {
        private static ClassResult _instance;

        public static ClassResult instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ClassResult();
                return _instance;
            }
        }
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions fun = new CommonFunctions();
        public ClassResult()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            txtExam.Properties.DataSource = fun.GetAllExams_dt();
            txtExam.Properties.DisplayMember = "name";
            txtExam.Properties.ValueMember = "exam_id";

            txtClass.Properties.DataSource = fun.GetAllClasses_dt();
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";
        }

        ObservableCollection<string> s = new ObservableCollection<string>();
        int examID, classID, sectionID;
        object grid_ID;
        private void btnCRShow_Click(object sender, System.EventArgs e)
        {
            if (txtExam.EditValue == null || txtClass.EditValue == null)
            {
                MessageBox.Show("Fill all fields....!!", "Info");
                return;
            }
            fun.loaderform(() =>
            {
                DataTable table = new DataTable();
                int n = 5;
                examID = Convert.ToInt32(txtExam.EditValue);
                classID = Convert.ToInt32(txtClass.EditValue);
                sectionID = Convert.ToInt32(txtSection.EditValue);
                string subject_wise = fun.GetSettings("Institute_Type");
                if (subject_wise == "Subject Wise Institute")
                {
                    if (txtSection.Text == "")
                    {
                        MessageBox.Show("Please Select section", "Info");
                        return;
                    }
                    table = fun.classresult_bysubject(txtOrderby.Text, examID, classID, sectionID);
                }
                else
                    table = fun.classresult(txtOrderby.Text, examID, classID, sectionID, Convert.ToBoolean(txtIgnoreAbsent.EditValue));

                gridControl1.BeginUpdate();
                try
                {
                    gridView4.Columns.Clear();
                    gridControl1.DataSource = null;
                    gridControl1.DataSource = CommonFunctions.AutoNumberedTable(table);
                }
                finally
                {
                    gridControl1.EndUpdate();
                }
                #region column properites from database
                string query = "Select ID from grids where GridName = 'class_result'";
                grid_ID = fun.Execute_Scaler_string(query);
                if (Convert.ToInt32(grid_ID) <= 0)
                {
                    query = "INSERT INTO `grids`(`GridName`) VALUES ('class_result')";
                    grid_ID = fun.Execute_Insert(query);
                }
                ColumnView view = (ColumnView)gridControl1.FocusedView;
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

                }
                #endregion column properties
            });
        }
        string examDate = "";
        private void simpleButton1_Click(object sender, System.EventArgs e)
        {
            showReport();
            // PreviewPrintableComponent(gridControl1, gridControl1.LookAndFeel);
            // gridControl1.ShowRibbonPrintPreview();
            //var examInfo = fun.GetExamDetailIsSession(txtExam.Text, Main_FD.SelectedSession);
            // examDate = examInfo[0].Salary;
        }
        private void link_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");

            string reportHeader = "                                                                   " + school + "\n\r\n\r"
                + "                  " + "Session: " + Main_FD.SelectedSession + "                         Class: " + txtClass.Text + "                Section: " + txtSection.Text + "\n\r"
                + "                  " + "Examination: " + txtExam.Text + "       Examination Date:  " + examDate;
            e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Near);
            e.Graph.Font = new Font("Tahoma", 11, FontStyle.Regular);
            RectangleF rec = new RectangleF(0, 5, e.Graph.ClientPageSize.Width, 90);
            e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
            RectangleF recI = new RectangleF(5, 7, 70, 70);
            e.Graph.DrawImage(logo, recI, BorderSide.None, Color.Transparent);
        }
        private void simpleButton2_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel Files (*.xlsx)| *.xlsx";
            saveFileDialog1.Title = "Save an ExcelFile";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                gridControl1.ExportToXlsx(fs);
                fs.Close();
            }
        }
        int validRowCount = 0;
        double total = 0;
        double result2 = 0;
        private void gridView1_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            GridGroupSummaryItem item = e.Item as GridGroupSummaryItem;
            GridView view = sender as GridView;
            if (Equals("Pass", item.Tag))
            {

                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                {
                    validRowCount = 0;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                {
                    var a = view.GetRowCellValue(e.RowHandle, "Result").ToString();
                    if (a == "Pass")
                    {
                        validRowCount++;

                    }
                    //var s = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "status"));
                    //if (Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "Status")))
                    //    validRowCount++;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    e.TotalValue = validRowCount;
            }
            if (Equals("Fail", item.Tag))
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                {
                    validRowCount = 0;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                {
                    var a = view.GetRowCellValue(e.RowHandle, "Result").ToString();
                    if (a == "Fail")
                        validRowCount++;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    e.TotalValue = validRowCount;

            }
            if (Equals("Age", item.Tag))
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                {
                    validRowCount = 0; total = 0; result2 = 0;
                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                {
                    total++;
                    var a = view.GetRowCellValue(e.RowHandle, "Result").ToString();
                    if (a == "Pass")
                        validRowCount++;
                    result2 = (validRowCount / total) * 100;

                }
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                {
                    e.TotalValue = Math.Round(result2, 2);
                }
            }
        }
        private void gridView1_ColumnWidthChanged(object sender, ColumnEventArgs e)
        {
            ColumnView view = (ColumnView)gridControl1.FocusedView;
            view.ClearColumnsFilter();
            for (int i = 0; i < view.Columns.Count; i++)
            {
                string name = view.Columns[i].FieldName;
                int width = view.Columns[i].VisibleWidth;
                string query = "UPDATE `grids_columns` SET `Width`=" + width + " WHERE `Grid_ID` ='" + grid_ID + "' and `Column_Name` = '" + name + "'";
                fun.ExecuteInsert(query);
            }
        }
        private void gridView1_DragObjectDrop(object sender, DragObjectDropEventArgs e)
        {
            GridColumn cln = e.DragObject as GridColumn;
            ColumnView view = (ColumnView)gridControl1.FocusedView;
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
        void section_list(string classid)
        {
            txtSection.Properties.DataSource = fun.GetAllSection_dt(classid);
            txtSection.Properties.DisplayMember = "name";
            txtSection.Properties.ValueMember = "section_id";
        }
        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            if (txtClass.EditValue != null)
                section_list(txtClass.EditValue.ToString());
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.Column.FieldName == "Sr#" || e.Column.FieldName == "ID" || e.Column.FieldName == "Student" || e.Column.FieldName == "Section" || e.Column.FieldName == "Roll" ||
                e.Column.FieldName == "Phone" || e.Column.FieldName == "Obtained" || e.Column.FieldName == "Total" || e.Column.FieldName == "Average")
            {
                return;
            }
            if (e.Column.FieldName == "Result")
            {
                string avg = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Average"]);
                decimal val = avg == null ? 0 : fun.onlyDecimalNumber(avg)? Convert.ToDecimal(avg):0;
                if (val >= 80)
                    e.Appearance.ForeColor = Color.DarkSeaGreen;
                else if (val >= 60)
                    e.Appearance.ForeColor = Color.YellowGreen;
                else if (val >= 50)
                    e.Appearance.ForeColor = Color.Orange;
                else
                    e.Appearance.ForeColor = Color.Red;
            }
        }
        void showReport()
        {
            XtraClassResultReport report = new XtraClassResultReport();
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            report.lab_phone.Text = fun.GetSettings("phone");
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.LabAddress.Text = fun.GetSettings("address");
            report.labExam.Text = txtExam.Text;
            report.labClass.Text = txtClass.Text;
            report.LabSection.Text = txtSection.Text;
            report.GridControl = gridControl1;
            report.Landscape = print_orientation.IsOn;
            XtraReport xr_report = report;
            fun.report_print(xr_report);
        }
    }
}
