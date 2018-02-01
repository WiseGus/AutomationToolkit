using Api.Util.FormGenerator.Model;
using System.Collections.Generic;

namespace Api.Util.FormGenerator.DatasourceParsers {
  public interface IDatasourceParser {
    IEnumerable<DatasourceInfo> Parse();
  }

}
