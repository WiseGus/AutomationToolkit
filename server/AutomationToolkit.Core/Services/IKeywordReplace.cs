using System.Collections.Generic;
using AutomationToolkit.Core.Model;

namespace AutomationToolkit.Core.Services;

public interface IKeywordReplace
{
void AddKeywords(IEnumerable<Keyword> keywords);
string Replace(string value);
void ReplaceAll();
}
