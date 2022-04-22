using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using AutomationToolkit.Core.Model;
using AutomationToolkit.Core.Repositories;
using AutomationToolkit.Core.Services;

namespace AutomationToolkit.Api.Services;

public class PresetsService : IPresetsService
{
private readonly IPresetsRepository _presetsRepository;

public PresetsService(IPresetsRepository presetsRepository)
{
  _presetsRepository = presetsRepository;
}

public async Task<Preset> GetPresetById(string id)
{
  Contract.Assert(string.IsNullOrEmpty(id));

  var presets = await GetPresets();

  return presets.FirstOrDefault(p => p.Alias == id);
}

public Task<IEnumerable<Preset>> GetPresets()
{
  return _presetsRepository.Get();
}

public async Task RemovePreset(string id)
{
  Contract.Assert(string.IsNullOrEmpty(id));

  var presets = await GetPresets();

  var presetsList = new List<Preset>(presets);

  var preset = presetsList.Find(p => p.Alias == id);

  if (preset != null)
  {
    presetsList.Remove(preset);
  }
}

public async Task AddOrUpdatePreset(Preset value)
{
  Contract.Assert(value != null);

  var presets = await GetPresets();

  var presetsList = new List<Preset>(presets);

  var presetIdx = presetsList.FindIndex(p => p.Alias == value.Alias);

  if (presetIdx == -1)
  {
    presetsList.Add(value);
  }
  else
  {
    presetsList[presetIdx] = value;
  }

  await _presetsRepository.Save(presets);
}
}
