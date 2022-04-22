using System.Collections.Generic;
using System.Threading.Tasks;
using AutomationToolkit.Core.FormGenerator.Model;

namespace AutomationToolkit.Core.Services;

public interface IFormGenService
{
    Task<string> GenerateFormDesigner(FormGenInfo data);
    Task<IEnumerable<DatasourceInfo>> GetGlxPoco(FormGenInfo data);
}
