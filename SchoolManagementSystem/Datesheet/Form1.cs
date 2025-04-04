using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace SchoolManagementSystem.Datesheet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadClassNames();
            LoadSectionNames();
            LoadSubjectNames();
            LoadTeacherNames();
        }

        private void LoadClassNames()
        {
            string connectionString = "server=localhost;database=tnsbay_school;uid=root;pwd=;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT name FROM class";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["name"].ToString());
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void LoadSectionNames()
        {
            string connectionString = "server=localhost;database=tnsbay_school;uid=root;pwd=;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT name FROM section";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader["name"].ToString());
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void LoadSubjectNames()
        {
            string connectionString = "server=localhost;database=tnsbay_school;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT name FROM subject"; // Fetching subject names
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBox3.Items.Add(reader["name"].ToString());
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
                    string query = "SELECT name FROM teacher"; // Fetching teacher names
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

        // Updated button click event handler
        private void button1_Click(object sender, EventArgs e)
        {
            // Validate all inputs including the room from textBox1
            if (!ValidateInputs()) return;

            // Ensure DataGridView has columns
            InitializeDataGridViewColumns();

            // Add the new schedule entry with room from textBox1
            AddScheduleEntry();
        }

        // Updated validation method to include textBox1 (room)
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

            if (string.IsNullOrWhiteSpace(textBox1.Text)) // Validate room textbox
            {
                ShowValidationError("Please enter a Room number/name", textBox1);
                return false;
            }

            return true;
        }

        // Method to initialize DataGridView columns (updated with Room column)
        private void InitializeDataGridViewColumns()
        {
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add("Class", "Class");
                dataGridView1.Columns.Add("Section", "Section");
                dataGridView1.Columns.Add("Subject", "Subject");
                dataGridView1.Columns.Add("Teacher", "Teacher");
                dataGridView1.Columns.Add("Room", "Room");  // Room column from textBox1
                dataGridView1.Columns.Add("Date", "Date");

                // Optional formatting
                dataGridView1.Columns["Date"].DefaultCellStyle.Format = "yyyy-MM-dd";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        // Updated method to add schedule entry with room from textBox1
        private void AddScheduleEntry()
        {
            try
            {
                string room = textBox1.Text.Trim(); // Get room from textBox1

                dataGridView1.Rows.Add(
                    comboBox1.SelectedItem.ToString(),
                    comboBox2.SelectedItem.ToString(),
                    comboBox3.SelectedItem.ToString(),
                    comboBox4.SelectedItem.ToString(),
                    room,  // Use value from textBox1
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

        // Updated clear method to reset textBox1 (room)
        private void ClearInputs()
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            textBox1.Text = "";  // Clear room textbox
            dateTimePicker1.Value = DateTime.Now;
        }

        // Helper method to show validation errors
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
                        if (!row.IsNewRow) // Skip the empty new row
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

                                // Handle date conversion safely
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
            // Check if any rows are selected
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select one or more rows to delete.",
                              "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Confirm deletion with user
            DialogResult result = MessageBox.Show($"Are you sure you want to delete {dataGridView1.SelectedRows.Count} selected row(s)?",
                                                "Confirm Delete",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Delete rows from DataGridView (working backwards to avoid index issues)
                    for (int i = dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                    {
                        DataGridViewRow row = dataGridView1.SelectedRows[i];
                        if (!row.IsNewRow) // Skip the new row if present
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
            // Set landscape mode
            e.PageSettings.Landscape = true;

            // Define fonts
            Font headerFont = new Font("Arial", 16, FontStyle.Bold);
            Font subHeaderFont = new Font("Arial", 12, FontStyle.Bold | FontStyle.Italic);
            Font contentFont = new Font("Arial", 10);
            Pen borderPen = new Pen(Color.Black, 1); // Pen for cell borders

            // Margins and positions
            float pageWidth = e.MarginBounds.Width;
            float yPos = e.MarginBounds.Top;
            float lineSpacing = 30; // Space between lines

            // Calculate center positions for title and shift timings
            float titleX = e.MarginBounds.Left + (pageWidth - e.Graphics.MeasureString("Exam Date Sheet", headerFont).Width) / 2;
            float subHeaderX = e.MarginBounds.Left + (pageWidth - e.Graphics.MeasureString($"Morning Shift: {morningShiftTime}", subHeaderFont).Width) / 2;

            // Title (Bold & Center-Aligned)
            e.Graphics.DrawString("Exam Date Sheet", headerFont, Brushes.Black, titleX, yPos);
            yPos += lineSpacing;

            // Print Shift Timings (Bold-Italic & Center-Aligned)
            e.Graphics.DrawString($"Morning Shift: {morningShiftTime}", subHeaderFont, Brushes.Black, subHeaderX, yPos);
            yPos += lineSpacing;
            e.Graphics.DrawString($"Afternoon Shift: {afternoonShiftTime}", subHeaderFont, Brushes.Black, subHeaderX, yPos);
            yPos += lineSpacing;
            e.Graphics.DrawString($"Friday Time: {fridayTime}", subHeaderFont, Brushes.Black, subHeaderX, yPos);
            yPos += lineSpacing * 2;

            // Get column widths based on longest text
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

            // Print column headers
            float currentX = e.MarginBounds.Left;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                e.Graphics.DrawRectangle(borderPen, currentX, yPos, columnWidths[col.Index], lineSpacing);
                e.Graphics.DrawString(col.HeaderText, contentFont, Brushes.Black, currentX + 5, yPos + 5);
                currentX += columnWidths[col.Index];
            }
            yPos += lineSpacing;

            // Print table rows
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

                // If page limit is reached, create a new page
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

            // Get shift times from user
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
                    return; // Cancel printing if user closes dialog
                }
            }

            // Proceed with printing
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
    }
    }

