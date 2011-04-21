using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ACQC.Metrics {
	
	public interface IParser {
		void ParseText (TextReader reader);

		void ParseFile ();

		ResultCollector Results { get; }
	}
}
