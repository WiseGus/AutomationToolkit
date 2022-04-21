using Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
  public interface IAppSettingsService
  {
    Task<IEnumerable<Keyword>> GetKeywords();
    Task<bool> IsDebugMode();
    Task<string> TFPath();
    Task SaveKeywords(IEnumerable<Keyword> keywords);
  }
}
