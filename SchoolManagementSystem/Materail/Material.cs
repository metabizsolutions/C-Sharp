using System;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace SchoolManagementSystem.Materail
{
    public partial class Material : UserControl
    {
        private static Material _instance;

        public static Material instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Material();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        public Material()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            loaddata();
            loadmateriallist();
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
        void loadmateriallist()
        {
            string query = "select category_id,title,(case when status=1 then 'Active' else 'Not Active' End)  as status from material_category";
            MaterialCatList.Properties.DataSource = fun.FetchDataTable(query);
            MaterialCatList.Properties.DisplayMember = "title";
            MaterialCatList.Properties.ValueMember = "category_id";
        }
        void loaddata()
        {
            string query = "SELECT  material_id,material.title,type,material.category_id, mc.title as Category,attachment,description FROM `material` " +
                            " join material_category as mc on mc.category_id = material.category_id";
            gridMaterial.DataSource = fun.FetchDataTable(query);
            gridView1.Columns["category_id"].Visible = false;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                MessageBox.Show("Title is compulsory", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(combType.Text))
            {
                MessageBox.Show("Please select file Type", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtfilepath.Text))
            {
                MessageBox.Show("Please Select file", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MaterialCatList.EditValue == null)
            {
                MessageBox.Show("Please select category", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (row == null)
            {
                string query = "INSERT INTO `material`(`title`, `type`, `category_id`, `attachment`, `description`) VALUES " +
                "('" + txtTitle.Text + "','" + combType.Text + "','" + MaterialCatList.EditValue + "','" + txtfilepath.Text + "','" + txtdiscription.Text + "')";
                fun.ExecuteQuery(query);
            }
            else
            {
                string query = "UPDATE material SET title='" + txtTitle.Text + "',type='" + combType.Text + "',category_id='" + MaterialCatList.EditValue + "',attachment='" + txtfilepath.Text + "',description='" + txtdiscription.Text + "' WHERE material_id = '" + row["material_id"] + "'";
                fun.ExecuteQuery(query);
                btnAdd.Enabled = fun.isAllow("Add", "materials");
                MessageBox.Show("Data Updated Successfully", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            loaddata();
            empty();
        }
        void empty()
        {
            txtTitle.Text = "";
            combType.Text = "";
            MaterialCatList.EditValue = null;
            txtfilepath.Text = "";
            txtdiscription.Text = "";
            row = null;
        }
        DataRow row;
        private void btnEdit_Click(object sender, EventArgs e)
        {
            row = gridView1.GetFocusedDataRow();
            txtTitle.Text = row["title"].ToString();
            combType.Text = row["type"].ToString();
            MaterialCatList.EditValue = row["category_id"];
            txtfilepath.Text = row["attachment"].ToString();
            txtdiscription.Text = row["description"].ToString();
            btnAdd.Enabled = true;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (MessageBox.Show("Do you really want to delete named '" + dr["title"] + "'", "info", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                string query = "DELETE FROM material WHERE material_id = '" + dr["material_id"] + "'";
                fun.ExecuteQuery(query);
                loaddata();
            }
        }

        private void BtnBBrowse_Click(object sender, EventArgs e)
        {
            if (fun.CheckForInternetConnection())
            {
                DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
                if (result == DialogResult.OK) // Test result.
                {
                    string file = openFileDialog1.FileName;

                    string[] f = file.Split('\\');
                    // to get the only file name

                    string fn = f[(f.Length) - 1];
                    double length = new FileInfo(file).Length;
                    length = (length / 1024f) / 1024f;

                    string fileExt = Path.GetExtension(file);

                    if (length >= 700)
                    {
                        MessageBox.Show("file should be smaller then 700MB", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    try
                    {
                        string apipath = fun.GetSettings("api_url");
                        string school = fun.GetSettings("school_code");
                        string name = school + "_" + fn;
                        string dirpath = Directory.GetCurrentDirectory();
                        System.Net.WebClient Client = new System.Net.WebClient();
                        Client.Headers.Add("Content-Type", "binary/octet-stream");
                        byte[] resul = Client.UploadFile(apipath, "POST", file);
                        String uploadedpath = System.Text.Encoding.UTF8.GetString(resul, 0, resul.Length);
                        txtfilepath.Text = uploadedpath;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            else
                MessageBox.Show("Internet Connection is not available", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
