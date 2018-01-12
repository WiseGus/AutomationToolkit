using System.Collections.Generic;

public class AutomationUpdates
{
  public bool UseAutomationUpdates { get; set; }
  public string AutomationUpdatesPath { get; set; }
  public string AutomationUpdatesMode { get; set; }
  public Dictionary<string, string> AutomationUpdatesArgs { get; set; }

  public AutomationUpdates()
  {
    AutomationUpdatesArgs = new Dictionary<string, string>();
  }
}