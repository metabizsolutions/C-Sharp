using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;

namespace SchoolManagementSystem.Class
{
    public partial class ManageStaff : UserControl
    {
        private static ManageStaff _instance;
        public static ManageStaff instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ManageStaff();
                return _instance;
            }
        }
        public ManageStaff()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {

        }
        public void loadfunctions()
        {
            FillGridTeacher();
        }
        CommonFunctions fun = new CommonFunctions();
        /*private void FillGridTeacher()
        {
            string query = "SELECT teacher_id,name,FName,CNIC,sex,(CASE `birthday` WHEN '0000-00-00' THEN '0001-01-01' ELSE `birthday` END) as DOB,phone," +
                " (CASE `JoiningDate` WHEN '0000-00-00' THEN '0001-01-01' ELSE `JoiningDate` END) as JoiningDate, " +
                            " attendent_sms,phone2 as Guardian_Cell,salary,address,timeStart,timeEnd, " +
                            " designation,staff_type,subject_code,bank_account,passout,EndingDate,password " +
                            " from teacher where passout=0";
            DataTable table = fun.FetchDataTable(query);
            Image teacher_img = null;
            sta.Clear();
            foreach (DataRow dr in table.Rows)
            {
                staff s = new staff();
                    teacher_img = fun.get_image_teacher(@"\Images\Teachers\", dr["teacher_id"].ToString(), true, dr["sex"].ToString());
                
                s.ID = Convert.ToInt32(dr["teacher_id"]);
                s.Photo = teacher_img;
                s.Name = dr["Name"].ToString();
                s.FatherName = dr["FName"].ToString();
                s.ID = Convert.ToInt32(dr["teacher_id"]);
                s.CNIC = dr["CNIC"].ToString();
                s.Gender = dr["sex"].ToString();
                s.DOB = dr["DOB"].ToString();
                s.Phone = dr["phone"].ToString();
                s.JoiningDate = dr["JoiningDate"].ToString();
                s.Guardian_Cell = dr["Guardian_Cell"].ToString();
                s.salary= dr["salary"].ToString();
                s.address = dr["address"].ToString();
                s.timeStart= dr["timeStart"].ToString();
                s.timeEnd= dr["timeEnd"].ToString();
                s.designation = dr["designation"].ToString();
                s.staff_type = dr["staff_type"].ToString();
                s.subject_code = dr["subject_code"].ToString();
                s.bank_account = dr["bank_account"].ToString();
                s.EndingDate = dr["EndingDate"].ToString();
                s.password = dr["password"].ToString();
                sta.Add(s);
            }

            staff_grid.DataSource = sta;
        }
        */
        private void FillGridTeacher()
        {
            string query = "SELECT teacher_id,name as Name,FName as FatherName,CNIC,sex as Gender,(CASE `birthday` WHEN '0000-00-00' THEN '0001-01-01' ELSE `birthday` END) as DOB,Phone," +
                    " (CASE `JoiningDate` WHEN '0000-00-00' THEN '0001-01-01' ELSE `JoiningDate` END) as JoiningDate, " +
                                " attendent_sms,phone2 as Guardian_Cell,salary,address,timeStart,timeEnd, " +
                                " designation,staff_type,subject_code,bank_account,passout,EndingDate,password " +
                                " from teacher where passout=0";
            DataTable table = fun.FetchDataTable(query);
            query = "select * from teacher_education";
            DataTable edu_dt = fun.FetchDataTable(query);
            query = "select * from teacher_experience";
            DataTable exp_dt = fun.FetchDataTable(query);
            table.Columns.Add("Photo", typeof(Image));
            int e = 0;
            foreach (DataRow dr in table.Rows)
            {
                dr["Photo"] = fun.get_image_teacher(@"\Images\Teachers\", dr["teacher_id"].ToString(), true, dr["Gender"].ToString());
                DataRow[] e_dr = edu_dt.Select("teacher_id = '" + dr["teacher_id"] + "'");//edu_dt.AsEnumerable().s(tt => tt.Field<string>("teacher_id") == dr["teacher_id"].ToString());
                e = 0;
                foreach (DataRow e_r in e_dr)
                {
                    e++;
                    for (int j = 0; j < e_r.Table.Columns.Count; j++)
                    {
                        string col = e_r.Table.Columns[j].ColumnName;
                        if (col != "edu_id" && col != "teacher_id" && col != "obtain")
                        {
                            DataColumnCollection columns = table.Columns;
                            if (col == "total")
                            {
                                if (!columns.Contains("marks" + e))
                                    table.Columns.Add("marks" + e, typeof(string));
                                dr["marks" + e] = e_r["obtain"].ToString() + "/" + e_r["total"].ToString();
                            }
                            else
                            {
                                if (!columns.Contains(col + e))
                                    table.Columns.Add(col + e, typeof(string));
                                dr[col + e] = e_r[e_r.Table.Columns[j].ToString()];
                            }
                        }
                    }
                }
                DataRow[] ex_dr = exp_dt.Select("teacher_id = '" + dr["teacher_id"] + "'");//
                e = 0;
                foreach (DataRow ex_r in ex_dr)
                {
                    e++;
                    for (int j = 0; j < ex_r.Table.Columns.Count; j++)
                    {
                        string col = ex_r.Table.Columns[j].ColumnName;
                        if (col != "exp_id" && col != "teacher_id" && col != "from_date")
                        {
                            DataColumnCollection columns = table.Columns;
                            if (col == "to_date")
                            {
                                if (!columns.Contains("com_period" + e))
                                    table.Columns.Add("com_period" + e, typeof(string));
                                dr["com_period" + e] = ex_r["from_date"].ToString() + "-" + ex_r["to_date"].ToString();
                            }
                            else
                            {
                                if (!columns.Contains(col + e))
                                    table.Columns.Add(col + e, typeof(string));
                                dr[col + e] = ex_r[ex_r.Table.Columns[j].ToString()];
                            }
                        }
                    }
                }
            }

            staff_grid.DataSource = table;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            btnNew.Enabled = false;
            btnTedit.Enabled = false;
            //gridView1.AddNewRow();
            //int rowHandler = gridView1.FocusedRowHandle;
            //if (gridView1.IsNewItemRow(rowHandler))
            //{
            using (teacher_form de = new teacher_form(-1))
            {
                if (de.ShowDialog() == DialogResult.Yes) { }
                else
                {
                    FillGridTeacher();
                    /*if (de.inserted_student_id > 0)
                    {
                        is_add_student = true;
                        ColumnView view = (ColumnView)gridAddStudents.FocusedView;
                        DataTable dt = student_details_tb(" and SrNo = '" + de.inserted_student_id + "'");
                        for (int i = 0; i < view.Columns.Count; i++)
                        {
                            string name = view.Columns[i].FieldName;
                            string val = dt.Rows[0][name].ToString();
                            if (name == "addmission_date")
                            { }// gridView1.SetRowCellValue(rowHandler, gridView1.Columns[name],Convert.ToDateTime(val).ToString());
                            else
                                gridView1.SetRowCellValue(rowHandler, gridView1.Columns[name], val);
                        }
                    }*/
                }
            }
            //}
            btnNew.Enabled = true;
            btnTedit.Enabled = true;
            //is_add_student = false;
        }

        private void btnTedit_Click(object sender, EventArgs e)
        {
            btnNew.Enabled = false;
            btnTedit.Enabled = false;
            DataRow dr = tileView2.GetFocusedDataRow();
            if (dr != null)
            {
                using (teacher_form de = new teacher_form(Convert.ToInt32(dr["teacher_id"])))
                {
                    if (de.ShowDialog() == DialogResult.Yes) { }
                    else
                    {
                        FillGridTeacher();
                        /*if (de.inserted_student_id > 0)
                        {
                            is_add_student = true;
                            ColumnView view = (ColumnView)gridAddStudents.FocusedView;
                            DataTable dt = student_details_tb(" and SrNo = '" + de.inserted_student_id + "'");
                            for (int i = 0; i < view.Columns.Count; i++)
                            {
                                string name = view.Columns[i].FieldName;
                                string val = dt.Rows[0][name].ToString();
                                if (name == "addmission_date")
                                { }// gridView1.SetRowCellValue(rowHandler, gridView1.Columns[name],Convert.ToDateTime(val).ToString());
                                else
                                    gridView1.SetRowCellValue(rowHandler, gridView1.Columns[name], val);
                            }
                        }*/
                    }
                }
            }
            btnNew.Enabled = true;
            btnTedit.Enabled = true;
        }

        private void BtnTDelete_Click(object sender, EventArgs e)
        {
            DataRow dr = tileView2.GetFocusedDataRow();
            if (dr != null)
            {
                string query = "select * from teacher where teacher_id = '"+dr["teacher-id"]+"'";
                DataTable dt = fun.FetchDataTable(query);
                if(dt.Rows.Count > 0)
                {
                    query = " select * from ac_transaction ladger_from = '" + dt.Rows[0]["ladger_id"] + "' or ladger_to = '" + dt.Rows[0]["ladger_id"] + "'";
                    DataTable ac_dt = fun.FetchDataTable(query);
                    if(ac_dt.Rows.Count > 0)
                    {
                        MessageBox.Show("This Staff has some data in account you cannot delete this staff only passout this staff", "staff info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                if (XtraMessageBox.Show($"Are you sure want to delete Staff Name = '" + dr["Name"] + "' ?", "Warnning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    query = "delete from teacher where teacher_id = '" + dr["teacher_id"] + "';";
                    query += "delete from teacher_education where teacher_id = '" + dr["teacher_id"] + "';";
                        query += "delete from teacher_experience where teacher_id = '" + dr["teacher_id"] + "';";
                    fun.ExecuteQuery(query);
                    tileView2.DeleteRow(tileView2.FocusedRowHandle);
                }
            }
        }

        private void btn_staff_left_Click(object sender, EventArgs e)
        {
            DataRow dr = tileView2.GetFocusedDataRow();
            if(dr == null)
            {
                XtraMessageBox.Show("Please select staff and try again", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (XtraMessageBox.Show($"Are you sure Staff Name = '" + dr["Name"] + "' is leaving your institute ?", "Warnning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string query = "UPDATE teacher set passout='1',passout_date='"+DateTime.Now.ToString("yyyy-MM-dd")+"' where teacher_id = '" + dr["teacher_id"] + "'";
                fun.ExecuteQuery(query);
                tileView2.DeleteRow(tileView2.FocusedRowHandle);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PreviewPrintableComponent(staff_grid, staff_grid.LookAndFeel);
        }

        void PreviewPrintableComponent(IPrintable component, UserLookAndFeel lookAndFeel)
        {
            PrintingSystem ps = new PrintingSystem();
            ps.Document.AutoFitToPagesWidth = 1;
            PrintableComponentLink link = new PrintableComponentLink()
            {
                PrintingSystemBase = new PrintingSystemBase(),
                Component = component,
                Landscape = true,
                PaperKind = PaperKind.A4,
                Margins = new Margins(20, 20, 20, 20)
            };
            link.CreateReportHeaderArea += link_CreateReportHeaderArea;
            link.PrintingSystem = ps;
            link.ShowRibbonPreview(lookAndFeel);
        }
        private void link_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
            Image logo = fun.Base64ToImage(Login.Logo);
            var school = fun.GetSettings("system_title");
            
            string reportHeader = school + "\n\r" + "Teachers List";
            e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Near);
            e.Graph.Font = new Font("Tahoma", 12, FontStyle.Regular);
            RectangleF rec = new RectangleF(60, 5, e.Graph.ClientPageSize.Width, 50);
            e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
            RectangleF recI = new RectangleF(5, 5, 50, 50);
            e.Graph.DrawImage(logo, recI);
        }
    }
}
