using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem.Exam
{
    public partial class upload_manage_marks : Form
    {
        CommonFunctions fun = new CommonFunctions();
        int classID = 0;
        int examID = 0;
        int sectionID = 0;
        int subjectID = 0;
        public upload_manage_marks(string[] data)
        {
            InitializeComponent();
            fun.selectall_Controls(this);
            classID = Convert.ToInt32(data[0]);
            examID = Convert.ToInt32(data[1]);
            sectionID = Convert.ToInt32(data[2]);
            subjectID = Convert.ToInt32(data[3]);

            lbl_class.Text = data[4];
            lbl_section.Text = data[5];
            lbl_subject.Text = data[6];
            lbl_exam.Text = data[7];
        }
        string filename = "";
        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filename))
            {
                MessageBox.Show("Please select file","Error",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            fun.loaderform(() =>
            {
                String check_query = "select * from tbl_mark_subject where subject_id = '{0}' and exam_id = '{1}' ";
                check_query = String.Format(check_query, subjectID, examID);
                DataTable dt_excel = fun.FetchData_excel(filename, "SELECT * FROM [Sheet$]");
                foreach (DataRow dr in dt_excel.Rows)
                {
                    string query = "SELECT mark.* FROM mark " +
                            " JOIN student ON(student.student_id = mark.student_id)" +
                            " WHERE student.student_id = '" + dr["student_id"] + "' AND exam_id = '" + examID + "' AND student.class_id = '" + classID + "'" +
                            " AND student.section_id = '" + sectionID + "' AND subject_id = '" + subjectID + "' AND student.passout = 0; ";
                    DataTable dt = fun.FetchDataTable(query);
                    string markobtained = dr["Obtain Marks"].ToString();

                    //if (dr["Attnd"].ToString() == "A")//if (Convert.ToInt32(Attendance) == 1)
                    //  markobtained = "-1";
                    int total = 0;//fun.GetSubjectTotalMarks(subjectID, examID);
                    var totalSubject = (total == 0) ? 0 : total;

                    if (dt.Rows.Count <= 0)
                    {
                        var q = "INSERT into mark(student_id,subject_id,class_id,section_id,exam_id,mark_obtained,mark_total,sync,ondate) VALUES " +
                            " ('" + dr["student_id"] + "','" + subjectID + "','" + classID + "','" + sectionID + "','" + examID + "','" + markobtained + "','" + totalSubject + "','0','" + DateTime.Now.ToString("yyyy-MM-dd") + "');";
                        q += "UPDATE `tbl_mark_subject` SET submit_on='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where subject_id='" + subjectID + "' and exam_id='" + examID + "';";
                        fun.ExecuteQuery(q);
                    }
                    else if (dt.Rows.Count == 1)
                    {
                        var q = "UPDATE `mark` " +
                            " SET `student_id` = '" + dr["student_id"] + "',`subject_id` = '" + subjectID + "',`class_id` = '" + classID + "',`section_id` = '" + sectionID + "',`exam_id` = '" + examID + "',`mark_obtained` = '" + markobtained + "',`mark_total` = '" + totalSubject + "',`ondate` = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' WHERE `mark_id` = '" + dt.Rows[0]["mark_id"] + "'; ";
                        q += "UPDATE `tbl_mark_subject` SET submit_on='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where subject_id='" + subjectID + "' and exam_id='" + examID + "';";
                        fun.ExecuteQuery(q);
                    }
                    else if (dt.Rows.Count > 1)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i > 0)
                            {
                                query = "DELETE FROM mark WHERE mark_id = '" + dt.Rows[i]["mark_id"] + "'";
                                fun.ExecuteQuery(query);
                            }
                        }
                    }
                }

            });
            btn_get_excel_file.EditValue = null;
            filename = "";
            MessageBox.Show("Marks Uploaded Successfully", "Success Massage", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_get_excel_file_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog1.Title = "Open file";
                openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filename = openFileDialog1.FileName;
                    btn_get_excel_file.Text = filename;
                    btnUpload.Enabled = true;
                }
            }
        }
    }
}
