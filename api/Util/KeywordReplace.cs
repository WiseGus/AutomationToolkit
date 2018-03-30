using System;
using System.Collections.Generic;
using Api.Compiler;
using Api.Util;

namespace Api.Util
{
    internal class KeywordReplace
    {
        private List<Keyword> _keywords;

        public KeywordReplace()
        {
            _keywords = new List<Keyword>();
        }

        internal void AddKeywords(List<Keyword> keywords)
        {
            _keywords.AddRange(keywords);
        }

        internal void ReplaceAll()
        {
            foreach (var keyword in _keywords)
            {
                keyword.Replacement = Replace(keyword.Replacement);
            }
        }

        internal string Replace(string value)
        {
            if (string.IsNullOrEmpty(value)) {
                return value;
            }
            return ReplaceKeywords(value);
        }

        private string ReplaceKeywords(string text)
        {
            _keywords.ForEach(keyword =>
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
