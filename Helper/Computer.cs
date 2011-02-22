using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ACQC.Metrics.Helper
{
    internal class Computer
    {
        static internal ResultCollector Analyze(FileInfo file)
        {
            String extension = file.Extension.ToLowerInvariant();
            switch (extension)
            {
                case ".h":
                case ".hh":
                case ".h++":
                case ".hpp":
                case ".c":
                case ".cc":
                case ".c++":
                case ".cpp":
                    using (Stream fileStream = new FileStream(file.FullName, FileMode.Open))
                    {
                        TextReader input = new StreamReader(fileStream);
                        IParser parser = new CppParser(input, file);

                        String line;
                        while ((line = input.ReadLine()) != null)
                        {
                            parser.ParseLine(line);
                        }
                        return parser.Results;
                    }
            }
            return null;
        }
    }
}
