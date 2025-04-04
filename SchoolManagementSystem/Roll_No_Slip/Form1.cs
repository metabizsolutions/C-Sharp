using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using SchoolManagementSystem.Exam;

namespace SchoolManagementSystem.Roll_No_Slip
{
    public partial class Form1 : Form
    {
        private StudentDetails studentDetails;
        private List<ExamSubject> examSubjects;

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Text = "Roll No Slip Generator";
        }

        public void SetStudentDetails(
            string name, string fatherName, string rollNo,
            string group, string gender, string className,
            string section, string session)
        {
            studentDetails = new StudentDetails
            {
                Name = name,
                FatherName = fatherName,
                RollNo = rollNo,
                Group = group,
                Gender = gender,
                Class = className,
                Section = section,
                Session = session
            };
        }

        public void SetExamSubjects(List<ExamSubject> subjects)
        {
            examSubjects = subjects ?? new List<ExamSubject>();
            GenerateRollNoSlip();
        }

        private void GenerateRollNoSlip()
        {
            mainContainer.SuspendLayout();
            mainContainer.Controls.Clear();

            int padding = 20;
            int contentWidth = Math.Max(600, this.ClientSize.Width - 2 * padding);
            int currentY = padding;

            // Header Panel
            Panel headerPanel = new Panel
            {
                BackColor = Color.LightBlue,
                Size = new Size(contentWidth, 120),
                Location = new Point(padding, currentY)
            };
            mainContainer.Controls.Add(headerPanel);
            currentY += headerPanel.Height + padding;

            // School Information (centered)
            AddLabel(headerPanel, "Sunrise Model Public School & College",
                (headerPanel.Width - 300) / 2, 20, 14, true);
            AddLabel(headerPanel, "KHUZUAB (B  ALOGHEI5740)",
                (headerPanel.Width - 200) / 2, 50, 10);
            AddLabel(headerPanel, "Phone: 084612999 Mob: 03337881630/03337972999",
                (headerPanel.Width - 300) / 2, 80, 9);

            // Title
            AddLabel(mainContainer, $"DateSheet / Roll No.Slip of session {studentDetails.Session}",
                (contentWidth - 300) / 2, currentY, 12, true);
            currentY += 30;

            // Student Info Panel
            Panel infoPanel = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(contentWidth, 180),
                Location = new Point(padding, currentY)
            };
            mainContainer.Controls.Add(infoPanel);
            currentY += infoPanel.Height + padding;

            // Student Information
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
                AddLabelPair(infoPanel, labels[i], values[i], 20, 20 + (i * 20));
            }

            // Exam Schedule
            AddLabel(mainContainer, "Exam Schedule", padding, currentY, 12, true);
            currentY += 30;

            // Dynamic grid height
            int gridHeight = Math.Min(400, Math.Max(100, examSubjects.Count * 30));
            DataGridView grid = CreateExamScheduleGrid(padding, currentY, contentWidth, gridHeight);
            mainContainer.Controls.Add(grid);
            currentY += grid.Height + padding;

            // Notes Section
            AddLabel(mainContainer, "Notes:", padding, currentY, 10, true);
            currentY += 20;

            rtbNotes.Location = new Point(padding, currentY);
            rtbNotes.Size = new Size(contentWidth, 120);
            rtbNotes.Text = GetNotesText();
            mainContainer.Controls.Add(rtbNotes);
            currentY += rtbNotes.Height + padding;

            // Signature and Date
            AddLabel(mainContainer, "Principal's Signature", padding, currentY, 10, true);
            AddLabel(mainContainer, $"Issued Date & time: {DateTime.Now:yyyy/MM/d  d HH:mm:ss}",
                contentWidth - 250, currentY, 9);
            currentY += 30;

            // Action Buttons (centered at bottom)
            btnPrint.Location = new Point(
                (contentWidth - btnPrint.Width - btnResetNotes.Width - 20) / 2,
                currentY);
            btnResetNotes.Location = new Point(
                btnPrint.Right + 20,
                currentY);

            mainContainer.Controls.Add(btnPrint);
            mainContainer.Controls.Add(btnResetNotes);

            mainContainer.ResumeLayout(true);
        }

        private DataGridView CreateExamScheduleGrid(int x, int y, int width, int height)
        {
            DataGridView grid = new DataGridView
            {
                Size = new Size(width, height),
                Location = new Point(x, y),
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            grid.Columns.Add("Sr#", "Sr#");
            grid.Columns.Add("Date", "Date");
            grid.Columns.Add("Subject", "Subject");
            grid.Columns.Add("Start Time", "Start Time");
            grid.Columns.Add("End Time", "End Time");

            for (int i = 0; i < examSubjects.Count; i++)
            {
                var subject = examSubjects[i];
                grid.Rows.Add(
                    (i + 1).ToString(),
                    subject.Date.ToString("dd/MM/yyyy"),
                    subject.SubjectName,
                    subject.StartTime.ToString("hh  :mm tt"),
                    subject.EndTime.ToString("hh  :mm tt")
                );
            }
            return grid;
        }

        private void AddLabel(Control parent, string text, int x, int y, float fontSize = 10, bool bold = false)
        {
            Label label = new Label
            {
                Text = text,
                Font = new Font("Arial", fontSize, bold ? FontStyle.Bold : FontStyle.Regular),
                AutoSize = true,
                Location = new Point(x, y)
            };
            parent.Controls.Add(label);
        }

        private void AddLabelPair(Control parent, string labelText, string valueText, int x, int y)
        {
            AddLabel(parent, labelText, x, y, 10, true);
            AddLabel(parent, valueText, x + 130, y, 10);
        }

        private string GetNotesText()
        {
            return @"1) Those students will not be allowed in the exam whom have not paid remaining dues.
2) Late course will not be permitted to sit in the examination according to the given syllabus.
3) Parents are required to get preparation of their children according to the other and any other
4) SSMPs reserves the right to modify the date sheet of annual exam due to weather and any other certain  situation  .";
        }

        private void btnResetNotes_Click(object sender, EventArgs e)
        {
            rtbNotes.Text = GetNotesText();
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            // Store original button visibility
            bool printButtonVisible = btnPrint.Visible;
            bool resetButtonVisible = btnResetNotes.Visible;

            try
            {
                // Hide buttons before printing
                btnPrint.Visible = false;
                btnResetNotes.Visible = false;

                // Force the form to update the layout
                this.Update();
                Application.DoEvents();

                using (PrintDocument pd = new PrintDocument())
                {
                    // Set landscape orientation
                    pd.DefaultPageSettings.Landscape = true;

                    // Calculate the scale factor to fit the content
                    float scaleFactor = 0.8f; // Adjust this value as needed

                    pd.PrintPage += (s, ev) =>
                    {
                        // Create a bitmap of just the mainContainer (excluding buttons)
                        Bitmap bitmap = new Bitmap(mainContainer.Width, mainContainer.Height);
                        mainContainer.DrawToBitmap(bitmap, new Rectangle(0, 0, mainContainer.Width, mainContainer.Height));

                        // Calculate scaled dimensions to fit the page
                        float scaledWidth = bitmap.Width * scaleFactor;
                        float scaledHeight = bitmap.Height * scaleFactor;

                        // Center the image on the page
                        float x = (ev.PageBounds.Width - scaledWidth) / 2;
                        float y = (ev.PageBounds.Height - scaledHeight) / 2;

                        // Draw the bitmap with scaling
                        ev.Graphics.DrawImage(bitmap, x, y, scaledWidth, scaledHeight);
                        bitmap.Dispose();
                    };

                    using (PrintDialog printDialog = new PrintDialog { Document = pd })
                    {
                        if (printDialog.ShowDialog() == DialogResult.OK)
                        {
                            pd.Print();
                        }
                    }
                }
            }
            finally
            {
                // Restore button visibility
                btnPrint.Visible = printButtonVisible;
                btnResetNotes.Visible = resetButtonVisible;
            }
        }
    }
}