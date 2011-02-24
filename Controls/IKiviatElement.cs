using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACQC.Metrics.Data {

	interface IKiviatElement {
		String Name { get; }
		float GetValue (String name);
	}
}
