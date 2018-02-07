using Api.Util.FormGenerator.FormEditors;
using System;
using System.Collections.Generic;

namespace Api.Util.FormGenerator.Visitors
{

  public class MainLayoutGroupEditor : BaseEditor, IEditorVisitor, IIgnoreVisit
  {
    private List<string> _editors = new List<string>();

    public override string Name => "MainLayoutGroup";
    public override string ControlName => "MainLayoutGroup";
    public override string LayoutName => "MainLayoutGroup";

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
      return $@"((System.ComponentModel.ISupportInitialize)(this.{ControlName})).BeginInit();";
    }

    protected override string AddISupportInitializeEnd()
    {
      return $@"((System.ComponentModel.ISupportInitialize)(this.{ControlName})).EndInit();";
    }

    protected override string AddPropsSetup()
    {
      var res = new List<string>();
      res.Add("//");
      res.Add($"// {ControlName}");
      res.Add("//");
      res.Add($"this.{ControlName}.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {{");
      _editors.ForEach(editor =>
      {
        res.Add($"this.{editor},");
      });
      res.Add("});");
      return string.Join(Environment.NewLine, res);
    }

    public void Visit(IApplyFormEditor editor)
    {
      if (editor is IIgnoreVisit)
      {
        return;
      }

      if (editor is BaseEditor)
      {
        _editors.Add(editor.LayoutName);
      }
    }
  }
}
