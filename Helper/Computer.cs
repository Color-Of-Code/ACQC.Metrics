using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ACQC.Metrics.Helper {
	internal class Computer {
		static internal ResultCollector Analyze (FileInfo file)
		{
			String extension = file.Extension.ToLowerInvariant ();

			IParser parser = null;
			switch (extension) {
			case ".h":
			case ".hh":
			case ".h++":
			case ".hpp":
			case ".c":
			case ".cc":
			case ".c++":
			case ".cpp":
				parser = new CppParser (file);
				break;

			case ".java":
				parser = new JavaParser (file);
				break;

			case ".cs":
				parser = new CsharpParser (file);
				break;

			default:
				return null;
			}
			return ParseInput (parser);
		}

		private static ResultCollector ParseInput (IParser parser)
		{
			parser.ParseFile ();
			return parser.Results;
		}
	}
}
