using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem.Class
{
    public partial class Add_section_subject : Form
    {
        DataTable teacherlist = new DataTable();
        CommonFunctions fun = new CommonFunctions();
        public Add_section_subject()
        {
            InitializeComponent();
            fun.selectall_Controls(this);
            String examSQL = "SELECT teacher_id,`name`,designation,subject_code FROM teacher ORDER BY name DESC";
            teacherlist = fun.FetchDataTable(examSQL);
        }
        int subno = 0;
        private void btn_addsubject_Click(object sender, EventArgs e)
        {
            subno++;
            if (subno == 1)
                panelinstallment.Height = 30;
            else
                panelinstallment.Height += 30;
            Label lbl = new Label();
            lbl.AutoSize = false;
            lbl.Text = "Subject: " + subno.ToString();
            lbl.Margin = new Padding(38, 6, 0, 0);
            lbl.BorderStyle = BorderStyle.Fixed3D;
            panelinstallment.Controls.Add(lbl);

            TextBox txtname = new TextBox();
            panelinstallment.Controls.Add(txtname);
            txtname.Size = new Size(100, 21);
            txtname.Name = "name" + subno;
            txtname.Text = "";

            NumericUpDown txtworkload = new NumericUpDown();
            panelinstallment.Controls.Add(txtworkload);
            txtworkload.Size = new Size(100, 21);
            txtworkload.Name = "workload" + subno;
            txtworkload.Text = "0";

            SearchLookUpEdit teacher = new SearchLookUpEdit();
            panelinstallment.Controls.Add(teacher);
            teacher.Size = new Size(100, 21);
            teacher.Properties.DataSource = teacherlist;
            teacher.Properties.DisplayMember = "name";
            teacher.Properties.ValueMember = "teacher_id";
            teacher.Name = "Teacher-" + subno;
            panelinstallment.SetFlowBreak(teacher, true);
        }
        public DataTable subdt { get; set; }
        private void Add_section_subject_FormClosed(object sender, FormClosedEventArgs e)
        {
            DataTable dt = new DataTable();
            var c = fun.GetAllControls(panelinstallment, typeof(TextBox), "name");
            dt.Columns.Add("name");
            dt.Columns.Add("workload");
            dt.Columns.Add("teacher");
            DataRow dr;
            int count = 0;
            foreach (TextBox txt in c)
            {
                count++;
                if (txt.Text != "")
                {
                    dr = dt.NewRow();
                    dr["name"] = txt.Text;

                    var num = fun.GetAllControls(panelinstallment, typeof(NumericUpDown), "workload" + count);
                    foreach (NumericUpDown nu in num)
                        dr["workload"] = nu.Value;
                    var ddl = fun.GetAllControls(panelinstallment, typeof(SearchLookUpEdit), "Teacher-" + count);
                    foreach (SearchLookUpEdit D in ddl)
                        dr["teacher"] = D.EditValue;
                    dt.Rows.Add(dr);
                }
            }
            subdt = dt;
        }
    }
}
