using System;
using System.Collections.Generic;
using CWDev.SLNTools.Core;

namespace AutomationToolkit.Infrastructure;

public class SolutionParser
{

    private string _solutionPath;

    public SolutionParser(string solutionPath)
    {
        _solutionPath = solutionPath;
    }

    public void AddProject(string newProjectName)
    {
        var baseSln = SolutionFile.FromFile(_solutionPath);

        var templateProject = GetTemplateProject(baseSln.Projects);

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

    private Project GetTemplateProject(ProjectHashList projects)
    {
        int i = 0;

        Project templateProject;
        while (true)
        {
            templateProject = projects[i++];

            var relativeLevels = templateProject.RelativePath.Split("..", 2, StringSplitOptions.RemoveEmptyEntries);
            if (relativeLevels.Length == 1) break;
        }

        return templateProject;
    }

    private static IEnumerable<PropertyLine> SetVersionControlLines(Project templateProject, string templateProjectName, string newProjectName)
    {
        List<PropertyLine> propLines = new List<PropertyLine>();

        foreach (var vcl in templateProject.VersionControlLines)
        {
            propLines.Add(new PropertyLine(vcl.Name, vcl.Value.Replace(templateProjectName, newProjectName)));
        }

        return propLines;
    }
}
