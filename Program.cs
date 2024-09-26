using StructuralPatternsHunter;
using StructuralPatternsHunter.Analysis;
using StructuralPatternsHunter.Output;


var rootDirectory = "/Users/maximvolk/Source/design-patterns-csharp/Bridge.Conceptual";
var outputPath = "./test.txt";
var maxParallelFiles = 20;

var parsingFacade = new ParsingFacade(maxParallelFiles);
var entities = await parsingFacade.ParseAsync(rootDirectory);

var treeCreator = new EntitiesTreeCreator();
treeCreator.FillRelationships(entities);

var outputWriter = new OutputWriter(outputPath);
var analyzer = new Analyzer(outputWriter);

analyzer.Analyze(entities);
