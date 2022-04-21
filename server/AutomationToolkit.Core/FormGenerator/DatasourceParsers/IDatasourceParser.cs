using Core.FormGenerator.Model;
using System.Collections.Generic;

namespace Core.FormGenerator.DatasourceParsers
{
  public interface IDatasourceParser
  {
    IEnumerable<DatasourceInfo> Parse();
  }

}
