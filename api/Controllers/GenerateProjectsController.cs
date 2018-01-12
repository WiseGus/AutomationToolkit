using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using Api.Util;

namespace Api.Controllers
{
  [Route("api/[controller]")]
  public class GenerateProjectsController : Controller
  {
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]Preset value)
    {

      /* Replace internal keywords */
      value.Keywords.ForEach(keyword => keyword.Replacement = keyword.Replacement.ReplaceKeywords(value.Keywords));

      /* Copy Project structure from template */
      if (!Path.IsPathRooted(value.TemplateOrigin))
      {
        value.TemplateOrigin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, value.TemplateOrigin);
      }
      var templateOriginInfo = new DirectoryInfo(value.TemplateOrigin);
      if (!templateOriginInfo.Exists) throw new ArgumentException("Invalid template origin path");

      var outputFolderPathInfo = new DirectoryInfo(value.OutputFolderPath.ReplaceKeywords(value.Keywords));
      outputFolderPathInfo.Create();

      DirectoryCopy(templateOriginInfo.FullName, outputFolderPathInfo.FullName, true, value.Keywords);

      /* Replace keywords in files matching search pattern */
      var searchPatterns = string.IsNullOrEmpty(value.FileKeywordTypesExtensions) ? new[] { "*.*" } : value.FileKeywordTypesExtensions.Split(',');
      foreach (string searchPattern in searchPatterns)
      {
        foreach (var fileInfo in outputFolderPathInfo.EnumerateFiles(searchPattern, SearchOption.AllDirectories))
        {
          var fileText = await System.IO.File.ReadAllTextAsync(fileInfo.FullName, Encoding.UTF8);
          fileText = fileText.ReplaceKeywords(value.Keywords);
          try
          {
            await System.IO.File.WriteAllTextAsync(fileInfo.FullName, fileText, Encoding.UTF8);
          }
          catch { }
        }
      }

      if (value.AddToSourceControl)
      {
        /* Add to Source Control */
        // TODO
      }

      if (value.AutomationUpdates.UseAutomationUpdates)
      {
        var appSettsObj = await new SettingsController().GetAppSettings();
        var handler = new AutomationUpdatesHandler(value, appSettsObj);
        return new ObjectResult(handler.Execute());
      }

      return new OkResult();
    }

    private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, List<Keyword> keywords)
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
        string tempPath = Path.Combine(destDirName, file.Name);
        tempPath = tempPath.ReplaceKeywords(keywords);
        file.CopyTo(tempPath, true);
      }

      // If copying subdirectories, copy them and their contents to new location.
      if (copySubDirs)
      {
        foreach (DirectoryInfo subdir in dirs)
        {
          string tempPath = Path.Combine(destDirName, subdir.Name);
          tempPath = tempPath.ReplaceKeywords(keywords);
          DirectoryCopy(subdir.FullName, tempPath, copySubDirs, keywords);
        }
      }
    }
  }
}
