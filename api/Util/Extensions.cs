using System.Collections.Generic;

namespace Api.Util
{
    public static class Extensions
    {
        public static string ReplaceKeywords(this string text, List<Keyword> keywords)
        {
            keywords.ForEach(keyword =>
            {
                text = text.Replace(keyword.KeywordName, keyword.Replacement);
            });
            return text;
        }
    }
}
