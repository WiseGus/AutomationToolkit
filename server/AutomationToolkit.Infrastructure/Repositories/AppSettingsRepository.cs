using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutomationToolkit.Core.Model;
using AutomationToolkit.Core.Repositories;
using Newtonsoft.Json;

namespace AutomationToolkit.Infrastructure.Repositories;

public class AppSettingsRepository : IAppSettingsRepository
{
private readonly string _apiSettingsFile = "Api.Settings.json";

public async Task<IEnumerable<Keyword>> Get()
{
  var result = new List<Keyword>();

  var settingsFile = GetSettingsFile();

  if (settingsFile.Exists)
  {
    using (var sr = new StreamReader(settingsFile.FullName, Encoding.UTF8))
    {
      var settingsString = await sr.ReadToEndAsync();
      result = JsonConvert.DeserializeObject<List<Keyword>>(settingsString);
    }
  }

  return result;
}

public async Task Save(IEnumerable<Keyword> data)
{
  var settingsFile = GetSettingsFile();

  using (var sr = new StreamWriter(settingsFile.FullName, false, Encoding.UTF8))
  {
    var resString = JsonConvert.SerializeObject(data, Formatting.Indented);
    await sr.WriteAsync(resString);
  }
}

protected FileInfo GetSettingsFile()
{
  var rootDirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

  var fileName = Path.Combine(rootDirInfo.FullName, _apiSettingsFile);

  return new FileInfo(fileName);
}
}
