using DevExpress.XtraGrid;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Drawing;

namespace SchoolManagementSystem.Students
{
    public partial class XtraAdmissionForm : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraAdmissionForm()
        {
            InitializeComponent();
            DrawWatermark = true;
            Watermark.Text = "WWW.TNSBAY.COM";
            //Watermark.Image = Image.FromFile("~\\Practice\\StudentManagementSGC\\SchoolManagementSystem\\Resources\\logo.png");
            Watermark.Font = new Font(Watermark.Font.FontFamily, 40);
            Watermark.ForeColor = Color.DodgerBlue;
            Watermark.TextTransparency = 150;
            Watermark.ShowBehind = false;
        }

        public void Names(string Name, string Fname, string Mobile, string parentMobile, string DOB)
        {

            var row = new XRTableRow();
            xrTableStudentName.Rows.RemoveAt(0);
            int Index = 0;
            int total = 35;
            foreach (char ch in Name)
            {
                var cell = new XRTableCell();
                cell.WidthF = 23.66F;
                cell.Text = ch.ToString().ToUpper();
                row.Cells.Add(cell);
                Index++;
            }
            for (int i = Index; i < total; i++)
            {
                var cell = new XRTableCell();
                cell.WidthF = 23.66F;
                cell.Text = "";
                row.Cells.Add(cell);
            }
            xrTableStudentName.Rows.Add(row);
            ////////////////////////////////
            var row1 = new XRTableRow();
            Index = 0;
            xrTableFather.Rows.RemoveAt(0);

            foreach (char ch in Fname)
            {
                var cell = new XRTableCell();
                cell.WidthF = 23.66F;
                cell.Text = ch.ToString().ToUpper();
                row1.Cells.Add(cell);
                Index++;
            }
            for (int i = Index; i < total; i++)
            {
                var cell = new XRTableCell();
                cell.WidthF = 23.66F;
                cell.Text = "";
                row1.Cells.Add(cell);
            }
            xrTableFather.Rows.Add(row1);
            ///////////////////////////////
            var row2 = new XRTableRow();
            Index = 0;
            xrTableMobile.Rows.RemoveAt(0);

            foreach (char ch in Mobile)
            {
                var cell = new XRTableCell();
                cell.WidthF = 23.66F;
                cell.Text = ch.ToString();
                row2.Cells.Add(cell);
                Index++;
            }
            xrTableMobile.Rows.Add(row2);
            ///////////////////////////////
            var row3 = new XRTableRow();
            Index = 0;
            xrTableParentMobile.Rows.RemoveAt(0);

            foreach (char ch in parentMobile)
            {
                var cell = new XRTableCell();
                cell.WidthF = 23.66F;
                cell.Text = ch.ToString();
                row3.Cells.Add(cell);
                Index++;
            }
            xrTableParentMobile.Rows.Add(row3);
            ///////////////////////////////
            var row4 = new XRTableRow();
            Index = 0;
            xrTableDOB.Rows.RemoveAt(0);

            foreach (char ch in DOB)
            {
                var cell = new XRTableCell();
                cell.WidthF = 23.66F;
                cell.Text = ch.ToString();
                row4.Cells.Add(cell);
                Index++;
            }
            xrTableDOB.Rows.Add(row4);
        }

        public void extraField(DataTable DT)
        {
            float x = 10;
            float y = 10;
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(DT.Rows[i]["Type"].ToString()))
                {
                    XRLabel lab1 = new XRLabel();

                    lab1.Text = DT.Rows[i][0].ToString();
                    lab1.AutoWidth = true;
                    lab1.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                    XRLabel lab = new XRLabel();
                    XRTable tab = new XRTable();
                    if (DT.Rows[i][1].ToString() == "True")
                    {
                        lab.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                        lab.Width = (int)DT.Rows[i][2];
                        lab.Font = new Font("Microsoft New Tai Lue", 10, FontStyle.Regular);
                        lab.Text = DT.Rows[i][3].ToString();
                    }
                    else
                    {
                        tab.Borders = DevExpress.XtraPrinting.BorderSide.All;
                        tab.Width = (int)DT.Rows[i][2] * 23;
                        tab.Font = new Font("Microsoft New Tai Lue", 10, FontStyle.Regular);

                    }
                    lab1.LocationF = new PointF(x, y);
                    x = lab1.LocationF.X + lab1.WidthF;
                    float fieldwidth = 0;
                    if (DT.Rows[i][1].ToString() == "True")
                    {
                        lab.LocationF = new PointF(x, y);
                        fieldwidth = x + lab.WidthF;
                    }
                    else
                    {
                        tab.LocationF = new PointF(x, y);
                        fieldwidth = x + tab.WidthF;
                    }
                    
                    //if (x + lab.WidthF<= 598.52)
                    if (fieldwidth <= xrPanelExtraField.Width)
                    {
                    }
                    else
                    {
                        y = y + lab.HeightF + 10;
                        x = 7.47F;
                        lab1.LocationF = new PointF(x, y);
                        x = lab1.LocationF.X + lab1.WidthF;
                        if (DT.Rows[i][1].ToString() == "True")
                            lab.LocationF = new PointF(x, y);
                        else
                            tab.LocationF = new PointF(x, y);
                    }

                    xrPanelExtraField.Controls.Add(lab1);
                    if (DT.Rows[i][1].ToString() == "True")
                        xrPanelExtraField.Controls.Add(lab);
                    else
                    {
                        xrPanelExtraField.Controls.Add(tab);
                        var row = new XRTableRow();
                        if (DT.Rows[i][3].ToString() == "")
                        {
                            for (int j = Index; j < (int)DT.Rows[i][2]; j++)
                            {
                                var cell = new XRTableCell();
                                cell.WidthF = 23.66F;
                                cell.Text = "  ";
                                row.Cells.Add(cell);
                            }
                        }
                        else
                        {
                            string value = DT.Rows[i][3].ToString();
                            foreach (char ch in value)
                            {
                                var cell = new XRTableCell();
                                cell.WidthF = 23.66F;
                                cell.Text = ch.ToString().ToUpper();
                                row.Cells.Add(cell);
                                //Index++;
                            }
                        }
                        tab.Rows.Add(row);
                    }
                    if (DT.Rows[i][1].ToString() == "True")
                        x = lab.LocationF.X + lab.WidthF;
                    else
                        x = tab.LocationF.X + tab.WidthF;
                }
            }

        }

    }
}
