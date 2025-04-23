namespace SchoolManagementSystem.Exam
{
    partial class RollNoSlipsViewer
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.comboContainerPanel = new System.Windows.Forms.Panel();
            this.filterPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.cmbClass = new System.Windows.Forms.ComboBox();
            this.cmbSection = new System.Windows.Forms.ComboBox();
            this.cmbStudentName = new System.Windows.Forms.ComboBox();
            this.spacerPanel = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dataGridContainerPanel = new System.Windows.Forms.Panel();
            this.dataGridSpacerPanel = new System.Windows.Forms.Panel();
            this.dataGridViewSlips = new System.Windows.Forms.DataGridView();
            this.previewPictureBox = new System.Windows.Forms.PictureBox();
            this.panel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSlips)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).BeginInit();
            this.filterPanel.SuspendLayout();
            this.panel.SuspendLayout();
            this.comboContainerPanel.SuspendLayout();
            this.dataGridContainerPanel.SuspendLayout();
            this.SuspendLayout();

            // comboContainerPanel
            this.comboContainerPanel.Controls.Add(this.filterPanel);
            this.comboContainerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboContainerPanel.Height = 70;
            this.comboContainerPanel.Name = "comboContainerPanel";
            this.comboContainerPanel.Size = new System.Drawing.Size(1200, 70);

            // filterPanel
            this.filterPanel.Controls.Add(this.cmbClass);
            this.filterPanel.Controls.Add(this.cmbSection);
            this.filterPanel.Controls.Add(this.cmbStudentName);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.filterPanel.Location = new System.Drawing.Point(0, 0);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Padding = new System.Windows.Forms.Padding(5);
            this.filterPanel.Size = new System.Drawing.Size(1200, 70);
            this.filterPanel.TabIndex = 0;
            this.filterPanel.AutoScroll = true;

            // cmbClass
            this.cmbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClass.Location = new System.Drawing.Point(8, 8);
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Size = new System.Drawing.Size(150, 21);
            this.cmbClass.TabIndex = 0;
            this.cmbClass.SelectedIndexChanged += new System.EventHandler(this.CmbClass_SelectedIndexChanged);

            // cmbSection
            this.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSection.Location = new System.Drawing.Point(164, 8);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(150, 21);
            this.cmbSection.TabIndex = 1;
            this.cmbSection.SelectedIndexChanged += new System.EventHandler(this.CmbSection_SelectedIndexChanged);

            // cmbStudentName
            this.cmbStudentName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStudentName.Location = new System.Drawing.Point(320, 8);
            this.cmbStudentName.Name = "cmbStudentName";
            this.cmbStudentName.Size = new System.Drawing.Size(200, 21);
            this.cmbStudentName.TabIndex = 2;
            this.cmbStudentName.SelectedIndexChanged += new System.EventHandler(this.CmbStudentName_SelectedIndexChanged);

            // spacerPanel
            this.spacerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.spacerPanel.Height = 10;
            this.spacerPanel.Name = "spacerPanel";
            this.spacerPanel.Size = new System.Drawing.Size(1200, 10);
            this.spacerPanel.TabIndex = 1;

            // splitContainer
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 80);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.splitContainer.Size = new System.Drawing.Size(1200, 470);
            this.splitContainer.SplitterDistance = 800;
            this.splitContainer.TabIndex = 2;

            // splitContainer.Panel1
            this.splitContainer.Panel1.Controls.Add(this.dataGridContainerPanel);

            // dataGridContainerPanel
            this.dataGridContainerPanel.Controls.Add(this.dataGridViewSlips);
            this.dataGridContainerPanel.Controls.Add(this.dataGridSpacerPanel);
            this.dataGridContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridContainerPanel.Name = "dataGridContainerPanel";
            this.dataGridContainerPanel.Size = new System.Drawing.Size(800, 470);

            // dataGridSpacerPanel
            this.dataGridSpacerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridSpacerPanel.Height = 10;
            this.dataGridSpacerPanel.Name = "dataGridSpacerPanel";
            this.dataGridSpacerPanel.Size = new System.Drawing.Size(800, 10);

            // dataGridViewSlips
            this.dataGridViewSlips.AllowUserToAddRows = false;
            this.dataGridViewSlips.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewSlips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSlips.Location = new System.Drawing.Point(0, 10);
            this.dataGridViewSlips.Name = "dataGridViewSlips";
            this.dataGridViewSlips.ReadOnly = true;
            this.dataGridViewSlips.MultiSelect = false;
            this.dataGridViewSlips.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dataGridViewSlips.Size = new System.Drawing.Size(800, 460);
            this.dataGridViewSlips.TabIndex = 1;
            this.dataGridViewSlips.SelectionChanged += new System.EventHandler(this.DataGridViewSlips_SelectionChanged);

            // splitContainer.Panel2
            this.splitContainer.Panel2.Controls.Add(this.previewPictureBox);

            // previewPictureBox
            this.previewPictureBox.BackColor = System.Drawing.Color.White;
            this.previewPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewPictureBox.Location = new System.Drawing.Point(0, 0);
            this.previewPictureBox.Name = "previewPictureBox";
            this.previewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
            this.previewPictureBox.Size = new System.Drawing.Size(396, 470);
            this.previewPictureBox.TabIndex = 0;
            this.previewPictureBox.TabStop = false;

            // panel
            this.panel.Controls.Add(this.btnCancel);
            this.panel.Controls.Add(this.btnDelete);
            this.panel.Controls.Add(this.btnPrint);
            this.panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.panel.Height = 50;
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1200, 50);
            this.panel.TabIndex = 3;

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(1097, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;

            // btnDelete
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(941, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(150, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete Selected Slip";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);

            // btnPrint
            this.btnPrint.Enabled = false;
            this.btnPrint.Location = new System.Drawing.Point(785, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(150, 23);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "Print Selected Slip";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);

            // RollNoSlipsViewer
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.spacerPanel);
            this.Controls.Add(this.comboContainerPanel);
            this.Controls.Add(this.panel);
            this.Name = "RollNoSlipsViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "View Roll Number Slips";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSlips)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).EndInit();
            this.filterPanel.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.comboContainerPanel.ResumeLayout(false);
            this.dataGridContainerPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel comboContainerPanel;
        private System.Windows.Forms.FlowLayoutPanel filterPanel;
        private System.Windows.Forms.ComboBox cmbClass;
        private System.Windows.Forms.ComboBox cmbSection;
        private System.Windows.Forms.ComboBox cmbStudentName;
        private System.Windows.Forms.Panel spacerPanel;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel dataGridContainerPanel;
        private System.Windows.Forms.Panel dataGridSpacerPanel;
        private System.Windows.Forms.DataGridView dataGridViewSlips;
        private System.Windows.Forms.PictureBox previewPictureBox;
        private System.Windows.Forms.FlowLayoutPanel panel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnPrint;
    }
}
