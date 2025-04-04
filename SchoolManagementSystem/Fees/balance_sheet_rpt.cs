using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid;
using System.Data;

namespace SchoolManagementSystem.Fees
{
    public partial class balance_sheet_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public balance_sheet_rpt(DataTable incdt,DataTable expdt)
        {
            InitializeComponent();
            int total = 0;
            for (int i = 0; i <= incdt.Rows.Count; i++)
            {
                var row1 = new XRTableRow();
                for (int j = 0; j < incdt.Columns.Count; j++)
                {
                        var cell1 = new XRTableCell();
                    if (i == incdt.Rows.Count)
                    {
                        if (j == 1) cell1.Text = "Total";
                        if (j == 2)
                            cell1.Text = total.ToString();
                    }
                    else
                    {
                        cell1.Text = incdt.Rows[i][j].ToString();
                        if (j == 2)
                            total = total + Convert.ToInt32(incdt.Rows[i][j]);
                    }
                        row1.Cells.Add(cell1);
                }
                xrTableincome.Rows.Add(row1);
            }
            total = 0;
            bool expcount = true;
            for (int i = 0; i <= expdt.Rows.Count; i++)
            {
                var row1 = new XRTableRow();
                if (i== expdt.Rows.Count)
                    expcount = false;
                else if(expdt.Rows[i]["Title"].ToString() == "Total Expense")
                    expcount = false;
                for (int j = 0; j < expdt.Columns.Count; j++)
                {
                        var cell1 = new XRTableCell();
                    if (i == expdt.Rows.Count)
                    {
                        if (j == 1) cell1.Text = "Total";
                        if (j == 2)
                            cell1.Text = total.ToString();
                    }
                    else
                    {
                        cell1.Text = expdt.Rows[i][j].ToString();
                        if (j == 2 && expcount)
                            total = total + Convert.ToInt32(expdt.Rows[i][j]);
                        
                    }
                    row1.Cells.Add(cell1);
                }
                xrtable_expense.Rows.Add(row1);
                expcount = true;
            }
        }

    }
}
