using System;
using System.IO;

using static ACQC.Metrics.Core.Parsers.LanguageParsers;

var testString = "4,\"quoted,\"\" with comma\",words\nj, are here\n\"sss\"\r";

var start = DateTime.UtcNow;
Console.WriteLine($"Start: {start}");
testString = File.ReadAllText(testString);
var r = CsvParser().Parse(testString); //("{ test: \"test\" }");

var end = DateTime.UtcNow;
Console.WriteLine($"End: {end} Elapsed: {end-start}");

Console.WriteLine($"IsFaulted: {r.IsFaulted}");
Console.WriteLine($"Error:     {(r.IsFaulted ? r.Reply.Error : string.Empty)}");
Console.WriteLine($"Result:     {r.Reply.Result}");

