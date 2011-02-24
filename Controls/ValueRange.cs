using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACQC.Metrics.Data {
	class ValueRange {
		public ValueRange (String name, String description)
		{
			Name = name;
			Description = description;
		}

		public String Name { get; private set; }
		public String Description { get; private set; }

		public float MinPreferred { get; set; }
		public float MaxPreferred { get; set; }

		public float PreferredRange { get { return MaxPreferred - MinPreferred; } }

		public float MinRange { get; set; }
		public float MaxRange { get; set; }

		public float Range { get { return MaxRange - MinRange; } }
	}
}
