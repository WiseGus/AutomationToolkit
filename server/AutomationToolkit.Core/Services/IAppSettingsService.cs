using System.Collections.Generic;
using System.Threading.Tasks;
using AutomationToolkit.Core.Model;

namespace AutomationToolkit.Core.Services;

public interface IAppSettingsService
{
Task<IEnumerable<Keyword>> GetKeywords();
Task<bool> IsDebugMode();
Task<string> TFPath();
Task SaveKeywords(IEnumerable<Keyword> keywords);
}
