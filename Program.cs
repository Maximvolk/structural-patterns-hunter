using StructuralPatternsHunter;
using StructuralPatternsHunter.Analysis;
using StructuralPatternsHunter.Output;


if (args.Length != 1)
{
    Console.WriteLine("No source code directory specified");
    Environment.Exit(1);
}

var rootDirectory = args[0];
if (!Directory.Exists(rootDirectory))
{
    Console.WriteLine("Source directory does not exist");
    Environment.Exit(1);
}

var maxParallelFiles = 20;

Console.WriteLine("Parsing files...");
var parsingFacade = new ParsingFacade(maxParallelFiles);
var entities = await parsingFacade.ParseAsync(rootDirectory);

Console.WriteLine("Building entities tree...");
EntitiesTreeCreator.FillRelationships(entities);

using var outputWriter = new OutputWriter("patterns-report.md");
var analyzer = new Analyzer(outputWriter);

Console.WriteLine("Looking for patterns...");
analyzer.Analyze(entities);
