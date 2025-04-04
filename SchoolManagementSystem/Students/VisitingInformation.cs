using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout;
using DevExpress.XtraReports.UI;
using MySql.Data.MySqlClient;
using SchoolManagementSystem.Fees;
using SchoolManagementSystem.Principal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace SchoolManagementSystem.Students
{
    public partial class VisitingInformation : DevExpress.XtraEditors.XtraUserControl
    {
        private static VisitingInformation _instance;

        public static VisitingInformation instance
        {
            get
            {
                if (_instance == null)
                    _instance = new VisitingInformation();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();
        RepositoryItemSearchLookUpEdit riSearch;
        ObservableCollection<AllValues> allSchool;
        RepositoryItemComboBox riComboC;
        Dictionary<int, TextEdit> txtedit_array = new Dictionary<int, TextEdit>();

        public VisitingInformation()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnAddSchool.Enabled = false;
            btnNewAppointment.Enabled = false;
            if (add)
            {
                btnAddSchool.Enabled = true;
                btnNewAppointment.Enabled = true;
            }
            btnSave.Enabled = false;
            gridView1.OptionsBehavior.Editable = false;
            if (Edit)
            {
                btnSave.Enabled = true;
                gridView1.OptionsBehavior.Editable = true;
            }
        }
        public void loadfunctions()
        {

            layoutControl1.Visible = false;
            fun.DateFormat(txtDate);
            allClass.Clear();
            CmboClass.Properties.Items.Clear();

            txtClass.Properties.Items.Clear();
            allClass = fun.GetAllClassisSession(fun.GetDefaultSessionName());
            foreach (var allclass in allClass)
            {
                txtClass.Properties.Items.Add(allclass.Name);
                CmboClass.Properties.Items.Add(allclass.Name);
                CmboClass.Properties.Items.Add(allclass.Name);
            }

            //add extra items 
            layoutControlGroupAO.Clear();
            String query = "SELECT field_id,title FROM student_fields ORDER BY title DESC ";
            DataTable table = fun.FetchDataTable(query);
            TextEdit textEdit1;
            LayoutControlItem item1;
            int field_id;
            foreach (DataRow row in table.Rows)
            {
                item1 = layoutControlGroupAO.AddItem();
                textEdit1 = new TextEdit();
                textEdit1.Name = "txt_" + row["field_id"].ToString();
                field_id = Convert.ToInt32(row["field_id"]);
                txtedit_array[field_id] = textEdit1;
                item1.Control = textEdit1;
                item1.Text = row["title"].ToString();
            }


            FillGrid();
            loadSchool();
        }
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            var pro = txtProspectus.Text == "Yes" ? '1' : '0';
            var status = txtStatus.Text == "Active" ? '1' : '0';
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            int classID = fun.GetClassIDisSession(txtClass.Text, fun.GetDefaultSessionName());
            String school_id = "0";
            if (txtPSchool.Text != "" && txtPSchool.Text != "[EditValue is null]") {
                school_id = txtPSchool.EditValue.ToString();
            }
            String query = "INSERT into visitor(Name,FName,Date,School,Sms,ClassId,Prospectus,Reference,Payment,Status,CNIC,FatherOcc,Cell,Address,Remarks,birthday,sex) " +
                " VALUES('" + txtName.Text + "','" + txtFatherName.Text + "','" + fun.ToInDate(txtDate.Text) + "','" + school_id + "','" + txtSMS.Text + "','" + classID + "','" + pro + "','" + txtReference.Text + "','" + txtPayment.Text + "','" + status + "','" + txtCNIC.Text + "','" + txtFatherOcc.Text + "','" + txtCell.Text + "','" + txtAddress.Text + "','" + txtRemarks.Text + "','" + txtDOB.Text + "','" + dropSex.Text + "')";

            var student_id = fun.ExecuteInsert(query);
            string smsenabales = fun.GetSettings("enable_feecollection_sms");
            if (smsenabales == "1")
            {
                string message = fun.GetSettings("visitor_sms");
                message = message.Replace("[visitorname]", txtName.Text).Replace("[schoolname]", fun.GetSettings("system_name"));
                query = "INSERT INTO `sms_que`(`mobile`, `sms`, `status`, `ondate`) VALUES ('" + txtSMS.Text + "','" + message + "',0," + fun.time() + ")";
                fun.ExecuteInsert(query);
            }
            //inert extra fields 
            TextEdit txtedit;
            foreach (int field_id in txtedit_array.Keys)
            {
                txtedit = txtedit_array[field_id];
                query = "INSERT INTO visiter_fields_values SET student_id = '{0}',field_id = '{1}',`value` = '{2}' ";
                query = String.Format(query, student_id, field_id, txtedit.Text.ToString());
                fun.ExecuteQuery(query);

            }

            if (picBoxStudent.Image != null)
            {
                string name = student_id.ToString() + "_vis";
                fun.saveImage(name, fileOpen, picBoxStudent, @"\Images\Students\");
                //Path.GetDirectoryName(Application.ExecutablePath).ToString() + @"\Images\Students\";
                string imgaepath = name + ".Jpeg";
                query = "UPDATE visitor SET `image` = '" + imgaepath + "' WHERE VID = '" + student_id + "';";
                fun.ExecuteQuery(query);
            }
            FillGrid();
            Empty();
        }
        private void Empty() {
            btnSave.Enabled = true;
            txtName.Text = "";
            txtFatherName.Text = "";
            txtFatherOcc.Text = "";
            txtPayment.Text = "";
            txtProspectus.Text = "";
            txtReference.Text = "";
            txtRemarks.Text = "";
            txtSMS.Text = "";
            txtCNIC.Text = "";
            txtCell.Text = "";
            txtAddress.Text = "";
            picBoxStudent.Image = null;
        }
        private void loadSchool()
        {
            allSchool = new ObservableCollection<AllValues>();
            allSchool = fun.GetAllSchool();
            txtPSchool.Text = "0";
            txtPSchool.Properties.DataSource = allSchool;
            txtPSchool.Properties.DisplayMember = "Name";
            txtPSchool.Properties.ValueMember = "ID";

        }
        int[] cat_ids;
        String[] cat_titles;
        void FillGrid()
        {
            gridView1.GroupSummary.Clear();

            //extra collumns
            string query_cat = "SELECT field_id,title FROM student_fields ORDER BY title ASC  ";
            DataTable table_cat = fun.GetQueryTable(query_cat);
            cat_ids = new int[table_cat.Rows.Count];
            cat_titles = new String[table_cat.Rows.Count];
            int count = 0;
            int fee_cat_id;
            String extra_col = "";
            foreach (DataRow row in table_cat.Rows)
            {
                fee_cat_id = Convert.ToInt32(row["field_id"].ToString());
                cat_ids[count] = fee_cat_id;
                cat_titles[count] = row["title"].ToString();
                extra_col += ",(SELECT IFNULL(`value`,'') FROM visiter_fields_values WHERE field_id = '" + fee_cat_id + "' AND student_id = visitor.VID ORDER BY relation_id DESC LIMIT 1) AS '" + row["title"].ToString() + "'";
                count++;
            }

            String sql = "SELECT visitor.Date,VID,visitor.Name AS `Candidate`,CNIC, FName AS `Father`,FatherOcc AS `Occupation`  ,School,ClassId AS Class,Prospectus,Payment, " +
                        " Status,Cell ,visitor.Address,Reference,Remarks,Sms AS SMS,birthday, sex AS Gender "+ extra_col +
                        " FROM visitor " +
                        " ORDER BY VID DESC ";

            DataTable table = fun.FetchDataTable(sql);

            if (table.Rows.Count > 0)
            {
                gridVisitor.DataSource = null;
                gridView1.Columns.Clear();
                gridVisitor.DataSource = CommonFunctions.AutoNumberedTable(table);
                gridView1.BestFitColumns();

                GridColumnSummaryItem item0 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Sr#", "{0}");
                gridView1.Columns["Sr#"].Summary.Add(item0);

                gridView1.Columns["VID"].Visible = false;
                //gridView1.Columns["Date"].OptionsColumn.ReadOnly = true;

                GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Payment", "{0}");
                gridView1.Columns["Payment"].Summary.Add(item1);

                allSchool = new ObservableCollection<AllValues>();
                riSearch = new RepositoryItemSearchLookUpEdit();
                allSchool = fun.GetAllClasses();
                riSearch.DataSource = allSchool;
                riSearch.ValueMember = "ID";
                riSearch.DisplayMember = "Name";
                gridView1.Columns["Class"].ColumnEdit = riSearch;

                allSchool = new ObservableCollection<AllValues>();
                riSearch = new RepositoryItemSearchLookUpEdit();
                allSchool = fun.GetAllSchool();
                riSearch.DataSource = allSchool;
                riSearch.ValueMember = "ID";
                riSearch.DisplayMember = "Name";
                gridView1.Columns["School"].ColumnEdit = riSearch;
            }
        }

        private void btnnPrint_Click(object sender, System.EventArgs e)
        {
            XtraStudentAdmissionReport report = new XtraStudentAdmissionReport();
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            report.PicIogoBox.Image = logo;
            report.LabTitle.Text = school;
            report.LabAddress.Text = fun.GetSettings("address");
            report.LabClass.Text = "";
            report.GridControl = gridVisitor;

            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }
        int VID;
        DataRow row;
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //FillGrid();
            row = null;
            row = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            var Pro = row[10].ToString() == "True" ? "Yes" : "No";
            var Status = row[12].ToString() == "True" ? "Active" : "Non Active";

            VID = int.Parse(row[2].ToString());
            txtName.Text = row[3].ToString();
            txtFatherName.Text = row[5].ToString();
            txtFatherOcc.Text = row[6].ToString();
            txtPayment.Text = row[11].ToString();
            txtProspectus.Text = Pro;
            txtReference.Text = row[15].ToString();
            txtRemarks.Text = row[16].ToString();
            txtPSchool.Text = row[8].ToString();
            txtSMS.Text = row[17].ToString();
            txtStatus.Text = Status;
            txtDate.Text = row[1].ToString();
            txtCNIC.Text = row[4].ToString();
            txtClass.Text = row[9].ToString();
            txtCell.Text = row[13].ToString();
            txtAddress.Text = row[14].ToString();
            txtDOB.Text = row[18].ToString();
            dropSex.Text = row[19].ToString();


        }

        private void btnNew_Click(object sender, System.EventArgs e)
        {
            layoutControl1.Visible = false;
        }

        
        private void UpsertCategory(String student_id, int cat_id, String title, String val)
        {
            String query = "SELECT * FROM visiter_fields_values WHERE field_id = '{0}' AND student_id = '{1}'";
            query = String.Format(query, cat_id, student_id);
            DataTable table = fun.FetchDataTable(query);
            if (table.Rows.Count > 0)
            {
                query = "UPDATE visiter_fields_values SET `value` = '{0}' WHERE field_id = '{1}' AND student_id = '{2}'";
                query = String.Format(query, val, cat_id, student_id);
            }
            else
            {
                query = "INSERT INTO visiter_fields_values SET `value` = '{0}', field_id = '{1}', student_id = '{2}'";
                query = String.Format(query, val, cat_id, student_id);
            }

            fun.ExecuteQuery(query);
        }

        private void btnNewAppointment_Click(object sender, System.EventArgs e)
        {
            VisitorAppointment app = new VisitorAppointment();
            app.ShowDialog();
        }

        private void VisitingInformation_Enter(object sender, System.EventArgs e)
        {
            FillGrid();
        }
        public static DataRow stdRow { get; set; }
        private void btnFeeRecipt_Click(object sender, System.EventArgs e)
        {
            row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
                stdRow = row;
            ProMasterFeeReciptReport MReport = new ProMasterFeeReciptReport();

            ReportPrintTool printTool = new ReportPrintTool(MReport);
            printTool.PreviewForm.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        }
        public ProFeeRecipt showReport()
        {
            DataRow data = stdRow;

            ProFeeRecipt report = new ProFeeRecipt();
            if (data != null)
            {
                var Pro = data[9].ToString() == "True" ? "Yes" : "No";
                var Status = data[11].ToString() == "True" ? "Active" : "Non Active";
                var VID = data["VID"].ToString();

                Image logo = fun.Base64ToImage(Login.Logo);
                var school = fun.GetSettings("system_title");
                report.PicIogoBox.Image = logo;
                report.LabTitle.Text = school;
                report.LabAddress.Text = fun.GetSettings("address");
                report.LabTel.Text = fun.GetSettings("phone");
                report.LabSID.Text = VID;
                report.LabSName.Text = data[3].ToString();
                int class_id = data[8].ToString() != "" ? Convert.ToInt32(data[8].ToString()) : 0;
                String Class = class_id > 0 ? fun.GetClassName(class_id) : "";
                report.LabSClass.Text = Class;
                report.LabDate.Text = Convert.ToDateTime(data[1]).ToString("dd-M-yyyy");
                report.labPro.Text = Pro;
                report.labStatus.Text = Status;
                report.labFee.Text = data[10].ToString();
            }
            return report;
        }
        
        private void btnAddSchool_Click(object sender, EventArgs e)
        {
            AddSchool sch = new AddSchool();
            sch.ShowDialog();
            loadSchool();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (filename == "" || CmboClass.Text == "")
            {
                MessageBox.Show("File and Class are required fields! ");
            }
            else
            {
                fun.ImportVisitFile(filename, CmboClass.Text);
                FillGrid();
            }
        }

        string filename = "";
        private void buttonEdit1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog1.Title = "Open file";
                openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filename = openFileDialog1.FileName;
                    buttonEdit1.Text = filename;
                }
            }
        }

        private void btnSample_Click(object sender, EventArgs e)
        {
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string xslLocation = Path.Combine(executableLocation, "StudentVisit.xlsx");
            Process.Start(xslLocation);
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                String query;
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                
                var Prospectus = (row["Prospectus"] != DBNull.Value && Convert.ToBoolean(row["Prospectus"]) == true) ? 1 : 0;
                var Status = (row["Status"] != DBNull.Value && Convert.ToBoolean(row["Status"]) == true) ? 1 : 0;
                query = "UPDATE visitor SET `Date` = '{0}', Name = '{1}',CNIC='{2}',FName = '{3}',FatherOcc = '{4}',School='{5}',Prospectus = '{6}',Payment='{7}',`Status` = '{8}',Cell='{9}',Address='{10}',Reference='{11}', Remarks='{12}',Sms='{13}',birthday='{14}',sex='{15}',ClassId='{16}' WHERE VID = '{17}' ";
                query = String.Format(query, row["Date"], row["Candidate"], row["CNIC"], row["Father"], row["Occupation"], row["School"], Prospectus, row["Payment"], Status, row["Cell"], row["Address"], row["Reference"], row["Remarks"], row["SMS"], row["birthday"], row["Gender"],row["Class"], row["VID"]);
                fun.ExecuteQuery(query);


                for (int i = 0; i < cat_titles.Length; i++)
                {
                    int cat_id = cat_ids[i];
                    string title = cat_titles[i];
                    UpsertCategory(row["VID"].ToString(), cat_id, title, row[title].ToString());

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddVisit_Click(object sender, EventArgs e)
        {
            layoutControl1.Visible = true;
        }

        OpenFileDialog fileOpen = new OpenFileDialog();
        private void BtnLoadImage_Click(object sender, EventArgs e)
        {
            fileOpen.Title = "Open Image file";
            fileOpen.Filter = "JPEG Files (*.Jpeg)| *.Jpeg";

            if (fileOpen.ShowDialog() == DialogResult.OK)
            {
                picBoxStudent.Image = Image.FromFile(fileOpen.FileName);
            }
        }

        private void BtnTakeImage_Click(object sender, EventArgs e)
        {
            TakeImage t = new TakeImage();

            t.ShowDialog();
            picBoxStudent.Image = t.imgCapture.Image;
        }
    }
}
