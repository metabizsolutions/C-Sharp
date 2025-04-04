using System;
using System.Data;
using System.Windows.Forms;

namespace SchoolManagementSystem.Materail
{
    public partial class MaterialCategory : UserControl
    {
        private static MaterialCategory _instance;

        public static MaterialCategory instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MaterialCategory();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        public MaterialCategory()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            loaddata();
        }
        public void permissions_btns(bool add, bool Edit, bool Delete)
        {
            if (add)
                btnAdd.Enabled = true;
            else
                btnAdd.Enabled = false;
            if (Edit)
                btnEdit.Enabled = true;
            else
                btnEdit.Enabled = false;
            if (Delete)
                btnDelete.Enabled = true;
            else
                btnDelete.Enabled = false;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                MessageBox.Show("Title is Compulsory", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (row == null)
            {
                string query = "INSERT INTO `material_category`(`title`, `status`) VALUES ('" + txtTitle.Text + "','" + status + "')";
                fun.ExecuteQuery(query);

            }
            else
            {

                string query = "UPDATE `material_category` SET title='" + txtTitle.Text + "',status='" + status + "' WHERE category_id = '" + row["category_id"] + "'";
                fun.ExecuteQuery(query);
                btnAdd.Enabled = fun.isAllow("Add", "materialcategory");
                MessageBox.Show("Data Updated Successfully", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            row = null;
            txtTitle.Text = "";
            toggleStatus.IsOn = true;
            status = 1;
            loaddata();
        }
        void loaddata()
        {
            string query = "select category_id,title,(case when status=1 then 'Active' else 'Not Active' End)  as status from material_category";
            gridMaterialCategory.DataSource = fun.FetchDataTable(query);
        }

        int status = 1;
        private void toggleStatus_Toggled(object sender, EventArgs e)
        {
            status = toggleStatus.IsOn == true ? 1 : 0;
        }
        DataRow row;
        private void btnEdit_Click(object sender, EventArgs e)
        {
            row = gridView1.GetFocusedDataRow();
            txtTitle.Text = row["title"].ToString();
            toggleStatus.IsOn = row["status"].ToString() == "Active" ? true : false;
            status = toggleStatus.IsOn == true ? 1 : 0;
            btnAdd.Enabled = true;
        }
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (MessageBox.Show("Do you really want to delete this Category named '" + dr["title"] + "'", "info", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                string query = "DELETE FROM material_category WHERE category_id = '" + dr["category_id"] + "'";
                fun.ExecuteQuery(query);
                loaddata();
            }
        }
    }
}
