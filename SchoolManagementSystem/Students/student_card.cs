using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace SchoolManagementSystem.Students
{
    public partial class student_card : DevExpress.XtraReports.UI.XtraReport
    {
        public student_card()
        {
            InitializeComponent();
            //if (Login.ShopType == "Electronics")
            //{
            //  ProductName.WidthF = 97.29163F;
            //XrlblPrice.Visible = false;

            //}
            //else
            //{
                lbl_name.WidthF = 58.66661F;
                lbl_section.Visible = true;
           // }
        }

    }
}
