<Query Kind="Statements" />

var sample = Util.GetSamples().FirstOrDefault (s => s.Location.EndsWith ("\\LINQPad Controls"));

if (sample == null)
	"You need to run this query with a later version of LINQPad".Dump();
else
	sample.OpenLink.Dump ("Click me for LINQPad control samples");