using System;
using LanguageExt;
using LanguageExt.Parsec;
using static LanguageExt.Parsec.Char;
using static LanguageExt.Parsec.Prim;
using static LanguageExt.Parsec.Token;
using static LanguageExt.Prelude;

namespace ACQC.Metrics.Core.Parsers;

public static partial class LanguageParsers
{
    public static Parser<Seq<Seq<string>>> RubyParser()
    {
        throw new NotImplementedException();
        // var token = makeTokenParser(GenLanguageDef.Empty.With(
        //         CommentStart: "/*",
        //         CommentEnd: "*/",
        //         CommentLine: "//",
        //         NestedComments: true,
        //         IdentStart: letter,
        //         IdentLetter: either(alphaNum, oneOf("_'")),
        //         OpStart: oneOf(@"!%&*+.<=>?@\^|-~"),
        //         OpLetter: oneOf(@"!%&*+.<=>?@\^|-~"),
        //         ReservedOpNames: List<string>(),
        //         ReservedNames: List<string>(),
        //         CaseSensitive: true
        //         ));
    }
}
