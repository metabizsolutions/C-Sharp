using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using SchoolManagementSystem.Exam;

namespace SchoolManagementSystem.Roll_No_Slip
{
    public partial class Form1 : Form
    {
        private List<StudentDetails> studentDetailsList;
        private Dictionary<StudentDetails, List<ExamSubject>> studentExamSubjects;
        private readonly string connectionString = "server=localhost;database=tnsbay_school;uid=root;pwd=;";

        public Form1()
        {
            InitializeComponent();
            studentDetailsList = new List<StudentDetails>();
            studentExamSubjects = new Dictionary<StudentDetails, List<ExamSubject>>();
        }

        public void SetStudentDetails(List<Student> students, string className, string section, string session)
        {
            studentDetailsList.Clear();
            studentExamSubjects.Clear();

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

                if (!DoesSectionExistInDatesheet(className, section))
                {
                    MessageBox.Show($"Error: No exam datesheet found for class {className} section {section}",
                                   "Invalid Section", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Fetch subjects for this student
                var examSubjects = FetchExamSubjectsFromDatabase(className, section);
                if (examSubjects == null || examSubjects.Count == 0)
                {
                    MessageBox.Show($"Error: No exam subjects found for {student.Name} (Roll No: {student.RollNo})",
                                   "No Subjects", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                studentDetailsList.Add(studentDetails);
                studentExamSubjects.Add(studentDetails, examSubjects);
            }

            if (studentDetailsList.Count > 0)
            {
                CreatePdf();
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

        private void CreatePdf()
        {
            try
            {
                using (PdfDocument document = new PdfDocument())
                {
                    foreach (var studentDetails in studentDetailsList)
                    {
                        var examSubjects = studentExamSubjects[studentDetails];
                        PdfPage page = document.AddPage();
                        page.Orientation = PdfSharp.PageOrientation.Landscape;
                        page.Width = PdfSharp.Drawing.XUnit.FromPoint(842); // A4 landscape width
                        page.Height = PdfSharp.Drawing.XUnit.FromPoint(595); // A4 landscape height

                        using (XGraphics gfx = XGraphics.FromPdfPage(page))
                        {
                            // Cast System.Drawing.FontStyle to PdfSharp.Drawing.XFontStyle
                            XFont headerFont = new XFont("Arial", 14, (PdfSharp.Drawing.XFontStyle)System.Drawing.FontStyle.Bold);
                            XFont titleFont = new XFont("Arial", 12, (PdfSharp.Drawing.XFontStyle)System.Drawing.FontStyle.Bold);
                            XFont regularFont = new XFont("Arial", 10, (PdfSharp.Drawing.XFontStyle)System.Drawing.FontStyle.Regular);
                            XFont boldFont = new XFont("Arial", 10, (PdfSharp.Drawing.XFontStyle)System.Drawing.FontStyle.Bold);

                            int padding = 20;
                            int currentY = padding;

                            // Header
                            gfx.DrawRectangle(XBrushes.LightBlue, padding, currentY, page.Width.Point - 2 * padding, 80);
                            gfx.DrawString("Sunrise Model Public School & College", headerFont, XBrushes.Black,
                                new XRect(padding, currentY + 10, page.Width.Point, 0), XStringFormats.TopCenter);
                            gfx.DrawString("KHUZUAB (B ALOGHEI5740)", regularFont, XBrushes.Black,
                                new XRect(padding, currentY + 40, page.Width.Point, 0), XStringFormats.TopCenter);
                            gfx.DrawString("Phone: 084612999 Mob: 03337881630/03337972999", regularFont, XBrushes.Black,
                                new XRect(padding, currentY + 60, page.Width.Point, 0), XStringFormats.TopCenter);
                            currentY += 100;

                            // Title
                            gfx.DrawString($"DateSheet / Roll No.Slip of session {studentDetails.Session}", titleFont, XBrushes.Black,
                                new XRect(padding, currentY, page.Width.Point, 0), XStringFormats.TopCenter);
                            currentY += 30;

                            // Student Info
                            gfx.DrawRectangle(new XPen(XColors.Black), padding, currentY, page.Width.Point - 2 * padding, 140);
                            string[] labels = { "Name:", "Father's Name:", "Roll No.:", "Group:", "Gender:", "Class:", "Section:", "Session:" };
                            string[] values = {
                                studentDetails.Name,
                                studentDetails.FatherName,
                                studentDetails.RollNo,
                                studentDetails.Group,
                                studentDetails.Gender,
                                studentDetails.Class,
                                studentDetails.Section,
                                studentDetails.Session
                            };

                            for (int i = 0; i < labels.Length; i++)
                            {
                                gfx.DrawString(labels[i], boldFont, XBrushes.Black, padding + 10, currentY + 10 + (i * 20));
                                gfx.DrawString(values[i], regularFont, XBrushes.Black, padding + 140, currentY + 10 + (i * 20));
                            }
                            currentY += 150;

                            // Exam Schedule
                            gfx.DrawString("Exam Schedule", titleFont, XBrushes.Black, padding, currentY);
                            currentY += 20;

                            // Table header
                            int tableWidth = (int)(page.Width.Point - 2 * padding);
                            int[] columnWidths = { 50, 100, 200, 100, 100 };
                            string[] headers = { "Sr#", "Date", "Subject", "Start Time", "End Time" };
                            gfx.DrawRectangle(new XPen(XColors.Black), XBrushes.LightGray, padding, currentY, tableWidth, 20);
                            int x = padding;
                            for (int i = 0; i < headers.Length; i++)
                            {
                                gfx.DrawString(headers[i], boldFont, XBrushes.Black, x + 5, currentY + 5);
                                x += columnWidths[i];
                            }
                            currentY += 20;

                            // Table rows
                            for (int i = 0; i < examSubjects.Count; i++)
                            {
                                var subject = examSubjects[i];
                                gfx.DrawRectangle(new XPen(XColors.Black), padding, currentY, tableWidth, 20);
                                x = padding;
                                string[] row = {
                                    (i + 1).ToString(),
                                    subject.Date.ToString("dd/MM/yyyy"),
                                    subject.SubjectName,
                                    subject.StartTime.ToString("hh:mm tt"),
                                    subject.EndTime.ToString("hh:mm tt")
                                };
                                for (int j = 0; j < row.Length; j++)
                                {
                                    gfx.DrawString(row[j], regularFont, XBrushes.Black, x + 5, currentY + 5);
                                    x += columnWidths[j];
                                }
                                currentY += 20;
                            }

                            // Notes
                            currentY += 10;
                            gfx.DrawString("Notes:", boldFont, XBrushes.Black, padding, currentY);
                            currentY += 20;
                            string notes = GetNotesText();
                            string[] noteLines = notes.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                            foreach (var line in noteLines)
                            {
                                gfx.DrawString(line, regularFont, XBrushes.Black, padding, currentY);
                                currentY += 15;
                            }

                            // Signature and Date
                            currentY += 20;
                            gfx.DrawString("Principal's Signature", boldFont, XBrushes.Black, padding, currentY);
                            gfx.DrawString($"Issued Date & time: {DateTime.Now:yyyy/MM/dd HH:mm:ss}", regularFont, XBrushes.Black,
                                page.Width.Point - padding - 200, currentY);
                        }
                    }

                    // Save PDF to Desktop
                    string pdfPath = System.IO.Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                        $"RollNoSlips_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
                    document.Save(pdfPath);
                    MessageBox.Show($"PDF generated successfully at: {pdfPath}", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Optionally open the PDF
                    Process.Start(new ProcessStartInfo(pdfPath) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating PDF: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetNotesText()
        {
            return @"1) Those students will not be allowed in the exam whom have not paid remaining dues.
2) Late course will not be permitted to sit in the examination according to the given syllabus.
3) Parents are required to get preparation of their children according to the other and any other
4) SSMPs reserves the right to modify the date sheet of annual exam due to weather and any other certain  situation  .";
        }

        private List<ExamSubject> FetchExamSubjectsFromDatabase(string className, string section)
        {
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

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime examDate = reader.GetDateTime("date");
                                string subjectName = reader.GetString("subject");

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
                }
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
}