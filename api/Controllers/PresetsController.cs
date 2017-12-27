using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class PresetsController : Controller
    {

        [HttpGet("ping")]
        public IActionResult Ping()
        {
          return new JsonResult("pong");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var templateDirsInfo = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates"));
            if (!templateDirsInfo.Exists) return new NotFoundResult();

            var templateDirs = templateDirsInfo.GetDirectories();

            Console.WriteLine(templateDirsInfo.FullName);
            var res = templateDirs.Select(p => p.Name).ToList();

            return new JsonResult(res, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public IActionResult Post([FromBody]Preset value)
        {
            var newDirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", value.Name);
            if (Directory.Exists(newDirPath))
            {
                return new UnauthorizedResult();
            }

            var templateDirsInfo = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates"));
            var newDirInfo = templateDirsInfo.CreateSubdirectory(value.Name);
            DirectoryCopy(value.TemplateOrigin, newDirInfo.FullName, true);

            return new OkResult();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
  }
}
