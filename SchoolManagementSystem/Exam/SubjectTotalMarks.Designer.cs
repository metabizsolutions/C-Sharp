namespace SchoolManagementSystem.Exam
{
    partial class SubjectTotalMarks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubjectTotalMarks));
            this.gridManageMarks = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.DropdownExamNew = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnMSTM = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridManageMarks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DropdownExamNew.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // gridManageMarks
            // 
            this.gridManageMarks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridManageMarks.Location = new System.Drawing.Point(0, 72);
            this.gridManageMarks.MainView = this.gridView1;
            this.gridManageMarks.Name = "gridManageMarks";
            this.gridManageMarks.Size = new System.Drawing.Size(959, 392);
            this.gridManageMarks.TabIndex = 9;
            this.gridManageMarks.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridManageMarks.EditorKeyDown += new System.Windows.Forms.KeyEventHandler(this.gridManageMarks_EditorKeyDown);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridManageMarks;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.DropdownExamNew);
            this.groupControl1.Controls.Add(this.btnMSTM);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(959, 72);
            this.groupControl1.TabIndex = 8;
            this.groupControl1.Text = "Manage Subjects Total Marks";
            // 
            // DropdownExamNew
            // 
            this.DropdownExamNew.Location = new System.Drawing.Point(5, 45);
            this.DropdownExamNew.Name = "DropdownExamNew";
            this.DropdownExamNew.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DropdownExamNew.Properties.PopupView = this.gridView2;
            this.DropdownExamNew.Size = new System.Drawing.Size(182, 20);
            this.DropdownExamNew.TabIndex = 78;
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // btnMSTM
            // 
            this.btnMSTM.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMSTM.ImageOptions.Image")));
            this.btnMSTM.Location = new System.Drawing.Point(193, 42);
            this.btnMSTM.Name = "btnMSTM";
            this.btnMSTM.Size = new System.Drawing.Size(122, 23);
            this.btnMSTM.TabIndex = 59;
            this.btnMSTM.Text = "Manage Total Marks";
            this.btnMSTM.Click += new System.EventHandler(this.btnMSTM_Click);
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(5, 26);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(59, 13);
            this.labelControl7.TabIndex = 49;
            this.labelControl7.Text = "Select Exam";
            // 
            // SubjectTotalMarks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridManageMarks);
            this.Controls.Add(this.groupControl1);
            this.Name = "SubjectTotalMarks";
            this.Size = new System.Drawing.Size(959, 464);
            this.Enter += new System.EventHandler(this.SubjectTotalMarks_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.gridManageMarks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DropdownExamNew.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridManageMarks;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnMSTM;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.SearchLookUpEdit DropdownExamNew;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
    }
}
