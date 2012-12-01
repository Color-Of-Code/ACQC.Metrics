using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ACQC.Metrics
{
	internal class JavaParser: BaseParser
	{

		public JavaParser(FileInfo inputFile)
			: base(inputFile)
		{
		}

		public override void ParseText(TextReader reader)
		{
			throw new NotImplementedException();
		}

	}
}
