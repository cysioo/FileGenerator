namespace FileGenerator.LineGeneration.TokenGeneration
{
    public class StaticText : ITokenGenerator
    {
        private readonly string _text;

        public StaticText(string text)
        {
            _text = text;
        }

        public string Generate()
        {
            return _text;
        }
    }
}
