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

        public Form1()
        {
            InitializeComponent();
            studentDetailsList = new List<StudentDetails>();
            studentExamSubjects = new Dictionary<StudentDetails, List<ExamSubject>>();
            classSectionSchedules = new Dictionary<string, List<ExamSubject>>();
        }

        public void SetStudentDetails(List<Student> students, string className, string section, string session)
        {
            studentDetailsList.Clear();
            studentExamSubjects.Clear();

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
                // Save roll no slips to database for all students
                try
                {
                    foreach (var studentDetails in studentDetailsList)
                    {
                        var examSubjectsForStudent = studentExamSubjects[studentDetails];
                        SaveSlipToDatabase(studentDetails, examSubjectsForStudent);
                    }
                    MessageBox.Show("Roll No Slips created Successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void SaveSlipToDatabase(StudentDetails student, List<ExamSubject> subjects)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insert into roll_no_slips
                        string insertSlipQuery = @"
                    INSERT INTO roll_no_slips (student_name, father_name, roll_no, class, section, group_name, gender, session, issued_datetime)
                    VALUES (@name, @father, @rollNo, @class, @section, @group, @gender, @session, @issuedDate);
                    SELECT LAST_INSERT_ID();";

                        long slipId;
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

                            slipId = Convert.ToInt64(cmd.ExecuteScalar());
                        }

                        // Insert subjects
                        string insertSubjectQuery = @"
                    INSERT INTO roll_no_slip_subjects (slip_id, sr_no, subject_name, exam_date, start_time, end_time)
                    VALUES (@slipId, @sr, @subject, @date, @start, @end);";

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

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error saving slip to database: " + ex.Message);
                    }
                }
            }
        }

        private List<ExamSubject> FetchExamSubjectsFromDatabase(string className, string section)
        {
            string key = $"{className}_{section}";
            if (classSectionSchedules.ContainsKey(key))
            {
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

                        // Prompt for times once for all subjects
                        foreach (var (subjectName, examDate) in subjectDates)
                        {
                            using (var timeDialog = new ExamTimeDialog(subjectName, examDate))
                            {
                                if (timeDialog.ShowDialog() == DialogResult.OK)
                                {
                                    subjects.Add(new ExamSubject
                                    {
                                        SubjectName = subjectName,
                                        Date = examDate,
                                        StartTime = timeDialog.StartTime,
                                        EndTime = timeDialog.EndTime
                                    });
                                }
                                else
                                {
                                    // User canceled - return null to abort slip generation
                                    return null;
                                }
                            }
                        }
                    }
                }

                // Cache the schedule for this class/section
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
            Text = $"Set Times for {subjectName}";

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

            // label不同
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
}