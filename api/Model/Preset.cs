using System;
using System.Collections.Generic;

public class Preset
{
  public string Alias { get; set; }
  public string AliasCategory { get; set; }
  public string Tooltip { get; set; }
  public string ProjectName { get; set; }
  public string TemplateOrigin { get; set; }
  public string OutputFolderPath { get; set; }
  public string FileKeywordTypesExtensions { get; set; }
  public List<Keyword> Keywords { get; set; }
  public bool AddToSourceControl { get; set; }
  public AutomationUpdates AutomationUpdates { get; set; }

  public Preset()
  {
    Keywords = new List<Keyword>();
    AutomationUpdates = new AutomationUpdates();
  }
}
