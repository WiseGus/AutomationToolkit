using System.Collections.Generic;

namespace AutomationToolkit.Core.Model;

public class AutomationUpdates
{
public bool UseAutomationUpdates { get; set; }
public string AutomationUpdatesPath { get; set; }
public Dictionary<string, string> AutomationUpdatesArgs { get; set; }

public AutomationUpdates()
{
  AutomationUpdatesArgs = new Dictionary<string, string>();
}
}
