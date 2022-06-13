using System.Diagnostics.Contracts;
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
        //Contract.Assert(string.IsNullOrEmpty(id));

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

        var presets = (await GetPresets()).ToList();

        var preset = presets.Find(p => p.Alias == id);

        if (preset != null)
        {
            presets.Remove(preset);
        }

        await _presetsRepository.Save(presets);
    }

    public async Task AddOrUpdatePreset(Preset value)
    {
        Contract.Assert(value != null);

        var presets = (await GetPresets()).ToList();

        var presetIdx = presets.FindIndex(p => p.Alias == value.Alias);

        if (presetIdx == -1)
        {
            presets.Add(value);
        }
        else
        {
            presets[presetIdx] = value;
        }

        await _presetsRepository.Save(presets);
    }
}
