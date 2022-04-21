using Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
  public interface IPresetsService
  {
    Task<IEnumerable<Preset>> GetPresets();
    Task<Preset> GetPresetById(string id);
    Task AddOrUpdatePreset(Preset preset);
    Task RemovePreset(string id);
  }
}
