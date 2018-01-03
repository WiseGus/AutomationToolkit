using System;
using System.Collections.Generic;

[Serializable]
public class Preset
{
    public string Alias { get; set; }
    public string AliasCategory { get; set; }
    public string ProjectName { get; set; }
    public string TemplateOrigin { get; set; }
    public string OutputFolderPath { get; set; }
    public string FileKeywordTypesExtensions { get; set; }
    public List<Keyword> Keywords { get; set; }
    public AutoUpdates AutoUpdates { get; set; }

    public Preset()
    {
        Keywords = new List<Keyword>();
        AutoUpdates = new AutoUpdates();
    }
}
