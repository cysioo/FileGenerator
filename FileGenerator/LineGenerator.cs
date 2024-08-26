using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileGenerator
{
    public class LineGenerator
    {
        //public string GenerateLine(string lineTemplate) {
        //    var parts = SplitStringIntoParts(lineTemplate);
        //}

        public List<string> SplitStringIntoParts(string lineTemplate)
        {
            var parts = new List<string>();
            var regex = new Regex(@"(\{\{.*?\}\})");
            var matches = regex.Matches(lineTemplate);

            int lastIndex = 0;
            foreach (Match match in matches)
            {
                if (match.Index > lastIndex)
                {
                    parts.Add(lineTemplate.Substring(lastIndex, match.Index - lastIndex));
                }
                parts.Add(match.Value);
                lastIndex = match.Index + match.Length;
            }

            if (lastIndex < lineTemplate.Length)
            {
                parts.Add(lineTemplate.Substring(lastIndex));
            }

            return parts;
        }
    }
}
