namespace FileGenerator.LineGeneration.TokenGeneration
{
    public class SequenceGenerator : ITokenGenerator
    {
        private long _currentSequenceNumber = 0;

        public string Generate()
        {
            return _currentSequenceNumber++.ToString();
        }
    }
}
