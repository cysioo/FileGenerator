using System.Text;

namespace FileGenerator.LineGeneration.TokenGeneration
{
    public class WordsGenerator : ITokenGenerator
    {
        private const int RememberedStringsLimit = 1000;
        private readonly IAppSettings _appSettings;
        private IList<string> _rememberedStrings = new List<string>();
        private Random _randomNumberGenerator = new Random();
        private readonly string[] _words;
        private int _minNumberOfWords = 1;
        private int _maxNumberOfWords = 10;

        public WordsGenerator(IAppSettings appSettings, IFileService fileService, string[] parameters)
        {
            _appSettings = appSettings;
            _words = fileService.Words;

            if (parameters != null)
            {
                if (parameters.Length > 0)
                {
                    if (!int.TryParse(parameters[0], out _minNumberOfWords))
                    {
                        throw new ArgumentException($"The 'minWords' parameter for 'words' must be a number (but {parameters[0]} was found).");
                    }

                    if (_minNumberOfWords < 1)
                    {
                        throw new ArgumentException($"The 'minWords' parameter for 'words' must be at least 1.");
                    }
                }

                if (parameters.Length == 1)
                {
                    _maxNumberOfWords = _minNumberOfWords;
                }

                if (parameters.Length > 1)
                {
                    if (!int.TryParse(parameters[1], out _maxNumberOfWords))
                    {
                        throw new ArgumentException($"The 'maxWords' parameter for 'words' must be a number (but {parameters[1]} was found).");
                    }
                }

                if (_minNumberOfWords > _maxNumberOfWords)
                {
                    throw new ArgumentException($"The 'maxWords' parameter for 'words' must NOT be lower then 'minWords'.");
                }
            }
        }

        public string Generate()
        {
            bool shouldRepeatAString = ShouldStringBeRepeated();
            string stringPart = IsAnyStringRemembered && shouldRepeatAString ? GetRemeberedString() : GenerateString();
            RememberString(stringPart);
            return stringPart;
        }

        private bool ShouldStringBeRepeated()
        {
            if (_appSettings.RepeatRate.HasValue)
            {
                if (_appSettings.RepeatRate.Value <= 0)
                {
                    throw new InvalidOperationException("The RepeatRate in appsettings.json is invalid - it must be greater then 0");
                }

                return _randomNumberGenerator.Next(0, _appSettings.RepeatRate.Value) == 0;
            }

            return false;
        }

        private string GenerateString()
        {
            var numberOfWords = _randomNumberGenerator.Next(_minNumberOfWords, _maxNumberOfWords + 1);
            var stringPartBuilder = new StringBuilder();
            for (var i = 0; i < numberOfWords; i++)
            {
                var wordIndex = _randomNumberGenerator.Next(0, _words.Length - 1);
                var word = _words[wordIndex];
                stringPartBuilder.Append($" {word}");
            }

            stringPartBuilder.Remove(0, 1); // remove the 1st space

            return stringPartBuilder.ToString();
        }

        private void RememberString(string stringToRemember)
        {
            if (_rememberedStrings.Count > RememberedStringsLimit)
            {
                var random = new Random();
                var indexToReplace = random.Next(0, RememberedStringsLimit - 1);
                _rememberedStrings[indexToReplace] = stringToRemember;
            }
            else
            {
                _rememberedStrings.Add(stringToRemember);
            }
        }

        private string GetRemeberedString()
        {
            if (!IsAnyStringRemembered)
            {
                throw new InvalidOperationException("No string was remembered");
            }

            var random = new Random();
            var index = random.Next(0, _rememberedStrings.Count - 1);
            return _rememberedStrings[index];
        }

        private bool IsAnyStringRemembered => _rememberedStrings.Any();
    }
}
