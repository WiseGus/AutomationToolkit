using Api.Compiler;
using System.Collections.Generic;
using System;

namespace Api.Util
{
    public static class Extensions
    {
        public static string ReplaceKeywords(this string text, List<Keyword> keywords)
        {
            keywords.ForEach(keyword =>
            {
                if (keyword.KeywordType == "function")
                {
                    var compiler = new ExecutronCompiler();
                    compiler.Compile(keyword.Replacement.Replace(@"\\", @"\"));
                    if (compiler.HasError)
                    {
                        throw new ArgumentException($"Compile error for {keyword.KeywordName}: {compiler.Result}");
                    }
                    keyword.Replacement = compiler.Result;
                    keyword.KeywordType = "text";
                }
                text = text.Replace(keyword.KeywordName, keyword.Replacement);
            });
            return text;
        }
    }
}
