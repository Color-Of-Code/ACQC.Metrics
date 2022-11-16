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
        var separator = ch(separatorChar)
                        .label("separator");

        var eol = (from rn in oneOf("\n\r")
                   from _b in attempt(ch(rn == '\n' ? '\r' : '\n'))
                   select '\n')
                   .label("eol");

        var delimiter = choice(separator, eol, eof.Map(_ => '\n'))
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
                           .label("quotedValue");

        var value = asString(either(
            quotedValue,
            manyUntil(anyChar, lookAhead(delimiter))
            ))
            .label("value");

        var line = sepBy(value, separator)
                   .label("line");

        return sepBy(line, eol)
               .label("file");
    }
}
