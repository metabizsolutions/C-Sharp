using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Exam
{
    public partial class ExamGrades : DevExpress.XtraEditors.XtraUserControl
    {
        CommonFunctions fun = new CommonFunctions();
        private static ExamGrades _instance;
        public static ExamGrades instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ExamGrades();
                return _instance;
            }
        }
        public ExamGrades()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            FillGridExamGrades();
        }
        private void btnEGAdd_Click(object sender, EventArgs e)
        {

            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("INSERT into grade(name,grade_point,mark_from,mark_upto,comment,sync) VALUES('" + txtName.Text + "','" + txtGPoint.Text + "','" + txtMarkFrom.Text + "','" + txtMarkUpto.Text + "','" + txtComment.Text + "','0');", con);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Error");
                return;
            }
            con.Close();
            FillGridExamGrades();
            empty();
        }
        private void empty()
        {
            txtName.Text = "";
            txtGPoint.Text = "";
            txtMarkFrom.Text = "";
            txtMarkUpto.Text = "";
            txtComment.Text = "";
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            btnEGAdd.Enabled = false;
            if (add)
            {
                btnEGAdd.Enabled = true;
            }
            gridView1.OptionsBehavior.Editable = false;
            if (Edit)
            {
                gridView1.OptionsBehavior.Editable = true;
            }
            BtnEGDelete.Enabled = false;
            if (Delete)
                BtnEGDelete.Enabled = true;
        }
        private void FillGridExamGrades()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdP = new MySqlCommand("SELECT * FROM grade;", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridExamGrades.DataSource = table;
            gridView1.BestFitColumns();
            var col = gridView1.Columns["grade_id"];
            col.OptionsColumn.ReadOnly = true;
            con.Close();
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE grade set name='" + row[1] + "',grade_point='" + row[2] + "',mark_from='" + row[3] + "',mark_upto='" + row[4] + "',comment='" + row[5] + "',sync='0' WHERE grade_id='" + row[0] + "';", con);
            cmd.ExecuteNonQuery();
            con.Close();
            FillGridExamGrades();
        }

        private void BtnEGDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to delete?", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    MySqlConnection con = new MySqlConnection(Login.constring);
                    con.Open();
                    MySqlCommand cmdM = new MySqlCommand("DELETE from grade WHERE grade_id='" + row[0] + "';", con);
                    cmdM.ExecuteNonQuery();
                    con.Close();
                    FillGridExamGrades();
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Info");
            }
        }
    }
}
