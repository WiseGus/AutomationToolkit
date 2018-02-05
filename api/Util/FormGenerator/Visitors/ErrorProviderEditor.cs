using Api.Util.FormGenerator.FormEditors;

namespace Api.Util.FormGenerator.FormEditors
{

  public abstract class ErrorProviderEditor : BaseEditor, IEditorVisitor
  {
    public override string Name => _name;
    public override string ControlName => "ep" + _name;

    private string _name;
    private string _namespacePrefix;
    private string _controlPrefix;

    public ErrorProviderEditor(string name, bool isCrm)
    {
      _name = name;
      _namespacePrefix = isCrm ? "Crm" : "Glx";
      _controlPrefix = isCrm ? "cm" : "gx";
    }

    protected override string AddDeclaration()
    {
      return $@"private {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}ErrorProvider {ControlName};";
    }

    protected override string AddInstantiation()
    {
      return $@"this.{ControlName} = new {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}ErrorProvider(this.components);";
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
                //
                this.{ControlName}.ContainerControl = this;";
    }

    public void Visit(IApplyFormEditor editor)
    {
    }
  }

  public class cmErrorProviderEditor : ErrorProviderEditor
  {
    public cmErrorProviderEditor(string name)
      : base(name, true) { }
  }

  public class gxErrorProviderEditor : ErrorProviderEditor
  {
    public gxErrorProviderEditor(string name)
      : base(name, true) { }
  }

}
