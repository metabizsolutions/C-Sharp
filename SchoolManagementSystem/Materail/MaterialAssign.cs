using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SchoolManagementSystem.Materail
{
    public partial class MaterialAssign : UserControl
    {
        private static MaterialAssign _instance;

        public static MaterialAssign instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MaterialAssign();
                return _instance;
            }
        }
        CommonFunctions fun = new CommonFunctions();
        public MaterialAssign()
        {
            InitializeComponent();
            loadfunctions();
        }
        public void loadfunctions()
        {
            loadlist();
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
        void loadlist()
        {
            string query = "select category_id,title,(case when status=1 then 'Active' else 'Not Active' End)  as status from material_category";
            MaterialCatList.Properties.DataSource = fun.FetchDataTable(query);
            MaterialCatList.Properties.DisplayMember = "title";
            MaterialCatList.Properties.ValueMember = "category_id";

            query = "SELECT section_id as ID,concat(class.name,'>',section.name) as Section FROM section join class on(class.class_id=section.class_id);";
            SectionList.Properties.DataSource = fun.FetchDataTable(query);
            SectionList.Properties.DisplayMember = "Section";
            SectionList.Properties.ValueMember = "ID";
        }
        void loaddata()
        {
            string query = "SELECT  msa.assign_id,msa.material_id,msa.section_id,sec.name as Section,m.title,m.type,m.category_id,mc.title as Category,m.attachment,m.description FROM material_section_assign as msa" +
                            " join material as m on m.material_id = msa.material_id " +
                            " join material_category as mc on mc.category_id = m.category_id" +
                            " join section as sec on sec.section_id = msa.section_id";
            gridMaterialAssign.DataSource = fun.FetchDataTable(query);
            gridView1.Columns["material_id"].Visible = false;
            gridView1.Columns["section_id"].Visible = false;
            gridView1.Columns["category_id"].Visible = false;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                MessageBox.Show("Title is compulsory", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (SectionList.EditValue == null)
            {
                MessageBox.Show("Please select Section", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fun.loaderform(() =>
            {
                if (row == null)
                {
                    string query = "INSERT INTO `material`(`title`, `category_id`, `attachment`, `description`) VALUES " +
                    "('" + txtTitle.Text + "','" + MaterialCatList.EditValue + "','" + txtfilepath.Text + "','" + txtdiscription.Text + "')";
                    int materialid = fun.ExecuteInsert(query);
                    string[] sections = SectionList.EditValue.ToString().Split(',');
                    foreach (string sec in sections)
                    {
                        query = "INSERT INTO `material_section_assign`( `material_id`, `section_id`) VALUES " +
                    " ('" + materialid + "','" + sec + "')";
                        fun.ExecuteInsert(query);
                    }
                }
                else
                {
                    string query = "UPDATE material SET title='" + txtTitle.Text + "',category_id='" + MaterialCatList.EditValue + "',attachment='" + txtfilepath.Text + "',description='" + txtdiscription.Text + "' WHERE material_id = '" + row["material_id"] + "'";
                    fun.ExecuteQuery(query);
                    query = "UPDATE material_section_assign SET material_id='" + row["material_id"] + "',section_id='" + SectionList.EditValue + "' WHERE assign_id = '" + row["assign_id"] + "'";
                    fun.ExecuteQuery(query);
                    btnAdd.Enabled = fun.isAllow("Add", "materialassign");
                    MessageBox.Show("Data Updated Successfully", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            });
            txtdiscription.Text = "";
            txtfilepath.Text = "";
            txtTitle.Text = "";
            SectionList.EditValue = null;
            MaterialCatList.EditValue = null;
            loaddata();
        }
        DataRow row;
        private void btnEdit_Click(object sender, EventArgs e)
        {
            row = gridView1.GetFocusedDataRow();
            SectionList.EditValue = row["section_id"];
            MaterialCatList.EditValue = row["category_id"];
            txtTitle.Text = row["title"].ToString();
            txtfilepath.Text = row["attachment"].ToString();
            SectionList.EditValue = row["section_id"];
            txtdiscription.Text = row["description"].ToString();
            btnAdd.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (MessageBox.Show("Do you really want to delete named '" + dr["title"] + "'", "info", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                fun.loaderform(() =>
                {
                    string query = "DELETE FROM material_section_assign WHERE assign_id = '" + dr["assign_id"] + "'";
                    fun.ExecuteQuery(query);
                    loaddata();
                });
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
                        fun.loaderform(() =>
                        {
                            string apipath = fun.GetSettings("api_url")+ "/upload";
                            string school = fun.GetSettings("school_code");
                            string name = school + "_" + fn;
                            string dirpath = Directory.GetCurrentDirectory();
                            System.Net.WebClient Client = new System.Net.WebClient();
                            Client.Headers.Add("Content-Type", "binary/octet-stream");
                            byte[] resul = Client.UploadFile(apipath, "POST", file);
                            String uploadedpath = System.Text.Encoding.UTF8.GetString(resul, 0, resul.Length);
                            txtfilepath.Text = uploadedpath;
                        });
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
