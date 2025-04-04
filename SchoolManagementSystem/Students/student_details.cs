using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem.Students
{
    public partial class student_details : Form
    {
        CommonFunctions fun = new CommonFunctions();
        public DataRow row;
        int student_id = 0;
        public student_details(DataRow dr)
        {
            InitializeComponent();
            
            student_id = Convert.ToInt32(dr["SrNo"]);
            if (Login.Logo != null)
            {
                Image mylogo = fun.Base64ToImage(Login.Logo);
                PicLogo.Image = mylogo;
            }

            picBoxStudent.Image = fun.get_image(@"\Images\Students\", student_id.ToString() + "_std", false,dr["Gender"].ToString());

            lbl_school.Text = fun.GetSettings("system_title");
            row = dr;
            load_std_data();
            fun.selectall_Controls(this);
        }
        int[] cat_ids;
        String[] cat_titles;
        Dictionary<int, string> cat = new Dictionary<int, string>();
        SearchLookUpEdit sec_gl = new SearchLookUpEdit();
        Label lb;
        bool has_class_change = false;
        void load_std_data()
        {
            cat.Clear();
            string query_cat = "SELECT field_id,title FROM student_fields ORDER BY title ASC  ";
            DataTable table_cat = fun.GetQueryTable(query_cat);
            int count = 0;
            int fee_cat_id;
            String extra_col = "";
            foreach (DataRow row in table_cat.Rows)
            {
                fee_cat_id = Convert.ToInt32(row["field_id"].ToString());
                cat.Add(fee_cat_id, row["title"].ToString());
                extra_col += ",(SELECT IFNULL(`value`,'') FROM student_fields_values WHERE field_id = '" + fee_cat_id + "' AND student_id = students.SrNo limit 1) AS '" + row["title"].ToString() + "'";
                count++;
            }
            String sql = "SELECT students.* " + extra_col + " " +
                 " from students where passout != 1 and students.SrNo = '" + student_id + "' ";
            DataTable table = fun.FetchDataTable(sql);
            sql = "select * from student where student_id = '" + student_id + "'";
            DataTable std_details = fun.FetchDataTable(sql);
            foreach (DataRow data in table.Rows)
            {
                string cls_id = std_details.Rows[0]["class_id"] == null ? "0" : std_details.Rows[0]["class_id"].ToString();
                string sec_id = std_details.Rows[0]["section_id"] == null ? "0" : std_details.Rows[0]["section_id"].ToString();
                foreach (DataColumn cl in table.Columns)
                {
                    string c = cl.ColumnName;
                    FlowLayoutPanel flp = new FlowLayoutPanel();
                    flp.Width = 329;
                    flp.Height = 26;
                    //flp.AutoScroll = true;
                    if (c == "SrNo" || c == "SessionID" || c == "P_School" || c == "PassOut" || c == "Enotice_sms" || c == "Fees_sms" || c == "Exam_sms" || c == "Attendent_sms" || c == "HostelRoom" || c == "free_student" || c == "free_std_remakrs" || c == "class_id" || c == "section_id" || c== "ladger_id")
                    { }
                    else
                    {
                        if (c == "FatherName")
                        {
                            CheckBox cb = new CheckBox();
                            cb.Name = "new_parent";
                            cb.Width = 15;
                            //cb.Text = "New Gaurdian";
                            flp.Controls.Add(cb);
                        }
                        if (c == "Class" || c == "Section" || c == "Name" || c == "FatherName" || c == "FatherPhone" || c == "Phone")
                        {
                            lb = new Label();
                            lb.Text = cl.ColumnName + "*";
                            lb.ForeColor = Color.Red;
                            lb.Margin = new Padding(0,5,0,0);
                            flp.Controls.Add(lb);
                        }
                        else
                        {
                            lb = new Label();
                            lb.Text = cl.ColumnName;
                            lb.Margin = new Padding(0, 5, 0, 0);
                            flp.Controls.Add(lb);
                        }
                        if (c == "Class")
                        {
                            has_class_change = false;
                            SearchLookUpEdit gl = new SearchLookUpEdit();
                            gl.Name = cl.ColumnName;
                            gl.Width = 180;
                            gl.EditValueChanged += Gl_EditValueChanged;
                            gl.Properties.DataSource = fun.GetAllClasses_dt();
                            gl.Properties.DisplayMember = "name";
                            gl.Properties.ValueMember = "class_id";
                            gl.EditValue = cls_id;
                            flp.Controls.Add(gl);
                        }
                        else if (c == "Section")
                        {
                            sec_gl.Name = cl.ColumnName;
                            sec_gl.Width = 180;
                            sec_gl.Properties.DataSource = fun.GetAllSection_dt(cls_id);
                            sec_gl.Properties.DisplayMember = "name";
                            sec_gl.Properties.ValueMember = "section_id";
                            sec_gl.EditValue = sec_id;
                            flp.Controls.Add(sec_gl);
                        }
                        else if (c == "Birthday")
                        {
                            DateEdit de = new DateEdit();
                            de.Name = cl.ColumnName;
                            string vl = "";
                            try
                            {
                                vl = data[cl] == null ? "" : Convert.ToDateTime(string.IsNullOrEmpty(data[cl].ToString()) ? "0001-01-01" : data[cl]).ToString("yyyy-MM-dd");
                            }
                            catch (FormatException)
                            {
                                MessageBox.Show("Birthday is not correct please this date", "Birthday Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                vl = "01/01/0001";
                            }
                            de.Text = vl;
                            de.Width = 180;
                            flp.Controls.Add(de);
                        }
                        else if (c == "addmission_date")
                        {
                            DateTimePicker de = new DateTimePicker();
                            de.Format = DateTimePickerFormat.Custom;
                            de.CustomFormat = "yyyy-MM-dd HH:mm:ss";
                            de.Name = cl.ColumnName;
                            string vl = "";
                            try
                            {
                                vl = Convert.ToDateTime(data[cl]).ToString();
                            }
                            catch (FormatException)
                            {
                                MessageBox.Show("Addmission Date is not correct please this date", "Birthday Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                vl = "01/01/0001";
                            }
                            de.Text = vl;
                            de.Width = 180;
                            flp.Controls.Add(de);
                        }
                        else if (c == "Gender")
                        {
                            ComboBoxEdit cob = new ComboBoxEdit();
                            cob.Name = cl.ColumnName;
                            cob.Properties.Items.Add("Male");
                            cob.Properties.Items.Add("Female");
                            cob.Text = data[cl] == null ? "" : data[cl].ToString();
                            cob.Width = 180;
                            flp.Controls.Add(cob);
                        }
                        else
                        {
                            TextBox tx = new TextBox();
                            if (c == "FatherName")
                                tx.Width = 160;
                            else
                                tx.Width = 180;
                            tx.Name = cl.ColumnName;
                            tx.Text = data[cl] == null ? "" : data[cl].ToString();
                            flp.Controls.Add(tx);
                        }
                        std_panel.Controls.Add(flp);
                    }
                }
            }
        }

        private void Gl_EditValueChanged(object sender, EventArgs e)
        {
            if (has_class_change)
            {
                SearchLookUpEdit gl = sender as SearchLookUpEdit;
                sec_gl.Properties.DataSource = fun.GetAllSection_dt(gl.EditValue == null ? "0" : gl.EditValue.ToString());
                sec_gl.EditValue = null;
            }
            has_class_change = true;
        }

        private void BtnTakeImage_Click(object sender, EventArgs e)
        {
            using (TakeImage t = new TakeImage())
            {
                if (t.ShowDialog() == DialogResult.OK)
                {

                }
                else
                {
                    string name = student_id + "_std.Jpeg";
                    string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\Students\";
                    if (File.Exists(appPath + name))
                    {
                        picBoxStudent.Image.Dispose();
                        GC.Collect();
                        File.Delete(appPath + name);
                    }
                    Image image = t.imgCapture.Image;
                    FileStream fstream = new FileStream(appPath + name, FileMode.Create);
                    image.Save(fstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    fstream.Close();

                    picBoxStudent.Image = t.imgCapture.Image;
                }
            }

        }
        OpenFileDialog fileOpen = new OpenFileDialog();
        private void BtnLoadImage_Click(object sender, EventArgs e)
        {
            fileOpen.Title = "Open Image file";
            fileOpen.Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif";

            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\Students\";
            if (Directory.Exists(appPath) == false)                                              // <---
            {                                                                                    // <---
                Directory.CreateDirectory(appPath);                                              // <---
            }

            if (fileOpen.ShowDialog() == DialogResult.OK)
            {
                string name = student_id + "_std.Jpeg";

                if (File.Exists(appPath + name))
                {
                    picBoxStudent.Image.Dispose();
                    GC.Collect();
                    File.Delete(appPath + name);
                }
                File.Copy(fileOpen.FileName, appPath + name, true);
                picBoxStudent.Image = Image.FromFile(fileOpen.FileName);

            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            string query = "";
            string cols = "";
            string de_cols = "";
            string extra_cols_query = "";
            var check_box = fun.GetAllControls(std_panel, typeof(CheckBox));
            foreach (CheckBox check in check_box)
            {
                if (check.Name == "new_parent" && check.Checked)
                {
                    string parent_qry = "INSERT INTO `parent` (`name`) VALUES ('new Parent');";
                    long parent_id = fun.Execute_Insert(parent_qry);
                    parent_qry = "UPDATE `student` SET `parent_id` = '" + parent_id + "' WHERE `student_id` = '" + student_id + "'";
                    fun.ExecuteQuery(parent_qry);
                }
            }
            var c = fun.GetAllControls(std_panel, typeof(TextBox));
            string de_c = "";
            foreach (TextBox txt in c)
            {
                string na = txt.Name;
                if ((na == "Name" || na == "FatherName" || na == "FatherPhone" || na == "Phone") && string.IsNullOrEmpty(txt.Text))
                {
                    MessageBox.Show("Please Fill All FIelds With *", "txtboxes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    
                    if (de_cols != "" && !de_cols.EndsWith(","))
                        de_cols += ",";
                    if(na == "Name" || na == "Phone" || na == "Address" || na == "Email")
                    {
                        de_c = txt.Name == "Name" ? "title" : txt.Name == "Phone" ? "mobile" : txt.Name;
                        de_cols += "`" + de_c + "`='" + txt.Text + "'";
                    }
                    if (cols != "" && !cols.EndsWith(","))
                        cols += ",";
                    string ParentID = fun.GetParentID(student_id).ToString();
                    string parent_qry = "select * from parent where parent_id = '" + ParentID + "'";
                    DataTable dt_parent = fun.FetchDataTable(parent_qry);
                    if (dt_parent.Rows.Count == 0)
                    {
                        parent_qry = "INSERT INTO `parent` (parent_id,`name`) VALUES ('" + ParentID + "','" + txt.Text + "');";
                        fun.ExecuteQuery(parent_qry);
                    }
                    if (na == "FatherPhone")
                        query += "update parent set `phone`='" + txt.Text + "'  WHERE parent_id='" + ParentID + "';";
                    else if (na == "FatherName")
                        query += "update parent set `name`='" + txt.Text + "'  WHERE parent_id='" + ParentID + "';";
                    else if (!cat.ContainsValue(txt.Name))
                        cols += "`" + txt.Name + "`='" + txt.Text + "'";
                    else
                    {
                        var field = cat.FirstOrDefault(x => x.Value == txt.Name).Key;
                        string fied_query = "select * from student_fields_values WHERE `student_id` = '" + student_id + "' AND `field_id` = '" + field + "';";
                        DataTable dt_field = fun.FetchDataTable(fied_query);
                        if (dt_field.Rows.Count > 0)
                            extra_cols_query += "UPDATE `student_fields_values` SET `value` = '" + txt.Text + "' WHERE `student_id` = '" + student_id + "' AND `field_id` = '" + field + "';";
                        else
                            extra_cols_query += "INSERT INTO `student_fields_values` (`student_id`,`field_id`,`value`) VALUES ('" + student_id + "','" + field + "','" + txt.Text + "'); ";
                    }
                }
            }
            if (cols.EndsWith(","))
                cols = cols.Remove(cols.Length - 1, 1);
            var lue = fun.GetAllControls(std_panel, typeof(SearchLookUpEdit));
            int class_id = 0;
            int section_id = 0;
            foreach (SearchLookUpEdit g in lue)
            {
                string na = g.Name;
                if ((na == "Class" || na == "Section") && (g.EditValue == null || string.IsNullOrEmpty(g.Text)))
                {
                    MessageBox.Show("Please Fill All FIelds With *", "Drop DownList", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    if (na == "Class")
                    {
                        cols += ",`class_id`='" + g.EditValue + "'";
                        class_id = Convert.ToInt32(g.EditValue);
                    }
                    else if (na == "Section")
                    {
                        cols += ",`section_id`='" + g.EditValue + "'";
                        section_id = Convert.ToInt32(g.EditValue);
                    }
                }
            }
            var date = fun.GetAllControls(std_panel, typeof(DateEdit));
            foreach (DateEdit d in date)
                cols += ",`" + d.Name + "`='" + Convert.ToDateTime(d.Text).ToString("yyyy-MM-dd") + "'";
            var addmission_date = fun.GetAllControls(std_panel, typeof(DateTimePicker));
            foreach (DateTimePicker d in addmission_date)
                cols += ",`" + d.Name + "`='" + Convert.ToDateTime(d.Text).ToString("yyyy-MM-dd HH:mm:ss") + "'";
            var cb = fun.GetAllControls(std_panel, typeof(ComboBoxEdit));
            foreach (ComboBoxEdit cob in cb)
            {
                cols += ",`sex`='" + cob.Text + "'";
            }
            #region Chaking if class change then mantaining class change history
            string invquery = "select class_id, section_id from student where student_id = '" + student_id + "' and `passout` != 1";
            DataTable dt = fun.FetchDataTable(invquery);
            int old_class_id = 0;
            int old_section_id = 0;
            if (dt.Rows.Count > 0)
            {
                 old_class_id = Convert.ToInt32(dt.Rows[0]["class_id"]);
                 old_section_id = Convert.ToInt32(dt.Rows[0]["section_id"]);
            }

            if (class_id > 0 && old_class_id > 0 && class_id != old_class_id)
            {
                invquery = "select invoice_id from invoice where student_id = '" + student_id + "' and `forward`=0";
                object invoice_id = fun.Execute_Scaler_string(invquery);
                if (invoice_id != null)
                    query += "INSERT INTO `change_class`(`std_id`,name,rollno, `class_id`, `section_id`, `invoice_id`) VALUES ('" + student_id + "','" + row["Name"] + "','" + row["Roll"] + "','" + class_id + "','" + section_id + "','" + invoice_id + "');";
            }
            if(section_id > 0 && old_section_id > 0 && section_id != old_section_id)
            {
                fun.TransferMarks(student_id, old_section_id, section_id);
            }
            # endregion Class Change History
            string name = "";
            query += "update student set " + cols + " WHERE student_id='" + student_id + "';";
            if (picBoxStudent.Image != null)
            {
                name = student_id.ToString() + "_std.Jpeg";
                query += "UPDATE `student` SET `image` = '" + name + "' WHERE `student_id` = '" + student_id + "';";
            }
            string qey = "SELECT ifnull(std.`ladger_id`,0) as ladger_id FROM student AS STD WHERE std.`student_id` ='" + student_id + "'";
            object ladger_id = fun.Execute_Scaler_string(qey);
            if (de_cols.EndsWith(","))
                de_cols = de_cols.Remove(de_cols.Length - 1, 1);
            if (!string.IsNullOrEmpty(ladger_id.ToString()) && Convert.ToInt32(ladger_id) > 0)
                query += "UPDATE `ac_ledger` SET " + de_cols + ",photo='" + name + "' WHERE `id` = '"+ ladger_id + "';";
            else
                query += "insert into `ac_ledger` SET " + de_cols + ",photo='" + name + "',head_id=1,status=1,is_auto_genrated=1,balance=0;";
            fun.ExecuteQuery(query + extra_cols_query);
            this.Close();
        }
    }
}
