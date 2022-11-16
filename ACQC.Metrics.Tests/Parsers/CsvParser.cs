using Xunit;

namespace ACQC.Metrics.Tests.Parsers;

using static ACQC.Metrics.Core.Parsers.LanguageParsers;
using LanguageExt;

public class CsvParserTests
{
    [Fact]
    public void Parse_EmptyString_ResultOk()
    {
        var result = CsvParser().Parse("");
        Assert.False(result.IsFaulted);
        Assert.Equal(Seq.create(Seq.create("")), result.Reply.Result);
    }

    [Fact]
    public void Parse_QuotedString_ResultOk()
    {
        var result = CsvParser().Parse("\"quo,ted\"");
        Assert.False(result.IsFaulted);
        Assert.Equal(Seq.create(Seq.create("quo,ted")), result.Reply.Result);
    }

    [Theory]
    [InlineData("\"a")] // quoted value, missing end quote
    public void IsFaulted_InvalidFormat_IsTrue(string csv)
    {
        var result = CsvParser().Parse(csv);
        Assert.True(result.IsFaulted);
    }
}
