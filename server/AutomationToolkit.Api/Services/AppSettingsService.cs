using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutomationToolkit.Core.Model;
using AutomationToolkit.Core.Repositories;
using AutomationToolkit.Core.Services;

namespace AutomationToolkit.Api.Services;

public class AppSettingsService : IAppSettingsService
{
private readonly IAppSettingsRepository _appSettingsRepository;

public AppSettingsService(IAppSettingsRepository appSettingsRepository)
{
  _appSettingsRepository = appSettingsRepository;
}

public async Task<string> TFPath()
{
  return await GetAppSettingKeyValue("@TfPath@");
}

public async Task<bool> IsDebugMode()
{
  var value = await GetAppSettingKeyValue("@DebugMode@");

  return Convert.ToBoolean(value);
}

public Task<IEnumerable<Keyword>> GetKeywords()
{
  return _appSettingsRepository.Get();
}

protected async Task<string> GetAppSettingKeyValue(string key)
{
  var appSettings = await _appSettingsRepository.Get();

  var keyword = appSettings.FirstOrDefault(p => p.KeywordName == key);

  return keyword?.Replacement;
}

public Task SaveKeywords(IEnumerable<Keyword> keywords)
{
  return _appSettingsRepository.Save(keywords);
}
}
