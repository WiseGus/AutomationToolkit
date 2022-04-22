using AutomationToolkit.Core.FormGenerator;
using AutomationToolkit.Core.FormGenerator.FormEditors;
using System.Collections.Generic;

namespace AutomationToolkit.Infrastructure.FormGenerator.FormEditors;

public class MainPanelEditor : BaseEditor, IIgnoreVisit
{
private List<string> _editors = new List<string>();

public override string Name => "MainPanel";
public override string ControlName => "MainPanel";
public override string LayoutName => "MainPanel";

protected override string AddDeclaration()
{
  return string.Empty;
}

protected override string AddInstantiation()
{
  return string.Empty;
}

protected override string AddISupportInitializeBegin()
{
  return $@"((System.ComponentModel.ISupportInitialize)(this.{ControlName})).BeginInit();
                this.MainPanel.SuspendLayout();";
}

protected override string AddISupportInitializeEnd()
{
  return $@"((System.ComponentModel.ISupportInitialize)(this.{ControlName})).EndInit();
                this.MainPanel.ResumeLayout(false);";
}

protected override string AddPropsSetup()
{
  return $@"// 
                // {ControlName}
                // 
                this.{ControlName}.Size = new System.Drawing.Size(890, 568);";
}
}
