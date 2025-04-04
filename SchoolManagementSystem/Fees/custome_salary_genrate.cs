using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid;

namespace SchoolManagementSystem.Fees
{
    public partial class custome_salary_genrate : UserControl
    {

        private static custome_salary_genrate _instance;

        public static custome_salary_genrate instance
        {
            get
            {
                if (_instance == null)
                    _instance = new custome_salary_genrate();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        public custome_salary_genrate()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            txt_extra_fee.EditValue = fun.GetSettings("CS_extra_fee");
            txt_institute_percent.EditValue = fun.GetSettings("CS_institute_percent");
            txt_principle_percent.EditValue = fun.GetSettings("CS_principle_percent");
            txt_total_teachers.EditValue = fun.GetSettings("CS_total_teacher");
            load_custom_salary(dtp_date.Value.ToString("MMMM-yyyy"), txt_extra_fee.EditValue, txt_institute_percent.EditValue, txt_principle_percent.EditValue, txt_total_teachers.EditValue);
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
        }

        void load_custom_salary(string date,object extra_fee,object institue_percent,object principle_percent,object total_teachers)
        {
            if(string.IsNullOrEmpty(extra_fee.ToString()) || extra_fee.ToString() == "0" || string.IsNullOrEmpty(institue_percent.ToString()) || institue_percent.ToString() == "0" || string.IsNullOrEmpty(principle_percent.ToString()) || principle_percent.ToString() == "0" || string.IsNullOrEmpty(total_teachers.ToString()) || total_teachers.ToString() == "0")
            {
                return;
            }
            string query = "SELECT *,((total_income-`"+ institue_percent + "%`)-`"+ principle_percent + "%`) AS Remaining_amount,(((total_income-`"+ institue_percent + "%`)-`"+ principle_percent + "%`)/"+total_teachers+") AS teacher_salary FROM "+
                            " ( "+
                               " SELECT section, addmission_fee, ((amount_paid - addmission_fee) - extra_fee) AS tution_fee, extra_fee, (amount_paid - addmission_fee) AS total_income, "+
                               " (amount_paid - addmission_fee) * "+institue_percent+" / 100 AS `"+ institue_percent + "%`, (amount_paid - addmission_fee) * "+ principle_percent + " / 100 AS `"+ principle_percent + "%` " +
                                 " FROM( "+
                                   " SELECT sec.`name` AS section, "+
                                   " (SELECT "+
                                   " (CASE SUBSTRING_INDEX(SUBSTRING_INDEX(SUBSTRING_INDEX(v.`description`, ';', 2), ';', -1), ':', 1) WHEN 'Admission Fee'"+
                                   " THEN SUBSTRING_INDEX(SUBSTRING_INDEX(SUBSTRING_INDEX(v.`description`, ';', 2), ';', -1), ':', -1) ELSE '0' END) "+
                                   " FROM invoice AS v WHERE DATE_FORMAT(v.`date`, '%M-%Y') = DATE_FORMAT(inv.`date`, '%M-%Y') AND v.`student_id` = inv.`student_id`) AS addmission_fee, "+
                                   " SUM(inv.`amount_paid`) AS `amount_paid`, SUM("+ extra_fee + ") AS extra_fee "+
                                   " FROM invoice AS inv  INNER JOIN section AS sec ON sec.`section_id` = inv.`section_id` "+
                                   " WHERE 1 = 1 AND DATE_FORMAT(inv.`date`, '%M-%Y') = '" + date + "' AND inv.amount_paid > 0 GROUP BY inv.`section_id` " +
	                             " ) AS tb "+
                            ") AS final_tb";
            DataTable dt = fun.FetchDataTable(query);
            gridControl1.DataSource = dt;
            grid_cutomization();
        }
        public void grid_cutomization()
        {

            #region column properites from database
            
            ColumnView view = (ColumnView)gridControl1.FocusedView;
            view.ClearColumnsFilter();
            for (int i = 0; i < view.Columns.Count; i++)
            {
                string name = view.Columns[i].FieldName;
                
                if (name != "section")
                {
                    gridView1.Columns[name].Summary.Clear();
                    gridView1.Columns[name].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, name, "{0}"));
                }
            }
            #endregion column properties
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            fun.update_setting(txt_extra_fee.EditValue.ToString(), "CS_extra_fee");
            fun.update_setting(txt_institute_percent.EditValue.ToString(), "CS_institute_percent");
            fun.update_setting(txt_principle_percent.EditValue.ToString(), "CS_principle_percent");
            fun.update_setting(txt_total_teachers.EditValue.ToString(), "CS_total_teacher");
            load_custom_salary(dtp_date.Value.ToString("MMMM-yyyy"), txt_extra_fee.EditValue, txt_institute_percent.EditValue, txt_principle_percent.EditValue, txt_total_teachers.EditValue);
        }
    }
}
