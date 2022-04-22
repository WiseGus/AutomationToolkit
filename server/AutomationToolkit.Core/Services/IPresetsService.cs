using System.Collections.Generic;
using System.Threading.Tasks;
using AutomationToolkit.Core.Model;

namespace AutomationToolkit.Core.Services;

public interface IPresetsService
{
Task<IEnumerable<Preset>> GetPresets();
Task<Preset> GetPresetById(string id);
Task AddOrUpdatePreset(Preset preset);
Task RemovePreset(string id);
}
