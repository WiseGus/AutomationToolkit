using Newtonsoft.Json;
using SLnet.Sand.Schema;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Api.Util.FormGenerator {
  public class slSchemaTableParser : IDatasourceParser {
    private List<DatasourceInfo> _datasourceInfo = new List<DatasourceInfo>();
    string _tableXmlPath;

    public slSchemaTableParser(string tableXmlPath) {
      _tableXmlPath = tableXmlPath;
    }

    public IEnumerable<DatasourceInfo> Parse() {
      using (var sr = File.OpenText(_tableXmlPath)) {
        var ser = new XmlSerializer(typeof(slsSchemaTable));
        var tbl = ser.Deserialize(sr) as slsSchemaTable;

        tbl.Fields.ForEach(fld => {
          _datasourceInfo.Add(new DatasourceInfo {
            Name = fld.Alias,
            Caption = fld.Description ?? fld.Alias,
            DataType = fld.DataType.ToString()
          });
        });
      }

      return _datasourceInfo;
    }
  }
}
