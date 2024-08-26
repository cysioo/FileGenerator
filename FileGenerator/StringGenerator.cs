using System.Text;

namespace FileGenerator
{
    public class StringGenerator : IStringGenerator
    {
        private const int RememberedStringsLimit = 1000;
        private readonly IAppSettings _appSettings;
        private IList<string> _rememberedStrings = new List<string>();
        private Random _randomNumberGenerator = new Random();
        private readonly string[] _words;

        public StringGenerator(IAppSettings appSettings, IFileService fileService)
        {
            _appSettings = appSettings;
            _words = fileService.Words;
        }

        public string GetNewStringPart()
        {
            var shouldRepeatAString = _randomNumberGenerator.Next(0, _appSettings.StringRepeatRate) == 0;
            string stringPart = IsAnyStringRemembered && shouldRepeatAString ? GetRemeberedString() : GenerateString();
            RememberString(stringPart);
            return stringPart;
        }

        private string GenerateString()
        {
            var numberOfWords = _randomNumberGenerator.Next(1, 10);
            var stringPartBuilder = new StringBuilder();
            for (var i = 0; i < numberOfWords; i++)
            {
                var wordIndex = _randomNumberGenerator.Next(0, _words.Length - 1);
                var word = _words[wordIndex];
                stringPartBuilder.Append($" {word}");
            }

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
