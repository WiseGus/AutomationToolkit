using System.Collections.Generic;

namespace AutomationToolkit.Core.FormGenerator.Model;

public class FormGenInfo {
public bool IsGlxSchema { get; set; }
public string TableXmlPath { get; set; }
public string AssemblyPath { get; set; }
public string ClassFullName { get; set; }
public List<FormEditorInfo> PropertiesInfo { get; set; }
}

public class FormEditorInfo {
public string Name { get; set; }
public string FormEditor { get; set; }
}
