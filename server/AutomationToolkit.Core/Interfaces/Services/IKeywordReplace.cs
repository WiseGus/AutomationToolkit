using Core.Model;
using System.Collections.Generic;

namespace Core.Interfaces.Services
{
  public interface IKeywordReplace
  {
    void AddKeywords(IEnumerable<Keyword> keywords);
    string Replace(string value);
    void ReplaceAll();
  }
}
