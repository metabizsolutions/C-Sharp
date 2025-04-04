using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SchoolManagementSystem.Class
{
    public partial class create_teacher_card : UserControl
    {
        private static create_teacher_card _instance;

        public static create_teacher_card instance
        {
            get
            {
                if (_instance == null)
                    _instance = new create_teacher_card();
                return _instance;
            }
        }

        CommonFunctions fun = new CommonFunctions();
        teacher_card report = new teacher_card();
        public create_teacher_card()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            load_staff();
            if (!string.IsNullOrEmpty(fun.GetSettings("card_HF_backcolor")))
                card_HF_backcolor.EditValue = fun.GetColor(fun.GetSettings("card_HF_backcolor"));
            else
                card_HF_backcolor.EditValue = Color.White;
            if (!string.IsNullOrEmpty(fun.GetSettings("card_HF_textcolor")))
                card_HF_textcolor.EditValue = fun.GetColor(fun.GetSettings("card_HF_textcolor"));
            else
                card_HF_textcolor.EditValue = Color.Black;
            if (!string.IsNullOrEmpty(fun.GetSettings("card_body_backcolor")))
                card_body_backcolor.EditValue = fun.GetColor(fun.GetSettings("card_body_backcolor"));
            else
                card_body_backcolor.EditValue = ColorTranslator.FromHtml("#4054B2");
            if (!string.IsNullOrEmpty(fun.GetSettings("card_body_textcolor")))
                card_body_textcolor.EditValue = fun.GetColor(fun.GetSettings("card_body_textcolor"));
            else
                card_body_textcolor.EditValue = Color.Black;
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            simpleButton1.Enabled = false;
            if (add)
                simpleButton1.Enabled = true;
        }

        private void btn_get_students_Click(object sender, EventArgs e)
        {
            
        }
        void load_staff()
        {
            string query = "SELECT `teacher_id`,`name`,`birthday`,`sex`,`JoiningDate`,`designation`,`staff_type` FROM `teacher` WHERE `passout` != 1";
            DataTable dt = fun.FetchDataTable(query);
            gridControl1.DataSource = dt;
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridView1.Columns["birthday"].Visible = false;
            gridView1.Columns["sex"].Visible = false;
            gridView1.Columns["JoiningDate"].Visible = false;
            gridView1.Columns["designation"].Visible = false;
            gridView1.Columns["staff_type"].Visible = false;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int[] selectd_std = gridView1.GetSelectedRows();
            string school_name = fun.GetSettings("system_title");
            string school_address = fun.GetSettings("address");
            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\Teachers\";
            ObservableCollection<teachercard> aa = new ObservableCollection<teachercard>();
            fun.loaderform(() =>
            {
                foreach (int row in selectd_std)
                {
                    DataRow dr = gridView1.GetDataRow(row);
                    string card_id = dr["teacher_id"].ToString().PadLeft(4, '0');
                    string query = "update teacher set card_id = '" + card_id + "' where teacher_id = '" + dr["teacher_id"].ToString() + "'";
                    fun.ExecuteQuery(query);

                    teachercard std = new teachercard();
                    std.system_title = school_name;
                    std.system_address = school_address;
                    std.system_logo = fun.Base64ToImage(Login.Logo);
                    std.Name = string.IsNullOrEmpty(dr["name"].ToString())?"-": dr["name"].ToString();
                    string bd = string.IsNullOrEmpty(dr["birthday"].ToString()) ? "0001-01-01" : dr["birthday"].ToString();
                    std.birthday = Convert.ToDateTime(bd).ToString("yyyy-MM-dd");
                    string jd = string.IsNullOrEmpty(dr["JoiningDate"].ToString()) ? "0001-01-01" : dr["JoiningDate"].ToString();
                    std.JoiningDate = Convert.ToDateTime(jd).ToString("yyyy-MM-dd");
                    std.gender =  string.IsNullOrEmpty(dr["sex"].ToString())?"-": dr["sex"].ToString();
                    std.card_id = card_id;
                    std.staff_type = string.IsNullOrEmpty(dr["staff_type"].ToString())?"Staff": dr["staff_type"].ToString();
                    std.designation = string.IsNullOrEmpty(dr["designation"].ToString())?"-": dr["designation"].ToString();
                    std.teacher_id = dr["teacher_id"].ToString();
                    std.image_url = fun.get_image(@"\Images\Teachers\", dr["teacher_id"].ToString(), false, dr["sex"].ToString());
                    aa.Add(std);
                }
                //header footer Color
                report.lbl_staff_type.BackColor = card_HF_backcolor.Color;
                report.lbl_system_title.BackColor = card_HF_backcolor.Color;
                report.lbl_system_address.BackColor = card_HF_backcolor.Color;
                report.lbl_staff_type.ForeColor = card_HF_textcolor.Color;
                report.lbl_system_address.ForeColor = card_HF_textcolor.Color;
                report.lbl_system_title.ForeColor = card_HF_textcolor.Color;
                //Body Color
                report.card_panel.BackColor = card_body_backcolor.Color;
                //Body Text Color
                report.xrLabel3.ForeColor = card_body_textcolor.Color;
                report.xrLabel4.ForeColor = card_body_textcolor.Color;
                report.lbl_designation.ForeColor = card_body_textcolor.Color;
                report.lbl_staff_type.ForeColor = card_body_textcolor.Color;

                report.xrLabel3.BorderColor = card_body_textcolor.Color;
                report.xrLabel4.BorderColor = card_body_textcolor.Color;
                report.lbl_designation.BorderColor = card_body_textcolor.Color;
                report.lbl_staff_type.BorderColor = card_body_textcolor.Color;

                report.lbl_card_id.ForeColor = card_body_textcolor.Color;
                report.lbl_name.ForeColor = card_body_textcolor.Color;
                report.lbl_name.BorderColor = card_body_textcolor.Color;
                report.DataSource = aa;
                report.Landscape = false;
                report.CreateDocument(true);
                report.ShowPrintMarginsWarning = false;
                documentViewer1.DocumentSource = report;
            });
        }

        private void hf_back_color_EditValueChanged(object sender, EventArgs e)
        {
            ColorEdit ce = (sender) as ColorEdit;
            if (ce.EditValue != null)
            {
                string query = "update settings set description = '" + ce.EditValue + "' where type = '"+ce.Name+"'";
                fun.ExecuteQuery(query);
            }
        }
    }

    public partial class teachercard
    {
        public string teacher_id { get; set; }
        public string Name { get; set; }
        public string card_id { get; set; }
        public string JoiningDate { get; set; }
        public string birthday { get; set; }
        public string gender { get; set; }
        public string designation { get; set; }
        public string staff_type { get; set; }
        public Image image_url { get; set; }
        public string system_title { get; set; }
        public Image system_logo { get; set; }
        public string system_address { get; set; }

    }
}
