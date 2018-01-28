namespace Api.Util.FormGenerator.FormEditors
{

  public abstract class TextEditEditor : IFormEditorInfo, IApplyFormEditor
  {
    public AssignType AssignType => AssignType.String;
    public abstract bool IsDefaultForAssignType { get; }
    public abstract string EditorName { get; }
    public abstract string Category { get; }

    public string ControlName => "ctrl" + _name;
    public string LayoutName => "lo" + _name;

    private string _name;
    private string _caption;
    private string _bindingSourceName;
    private string _namespacePrefix;
    private string _controlPrefix;

    public TextEditEditor() { }

    public TextEditEditor(string name, string caption, string bindingSourceName, bool isCrm)
    {
      _name = name;
      _caption = caption;
      _bindingSourceName = bindingSourceName;
      _namespacePrefix = isCrm ? "Crm" : "Glx";
      _controlPrefix = isCrm ? "cm" : "gx";
    }

    public string AddDeclaration()
    {
      return $@"private {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}TextEdit {ControlName};
                private {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}LayoutControlItem {LayoutName};";
    }

    public string AddInstantiation()
    {
      return $@"this.{ControlName} = new {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}TextEdit();
                this.{LayoutName}= new {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}LayoutControlItem();";
    }

    public string AddISupportInitializeBegin()
    {
      return $@"((System.ComponentModel.ISupportInitialize)(this.{ControlName}.Properties)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.{LayoutName})).BeginInit();";
    }

    public string AddISupportInitializeEnd()
    {
      return $@"((System.ComponentModel.ISupportInitialize)(this.{ControlName}.Properties)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.{LayoutName})).EndInit();";
    }

    public string AddPropsSetup()
    {
      return $@"// 
                // {ControlName}
                //
                this.{ControlName}.DataBindings.Add(new System.Windows.Forms.Binding(""EditValue"", this.{_bindingSourceName}, ""{ControlName}"", true));
                this.{ControlName}.Name = ""{ControlName}"";
                this.{ControlName}.StyleController = this.MainLayout;
                // 
                // {LayoutName}
                // 
                this.{LayoutName}.BackColor = System.Drawing.Color.White;
                this.{LayoutName}.Control = this.{ControlName};
                this.{LayoutName}.Name = ""{LayoutName}"";
                this.{LayoutName}.ReadOnly = false;
                this.{LayoutName}.Text = ""{_caption}"";
                this.{LayoutName}.TextSize = new System.Drawing.Size(50, 13);";
    }
  }

  public class cmTextEditEditor : TextEditEditor
  {
    public override bool IsDefaultForAssignType => true;
    public override string EditorName => "cmTextEdit";
    public override string Category => "Crm";

    public cmTextEditEditor() { }

    public cmTextEditEditor(string name, string caption, string bindingSourceName)
      : base(name, caption, bindingSourceName, true) { }
  }

  public class gxTextEditEditor : TextEditEditor
  {
    public override bool IsDefaultForAssignType => true;
    public override string EditorName => "gxTextEdit";
    public override string Category => "Glx";

    public gxTextEditEditor() { }

    public gxTextEditEditor(string name, string caption, string bindingSourceName)
      : base(name, caption, bindingSourceName, true) { }
  }

}
