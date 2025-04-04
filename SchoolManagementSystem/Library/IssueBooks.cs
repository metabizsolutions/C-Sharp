using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Library
{
    public partial class IssueBooks : DevExpress.XtraEditors.XtraUserControl
    {
        private static IssueBooks _instance;

        public static IssueBooks instance
        {
            get
            {
                if (_instance == null)
                    _instance = new IssueBooks();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();

        public IssueBooks()
        {
            InitializeComponent();
        }

        private void IssueBooks_Enter(object sender, EventArgs e)
        {
            txtBook.Properties.Items.Clear();
            allClass.Clear();
            allClass = fun.GetAllBooks();
            foreach (var allclass in allClass)
                txtBook.Properties.Items.Add(allclass.Name);

            txtIssueTo.Properties.Items.Clear();
            allClass.Clear();
            allClass = fun.GetAllStudents();
            foreach (var allclass in allClass)
                txtIssueTo.Properties.Items.Add(allclass.Name);
            allClass.Clear();
            allClass = fun.GetAllTeacher();
            foreach (var allclass in allClass)
                txtIssueTo.Properties.Items.Add(allclass.Name);
        }

        private void txtBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQty.Text = fun.GetBookQty(txtBook.Text).ToString();
        }

        private void BtnIssue_Click(object sender, EventArgs e)
        {
            var std = txtIssueTo.Text;
            int memberID;
            if (std.Contains(">"))
            {
                memberID = fun.GetStudentID(txtIssueTo.Text.Split('>')[0]);
            }
            else
            {
                memberID = fun.GetTeacherID(txtIssueTo.Text);
            }
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            try
            {
                var bookID = fun.GetBookID(txtBook.Text);
                var query = "INSERT into book_issue(book_id,ID,member_id,issue_date,return_date,issueby,sync,status) VALUES('" + fun.GetBookID(txtBook.Text) + "','" + txtBookID.Text + "','" + memberID + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.AddDays(15).ToShortDateString() + "','" + Login.CurrentUserID + "','0','0');";
                query += "UPDATE book set qty=qty-1 where book_id='" + bookID + "'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message, "Error");
                return;
            }
            con.Close();
            FillGridLibrary();
            emptyL();
        }
        private void emptyL()
        {
            txtBook.Text = "";
            txtBookID.Text = "";
            txtIssueTo.Text = "";
            txtQty.Text = "";
        }
        private void FillGridLibrary()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdP = new MySqlCommand("SELECT book.name as Book ,book.author as Auther, student.name as Student, teacher.name as Teacher,book_issue.return_date as ReturnDate FROM book_issue left join student on student.student_id=book_issue.member_id left join teacher on teacher.teacher_id=book_issue.member_id inner join book on book.book_id=book_issue.book_id where book_issue.status=0", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridLibrary.DataSource = table;
            gridView2.BestFitColumns();
            con.Close();
        }
    }
}
