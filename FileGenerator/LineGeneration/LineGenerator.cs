using FileGenerator.LineGeneration.TokenGeneration;
using System.Text.RegularExpressions;

namespace FileGenerator.LineGeneration
{
    public class LineGenerator
    {
        //public string GenerateLine(string lineTemplate) {
        //    var parts = SplitStringIntoParts(lineTemplate);
        //}

        public IList<LineTemplatePart> SplitLineTemplateIntoParts(string lineTemplate)
        {
            var parts = new List<LineTemplatePart>();
            var regex = new Regex(@"(\{\{.*?\}\})");
            var matches = regex.Matches(lineTemplate);

            int lastIndex = 0;
            foreach (Match match in matches)
            {
                if (match.Index > lastIndex)
                {
                    var text = lineTemplate.Substring(lastIndex, match.Index - lastIndex);
                    var textPart = new LineTemplatePart { Text = text, TokenType = TokenType.Text };
                    parts.Add(textPart);
                }

                var tokenType = GetTokenType(match.Value);
                var tokenPart = new LineTemplatePart { Text = match.Value, TokenType = tokenType };
                parts.Add(tokenPart);

                lastIndex = match.Index + match.Length;
            }

            if (lastIndex < lineTemplate.Length)
            {
                var text = lineTemplate.Substring(lastIndex);
                var tokenType = GetTokenType(text);
                var tokenPart = new LineTemplatePart { Text = text, TokenType = tokenType };
                parts.Add(tokenPart);
            }

            return parts;
        }

        public TokenType GetTokenType(string lineTemplatePart)
        {
            if (!lineTemplatePart.StartsWith("{{") || !lineTemplatePart.EndsWith("}}"))
            {
                return TokenType.Text;
            }

            var tokenLength = lineTemplatePart.IndexOf(':') - 2;
            if (tokenLength < 0)
            {
                tokenLength = lineTemplatePart.Length - 4;
            }

            TokenType result = TokenType.Unknown;
            if (Enum.TryParse(lineTemplatePart.AsSpan(2, tokenLength), true, out result))
            {
                return result;
            }

            return TokenType.Unknown;
        }

        private ITokenGenerator GetTokenGenerator(LineTemplatePart templatePart)
        {
            switch (templatePart.TokenType)
            {
                case TokenType.Text: return new StaticText(templatePart.Text);
                case TokenType.Sequence: return new SequenceGenerator();
            }

            throw new NotSupportedException($"The template has an unsupported token: {templatePart.Text}");
        }
    }
}
