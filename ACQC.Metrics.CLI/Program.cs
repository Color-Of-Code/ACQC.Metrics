using System;
using System.IO;
using ACQC.Metrics.Core.Parsers;

var testString = "4,\"quoted,\"\" with comma\",words\nj, are here\n\"sss\"\r";
var testFile = "analysis_results.csv";

var start = DateTime.UtcNow;
Console.WriteLine($"Start: {start}");
testString = File.ReadAllText(testFile);
var r = Parsers.CsvParser().Parse(testString); //("{ test: \"test\" }");

var end = DateTime.UtcNow;
Console.WriteLine($"End: {end} Elapsed: {end-start}");

Console.WriteLine($"IsFaulted: {r.IsFaulted}");
Console.WriteLine($"Error:     {(r.IsFaulted ? r.Reply.Error : string.Empty)}");
Console.WriteLine($"Result:     {r.Reply.Result}");

