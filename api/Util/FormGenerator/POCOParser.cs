using System;
using System.Collections.Generic;
using System.Reflection;

namespace Api.Util.FormGenerator
{
  public class POCOParser : IDatasourceParser
  {
    private Type _type;
    private List<DatasourceInfo> _datasourceInfo = new List<DatasourceInfo>();

    public POCOParser(Type type)
    {
      _type = type;
    }

    public POCOParser(string assemblyPath, string fullClassName)
    {
      var asm = Assembly.LoadFrom(assemblyPath);
      _type = asm.GetType(fullClassName);
    }

    public IEnumerable<DatasourceInfo> Parse()
    {
      var props = _type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
      foreach (var prop in props)
      {
        _datasourceInfo.Add(new DatasourceInfo
        {
          Name = prop.Name,
          DataType = prop.PropertyType.Name
        });
      }


      return _datasourceInfo;
    }
  }
}
