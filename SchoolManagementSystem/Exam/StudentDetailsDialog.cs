using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SchoolManagementSystem.Exam
{
    public partial class StudentDetailsDialog : Form
    {
        private readonly string connectionString = "server=localhost;database=tnsbay_school;uid=root;pwd=;";

        // Public properties for accessing form values
        public string SelectedName => cmbName.Text;
        public string SelectedFatherName => cmbFatherName.Text;
        public string SelectedRollNo => cmbRollNo.Text;
        public string SelectedGroup => txtGroup.Text;
        public string SelectedGender => cmbGender.Text;
        public string SelectedClass => cmbClass.Text;
        public string SelectedSection => cmbSection.Text;
        public string SelectedSession => cmbSession.Text;

        public StudentDetails StudentDetails { get; private set; }

        public StudentDetailsDialog()
        {
            InitializeComponent();

            try
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Database connection string is not configured");
                }

                LoadDefaultValues();
                LoadStudentData();
                LoadClassData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Initialization error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void LoadDefaultValues()
        {
            try
            {
                // Initialize gender options
                cmbGender.Items.Clear();
                cmbGender.Items.AddRange(new string[] { "Male", "Female", "Other" });
                cmbGender.SelectedIndex = 0;

                // Initialize section options
                cmbSection.Items.Clear();
                cmbSection.Items.AddRange(new string[] { "A", "B", "C", "D" });
                cmbSection.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading default values: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStudentData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT name, father_name, student_id FROM student";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            cmbName.BeginUpdate();
                            cmbFatherName.BeginUpdate();
                            cmbRollNo.BeginUpdate();

                            cmbName.Items.Clear();
                            cmbFatherName.Items.Clear();
                            cmbRollNo.Items.Clear();

                            while (reader.Read())
                            {
                                cmbName.Items.Add(reader["name"].ToString());
                                cmbFatherName.Items.Add(reader["father_name"].ToString());
                                cmbRollNo.Items.Add(reader["student_id"].ToString());
                            }

                            cmbName.EndUpdate();
                            cmbFatherName.EndUpdate();
                            cmbRollNo.EndUpdate();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading student data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadClassData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT DISTINCT name, session FROM classes";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            cmbClass.BeginUpdate();
                            cmbSession.BeginUpdate();

                            cmbClass.Items.Clear();
                            cmbSession.Items.Clear();

                            while (reader.Read())
                            {
                                cmbClass.Items.Add(reader["name"].ToString());
                                cmbSession.Items.Add(reader["session"].ToString());
                            }

                            cmbClass.EndUpdate();
                            cmbSession.EndUpdate();

                            // Set default selections if items exist
                            if (cmbClass.Items.Count > 0) cmbClass.SelectedIndex = 0;
                            if (cmbSession.Items.Count > 0) cmbSession.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading class data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                try
                {
                    StudentDetails = new StudentDetails
                    {
                        Name = cmbName.Text,
                        FatherName = cmbFatherName.Text,
                        RollNo = cmbRollNo.Text,
                        Group = txtGroup.Text,
                        Gender = cmbGender.Text,
                        Class = cmbClass.Text,
                        Section = cmbSection.Text,
                        Session = cmbSession.Text
                    };

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving student details: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(cmbName.Text))
            {
                MessageBox.Show("Please select student name", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbFatherName.Text))
            {
                MessageBox.Show("Please select father's name", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbFatherName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbRollNo.Text))
            {
                MessageBox.Show("Please select roll number", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRollNo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtGroup.Text))
            {
                MessageBox.Show("Please enter group", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGroup.Focus();
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbName.SelectedIndex >= 0 &&
                    cmbFatherName.Items.Count > cmbName.SelectedIndex &&
                    cmbRollNo.Items.Count > cmbName.SelectedIndex)
                {
                    cmbFatherName.SelectedIndex = cmbName.SelectedIndex;
                    cmbRollNo.SelectedIndex = cmbName.SelectedIndex;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error synchronizing selections: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbRollNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbRollNo.SelectedIndex >= 0 &&
                    cmbName.Items.Count > cmbRollNo.SelectedIndex &&
                    cmbFatherName.Items.Count > cmbRollNo.SelectedIndex)
                {
                    cmbName.SelectedIndex = cmbRollNo.SelectedIndex;
                    cmbFatherName.SelectedIndex = cmbRollNo.SelectedIndex;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error synchronizing selections: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class StudentDetails
    {
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string RollNo { get; set; }
        public string Group { get; set; }
        public string Gender { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public string Session { get; set; }
    }
}