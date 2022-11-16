using LanguageExt;
using LanguageExt.Parsec;
using static LanguageExt.Parsec.Char;
using static LanguageExt.Parsec.Prim;
using static LanguageExt.Prelude;

namespace ACQC.Metrics.Core.Parsers;

public static partial class LanguageParsers
{
    public static Parser<Seq<Seq<string>>> CsvParser(char separatorChar = ',')
    {
        var endOfValue = $"{separatorChar}\n\r";
        var separator = ch(separatorChar)
                        .label("separator");

        var delimiter = choice(separator, endOfLine)
                        .label("delimiter");

        var quotedChar = either(
                            noneOf("\""),
                            attempt(str("\"\"").Map(_ => '"'))
                         )
                         .label("quotedChar");

        var quotedValue = (from _b in ch('"').label("quote at begin of value")
                           from content in many(quotedChar)
                           from _e in ch('"').label("quote at end of value")
                           select content)
                           .label("quoted value");

        var unquotedValue = many(noneOf(endOfValue))
                            .label("unquoted value");

        var value = asString(either(quotedValue, unquotedValue))
            .label("value");

        var line = sepBy(value, separator)
                   .label("line");

        return sepBy(line, endOfLine)
               .label("file");
    }
}
