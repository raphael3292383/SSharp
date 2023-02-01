namespace SSharp.Tokens
{
    public class Keyword : Token
    {
        public Keyword(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public string Name { get; private set; }
    }
}
