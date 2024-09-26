using StructuralPatternsHunter.Entities;

namespace StructuralPatternsHunter.Reading
{
    internal struct Token(string value, int line, string file)
    {
        public string Value { get; set; } = value;
        public Location Location { get; set; } = new(file, line);

        public static bool operator ==(Token left, string right)
        {
            if (right == null)
                return false;

            return left.Value == right;
        }

        public static bool operator !=(Token left, string right)
        {
            if (right == null)
                return true;

            return left.Value != right;
        }

        public static bool operator ==(string left, Token right)
        {
            if (left == null)
                return false;

            return left == right.Value;
        }

        public static bool operator !=(string left, Token right)
        {
            if (left == null)
                return true;

            return left != right.Value;
        }

        public override bool Equals(object? obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
