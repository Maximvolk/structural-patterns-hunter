using StructuralPatternsHunter;
using StructuralPatternsHunter.Analysis;
using StructuralPatternsHunter.Output;


var rootDirectory = "/Projects/test/";
var outputPath = "/Projects/test.txt";
var maxParallelFiles = 5;

var parsingFacade = new ParsingFacade(maxParallelFiles);
var nodes = await parsingFacade.ParseAsync(rootDirectory);

var outputWriter = new OutputWriter(outputPath);
var analyzer = new Analyzer(outputWriter);

foreach (var node in nodes)
    analyzer.Analyze(node);
