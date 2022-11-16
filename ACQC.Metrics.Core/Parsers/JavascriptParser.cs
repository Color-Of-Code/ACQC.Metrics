using ACQC.Metrics.Core.Models;
using LanguageExt.Parsec;
using static LanguageExt.Parsec.Char;
using static LanguageExt.Parsec.Prim;
using static LanguageExt.Prelude;

namespace ACQC.Metrics.Core.Parsers;

public static partial class LanguageParsers
{
    public static Parser<FileMetadata> JavascriptParser()
    {
        throw new System.NotImplementedException();
        // either  comment | whitespace | code (string literal?)
        // comment = // -> eol
        //         | /* -> */
        // whitespace = while isWhitespace
        // code       = otherwise
        // var spaces = either(
        //         eof,
        //         many1(satisfy(char.IsWhiteSpace)).Map(_ => unit)
        //     );

        // Parser<A> token<A>(Parser<A> parserA) =>
        //     from res in parserA
        //     from spc in spaces
        //     select res;

        // var word = from w in token(asString(many1(letter)))
        //            select new Word(w) as Term;

        // var number = from d in token(asString(many1(digit)))
        //              select new Number(int.Parse(d)) as Term;

        // var term = either(word, number);

        // var parser = from sp in spaces
        //              from ws in many1(term)
        //              select ws;

        // return parser;
    }

    internal abstract class Term { }

    internal class Word : Term
    {
        public readonly string Value;
        public Word(string value) => Value = value;
    }

    internal class Number : Term
    {
        public readonly int Value;
        public Number(int value) => Value = value;
    }
}
