using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class PresetsController : Controller
    {
        private DirectoryInfo _rootDirInfo;
        private FileInfo _presetsFile;

        public PresetsController()
        {
            _rootDirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            _presetsFile = new FileInfo(_rootDirInfo + "/" + "Api.Presets.json");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var presetsList = await ReadPresetsFile();
            return new JsonResult(presetsList, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        [HttpGet("{alias}")]
        public async Task<IActionResult> Get(string alias)
        {
            var presetsList = await ReadPresetsFile();
            var presetObj = presetsList.Find(p => p.Alias == alias);

            return new JsonResult(presetObj, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Preset value)
        {
            var presetsList = await ReadPresetsFile();
            presetsList.Add(value);
            WritePresetsFile(presetsList);

            return new OkResult();
        }

        [HttpPut("{alias}")]
        public async void Put(string alias, [FromBody]Preset value)
        {
            var presetsList = await ReadPresetsFile();
            var presetIdx = presetsList.FindIndex(p => p.Alias == alias);
            if (presetIdx > -1)
            {
                presetsList.RemoveAt(presetIdx);
                presetsList.Insert(presetIdx, value);
            }
            WritePresetsFile(presetsList);
        }

        [HttpDelete("{alias}")]
        public async void Delete(string alias)
        {
            var presetsList = await ReadPresetsFile();
            var presetObj = presetsList.Find(p => p.Alias == alias);
            if (presetObj != null)
            {
                presetsList.Remove(presetObj);
            }
            WritePresetsFile(presetsList);
        }

        private async Task<List<Preset>> ReadPresetsFile()
        {
            var presetsList = new List<Preset>();
            if (_presetsFile.Exists)
            {
                using (var sr = new StreamReader(_presetsFile.FullName, Encoding.UTF8))
                {
                    var presetsString = await sr.ReadToEndAsync();
                    presetsList = JsonConvert.DeserializeObject<List<Preset>>(presetsString);
                }
            }

            return presetsList;
        }

        private async void WritePresetsFile(List<Preset> presetsList)
        {
            using (var sr = new StreamWriter(_presetsFile.FullName, false, Encoding.UTF8))
            {
                var resString = JsonConvert.SerializeObject(presetsList, Formatting.Indented);
                await sr.WriteAsync(resString);
            }
        }
    }
}
