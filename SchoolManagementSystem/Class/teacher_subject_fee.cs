using DevExpress.XtraGrid;
using System;
using System.Data;
using System.Windows.Forms;

namespace SchoolManagementSystem.Class
{
    public partial class teacher_subject_fee : Form
    {
        CommonFunctions fun = new CommonFunctions();
        DataRow row;
        public teacher_subject_fee(DataRow dr)
        {
            InitializeComponent();
            fun.selectall_Controls(this);
            row = dr;
            lblteacherName.Text = dr["name"].ToString();
            loadsubjects();
            loadgrid(Convert.ToInt32(dr["SrNo"]));
        }
        public void loadsubjects()
        {
            string query = "SELECT ss.subject_id,sub.subject_code,sub.name,ss.subject_fee FROM section_subject as ss " +
                            " inner join subject_default as sub on sub.id=ss.subject_id " +
                            " join student as std on std.section_id = ss.section_id" +
                            " where std.student_id = '" + row["SrNo"] + "'";
            gridLookUpEdit_subject.Properties.DataSource = fun.FetchDataTable(query);
            gridLookUpEdit_subject.Properties.DisplayMember = "name";
            gridLookUpEdit_subject.Properties.ValueMember = "subject_id";
        }
        public void loadgrid(int id)
        {
            string query = "SELECT GROUP_CONCAT(ss.subject_id) AS subjects FROM section_subject AS ss " +
                " JOIN student AS STD ON std.`section_id` = ss.`section_id` WHERE std.`student_id` = '"+id+"'";
            object allsubjects = fun.Execute_Scaler_string(query);
            query = "SELECT fbst.`id`,std.name as student,tech.name as Teacher,sub.name as subject,fbst.amount,fbst.concession,(fbst.amount-fbst.concession) as Total FROM fee_by_subject_teacher as fbst " +
                            " join teacher as tech on tech.teacher_id = fbst.teacher_id  " +
                            " join subject_default as sub on sub.id = fbst.subject_id  " +
                            " join student as std on std.student_id = fbst.student_id " +
                            " where fbst.student_id = '" +id+"' and fbst.subject_id in ("+ allsubjects + ")";
            gridControl1.DataSource = fun.FetchDataTable(query);
            gridView2.Columns["id"].OptionsColumn.ReadOnly = true;
            gridView2.Columns["Teacher"].OptionsColumn.ReadOnly = true;
            gridView2.Columns["subject"].OptionsColumn.ReadOnly = true;
            gridView2.Columns["student"].OptionsColumn.ReadOnly = true;

            GridColumnSummaryItem item = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "amount", "{0}");
            GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "concession", "{0}");
            GridColumnSummaryItem item2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Total", "{0}");
            gridView2.Columns["amount"].Summary.Clear();
            gridView2.Columns["amount"].Summary.Add(item);
            gridView2.Columns["concession"].Summary.Clear();
            gridView2.Columns["concession"].Summary.Add(item1); 
            gridView2.Columns["Total"].Summary.Clear();
            gridView2.Columns["Total"].Summary.Add(item2);
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            if(gridLookUpEdit_teacher.EditValue == null || gridLookUpEdit_subject.EditValue==null || string.IsNullOrEmpty(txtamount.Text))
            {
                MessageBox.Show("value not set", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string query = "select * from fee_by_subject_teacher where class_id = '"+row["class_id"]+"' and section_id = '"+row["section_id"]+"' and subject_id = '" + gridLookUpEdit_subject.EditValue + "' and student_id='" + row["SrNo"] + "'";
            DataTable dt = fun.FetchDataTable(query);
            if (dt.Rows.Count <= 0)
            {
                query = "INSERT INTO `fee_by_subject_teacher`(class_id,section_id,subject_id,`student_id`, `teacher_id`, `amount`) VALUES ('" + row["class_id"] + "','" + row["section_id"] + "','" + gridLookUpEdit_subject.EditValue + "','" + row["SrNo"] + "','"+ gridLookUpEdit_teacher.EditValue + "',(select subject_fee from section_subject where class_id = '" + row["class_id"] + "' and section_id = '" + row["section_id"] + "' and subject_id = '" + gridLookUpEdit_subject.EditValue + "'))";
                fun.Execute_Query(query);
                loadgrid(Convert.ToInt32(row["SrNo"]));
            }
            else
                MessageBox.Show("already have value agaist '"+ gridLookUpEdit_subject.EditValue + "'", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void gridView2_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow uprow = gridView2.GetFocusedDataRow();
            string query = "UPDATE fee_by_subject_teacher SET amount='" + uprow["amount"] + "',concession='" + uprow["concession"] + "' WHERE id ='" + uprow["id"] + "'";
            fun.Execute_Query(query);
        }

        private void gridLookUpEdit_subject_EditValueChanged(object sender, EventArgs e)
        {
            string query = "SELECT subject_fee FROM section_subject where subject_id = '" + gridLookUpEdit_subject.EditValue + "'";
            object val = fun.Execute_Scaler_string(query);
            txtamount.Text = val == null ? "" : val.ToString();
            query = "SELECT `teacher_id`,name FROM `teacher` WHERE `subject_code` ='" + gridLookUpEdit_subject.Text + "'";
                gridLookUpEdit_teacher.Properties.DataSource = fun.FetchDataTable(query);
                gridLookUpEdit_teacher.Properties.DisplayMember = "name";
                gridLookUpEdit_teacher.Properties.ValueMember = "teacher_id";
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            DataRow uprow = gridView2.GetFocusedDataRow();
            string query = "delete from fee_by_subject_teacher WHERE id ='" + uprow["id"] + "'";
            fun.Execute_Query(query);
            loadgrid(Convert.ToInt32(row["SrNo"]));
        }
    }
}
