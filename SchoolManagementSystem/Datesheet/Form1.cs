using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Layout.Properties;


namespace SchoolManagementSystem.Datesheet
{
    public partial class Form1 : Form
    {
        private Dictionary<string, int> classIdMap = new Dictionary<string, int>();
        private Dictionary<string, int> sectionIdMap = new Dictionary<string, int>();
        private Dictionary<string, int> subjectIdMap = new Dictionary<string, int>();

        public Form1()
        {
            InitializeComponent();
            LoadClassNames();
            LoadTeacherNames();
            // LoadSectionNames and LoadSubjectNames will be called dynamically
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
        }

        private void LoadClassNames()
        {
            string connectionString = "server=localhost;database=tnsbay_school;uid=root;pwd=;";
            comboBox1.Items.Clear();
            classIdMap.Clear();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT class_id, name FROM class";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int classId = reader.GetInt32("class_id");
                        string className = reader.GetString("name");
                        string displayText = $"{className} (ID: {classId})";
                        comboBox1.Items.Add(displayText);
                        classIdMap[displayText] = classId;
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading classes: " + ex.Message);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.SelectedIndex = -1;
            comboBox3.Items.Clear();
            comboBox3.SelectedIndex = -1;
            sectionIdMap.Clear();
            subjectIdMap.Clear();

            if (comboBox1.SelectedIndex != -1)
            {
                string selectedClass = comboBox1.SelectedItem.ToString();
                if (classIdMap.ContainsKey(selectedClass))
                {
                    int classId = classIdMap[selectedClass];
                    LoadSectionNames(classId);
                }
            }
        }

        private void LoadSectionNames(int classId)
        {
            string connectionString = "server=localhost;database=tnsbay_school;uid=root;pwd=;";
            comboBox2.Items.Clear();
            sectionIdMap.Clear();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT section_id, name FROM section WHERE class_id = @classId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@classId", classId);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int sectionId = reader.GetInt32("section_id");
                        string sectionName = reader.GetString("name");
                        string displayText = $"{sectionName} (ID: {sectionId})";
                        comboBox2.Items.Add(displayText);
                        sectionIdMap[displayText] = sectionId;
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading sections: " + ex.Message);
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            comboBox3.SelectedIndex = -1;
            subjectIdMap.Clear();

            if (comboBox2.SelectedIndex != -1)
            {
                string selectedSection = comboBox2.SelectedItem.ToString();
                if (sectionIdMap.ContainsKey(selectedSection))
                {
                    int sectionId = sectionIdMap[selectedSection];
                    LoadSubjectNames(sectionId);
                }
            }
        }

        private void LoadSubjectNames(int sectionId)
        {
            string connectionString = "server=localhost;database=tnsbay_school;uid=root;pwd=;";
            comboBox3.Items.Clear();
            subjectIdMap.Clear();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT subject_id, name FROM subject WHERE section_id = @sectionId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@sectionId", sectionId);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int subjectId = reader.GetInt32("subject_id");
                        string subjectName = reader.GetString("name");
                        string displayText = $"{subjectName} (ID: {subjectId})";
                        comboBox3.Items.Add(displayText);
                        subjectIdMap[displayText] = subjectId;
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading subjects: " + ex.Message);
                }
            }
        }

        private void LoadTeacherNames()
        {
            string connectionString = "server=localhost;database=tnsbay_school;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT name FROM teacher";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBox4.Items.Add(reader["name"].ToString());
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading teachers: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;
            InitializeDataGridViewColumns();
            AddScheduleEntry();
        }

        private bool ValidateInputs()
        {
            if (comboBox1.SelectedIndex == -1)
            {
                ShowValidationError("Please select a Class", comboBox1);
                return false;
            }

            if (comboBox2.SelectedIndex == -1)
            {
                ShowValidationError("Please select a Section", comboBox2);
                return false;
            }

            if (comboBox3.SelectedIndex == -1)
            {
                ShowValidationError("Please select a Subject", comboBox3);
                return false;
            }

            if (comboBox4.SelectedIndex == -1)
            {
                ShowValidationError("Please select a Teacher", comboBox4);
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                ShowValidationError("Please enter a Room number/name", textBox1);
                return false;
            }

            return true;
        }

        private void InitializeDataGridViewColumns()
        {
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add("Class", "Class");
                dataGridView1.Columns.Add("Section", "Section");
                dataGridView1.Columns.Add("Subject", "Subject");
                dataGridView1.Columns.Add("Teacher", "Teacher");
                dataGridView1.Columns.Add("Room", "Room");
                dataGridView1.Columns.Add("Date", "Date");

                dataGridView1.Columns["Date"].DefaultCellStyle.Format = "yyyy-MM-dd";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void AddScheduleEntry()
        {
            try
            {
                string room = textBox1.Text.Trim();
                // Extract only the name part without the ID for display
                string className = comboBox1.SelectedItem.ToString().Split('(')[0].Trim();
                string sectionName = comboBox2.SelectedItem.ToString().Split('(')[0].Trim();
                string subjectName = comboBox3.SelectedItem.ToString().Split('(')[0].Trim();

                dataGridView1.Rows.Add(
                    className,
                    sectionName,
                    subjectName,
                    comboBox4.SelectedItem.ToString(),
                    room,
                    dateTimePicker1.Value
                );

                ClearInputs();
                MessageBox.Show("Schedule added successfully!", "Success",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding schedule: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.Items.Clear();
            comboBox2.SelectedIndex = -1;
            comboBox3.Items.Clear();
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            textBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }

        private void ShowValidationError(string message, Control control)
        {
            MessageBox.Show(message, "Validation Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Warning);
            control.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No data to save!", "Information",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string connectionString = "server=localhost;database=tnsbay_school;uid=root;pwd=;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string query = @"INSERT INTO datesheet 
                                    (class, section, subject, 
                                     teacher, room, date)
                                    VALUES (@class, @section, @subject, 
                                            @teacher, @room, @date)";

                            using (MySqlCommand cmd = new MySqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@class", row.Cells["Class"].Value?.ToString() ?? "");
                                cmd.Parameters.AddWithValue("@section", row.Cells["Section"].Value?.ToString() ?? "");
                                cmd.Parameters.AddWithValue("@subject", row.Cells["Subject"].Value?.ToString() ?? "");
                                cmd.Parameters.AddWithValue("@teacher", row.Cells["Teacher"].Value?.ToString() ?? "");
                                cmd.Parameters.AddWithValue("@room", row.Cells["Room"].Value?.ToString() ?? "");

                                if (DateTime.TryParse(row.Cells["Date"].Value?.ToString(), out DateTime scheduleDate))
                                {
                                    cmd.Parameters.AddWithValue("@date", scheduleDate);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@date", DateTime.Today);
                                }

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageBox.Show($"Successfully saved {dataGridView1.Rows.Count - 1} records!",
                                   "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving data: {ex.Message}",
                                  "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select one or more rows to delete.",
                              "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show($"Are you sure you want to delete {dataGridView1.SelectedRows.Count} selected row(s)?",
                                                "Confirm Delete",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    for (int i = dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                    {
                        DataGridViewRow row = dataGridView1.SelectedRows[i];
                        if (!row.IsNewRow)
                        {
                            dataGridView1.Rows.Remove(row);
                        }
                    }

                    MessageBox.Show($"{dataGridView1.SelectedRows.Count} row(s) deleted successfully.",
                                   "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting rows: {ex.Message}",
                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.PageSettings.Landscape = true;
            Font headerFont = new Font("Arial", 16, FontStyle.Bold);
            Font subHeaderFont = new Font("Arial", 12, FontStyle.Bold | FontStyle.Italic);
            Font contentFont = new Font("Arial", 10);
            Pen borderPen = new Pen(Color.Black, 1);

            float pageWidth = e.MarginBounds.Width;
            float yPos = e.MarginBounds.Top;
            float lineSpacing = 30;

            float titleX = e.MarginBounds.Left + (pageWidth - e.Graphics.MeasureString("Exam Date Sheet", headerFont).Width) / 2;
            float subHeaderX = e.MarginBounds.Left + (pageWidth - e.Graphics.MeasureString($"Morning Shift: {morningShiftTime}", subHeaderFont).Width) / 2;

            e.Graphics.DrawString("Exam Date Sheet", headerFont, Brushes.Black, titleX, yPos);
            yPos += lineSpacing;

            e.Graphics.DrawString($"Morning Shift: {morningShiftTime}", subHeaderFont, Brushes.Black, subHeaderX, yPos);
            yPos += lineSpacing;
            e.Graphics.DrawString($"Afternoon Shift: {afternoonShiftTime}", subHeaderFont, Brushes.Black, subHeaderX, yPos);
            yPos += lineSpacing;
            e.Graphics.DrawString($"Friday Time: {fridayTime}", subHeaderFont, Brushes.Black, subHeaderX, yPos);
            yPos += lineSpacing * 2;

            int[] columnWidths = new int[dataGridView1.Columns.Count];
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                int maxWidth = TextRenderer.MeasureText(dataGridView1.Columns[i].HeaderText, contentFont).Width + 20;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;
                    string text = row.Cells[i].Value?.ToString() ?? "";
                    int textWidth = TextRenderer.MeasureText(text, contentFont).Width + 20;
                    if (textWidth > maxWidth) maxWidth = textWidth;
                }
                columnWidths[i] = maxWidth;
            }

            float currentX = e.MarginBounds.Left;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                e.Graphics.DrawRectangle(borderPen, currentX, yPos, columnWidths[col.Index], lineSpacing);
                e.Graphics.DrawString(col.HeaderText, contentFont, Brushes.Black, currentX + 5, yPos + 5);
                currentX += columnWidths[col.Index];
            }
            yPos += lineSpacing;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                currentX = e.MarginBounds.Left;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string text = cell.Value?.ToString() ?? "";
                    e.Graphics.DrawRectangle(borderPen, currentX, yPos, columnWidths[cell.ColumnIndex], lineSpacing);
                    e.Graphics.DrawString(text, contentFont, Brushes.Black, currentX + 5, yPos + 5);
                    currentX += columnWidths[cell.ColumnIndex];
                }
                yPos += lineSpacing;

                if (yPos + lineSpacing > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }
        }

        private string morningShiftTime = "";
        private string afternoonShiftTime = "";
        private string fridayTime = "";

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No records available to print.", "Print Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (Form inputForm = new Form())
            {
                inputForm.Text = "Enter Exam Shift Timings";
                inputForm.Size = new Size(400, 250);
                inputForm.StartPosition = FormStartPosition.CenterScreen;

                Label lblMorning = new Label { Text = "Morning Shift Time:", Left = 10, Top = 20, Width = 120 };
                TextBox txtMorning = new TextBox { Left = 140, Top = 20, Width = 200 };

                Label lblAfternoon = new Label { Text = "Afternoon Shift Time:", Left = 10, Top = 60, Width = 120 };
                TextBox txtAfternoon = new TextBox { Left = 140, Top = 60, Width = 200 };

                Label lblFriday = new Label { Text = "Friday Time:", Left = 10, Top = 100, Width = 120 };
                TextBox txtFriday = new TextBox { Left = 140, Top = 100, Width = 200 };

                Button btnOK = new Button { Text = "OK", Left = 140, Top = 140, Width = 80 };
                btnOK.DialogResult = DialogResult.OK;

                inputForm.Controls.Add(lblMorning);
                inputForm.Controls.Add(txtMorning);
                inputForm.Controls.Add(lblAfternoon);
                inputForm.Controls.Add(txtAfternoon);
                inputForm.Controls.Add(lblFriday);
                inputForm.Controls.Add(txtFriday);
                inputForm.Controls.Add(btnOK);

                inputForm.AcceptButton = btnOK;

                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    morningShiftTime = txtMorning.Text.Trim();
                    afternoonShiftTime = txtAfternoon.Text.Trim();
                    fridayTime = txtFriday.Text.Trim();
                }
                else
                {
                    return;
                }
            }

            PrintDocument printDocument = new PrintDocument();
            printDocument.DefaultPageSettings.Landscape = true;
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog
            {
                Document = printDocument,
                Width = 800,
                Height = 600
            };

            printPreviewDialog.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Create a new form to display the datesheets
            Form datesheetForm = new Form
            {
                Text = "Saved Datesheets",
                Size = new Size(1000, 600), // Increased width to accommodate more content
                StartPosition = FormStartPosition.CenterScreen
            };

            // Create a DataGridView to display the data
            DataGridView dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells, // Changed to AllCells
                ReadOnly = true,
                AllowUserToResizeColumns = true,
                AllowUserToOrderColumns = true
            };

            // Create a Print button
            Button printButton = new Button
            {
                Text = "Print DateSheet",
                Size = new Size(150, 30),
                Location = new Point(10, 10)
            };

            // Create a panel to hold the button
            Panel buttonPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50
            };
            buttonPanel.Controls.Add(printButton);

            // Add controls to the form
            datesheetForm.Controls.Add(dataGridView);
            datesheetForm.Controls.Add(buttonPanel);

            // Load data into DataGridView
            string connectionString = "server=localhost;database=tnsbay_school;uid=root;pwd=;";
            DataTable dataTable = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id, class, section, subject, teacher, room, date, created_at FROM datesheet";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);

                    dataGridView.DataSource = dataTable;

                    // Format columns and set minimum widths
                    foreach (DataGridViewColumn column in dataGridView.Columns)
                    {
                        column.MinimumWidth = 100; // Set a reasonable minimum width
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }

                    if (dataGridView.Columns["date"] != null)
                    {
                        dataGridView.Columns["date"].DefaultCellStyle.Format = "yyyy-MM-dd";
                    }
                    if (dataGridView.Columns["created_at"] != null)
                    {
                        dataGridView.Columns["created_at"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading datesheets: {ex.Message}", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Variables to store shift timings
            string morningShiftTime = "";
            string afternoonShiftTime = "";
            string fridayTime = "";

            // Print button click event
            printButton.Click += (s, ev) =>
            {
                // Prompt for shift timings
                using (Form inputForm = new Form())
                {


                    inputForm.Text = "Enter Exam Shift Timings";
                    inputForm.Size = new Size(400, 250);
                    inputForm.StartPosition = FormStartPosition.CenterScreen;

                    Label lblMorning = new Label { Text = "Morning Shift Time:", Left = 10, Top = 20, Width = 120 };
                    TextBox txtMorning = new TextBox { Left = 140, Top = 20, Width = 200 };

                    Label lblAfternoon = new Label { Text = "Afternoon Shift Time:", Left = 10, Top = 60, Width = 120 };
                    TextBox txtAfternoon = new TextBox { Left = 140, Top = 60, Width = 200 };

                    Label lblFriday = new Label { Text = "Friday Time:", Left = 10, Top = 100, Width = 120 };
                    TextBox txtFriday = new TextBox { Left = 140, Top = 100, Width = 200 };

                    Button btnOK = new Button { Text = "OK", Left = 140, Top = 140, Width = 80 };
                    btnOK.DialogResult = DialogResult.OK;

                    inputForm.Controls.Add(lblMorning);
                    inputForm.Controls.Add(txtMorning);
                    inputForm.Controls.Add(lblAfternoon);
                    inputForm.Controls.Add(txtAfternoon);
                    inputForm.Controls.Add(lblFriday);
                    inputForm.Controls.Add(txtFriday);
                    inputForm.Controls.Add(btnOK);

                    inputForm.AcceptButton = btnOK;

                    if (inputForm.ShowDialog() == DialogResult.OK)
                    {
                        morningShiftTime = txtMorning.Text.Trim();
                        afternoonShiftTime = txtAfternoon.Text.Trim();
                        fridayTime = txtFriday.Text.Trim();
                    }
                    else
                    {
                        return;
                    }

                }

                string assessmentTitle = "FIRST TERM SCHOOL BASED ASSESSMENT (SBA) 2024-25";
                string footerText = "Term-1 SBA 2025 Datesheet and Letters";

                // Prompt for assessment title
                using (Form textInputForm = new Form())
                {
                    textInputForm.Text = "Enter Assessment Title";
                    textInputForm.Size = new Size(500, 150);
                    textInputForm.StartPosition = FormStartPosition.CenterScreen;

                    Label lblTitle = new Label { Text = "Assessment Title:", Left = 10, Top = 20, Width = 120 };
                    TextBox txtTitle = new TextBox { Text = assessmentTitle, Left = 140, Top = 20, Width = 320 };

                    Button btnOK = new Button { Text = "OK", Left = 200, Top = 70, Width = 80 };
                    btnOK.DialogResult = DialogResult.OK;

                    textInputForm.Controls.Add(lblTitle);
                    textInputForm.Controls.Add(txtTitle);
                    textInputForm.Controls.Add(btnOK);

                    if (textInputForm.ShowDialog() == DialogResult.OK)
                    {
                        assessmentTitle = txtTitle.Text.Trim();
                    }
                }

                // Prompt for footer text
                using (Form textInputForm = new Form())
                {
                    textInputForm.Text = "Enter Footer Text";
                    textInputForm.Size = new Size(500, 150);
                    textInputForm.StartPosition = FormStartPosition.CenterScreen;

                    Label lblFooter = new Label { Text = "Footer Text:", Left = 10, Top = 20, Width = 120 };
                    TextBox txtFooter = new TextBox { Text = footerText, Left = 140, Top = 20, Width = 320 };

                    Button btnOK = new Button { Text = "OK", Left = 200, Top = 70, Width = 80 };
                    btnOK.DialogResult = DialogResult.OK;

                    textInputForm.Controls.Add(lblFooter);
                    textInputForm.Controls.Add(txtFooter);
                    textInputForm.Controls.Add(btnOK);

                    if (textInputForm.ShowDialog() == DialogResult.OK)
                    {
                        footerText = txtFooter.Text.Trim();
                    }
                }

                // Set up PrintDocument
                PrintDocument printDocument = new PrintDocument();
                printDocument.DefaultPageSettings.Landscape = true;
                printDocument.PrintPage += (printSender, args) => PrintDocument_PrintPage(args, dataTable, morningShiftTime, afternoonShiftTime, fridayTime, assessmentTitle, footerText);

                // Show PrintPreviewDialog
                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog
                {
                    Document = printDocument,
                    Width = 1000,
                    Height = 600
                };

                printPreviewDialog.ShowDialog();

                // Prompt to save as PDF
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    Title = "Save DateSheet PDF",
                    FileName = $"DateSheet_2024-25.pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string filePath = saveFileDialog.FileName;
                        string directory = Path.GetDirectoryName(filePath);
                        if (!Directory.Exists(directory))
                        {
                            MessageBox.Show("The selected directory does not exist.", "Error",
                                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        try
                        {
                            using (FileStream fs = File.Open(filePath, FileMode.CreateNew))
                            {
                                fs.Close();
                            }
                        }
                        catch (IOException ioEx)
                        {
                            MessageBox.Show($"Cannot access the file. It may be in use or you lack permissions: {ioEx.Message}", "Error",
                                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Generate PDF using iText7
                        using (var writer = new PdfWriter(filePath))
                        using (var pdf = new PdfDocument(writer))
                        using (var document = new Document(pdf)) // Landscape
                        {
                            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                            PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                            // Title
                            document.Add(new Paragraph("DATE SHEET")
                                .SetFont(boldFont)
                                .SetFontSize(18)
                                .SetTextAlignment(TextAlignment.CENTER));

                            document.Add(new Paragraph(assessmentTitle)
                                .SetFont(boldFont)
                                .SetFontSize(12)
                                .SetTextAlignment(TextAlignment.CENTER));

                            document.Add(new Paragraph($"MORNING SHIFT TIMING: {morningShiftTime}")
                                .SetFont(font)
                                .SetFontSize(10)
                                .SetTextAlignment(TextAlignment.CENTER));

                            document.Add(new Paragraph($"AFTERNOON SHIFT TIMING: {afternoonShiftTime} (Friday {fridayTime})")
                                .SetFont(font)
                                .SetFontSize(10)
                                .SetTextAlignment(TextAlignment.CENTER));

                            // Get unique class names from the dataTable
                            var classNames = dataTable.AsEnumerable()
                                .Select(row => row.Field<string>("class"))
                                .Distinct()
                                .OrderBy(c => c)
                                .ToList();

                            // Calculate column widths based on content
                            float[] columnWidths = CalculateColumnWidths(dataTable, classNames, font, boldFont);

                            // Create table with calculated column widths
                            Table table = new Table(columnWidths)
                                .UseAllAvailableWidth();

                            // Table headers
                            table.AddHeaderCell(new Cell().Add(new Paragraph("DATE").SetFont(boldFont)));
                            table.AddHeaderCell(new Cell().Add(new Paragraph("DAY").SetFont(boldFont)));
                            foreach (var className in classNames)
                            {
                                table.AddHeaderCell(new Cell().Add(new Paragraph($"Class {className}").SetFont(boldFont)));
                            }

                            // Group data by date and map subjects to classes
                            var dates = dataTable.AsEnumerable()
                                .GroupBy(row => row.Field<DateTime>("date").ToString("yyyy-MM-dd"))
                                .OrderBy(g => g.Key);

                            Dictionary<string, Dictionary<string, string>> dateClassSubjects = new Dictionary<string, Dictionary<string, string>>();
                            foreach (DataRow row in dataTable.Rows)
                            {
                                string date = row.Field<DateTime>("date").ToString("yyyy-MM-dd");
                                string className = row.Field<string>("class");
                                string subject = row.Field<string>("subject");
                                if (!dateClassSubjects.ContainsKey(date))
                                    dateClassSubjects[date] = new Dictionary<string, string>();
                                dateClassSubjects[date][className] = subject;
                            }

                            // Add rows to the table
                            foreach (var dateGroup in dates)
                            {
                                string date = dateGroup.Key;
                                string day = DateTime.Parse(date).ToString("dddd");
                                table.AddCell(new Cell().Add(new Paragraph(date).SetFont(font)));
                                table.AddCell(new Cell().Add(new Paragraph(day).SetFont(font)));

                                foreach (var className in classNames)
                                {
                                    string subject = dateClassSubjects.ContainsKey(date) && dateClassSubjects[date].ContainsKey(className)
                                        ? dateClassSubjects[date][className]
                                        : "-";
                                    table.AddCell(new Cell().Add(new Paragraph(subject).SetFont(font)));
                                }
                            }

                            document.Add(table);
                            // And later for the footer:
                            document.Add(new Paragraph(footerText)
                                .SetFont(font)
                                .SetFontSize(10)
                                .SetTextAlignment(TextAlignment.CENTER));
                        }

                        MessageBox.Show("PDF generated successfully!", "Success",
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error generating PDF: {ex.Message}", "Error",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };

            // Show the form
            datesheetForm.ShowDialog();
        }

        // Helper method to calculate column widths based on content
        private float[] CalculateColumnWidths(DataTable dataTable, List<string> classNames, PdfFont font, PdfFont boldFont)
        {
            // Initialize column widths
            float dateWidth = boldFont.GetWidth("DATE", 10) + 10;
            float dayWidth = boldFont.GetWidth("DAY", 10) + 10;
            List<float> classWidths = new List<float>();

            foreach (var className in classNames)
            {
                string header = $"Class {className}";
                float width = boldFont.GetWidth(header, 10) + 10;
                classWidths.Add(width);
            }

            // Check content widths
            foreach (DataRow row in dataTable.Rows)
            {
                string date = row.Field<DateTime>("date").ToString("yyyy-MM-dd");
                string day = DateTime.Parse(date).ToString("dddd");
                string subject = row.Field<string>("subject");

                float currentDateWidth = font.GetWidth(date, 10) + 10;
                if (currentDateWidth > dateWidth)
                    dateWidth = currentDateWidth;

                float currentDayWidth = font.GetWidth(day, 10) + 10;
                if (currentDayWidth > dayWidth)
                    dayWidth = currentDayWidth;

                int classIndex = classNames.IndexOf(row.Field<string>("class"));
                if (classIndex >= 0)
                {
                    float currentSubjectWidth = font.GetWidth(subject, 10) + 10;
                    if (currentSubjectWidth > classWidths[classIndex])
                        classWidths[classIndex] = currentSubjectWidth;
                }
            }

            // Convert to relative widths (percentages)
            float totalWidth = dateWidth + dayWidth + classWidths.Sum();
            float[] relativeWidths = new float[2 + classNames.Count];
            relativeWidths[0] = (dateWidth / totalWidth) * 100;
            relativeWidths[1] = (dayWidth / totalWidth) * 100;

            for (int i = 0; i < classWidths.Count; i++)
            {
                relativeWidths[i + 2] = (classWidths[i] / totalWidth) * 100;
            }

            return relativeWidths;
        }

        // PrintDocument_PrintPage method with improved column width calculation
        private void PrintDocument_PrintPage(PrintPageEventArgs e, DataTable dataTable, string morningShiftTime, string afternoonShiftTime, string fridayTime, string assessmentTitle, string footerText)
        {
            e.PageSettings.Landscape = true;
            Font headerFont = new Font("Arial", 16, FontStyle.Bold);
            Font subHeaderFont = new Font("Arial", 12, FontStyle.Bold | FontStyle.Italic);
            Font contentFont = new Font("Arial", 10);
            Font boldContentFont = new Font("Arial", 10, FontStyle.Bold);
            Pen borderPen = new Pen(Color.Black, 1);

            float pageWidth = e.MarginBounds.Width;
            float yPos = e.MarginBounds.Top;
            float lineSpacing = 30;

            // Draw headers
            e.Graphics.DrawString("DATE SHEET", headerFont, Brushes.Black,
                e.MarginBounds.Left + (pageWidth - e.Graphics.MeasureString("DATE SHEET", headerFont).Width) / 2,
                yPos);
            yPos += lineSpacing;

            e.Graphics.DrawString(assessmentTitle, subHeaderFont, Brushes.Black,
        e.MarginBounds.Left + (pageWidth - e.Graphics.MeasureString(assessmentTitle, subHeaderFont).Width) / 2,
        yPos);
            yPos += lineSpacing;

            e.Graphics.DrawString($"MORNING SHIFT TIMING: {morningShiftTime}", subHeaderFont, Brushes.Black,
                e.MarginBounds.Left + (pageWidth - e.Graphics.MeasureString($"MORNING SHIFT TIMING: {morningShiftTime}", subHeaderFont).Width) / 2,
                yPos);
            yPos += lineSpacing;

            e.Graphics.DrawString($"AFTERNOON SHIFT TIMING: {afternoonShiftTime} (Friday {fridayTime})", subHeaderFont, Brushes.Black,
                e.MarginBounds.Left + (pageWidth - e.Graphics.MeasureString($"AFTERNOON SHIFT TIMING: {afternoonShiftTime} (Friday {fridayTime})", subHeaderFont).Width) / 2,
                yPos);
            yPos += lineSpacing * 2;

            // Get unique class names from the dataTable
            var classNames = dataTable.AsEnumerable()
                .Select(row => row.Field<string>("class"))
                .Distinct()
                .OrderBy(c => c)
                .ToList();
            int columnCount = 2 + classNames.Count;

            // Calculate column widths based on content
            float[] columnWidths = CalculatePrintColumnWidths(e, dataTable, classNames, contentFont, boldContentFont, pageWidth);

            // Draw table headers
            float currentX = e.MarginBounds.Left;
            string[] headers = new string[] { "DATE", "DAY" }.Concat(classNames.Select(c => $"Class {c}")).ToArray();

            for (int i = 0; i < columnCount; i++)
            {
                e.Graphics.DrawRectangle(borderPen, currentX, yPos, columnWidths[i], lineSpacing);
                e.Graphics.DrawString(headers[i], boldContentFont, Brushes.Black,
                    currentX + (columnWidths[i] - e.Graphics.MeasureString(headers[i], boldContentFont).Width) / 2,
                    yPos + 5);
                currentX += columnWidths[i];
            }
            yPos += lineSpacing;

            // Group data by date and map subjects to classes
            var dates = dataTable.AsEnumerable()
                .GroupBy(row => row.Field<DateTime>("date").ToString("yyyy-MM-dd"))
                .OrderBy(g => g.Key);

            Dictionary<string, Dictionary<string, string>> dateClassSubjects = new Dictionary<string, Dictionary<string, string>>();
            foreach (DataRow row in dataTable.Rows)
            {
                string date = row.Field<DateTime>("date").ToString("yyyy-MM-dd");
                string className = row.Field<string>("class");
                string subject = row.Field<string>("subject");
                if (!dateClassSubjects.ContainsKey(date))
                    dateClassSubjects[date] = new Dictionary<string, string>();
                dateClassSubjects[date][className] = subject;
            }

            // Draw table rows
            foreach (var dateGroup in dates)
            {
                string date = dateGroup.Key;
                string day = DateTime.Parse(date).ToString("dddd");
                currentX = e.MarginBounds.Left;

                // Date cell
                e.Graphics.DrawRectangle(borderPen, currentX, yPos, columnWidths[0], lineSpacing);
                e.Graphics.DrawString(date, contentFont, Brushes.Black,
                    currentX + (columnWidths[0] - e.Graphics.MeasureString(date, contentFont).Width) / 2,
                    yPos + 5);
                currentX += columnWidths[0];

                // Day cell
                e.Graphics.DrawRectangle(borderPen, currentX, yPos, columnWidths[1], lineSpacing);
                e.Graphics.DrawString(day, contentFont, Brushes.Black,
                    currentX + (columnWidths[1] - e.Graphics.MeasureString(day, contentFont).Width) / 2,
                    yPos + 5);
                currentX += columnWidths[1];

                // Class subject cells
                foreach (var className in classNames)
                {
                    string subject = dateClassSubjects.ContainsKey(date) && dateClassSubjects[date].ContainsKey(className)
                        ? dateClassSubjects[date][className]
                        : "-";

                    e.Graphics.DrawRectangle(borderPen, currentX, yPos, columnWidths[2 + classNames.IndexOf(className)], lineSpacing);
                    e.Graphics.DrawString(subject, contentFont, Brushes.Black,
                        currentX + (columnWidths[2 + classNames.IndexOf(className)] - e.Graphics.MeasureString(subject, contentFont).Width) / 2,
                        yPos + 5);
                    currentX += columnWidths[2 + classNames.IndexOf(className)];
                }
                yPos += lineSpacing;

                // Check for page break
                if (yPos + lineSpacing > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            // Footer
            e.Graphics.DrawString(footerText, contentFont, Brushes.Black,
        e.MarginBounds.Left + (pageWidth - e.Graphics.MeasureString(footerText, contentFont).Width) / 2,
        yPos + 10);
            e.HasMorePages = false;
        }

        // Helper method to calculate column widths for printing
        private float[] CalculatePrintColumnWidths(PrintPageEventArgs e, DataTable dataTable, List<string> classNames, Font contentFont, Font boldContentFont, float pageWidth)
        {
            // Initialize column widths with header widths
            float[] columnWidths = new float[2 + classNames.Count];
            columnWidths[0] = e.Graphics.MeasureString("DATE", boldContentFont).Width + 20;
            columnWidths[1] = e.Graphics.MeasureString("DAY", boldContentFont).Width + 20;

            for (int i = 0; i < classNames.Count; i++)
            {
                columnWidths[i + 2] = e.Graphics.MeasureString($"Class {classNames[i]}", boldContentFont).Width + 20;
            }

            // Check content widths
            foreach (DataRow row in dataTable.Rows)
            {
                string date = row.Field<DateTime>("date").ToString("yyyy-MM-dd");
                string day = DateTime.Parse(date).ToString("dddd");
                string subject = row.Field<string>("subject");

                float dateWidth = e.Graphics.MeasureString(date, contentFont).Width + 20;
                if (dateWidth > columnWidths[0])
                    columnWidths[0] = dateWidth;

                float dayWidth = e.Graphics.MeasureString(day, contentFont).Width + 20;
                if (dayWidth > columnWidths[1])
                    columnWidths[1] = dayWidth;

                int classIndex = classNames.IndexOf(row.Field<string>("class"));
                if (classIndex >= 0)
                {
                    float subjectWidth = e.Graphics.MeasureString(subject, contentFont).Width + 20;
                    if (subjectWidth > columnWidths[2 + classIndex])
                        columnWidths[2 + classIndex] = subjectWidth;
                }
            }

            // Ensure the total width doesn't exceed page width
            float totalWidth = columnWidths.Sum();
            if (totalWidth > pageWidth)
            {
                float scaleFactor = pageWidth / totalWidth;
                for (int i = 0; i < columnWidths.Length; i++)
                {
                    columnWidths[i] *= scaleFactor;
                }
            }

            return columnWidths;
        }
    }
}