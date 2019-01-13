using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACQC.Metrics.Data {
	class MetricsKiviatModel : IKiviatModel {
		public MetricsKiviatModel ()
		{
			Axes = new List<ValueRange>();
			ValueRange mCARGS = new ValueRange("CARGS", "Parameters");
			mCARGS.MinRange = MetricBounds.CARGS_MIN;
			mCARGS.MaxRange = MetricBounds.CARGS_MAX;
			mCARGS.MinPreferred = MetricBounds.CARGS_MIN;
			mCARGS.MaxPreferred = MetricBounds.CARGS_MAX;

			ValueRange mLLOC = new ValueRange("LLOC", "Lines of Code");
			mLLOC.MinRange = MetricBounds.LLOC_MIN;
			mLLOC.MaxRange = MetricBounds.LLOC_MAX;
			mLLOC.MinPreferred = MetricBounds.LLOC_MIN;
			mLLOC.MaxPreferred = MetricBounds.LLOC_MAX;

			ValueRange mLLOCi = new ValueRange("LLOCi", "Lines of Comments");
			mLLOCi.MinRange = MetricBounds.LLOCi_MIN;
			mLLOCi.MaxRange = MetricBounds.LLOCi_MAX;
			mLLOCi.MinPreferred = MetricBounds.LLOCi_MIN;
			mLLOCi.MaxPreferred = MetricBounds.LLOCi_MAX;

			ValueRange mCC = new ValueRange("CC", "Complexity");
			mCC.MinRange = MetricBounds.CC_MIN;
			mCC.MaxRange = MetricBounds.CC_MAX;
			mCC.MinPreferred = MetricBounds.CC_MIN;
			mCC.MaxPreferred = MetricBounds.CC_MAX;

			ValueRange mDC = new ValueRange("DC", "Depth Complexity");
			mDC.MinRange = MetricBounds.DC_MIN;
			mDC.MaxRange = MetricBounds.DC_MAX;
			mDC.MinPreferred = MetricBounds.DC_MIN;
			mDC.MaxPreferred = MetricBounds.DC_MAX;

			Axes.Add(mCARGS);
			Axes.Add(mLLOC);
			Axes.Add(mLLOCi);
			Axes.Add(mCC);
			Axes.Add(mDC);

			Elements = new List<IKiviatElement>();
		}

		public string Title
		{
			get; set;
		}

		public IList<ValueRange> Axes
		{
			get; private set;
		}

		public IList<IKiviatElement> Elements
		{
			get; private set;
		}

		public IKiviatElement Average
		{
			get {
				throw new NotImplementedException ();
			}
		}

		public IKiviatElement Minimum
		{
			get { throw new NotImplementedException (); }
		}

		public IKiviatElement Maximum
		{
			get { throw new NotImplementedException (); }
		}

		public bool IsEmpty
		{
			get { return Axes.Count == 0 || Elements.Count == 0; }
		}
	}
}
