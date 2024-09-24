using StructuralPatternsHunter.AST;
using StructuralPatternsHunter.Output;

namespace StructuralPatternsHunter.Analysis
{
    internal class Analyzer(OutputWriter outputWriter)
    {
        private readonly OutputWriter _outputWriter = outputWriter;

        public void Analyze(ASTNode node)
        {

        }
    }
}
