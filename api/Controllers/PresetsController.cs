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

            // var settsController = new SettingsController();
            // var appSettings = await settsController.GetAppSettings();

            // var templateDirsInfo = new DirectoryInfo(appSettings.TemplatesFolderPath);
            // if (!templateDirsInfo.Exists) return new NotFoundResult();

            // var templateDirs = templateDirsInfo.GetDirectories();

            // Console.WriteLine(templateDirsInfo.FullName);
            // var res = templateDirs.Select(p => p.Name).ToList();

            return new JsonResult(presetsList, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var presetsList = await ReadPresetsFile();
            var presetObj = presetsList.Find(p => p.Name == name);

            return new JsonResult(presetObj, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Preset value)
        {
            var presetsList = await ReadPresetsFile();
            presetsList.Add(value);
            WritePresetsFile(presetsList);

            // var newDirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", value.Name);
            // if (Directory.Exists(newDirPath))
            // {
            //     return new UnauthorizedResult();
            // }

            // var templateDirsInfo = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates"));
            // var newDirInfo = templateDirsInfo.CreateSubdirectory(value.Name);
            // DirectoryCopy(value.TemplateOrigin, newDirInfo.FullName, true);

            return new OkResult();
        }

        [HttpPut("{name}")]
        public async void Put(string name, [FromBody]Preset value)
        {
            var presetsList = await ReadPresetsFile();
            var presetIdx = presetsList.FindIndex(p => p.Name == name);
            if (presetIdx > -1)
            {
                presetsList.RemoveAt(presetIdx);
                presetsList.Insert(presetIdx, value);
            }
            WritePresetsFile(presetsList);
        }

        [HttpDelete("{name}")]
        public async void Delete(string name)
        {
            var presetsList = await ReadPresetsFile();
            var presetObj = presetsList.Find(p => p.Name == name);
            if (presetObj != null)
            {
                presetsList.Remove(presetObj);
            }
            WritePresetsFile(presetsList);
        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
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
