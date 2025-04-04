using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System.Linq;
using DevExpress.XtraBars.Navigation;
using System.Collections.Generic;

namespace SchoolManagementSystem.Admin
{
    public partial class Permissions : XtraForm
    {
        CommonFunctions fun = new CommonFunctions();
        CheckEdit[] boxes;
        int totalCheckBoxes = 0;
        bool isclick = false;
        public Permissions()
        {
            InitializeComponent();
            fun.selectall_Controls(this);
            loadgridRoleSettings();
            allchackboxes();
        }
        void allchackboxes()
        {
            var c = fun.GetAllControls(this.AP_panel, typeof(CheckEdit));
            totalCheckBoxes = c.Count();
            boxes = new CheckEdit[c.Count()];
            int index = 0;
            foreach (CheckEdit Chb in c)
            {
                boxes[index++] = Chb;
            }
        }
        void loadgridRoleSettings()
        {
            string query = "Select * from tbl_permission";
            DataTable dt = fun.FetchDataTable(query);
            CB_roles.DataSource = dt;
            CB_roles.DisplayMember = "title";
            CB_roles.ValueMember = "permission_id";
        }
        void mainchb(CheckEdit main_ce, string name, string text, bool val)
        {
            main_ce.Text = text;
            main_ce.Name = name;
            main_ce.CheckedChanged += Main_ce_CheckedChanged; ;
            main_ce.ForeColor = ColorTranslator.FromHtml("192,0,0");
            main_ce.Font = new Font("Tahoma", 10, FontStyle.Bold);
            main_ce.Width = 150;
            main_ce.Checked = val; //Convert.ToBoolean(rw_main["value"]);
        }

        private void Main_ce_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit ch = (CheckEdit)sender;
            string query = "select * from tbl_role where `key` ='" + ch.Name + "' and not subkey is null  GROUP BY `key`,`subkey`,`subsubkey`";
            DataTable dt = fun.FetchDataTable(query);

            foreach (DataRow dr in dt.Rows)
            {
                CheckEdit cb = boxes.FirstOrDefault(tt => tt.Name == dr["subkey"].ToString());
                if (cb != null)
                    cb.Checked = ch.Checked;
            }
        }
        void single_mainchb(CheckEdit main_ce, string name, string text, bool val)
        {
            main_ce.Text = text;
            main_ce.Name = name;
            main_ce.CheckedChanged += main_single_chack_CheckedChanged;
            main_ce.ForeColor = ColorTranslator.FromHtml("192,0,0");
            main_ce.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
            main_ce.Width = 150;
            main_ce.Checked = val; //Convert.ToBoolean(rw_main["value"]);
        }
        private void main_single_chack_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit ch = (CheckEdit)sender;
            string query = "select * from tbl_role where `key` ='" + ch.Name + "' and `subkey` is null  GROUP BY `key`,`subkey`,`subsubkey`";
            DataTable dt = fun.FetchDataTable(query);

            foreach (DataRow dr in dt.Rows)
            {
                CheckEdit cb_a = boxes.FirstOrDefault(tt => tt.Name == "add_" + ch.Name);
                if (cb_a != null)
                    cb_a.Checked = ch.Checked;
                CheckEdit cb_e = boxes.FirstOrDefault(tt => tt.Name == "edit_" + ch.Name);
                if (cb_e != null)
                    cb_e.Checked = ch.Checked;
                CheckEdit cb_d = boxes.FirstOrDefault(tt => tt.Name == "delete_" + ch.Name);
                if (cb_d != null)
                    cb_d.Checked = ch.Checked;
            }
        }
        void subchb(CheckEdit sub_main_ce, string name, string text, bool val)
        {
            sub_main_ce.Text = text;
            sub_main_ce.Name = name;
            sub_main_ce.CheckedChanged += subchb_chack_CheckedChanged;
            sub_main_ce.ForeColor = ColorTranslator.FromHtml("32,31,53");
            sub_main_ce.Font = new Font("Microsoft Sans Serif", 8.25f);
            sub_main_ce.Width = 146;
            sub_main_ce.Checked = val;//Convert.ToBoolean(rw_sub["subvalue"]);
        }
        private void subchb_chack_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit ch = (CheckEdit)sender;
            string query = "select * from tbl_role where `subkey` ='" + ch.Name + "' and subsubkey is null  GROUP BY `key`,`subkey`,`subsubkey`";
            DataTable dt = fun.FetchDataTable(query);

            foreach (DataRow dr in dt.Rows)
            {
                CheckEdit cb_a = boxes.FirstOrDefault(tt => tt.Name == "add_" + ch.Name);
                if (cb_a != null)
                    cb_a.Checked = ch.Checked;
                CheckEdit cb_e = boxes.FirstOrDefault(tt => tt.Name == "edit_" + ch.Name);
                if (cb_e != null)
                    cb_e.Checked = ch.Checked;
                CheckEdit cb_d = boxes.FirstOrDefault(tt => tt.Name == "delete_" + ch.Name);
                if (cb_d != null)
                    cb_d.Checked = ch.Checked;
            }
        }
        void subsubchb(CheckEdit sub_main_ce, string name, string text, bool val)
        {
            sub_main_ce.Text = text;
            sub_main_ce.Name = name;
            sub_main_ce.CheckedChanged += subsubchb_chack_CheckedChanged;
            sub_main_ce.ForeColor = ColorTranslator.FromHtml("32,31,53");
            sub_main_ce.Font = new Font("Microsoft Sans Serif", 8.25f);
            sub_main_ce.Width = 146;
            sub_main_ce.Checked = val;//Convert.ToBoolean(rw_sub["subvalue"]);
        }
        private void subsubchb_chack_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit ch = (CheckEdit)sender;
            string query = "select * from tbl_role where `subsubkey` ='" + ch.Name + "'  GROUP BY `key`,`subkey`,`subsubkey`";
            DataTable dt = fun.FetchDataTable(query);

            foreach (DataRow dr in dt.Rows)
            {
                CheckEdit cb_a = boxes.FirstOrDefault(tt => tt.Name == "add_" + ch.Name);
                if (cb_a != null)
                    cb_a.Checked = ch.Checked;
                CheckEdit cb_e = boxes.FirstOrDefault(tt => tt.Name == "edit_" + ch.Name);
                if (cb_e != null)
                    cb_e.Checked = ch.Checked;
                CheckEdit cb_d = boxes.FirstOrDefault(tt => tt.Name == "delete_" + ch.Name);
                if (cb_d != null)
                    cb_d.Checked = ch.Checked;
            }
        }
        void sub_group_chb(CheckEdit sub_main_ce, string name, string text, bool val)
        {
            sub_main_ce.Text = text;
            sub_main_ce.Name = name;
            sub_main_ce.CheckedChanged += Sub_main_ce_CheckedChanged; ;
            sub_main_ce.ForeColor = ColorTranslator.FromHtml("Chocolate");
            sub_main_ce.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
            sub_main_ce.Width = 146;
            sub_main_ce.Checked = val;
        }

        private void Sub_main_ce_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit ch = (CheckEdit)sender;
            string query = "select * from tbl_role where `subkey` ='" + ch.Name + "' and not subsubkey is null  GROUP BY `key`,`subkey`,`subsubkey`";
            DataTable dt = fun.FetchDataTable(query);

            foreach (DataRow dr in dt.Rows)
            {
                CheckEdit cb = boxes.FirstOrDefault(tt => tt.Name == dr["subsubkey"].ToString());
                if (cb != null)
                    cb.Checked = ch.Checked;
            }
        }
        void chb(CheckEdit chb_aed, string name, string text, bool val)
        {
            chb_aed.Text = text;
            chb_aed.Name = name;
            chb_aed.ForeColor = ColorTranslator.FromHtml("32,31,53");
            chb_aed.Font = new Font("Verdana", 5.25f, FontStyle.Bold);
            chb_aed.Width = 27;
            chb_aed.Checked = val;
        }
        Main_FD form = new Main_FD("user Permission");
        List<settings_ddl> set_ddl = new List<settings_ddl>();
        private void CB_roles_SelectedIndexChanged(object sender, EventArgs e)
        {
            fun.loaderform(() =>
            {

                AP_panel.Controls.Clear();

                if (CB_roles.SelectedValue != null && CB_roles.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    string query = "SELECT * from tbl_role where permission_id = '" + CB_roles.SelectedValue + "' and `Type` = 'Desktop'";
                    DataTable permission_dt = fun.FetchDataTable(query);
                    query = "SELECT role_id,tab_title as Tab from tbl_role where permission_id = '" + CB_roles.SelectedValue + "' and `Type` = 'Desktop'";
                    DataTable load_list_roles = fun.FetchDataTable(query);
                    defult_role_selected.Properties.DataSource = load_list_roles;
                    defult_role_selected.Properties.DisplayMember = "Tab";
                    defult_role_selected.Properties.ValueMember = "role_id";
                    defult_role_selected.EditValue = null;
                    FlowLayoutPanel single_p = new FlowLayoutPanel();
                    single_p.BorderStyle = BorderStyle.FixedSingle;
                    single_p.Width = 222;
                    single_p.Height = 30;
                    AP_panel.Controls.Add(single_p);
                    FlowLayoutPanel p = null;
                    bool is_default = false;
                    bool val = false;
                    bool is_a = false;
                    bool is_e = false;
                    bool is_d = false;
                    for (int i = 0; i < form.accordionControlMain.Elements.Count; i++)
                    {
                        string tabname = form.accordionControlMain.Elements[i].Name;
                        string tabtext = form.accordionControlMain.Elements[i].Text;
                        DataRow rw_main = permission_dt.AsEnumerable().FirstOrDefault(tt => tt.Field<string>("key") == tabname && string.IsNullOrEmpty(tt.Field<string>("subkey")));
                        if (rw_main != null)
                        {
                            val = Convert.ToBoolean(rw_main["value"] == null ? false : rw_main["value"].ToString() == "" ? false : rw_main["value"]);
                            is_a = Convert.ToBoolean(rw_main["IsAdd"] == null ? false : rw_main["IsAdd"].ToString() == "" ? false : rw_main["IsAdd"]);
                            is_e = Convert.ToBoolean(rw_main["IsEdit"] == null ? false : rw_main["is_default"].ToString() == "" ? false : rw_main["IsEdit"]);
                            is_d = Convert.ToBoolean(rw_main["IsDelete"] == null ? false : rw_main["is_default"].ToString() == "" ? false : rw_main["IsDelete"]);
                            is_default = Convert.ToBoolean(rw_main["is_default"] == null ? false : rw_main["is_default"].ToString() == "" ? false : rw_main["is_default"]);
                            if (is_default)
                                defult_role_selected.EditValue = rw_main["role_id"];
                        }
                        if (!string.IsNullOrEmpty(tabtext))
                        {
                            CheckEdit main_ce = new CheckEdit();
                            if (form.accordionControlMain.Elements[i].Elements.Count > 0)
                            {
                                mainchb(main_ce, tabname, tabtext, val);
                                p = new FlowLayoutPanel();
                                p.BorderStyle = BorderStyle.FixedSingle;
                                p.Width = 265;
                                p.Height = 30;
                                p.Controls.Add(main_ce);
                                p.SetFlowBreak(main_ce, true);
                                AP_panel.Controls.Add(p);
                            }
                            else
                            {
                                single_mainchb(main_ce, tabname, tabtext, val);
                                single_p.Height += main_ce.Height + 6;
                                single_p.Width = 265;
                                single_p.Controls.Add(main_ce);
                                CheckEdit chb_add = new CheckEdit();
                                chb(chb_add, "add_" + tabname, "A", is_a);
                                CheckEdit chb_edit = new CheckEdit();
                                chb(chb_edit, "edit_" + tabname, "E", is_e);
                                CheckEdit chb_delete = new CheckEdit();
                                chb(chb_delete, "delete_" + tabname, "D", is_d);
                                single_p.Controls.Add(chb_add);
                                single_p.Controls.Add(chb_edit);
                                single_p.Controls.Add(chb_delete);
                                single_p.SetFlowBreak(chb_delete, true);
                            }

                            for (int j = 0; j < form.accordionControlMain.Elements[i].Elements.Count; j++)
                            {
                                string subtabname = form.accordionControlMain.Elements[i].Elements[j].Name;
                                string subtabtext = form.accordionControlMain.Elements[i].Elements[j].Text;
                                DataRow rw_sub = permission_dt.AsEnumerable().FirstOrDefault(tt => tt.Field<string>("subkey") == subtabname && string.IsNullOrEmpty(tt.Field<string>("subsubkey")));
                                val = false; is_a = false; is_e = false; is_d = false;
                                if (rw_sub != null)
                                {
                                    val = Convert.ToBoolean(rw_sub["value"] == null ? false : rw_sub["value"].ToString() == "" ? false : rw_sub["value"]);
                                    is_a = Convert.ToBoolean(rw_sub["IsAdd"] == null ? false : rw_sub["IsAdd"].ToString() == "" ? false : rw_sub["IsAdd"]);
                                    is_e = Convert.ToBoolean(rw_sub["IsEdit"] == null ? false : rw_sub["is_default"].ToString() == "" ? false : rw_sub["IsEdit"]);
                                    is_d = Convert.ToBoolean(rw_sub["IsDelete"] == null ? false : rw_sub["is_default"].ToString() == "" ? false : rw_sub["IsDelete"]);
                                    is_default = Convert.ToBoolean(rw_sub["is_default"] == null ? false : rw_sub["is_default"].ToString() == "" ? false : rw_sub["is_default"]);
                                    if (is_default)
                                        defult_role_selected.EditValue = rw_sub["role_id"];
                                }
                                if (!string.IsNullOrEmpty(subtabtext))
                                {
                                    if (form.accordionControlMain.Elements[i].Elements[j].Elements.Count <= 0)
                                    {
                                        CheckEdit sub_main_ce = new CheckEdit();
                                        subchb(sub_main_ce, subtabname, subtabtext, val);
                                        p.Height += sub_main_ce.Height + 6;
                                        p.Controls.Add(sub_main_ce);
                                        CheckEdit chb_add = new CheckEdit();
                                        chb(chb_add, "add_" + subtabname, "A", is_a);
                                        CheckEdit chb_edit = new CheckEdit();
                                        chb(chb_edit, "edit_" + subtabname, "E", is_e);
                                        CheckEdit chb_delete = new CheckEdit();
                                        chb(chb_delete, "delete_" + subtabname, "D", is_d);
                                        p.Controls.Add(chb_add);
                                        p.Controls.Add(chb_edit);
                                        p.Controls.Add(chb_delete);
                                        p.SetFlowBreak(chb_delete, true);
                                    }
                                    else
                                    {
                                        CheckEdit sub_main_ce = new CheckEdit();
                                        sub_group_chb(sub_main_ce, subtabname, subtabtext, val);
                                        p.Height += sub_main_ce.Height + 6;
                                        p.Controls.Add(sub_main_ce);

                                        for (int k = 0; k < form.accordionControlMain.Elements[i].Elements[j].Elements.Count; k++)
                                        {
                                            AccordionControlElement acec = form.accordionControlMain.Elements[i].Elements[j].Elements[k];
                                            string subsubtabname = acec.Name;
                                            string subsubtabtext = acec.Text;
                                            DataRow rw_subsub = permission_dt.AsEnumerable().FirstOrDefault(tt => tt.Field<string>("subsubkey") == subsubtabname);
                                            val = false; is_a = false; is_e = false; is_d = false;
                                            if (rw_subsub != null)
                                            {
                                                val = Convert.ToBoolean(rw_subsub["value"] == null ? false : rw_subsub["value"].ToString() == "" ? false : rw_subsub["value"]);
                                                is_a = Convert.ToBoolean(rw_subsub["IsAdd"] == null ? false : rw_subsub["IsAdd"].ToString() == "" ? false : rw_subsub["IsAdd"]);
                                                is_e = Convert.ToBoolean(rw_subsub["IsEdit"] == null ? false : rw_subsub["is_default"].ToString() == "" ? false : rw_subsub["IsEdit"]);
                                                is_d = Convert.ToBoolean(rw_subsub["IsDelete"] == null ? false : rw_subsub["is_default"].ToString() == "" ? false : rw_subsub["IsDelete"]);
                                                is_default = Convert.ToBoolean(rw_subsub["is_default"] == null ? false : rw_subsub["is_default"].ToString() == "" ? false : rw_subsub["is_default"]);
                                                if (is_default)
                                                    defult_role_selected.EditValue = rw_subsub["role_id"];
                                            }
                                            if (!string.IsNullOrEmpty(subtabtext))
                                            {
                                                CheckEdit subsub_main_ce = new CheckEdit();
                                                subsubchb(subsub_main_ce, subsubtabname, subsubtabtext, val);
                                                p.Height += subsub_main_ce.Height + 6;
                                                p.Controls.Add(subsub_main_ce);
                                                CheckEdit chb_add = new CheckEdit();
                                                chb(chb_add, "add_" + subsubtabname, "A", is_a);
                                                CheckEdit chb_edit = new CheckEdit();
                                                chb(chb_edit, "edit_" + subsubtabname, "E", is_e);
                                                CheckEdit chb_delete = new CheckEdit();
                                                chb(chb_delete, "delete_" + subsubtabname, "D", is_d);
                                                p.Controls.Add(chb_add);
                                                p.Controls.Add(chb_edit);
                                                p.Controls.Add(chb_delete);
                                                p.SetFlowBreak(chb_delete, true);
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }

                    DataRow rw_ddl = permission_dt.AsEnumerable().FirstOrDefault(tt => tt.Field<string>("key") == "Settings" && string.IsNullOrEmpty(tt.Field<string>("subkey")));
                    val = false;
                    if (rw_ddl != null)
                    {
                        val = Convert.ToBoolean(rw_ddl["value"] == null ? false : rw_ddl["value"].ToString() == "" ? false : rw_ddl["value"]);
                    }
                    CheckEdit ddl_ce = new CheckEdit();
                    mainchb(ddl_ce, "Settings", "Settings", val);
                    p = new FlowLayoutPanel();
                    p.BorderStyle = BorderStyle.FixedSingle;
                    p.Width = 265;
                    p.Height = 30;
                    p.Controls.Add(ddl_ce);
                    p.SetFlowBreak(ddl_ce, true);
                    AP_panel.Controls.Add(p);
                    set_ddl.Clear();
                    set_ddl = form.settings_ddl();
                    for (int i = 0; i < set_ddl.Count; i++)
                    {
                        string set_val = set_ddl[i].value;
                        string set_text = set_ddl[i].description;
                        DataRow rw_sub = permission_dt.AsEnumerable().FirstOrDefault(tt => tt.Field<string>("subkey") == set_val && string.IsNullOrEmpty(tt.Field<string>("subsubkey")));
                        val = false; is_a = false; is_e = false; is_d = false;
                        if (rw_sub != null)
                        {
                            val = Convert.ToBoolean(rw_sub["value"] == null ? false : rw_sub["value"].ToString() == "" ? false : rw_sub["value"]);
                            is_a = Convert.ToBoolean(rw_sub["IsAdd"] == null ? false : rw_sub["IsAdd"].ToString() == "" ? false : rw_sub["IsAdd"]);
                            is_e = Convert.ToBoolean(rw_sub["IsEdit"] == null ? false : rw_sub["is_default"].ToString() == "" ? false : rw_sub["IsEdit"]);
                            is_d = Convert.ToBoolean(rw_sub["IsDelete"] == null ? false : rw_sub["is_default"].ToString() == "" ? false : rw_sub["IsDelete"]);
                            is_default = Convert.ToBoolean(rw_sub["is_default"] == null ? false : rw_sub["is_default"].ToString() == "" ? false : rw_sub["is_default"]);
                            if (is_default)
                                defult_role_selected.EditValue = rw_sub["role_id"];
                        }
                        if (set_val != "0")
                        {
                            CheckEdit sub_main_ce = new CheckEdit();
                            subchb(sub_main_ce, set_val, set_text, val);
                            p.Height += sub_main_ce.Height + 6;
                            p.Controls.Add(sub_main_ce);
                            CheckEdit chb_add = new CheckEdit();
                            chb(chb_add, "add_" + set_val, "A", is_a);
                            CheckEdit chb_edit = new CheckEdit();
                            chb(chb_edit, "edit_" + set_val, "E", is_e);
                            CheckEdit chb_delete = new CheckEdit();
                            chb(chb_delete, "delete_" + set_val, "D", is_d);
                            p.Controls.Add(chb_add);
                            p.Controls.Add(chb_edit);
                            p.Controls.Add(chb_delete);
                            p.SetFlowBreak(chb_delete, true);
                        }

                    }
                    allchackboxes();
                }
            });
        }



        private void btn_add_role_Click(object sender, EventArgs e)
        {
            Main_FD form = new Main_FD();
            string query = "select * from tbl_permission where title = '" + txt_role_name.Text + "'";
            DataTable perdt = fun.FetchDataTable(query);
            if (perdt.Rows.Count <= 0)
            {
                fun.loaderform(() =>
                {
                    query = "INSERT INTO `tbl_permission`(`title`) VALUES ('" + txt_role_name.Text + "');";
                    long permistionid = fun.Execute_Insert(query);
                    for (int i = 0; i < form.accordionControlMain.Elements.Count; i++)
                    {
                        string tabname = form.accordionControlMain.Elements[i].Name;
                        query = "SELECT * from tbl_role where permission_id = '" + permistionid + "' and `key` ='" + tabname + "'";
                        DataTable maintab = fun.FetchDataTable(query);
                        if (maintab.Rows.Count <= 0)
                        {
                            query = "INSERT INTO `tbl_role`(`key`, `value`, `permission_id`, `IsAdd`, `IsEdit`, `IsDelete`) VALUES " +
                                "('" + tabname + "',0,'" + permistionid + "',0,0,0)";
                            fun.Execute_Query(query);
                            for (int j = 0; j < form.accordionControlMain.Elements[i].Elements.Count; j++)
                            {
                                string subtabname = form.accordionControlMain.Elements[i].Elements[j].Name;
                                query = "SELECT * from tbl_role where permission_id = '" + permistionid + "' and `key` ='" + tabname + "' and subkey ='" + subtabname + "'";
                                DataTable detdt = fun.FetchDataTable(query);
                                if (detdt.Rows.Count <= 0)
                                {
                                    query = "INSERT INTO `tbl_role`(`key`, `value`,subkey,subvalue, `permission_id`, `IsAdd`, `IsEdit`, `IsDelete`) VALUES " +
                                        "('" + tabname + "','0','" + subtabname + "',0,'" + permistionid + "',0,0,0)";
                                    fun.Execute_Query(query);
                                }
                            }
                        }
                    }
                    txt_role_name.Text = "";
                });
                loadgridRoleSettings();
            }
            else
                MessageBox.Show("Role With Same Name already Exit");
        }

        private void btn_delete_permission_Click(object sender, EventArgs e)
        {
            if (CB_roles.SelectedValue != null && CB_roles.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                if (MessageBox.Show("Do you really want to delete Role = " + CB_roles.Text + "", "Confirmation Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    fun.loaderform(() =>
                    {
                        string query = "Delete from tbl_permission where permission_id = '" + CB_roles.SelectedValue + "';";
                        query += "DELETE FROM `tbl_role` WHERE `permission_id`= '" + CB_roles.SelectedValue + "';";
                        fun.Execute_Query(query);

                    });
                    loadgridRoleSettings();
                }
            }
        }

        private void chb_all_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].Checked = chb_all.Checked;
            }
        }

        string per_query = "";
        private void btnApply_Click(object sender, EventArgs e)
        {
            //var id = fun.GetPermissionID(CB_roles.SelectedValue, "Desktop");

            if (CB_roles.SelectedValue != null && CB_roles.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                fun.loaderform(() =>
                {
                    per_query = "";
                    user_permission();
                    if (!string.IsNullOrEmpty(per_query))
                    {
                        fun.Execute_Query(per_query);
                        MessageBox.Show("Permission Updated Successfully", "Permission", MessageBoxButtons.OK);
                    }
                });
            }
            else
            {
                MessageBox.Show("Select Permission is not exit.", "Info");
                return;
            }
        }
        public void user_permission()
        {
            string query = "SELECT * from tbl_role where permission_id = '" + CB_roles.SelectedValue + "' and `Type` = 'Desktop'";
            DataTable permission_dt = fun.FetchDataTable(query);
            for (int i = 0; i < form.accordionControlMain.Elements.Count; i++)
            {
                form.accordionControlMain.Elements[i].Visible = false;
                string tabname = form.accordionControlMain.Elements[i].Name;
                string tabtext = form.accordionControlMain.Elements[i].Text;
                DataRow rw_main = permission_dt.AsEnumerable().FirstOrDefault(tt => tt.Field<string>("key") == tabname && (tt.Field<string>("subkey") == null || tt.Field<string>("subkey") == ""));

                CheckEdit cb_val = boxes.FirstOrDefault(tt => tt.Name == tabname);
                int is_a = 0, is_e = 0, is_d = 0, val = 0;
                val = cb_val.Checked ? 1 : 0;
                if (form.accordionControlMain.Elements[i].Elements.Count <= 0)
                {
                    CheckEdit cb_add = boxes.FirstOrDefault(tt => tt.Name == "add_" + tabname);
                    is_a = cb_add.Checked ? 1 : 0;
                    CheckEdit cb_edit = boxes.FirstOrDefault(tt => tt.Name == "edit_" + tabname);
                    is_e = cb_edit.Checked ? 1 : 0;
                    CheckEdit cb_delete = boxes.FirstOrDefault(tt => tt.Name == "delete_" + tabname);
                    is_d = cb_delete.Checked ? 1 : 0;
                }
                if (rw_main == null)
                {
                    per_query += "INSERT INTO `tbl_role`(`key`,tab_title, `value`, `permission_id`, `IsAdd`, `IsEdit`, `IsDelete`,`Type`) VALUES " +
                        "('" + tabname + "','" + tabtext + "','" + val + "','" + CB_roles.SelectedValue + "','" + is_a + "','" + is_e + "','" + is_d + "','Desktop');";
                }
                else
                {
                    per_query += "update `tbl_role` set `value` = '" + val + "',`IsAdd` = '" + is_a + "',`IsEdit` = '" + is_e + "',`IsDelete` = '" + is_d + "' where `role_id` = '" + rw_main["role_id"].ToString() + "';";
                }



                if (!string.IsNullOrEmpty(tabtext))
                    subkey_accordian_elements_permission(i, permission_dt);
            }
            #region Settings Dropdownlist
            DataRow rw_ddl = permission_dt.AsEnumerable().FirstOrDefault(tt => tt.Field<string>("key") == "Settings" && (tt.Field<string>("subkey") == null || tt.Field<string>("subkey") == ""));
            CheckEdit cb_ddl_val = boxes.FirstOrDefault(tt => tt.Name == "Settings");
            int ddl_is_a = 0, ddl_is_e = 0, ddl_is_d = 0, ddl_val = 0;
            ddl_val = cb_ddl_val.Checked ? 1 : 0;
            if (rw_ddl == null)
                per_query += "INSERT INTO `tbl_role`(`key`,tab_title, `value`, `permission_id`, `IsAdd`, `IsEdit`, `IsDelete`,`Type`) VALUES ('Settings','Settings_list','" + ddl_val + "','" + CB_roles.SelectedValue + "','" + ddl_is_a + "','" + ddl_is_e + "','" + ddl_is_d + "','Desktop');";
            else
                per_query += "update `tbl_role` set `value` = '" + ddl_val + "',`IsAdd` = '" + ddl_is_a + "',`IsEdit` = '" + ddl_is_e + "',`IsDelete` = '" + ddl_is_d + "' where `role_id` = '" + rw_ddl["role_id"].ToString() + "';";
            set_ddl.Clear();
            set_ddl = form.settings_ddl();
            for (int i = 0; i < set_ddl.Count; i++)
            {
                string set_val = set_ddl[i].value;
                string set_text = set_ddl[i].description;
                DataRow rw_sub = permission_dt.AsEnumerable().FirstOrDefault(tt => tt.Field<string>("subkey") == set_val && (tt.Field<string>("subsubkey") == null || tt.Field<string>("subsubkey") == ""));
                CheckEdit cb_val = boxes.FirstOrDefault(tt => tt.Name == set_val);
                int is_a = 0, is_e = 0, is_d = 0, val = 0;
                val = cb_val.Checked ? 1 : 0;

                CheckEdit cb_add = boxes.FirstOrDefault(tt => tt.Name == "add_" + set_val);
                is_a = cb_add.Checked ? 1 : 0;
                CheckEdit cb_edit = boxes.FirstOrDefault(tt => tt.Name == "edit_" + set_val);
                is_e = cb_edit.Checked ? 1 : 0;
                CheckEdit cb_delete = boxes.FirstOrDefault(tt => tt.Name == "delete_" + set_val);
                is_d = cb_delete.Checked ? 1 : 0;
                if (rw_sub == null)
                {
                    per_query += "INSERT INTO `tbl_role`(`key`,subkey,tab_title, `value`, `permission_id`, `IsAdd`, `IsEdit`, `IsDelete`,`Type`) VALUES " +
                        "('Settings','" + set_val + "','" + set_text + "','" + val + "','" + CB_roles.SelectedValue + "','" + is_a + "','" + is_e + "','" + is_d + "','Desktop');";
                }
                else
                {
                    per_query += "update `tbl_role` set `value` = '" + val + "',`IsAdd` = '" + is_a + "',`IsEdit` = '" + is_e + "',`IsDelete` = '" + is_d + "' where `role_id` = '" + rw_sub["role_id"].ToString() + "';";
                }
            }
            #endregion
        }

        public void subkey_accordian_elements_permission(int tab_index, DataTable permission_dt)
        {
            for (int i = 0; i < form.accordionControlMain.Elements[tab_index].Elements.Count; i++)
            {
                AccordionControlElement acec = form.accordionControlMain.Elements[tab_index].Elements[i];
                acec.Visible = false;
                string tabname = form.accordionControlMain.Elements[tab_index].Name;
                string subtabname = acec.Name;
                string subtabtext = acec.Text;
                DataRow rw_sub = permission_dt.AsEnumerable().FirstOrDefault(tt => tt.Field<string>("subkey") == subtabname && (tt.Field<string>("subsubkey") == null || tt.Field<string>("subsubkey") == ""));
                CheckEdit cb_val = boxes.FirstOrDefault(tt => tt.Name == subtabname);
                int is_a = 0, is_e = 0, is_d = 0, val = 0;
                val = cb_val.Checked ? 1 : 0;
                if (acec.Elements.Count <= 0)
                {
                    CheckEdit cb_add = boxes.FirstOrDefault(tt => tt.Name == "add_" + subtabname);
                    is_a = cb_add.Checked ? 1 : 0;
                    CheckEdit cb_edit = boxes.FirstOrDefault(tt => tt.Name == "edit_" + subtabname);
                    is_e = cb_edit.Checked ? 1 : 0;
                    CheckEdit cb_delete = boxes.FirstOrDefault(tt => tt.Name == "delete_" + subtabname);
                    is_d = cb_delete.Checked ? 1 : 0;
                    
                }
                if (rw_sub == null)
                {
                    per_query += "INSERT INTO `tbl_role`(`key`,subkey,tab_title, `value`, `permission_id`, `IsAdd`, `IsEdit`, `IsDelete`,`Type`) VALUES " +
                        "('" + tabname + "','" + subtabname + "','" + subtabtext + "','" + val + "','" + CB_roles.SelectedValue + "','" + is_a + "','" + is_e + "','" + is_d + "','Desktop');";
                }
                else
                {
                    per_query += "update `tbl_role` set `value` = '" + val + "',`IsAdd` = '" + is_a + "',`IsEdit` = '" + is_e + "',`IsDelete` = '" + is_d + "' where `role_id` = '" + rw_sub["role_id"].ToString() + "';";
                }
                if (!string.IsNullOrEmpty(subtabtext))
                    subsubkey_accordian_elements_permission(tab_index, i, permission_dt);
            }
        }
        public void subsubkey_accordian_elements_permission(int tab_index, int subtab_index, DataTable permission_dt)
        {
            for (int i = 0; i < form.accordionControlMain.Elements[tab_index].Elements[subtab_index].Elements.Count; i++)
            {
                string tabname = form.accordionControlMain.Elements[tab_index].Name;
                string subtabname = form.accordionControlMain.Elements[tab_index].Elements[subtab_index].Name;
                AccordionControlElement acec = form.accordionControlMain.Elements[tab_index].Elements[subtab_index].Elements[i];
                acec.Visible = false;
                string subsubtabname = acec.Name;
                string subsubtabtext = acec.Text;
                CheckEdit cb_val = boxes.FirstOrDefault(tt => tt.Name == subsubtabname);
                int is_a = 0, is_e = 0, is_d = 0, val = 0;
                val = cb_val.Checked ? 1 : 0;
                if (acec.Elements.Count <= 0)
                {
                    CheckEdit cb_add = boxes.FirstOrDefault(tt => tt.Name == "add_" + subsubtabname);
                    is_a = cb_add.Checked ? 1 : 0;
                    CheckEdit cb_edit = boxes.FirstOrDefault(tt => tt.Name == "edit_" + subsubtabname);
                    is_e = cb_edit.Checked ? 1 : 0;
                    CheckEdit cb_delete = boxes.FirstOrDefault(tt => tt.Name == "delete_" + subsubtabname);
                    is_d = cb_delete.Checked ? 1 : 0;
                }
                DataRow rw_subsub = permission_dt.AsEnumerable().FirstOrDefault(tt => tt.Field<string>("subsubkey") == subsubtabname);
                if (rw_subsub == null)
                {
                    per_query += "INSERT INTO `tbl_role`(`key`,subkey,`subsubkey`,tab_title, `value`, `permission_id`, `IsAdd`, `IsEdit`, `IsDelete`,`Type`) VALUES " +
                        "('" + tabname + "','" + subtabname + "','" + subsubtabname + "','" + subsubtabtext + "','" + val + "','" + CB_roles.SelectedValue + "','" + is_a + "','" + is_e + "','" + is_d + "','Desktop');";
                }
                else
                {
                    per_query += "update `tbl_role` set `value` = '" + val + "',`IsAdd` = '" + is_a + "',`IsEdit` = '" + is_e + "',`IsDelete` = '" + is_d + "' where `role_id` = '" + rw_subsub["role_id"].ToString() + "';";
                }
            }
        }
        private void defult_role_selected_EditValueChanged(object sender, EventArgs e)
        {
            if (defult_role_selected != null)
            {
                if (CB_roles.SelectedValue == null)
                {
                    MessageBox.Show("Please Select Role first and try again", "info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                string query = "update `tbl_role` set `is_default` = '0' where `permission_id` = '" + CB_roles.SelectedValue + "';";
                fun.ExecuteQuery(query);
                query = "update `tbl_role` set `is_default` = '1' where `role_id` = '" + defult_role_selected.EditValue + "';";
                fun.ExecuteQuery(query);
            }
        }
    }
}
