using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ACQC.Metrics {
	internal abstract class BaseParser : IParser {
		protected ResultCollector _collector;
		private FileInfo _file = null;

		public BaseParser (FileInfo inputFile)
		{
			_collector = new ResultCollector (inputFile);
			_file = inputFile;
		}

		public ResultCollector Results
		{ get { return _collector; } }

		public abstract void ParseLine (string line);

		public virtual void ParseFile ()
		{
			using (Stream fileStream = new FileStream (_file.FullName, FileMode.Open, FileAccess.Read)) {
				TextReader input = new StreamReader (fileStream);
				String line;
				while ((line = input.ReadLine ()) != null) {
					ParseLine (line);
				}
			}
		}
	}
}
