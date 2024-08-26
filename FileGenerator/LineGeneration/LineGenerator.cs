using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FileGenerator.LineGeneration
{
    public class LineGenerator
    {
        //public string GenerateLine(string lineTemplate) {
        //    var parts = SplitStringIntoParts(lineTemplate);
        //}

        public List<LineTemplatePart> SplitStringIntoParts(string lineTemplate)
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

                var tokenPart = new LineTemplatePart { Text = match.Value, TokenType = TokenType.Sequence };
                parts.Add(tokenPart);

                lastIndex = match.Index + match.Length;
            }

            if (lastIndex < lineTemplate.Length)
            {
                var text = lineTemplate.Substring(lastIndex);
                var tokenPart = new LineTemplatePart { Text = text, TokenType = TokenType.Sequence };
                parts.Add(tokenPart);
            }

            return parts;
        }
    }
}
