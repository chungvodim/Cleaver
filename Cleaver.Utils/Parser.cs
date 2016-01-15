using Cleaver.Data;
using Cleaver.Utils.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cleaver.Utils
{
    public class Parser
    {
        public static ParseResult ParseValue(string pattern, string content)
        {
            var regex = new Regex(pattern);
            var match = regex.Match(content);

            if (match.Success)
            {
                return new ParseResult() { IsSuccessful = true, Value = match.Groups[1].Value };
            }
            else
                return new ParseResult() { IsSuccessful = false };
        }

        public static bool ParseHiddenFields(string pattern, string content, out Parameters p)
        {
            p = new Parameters();

            var regex = new Regex(pattern);
            foreach (Match match in regex.Matches(content))
            {
                if (!match.Success || match.Groups.Count != 3)
                    return false;

                p[match.Groups[1].Value] = match.Groups[2].Value;
            }

            return true;
        }
    }
}
