namespace ACQC.Metrics
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager (typeof (AboutBox));
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel ();
			this.labelDownload = new System.Windows.Forms.Label ();
			this.logoPictureBox = new System.Windows.Forms.PictureBox ();
			this.labelProductName = new System.Windows.Forms.Label ();
			this.labelVersion = new System.Windows.Forms.Label ();
			this.labelCopyright = new System.Windows.Forms.Label ();
			this.labelCompanyName = new System.Windows.Forms.Label ();
			this.linkLabel = new System.Windows.Forms.LinkLabel ();
			this.okButton = new System.Windows.Forms.Button ();
			this.richTextBoxLicense = new System.Windows.Forms.RichTextBox ();
			this.tableLayoutPanel.SuspendLayout ();
			((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit ();
			this.SuspendLayout ();
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.ColumnCount = 3;
			this.tableLayoutPanel.ColumnStyles.Add (new System.Windows.Forms.ColumnStyle (System.Windows.Forms.SizeType.Percent, 33F));
			this.tableLayoutPanel.ColumnStyles.Add (new System.Windows.Forms.ColumnStyle (System.Windows.Forms.SizeType.Percent, 67F));
			this.tableLayoutPanel.ColumnStyles.Add (new System.Windows.Forms.ColumnStyle ());
			this.tableLayoutPanel.Controls.Add (this.labelDownload, 0, 5);
			this.tableLayoutPanel.Controls.Add (this.logoPictureBox, 0, 0);
			this.tableLayoutPanel.Controls.Add (this.labelProductName, 1, 0);
			this.tableLayoutPanel.Controls.Add (this.labelVersion, 1, 1);
			this.tableLayoutPanel.Controls.Add (this.labelCopyright, 1, 2);
			this.tableLayoutPanel.Controls.Add (this.labelCompanyName, 1, 3);
			this.tableLayoutPanel.Controls.Add (this.linkLabel, 1, 6);
			this.tableLayoutPanel.Controls.Add (this.okButton, 2, 8);
			this.tableLayoutPanel.Controls.Add (this.richTextBoxLicense, 1, 7);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point (9, 9);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 9;
			this.tableLayoutPanel.RowStyles.Add (new System.Windows.Forms.RowStyle ());
			this.tableLayoutPanel.RowStyles.Add (new System.Windows.Forms.RowStyle ());
			this.tableLayoutPanel.RowStyles.Add (new System.Windows.Forms.RowStyle ());
			this.tableLayoutPanel.RowStyles.Add (new System.Windows.Forms.RowStyle ());
			this.tableLayoutPanel.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Absolute, 10F));
			this.tableLayoutPanel.RowStyles.Add (new System.Windows.Forms.RowStyle ());
			this.tableLayoutPanel.RowStyles.Add (new System.Windows.Forms.RowStyle ());
			this.tableLayoutPanel.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.RowStyles.Add (new System.Windows.Forms.RowStyle ());
			this.tableLayoutPanel.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel.Size = new System.Drawing.Size (547, 451);
			this.tableLayoutPanel.TabIndex = 0;
			// 
			// labelDownload
			// 
			this.tableLayoutPanel.SetColumnSpan (this.labelDownload, 2);
			this.labelDownload.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelDownload.Location = new System.Drawing.Point (159, 78);
			this.labelDownload.Margin = new System.Windows.Forms.Padding (6, 0, 3, 0);
			this.labelDownload.MaximumSize = new System.Drawing.Size (0, 17);
			this.labelDownload.Name = "labelDownload";
			this.labelDownload.Size = new System.Drawing.Size (385, 17);
			this.labelDownload.TabIndex = 26;
			this.labelDownload.Text = "Download link:";
			this.labelDownload.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// logoPictureBox
			// 
			this.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject ("logoPictureBox.Image")));
			this.logoPictureBox.Location = new System.Drawing.Point (3, 3);
			this.logoPictureBox.Name = "logoPictureBox";
			this.tableLayoutPanel.SetRowSpan (this.logoPictureBox, 8);
			this.logoPictureBox.Size = new System.Drawing.Size (147, 415);
			this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.logoPictureBox.TabIndex = 12;
			this.logoPictureBox.TabStop = false;
			// 
			// labelProductName
			// 
			this.tableLayoutPanel.SetColumnSpan (this.labelProductName, 2);
			this.labelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelProductName.Location = new System.Drawing.Point (159, 0);
			this.labelProductName.Margin = new System.Windows.Forms.Padding (6, 0, 3, 0);
			this.labelProductName.MaximumSize = new System.Drawing.Size (0, 17);
			this.labelProductName.Name = "labelProductName";
			this.labelProductName.Size = new System.Drawing.Size (385, 17);
			this.labelProductName.TabIndex = 19;
			this.labelProductName.Text = "Product Name";
			this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelVersion
			// 
			this.tableLayoutPanel.SetColumnSpan (this.labelVersion, 2);
			this.labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelVersion.Location = new System.Drawing.Point (159, 17);
			this.labelVersion.Margin = new System.Windows.Forms.Padding (6, 0, 3, 0);
			this.labelVersion.MaximumSize = new System.Drawing.Size (0, 17);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size (385, 17);
			this.labelVersion.TabIndex = 0;
			this.labelVersion.Text = "Version";
			this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelCopyright
			// 
			this.tableLayoutPanel.SetColumnSpan (this.labelCopyright, 2);
			this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelCopyright.Location = new System.Drawing.Point (159, 34);
			this.labelCopyright.Margin = new System.Windows.Forms.Padding (6, 0, 3, 0);
			this.labelCopyright.MaximumSize = new System.Drawing.Size (0, 17);
			this.labelCopyright.Name = "labelCopyright";
			this.labelCopyright.Size = new System.Drawing.Size (385, 17);
			this.labelCopyright.TabIndex = 21;
			this.labelCopyright.Text = "Copyright";
			this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelCompanyName
			// 
			this.tableLayoutPanel.SetColumnSpan (this.labelCompanyName, 2);
			this.labelCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelCompanyName.Location = new System.Drawing.Point (159, 51);
			this.labelCompanyName.Margin = new System.Windows.Forms.Padding (6, 0, 3, 0);
			this.labelCompanyName.MaximumSize = new System.Drawing.Size (0, 17);
			this.labelCompanyName.Name = "labelCompanyName";
			this.labelCompanyName.Size = new System.Drawing.Size (385, 17);
			this.labelCompanyName.TabIndex = 22;
			this.labelCompanyName.Text = "Company Name";
			this.labelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// linkLabel
			// 
			this.linkLabel.AutoSize = true;
			this.tableLayoutPanel.SetColumnSpan (this.linkLabel, 2);
			this.linkLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.linkLabel.Location = new System.Drawing.Point (156, 95);
			this.linkLabel.Name = "linkLabel";
			this.linkLabel.Size = new System.Drawing.Size (388, 13);
			this.linkLabel.TabIndex = 25;
			this.linkLabel.TabStop = true;
			this.linkLabel.Text = "https://color-of-code.de";
			this.linkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler (this.linkLabel_LinkClicked);
			// 
			// okButton
			// 
			this.okButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.okButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.okButton.Location = new System.Drawing.Point (466, 424);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size (78, 24);
			this.okButton.TabIndex = 24;
			this.okButton.Text = "&OK";
			// 
			// richTextBoxLicense
			// 
			this.tableLayoutPanel.SetColumnSpan (this.richTextBoxLicense, 2);
			this.richTextBoxLicense.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBoxLicense.Location = new System.Drawing.Point (156, 111);
			this.richTextBoxLicense.Name = "richTextBoxLicense";
			this.richTextBoxLicense.ReadOnly = true;
			this.richTextBoxLicense.Size = new System.Drawing.Size (388, 307);
			this.richTextBoxLicense.TabIndex = 27;
			this.richTextBoxLicense.Text = resources.GetString ("richTextBoxLicense.Text");
			// 
			// AboutBox
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size (565, 469);
			this.Controls.Add (this.tableLayoutPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutBox";
			this.Padding = new System.Windows.Forms.Padding (9);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "AboutBox";
			this.tableLayoutPanel.ResumeLayout (false);
			this.tableLayoutPanel.PerformLayout ();
			((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit ();
			this.ResumeLayout (false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelCompanyName;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label labelDownload;
        private System.Windows.Forms.LinkLabel linkLabel;
        private System.Windows.Forms.RichTextBox richTextBoxLicense;
    }
}
