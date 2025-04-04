using DevExpress.XtraEditors.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Media;
using System.Windows.Forms;

namespace SchoolManagementSystem.Library
{
    public partial class AddBooks : DevExpress.XtraEditors.XtraUserControl
    {
        private static AddBooks _instance;

        public static AddBooks instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AddBooks();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        ObservableCollection<AllClass> allClass = new ObservableCollection<AllClass>();

        public AddBooks()
        {
            InitializeComponent();
        }
        private void AddBooks_Enter(object sender, EventArgs e)
        {
            txtCategory.Properties.Items.Clear();
            allClass.Clear();
            allClass = fun.GetAllBooksCategory();
            foreach (var allclass in allClass)
                txtCategory.Properties.Items.Add(allclass.Name);
        }
        private void BtnLAdd_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("INSERT into book(name,description,author,publisher,status,price,sync,category) VALUES('" + txtLName.Text + "','" + txtLDes.Text + "','" + txtLAuthor.Text + "','" + txtPublisher + "','" + txtLStatus.Text + "','" + txtLPrice.Text + "','0','" + fun.GetBookCategoryID(txtCategory.Text) + "');", con);
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
            txtLAuthor.Text = "";
            txtCategory.Text = "";
            txtLDes.Text = "";
            txtLName.Text = "";
            txtLPrice.Text = "";
            txtLStatus.Text = "";
        }
        private void FillGridLibrary()
        {
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmdP = new MySqlCommand("SELECT book_id as ID,book.name as Name,description as Description,author as Author,publisher as Publisher,books_category.name as Category,status as Status,price as Price ,book.sync as Sync FROM book inner join books_category on books_category.books_category_id=book.category;", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmdP);
            DataTable table = new DataTable();
            adp.Fill(table);
            gridLibrary.DataSource = table;
            gridView2.BestFitColumns();
            var col = gridView2.Columns["ID"];
            col.OptionsColumn.ReadOnly = true;
            con.Close();
            RepositoryItemComboBox riComboC = new RepositoryItemComboBox();
            allClass.Clear();
            allClass = fun.GetAllBooksCategory();
            foreach (var allclass in allClass)
                riComboC.Items.Add(allclass.Name);
            gridView2.Columns["Category"].ColumnEdit = riComboC;

            RepositoryItemComboBox riComboStatus = new RepositoryItemComboBox();
            riComboStatus.Items.Add("Available");
            riComboStatus.Items.Add("Unavailable");
            gridView2.Columns["Status"].ColumnEdit = riComboStatus;
        }

        private void gridView2_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            MySqlConnection con = new MySqlConnection(Login.constring);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE book set name='" + row[1] + "',description='" + row[2] + "',author='" + row[3] + "',publisher='" + row[4] + "',category='" + fun.GetBookCategoryID(row[5].ToString()) + "'status='" + row[6] + "' ,price='" + row[7] + "',sync='0' WHERE book_id='" + row[0] + "';", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void btnLDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to delete?", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataRow row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                    MySqlConnection con = new MySqlConnection(Login.constring);
                    con.Open();
                    MySqlCommand cmdM = new MySqlCommand("DELETE from book WHERE book_id='" + row[0] + "';", con);
                    cmdM.ExecuteNonQuery();
                    con.Close();
                    FillGridLibrary();
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
