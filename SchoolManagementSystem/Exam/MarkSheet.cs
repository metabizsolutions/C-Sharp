
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SchoolManagementSystem.Exam;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public partial class MarkSheet : DevExpress.XtraEditors.XtraUserControl
    {
        private static MarkSheet _instance;

        public static MarkSheet instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MarkSheet();
                return _instance;
            }
        }
        //ObservableCollection<Result> resultCard = new ObservableCollection<Result>();
        ObservableCollection<Resultinfo> resultInfo = new ObservableCollection<Resultinfo>();
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        CommonFunctions fun = new CommonFunctions();
        String exam_title = "";
        public MarkSheet()
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
        private void btnCMShow_Click(object sender, System.EventArgs e)
        {
            if (txtExam.EditValue == null || txtClass.EditValue == null || txtSection.EditValue == null)
            {
                MessageBox.Show("Fill all fields....", "Info");
                return;
            }
            result();
        }
        void result()
        {
            var classID = Convert.ToInt32(txtClass.EditValue);
            var sectionID = Convert.ToInt32(txtSection.EditValue);

                var query = "SELECT student.student_id as ID, student.name as Student FROM mark " +
                     " join student on(student.student_id = mark.student_id) and mark.class_id = student.class_id " +
                    " where student.class_id = '" + classID + "' and student.section_id='" + sectionID + "' AND student.passout != 1 "
                           + " GROUP BY mark.student_id ,mark.class_id";
                DataTable table = fun.FetchDataTable(query);
                gridClassMarkSheet.DataSource = table;
                gridView1.BestFitColumns();

                var examID = Convert.ToInt32(txtExam.EditValue);
                String sql = "SELECT `name`,`date` FROM exam WHERE  exam_id = '" + examID + "'";
                DataTable mytable = fun.FetchDataTable(sql);
                if (mytable.Rows.Count > 0)
                {
                    try
                    {
                        exam_title = mytable.Rows[0]["name"] + " (" + Convert.ToDateTime(mytable.Rows[0]["date"]).ToString("dd-MM-yyyy") + ")";
                    }
                    catch (Exception e) { }
                }
        }
        public static DataRow row;
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                reports.Clear();
                MakeReport(Convert.ToInt32(row["ID"].ToString()));
            }
        }

        private void MarkSheet_Enter(object sender, EventArgs e)
        {

        }
        void section_list(string classid)
        {
            txtSection.Properties.DataSource = fun.GetAllSection_dt(classid);
            txtSection.Properties.DisplayMember = "name";
            txtSection.Properties.ValueMember = "section_id";
        }

        private void MakeReport(int student_id)
        {
            reports.Clear();
            fun.loaderform(() =>
            {
                
                String where = "";
                if (student_id > 0) { where += " AND student.student_id = '" + student_id + "'"; }
                var classID = txtClass.EditValue;
                var sectionID = txtSection.EditValue;

                String query = "SELECT student.student_id, student.name,student.roll,student.sex,student.class_id,student.section_id, parent.name AS parent,parent.phone, class.name AS class, section.name AS section " +
                                " FROM student " +
                                " INNER JOIN parent ON parent.parent_id = student.parent_id " +
                                " INNER JOIN class ON class.class_id = student.class_id  " +
                                " INNER JOIN section ON section.section_id = student.section_id " +
                                " WHERE student.passout != 1 AND student.section_id ='" + sectionID + "' " + where;
                DataTable table = fun.FetchDataTable(query);
                foreach (DataRow row in table.Rows)
                {
                    XtraStdMarksSheet rep = result_card(row, gridControlResult, gridViewResult);
                    AppendReport(rep);
                }
            });
            documentViewer1.DocumentSource = CreateReport();
            documentViewer1.PrintingSystem.Document.AutoFitToPagesWidth = 1;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < gridView1.DataRowCount; i++) {
            //row = gridView1.GetDataRow(i);
            MakeReport(0);//Convert.ToInt32(row["ID"]));
            //}
        }
        List<XtraStdMarksSheet> reports = new List<XtraStdMarksSheet>();

        public void AppendReport(XtraStdMarksSheet report)
        {
            reports.Add(report);
        }
        public XtraStdMarksSheet CreateReport()
        {
            XtraStdMarksSheet output = new XtraStdMarksSheet();
            foreach (var report in reports)
            {
                report.CreateDocument(false);
                output.Pages.AddRange(report.Pages);
            }
            if (output == null) output = new XtraStdMarksSheet();
            output.PrintingSystem.ContinuousPageNumbering = true;
            return output;
        }
        GridControl grid;
        GridView view;
        public XtraStdMarksSheet result_card(DataRow row, GridControl gridControlResultobj, GridView gridViewResultobj)
        {
            GridControl gridControlResultnew = new GridControl();
            String std_id = row["student_id"].ToString();
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            XtraStdMarksSheet report = new XtraStdMarksSheet();
            report.PicIogoBox.Image = logo;
            if (Login.Principal_Sign != "")
                report.picPrincipal_Sign.Image = fun.Base64ToImage(Login.Principal_Sign);
            if (Login.Exam_Sign != "")
                report.PicExam_Sign.Image = fun.Base64ToImage(Login.Exam_Sign);
            report.LabTitle.Text = school;
            report.labName.Text = row["name"].ToString();
            report.LabFName.Text = row["parent"].ToString();
            report.LabClass.Text = row["class"].ToString();
            report.LabSection.Text = row["section"].ToString();
            report.labstudentid.Text = row["student_id"].ToString();
            report.labRoll.Text = row["roll"].ToString();
            report.txtGender.Text = row["sex"].ToString();
            report.txtResultDate.Text = fun.CurrentDate();
            report.LabCell.Text = row["phone"].ToString();
            report.txtLabelExam.Text = exam_title;

            //Student Extra Field manegement 
            String FieldQuery = "SELECT sf.title, IFNULL(sfv.value,'') AS val FROM student_fields AS sf " +
                                "LEFT JOIN student_fields_values AS sfv ON sfv.field_id = sf.field_id AND sfv.student_id = '{0}'  " +
                                "WHERE sf.is_sheet = 1  " +
                                "ORDER BY sf.field_id ASC  " +
                                "LIMIT 3 ";
            FieldQuery = String.Format(FieldQuery, std_id);
            DataTable FieldTable = fun.FetchDataTable(FieldQuery);
            int field_count = 1;
            foreach (DataRow myrow in FieldTable.Rows)
            {
                if (field_count == 1)
                {
                    report.FieldLabel1.Text = myrow["title"].ToString();
                    report.FieldValue1.Text = myrow["val"].ToString();
                    report.FieldLabel1.Visible = true;
                    report.FieldValue1.Visible = true;
                }
                else if (field_count == 2)
                {
                    report.FieldLabel2.Text = myrow["title"].ToString();
                    report.FieldValue2.Text = myrow["val"].ToString();
                    report.FieldLabel2.Visible = true;
                    report.FieldValue2.Visible = true;
                }
                else
                {
                    report.FieldLabel3.Text = myrow["title"].ToString();
                    report.FieldValue3.Text = myrow["val"].ToString();
                    report.FieldLabel3.Visible = true;
                    report.FieldValue3.Visible = true;
                }
                field_count++;
            }
            report.LabAddress.Text = fun.GetSettings("address");
            report.LabTel.Text = fun.GetSettings("phone");
            report.LabPrincipal.Text = "( " + fun.GetSettings("principal") + " )";
            report.LabController.Text = "( " + fun.GetSettings("controller_exam") + " )";
            var vp = fun.GetSettings("vice_d/n");
            if (vp != "")
            {
                report.picVice_Sign.Visible = true;
                if (Login.Vice_Principal_Sign != "")
                    report.picVice_Sign.Image = fun.Base64ToImage(Login.Vice_Principal_Sign);
                report.labVL.Visible = true;
                report.LabViceD.Visible = true;
                report.LabViceD.Text = vp.Split('/')[0];
                if (vp.Split('/').Length > 1)
                {
                    report.LabViceN.Visible = true;
                    report.LabViceN.Text = "( " + vp.Split('/')[1] + " )";
                }
            }
            report.PicStdBox.Image = fun.get_image(@"\Images\Students\", row["student_id"].ToString() + "_std", false, row["sex"].ToString());
            var examID = txtExam.EditValue.ToString();
            DataTable exam_detail = fun.GetExamDetail_dt(examID);
            var classID = row["class_id"].ToString();
            int sectionID = Convert.ToInt32(row["section_id"]);

            report.txtLabelExam.Text = txtExam.Text;
            if (exam_detail.Rows.Count > 0)
                report.txtExamDate.Text = Convert.ToDateTime(exam_detail.Rows[0]["date"].ToString()).ToString("dd-MM-yyyy");

            
            ObservableCollection<Result> resultCard = new ObservableCollection<Result>();
            DataTable table = fun.classresult("%Avg", Convert.ToInt32(examID), Convert.ToInt32(classID), sectionID,false);
            DataRow[] e_dr = table.Select("ID = '" + std_id + "'");
            double obt = 0;
            foreach (DataRow r in e_dr)
            {
                foreach (DataColumn c in r.Table.Columns)
                {
                    string col = c.ColumnName;
                    if (col == "Result" || col == "Parent" || col == "Rank" || col == "9thMarks" || col == "10thMarks" || col == "ID" || col == "Roll" || col == "Student" || col == "Phone" || col == "Section" || col == "Obtained" || col == "Total" || col == "Average") { }
                    else
                    {
                        obt = Convert.ToDouble(string.IsNullOrEmpty(r[col].ToString()) ? 0 : r[col].ToString() == "A" ? 0 : r[col].ToString() == "N/A"?0: r[col]);
                        Result d = new Result();
                        string[] sub = col.Split('(', ')');
                        d.StdNo = r["ID"].ToString();
                        d.Subject = sub[0].Trim();
                        d.Obtained = r[col].ToString();
                        d.Total = Convert.ToInt32(sub[1].Trim());
                        d.Percentage = (obt / d.Total * 100).ToString("#0.00") + "%";
                        resultCard.Add(d);
                    }
                }

                report.txtObtainMarks.Text = r["Obtained"].ToString();
                report.txtTotalMarks.Text = r["Total"].ToString();
                report.txtPercentage.Text = r["Average"].ToString();
                report.txtRanking.Text = r["Rank"].ToString();
                
            }
            gridControlResultnew.DataSource = null;
            gridControlResultobj.DataSource = null;
            gridViewResultobj.Columns.Clear();

            gridControlResultobj.DataSource = resultCard;
            gridControlResultnew.DataSource = resultCard;
            gridViewResultobj.BestFitColumns();
            ColumnView view = (ColumnView)gridViewResultobj;
            view.ClearColumnsFilter();
            for (int i = 0; i < view.Columns.Count; i++)
            {
                string c = view.Columns[i].FieldName;
                view.Columns[i].Visible = false;
                if (c == "Subject" || c== "Obtained" || c== "Total" || c== "Percentage")
                {
                    view.Columns[i].Visible = true;
                    view.Columns[i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    view.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }
            }
            gridControlResultnew.MainView = gridViewResultobj;
            report.GridControl = gridControlResultnew;
            report.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            report.PrintingSystem.Document.CanChangePageSettings = true;
            return report;
        }

        private void txtClass_EditValueChanged(object sender, EventArgs e)
        {
            if (txtClass.EditValue != null)
                section_list(txtClass.EditValue.ToString());
        }
    }
}
