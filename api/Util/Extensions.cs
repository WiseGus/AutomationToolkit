using System.Collections.Generic;

namespace Api.Util
{
    public static class Extensions
    {
        public static List<Keyword> AsKeywords(this AppSettings obj)
        {
            return new List<Keyword> {
              new Keyword {
                KeywordName = WrapWith("@", nameof(obj.TfsUrl)),
                Replacement = obj.TfsUrl,
                KeywordType = "text"
              },
              new Keyword {
                KeywordName = WrapWith("@", nameof(obj.TfsWorkspace)),
                Replacement = obj.TfsWorkspace,
                KeywordType = "text"
              }
            };
        }

        private static string WrapWith(string wrapString, string source)
        {
            return wrapString + source + wrapString;
        }
    }
}
