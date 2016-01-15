using Cleaver.Utils.Http;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Cleaver.Utils {
    public class StringUtils {

        private static Regex replaceNewlinesRegex = new Regex("<br.*?>", RegexOptions.Compiled);
        private static Regex removeAllTagsRegex = new Regex("<[^>]*>", RegexOptions.Compiled);
        private static Regex removeWhitespacesRegex = new Regex(@"\s+", RegexOptions.Compiled);

        public static string Process(string src, params Func<string, string>[] funcs) {
            if (src == null)
                return null;
            var tmp = src;
            foreach (var f in funcs) {
                tmp = f(tmp);
            }
            return tmp;
        }

        public static string ReplaceNewlines(string src) {
            return replaceNewlinesRegex.Replace(src, " ");
        }

        public static string RemoveWhitespaces(string src) {
            return removeWhitespacesRegex.Replace(src, string.Empty);
        }

        public static string RemoveWhitespaces(string src, string replacement) {
            return removeWhitespacesRegex.Replace(src, replacement);
        }

        public static string RemoveHtmlTags(string src) {
            var tmp = removeAllTagsRegex.Replace(src, string.Empty);
            tmp = HttpUtility.HtmlDecode(tmp);
            return tmp.Trim();
        }

        public static Parameters FindCollections(string source, Regex regex) {
            var matchers = regex.Matches(source);
            if (matchers.Count == 0) {
                return null;
            }
            var result = new Parameters();
            foreach (Match matcher in matchers) {
                if (matcher.Success) {
                    result[matcher.Groups[1].Value] = matcher.Groups[2].Value;
                }
            }
            return result;
        }

        public static string RegexGroup(string input, Regex regex, int group = 1) {
            var m = regex.Match(input);
            if (m.Success && m.Groups.Count >= group) {
                return m.Groups[group].Value;
            }
            return null;
        }

        public static bool IgnoreDiacriticsCompare(string x, string y) {
            if (x == null || y == null) return false;
            return System.String.Compare(x.Trim(), y.Trim(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0;
        }

        public static string NormalizeAmount(string org) {
            if (string.IsNullOrWhiteSpace(org)) {
                return string.Empty;
            }
            return org.Replace(",", "").Trim();
        }
    }
}
