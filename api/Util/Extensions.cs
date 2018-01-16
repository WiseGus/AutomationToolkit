using System;
using System.Collections.Generic;
using System.Reflection;

namespace Api.Util
{
  public static class Extensions
  {
    public static List<Keyword> AsKeywords(this AppSettings obj)
    {
      var res = new List<Keyword>();

      var propsInfo = typeof(AppSettings).GetProperties(BindingFlags.Public | BindingFlags.Instance);
      foreach (var propInfo in propsInfo)
      {
        res.Add(new Keyword
        {
          KeywordName = WrapWith("@", propInfo.Name),
          Replacement = Convert.ToString(propInfo.GetValue(obj)),
          KeywordType = "text"
        });
      }
      
      return res;
    }

    private static string WrapWith(string wrapString, string source)
    {
      return wrapString + source + wrapString;
    }
  }
}
