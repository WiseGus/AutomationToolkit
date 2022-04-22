using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutomationToolkit.Core.Model;
using AutomationToolkit.Core.Repositories;
using Newtonsoft.Json;

namespace AutomationToolkit.Infrastructure.Repositories;

public class PresetsRepository : IPresetsRepository
{
    private readonly string _presetsFile = "Api.Presets.json";

    public async Task<IEnumerable<Preset>> Get()
    {
        var result = new List<Preset>();

        var presetsFile = GetPresetsFile();

        if (presetsFile.Exists)
        {
            using (var sr = new StreamReader(presetsFile.FullName, Encoding.UTF8))
            {
                var presetsString = await sr.ReadToEndAsync();
                result = JsonConvert.DeserializeObject<List<Preset>>(presetsString);
            }
        }

        return result;
    }

    public async Task Save(IEnumerable<Preset> data)
    {
        var presetsFile = GetPresetsFile();

        using (var sr = new StreamWriter(presetsFile.FullName, false, Encoding.UTF8))
        {
            var resString = JsonConvert.SerializeObject(data, Formatting.Indented);
            await sr.WriteAsync(resString);
        }
    }

    protected FileInfo GetPresetsFile()
    {
        var rootDirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

        var fileName = Path.Combine(rootDirInfo.FullName, _presetsFile);

        return new FileInfo(fileName);
    }
}
