using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACQC.Metrics.Data
{

    internal static class MetricBounds
    {

        #region Limits
        public const int CARGS_MIN = 0;
        public const int CARGS_MAX = 7;

        public const int LLOC_MIN = 0;
        public const int LLOC_MAX = 200;

        public const int LLOCi_MIN = 3;
        public const int LLOCi_MAX = 2000;

        public const int CC_MIN = 1;
        public const int CC_MAX = 15;

        public const int DC_MIN = 0;
        public const int DC_MAX = 30;

        public const int LINES_MIN = 0;
        public const int LINES_MAX = 300;
        #endregion

        public static Boolean IsOutOfBound(Metrics m)
        {
            if (m.CARGS > CARGS_MAX) return true;
            if (m.LLOC > LLOC_MAX) return true;
            if (m.CC > CC_MAX) return true;
            if (m.DC > DC_MAX) return true;
            if (m.LINES > LINES_MAX) return true;
            return false;
        }
    }
}
