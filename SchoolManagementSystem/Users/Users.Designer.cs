namespace SchoolManagementSystem.Users
{
    partial class Users
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Users));
            this.gridEmployees = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnCLavel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelEmployee = new DevExpress.XtraEditors.SimpleButton();
            this.label32 = new System.Windows.Forms.Label();
            this.txtUPass = new DevExpress.XtraEditors.TextEdit();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.BtnAddEmployees = new DevExpress.XtraEditors.SimpleButton();
            this.txtUEmail = new DevExpress.XtraEditors.TextEdit();
            this.label29 = new System.Windows.Forms.Label();
            this.txtUName = new DevExpress.XtraEditors.TextEdit();
            this.txtUStatus = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridEmployees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUPass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // gridEmployees
            // 
            this.gridEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridEmployees.Location = new System.Drawing.Point(228, 0);
            this.gridEmployees.MainView = this.gridView5;
            this.gridEmployees.Name = "gridEmployees";
            this.gridEmployees.Size = new System.Drawing.Size(984, 581);
            this.gridEmployees.TabIndex = 34;
            this.gridEmployees.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5});
            // 
            // gridView5
            // 
            this.gridView5.GridControl = this.gridEmployees;
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            this.gridView5.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView5_RowUpdated);
            // 
            // groupControl5
            // 
            this.groupControl5.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl5.AppearanceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(130)))), ((int)(((byte)(184)))));
            this.groupControl5.AppearanceCaption.Options.UseFont = true;
            this.groupControl5.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl5.Controls.Add(this.simpleButton1);
            this.groupControl5.Controls.Add(this.btnCLavel);
            this.groupControl5.Controls.Add(this.BtnDelEmployee);
            this.groupControl5.Controls.Add(this.label32);
            this.groupControl5.Controls.Add(this.txtUPass);
            this.groupControl5.Controls.Add(this.label30);
            this.groupControl5.Controls.Add(this.label31);
            this.groupControl5.Controls.Add(this.BtnAddEmployees);
            this.groupControl5.Controls.Add(this.txtUEmail);
            this.groupControl5.Controls.Add(this.label29);
            this.groupControl5.Controls.Add(this.txtUName);
            this.groupControl5.Controls.Add(this.txtUStatus);
            this.groupControl5.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl5.Location = new System.Drawing.Point(0, 0);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(228, 581);
            this.groupControl5.TabIndex = 33;
            this.groupControl5.Text = "Employees Info";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.BorderColor = System.Drawing.Color.Purple;
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(99)))), ((int)(((byte)(68)))));
            this.simpleButton1.Appearance.Options.UseBorderColor = true;
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Appearance.Options.UseForeColor = true;
            this.simpleButton1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.simpleButton1.Location = new System.Drawing.Point(2, 529);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(224, 25);
            this.simpleButton1.TabIndex = 45;
            this.simpleButton1.Text = "Create Role For Teacher App";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnCLavel
            // 
            this.btnCLavel.Appearance.BorderColor = System.Drawing.Color.Purple;
            this.btnCLavel.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCLavel.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(99)))), ((int)(((byte)(68)))));
            this.btnCLavel.Appearance.Options.UseBorderColor = true;
            this.btnCLavel.Appearance.Options.UseFont = true;
            this.btnCLavel.Appearance.Options.UseForeColor = true;
            this.btnCLavel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnCLavel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCLavel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCLavel.ImageOptions.Image")));
            this.btnCLavel.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnCLavel.Location = new System.Drawing.Point(2, 554);
            this.btnCLavel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCLavel.Name = "btnCLavel";
            this.btnCLavel.Size = new System.Drawing.Size(224, 25);
            this.btnCLavel.TabIndex = 44;
            this.btnCLavel.Text = "Create Role For Desktop System";
            this.btnCLavel.Click += new System.EventHandler(this.btnCLavel_Click);
            // 
            // BtnDelEmployee
            // 
            this.BtnDelEmployee.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtnDelEmployee.Appearance.BorderColor = System.Drawing.Color.Purple;
            this.BtnDelEmployee.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelEmployee.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(99)))), ((int)(((byte)(68)))));
            this.BtnDelEmployee.Appearance.Options.UseBackColor = true;
            this.BtnDelEmployee.Appearance.Options.UseBorderColor = true;
            this.BtnDelEmployee.Appearance.Options.UseFont = true;
            this.BtnDelEmployee.Appearance.Options.UseForeColor = true;
            this.BtnDelEmployee.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.BtnDelEmployee.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnDelEmployee.ImageOptions.Image")));
            this.BtnDelEmployee.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.BtnDelEmployee.Location = new System.Drawing.Point(119, 246);
            this.BtnDelEmployee.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDelEmployee.Name = "BtnDelEmployee";
            this.BtnDelEmployee.Size = new System.Drawing.Size(90, 25);
            this.BtnDelEmployee.TabIndex = 43;
            this.BtnDelEmployee.Text = " Delete";
            this.BtnDelEmployee.Click += new System.EventHandler(this.BtnDelEmployee_Click);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(99)))), ((int)(((byte)(68)))));
            this.label32.Location = new System.Drawing.Point(3, 28);
            this.label32.Margin = new System.Windows.Forms.Padding(4);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(44, 17);
            this.label32.TabIndex = 25;
            this.label32.Text = "Name";
            // 
            // txtUPass
            // 
            this.txtUPass.Location = new System.Drawing.Point(7, 103);
            this.txtUPass.Margin = new System.Windows.Forms.Padding(4);
            this.txtUPass.Name = "txtUPass";
            this.txtUPass.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUPass.Properties.Appearance.Options.UseFont = true;
            this.txtUPass.Size = new System.Drawing.Size(215, 24);
            this.txtUPass.TabIndex = 28;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(99)))), ((int)(((byte)(68)))));
            this.label30.Location = new System.Drawing.Point(3, 135);
            this.label30.Margin = new System.Windows.Forms.Padding(4);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(108, 17);
            this.label30.TabIndex = 29;
            this.label30.Text = "Email/UserName";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(99)))), ((int)(((byte)(68)))));
            this.label31.Location = new System.Drawing.Point(3, 83);
            this.label31.Margin = new System.Windows.Forms.Padding(4);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(66, 17);
            this.label31.TabIndex = 27;
            this.label31.Text = "Password";
            // 
            // BtnAddEmployees
            // 
            this.BtnAddEmployees.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtnAddEmployees.Appearance.BorderColor = System.Drawing.Color.Purple;
            this.BtnAddEmployees.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAddEmployees.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(99)))), ((int)(((byte)(68)))));
            this.BtnAddEmployees.Appearance.Options.UseBackColor = true;
            this.BtnAddEmployees.Appearance.Options.UseBorderColor = true;
            this.BtnAddEmployees.Appearance.Options.UseFont = true;
            this.BtnAddEmployees.Appearance.Options.UseForeColor = true;
            this.BtnAddEmployees.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.BtnAddEmployees.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnAddEmployees.ImageOptions.Image")));
            this.BtnAddEmployees.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.BtnAddEmployees.Location = new System.Drawing.Point(21, 246);
            this.BtnAddEmployees.Margin = new System.Windows.Forms.Padding(4);
            this.BtnAddEmployees.Name = "BtnAddEmployees";
            this.BtnAddEmployees.Size = new System.Drawing.Size(90, 25);
            this.BtnAddEmployees.TabIndex = 33;
            this.BtnAddEmployees.Text = " Add";
            this.BtnAddEmployees.Click += new System.EventHandler(this.BtnAddEmployees_Click);
            // 
            // txtUEmail
            // 
            this.txtUEmail.Location = new System.Drawing.Point(7, 159);
            this.txtUEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtUEmail.Name = "txtUEmail";
            this.txtUEmail.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUEmail.Properties.Appearance.Options.UseFont = true;
            this.txtUEmail.Properties.DisplayFormat.FormatString = "0000-0000000";
            this.txtUEmail.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtUEmail.Properties.EditFormat.FormatString = "0000-0000000";
            this.txtUEmail.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtUEmail.Size = new System.Drawing.Size(215, 24);
            this.txtUEmail.TabIndex = 30;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(99)))), ((int)(((byte)(68)))));
            this.label29.Location = new System.Drawing.Point(3, 191);
            this.label29.Margin = new System.Windows.Forms.Padding(4);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(73, 17);
            this.label29.TabIndex = 31;
            this.label29.Text = "Select Role";
            // 
            // txtUName
            // 
            this.txtUName.Location = new System.Drawing.Point(7, 51);
            this.txtUName.Margin = new System.Windows.Forms.Padding(4);
            this.txtUName.Name = "txtUName";
            this.txtUName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUName.Properties.Appearance.Options.UseFont = true;
            this.txtUName.Size = new System.Drawing.Size(215, 24);
            this.txtUName.TabIndex = 26;
            // 
            // txtUStatus
            // 
            this.txtUStatus.Location = new System.Drawing.Point(7, 215);
            this.txtUStatus.Name = "txtUStatus";
            this.txtUStatus.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUStatus.Properties.Appearance.Options.UseFont = true;
            this.txtUStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtUStatus.Properties.NullText = "";
            this.txtUStatus.Properties.PopupSizeable = false;
            this.txtUStatus.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtUStatus.Size = new System.Drawing.Size(215, 24);
            this.txtUStatus.TabIndex = 37;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // Users
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridEmployees);
            this.Controls.Add(this.groupControl5);
            this.Name = "Users";
            this.Size = new System.Drawing.Size(1212, 581);
            ((System.ComponentModel.ISupportInitialize)(this.gridEmployees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            this.groupControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUPass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridEmployees;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btnCLavel;
        private DevExpress.XtraEditors.SimpleButton BtnDelEmployee;
        private System.Windows.Forms.Label label32;
        private DevExpress.XtraEditors.TextEdit txtUPass;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private DevExpress.XtraEditors.SimpleButton BtnAddEmployees;
        private DevExpress.XtraEditors.TextEdit txtUEmail;
        private System.Windows.Forms.Label label29;
        private DevExpress.XtraEditors.TextEdit txtUName;
        private DevExpress.XtraEditors.SearchLookUpEdit txtUStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
    }
}
