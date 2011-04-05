using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACQC.Metrics {
	
	public interface IParser {
		void ParseLine (String line);

		void ParseFile ();

		ResultCollector Results { get; }
	}
}
