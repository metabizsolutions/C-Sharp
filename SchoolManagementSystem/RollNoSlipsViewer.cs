using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;

namespace SchoolManagementSystem.Exam
{
    public partial class RollNoSlipsViewer : Form
    {
        private DataGridView dataGridViewSlips;
        private Button btnPrint;
        private Button btnDelete;
        private Button btnCancel;
        private readonly string connectionString = "server=localhost;database=tnsbay_school;uid=root;pwd=;";

        public RollNoSlipsViewer()
        {
            InitializeComponents();
            LoadSlips();
        }

        private void InitializeComponents()
        {
            this.Text = "View Roll Number Slips";
            this.Size = new System.Drawing.Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            // DataGridView
            dataGridViewSlips = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                MultiSelect = false
            };
            dataGridViewSlips.SelectionChanged += DataGridViewSlips_SelectionChanged;

            // Buttons panel
            var panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                FlowDirection = FlowDirection.RightToLeft
            };

            btnPrint = new Button
            {
                Text = "Print Selected Slip",
                Enabled = false,
                Width = 150
            };
            btnPrint.Click += BtnPrint_Click;

            btnDelete = new Button
            {
                Text = "Delete Selected Slip",
                Enabled = false,
                Width = 150
            };
            btnDelete.Click += BtnDelete_Click;

            btnCancel = new Button
            {
                Text = "Close",
                Width = 100
            };
            btnCancel.Click += (s, e) => this.Close();

            panel.Controls.Add(btnCancel);
            panel.Controls.Add(btnDelete); // Added Delete button
            panel.Controls.Add(btnPrint);

            this.Controls.Add(dataGridViewSlips);
            this.Controls.Add(panel);
        }

        private void DataGridViewSlips_SelectionChanged(object sender, EventArgs e)
        {
            bool hasSelection = dataGridViewSlips.SelectedRows.Count > 0;
            btnPrint.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection; // Enable/disable Delete button
        }

        private void LoadSlips()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                    SELECT slip_id, student_name, father_name, roll_no, class, section, group_name, gender, session, issued_datetime
                    FROM roll_no_slips
                    ORDER BY issued_datetime DESC";

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dataGridViewSlips.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading slips: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridViewSlips.SelectedRows.Count == 0)
                return;

            try
            {
                var row = dataGridViewSlips.SelectedRows[0].DataBoundItem as DataRowView;
                if (row == null)
                    return;

                long slipId = Convert.ToInt64(row["slip_id"]);
                var studentDetails = new SchoolManagementSystem.Roll_No_Slip.StudentDetails
                {
                    Name = row["student_name"].ToString(),
                    FatherName = row["father_name"].ToString(),
                    RollNo = row["roll_no"].ToString(),
                    Class = row["class"].ToString(),
                    Section = row["section"].ToString(),
                    Group = row["group_name"].ToString(),
                    Gender = row["gender"].ToString(),
                    Session = row["session"].ToString()
                };

                var subjects = GetSlipSubjects(slipId);
                GeneratePdf(studentDetails, subjects);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing slip: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewSlips.SelectedRows.Count == 0)
                return;

            var row = dataGridViewSlips.SelectedRows[0].DataBoundItem as DataRowView;
            if (row == null)
                return;

            var result = MessageBox.Show(
                "Are you sure you want to delete this roll number slip?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    long slipId = Convert.ToInt64(row["slip_id"]);
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        // Delete from roll_no_slip_subjects first (child table)
                        string deleteSubjectsQuery = "DELETE FROM roll_no_slip_subjects WHERE slip_id = @slipId";
                        using (MySqlCommand cmd = new MySqlCommand(deleteSubjectsQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@slipId", slipId);
                            cmd.ExecuteNonQuery();
                        }

                        // Delete from roll_no_slips (parent table)
                        string deleteSlipQuery = "DELETE FROM roll_no_slips WHERE slip_id = @slipId";
                        using (MySqlCommand cmd = new MySqlCommand(deleteSlipQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@slipId", slipId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Refresh the grid
                    LoadSlips();
                    MessageBox.Show("Roll number slip deleted successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting slip: {ex.Message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private List<SchoolManagementSystem.Roll_No_Slip.ExamSubject> GetSlipSubjects(long slipId)
        {
            var subjects = new List<SchoolManagementSystem.Roll_No_Slip.ExamSubject>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                SELECT subject_name, exam_date, start_time, end_time
                FROM roll_no_slip_subjects
                WHERE slip_id = @slipId
                ORDER BY sr_no";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@slipId", slipId);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime examDate = reader.GetDateTime("exam_date");
                            TimeSpan startTime = reader.GetTimeSpan("start_time");
                            TimeSpan endTime = reader.GetTimeSpan("end_time");

                            subjects.Add(new SchoolManagementSystem.Roll_No_Slip.ExamSubject
                            {
                                SubjectName = reader.GetString("subject_name"),
                                Date = examDate,
                                StartTime = examDate.Add(startTime),
                                EndTime = examDate.Add(endTime)
                            });
                        }
                    }
                }
            }
            return subjects;
        }

        private void GeneratePdf(SchoolManagementSystem.Roll_No_Slip.StudentDetails studentDetails, List<SchoolManagementSystem.Roll_No_Slip.ExamSubject> examSubjects)
        {
            using (PdfDocument document = new PdfDocument())
            {
                PdfPage page = document.AddPage();
                page.Orientation = PdfSharp.PageOrientation.Landscape;
                page.Width = PdfSharp.Drawing.XUnit.FromPoint(842);
                page.Height = PdfSharp.Drawing.XUnit.FromPoint(595); // Adjusted to standard A4 height

                using (XGraphics gfx = XGraphics.FromPdfPage(page))
                {
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
                    gfx.DrawRectangle(new XPen(XColors.Black), padding, currentY, page.Width.Point - 2 * padding, 160);
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
                    currentY += 190;

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
                        gfx.DrawString(headers[i], boldFont, XBrushes.Black, x + 5, currentY + 15);
                        x += columnWidths[i];
                    }
                    currentY += 30;

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
                            gfx.DrawString(row[j], regularFont, XBrushes.Black, x + 5, currentY + 15);
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

                // Save PDF to Desktop
                string pdfPath = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"RollNoSlip_Reprint_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
                document.Save(pdfPath);
                MessageBox.Show($"PDF generated successfully at: {pdfPath}", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open the PDF
                Process.Start(new ProcessStartInfo(pdfPath) { UseShellExecute = true });
            }
        }

        public string GetNotesText()
        {
            string notes = string.Empty;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString)) // Use class connectionString
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT description FROM settings WHERE type='roll_slip_tnc'", con);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            notes = reader["description"].ToString();
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                notes = "Error retrieving notes: " + ex.Message;
            }
            return notes;
        }
    }
}