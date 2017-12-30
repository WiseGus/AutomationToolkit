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
    public class GenerateProjectsController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Preset value)
        {
            /* Copy Project structure from template */
            var templateOriginInfo = new DirectoryInfo(value.TemplateOrigin);
            if (!templateOriginInfo.Exists) throw new ArgumentException("Invalid template origin path");

            var outputFolderPathInfo = new DirectoryInfo(Path.Combine(value.OutputFolderPath, ReplaceKeyword(value.ProjectName, value.Keywords)));
            outputFolderPathInfo.Create();

            DirectoryCopy(templateOriginInfo.FullName, outputFolderPathInfo.FullName, true);

            /* Replace keywords in files matching search pattern */
            var searchPatterns = string.IsNullOrEmpty(value.FileTypesExtensions) ? new[] { "*.*" } : value.FileTypesExtensions.Split(',');
            foreach (string searchPattern in searchPatterns)
            {
                foreach (var fileInfo in outputFolderPathInfo.EnumerateFiles(searchPattern, SearchOption.AllDirectories))
                {
                    var fileText = await System.IO.File.ReadAllTextAsync(fileInfo.FullName);
                    ReplaceKeyword(fileText, value.Keywords);
                    await System.IO.File.WriteAllTextAsync(fileInfo.FullName, fileText);
                }
            }

            /* Perform updates in main build solutions, core folders */
            var appSettings = await new SettingsController().GetAppSettings();

            return new OkResult();
        }

        private string ReplaceKeyword(string text, List<Keyword> keywords)
        {
            keywords.ForEach(keyword =>
            {
                text = text.Replace(keyword.keyword, keyword.replacement);
            });
            return text;
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
