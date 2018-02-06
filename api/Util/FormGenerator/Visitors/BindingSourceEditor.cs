using Api.Util.FormGenerator.FormEditors;

namespace Api.Util.FormGenerator.Visitors
{

  public abstract class BindingSourceEditor : BaseEditor, IEditorVisitor, IIgnoreVisit
  {
    public override string Name => _name;
    public override string ControlName => "bs" + _name;
    public override string LayoutName => "bs" + _name;

    private string _name;
    private string _namespacePrefix;
    private string _controlPrefix;

    public BindingSourceEditor(string name, bool isCrm)
    {
      _name = name;
      _namespacePrefix = isCrm ? "Crm" : "Glx";
      _controlPrefix = isCrm ? "cm" : "gx";
    }

    protected override string AddDeclaration()
    {
      return $@"private {_namespacePrefix}.Core.WinControls.Data.{_controlPrefix}BindingSource {ControlName};";
    }

    protected override string AddInstantiation()
    {
      return $@"this.{ControlName} = new {_namespacePrefix}.Core.WinControls.Data.{_controlPrefix}BindingSource(this.components);";
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
      return $@"// 
                // {ControlName}
                //";
    }

    public void Visit(IApplyFormEditor editor)
    {

      if (editor is ErrorProviderEditor)
      {
        var data = $@"this.{editor.ControlName}.DataSource = this.{ControlName};";
        editor.PropsSetup.Insert(0, data);
        return;
      }
      
      if (editor is IIgnoreVisit)
      {
        return;
      }

      if (editor is BaseEditor)
      {
        var data = $@"this.{editor.ControlName}.DataBindings.Add(new System.Windows.Forms.Binding(""EditValue"", this.{ControlName}, ""{editor.Name}"", true));";
        editor.PropsSetup.Insert(0, data);
      }
    }
  }

  public class cmBindingSourceEditor : BindingSourceEditor
  {
    public cmBindingSourceEditor(string name)
      : base(name, true) { }
  }

  public class gxBindingSourceEditor : BindingSourceEditor
  {
    public gxBindingSourceEditor(string name)
      : base(name, true) { }
  }

}
