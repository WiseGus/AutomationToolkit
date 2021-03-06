using CWDev.SLNTools.Core;
using System;
using System.Collections.Generic;

namespace Api.Util {

  public class SolutionParser {

    private string _solutionPath;

    public SolutionParser(string solutionPath) {
      _solutionPath = solutionPath;
    }

    public void AddProject(string newProjectName) {
      var baseSln = SolutionFile.FromFile(_solutionPath);

      var templateProject = baseSln.Projects[0];

      baseSln.Projects.Add(new Project(
          container: baseSln,
          projectGuid: "{" + Guid.NewGuid().ToString().ToUpper() + "}",
          projectTypeGuid: templateProject.ProjectTypeGuid,
          projectName: newProjectName,
          relativePath: templateProject.RelativePath.Replace(templateProject.ProjectName, newProjectName),
          parentFolderGuid: templateProject.ParentFolderGuid,
          projectSections: templateProject.ProjectSections,
          versionControlLines: SetVersionControlLines(templateProject, templateProject.ProjectName, newProjectName),
          projectConfigurationPlatformsLines: templateProject.ProjectConfigurationPlatformsLines));

      baseSln.Save();
    }

    private static IEnumerable<PropertyLine> SetVersionControlLines(Project templateProject, string templateProjectName, string newProjectName) {
      List<PropertyLine> propLines = new List<PropertyLine>();

      foreach (var vcl in templateProject.VersionControlLines) {
        propLines.Add(new PropertyLine(vcl.Name, vcl.Value.Replace(templateProjectName, newProjectName)));
      }

      return propLines;
    }
  }
}
