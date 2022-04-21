using Core;
using Core.Interfaces.Services;
using Core.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services
{
  public class GenerateProjectsService : IGenerateProjectsService
  {
    private readonly ILogger<GenerateProjectsService> _logger;
    private readonly IAppSettingsService _appSettingsService;
    private readonly IKeywordReplace _keywordReplace;

    public GenerateProjectsService(ILogger<GenerateProjectsService> logger, IAppSettingsService appSettingsService, IKeywordReplace keywordReplace)
    {
      _logger = logger;
      _appSettingsService = appSettingsService;
      _keywordReplace = keywordReplace;
    }

    public async Task Generate(Preset value)
    {
      _logger.LogDebug("Post GenerateProjects");

      _keywordReplace.AddKeywords(value.Keywords);
      _keywordReplace.ReplaceAll();
      _logger.LogDebug("Replaced keywords");

      /* Copy Project structure from template */
      if (!Path.IsPathRooted(value.TemplateOrigin))
      {
        value.TemplateOrigin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, value.TemplateOrigin);
      }
      var templateOriginInfo = new DirectoryInfo(value.TemplateOrigin);
      if (!templateOriginInfo.Exists) throw new ArgumentException("Invalid template origin path");

      var copiedFolders = new List<DirectoryInfo>();
      var outputFolderPathInfo = new DirectoryInfo(_keywordReplace.Replace(value.OutputFolderPath));
      DirectoryCopy(templateOriginInfo.FullName, outputFolderPathInfo.FullName, true, outputFolderPathInfo.FullName, copiedFolders);

      /* Replace keywords in files matching search pattern */
      await ReplaceKeywordsInFiles(value.FileKeywordTypesExtensions, copiedFolders);
      _logger.LogDebug("Replaced keywords in files");

      if (value.AddToSourceControl)
      {
        var tfPath = await _appSettingsService.TFPath();
        var tf = new TFImpl(tfPath);
        foreach (var dirInfo in copiedFolders)
        {
          /* Add to Source Control */
          _logger.LogDebug("Adding project to Source Control");
          tf.RunTFCommand(dirInfo.FullName, new[] { "add", "*.*", "/recursive" });

          /* Update main solution with new project (Glx/Crm build convention)*/
          var buildSolutionDirInfo = dirInfo.Parent.GetDirectories($"Build_*");
          if (buildSolutionDirInfo.Length > 0)
          {
            var buildSolutionPath = buildSolutionDirInfo[0].GetFiles("*.sln");
            if (buildSolutionPath.Length > 0)
            {
              _logger.LogDebug("Update main build solution");
              tf.RunTFCommand(dirInfo.FullName, new[] { "checkout", buildSolutionPath[0].FullName });

              var slnObj = new SolutionParser(buildSolutionPath[0].FullName);
              slnObj.AddProject(dirInfo.Name);
            }
          }
        }
      }

      if (value.AutomationUpdates.UseAutomationUpdates)
      {
        _logger.LogDebug("Executing automation toolkit updates");
        var handler = new AutomationUpdatesHandler(_logger, value, _keywordReplace);
        handler.Execute();
      }
    }

    private async Task ReplaceKeywordsInFiles(string fileKeywordTypesExtensions, List<DirectoryInfo> copiedFolders)
    {
      var searchPatterns = string.IsNullOrEmpty(fileKeywordTypesExtensions) ? new[] { "*.*" } : fileKeywordTypesExtensions.Split(',');
      foreach (var dirInfo in copiedFolders)
      {
        foreach (string searchPattern in searchPatterns)
        {
          foreach (var fileInfo in dirInfo.EnumerateFiles(searchPattern, SearchOption.AllDirectories))
          {
            var fileText = await System.IO.File.ReadAllTextAsync(fileInfo.FullName, Encoding.UTF8);
            fileText = _keywordReplace.Replace(fileText);
            try
            {
              await System.IO.File.WriteAllTextAsync(fileInfo.FullName, fileText, Encoding.UTF8);
            }
            catch { }
          }
        }
      }
    }

    private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, string rootCopyFolder, List<DirectoryInfo> copiedRootFolders)
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
        var dirInfo = Directory.CreateDirectory(destDirName);
        if (dirInfo.Parent.FullName == rootCopyFolder)
        {
          copiedRootFolders.Add(dirInfo);
        }
      }

      // Get the files in the directory and copy them to the new location.
      FileInfo[] files = dir.GetFiles();
      foreach (FileInfo file in files)
      {
        string tempPath = Path.Combine(destDirName, file.Name);
        tempPath = _keywordReplace.Replace(tempPath);
        file.CopyTo(tempPath, true);
      }

      // If copying subdirectories, copy them and their contents to new location.
      if (copySubDirs)
      {
        foreach (DirectoryInfo subdir in dirs)
        {
          string tempPath = Path.Combine(destDirName, subdir.Name);
          tempPath = _keywordReplace.Replace(tempPath);
          DirectoryCopy(subdir.FullName, tempPath, copySubDirs, rootCopyFolder, copiedRootFolders);
        }
      }
    }
  }
}
