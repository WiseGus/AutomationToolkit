using Api.Util.FormGenerator.FormEditors;
using System;
using System.Collections.Generic;

namespace Api.Util.FormGenerator.Visitors
{

  public class MainLayoutEditor : BaseEditor, IEditorVisitor, IIgnoreVisit
  {
    private List<string> _editors = new List<string>();

    public override string Name => "MainLayout";
    public override string ControlName => "MainLayout";
    public override string LayoutName => "MainLayout";

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
                this.MainLayout.SuspendLayout();";
    }

    protected override string AddISupportInitializeEnd()
    {
      return $@"((System.ComponentModel.ISupportInitialize)(this.{ControlName})).EndInit();
                this.{ControlName}.ResumeLayout(false);";
    }

    protected override string AddPropsSetup()
    {
      var res = new List<string>();
      res.Add("//");
      res.Add($"// {ControlName}");
      res.Add("//");
      _editors.ForEach(editor =>
      {
        res.Add($"this.{ControlName}.Controls.Add(this.{editor});");
      });
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
        var data = $@"this.{editor.ControlName}.StyleController = this.{ControlName};";
        editor.PropsSetup.Add(data);

        _editors.Add(editor.ControlName);
      }
    }
  }
}
