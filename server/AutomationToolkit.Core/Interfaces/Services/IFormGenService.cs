using Core.FormGenerator.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
  public interface IFormGenService
  {
    Task<string> GenerateFormDesigner(FormGenInfo data);
    Task<IEnumerable<DatasourceInfo>> GetGlxPoco(FormGenInfo data);
  }
}
