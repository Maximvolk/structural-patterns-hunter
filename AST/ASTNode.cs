namespace StructuralPatternsHunter.AST
{
    internal struct ASTNode
    {
        public NodeType Type { get; set; }
        public IEnumerable<ASTNode> Parents { get; set; }
    }
}
