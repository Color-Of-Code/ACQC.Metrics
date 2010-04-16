using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACQC.Metrics
{
    public interface IParser
    {
        void ParseLine(String line);

        ResultCollector Results { get; }
    }
}
