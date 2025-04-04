namespace SchoolManagementSystem.Admin
{
    partial class Permissions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Permissions));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_delete_permission = new DevExpress.XtraEditors.SimpleButton();
            this.btn_add_role = new DevExpress.XtraEditors.SimpleButton();
            this.chb_all = new DevExpress.XtraEditors.CheckEdit();
            this.txt_role_name = new System.Windows.Forms.TextBox();
            this.CB_roles = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.AP_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.defult_role_selected = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chb_all.Properties)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defult_role_selected.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.defult_role_selected);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_delete_permission);
            this.panel1.Controls.Add(this.btn_add_role);
            this.panel1.Controls.Add(this.chb_all);
            this.panel1.Controls.Add(this.txt_role_name);
            this.panel1.Controls.Add(this.CB_roles);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(989, 37);
            this.panel1.TabIndex = 2;
            // 
            // btn_delete_permission
            // 
            this.btn_delete_permission.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.btn_delete_permission.Location = new System.Drawing.Point(569, 6);
            this.btn_delete_permission.Name = "btn_delete_permission";
            this.btn_delete_permission.Size = new System.Drawing.Size(66, 27);
            this.btn_delete_permission.TabIndex = 69;
            this.btn_delete_permission.Text = "Delete";
            this.btn_delete_permission.Click += new System.EventHandler(this.btn_delete_permission_Click);
            // 
            // btn_add_role
            // 
            this.btn_add_role.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_add_role.ImageOptions.Image")));
            this.btn_add_role.Location = new System.Drawing.Point(232, 5);
            this.btn_add_role.Name = "btn_add_role";
            this.btn_add_role.Size = new System.Drawing.Size(66, 27);
            this.btn_add_role.TabIndex = 69;
            this.btn_add_role.Text = "Add";
            this.btn_add_role.Click += new System.EventHandler(this.btn_add_role_Click);
            // 
            // chb_all
            // 
            this.chb_all.Location = new System.Drawing.Point(12, 10);
            this.chb_all.Name = "chb_all";
            this.chb_all.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.chb_all.Properties.Appearance.Options.UseFont = true;
            this.chb_all.Properties.Caption = "All";
            this.chb_all.Size = new System.Drawing.Size(75, 20);
            this.chb_all.TabIndex = 68;
            this.chb_all.CheckedChanged += new System.EventHandler(this.chb_all_CheckedChanged);
            // 
            // txt_role_name
            // 
            this.txt_role_name.Location = new System.Drawing.Point(93, 10);
            this.txt_role_name.Name = "txt_role_name";
            this.txt_role_name.Size = new System.Drawing.Size(132, 20);
            this.txt_role_name.TabIndex = 66;
            // 
            // CB_roles
            // 
            this.CB_roles.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CB_roles.FormattingEnabled = true;
            this.CB_roles.Location = new System.Drawing.Point(373, 9);
            this.CB_roles.Name = "CB_roles";
            this.CB_roles.Size = new System.Drawing.Size(190, 21);
            this.CB_roles.TabIndex = 65;
            this.CB_roles.SelectedIndexChanged += new System.EventHandler(this.CB_roles_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(304, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 23);
            this.label7.TabIndex = 63;
            this.label7.Text = "Roles";
            // 
            // AP_panel
            // 
            this.AP_panel.AutoScroll = true;
            this.AP_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AP_panel.Location = new System.Drawing.Point(0, 37);
            this.AP_panel.Name = "AP_panel";
            this.AP_panel.Size = new System.Drawing.Size(989, 465);
            this.AP_panel.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnApply);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 502);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(989, 54);
            this.panel2.TabIndex = 4;
            // 
            // btnApply
            // 
            this.btnApply.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnApply.ImageOptions.Image")));
            this.btnApply.Location = new System.Drawing.Point(277, 8);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(317, 38);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "SAVE";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(641, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 17);
            this.label1.TabIndex = 70;
            this.label1.Text = "Default Tab";
            // 
            // defult_role_selected
            // 
            this.defult_role_selected.Location = new System.Drawing.Point(734, 10);
            this.defult_role_selected.Name = "defult_role_selected";
            this.defult_role_selected.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.defult_role_selected.Properties.PopupView = this.searchLookUpEdit1View;
            this.defult_role_selected.Size = new System.Drawing.Size(243, 20);
            this.defult_role_selected.TabIndex = 72;
            this.defult_role_selected.EditValueChanged += new System.EventHandler(this.defult_role_selected_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // Permissions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 556);
            this.Controls.Add(this.AP_panel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Glow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Permissions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmpRole";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chb_all.Properties)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.defult_role_selected.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox CB_roles;
        private System.Windows.Forms.TextBox txt_role_name;
        private System.Windows.Forms.FlowLayoutPanel AP_panel;
        private DevExpress.XtraEditors.CheckEdit chb_all;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraEditors.SimpleButton btn_add_role;
        private DevExpress.XtraEditors.SimpleButton btn_delete_permission;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SearchLookUpEdit defult_role_selected;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
    }
}