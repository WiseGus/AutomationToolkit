using System;
using System.Collections.Generic;

[Serializable]
public class Preset
{
    public string Name { get; set; }
    public string TemplateOrigin { get; set; }
    public List<string> FileTypesExtensions { get; set; }
    public Dictionary<string, string> Keywords { get; set; }
    public AutoUpdates AutoUpdates { get; set; }

    public Preset()
    {
        FileTypesExtensions = new List<string>();
        Keywords = new Dictionary<string, string>();
        AutoUpdates = new AutoUpdates();
    }
}
