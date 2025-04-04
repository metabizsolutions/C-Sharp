namespace SchoolManagementSystem.Exam
{
    partial class SendMarksBySMS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendMarksBySMS));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnSendSMS = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtReceiver = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtExam = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtSection = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtSubject = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtprogressbar = new DevExpress.XtraEditors.ProgressBarControl();
            this.txtStatus = new DevExpress.XtraEditors.MemoEdit();
            this.txtClass = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiver.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtprogressbar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txtType);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.btnSendSMS);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.txtReceiver);
            this.groupControl1.Controls.Add(this.txtExam);
            this.groupControl1.Controls.Add(this.txtSection);
            this.groupControl1.Controls.Add(this.txtSubject);
            this.groupControl1.Controls.Add(this.txtClass);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1149, 69);
            this.groupControl1.TabIndex = 8;
            this.groupControl1.Text = "Send Marks By SMS";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(785, 22);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 13);
            this.labelControl4.TabIndex = 66;
            this.labelControl4.Text = "Type";
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(785, 41);
            this.txtType.Name = "txtType";
            this.txtType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtType.Properties.Items.AddRange(new object[] {
            "Both Present and Absent",
            "Present ",
            "Absent"});
            this.txtType.Size = new System.Drawing.Size(150, 20);
            this.txtType.TabIndex = 67;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(473, 23);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(68, 13);
            this.labelControl3.TabIndex = 64;
            this.labelControl3.Text = "Select Subject";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(317, 23);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(67, 13);
            this.labelControl2.TabIndex = 62;
            this.labelControl2.Text = "Select Section";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(629, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(74, 13);
            this.labelControl1.TabIndex = 60;
            this.labelControl1.Text = "Select Receiver";
            // 
            // btnSendSMS
            // 
            this.btnSendSMS.Enabled = false;
            this.btnSendSMS.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSendSMS.ImageOptions.Image")));
            this.btnSendSMS.Location = new System.Drawing.Point(941, 39);
            this.btnSendSMS.Name = "btnSendSMS";
            this.btnSendSMS.Size = new System.Drawing.Size(82, 23);
            this.btnSendSMS.TabIndex = 59;
            this.btnSendSMS.Text = "Send SMS";
            this.btnSendSMS.Click += new System.EventHandler(this.btnSendSMS_Click);
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(161, 23);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(57, 13);
            this.labelControl8.TabIndex = 51;
            this.labelControl8.Text = "Select Class";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(5, 23);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(58, 13);
            this.labelControl7.TabIndex = 49;
            this.labelControl7.Text = "Select Exam";
            // 
            // txtReceiver
            // 
            this.txtReceiver.Location = new System.Drawing.Point(629, 42);
            this.txtReceiver.Name = "txtReceiver";
            this.txtReceiver.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtReceiver.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtReceiver.Size = new System.Drawing.Size(150, 20);
            this.txtReceiver.TabIndex = 61;
            // 
            // txtExam
            // 
            this.txtExam.Location = new System.Drawing.Point(5, 42);
            this.txtExam.Name = "txtExam";
            this.txtExam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtExam.Size = new System.Drawing.Size(150, 20);
            this.txtExam.TabIndex = 50;
            // 
            // txtSection
            // 
            this.txtSection.Location = new System.Drawing.Point(317, 42);
            this.txtSection.Name = "txtSection";
            this.txtSection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSection.Properties.DisplayFormat.FormatString = "d";
            this.txtSection.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtSection.Properties.EditFormat.FormatString = "d";
            this.txtSection.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtSection.Size = new System.Drawing.Size(150, 20);
            this.txtSection.TabIndex = 63;
            this.txtSection.EditValueChanged += new System.EventHandler(this.txtSection_EditValueChanged);
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(473, 42);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSubject.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtSubject.Size = new System.Drawing.Size(150, 20);
            this.txtSubject.TabIndex = 65;
            // 
            // txtprogressbar
            // 
            this.txtprogressbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtprogressbar.Location = new System.Drawing.Point(0, 437);
            this.txtprogressbar.Name = "txtprogressbar";
            this.txtprogressbar.Properties.AutoHeight = true;
            this.txtprogressbar.Properties.ShowTitle = true;
            this.txtprogressbar.Size = new System.Drawing.Size(1149, 14);
            this.txtprogressbar.TabIndex = 54;
            this.txtprogressbar.TabStop = true;
            // 
            // txtStatus
            // 
            this.txtStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStatus.Location = new System.Drawing.Point(0, 69);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Properties.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(1149, 368);
            this.txtStatus.TabIndex = 63;
            // 
            // txtClass
            // 
            this.txtClass.Location = new System.Drawing.Point(161, 42);
            this.txtClass.Name = "txtClass";
            this.txtClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtClass.Properties.DisplayFormat.FormatString = "d";
            this.txtClass.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtClass.Properties.EditFormat.FormatString = "d";
            this.txtClass.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtClass.Properties.NullText = "";
            this.txtClass.Properties.PopupSizeable = false;
            this.txtClass.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtClass.Size = new System.Drawing.Size(150, 20);
            this.txtClass.TabIndex = 52;
            this.txtClass.EditValueChanged += new System.EventHandler(this.txtClass_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // SendMarksBySMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.txtprogressbar);
            this.Name = "SendMarksBySMS";
            this.Size = new System.Drawing.Size(1149, 451);
            this.Enter += new System.EventHandler(this.SendMarksBySMS_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiver.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtprogressbar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSendSMS;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.ProgressBarControl txtprogressbar;
        private DevExpress.XtraEditors.MemoEdit txtStatus;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtReceiver;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtExam;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtSection;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtSubject;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit txtType;
        private DevExpress.XtraEditors.SearchLookUpEdit txtClass;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
    }
}
