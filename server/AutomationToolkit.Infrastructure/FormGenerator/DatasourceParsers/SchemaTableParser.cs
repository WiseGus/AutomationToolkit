using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using AutomationToolkit.Core.FormGenerator.DatasourceParsers;
using AutomationToolkit.Core.FormGenerator.Model;
using SLnet.Sand.Schema;

namespace AutomationToolkit.Infrastructure.FormGenerator.DatasourceParsers;

public class slsSchemaTableParser : IDatasourceParser
{
private List<DatasourceInfo> _datasourceInfo = new List<DatasourceInfo>();
string _tableXmlPath;

public slsSchemaTableParser(string tableXmlPath)
{
  _tableXmlPath = tableXmlPath;
}

public IEnumerable<DatasourceInfo> Parse()
{
  var tbl = GetSchemaTable();
  tbl.Fields.ForEach(fld =>
  {
    _datasourceInfo.Add(new DatasourceInfo
    {
      Name = fld.Alias,
      Caption = fld.Description ?? fld.Alias,
      DataType = fld.DataType.ToString()
    });
  });

  return _datasourceInfo;
}

public slsSchemaTable GetSchemaTable()
{
  using (var sr = File.OpenText(_tableXmlPath))
  {
    var ser = new XmlSerializer(typeof(slsSchemaTable));
    return ser.Deserialize(sr) as slsSchemaTable;
  }
}
}
