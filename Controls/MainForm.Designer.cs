namespace ACQC.Metrics
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.buttonShowGroups = new System.Windows.Forms.Button();
            this.checkBoxShowFiles = new System.Windows.Forms.CheckBox();
            this.checkBoxShowFunctions = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.buttonShowGroups, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.listView, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonClear, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonAbout, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxShowFiles, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxShowFunctions, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(742, 273);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // listView
            // 
            this.listView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView.AllowDrop = true;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader10,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.tableLayoutPanel1.SetColumnSpan(this.listView, 3);
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(3, 3);
            this.listView.Name = "listView";
            this.listView.ShowItemToolTips = true;
            this.listView.Size = new System.Drawing.Size(736, 215);
            this.listView.TabIndex = 0;
            this.toolTips.SetToolTip(this.listView, "Displays the analysis results");
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.listView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File";
            this.columnHeader1.Width = 161;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Name";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "LINES";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "LLOC";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "LLOCi";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "LLOW";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "CC";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "DC";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "PROCS";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "CARGS";
            // 
            // buttonClear
            // 
            this.buttonClear.AutoSize = true;
            this.buttonClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonClear.Location = new System.Drawing.Point(497, 247);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(242, 23);
            this.buttonClear.TabIndex = 1;
            this.buttonClear.Text = "Clear";
            this.toolTips.SetToolTip(this.buttonClear, "Empty the list contents");
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonAbout
            // 
            this.buttonAbout.AutoSize = true;
            this.buttonAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAbout.Location = new System.Drawing.Point(3, 247);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(241, 23);
            this.buttonAbout.TabIndex = 2;
            this.buttonAbout.Text = "About...";
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // buttonShowGroups
            // 
            this.buttonShowGroups.AutoSize = true;
            this.buttonShowGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonShowGroups.Location = new System.Drawing.Point(250, 247);
            this.buttonShowGroups.Name = "buttonShowGroups";
            this.buttonShowGroups.Size = new System.Drawing.Size(241, 23);
            this.buttonShowGroups.TabIndex = 3;
            this.buttonShowGroups.Text = "Show groups";
            this.buttonShowGroups.UseVisualStyleBackColor = true;
            this.buttonShowGroups.Click += new System.EventHandler(this.buttonShowGroups_Click);
            // 
            // checkBoxShowFiles
            // 
            this.checkBoxShowFiles.AutoSize = true;
            this.checkBoxShowFiles.Checked = true;
            this.checkBoxShowFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxShowFiles.Location = new System.Drawing.Point(3, 224);
            this.checkBoxShowFiles.Name = "checkBoxShowFiles";
            this.checkBoxShowFiles.Size = new System.Drawing.Size(241, 17);
            this.checkBoxShowFiles.TabIndex = 4;
            this.checkBoxShowFiles.Text = "Show files";
            this.checkBoxShowFiles.UseVisualStyleBackColor = true;
            this.checkBoxShowFiles.CheckedChanged += new System.EventHandler(this.checkBoxShowFiles_CheckedChanged);
            // 
            // checkBoxShowFunctions
            // 
            this.checkBoxShowFunctions.AutoSize = true;
            this.checkBoxShowFunctions.Checked = true;
            this.checkBoxShowFunctions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxShowFunctions.Location = new System.Drawing.Point(250, 224);
            this.checkBoxShowFunctions.Name = "checkBoxShowFunctions";
            this.checkBoxShowFunctions.Size = new System.Drawing.Size(241, 17);
            this.checkBoxShowFunctions.TabIndex = 5;
            this.checkBoxShowFunctions.Text = "Show functions";
            this.checkBoxShowFunctions.UseVisualStyleBackColor = true;
            this.checkBoxShowFunctions.CheckedChanged += new System.EventHandler(this.checkBoxShowFunctions_CheckedChanged);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 273);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Metrics Computer (Drop source files on me)";
            this.toolTips.SetToolTip(this, "Gimme more files!!!");
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.Button buttonAbout;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.Button buttonShowGroups;
        private System.Windows.Forms.CheckBox checkBoxShowFiles;
        private System.Windows.Forms.CheckBox checkBoxShowFunctions;
    }
}