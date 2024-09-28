using StructuralPatternsHunter;
using StructuralPatternsHunter.Analysis;
using StructuralPatternsHunter.Output;


var rootDirectory = "/Projects/Applications.Fg";
var outputPath = "./test.txt";
var maxParallelFiles = 20;

Console.WriteLine("Parsing files...");
var parsingFacade = new ParsingFacade(maxParallelFiles);
var entities = await parsingFacade.ParseAsync(rootDirectory);

Console.WriteLine("Building entities tree...");
var treeCreator = new EntitiesTreeCreator();
treeCreator.FillRelationships(entities);

using var outputWriter = new OutputWriter(outputPath);
var analyzer = new Analyzer(outputWriter);

Console.WriteLine("Looking for patterns...");
analyzer.Analyze(entities);
