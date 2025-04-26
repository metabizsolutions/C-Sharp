using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace SchoolManagementSystem.Exam
{
    public partial class RollNoSlipsViewer : Form
    {
        private readonly string connectionString = "server=localhost;database=tnsbay_school;uid=root;pwd=;";

        public RollNoSlipsViewer()
        {
            InitializeComponent();
            btnCancel.Click += (s, e) => this.Close(); // Set btnCancel lambda
            LoadClasses();
            LoadSlips();
        }

        private void LoadClasses()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT DISTINCT c.class_id, c.name 
                        FROM `class` c
                        INNER JOIN `datesheet` d ON TRIM(LOWER(c.name)) = TRIM(LOWER(d.class))
                        ORDER BY c.name";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("class_id", typeof(int));
                        dt.Columns.Add("name", typeof(string));

                        // Add empty option
                        dt.Rows.Add(0, "-- Select Class --");

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(reader["class_id"], reader["name"]);
                            }
                        }

                        cmbClass.DataSource = dt;
                        cmbClass.DisplayMember = "name";
                        cmbClass.ValueMember = "class_id";
                        cmbClass.SelectedIndex = 0; // Select default option
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load classes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSections(int classId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT DISTINCT s.section_id, s.name 
                        FROM `section` s
                        INNER JOIN `class` c ON s.class_id = c.class_id
                        INNER JOIN `datesheet` d ON TRIM(LOWER(c.name)) = TRIM(LOWER(d.class)) 
                            AND TRIM(LOWER(s.name)) = TRIM(LOWER(d.section))
                        WHERE s.class_id = @classId 
                        ORDER BY s.name";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@classId", classId);

                        DataTable dt = new DataTable();
                        dt.Columns.Add("section_id", typeof(int));
                        dt.Columns.Add("name", typeof(string));

                        // Add empty option
                        dt.Rows.Add(0, "-- Select Section --");

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(reader["section_id"], reader["name"]);
                            }
                        }

                        cmbSection.DataSource = dt;
                        cmbSection.DisplayMember = "name";
                        cmbSection.ValueMember = "section_id";
                        cmbSection.SelectedIndex = 0; // Select default option
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load sections: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStudents(int sectionId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT DISTINCT s.student_id, s.name, s.roll 
                        FROM `student` s
                        INNER JOIN `roll_no_slips` r ON TRIM(LOWER(s.name)) = TRIM(LOWER(r.student_name))
                        WHERE s.section_id = @sectionId 
                        ORDER BY s.roll";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@sectionId", sectionId);

                        DataTable dt = new DataTable();
                        dt.Columns.Add("student_id", typeof(int));
                        dt.Columns.Add("display", typeof(string));
                        dt.Columns.Add("name", typeof(string));
                        dt.Columns.Add("roll", typeof(int));

                        // Add empty option
                        dt.Rows.Add(0, "-- Select Student --", "", 0);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int roll = reader.IsDBNull(reader.GetOrdinal("roll")) ? 0 : reader.GetInt32("roll");
                                dt.Rows.Add(
                                    reader["student_id"],
                                    $"{reader["name"]} (Roll: {roll})",
                                    reader["name"],
                                    roll
                                );
                            }
                        }

                        cmbStudentName.DataSource = dt;
                        cmbStudentName.DisplayMember = "display";
                        cmbStudentName.ValueMember = "student_id";
                        cmbStudentName.SelectedIndex = 0; // Select default option
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load students: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbClass.SelectedValue != null && cmbClass.SelectedValue is int classId && classId > 0)
            {
                LoadSections(classId);
            }
            else
            {
                // Reset sections and students combos
                DataTable dt = new DataTable();
                dt.Columns.Add("section_id", typeof(int));
                dt.Columns.Add("name", typeof(string));
                dt.Rows.Add(0, "-- Select Section --");
                cmbSection.DataSource = dt;
                cmbSection.SelectedIndex = 0;

                dt = new DataTable();
                dt.Columns.Add("student_id", typeof(int));
                dt.Columns.Add("display", typeof(string));
                dt.Columns.Add("name", typeof(string));
                dt.Columns.Add("roll", typeof(int));
                dt.Rows.Add(0, "-- Select Student --", "", 0);
                cmbStudentName.DataSource = dt;
                cmbStudentName.SelectedIndex = 0;
            }

            LoadSlipsWithCurrentFilters();
        }

        private void CmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSection.SelectedValue != null && cmbSection.SelectedValue is int sectionId && sectionId > 0)
            {
                LoadStudents(sectionId);
            }
            else
            {
                // Reset students combo
                DataTable dt = new DataTable();
                dt.Columns.Add("student_id", typeof(int));
                dt.Columns.Add("display", typeof(string));
                dt.Columns.Add("name", typeof(string));
                dt.Columns.Add("roll", typeof(int));
                dt.Rows.Add(0, "-- Select Student --", "", 0);
                cmbStudentName.DataSource = dt;
                cmbStudentName.SelectedIndex = 0;
            }

            LoadSlipsWithCurrentFilters();
        }

        private void CmbStudentName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSlipsWithCurrentFilters();
        }

        private void LoadSlipsWithCurrentFilters()
        {
            try
            {
                int? classId = cmbClass.SelectedValue as int?;
                int? sectionId = cmbSection.SelectedValue as int?;
                int? studentId = cmbStudentName.SelectedValue as int?;

                string className = classId > 0 ? cmbClass.Text : null;
                string sectionName = sectionId > 0 ? cmbSection.Text : null;
                string studentName = studentId > 0 ? (cmbStudentName.SelectedItem as DataRowView)?["name"]?.ToString() : null;

                LoadSlips(className, sectionName, studentName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying filters: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSlips(string classFilter = null, string sectionFilter = null, string studentNameFilter = null)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query;
                    if (studentNameFilter != null)
                    {
                        // When student is selected, ignore class and section filters
                        query = @"
                            SELECT r.slip_id, r.student_name, r.father_name, r.roll_no, r.class, r.section, r.group_name, r.gender, r.session, r.issued_datetime
                            FROM roll_no_slips r
                            WHERE TRIM(LOWER(r.student_name)) = TRIM(LOWER(@studentName))
                            ORDER BY r.issued_datetime DESC";
                    }
                    else
                    {
                        // Apply class and section filters with datesheet validation
                        query = @"
                            SELECT DISTINCT r.slip_id, r.student_name, r.father_name, r.roll_no, r.class, r.section, r.group_name, r.gender, r.session, r.issued_datetime
                            FROM roll_no_slips r
                            INNER JOIN datesheet d ON TRIM(LOWER(r.class)) = TRIM(LOWER(d.class))
                                AND (@section IS NULL OR TRIM(LOWER(r.section)) = TRIM(LOWER(d.section)))
                            WHERE (@class IS NULL OR TRIM(LOWER(r.class)) = TRIM(LOWER(@class)))
                              AND (@section IS NULL OR TRIM(LOWER(r.section)) = TRIM(LOWER(@section)))
                            ORDER BY r.issued_datetime DESC";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@class", (object)classFilter ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@section", (object)sectionFilter ?? DBNull.Value);
                        if (studentNameFilter != null)
                        {
                            cmd.Parameters.AddWithValue("@studentName", studentNameFilter);
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            // Clear existing data to prevent stale records
                            dataGridViewSlips.DataSource = null;
                            dataGridViewSlips.DataSource = table;

                            // Log the number of records loaded for debugging
                            System.Diagnostics.Debug.WriteLine($"Loaded {table.Rows.Count} slips with filters: class={classFilter}, section={sectionFilter}, studentName={studentNameFilter}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading slips: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridViewSlips_SelectionChanged(object sender, EventArgs e)
        {
            bool hasSelection = dataGridViewSlips.SelectedRows.Count > 0;
            btnPrint.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;

            previewPictureBox.Image?.Dispose();
            previewPictureBox.Image = null;

            if (hasSelection)
            {
                try
                {
                    var row = dataGridViewSlips.SelectedRows[0].DataBoundItem as DataRowView;
                    if (row != null)
                    {
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
                        DrawPreview(studentDetails, subjects);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error generating preview: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DrawErrorMessage("Error generating preview");
                }
            }
            else
            {
                DrawErrorMessage("Select a roll number slip to preview");
            }
        }

        private void DrawPreview(SchoolManagementSystem.Roll_No_Slip.StudentDetails studentDetails, List<SchoolManagementSystem.Roll_No_Slip.ExamSubject> examSubjects)
        {
            // High-resolution rendering (2x scale)
            const float renderScale = 2.0f;
            int baseWidth = 842;
            int baseHeight = 595;
            int subjectHeight = examSubjects.Count * 25;
            int notesHeight = GetNotesText().Split(new[] { "\r\n", "\n" }, StringSplitOptions.None).Length * 20;
            int totalHeight = baseHeight + subjectHeight + notesHeight + 50;
            int scaledWidth = (int)(baseWidth * renderScale);
            int scaledHeight = (int)(totalHeight * renderScale);

            Bitmap highResBitmap = new Bitmap(scaledWidth, scaledHeight);
            using (Graphics gfx = Graphics.FromImage(highResBitmap))
            {
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.TextRenderingHint = TextRenderingHint.AntiAlias;
                gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gfx.ScaleTransform(renderScale, renderScale);

                Font headerFont = new Font("Arial", 11, FontStyle.Bold);
                Font titleFont = new Font("Arial", 9, FontStyle.Bold);
                Font regularFont = new Font("Arial", 8, FontStyle.Regular);
                Font boldFont = new Font("Arial", 8, FontStyle.Bold);
                Brush blackBrush = Brushes.Black;
                Brush lightBlueBrush = new SolidBrush(Color.LightBlue);
                Pen blackPen = new Pen(Color.Black);

                int padding = 20;
                int currentY = padding;

                // Header
                gfx.FillRectangle(lightBlueBrush, padding, currentY, baseWidth - 2 * padding, 70);
                gfx.DrawString("Sunrise Model Public School & College", headerFont, blackBrush,
                    new Rectangle(padding, currentY + 10, baseWidth, 0), new StringFormat { Alignment = StringAlignment.Center });
                gfx.DrawString("KHUZUAB (B ALOGHEI5740)", regularFont, blackBrush,
                    new Rectangle(padding, currentY + 30, baseWidth, 0), new StringFormat { Alignment = StringAlignment.Center });
                gfx.DrawString("Phone: 084612999 Mob: 03337881630/03337972999", regularFont, blackBrush,
                    new Rectangle(padding, currentY + 45, baseWidth, 0), new StringFormat { Alignment = StringAlignment.Center });
                currentY += 80;

                // Title
                gfx.DrawString($"DateSheet / Roll No.Slip of session {studentDetails.Session}", titleFont, blackBrush,
                    new Rectangle(padding, currentY, baseWidth, 0), new StringFormat { Alignment = StringAlignment.Center });
                currentY += 40;

                // Student Info
                gfx.DrawRectangle(blackPen, padding, currentY, baseWidth - 2 * padding, 160);
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
                    gfx.DrawString(labels[i], boldFont, blackBrush, padding + 10, currentY + 10 + (i * 20));
                    gfx.DrawString(values[i], regularFont, blackBrush, padding + 140, currentY + 10 + (i * 20));
                }
                currentY += 170;

                // Exam Schedule
                gfx.DrawString("Exam Schedule", titleFont, blackBrush, padding, currentY);
                currentY += 30;

                // Table header
                int tableWidth = baseWidth - 2 * padding;
                int[] columnWidths = { 50, 100, 200, 100, 100 };
                string[] headers = { "Sr#", "Date", "Subject", "Start Time", "End Time" };
                gfx.FillRectangle(Brushes.LightGray, padding, currentY, tableWidth, 20);
                gfx.DrawRectangle(blackPen, padding, currentY, tableWidth, 20);
                int x = padding;
                for (int i = 0; i < headers.Length; i++)
                {
                    gfx.DrawString(headers[i], boldFont, blackBrush, x + 5, currentY + 5);
                    x += columnWidths[i];
                }
                currentY += 25;

                // Table rows
                for (int i = 0; i < examSubjects.Count; i++)
                {
                    var subject = examSubjects[i];
                    gfx.DrawRectangle(blackPen, padding, currentY, tableWidth, 20);
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
                        gfx.DrawString(row[j], regularFont, blackBrush, x + 5, currentY + 5);
                        x += columnWidths[j];
                    }
                    currentY += 25;
                }

                // Notes
                currentY += 30;
                gfx.DrawString("Notes:", boldFont, blackBrush, padding, currentY);
                currentY += 30;
                string notes = GetNotesText();
                string[] noteLines = notes.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                foreach (var line in noteLines)
                {
                    gfx.DrawString(line, regularFont, blackBrush, padding, currentY);
                    currentY += 20;
                }

                // Signature and Date
                currentY += 30;
                gfx.DrawString("Principal's Signature", boldFont, blackBrush, padding, currentY);
                gfx.DrawString($"Issued Date & time: {DateTime.Now:yyyy/MM/dd HH:mm:ss}", regularFont, blackBrush,
                    baseWidth - padding - 200, currentY);
            }

            // Scale to a target width of 600 pixels
            const int targetWidth = 600;
            float scale = (float)targetWidth / highResBitmap.Width;
            int finalWidth = targetWidth;
            int finalHeight = (int)(highResBitmap.Height * scale);

            Bitmap scaledBitmap = new Bitmap(finalWidth, finalHeight);
            using (Graphics scaleGfx = Graphics.FromImage(scaledBitmap))
            {
                scaleGfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                scaleGfx.DrawImage(highResBitmap, 0, 0, finalWidth, finalHeight);
            }

            highResBitmap.Dispose();
            previewPictureBox.Image = scaledBitmap;
            previewPictureBox.Size = new Size(finalWidth, finalHeight);
        }

        private void DrawErrorMessage(string message)
        {
            const float renderScale = 2.0f;
            int scaledWidth = (int)(842 * renderScale);
            int scaledHeight = (int)(595 * renderScale);

            Bitmap highResBitmap = new Bitmap(scaledWidth, scaledHeight);
            using (Graphics gfx = Graphics.FromImage(highResBitmap))
            {
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.TextRenderingHint = TextRenderingHint.AntiAlias;
                gfx.ScaleTransform(renderScale, renderScale);

                Font errorFont = new Font("Arial", 9, FontStyle.Bold);
                Brush blackBrush = Brushes.Black;
                gfx.Clear(Color.White);
                gfx.DrawString(message, errorFont, blackBrush,
                    new Rectangle(20, 20, 842, 595), new StringFormat { Alignment = StringAlignment.Near });
            }

            const int targetWidth = 600;
            float scale = (float)targetWidth / highResBitmap.Width;
            int finalWidth = targetWidth;
            int finalHeight = (int)(highResBitmap.Height * scale);

            Bitmap scaledBitmap = new Bitmap(finalWidth, finalHeight);
            using (Graphics scaleGfx = Graphics.FromImage(scaledBitmap))
            {
                scaleGfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                scaleGfx.DrawImage(highResBitmap, 0, 0, finalWidth, finalHeight);
            }

            highResBitmap.Dispose();
            previewPictureBox.Image = scaledBitmap;
            previewPictureBox.Size = new Size(finalWidth, finalHeight);
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

                        string deleteSubjectsQuery = "DELETE FROM roll_no_slip_subjects WHERE slip_id = @slipId";
                        using (MySqlCommand cmd = new MySqlCommand(deleteSubjectsQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@slipId", slipId);
                            cmd.ExecuteNonQuery();
                        }

                        string deleteSlipQuery = "DELETE FROM roll_no_slips WHERE slip_id = @slipId";
                        using (MySqlCommand cmd = new MySqlCommand(deleteSlipQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@slipId", slipId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    LoadSlipsWithCurrentFilters();
                    previewPictureBox.Image?.Dispose();
                    previewPictureBox.Image = null;
                    DrawErrorMessage("Select a roll number slip to preview");
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
                page.Width = XUnit.FromPoint(842);
                page.Height = XUnit.FromPoint(595);

                using (XGraphics gfx = XGraphics.FromPdfPage(page))
                {
                    XFont headerFont = new XFont("Arial", 14, (XFontStyle)FontStyle.Bold);
                    XFont titleFont = new XFont("Arial", 12, (XFontStyle)FontStyle.Bold);
                    XFont regularFont = new XFont("Arial", 10, (XFontStyle)FontStyle.Regular);
                    XFont boldFont = new XFont("Arial", 10, (XFontStyle)FontStyle.Bold);

                    int padding = 20;
                    int currentY = padding;

                    gfx.DrawRectangle(XBrushes.LightBlue, padding, currentY, page.Width.Point - 2 * padding, 80);
                    gfx.DrawString("Sunrise Model Public School & College", headerFont, XBrushes.Black,
                        new XRect(padding, currentY + 10, page.Width.Point, 0), XStringFormats.TopCenter);
                    gfx.DrawString("KHUZUAB (B ALOGHEI5740)", regularFont, XBrushes.Black,
                        new XRect(padding, currentY + 40, page.Width.Point, 0), XStringFormats.TopCenter);
                    gfx.DrawString("Phone: 084612999 Mob: 03337881630/03337972999", regularFont, XBrushes.Black,
                        new XRect(padding, currentY + 60, page.Width.Point, 0), XStringFormats.TopCenter);
                    currentY += 100;

                    gfx.DrawString($"DateSheet / Roll No.Slip of session {studentDetails.Session}", titleFont, XBrushes.Black,
                        new XRect(padding, currentY, page.Width.Point, 0), XStringFormats.TopCenter);
                    currentY += 30;

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

                    gfx.DrawString("Exam Schedule", titleFont, XBrushes.Black, padding, currentY);
                    currentY += 20;

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

                    currentY += 20;
                    gfx.DrawString("Principal's Signature", boldFont, XBrushes.Black, padding, currentY);
                    gfx.DrawString($"Issued Date & time: {DateTime.Now:yyyy/MM/dd HH:mm:ss}", regularFont, XBrushes.Black,
                        page.Width.Point - padding - 200, currentY);
                }

                string sanitizedFileName = $"Name={studentDetails.Name}_Class={studentDetails.Class}_Section={studentDetails.Section}_RollNo={studentDetails.RollNo}"
    .Replace(" ", "_")  // Replace spaces with underscores
    .Replace("/", "-")  // Replace slashes (invalid in file names)
    .Replace("\\", "-") // Replace backslashes
    .Replace(":", "-")  // Replace colons
    .Replace("*", "-")  // Replace asterisks
    .Replace("?", "")   // Remove question marks
    .Replace("\"", "")  // Remove double quotes
    .Replace("<", "")   // Remove less than
    .Replace(">", "")   // Remove greater than
    .Replace("|", "");  // Remove pipe

                string pdfPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"{sanitizedFileName}.pdf");

                document.Save(pdfPath);
                MessageBox.Show($"PDF generated successfully at: {pdfPath}", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                Process.Start(new ProcessStartInfo(pdfPath) { UseShellExecute = true });
            }
        }

        public string GetNotesText()
        {
            string notes = string.Empty;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
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