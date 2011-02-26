using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

using Microsoft.Win32;

namespace ACQC.Metrics {
	public partial class MainForm : Form {
		private ListViewGroup listViewGroupFunctions = new ListViewGroup ("Functions", HorizontalAlignment.Left);
		private ListViewGroup listViewGroupFiles = new ListViewGroup ("Files", HorizontalAlignment.Left);
		private ListViewGroup listViewGroupSum = new ListViewGroup ("Summary", HorizontalAlignment.Left);
		private ListViewColumnSorter lvwColumnSorter = new ListViewColumnSorter ();

		public MainForm ()
		{
			InitializeComponent ();

			listView.ListViewItemSorter = lvwColumnSorter;
			listView.DragDrop += new DragEventHandler (MainForm_DragDrop);
			listView.DragEnter += new DragEventHandler (MainForm_DragEnter);

			listView.Groups.Add (listViewGroupFunctions);
			listView.Groups.Add (listViewGroupFiles);
			listView.Groups.Add (listViewGroupSum);

			_kiviat.Model = _model;
			AddEditors ();
		}

		private void buttonClear_Click (object sender, EventArgs e)
		{
			listView.Items.Clear ();
			hiddenFiles.Clear ();
			hiddenFunctions.Clear ();
			_model.Elements.Clear ();
			_kiviat.Redraw ();
		}

		private void MainForm_DragDrop (object sender, DragEventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			listView.BeginUpdate ();

			string[] files = (string[])e.Data.GetData (DataFormats.FileDrop);

			try {
				foreach (string file in files) {
					if (Directory.Exists (file))
						AddDirectory (file);
					else
						AddFile (new FileInfo (file));
				}
			}
			catch (Exception ex) {
				MessageBox.Show (ex.Message);
			}

			UpdateSummary ();
			columnHeader1.AutoResize (ColumnHeaderAutoResizeStyle.ColumnContent);
			columnHeader10.AutoResize (ColumnHeaderAutoResizeStyle.ColumnContent);

			listView.EndUpdate ();
			Cursor.Current = Cursors.Default;
		}

		private void UpdateSummary ()
		{
			List<ListViewItem> items = new List<ListViewItem> ();
			items.AddRange (listViewGroupSum.Items.OfType<ListViewItem> ());
			foreach (ListViewItem item in items) {
				listView.Items.Remove (item);
			}

			if (listView.ShowGroups) {
				Data.Metrics sum = new Data.Metrics (String.Empty, "Sum");
				foreach (ListViewItem item in listViewGroupFiles.Items) {
					sum += item.Tag as Data.Metrics;
				}
				AddItem (sum, listViewGroupSum);

				Data.Metrics mean = new Data.Metrics (sum);
				mean.Name = "Mean";
				if (listViewGroupFiles.Items.Count > 0)
					mean /= listViewGroupFiles.Items.Count;
				AddItem (mean, listViewGroupSum);
			}
		}

		private void MainForm_DragEnter (object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent (DataFormats.FileDrop, false))
				e.Effect = DragDropEffects.All;
		}


		private void AddDirectory (String directoryName)
		{
			DirectoryInfo di = new DirectoryInfo (directoryName);
			if ((di.Attributes & FileAttributes.Hidden) != 0)
				return;
			foreach (DirectoryInfo directory in di.GetDirectories ()) {
				AddDirectory (directory.FullName);
			}
			foreach (FileInfo file in di.GetFiles ()) {
				AddFile (file);
			}
		}

		private void AddFile (FileInfo file)
		{
			ResultCollector results = Helper.Computer.Analyze (file);
			if (results != null) {
				Data.Metrics metrics = results.FileMetrics;
				AddItem (metrics, listViewGroupFiles);
				foreach (Data.Metrics fmetrics in results.FunctionMetrics) {
					AddItem (fmetrics, listViewGroupFunctions);
				}
			}
		}

		private void AddItem (Data.Metrics metrics, ListViewGroup group)
		{
			ListViewItem item = new ListViewItem ();
			item.Tag = metrics;
			item.Group = group;
			item.Text = metrics.Filename;
			item.SubItems.Add (metrics.Name);
			item.SubItems.Add (metrics.LINES.ToString ());
			item.SubItems.Add (metrics.LLOC.ToString ());
			item.SubItems.Add (metrics.LLOCi.ToString ());
			item.SubItems.Add (metrics.LLOW.ToString ());
			item.SubItems.Add (metrics.CC.ToString ());
			item.SubItems.Add (metrics.DC.ToString ());
			item.SubItems.Add (metrics.PROCS.ToString ());
			item.SubItems.Add (metrics.CARGS.ToString ());

			if (Data.MetricBounds.IsOutOfBound (metrics))
				item.ForeColor = Color.Red;

			listView.Items.Add (item);
		}

		private void DeleteSelectedItems ()
		{
			foreach (ListViewItem item in listView.SelectedItems) {
				if (item.Group != listViewGroupFiles) {
					MessageBox.Show ("Only files (not single functions) can be removed at the moment");
					return;
				}
			}

			Cursor.Current = Cursors.WaitCursor;
			listView.BeginUpdate ();

			List<ListViewItem> toremove = new List<ListViewItem> ();
			foreach (ListViewItem item in listView.SelectedItems) {
				toremove.Add (item);
				foreach (ListViewItem fitem in listViewGroupFunctions.Items) {
					if (fitem.Text == item.Text)
						toremove.Add (fitem);
				}
			}

			foreach (ListViewItem item in toremove)
				listView.Items.Remove (item);

			UpdateSummary ();
			columnHeader1.AutoResize (ColumnHeaderAutoResizeStyle.ColumnContent);
			columnHeader10.AutoResize (ColumnHeaderAutoResizeStyle.ColumnContent);

			listView.EndUpdate ();
			Cursor.Current = Cursors.Default;
		}

		private void CopyToClipboard ()
		{
			String text = "Functions\tName\tLINES\tLLOC\tLLOCi\tLLOW\tCC\tDC\tPROCS\tCARGS\r\n";
			foreach (ListViewItem item in listViewGroupFunctions.Items) {
				Data.Metrics m = item.Tag as Data.Metrics;
				text += TextFromMetrics (m);
			}
			text += "Files\tName\tLINES\tLLOC\tLLOCi\tLLOW\tCC\tDC\tPROCS\tCARGS\r\n";
			foreach (ListViewItem item in listViewGroupFiles.Items) {
				Data.Metrics m = item.Tag as Data.Metrics;
				text += TextFromMetrics (m);
			}
			text += "Summary\t-\t-\t-\t-\t-\t-\t-\t-\t-\r\n";
			foreach (ListViewItem item in listViewGroupSum.Items) {
				Data.Metrics m = item.Tag as Data.Metrics;
				text += TextFromMetrics (m);
			}

			Clipboard.SetText (text);
		}

		private static string TextFromMetrics (Data.Metrics m)
		{
			return String.Format ("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\r\n",
								m.Filename, m.Name, m.LINES, m.LLOC, m.LLOCi, m.LLOW, m.CC, m.DC, m.PROCS, m.CARGS);
		}

		private void listView_KeyDown (object sender, KeyEventArgs e)
		{
			switch ((Keys)e.KeyValue) {
			case Keys.Delete:
				DeleteSelectedItems ();
				break;
			case Keys.C:
				if ((e.Modifiers & Keys.Control) != 0)
					CopyToClipboard ();
				break;
			case Keys.F1:
				buttonAbout_Click (null, null);
				break;
			}
		}

		private void buttonAbout_Click (object sender, EventArgs e)
		{
			AboutBox about = new AboutBox ();
			about.ShowDialog (this);
		}

		private void listView_ColumnClick (object sender, ColumnClickEventArgs e)
		{
			// Determine if clicked column is already the column that is being sorted.
			if (e.Column == lvwColumnSorter.SortColumn) {
				// Reverse the current sort direction for this column.
				if (lvwColumnSorter.Order == SortOrder.Ascending) {
					lvwColumnSorter.Order = SortOrder.Descending;
				} else {
					lvwColumnSorter.Order = SortOrder.Ascending;
				}
			} else {
				// Set the column number that is to be sorted; default to ascending.
				lvwColumnSorter.SortColumn = e.Column;
				lvwColumnSorter.Order = SortOrder.Ascending;
			}
			listView.ShowGroups = false;
			UpdateSummary ();
			listView.Sort ();
		}

		private void buttonShowGroups_Click (object sender, EventArgs e)
		{
			listView.ShowGroups = true;
			UpdateSummary ();
		}

		private List<ListViewItem> hiddenFiles = new List<ListViewItem> ();
		private List<ListViewItem> hiddenFunctions = new List<ListViewItem> ();

		private void checkBoxShowFiles_CheckedChanged (object sender, EventArgs e)
		{
			if (checkBoxShowFiles.Checked) {
				foreach (ListViewItem item in hiddenFiles) {
					listView.Items.Add (item);
					listViewGroupFiles.Items.Add (item);
				}
				hiddenFiles.Clear ();
			} else {
				foreach (ListViewItem item in listViewGroupFiles.Items)
					hiddenFiles.Add (item);
				foreach (ListViewItem item in hiddenFiles)
					listView.Items.Remove (item);
			}
		}

		private void checkBoxShowFunctions_CheckedChanged (object sender, EventArgs e)
		{
			if (checkBoxShowFunctions.Checked) {
				foreach (ListViewItem item in hiddenFunctions) {
					listView.Items.Add (item);
					listViewGroupFunctions.Items.Add (item);
				}
				hiddenFunctions.Clear ();
			} else {
				foreach (ListViewItem item in listViewGroupFunctions.Items)
					hiddenFunctions.Add (item);
				foreach (ListViewItem item in hiddenFunctions)
					listView.Items.Remove (item);
			}
		}

		private void listView_DoubleClick (object sender, EventArgs e)
		{
			foreach (ListViewItem item in listView.SelectedItems) {
				Data.Metrics metrics = item.Tag as Data.Metrics;
				if (metrics != null)
					OpenSourceFile (metrics.Filename, metrics.Position);
			}
		}

		private void OpenSourceFile (String filename, int line)
		{
			// try to detect notepad++
			String editorName = comboBoxEditor.SelectedItem.ToString ();
			switch (editorName) {
			case EDITOR_NOTEPAD:
				FileInfo f = GetNotepadPlusPlusPath ();
				if (f != null && f.Exists) {
					ProcessStartInfo psi = new ProcessStartInfo ();
					psi.FileName = f.FullName;
					psi.Arguments = String.Format ("-n{1} \"{0}\"", filename, line);

					Process notePad = new Process ();
					notePad.StartInfo = psi;
					notePad.Start ();
				}
				break;
			default:
				Process.Start (filename);
				break;
			}

			//<Name>Ultra Edit</Name>
			//<ExecName>uedit32.exe</ExecName>
			//<Format>&quot;%e&quot; &quot;%f/%l/%c&quot;</Format>

			//<Name>Visual Slick Edit</Name>
			//<ExecName>vs.exe</ExecName>
			//<Format>&quot;%e&quot; &quot;%f&quot; &quot;-#goto-line %l&quot; &quot;-#goto-col %c&quot;</Format>

			//<Name>TextPad</Name>
			//<ExecName>textpad.exe</ExecName>
			//<Format>&quot;%e&quot; &quot;%f&quot;(%l)</Format>

			//<Name>MS Developer Studio 6</Name>
			//<ExecName>msdev.exe</ExecName>
			//<Format>&quot;%e&quot; &quot;%f&quot;</Format>

			//<Name>MS Visual Embedded C++ 4</Name>
			//<ExecName>evc.exe</ExecName>
			//<Format>&quot;%e&quot; &quot;%f&quot;</Format>

			//<Name>MS Developer Studio .NET 2005</Name>
			//<ExecName>devenv.exe</ExecName>
			//<Format>&quot;%e&quot; /Edit &quot;%f&quot;</Format>

			//<Name>Crimson Editor</Name>
			//<ExecName>cedt.exe</ExecName>
			//<Format>&quot;%e&quot; &quot;%f&quot; /L:%l</Format>

			//<Name>SciTE</Name>
			//<ExecName>SciTE.exe</ExecName>
			//<Format>&quot;%e&quot; &quot;%f&quot; -goto:%l,%c</Format>
		}

		private static FileInfo GetNotepadPlusPlusPath ()
		{
			RegistryKey hklm = Registry.LocalMachine;
			hklm = hklm.OpenSubKey (@"SOFTWARE\Wow6432Node\Notepad++");
			if (hklm == null)
				hklm = hklm.OpenSubKey (@"SOFTWARE\Notepad++");
			if (hklm != null) {
				String path = hklm.GetValue (String.Empty, String.Empty) as String;
				return new FileInfo (Path.Combine (path, "Notepad++.exe"));
			}
			return null;
		}

		private const String EDITOR_DEFAULT = "(Default handler)";
		private const String EDITOR_NOTEPAD = "Notepad++";
		private const String EDITOR_REQUEST = "Request support for a new editor...";

		private void AddEditors ()
		{
			comboBoxEditor.Items.Add (EDITOR_DEFAULT);
			FileInfo f = GetNotepadPlusPlusPath ();
			if (f != null && f.Exists) {
				comboBoxEditor.Items.Add (EDITOR_NOTEPAD);
			}

			Settings.Default.Upgrade ();
			String editor = Settings.Default.Editor;
			if (String.IsNullOrEmpty (editor))
				editor = EDITOR_DEFAULT;
			comboBoxEditor.SelectedItem = editor;

			comboBoxEditor.Items.Add ("Request support for a new editor...");
		}

		private void comboBoxEditor_SelectedIndexChanged (object sender, EventArgs e)
		{
			String editorName = comboBoxEditor.SelectedItem.ToString ();
			if (editorName == EDITOR_REQUEST) {
				Process.Start ("mailto:jaap.dehaan@color-of-code.de?" +
					"subject=ACQC.Metrics, please add support for editor <NAME>&" +
					"body=This is an Email template. Feel free to edit or delete text as appropriate.%0a%0d%0a%0d" +
					"Editor <NAME> doesn't seem to be supported yet. Please add support for it.%0a%0d" +
					"Please notify me per EMail as soon as the version containing the support for that editor is available.%0a%0d");
				comboBoxEditor.SelectedItem = EDITOR_DEFAULT;
			} else {
				Settings.Default.Editor = editorName;
				Settings.Default.Save ();
			}
		}

		private Controls.KiviatForm _kiviat = new Controls.KiviatForm ();
		private Data.IKiviatModel _model = new Data.MetricsKiviatModel ();

		private void checkBoxShowKiviat_CheckedChanged (object sender, EventArgs e)
		{
			if (checkBoxShowKiviat.Checked)
				_kiviat.Show (this);
			else
				_kiviat.Hide ();
		}

		private void listView_SelectedIndexChanged (object sender, EventArgs e)
		{
			_model.Elements.Clear ();
			foreach (ListViewItem item in listView.SelectedItems) {
				Data.Metrics metric = item.Tag as Data.Metrics;
				_model.Elements.Add (metric);
			}
			_kiviat.Redraw ();
		}
	}
}
