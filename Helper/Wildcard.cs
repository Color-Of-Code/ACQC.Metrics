using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ACQC.Metrics.Helper {
	internal class Wildcard {
		public Wildcard (String wildcard)
		{
			_rex = new Regex (
				 "^" + Regex.Escape (wildcard).Replace (@"\*", ".*").Replace (@"\?", ".") + "$",
				 RegexOptions.IgnoreCase | RegexOptions.Singleline
			 );
		}

		public bool IsMatch (String str)
		{
			return _rex.IsMatch (str);
		}

		private Regex _rex;
	}
}
