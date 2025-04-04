using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace SchoolManagementSystem.Students
{
    public partial class create_student_card : UserControl
    {
        private static create_student_card _instance;

        public static create_student_card instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new create_student_card();
                }

                return _instance;
            }
        }

        private CommonFunctions fun = new CommonFunctions();
        public create_student_card()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            ddl_classes.Properties.DataSource = fun.GetAllClasses_dt();
            ddl_classes.Properties.ValueMember = "class_id";
            ddl_classes.Properties.DisplayMember = "name";

            if (!string.IsNullOrEmpty(fun.GetSettings("card_HF_backcolor")))
            {
                card_HF_backcolor.EditValue = fun.GetColor(fun.GetSettings("card_HF_backcolor"));
            }
            else
            {
                card_HF_backcolor.EditValue = Color.White;
            }

            if (!string.IsNullOrEmpty(fun.GetSettings("card_HF_textcolor")))
            {
                card_HF_textcolor.EditValue = fun.GetColor(fun.GetSettings("card_HF_textcolor"));
            }
            else
            {
                card_HF_textcolor.EditValue = Color.Black;
            }

            if (!string.IsNullOrEmpty(fun.GetSettings("card_body_backcolor")))
            {
                card_body_backcolor.EditValue = fun.GetColor(fun.GetSettings("card_body_backcolor"));
            }
            else
            {
                card_body_backcolor.EditValue = ColorTranslator.FromHtml("#4054B2");
            }

            if (!string.IsNullOrEmpty(fun.GetSettings("card_body_textcolor")))
            {
                card_body_textcolor.EditValue = fun.GetColor(fun.GetSettings("card_body_textcolor"));
            }
            else
            {
                card_body_textcolor.EditValue = Color.Black;
            }
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            simpleButton1.Enabled = false;
            if (add)
            {
                simpleButton1.Enabled = true;
            }
        }
        private void ddl_classes_EditValueChanged(object sender, EventArgs e)
        {
            object classes = ddl_classes.EditValue;
            if (!string.IsNullOrEmpty(classes.ToString()))
            {
                ddl_sections.Properties.DataSource = fun.GetAllSection_dt(classes.ToString());
                ddl_sections.Properties.ValueMember = "section_id";
                ddl_sections.Properties.DisplayMember = "name";
            }
        }

        private void btn_get_students_Click(object sender, EventArgs e)
        {
            object sections = ddl_sections.EditValue;
            if (!string.IsNullOrEmpty(sections.ToString()))
            {
                string query = "select std.student_id,std.card_id,std.name,std.roll,std.sex as gender,cls.name as class,sec.name as section,`parent`.`name` as fathername from student as std " +
                    " join parent on `parent`.`parent_id` = std.parent_id " +
                    " join class as cls on cls.class_id = std.class_id " +
                    " join section as sec on sec.section_id = std.section_id " +
                    " where std.section_id in (" + sections + ") ";
                DataTable dt = fun.FetchDataTable(query);
                gridControl1.DataSource = dt;
                gridView1.OptionsSelection.MultiSelect = true;
                gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            create_card();
        }

        private void create_card()
        {
            int[] selectd_std = gridView1.GetSelectedRows();
            string school_name = fun.GetSettings("system_title");
            string school_address = fun.GetSettings("address");
            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\Students\";
            ObservableCollection<studentcard> aa = new ObservableCollection<studentcard>();
            fun.loaderform(() =>
            {
                foreach (int row in selectd_std)
                {
                    DataRow dr = gridView1.GetDataRow(row);
                    string card_id = dr["student_id"].ToString().PadLeft(4, '0');
                    string query = "update student set card_id = '" + card_id + "' where student_id = '" + dr["student_id"].ToString() + "'";
                    fun.ExecuteQuery(query);

                    studentcard std = new studentcard();
                    std.system_title = school_name;
                    std.system_address = school_address;
                    std.system_logo = fun.Base64ToImage(Login.Logo);
                    std.Name = dr["name"].ToString();
                    std.roll = "Roll#: " + dr["roll"].ToString();
                    std.Father_Name = dr["fathername"].ToString();
                    std.card_id = card_id;
                    std.Class = dr["class"].ToString();
                    std.Section = dr["section"].ToString();
                    std.student_id = "ID#: " + dr["student_id"].ToString();
                    std.image_url = fun.get_image(@"\Images\Students\", dr["student_id"].ToString() + "_std", false, dr["gender"].ToString());
                    aa.Add(std);
                }
                student_card report = new student_card();
                //header footer Color
                report.lbl_system_title.BackColor = card_HF_backcolor.Color;
                report.lbl_system_address.BackColor = card_HF_backcolor.Color;
                report.lbl_system_title.ForeColor = card_HF_textcolor.Color;
                report.lbl_system_address.ForeColor = card_HF_textcolor.Color;
                //Body Color
                report.card_panel.BackColor = card_body_backcolor.Color;
                //Body Text Color
                report.lbl_card.ForeColor = card_body_textcolor.Color;

                report.lbl_class.ForeColor = card_body_textcolor.Color;
                report.lbl_section.ForeColor = card_body_textcolor.Color;
                report.lbl_roll.ForeColor = card_body_textcolor.Color;
                report.lbl_std_id.ForeColor = card_body_textcolor.Color;

                report.lbl_class.BorderColor = card_body_textcolor.Color;
                report.lbl_section.BorderColor = card_body_textcolor.Color;
                report.lbl_roll.BorderColor = card_body_textcolor.Color;
                report.lbl_std_id.BorderColor = card_body_textcolor.Color;

                report.lbl_card_id.ForeColor = card_body_textcolor.Color;
                report.lbl_name.ForeColor = card_body_textcolor.Color;
                report.lbl_name.WidthF = 169.5f;

                /*if (!string.IsNullOrEmpty(std_card_height.EditValue.ToString()) && Convert.ToInt32(std_card_height.EditValue) > 0)
                {
                    report.card_panel.HeightF = Convert.ToInt32(std_card_height.EditValue);
                }

                if (!string.IsNullOrEmpty(std_card_width.EditValue.ToString()) && Convert.ToInt32(std_card_width.EditValue) > 0)
                {
                    report.card_panel.WidthF = Convert.ToInt32(std_card_width.EditValue);
                }*/

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
                string query = "update settings set description = '" + ce.EditValue + "' where type = '" + ce.Name + "'";
                fun.ExecuteQuery(query);
            }
        }

        private void txt_card_height_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit ce = (sender) as TextEdit;
            string query = "update settings set description = '" + ce.EditValue + "' where type = '" + ce.Name + "'";
            fun.ExecuteQuery(query);
        }
    }

    public partial class studentcard
    {
        public string student_id { get; set; }
        public string Name { get; set; }
        public string card_id { get; set; }
        public string Father_Name { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public string roll { get; set; }
        public Image image_url { get; set; }
        public string system_title { get; set; }
        public Image system_logo { get; set; }
        public string system_address { get; set; }

    }
}
