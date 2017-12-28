using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class SettingsController : Controller
    {

        [HttpGet("ping")]
        public IActionResult Ping()
        {
          return new JsonResult("pong");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await GetAppSettings();
            return new JsonResult(res, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AppSettings value)
        {
            var rootDirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var settingsFile = new FileInfo(rootDirInfo + "/" + "AppSettings.json");
            using (var sr = new StreamWriter(settingsFile.FullName, false, Encoding.UTF8))
            {
                var resString = JsonConvert.SerializeObject(value, Formatting.Indented);
                await sr.WriteAsync(resString);
            }

            return new OkResult();
        }

        public async Task<AppSettings> GetAppSettings() {
            var res = new AppSettings();

            var rootDirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var settingsFile = new FileInfo(rootDirInfo + "/" + "AppSettings.json");
            if (settingsFile.Exists)
            {
                using (var sr = new StreamReader(settingsFile.FullName, Encoding.UTF8))
                {
                  var settingsString = await sr.ReadToEndAsync();
                  res = JsonConvert.DeserializeObject<AppSettings>(settingsString);
                }
            }
            return res;
        }
    }
}
