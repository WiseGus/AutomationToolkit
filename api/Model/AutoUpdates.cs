using System;

[Serializable]
public class AutoUpdates
{
    public bool UpdateEntities { get; set; }
    public bool UpdatePresenters { get; set; }
    public bool UpdateWinforms { get; set; }
    public bool UpdateWpfforms { get; set; }
    public bool UpdateCoreBase { get; set; }
    public bool UpdateCoreMessages { get; set; }
    public bool UpdateActionManager { get; set; }
    public bool UpdateFormsCustomization { get; set; }
    public bool UpdateObjectsCustomization { get; set; }
    public bool UpdateWorkflowsCustomization { get; set; }
    public bool IsCrmProject { get; set; }
}
