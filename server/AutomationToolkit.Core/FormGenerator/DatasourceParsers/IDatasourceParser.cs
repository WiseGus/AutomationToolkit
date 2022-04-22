using System.Collections.Generic;
using AutomationToolkit.Core.FormGenerator.Model;

namespace AutomationToolkit.Core.FormGenerator.DatasourceParsers;

public interface IDatasourceParser
{
    IEnumerable<DatasourceInfo> Parse();
}

