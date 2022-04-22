using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using AutomationToolkit.Core.FormGenerator.DatasourceParsers;
using AutomationToolkit.Core.FormGenerator.Model;

namespace AutomationToolkit.Infrastructure.FormGenerator.DatasourceParsers;

public class POCOParser : IDatasourceParser
{
private Type _type;
private List<DatasourceInfo> _datasourceInfo = new List<DatasourceInfo>();

public POCOParser(Type type)
{
  _type = type;
}

public POCOParser(string assemblyPath, string classFullName)
{
  var asmName = AssemblyLoadContext.GetAssemblyName(assemblyPath);
  var asm = Assembly.LoadFrom(assemblyPath);
  _type = asm.GetType(classFullName);
}

public IEnumerable<DatasourceInfo> Parse()
{
  if (_type == null)
  {
    throw new ArgumentException("Invalid type");
  }

  var props = _type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
  foreach (var prop in props)
  {
    _datasourceInfo.Add(new DatasourceInfo
    {
      Name = prop.Name,
      Caption = prop.Name,
      DataType = PrettyTypeName(prop.PropertyType)
    });
  }

  return _datasourceInfo;
}

private string PrettyTypeName(Type t)
{
  if (t.IsGenericType)
  {
    return string.Format(
        "{0}<{1}>",
        t.Name.Substring(0, t.Name.LastIndexOf("`", StringComparison.InvariantCulture)),
        string.Join(", ", t.GetGenericArguments().Select(PrettyTypeName)));
  }

  return t.Name;
}
}
