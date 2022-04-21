using Core.Compiler;
using Core.Interfaces.Services;
using Core.Model;
using System;
using System.Collections.Generic;

namespace Core
{
  public class KeywordReplace : IKeywordReplace
  {
    private readonly List<Keyword> _keywords;
    private readonly ICompiler _compiler;

    public KeywordReplace(ICompiler compiler)
    {
      _keywords = new List<Keyword>();
      _compiler = compiler;
    }

    public void AddKeywords(IEnumerable<Keyword> keywords)
    {
      _keywords.AddRange(keywords);
    }

    public void ReplaceAll()
    {
      foreach (var keyword in _keywords)
      {
        keyword.Replacement = Replace(keyword.Replacement);
      }
    }

    public string Replace(string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        return value;
      }
      return ReplaceKeywords(value);
    }

    protected string ReplaceKeywords(string text)
    {
      _keywords.ForEach(keyword =>
      {
        if (keyword.KeywordType == "function")
        {
          _compiler.Compile(keyword.Replacement.Replace(@"\\", @"\"));
          if (_compiler.HasError)
          {
            throw new ArgumentException($"Compile error for {keyword.KeywordName}: {_compiler.Result}");
          }
          keyword.Replacement = _compiler.Result;
          keyword.KeywordType = "text";
        }
        text = text.Replace(keyword.KeywordName, keyword.Replacement);
      });
      return text;
    }
  }
}
