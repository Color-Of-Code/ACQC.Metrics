using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACQC.Metrics.Data {
	interface IKiviatModel {

		String Title { get; }

		IList<ValueRange> Axes { get; }

		IList<IKiviatElement> Elements { get; }

		IKiviatElement Average { get; }
		IKiviatElement Minimum { get; }
		IKiviatElement Maximum { get; }

		bool IsEmpty { get; }

	}
}
