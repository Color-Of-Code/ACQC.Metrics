using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ACQC.Metrics {
	class Program {
		private static int DoTheJob (String inputFileName, TextWriter output)
		{
			try {
				FileInfo inputFile = new FileInfo (inputFileName);
				if (inputFile.Exists) {
					var results = Helper.Computer.Analyze (inputFile);
					if (results != null) {
						using (XmlWriter writer = CreateXmlWriter (output)) {
							WriteHeader (writer);
							WriteResults (results, writer);
							WriteFooter (writer);
						}
						return 0;
					}
				}
				return -1;
			}
			catch (ArgumentException ex) {
				// illegal chars in path are the sign that it is probaby a
				// job with wildcards.
				var substitutedArg = Environment.ExpandEnvironmentVariables (inputFileName);

				var dirPart = Path.GetDirectoryName (substitutedArg);
				if (dirPart.Length == 0)
					dirPart = ".";

				var wildcard = new Helper.Wildcard (substitutedArg);
				//var filePart = Path.GetFileName (substitutedArg);

				using (XmlWriter writer = CreateXmlWriter (output)) {
					WriteHeader (writer);
					foreach (string file in Directory.GetFiles (dirPart, "*.*", SearchOption.AllDirectories)) {
						if (wildcard.IsMatch (file)) {
							FileInfo inputFile = new FileInfo (file);
							if (inputFile.Exists) {
								var results = Helper.Computer.Analyze (inputFile);
								if (results != null) {
									WriteResults (results, writer);
								}
							} else {
								Console.WriteLine ("File not found: {0}", file);
							}

						}
					}
					WriteFooter (writer);
				}
				return 0;
			}
			catch {
				return -1;
			}
		}

		private static void WriteFooter (XmlWriter writer)
		{
			writer.WriteEndElement ();
		}

		private static void WriteHeader (XmlWriter writer)
		{
			writer.WriteStartElement ("ACQC");
			writer.WriteAttributeString ("Timestamp", DateTime.Now.ToString ("O"));
		}

		private static XmlWriter CreateXmlWriter (TextWriter output)
		{
			XmlWriterSettings settings = new XmlWriterSettings ();
			settings.Indent = true;
			settings.Encoding = Encoding.UTF8;
			XmlWriter writer = XmlWriter.Create (output, settings);
			return writer;
		}

		private static void WriteResults (ResultCollector results, XmlWriter writer)
		{
			results.WriteXml (writer);
		}

		private static void ParseParameters (String[] parameters, ref String filenameIn, ref String filenameOut)
		{
			String option = null;

			foreach (String parameter in parameters) {
				if (parameter.StartsWith ("-")) {
					option = parameter;
					if (option == "-?" || option == "-h" || option == "--help") {
						DumpHelp ();
						option = null;
					} else if (option == "-o") {
						// ok
					} else {
						OutputOptionError (ref filenameIn, ref filenameOut, option);
						return;
					}
				} else {
					if (option == "-o") {
						filenameOut = parameter;
					} else if (option == null) {
						filenameIn = parameter;
					} else {
						OutputOptionError (ref filenameIn, ref filenameOut, option);
						return;
					}
					option = null;
				}
			}
		}

		private static void OutputOptionError (ref String filenameIn, ref String filenameOut, String option)
		{
			Console.Error.WriteLine ("Unknown command line option {0}\n", option);
			DumpHelp ();
			filenameIn = null;
			filenameOut = null;
		}

		private static void DumpHelp ()
		{
			Console.Out.WriteLine ("Usage:");
			Console.Out.WriteLine ("{0} [- options] <c or c++ files>", "metrics");
			Console.Out.WriteLine ("{0} [- options] <directory>", "metrics");
			Console.Out.WriteLine (" -o <output xml file>");
			Console.Out.WriteLine (" files can be specified using wildcards (e.g: dir\\*.cpp)");
			Console.Out.WriteLine ("The program dumps the results out to the console or into a specified XML file.");
		}

		//debug arguments: -o metrics.xml Reference.cpp
		[STAThread]
		public static Int32 Main (String[] parameters)
		{
			//Regex rex = new Regex(@"^(([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*"+
			//    @"@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9});)*$");
			//String emails = "aa@bb.com;cc@dfs.com;asdf@fasdf.com;sdfsdf@fsaf.com;";
			//Boolean ismatch1 = rex.IsMatch(emails);
			//Match match1 = rex.Match(emails);
			//Boolean ismatch2 = rex.IsMatch("aa@bb.com;cc@dfs.com;asdf@fasdf.com; sdfsdf@fsaf.com;");
			//Match match2 = rex.Match("aa@bb.com;cc@dfs.com;asdf@fasdf.com; sdfsdf@fsaf.com;");
			//foreach (Capture c in match2.Captures)
			//{
			//    Console.WriteLine(c.Value);
			//}
			//Regex rex3 = new Regex(@"^(([\d\w]([-\.\w\d])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9});)*$");
			//Match match3 = rex.Match("aa@bb.com;cc@dfs.com;asdf@fasdf.com;sdfsdf@fsaf.com;");
			//return 0;

			if (parameters.Length < 1) {
				return RunGuiTool ();
			} else {
				return RunClTool (parameters);
			}
		}

		private static int RunClTool (String[] parameters)
		{
			String filenameIn = null;
			String filenameOut = null;
			ParseParameters (parameters, ref filenameIn, ref filenameOut);
			if (filenameIn != null) {
				if (filenameOut == null) {
					DoTheJob (filenameIn, Console.Out);
				} else {
					TextWriter writer = new StreamWriter (filenameOut, false);
					using (writer) {
						DoTheJob (filenameIn, writer);
					}
				}
			}
			return 0;
		}

		private static int RunGuiTool ()
		{
			Application.EnableVisualStyles ();
			Application.SetCompatibleTextRenderingDefault (false);
			Application.Run (new MainForm ());
			return -1;
		}

	}
}
