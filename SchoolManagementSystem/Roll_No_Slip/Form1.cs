using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SchoolManagementSystem.Exam;

namespace SchoolManagementSystem.Roll_No_Slip
{
    public partial class Form1 : Form
    {
        private List<StudentDetails> studentDetailsList;
        private Dictionary<StudentDetails, List<ExamSubject>> studentExamSubjects;
        private readonly string connectionString = "server=localhost;database=tnsbay_school;uid=root;pwd=;";
        private Dictionary<string, List<ExamSubject>> classSectionSchedules; // Cache for class/section schedules
        private readonly ExamList examList; // Store ExamList reference

        public Form1(ExamList examList = null)
        {
            InitializeComponent();
            studentDetailsList = new List<StudentDetails>();
            studentExamSubjects = new Dictionary<StudentDetails, List<ExamSubject>>();
            classSectionSchedules = new Dictionary<string, List<ExamSubject>>();
            this.examList = examList; // Assign ExamList reference
        }
        public Form1()
        {
            InitializeComponent();
            studentDetailsList = new List<StudentDetails>();
            studentExamSubjects = new Dictionary<StudentDetails, List<ExamSubject>>();
            classSectionSchedules = new Dictionary<string, List<ExamSubject>>();
        }

        public void SetStudentDetails(List<Student> students, string className, string section, string session)
        {
            // Debugging: Log the call to SetStudentDetails
            Console.WriteLine($"SetStudentDetails called with {students.Count} students, class: {className}, section: {section}, session: {session}");

            studentDetailsList.Clear();
            studentExamSubjects.Clear();

            // Normalize inputs to prevent cache misses
            className = className?.Trim().ToLower();
            section = section?.Trim().ToLower();

            // Check if the section exists in the datesheet
            if (!DoesSectionExistInDatesheet(className, section))
            {
                MessageBox.Show($"Error: No exam datesheet found for class {className} section {section}",
                               "Invalid Section", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Fetch the exam schedule for this class/section once
            var examSubjects = FetchExamSubjectsFromDatabase(className, section);
            if (examSubjects == null || examSubjects.Count == 0)
            {
                MessageBox.Show($"Error: No exam subjects found for class {className} section {section}",
                               "No Subjects", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Process each selected student
            foreach (var student in students)
            {
                var studentDetails = new StudentDetails
                {
                    Name = student.Name,
                    FatherName = student.FatherName,
                    RollNo = student.RollNo,
                    Group = student.Group,
                    Gender = student.Gender,
                    Class = className,
                    Section = section,
                    Session = session
                };

                studentDetailsList.Add(studentDetails);
                studentExamSubjects.Add(studentDetails, new List<ExamSubject>(examSubjects)); // Use the same schedule
            }

            if (studentDetailsList.Count > 0)
            {
                // Save roll no slips to database for all students in a single operation
                try
                {
                    SaveSlipsToDatabase(studentDetailsList, examSubjects);
                    MessageBox.Show("Roll No Slips created Successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Call button2_Click from ExamList if reference exists
                    if (examList != null)
                    {
                        // Invoke button2_Click using reflection or delegate to simulate button click
                        examList.GetType().GetMethod("button2_Click",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                            ?.Invoke(examList, new object[] { null, EventArgs.Empty });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving roll no slips: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool DoesSectionExistInDatesheet(string className, string section)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM datesheet WHERE class = @className AND section = @section";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@className", className);
                        command.Parameters.AddWithValue("@section", section);

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking section existence: " + ex.Message, "Database Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void SaveSlipsToDatabase(List<StudentDetails> students, List<ExamSubject> subjects)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insert into roll_no_slips for all students
                        string insertSlipQuery = @"
                            INSERT INTO roll_no_slips (student_name, father_name, roll_no, class, section, group_name, gender, session, issued_datetime)
                            VALUES (@name, @father, @rollNo, @class, @section, @group, @gender, @session, @issuedDate);
                            SELECT LAST_INSERT_ID();";

                        // Dictionary to store slip IDs for each student
                        var slipIds = new Dictionary<StudentDetails, long>();

                        foreach (var student in students)
                        {
                            using (MySqlCommand cmd = new MySqlCommand(insertSlipQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@name", student.Name);
                                cmd.Parameters.AddWithValue("@father", student.FatherName);
                                cmd.Parameters.AddWithValue("@rollNo", student.RollNo);
                                cmd.Parameters.AddWithValue("@class", student.Class);
                                cmd.Parameters.AddWithValue("@section", student.Section);
                                cmd.Parameters.AddWithValue("@group", student.Group);
                                cmd.Parameters.AddWithValue("@gender", student.Gender);
                                cmd.Parameters.AddWithValue("@session", student.Session);
                                cmd.Parameters.AddWithValue("@issuedDate", DateTime.Now);

                                long slipId = Convert.ToInt64(cmd.ExecuteScalar());
                                slipIds.Add(student, slipId);
                            }
                        }

                        // Insert subjects for all students
                        string insertSubjectQuery = @"
                            INSERT INTO roll_no_slip_subjects (slip_id, sr_no, subject_name, exam_date, start_time, end_time)
                            VALUES (@slipId, @sr, @subject, @date, @start, @end);";

                        foreach (var student in students)
                        {
                            long slipId = slipIds[student];
                            for (int i = 0; i < subjects.Count; i++)
                            {
                                var subject = subjects[i];
                                using (MySqlCommand cmd = new MySqlCommand(insertSubjectQuery, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@slipId", slipId);
                                    cmd.Parameters.AddWithValue("@sr", i + 1);
                                    cmd.Parameters.AddWithValue("@subject", subject.SubjectName);
                                    cmd.Parameters.AddWithValue("@date", subject.Date.Date);
                                    cmd.Parameters.AddWithValue("@start", subject.StartTime.TimeOfDay);
                                    cmd.Parameters.AddWithValue("@end", subject.EndTime.TimeOfDay);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error saving roll no slips to database: " + ex.Message);
                    }
                }
            }
        }

        private List<ExamSubject> FetchExamSubjectsFromDatabase(string className, string section)
        {
            // Normalize inputs
            className = className?.Trim().ToLower();
            section = section?.Trim().ToLower();
            string key = $"{className}_{section}";

            // Debugging: Log cache check
            Console.WriteLine($"Fetching exam subjects for key: {key}, Cache contains: {classSectionSchedules.ContainsKey(key)}");

            if (classSectionSchedules.ContainsKey(key))
            {
                Console.WriteLine($"Using cached schedule for {key}");
                return classSectionSchedules[key]; // Reuse cached schedule
            }

            List<ExamSubject> subjects = new List<ExamSubject>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT subject, date FROM datesheet WHERE class = @className AND section = @section ORDER BY date";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@className", className);
                        command.Parameters.AddWithValue("@section", section);

                        List<(string SubjectName, DateTime Date)> subjectDates = new List<(string, DateTime)>();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                subjectDates.Add((
                                    reader.GetString("subject"),
                                    reader.GetDateTime("date")
                                ));
                            }
                        }

                        if (subjectDates.Count == 0)
                        {
                            MessageBox.Show($"No subjects found for class {className} section {section}", "No Data",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return null;
                        }

                        // Debugging: Log before showing dialog
                        Console.WriteLine($"Showing ExamTimeDialog for {className}_{section}");

                        // Prompt for a single start and end time for all subjects
                        using (var timeDialog = new ExamTimeDialog("All Subjects", subjectDates[0].Date))
                        {
                            if (timeDialog.ShowDialog() == DialogResult.OK)
                            {
                                foreach (var (subjectName, examDate) in subjectDates)
                                {
                                    subjects.Add(new ExamSubject
                                    {
                                        SubjectName = subjectName,
                                        Date = examDate,
                                        StartTime = timeDialog.StartTime,
                                        EndTime = timeDialog.EndTime
                                    });
                                }
                            }
                            else
                            {
                                // User canceled - return null to abort slip generation
                                Console.WriteLine("ExamTimeDialog canceled");
                                return null;
                            }
                        }
                    }
                }

                // Cache the schedule for this class/section
                Console.WriteLine($"Caching schedule for {key}");
                classSectionSchedules[key] = subjects;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching exam subjects: " + ex.Message, "Database Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return subjects;
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

    public class ExamSubject
    {
        public string SubjectName { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class ExamTimeDialog : Form
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public string SubjectName { get; private set; }

        private DateTimePicker dateTimePickerStart;
        private DateTimePicker dateTimePickerEnd;
        private Button btnOK;
        private Button btnCancel;
        private Label label1;
        private Label label2;

        public ExamTimeDialog(string subjectName, DateTime examDate)
        {
            SubjectName = subjectName;
            Text = subjectName == "All Subjects" ? "Set Exam Times for All Subjects" : $"Set Times for {subjectName}";

            InitializeComponents();

            // Set default times (9 AM to 12 PM)
            dateTimePickerStart.Value = examDate.Date.AddHours(9);
            dateTimePickerEnd.Value = examDate.Date.AddHours(12);
        }

        private void InitializeComponents()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.dateTimePickerStart = new DateTimePicker();
            this.dateTimePickerEnd = new DateTimePicker();
            this.btnOK = new Button();
            this.btnCancel = new Button();

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new Point(20, 20);
            this.label1.Text = "Start Time:";

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new Point(20, 60);
            this.label2.Text = "End Time:";

            // dateTimePickerStart
            this.dateTimePickerStart.Format = DateTimePickerFormat.Custom;
            this.dateTimePickerStart.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dateTimePickerStart.Location = new Point(100, 20);
            this.dateTimePickerStart.Size = new Size(200, 20);
            this.dateTimePickerStart.ShowUpDown = true;

            // dateTimePickerEnd
            this.dateTimePickerEnd.Format = DateTimePickerFormat.Custom;
            this.dateTimePickerEnd.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dateTimePickerEnd.Location = new Point(100, 60);
            this.dateTimePickerEnd.Size = new Size(200, 20);
            this.dateTimePickerEnd.ShowUpDown = true;

            // btnOK
            this.btnOK.Location = new Point(100, 100);
            this.btnOK.Text = "OK";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);

            // btnCancel
            this.btnCancel.Location = new Point(200, 100);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // Form settings
            this.ClientSize = new Size(320, 140);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePickerStart);
            this.Controls.Add(this.dateTimePickerEnd);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dateTimePickerEnd.Value <= dateTimePickerStart.Value)
            {
                MessageBox.Show("End time must be after start time", "Invalid Times");
                return;
            }

            StartTime = dateTimePickerStart.Value;
            EndTime = dateTimePickerEnd.Value;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }

    public partial class StudentDetailsDialog : Form
    {
        private readonly string connectionString = "server=localhost;database=tnsbay_school;uid=root;pwd=;";
        private CheckedListBox chkListStudents;
        private Label lblStudents;
        private List<Student> students;
        private ComboBox cmbClass;
        private ComboBox cmbSection;
        private ComboBox cmbSession;
        private Button btnOK;
        private Button btnCancel;

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

        private void InitializeComponent()
        {
            this.cmbClass = new ComboBox();
            this.cmbSection = new ComboBox();
            this.cmbSession = new ComboBox();
            this.btnOK = new Button();
            this.btnCancel = new Button();

            // cmbClass
            this.cmbClass.Location = new Point(100, 20);
            this.cmbClass.Size = new Size(200, 21);
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.SelectedIndexChanged += new EventHandler(this.cmbClass_SelectedIndexChanged);

            // cmbSection
            this.cmbSection.Location = new Point(100, 50);
            this.cmbSection.Size = new Size(200, 21);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.SelectedIndexChanged += new EventHandler(this.cmbSection_SelectedIndexChanged);

            // cmbSession
            this.cmbSession.Location = new Point(100, 80);
            this.cmbSession.Size = new Size(200, 21);
            this.cmbSession.Name = "cmbSession";
            this.cmbSession.SelectedIndexChanged += new EventHandler(this.cmbSession_SelectedIndexChanged);

            // btnOK
            this.btnOK.Location = new Point(200, 300);
            this.btnOK.Size = new Size(75, 23);
            this.btnOK.Text = "OK";
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);

            // btnCancel
            this.btnCancel.Location = new Point(280, 300);
            this.btnCancel.Size = new Size(75, 23);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // Form settings
            this.ClientSize = new Size(400, 360);
            this.Controls.Add(this.cmbClass);
            this.Controls.Add(this.cmbSection);
            this.Controls.Add(this.cmbSession);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Student Details";
        }

        private void InitializeCustomComponents()
        {
            // Add label for students
            lblStudents = new Label
            {
                AutoSize = true,
                Location = new Point(20, 110),
                Text = "Students",
                Name = "lblStudents"
            };
            this.Controls.Add(lblStudents);

            // Add Select All button
            Button btnSelectAll = new Button
            {
                Location = new Point(20, 260),
                Size = new Size(100, 30),
                Text = "Select All",
                Name = "btnSelectAll"
            };
            btnSelectAll.Click += BtnSelectAll_Click;
            this.Controls.Add(btnSelectAll);

            // Add CheckedListBox for students
            chkListStudents = new CheckedListBox
            {
                Location = new Point(20, 130),
                Size = new Size(340, 120),
                Name = "chkListStudents",
                CheckOnClick = true,
                FormattingEnabled = true
            };
            // Set custom format for items
            chkListStudents.Format += (s, e) =>
            {
                if (e.ListItem is Student student)
                {
                    e.Value = $" Roll: {student.RollNo} - Name: {student.Name}";
                }
            };
            this.Controls.Add(chkListStudents);
        }

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            bool allChecked = chkListStudents.CheckedItems.Count == chkListStudents.Items.Count;

            for (int i = 0; i < chkListStudents.Items.Count; i++)
            {
                chkListStudents.SetItemChecked(i, !allChecked);
            }

            // Update button text based on selection state
            Button btn = (Button)sender;
            btn.Text = allChecked ? "Select All" : "Deselect All";
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

                chkListStudents.BeginUpdate();
                chkListStudents.Items.Clear();
                chkListStudents.Items.AddRange(students.ToArray());
                chkListStudents.EndUpdate();
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
        public override string ToString() => $"{RollNo} - {Name}";
    }
}