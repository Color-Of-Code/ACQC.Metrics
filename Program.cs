using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ACQC.Metrics {
	class Program {
		private static int DoTheJob (String inputFileName, TextWriter output)
		{
			using (Stream fileStream = new FileStream (inputFileName, FileMode.Open, FileAccess.Read)) {
				TextReader input = new StreamReader (fileStream);
				CppParser parser = new CppParser (input, new FileInfo (inputFileName));

				String line;
				while ((line = input.ReadLine ()) != null) {
					parser.ParseLine (line);
				}

				parser.WriteResults (output);
			}
			return 0;
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
					}
				} else {
					if (option == "-o") {
						filenameOut = parameter;
					} else if (option == null) {
						filenameIn = parameter;
					} else {
						Console.Error.WriteLine ("Unknown command line option {0}", option);
						DumpHelp ();
					}
					option = null;
				}
			}
		}

		private static void DumpHelp ()
		{
			Console.Out.WriteLine ("Usage:");
			Console.Out.WriteLine ("{0} [- options] <c or c++ file>", "metrics");
			Console.Out.WriteLine ("{0} [- options] <directory>", "metrics");
			Console.Out.WriteLine (" -o <output xml file>");
			Console.Out.WriteLine ("The program dumps the results out to the console or given file.");
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
