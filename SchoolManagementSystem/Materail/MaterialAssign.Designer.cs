namespace SchoolManagementSystem.Materail
{
    partial class MaterialAssign
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.MaterialCatList = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SectionList = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtfilepath = new DevExpress.XtraEditors.TextEdit();
            this.BtnBBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtdiscription = new System.Windows.Forms.TextBox();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.gridMaterialAssign = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaterialCatList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SectionList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtfilepath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMaterialAssign)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.MaterialCatList);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.txtTitle);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.SectionList);
            this.flowLayoutPanel1.Controls.Add(this.txtfilepath);
            this.flowLayoutPanel1.Controls.Add(this.BtnBBrowse);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.txtdiscription);
            this.flowLayoutPanel1.Controls.Add(this.btnAdd);
            this.flowLayoutPanel1.Controls.Add(this.btnEdit);
            this.flowLayoutPanel1.Controls.Add(this.btnDelete);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1158, 211);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.Location = new System.Drawing.Point(3, 10);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 15);
            this.label4.TabIndex = 118;
            this.label4.Text = "Category:";
            // 
            // MaterialCatList
            // 
            this.MaterialCatList.Location = new System.Drawing.Point(67, 7);
            this.MaterialCatList.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.MaterialCatList.Name = "MaterialCatList";
            this.MaterialCatList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.MaterialCatList.Properties.PopupView = this.gridView3;
            this.MaterialCatList.Size = new System.Drawing.Size(120, 20);
            this.MaterialCatList.TabIndex = 121;
            // 
            // gridView3
            // 
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.Location = new System.Drawing.Point(193, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 15);
            this.label3.TabIndex = 116;
            this.label3.Text = "Title:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(232, 7);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(100, 20);
            this.txtTitle.TabIndex = 117;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.Location = new System.Drawing.Point(338, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 15);
            this.label2.TabIndex = 111;
            this.label2.Text = "Section:";
            // 
            // SectionList
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.SectionList, true);
            this.SectionList.Location = new System.Drawing.Point(395, 7);
            this.SectionList.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.SectionList.Name = "SectionList";
            this.SectionList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SectionList.Properties.NullText = "[EditValue is null]";
            this.SectionList.Size = new System.Drawing.Size(172, 20);
            this.SectionList.TabIndex = 112;
            // 
            // txtfilepath
            // 
            this.txtfilepath.Location = new System.Drawing.Point(3, 37);
            this.txtfilepath.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.txtfilepath.Name = "txtfilepath";
            this.txtfilepath.Size = new System.Drawing.Size(482, 20);
            this.txtfilepath.TabIndex = 119;
            // 
            // BtnBBrowse
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.BtnBBrowse, true);
            this.BtnBBrowse.Location = new System.Drawing.Point(491, 35);
            this.BtnBBrowse.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.BtnBBrowse.Name = "BtnBBrowse";
            this.BtnBBrowse.Size = new System.Drawing.Size(75, 23);
            this.BtnBBrowse.TabIndex = 120;
            this.BtnBBrowse.Text = "Browse";
            this.BtnBBrowse.Click += new System.EventHandler(this.BtnBBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.Location = new System.Drawing.Point(3, 71);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 122;
            this.label1.Text = "Discription:";
            // 
            // txtdiscription
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.txtdiscription, true);
            this.txtdiscription.Location = new System.Drawing.Point(77, 68);
            this.txtdiscription.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.txtdiscription.Multiline = true;
            this.txtdiscription.Name = "txtdiscription";
            this.txtdiscription.Size = new System.Drawing.Size(489, 105);
            this.txtdiscription.TabIndex = 123;
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.ImageOptions.ImageUri.Uri = "Add;Size16x16";
            this.btnAdd.Location = new System.Drawing.Point(3, 181);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(61, 23);
            this.btnAdd.TabIndex = 113;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Enabled = false;
            this.btnEdit.ImageOptions.ImageUri.Uri = "InLineWithText;Size16x16";
            this.btnEdit.Location = new System.Drawing.Point(70, 181);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(61, 23);
            this.btnEdit.TabIndex = 115;
            this.btnEdit.Text = "Edit";
            this.btnEdit.Visible = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.ImageOptions.ImageUri.Uri = "Cancel;Size16x16";
            this.btnDelete.Location = new System.Drawing.Point(137, 181);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(61, 23);
            this.btnDelete.TabIndex = 114;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // gridMaterialAssign
            // 
            this.gridMaterialAssign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMaterialAssign.Location = new System.Drawing.Point(0, 211);
            this.gridMaterialAssign.MainView = this.gridView1;
            this.gridMaterialAssign.Name = "gridMaterialAssign";
            this.gridMaterialAssign.Size = new System.Drawing.Size(1158, 330);
            this.gridMaterialAssign.TabIndex = 1;
            this.gridMaterialAssign.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridMaterialAssign;
            this.gridView1.Name = "gridView1";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MaterialAssign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridMaterialAssign);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "MaterialAssign";
            this.Size = new System.Drawing.Size(1158, 541);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaterialCatList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SectionList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtfilepath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMaterialAssign)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraGrid.GridControl gridMaterialAssign;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SearchLookUpEdit MaterialCatList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTitle;
        private DevExpress.XtraEditors.TextEdit txtfilepath;
        private DevExpress.XtraEditors.SimpleButton BtnBBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtdiscription;
        private DevExpress.XtraEditors.CheckedComboBoxEdit SectionList;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}
