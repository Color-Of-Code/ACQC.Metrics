using System;
using LanguageExt;
using LanguageExt.Parsec;

namespace ACQC.Metrics.Core.Parsers;

public static partial class Parsers
{
    public static Parser<Seq<Seq<string>>> JsonParser()
    {
        throw new NotImplementedException();

        // csvFile = endBy line eol
        // line = sepBy cell (char separator)
        // cell = quotedCell <|> many (noneOf ",\n\r")

        // quotedCell =
        //     do char '"'
        //        content <- many quotedChar
        //        char '"' <?> "quote at end of cell"
        //        return content

        // quotedChar =
        //         noneOf "\""
        //     <|> try (string "\"\"" >> return '"')

        // eol =   try (string "\n\r")
        //     <|> try (string "\r\n")
        //     <|> string "\n"
        //     <|> string "\r"
        //     <?> "end of line"

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

        // return from sp in spaces
        //        from ws in many1(term)
        //        select ws;
    }
}
