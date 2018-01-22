using System.Collections.Generic;

namespace Api.Util.FormGenerator
{
    public interface IDatasourceParser
    {
        IEnumerable<DatasourceInfo> Parse();
    }

}
