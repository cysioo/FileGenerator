namespace FileGenerator.LineGeneration.TokenGeneration
{
    public class RandomIntGenerator : ITokenGenerator
    {
        private Random _randomNumberGenerator = new Random();

        public string Generate()
        {
            return _randomNumberGenerator.Next(0, 1000_000).ToString();
        }
    }
}
