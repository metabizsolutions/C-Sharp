using DevExpress.LookAndFeel;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace SchoolManagementSystem.Exam
{
    public partial class Multi_ClassResult : UserControl
    {
        private static Multi_ClassResult _instance;

        public static Multi_ClassResult instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Multi_ClassResult();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        public Multi_ClassResult()
        {
            InitializeComponent();
            loadfunctions();
        }

        public void loadfunctions()
        {
            exam_list();
            class_list();
            //allClass.Clear();
            //allClass = fun.GetAllClassisSession(Main_FD.SelectedSession);
            //txtClass.Properties.Items.Clear();
            //foreach (var allclass in allClass)
            //    txtClass.Properties.Items.Add(allclass.Name);
        }
        void class_list()

        {
            string query = "SELECT class_id,name FROM class";
            txtClass.Properties.DataSource = fun.FetchDataTable(query);
            txtClass.Properties.DisplayMember = "name";
            txtClass.Properties.ValueMember = "class_id";
        }
        void exam_list()
        {
            txtExam.Properties.DataSource = fun.GetAllExams_dt();
            txtExam.Properties.DisplayMember = "name";
            txtExam.Properties.ValueMember = "exam_id";
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
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        void load_result()
        {
            GridView view = new GridView();
            gridClassResult.MainView = view;

            string orderby = txtOrderby.Text;
            DataTable table = fun.multi_classresult(orderby, txtExam.EditValue.ToString(), txtClass.EditValue.ToString(),txtSection.EditValue.ToString(), Convert.ToBoolean(txtIgnoreAbsent.EditValue));
            

            if (view.Columns.Count > 0)
                view.Columns.Clear();
            gridClassResult.BeginUpdate();
            try
            {
                gridView1.Columns.Clear();
                gridClassResult.DataSource = null;
                gridClassResult.DataSource = CommonFunctions.AutoNumberedTable(table);
            }
            finally
            {
                gridClassResult.EndUpdate();
            }
        }

        private void btnCRShow_Click(object sender, EventArgs e)
        {
            fun.loaderform(() =>
            {
                load_result();
            });
            
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            showReport();
        }
        void showReport()
        {
            XtraClassResultReport report = new XtraClassResultReport();
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.LabAddress.Text = fun.GetSettings("address");
            report.lab_phone.Text = fun.GetSettings("phone");
            report.labExam.Text = txtExam.Text;
            report.labClass.Text = txtClass.Text;
            report.LabSection.Text = txtSection.Text;
            report.GridControl = gridClassResult;
            report.Landscape = true;
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;

            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }

        DataTable table = new DataTable();

        void load_band_result()
        {
            string sections_list_done = "";
            bool ishalfdetails = toggleSwitch1.IsOn;
            DataTable testcol_tb = new DataTable();
            string orderby = txtOrderby.Text;
           // string secid = txtSection.EditValue.ToString().Split(',')[0];
            //allClass = fun.GetAllSubject(Convert.ToInt32(secid));
            DataTable all_subjects_dt = fun.GetAllSubject_by_section_dt(txtSection.EditValue.ToString());
            string sub_query = "";
            foreach (DataRow dr in all_subjects_dt.Rows)
            {
                // int total = fun.GetSubjectTotalMarks_bystudent(Convert.ToInt32(allClass[i].Salary), Convert.ToInt32(txtExam.EditValue));
                sub_query += ", '' AS `" + dr["name"] + "`";//", '' AS `" + allClass[i].Name + " (" + total + ")" + "`,";
            }
            string query = "SELECT student.student_id as ID,student.roll As Roll ,student.name AS Student ,parent.name AS Parent ,section.section_id,section.name as Section " + sub_query + " " +
                            " ,'0' as Obtained,'0' as Total,'' as `Result` " +
                            " FROM student INNER JOIN parent ON parent.parent_id = student.parent_id " +
                            " join class on class.class_id = student.class_id " +
                            " join section on section.section_id = student.section_id " +
                            " WHERE student.passout = 0 AND student.class_id = '" + txtClass.EditValue + "' and student.section_id in (" + txtSection.EditValue + ") ";
            table = fun.FetchDataTable(query);
            DataColumn newColumn1 = new DataColumn("Average", typeof(double));
            newColumn1.DefaultValue = "0";
            table.Columns.Add(newColumn1);
            testcol_tb = table.Copy();
            if (ishalfdetails) // get total numbers of exam in a row in table
            {
                DataRow dr_row = table.NewRow();
                table.Rows.InsertAt(dr_row, 0);
            }
            for (int i = 0; i < testcol_tb.Rows.Count; i++)
            {
                double obtained = 0;
                double total = 0;
                foreach (DataColumn cl in testcol_tb.Columns)
                {
                    string col = cl.ColumnName.Trim();
                    if (!string.IsNullOrEmpty(table.Rows[i]["ID"].ToString()) && col != "ID" && col != "Roll" && col != "Student" && col != "Parent" && col != "section_id" && col != "Section" && col != "Obtained" && col != "Total" && col != "Average" && col != "Result")
                    {
                        string marks_col = "";
                        if (ishalfdetails)
                            marks_col = "ifnull(max(case mark.`mark_obtained` when -1 then 'A' when -2 then 'NA' else mark.`mark_obtained` end),0) AS Marks";
                        else
                            marks_col = "CONCAT(ifnull(max(case mark.`mark_obtained` when -1 then 'A' when -2 then 'NA' else mark.`mark_obtained` end),0),'/',ifnull(sum(tms.marks),0)) AS Marks";
                        
                        string marks_query = "SELECT ifnull(max(case mark.`mark_obtained` when -1 then 'A' when -2 then 'NA' else mark.`mark_obtained` end),0) as obtained,ifnull(sum(tms.marks),0) as Total, " + marks_col + 
                                                    " FROM mark " +
                                                    " JOIN tbl_mark_subject AS tms ON tms.subject_id=mark.subject_id AND tms.exam_id=mark.exam_id  AND tms.`class_id` = mark.`class_id` AND tms.`section_id` = mark.`section_id` " +
                                                    " INNER JOIN `section_subject` AS ss ON ss.`subject_id` = mark.`subject_id` AND ss.`class_id` = mark.`class_id` AND ss.`section_id` = mark.`section_id` " +
                                                    " INNER JOIN `subject_default` AS sub ON sub.`id` = ss.`subject_id` "+
                                                    " WHERE mark.`student_id` = '" + table.Rows[i]["ID"].ToString() + "' " +// AND mark.`section_id` ='" + table.Rows[i]["section_id"].ToString() + "'" +
                                                    " AND mark.section_id ='" + table.Rows[i]["section_id"].ToString() + "' and sub.name = '" + col + "' " +
                                                    " AND mark.`exam_id` in (" + txtExam.EditValue + ")  GROUP BY mark.`student_id`,mark.`exam_id` ";
                        DataTable marks_dt = fun.FetchDataTable(marks_query);
                        int t = 0;
                        foreach (DataRow dr in marks_dt.Rows)
                        {
                            t++;
                            DataColumnCollection columns = table.Columns;
                            if (!columns.Contains("{" + col + "} T" + t))
                            {
                                DataColumn testcol = new DataColumn("{" + col + "} T" + t, typeof(string));
                                testcol.DefaultValue = "0";
                                table.Columns.Add(testcol);

                            }
                            if (ishalfdetails && !sections_list_done.Contains("{" + col + "} T" + t + (table.Rows[i]["section_id"]).ToString()))
                            {
                                sections_list_done += "," + "{" + col + "} T" + t + (table.Rows[i]["section_id"]).ToString();
                                if ((table.Rows[0]["{" + col + "} T" + t]).ToString() == "0")
                                    table.Rows[0]["{" + col + "} T" + t] = dr["Total"].ToString();
                                else
                                    table.Rows[0]["{" + col + "} T" + t] = (table.Rows[0]["{" + col + "} T" + t]).ToString() + "," + dr["Total"].ToString();
                            }
                            if (dr["obtained"].ToString() == "-1")
                                table.Rows[i]["{" + col + "} T" + t] = "A";
                            else
                                table.Rows[i]["{" + col + "} T" + t] = dr["Marks"].ToString();


                            obtained += Convert.ToDouble(dr["obtained"].ToString() == "A" || dr["obtained"].ToString() == "NA" ? "0" : dr["obtained"].ToString());
                            total += Convert.ToDouble(dr["Total"].ToString());
                        }
                    }
                }
                table.Rows[i]["obtained"] = obtained;
                table.Rows[i]["Total"] = total;
                if (total > 0)
                    table.Rows[i]["Average"] = ((obtained / total) * 100).ToString("#0.00");

                string rest = "SELECT grade.comment FROM grade WHERE grade.mark_from <= '" + table.Rows[i]["Average"] + "' AND grade.mark_upto > '" + table.Rows[i]["Average"] + "' LIMIT 1";
                object val_result = fun.Execute_Scaler_string(rest);
                table.Rows[i]["Result"] = val_result;
            }

            var all_rows = from row in table.AsEnumerable()
                           orderby row["Average"] descending
                           select row;
            if (orderby != "Roll#")
            {
                //n = 6;
                DataColumn newColumn = new DataColumn("Rank", typeof(int));
                newColumn.DefaultValue = "0";
                table.Columns.Add(newColumn);
                int R = 0;
                decimal rs = -1;
                //DataRow[] foundRows =table.Select("Average DESC");
                //table = foundRows.CopyToDataTable();
                DataRow[] dataRows = table.Select().OrderByDescending(u => u["Average"]).ToArray();
                table = dataRows.CopyToDataTable();
                foreach (DataRow row in table.Rows)
                {
                    if (rs != Convert.ToDecimal(row["Obtained"]))
                        R++;
                    row["Rank"] = R;
                    rs = Convert.ToDecimal(row["Obtained"]);
                    //if (rs == 0)
                    //    break;
                }
            }
            if (ishalfdetails) // hide and show exam totla number acording to full and half details
            {
                DataRow newRow = table.NewRow();
                int j = 0;
                foreach (DataRow row in table.Rows)
                {
                    if (row["section_id"].ToString() == "")
                    {
                        foreach (DataColumn cl in table.Columns)
                        {
                            string col = cl.ColumnName.Trim();
                            if (col.Contains('{') && !string.IsNullOrEmpty(row[col].ToString()))
                            {
                                string value = row[col].ToString();
                                newRow[col] = value;
                            }
                        }
                        break;
                    }
                    j++;
                }
                table.Rows.RemoveAt(j);
                table.Rows.InsertAt(newRow, 0);
            }
            gridClassResult.BeginUpdate();
            try
            {
                gridView1.Columns.Clear();
                gridClassResult.DataSource = null;
                gridClassResult.DataSource = CommonFunctions.AutoNumberedTable(table);
            }
            finally
            {
                gridClassResult.EndUpdate();
            }
        }
        private void btn_Banded_result_Click(object sender, EventArgs e)
        {
            fun.loaderform(() =>
            {
                load_band_result();

                BandedGridView view = new BandedGridView();
                view.OptionsBehavior.AutoPopulateColumns = false;
                view.OptionsView.ColumnAutoWidth = false;
                view.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Fast;
                gridClassResult.MainView = view;

                //GridBand band = new GridBand() { Caption = "student_info" };
                foreach (DataColumn cl in table.Columns)
                {
                    string col = cl.ColumnName.Trim();
                    //GridBand band = new GridBand() { Caption = "Main" };
                    if (col.Contains("{"))
                    {
                        //string[] testsplit = col.Split('{', '}');
                        //BandedGridColumn banded_col = new BandedGridColumn() { FieldName = col, Caption = testsplit[1], Visible = true };
                        //view.Columns.Add(banded_col);
                        //banded_col.OwnerBand = view.Bands[testsplit[0].Trim()];
                    }
                    else if (col != "ID" && col != "Roll" && col != "Student" && col != "Parent" && col != "section_id" && col != "Section" && col != "Obtained" && col != "Total" && col != "Average" && col != "Result" && col != "Rank")
                    {
                        GridBand band = new GridBand() { Caption = col.Trim() };
                        view.Bands.Add(band);
                    }
                    else
                    {
                        GridBand band = new GridBand() { Caption = col.Trim() };
                        view.Bands.Add(band);
                        BandedGridColumn banded_col = new BandedGridColumn() { FieldName = col, Visible = true };
                        view.Columns.Add(banded_col);
                        view.Columns[col].Width = view.Columns[col].GetBestWidth();

                        banded_col.OwnerBand = band;

                        if (col == "ID" || col == "Parent" || col == "section_id")
                            band.Visible = false;
                    }
                }

                foreach (GridBand b in view.Bands)
                {
                    foreach (DataColumn cl in table.Columns)
                    {
                        string bandname = b.Caption;
                        string col = cl.Caption;
                        if (col.Contains("{") && col.Contains(bandname))
                        {
                            string[] testsplit = col.Split('{', '}');
                            BandedGridColumn banded_col = new BandedGridColumn() { FieldName = col, Caption = testsplit[2], Visible = true };
                            view.Columns.Add(banded_col);
                            view.Columns[col].Width = view.Columns[col].GetBestWidth();
                            banded_col.OwnerBand = b;
                        }

                    }
                }
            });
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            fun.exporttoexcel(gridClassResult);
        }
    }
}
