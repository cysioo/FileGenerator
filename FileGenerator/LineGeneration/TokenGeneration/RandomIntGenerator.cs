namespace FileGenerator.LineGeneration.TokenGeneration
{
    public class RandomIntGenerator : ITokenGenerator
    {
        private Random _randomNumberGenerator = new Random();
        private int _minNumber = 0;
        private int _maxNumber = int.MaxValue - 1;

        public RandomIntGenerator(string[] parameters)
        {
            if (parameters != null)
            {
                if (parameters.Length > 0)
                {
                    if (!int.TryParse(parameters[0], out _minNumber))
                    {
                        throw new ArgumentException($"The 'minValue' parameter for randomInt must be a number (but {parameters[0]} was found).");
                    }
                }

                if (parameters.Length > 1)
                {
                    if (!int.TryParse(parameters[1], out _maxNumber))
                    {
                        throw new ArgumentException($"The 'maxValue' parameter for randomInt must be a number (but {parameters[1]} was found).");
                    }
                }

                if (_minNumber >= _maxNumber)
                {
                    throw new ArgumentException($"The 'minValue' parameter for randomInt must be lower then 'maxValue'.");
                }
            }
        }

        public string Generate()
        {
            return _randomNumberGenerator.Next(_minNumber, _maxNumber + 1).ToString();
        }
    }
}
