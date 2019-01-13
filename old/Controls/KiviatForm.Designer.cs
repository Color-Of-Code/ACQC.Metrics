namespace ACQC.Metrics.Controls {
	partial class KiviatForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager (typeof (KiviatForm));
			this.pictureBox = new System.Windows.Forms.PictureBox ();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit ();
			this.SuspendLayout ();
			// 
			// pictureBox
			// 
			this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox.Location = new System.Drawing.Point (0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size (434, 349);
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler (this.pictureBox_Paint);
			// 
			// KiviatForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size (434, 349);
			this.Controls.Add (this.pictureBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject ("$this.Icon")));
			this.Name = "KiviatForm";
			this.Text = "Kiviat Diagram";
			this.ResizeEnd += new System.EventHandler (this.KiviatForm_ResizeEnd);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit ();
			this.ResumeLayout (false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox;
	}
}