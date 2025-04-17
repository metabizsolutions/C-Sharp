using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SchoolManagementSystem.Exam
{
    public partial class StudentDetailsDialog : Form
    {
        private readonly string connectionString = "server=localhost;database=tnsbay_school;uid=root;pwd=;";
        private CheckedListBox chkListStudents;
        private Label lblStudents;
        private List<Student> students;

        // Public properties for accessing form values
        public string SelectedClass => cmbClass.Text;
        public string SelectedSection => cmbSection.Text;
        public string SelectedSession => cmbSession.Text;

        public StudentDetailsDialog()
        {
            InitializeComponent();
            InitializeCustomComponents();

            try
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Database connection string is not configured");
                }

                LoadClassData();
                LoadSectionData(); // Initial load, may be empty until a class is selected
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Initialization error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void InitializeCustomComponents()
        {
            // Add label for students
            lblStudents = new Label
            {
                AutoSize = true,
                Location = new Point(20, 90),
                Text = "Students",
                Name = "lblStudents"
            };
            this.Controls.Add(lblStudents);

            // Add CheckedListBox for students
            chkListStudents = new CheckedListBox
            {
                Location = new Point(20, 110),
                Size = new Size(340, 140),
                DisplayMember = "RollNo",
                Name = "chkListStudents"
            };
            this.Controls.Add(chkListStudents);

            // Adjust form size and button positions
            this.Size = new Size(400, 340);
            btnOK.Location = new Point(200, 260);
            btnCancel.Location = new Point(280, 260);
        }

        // Returns the list of students selected in the CheckedListBox for roll number slip generation
        public List<Student> GetSelectedStudents()
        {
            var selectedStudents = new List<Student>();
            foreach (Student student in chkListStudents.CheckedItems)
            {
                selectedStudents.Add(student);
            }
            return selectedStudents;
        }

        private void LoadSectionData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT DISTINCT sec.name
                        FROM section sec
                        JOIN student s ON s.section_id = sec.section_id
                        JOIN classes c ON s.class_id = c.class_id
                        WHERE c.name = @className";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@className", string.IsNullOrWhiteSpace(cmbClass.Text) ? (object)DBNull.Value : cmbClass.Text);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            cmbSection.BeginUpdate();
                            cmbSection.Items.Clear();

                            while (reader.Read())
                            {
                                cmbSection.Items.Add(reader["name"].ToString());
                            }

                            cmbSection.EndUpdate();

                            if (cmbSection.Items.Count > 0)
                            {
                                cmbSection.SelectedIndex = 0;
                                cmbSection.Enabled = true;
                            }
                            else
                            {
                                cmbSection.Enabled = false; // Disable if no sections are found
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading section data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSection.Enabled = false;
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

        private void LoadStudents()
        {
            if (string.IsNullOrWhiteSpace(cmbClass.Text) || string.IsNullOrWhiteSpace(cmbSection.Text) || string.IsNullOrWhiteSpace(cmbSession.Text))
            {
                chkListStudents.Items.Clear();
                return;
            }

            try
            {
                students = new List<Student>();
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT s.roll, s.name, s.father_name, s.blood_group, s.sex
                        FROM student s
                        JOIN classes c ON s.class_id = c.class_id
                        JOIN section sec ON s.section_id = sec.section_id
                        WHERE c.name = @class
                        AND sec.name = @section
                        AND c.session = @session";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@class", cmbClass.Text);
                        command.Parameters.AddWithValue("@section", cmbSection.Text);
                        command.Parameters.AddWithValue("@session", cmbSession.Text);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                students.Add(new Student
                                {
                                    RollNo = reader["roll"].ToString(),
                                    Name = reader["name"].ToString(),
                                    FatherName = reader["father_name"].ToString(),
                                    Group = reader["blood_group"].ToString(),
                                    Gender = reader["sex"].ToString()
                                });
                            }
                        }
                    }
                }

                chkListStudents.Items.Clear();
                chkListStudents.Items.AddRange(students.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading student data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(cmbClass.Text))
            {
                MessageBox.Show("Please select a class", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbClass.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbSection.Text))
            {
                MessageBox.Show("Please select a section", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbSection.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbSession.Text))
            {
                MessageBox.Show("Please select a session", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbSession.Focus();
                return false;
            }

            if (chkListStudents.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one student", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chkListStudents.Focus();
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSectionData(); // Reload sections based on selected class
            LoadStudents(); // Reload students based on new class and section
        }

        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void cmbSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStudents();
        }
    }

    public class Student
    {
        public string RollNo { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Group { get; set; }
        public string Gender { get; set; }
        public override string ToString() => RollNo;
    }
}