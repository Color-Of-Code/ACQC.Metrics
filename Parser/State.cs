using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACQC.Metrics.Parser {
	enum State {
		Initial,
		Litteral,
		Comment,
		Namespace,
		Class,
		Method,
		Statement
	}
}
