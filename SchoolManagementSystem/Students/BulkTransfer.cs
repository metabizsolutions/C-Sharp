using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace SchoolManagementSystem.Students
{
    public partial class BulkTransfer : DevExpress.XtraEditors.XtraUserControl
    {
        private static BulkTransfer _instance;

        public static BulkTransfer instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BulkTransfer();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        public BulkTransfer()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnTransfer.Enabled = false;
            if (Edit)
                btnTransfer.Enabled = true;
        }
        public void loadfunctions()
        {
            String sql = "SELECT class.name AS Class, section.name AS Section, section.section_id as id FROM section " +
                            "INNER JOIN class ON class.class_id =section.class_id " +
                            "ORDER BY class.name ASC, section.name ASC ";
            DataTable table = fun.FetchDataTable(sql);
            gridLookUpFrom.Properties.DataSource = table;
            gridLookUpFrom.Properties.DisplayMember = "Section";
            gridLookUpFrom.Properties.ValueMember = "id";

            gridLookUpTo.Properties.DataSource = table;
            gridLookUpTo.Properties.DisplayMember = "Section";
            gridLookUpTo.Properties.ValueMember = "id";
        }
        public void FillGridStudent(String section_from)
        {
            string query_cat = "SELECT field_id,title FROM student_fields ORDER BY title ASC  ";
            DataTable table_cat = fun.GetQueryTable(query_cat);
            int[] cat_ids = new int[table_cat.Rows.Count];
            String[] cat_titles = new String[table_cat.Rows.Count];
            int count = 0;
            int fee_cat_id;
            String extra_col = "";
            foreach (DataRow row in table_cat.Rows)
            {
                fee_cat_id = Convert.ToInt32(row["field_id"].ToString());
                cat_ids[count] = fee_cat_id;
                cat_titles[count] = row["title"].ToString();
                extra_col += ",(SELECT IFNULL(`value`,'') FROM student_fields_values WHERE field_id = '" + fee_cat_id + "' AND student_id = s.student_id LIMIT 1) AS '" + row["title"].ToString() + "' ";
                count++;
            }

            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            String sql = "SELECT student_id,class_id,section_id,`name`,roll,sex AS gender " + extra_col + " " +
                "from student AS s where 1 = 1 AND passout != 1 AND section_id = " + section_from;
            MySqlCommand cmdP = new MySqlCommand(sql, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
            DataTable table = new DataTable();
            adp.Fill(table);

            gridBulkTransfer.DataSource = null;
            gridView1.Columns.Clear();
            gridBulkTransfer.DataSource = CommonFunctions.AutoNumberedTable(table);
            gridView1.BestFitColumns();

        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            DataRow row;
            String student_id;
            String old_section_id = gridLookUpFrom.EditValue.ToString();
            String new_section_id = gridLookUpTo.EditValue.ToString();
            String section_from = "";
            string move_exam_by_section = fun.GetSettings("move_exam_by_section");
            section_from = gridLookUpFrom.EditValue.ToString();
            
                fun.loaderform(() =>
                {
                    try
                    {
                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {
                            row = gridView1.GetDataRow(i);
                            student_id = row["student_id"].ToString();
                            if (Convert.ToInt32(new_section_id) > 0 && Convert.ToInt32(old_section_id) > 0 && Convert.ToInt32(new_section_id) != Convert.ToInt32(old_section_id))
                            {
                                fun.TransferMarks(Convert.ToInt32(student_id), Convert.ToInt32(old_section_id), Convert.ToInt32(new_section_id));
                            }
                            fun.ChangeSection(student_id, old_section_id, new_section_id, null);
                        }
                        
                        MessageBox.Show("Students Transfered Successfully", "Transfered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Transfered Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                });
            
            FillGridStudent(section_from);
        }

        private void gridLookUpFrom_EditValueChanged(object sender, EventArgs e)
        {
            String section_from = gridLookUpFrom.EditValue.ToString();
            FillGridStudent(section_from);
        }

       
    }
}
