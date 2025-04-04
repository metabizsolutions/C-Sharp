using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolManagementSystem.Exam
{
    public partial class ExamSubjectsDialog : Form
    {
        public List<ExamSubject> ExamSubjects { get; private set; } = new List<ExamSubject>();

        private TextBox txtSubject;
        private DateTimePicker dtpDate;
        private DateTimePicker dtpStartTime;
        private DateTimePicker dtpEndTime;

        public ExamSubjectsDialog()
        {
            InitializeComponent();
            SetupControls();
        }

        private void SetupControls()
        {
            this.Text = "Add Exam Subjects";
            this.Size = new Size(450, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Subject Controls
            txtSubject = new TextBox { Location = new Point(100, 20), Width = 200 };
            this.Controls.Add(new Label { Text = "Subject:", Location = new Point(20, 20), AutoSize = true });
            this.Controls.Add(txtSubject);

            // Date Controls
            dtpDate = new DateTimePicker { Location = new Point(100, 50), Width = 200, Format = DateTimePickerFormat.Short };
            this.Controls.Add(new Label { Text = "Date:", Location = new Point(20, 50), AutoSize = true });
            this.Controls.Add(dtpDate);

            // Time Controls
            dtpStartTime = new DateTimePicker
            {
                Location = new Point(100, 80),
                Width = 200,
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true
            };
            this.Controls.Add(new Label { Text = "Start Time:", Location = new Point(20, 80), AutoSize = true });
            this.Controls.Add(dtpStartTime);

            dtpEndTime = new DateTimePicker
            {
                Location = new Point(100, 110),
                Width = 200,
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true
            };
            this.Controls.Add(new Label { Text = "End Time:", Location = new Point(20, 110), AutoSize = true });
            this.Controls.Add(dtpEndTime);

            // Buttons
            Button btnAdd = new Button { Text = "Add Subject", Location = new Point(20, 150), Width = 100 };
            btnAdd.Click += (s, e) => AddSubject();
            this.Controls.Add(btnAdd);

            Button btnOK = new Button { Text = "OK", Location = new Point(130, 150), Width = 100, DialogResult = DialogResult.OK };
            this.Controls.Add(btnOK);

            Button btnCancel = new Button { Text = "Cancel", Location = new Point(240, 150), Width = 100, DialogResult = DialogResult.Cancel };
            this.Controls.Add(btnCancel);
        }

        private void AddSubject()
        {
            if (string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                MessageBox.Show("Please enter a subject name", "Error");
                return;
            }

            ExamSubjects.Add(new ExamSubject
            {
                SubjectName = txtSubject.Text,
                Date = dtpDate.Value,
                StartTime = dtpStartTime.Value,
                EndTime = dtpEndTime.Value
            });

            txtSubject.Clear();
            MessageBox.Show("Subject added successfully!", "Success");
        }
    }

    public class ExamSubject
    {
        public string SubjectName { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}