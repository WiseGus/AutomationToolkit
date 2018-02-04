namespace Api.Util.FormGenerator.FormEditors {

  public abstract class CheckEditEditor : BaseEditor, IApplyFormEditor {
    public AssignType AssignType => AssignType.Int16 | AssignType.Boolean;
    public abstract bool IsDefaultForAssignType { get; }
    public abstract string EditorName { get; }
    public abstract string Category { get; }

    public override string Name => _name;
    public override string ControlName => "ctrl" + _name;
    public override string LayoutName => "lo" + _name;

    private string _name;
    private string _caption;
    private string _bindingSourceName;
    private string _namespacePrefix;
    private string _controlPrefix;

    public CheckEditEditor() { }

    public CheckEditEditor(string name, string caption, bool isCrm) {
      _name = name;
      _caption = caption;
      _namespacePrefix = isCrm ? "Crm" : "Glx";
      _controlPrefix = isCrm ? "cm" : "gx";
    }

    protected override string AddDeclaration() {
      return $@"private {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}CheckEdit {ControlName};
                private {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}LayoutControlItem {LayoutName};";
    }

    protected override string AddInstantiation() {
      return $@"this.{ControlName} = new {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}CheckEdit();
                this.{LayoutName}= new {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}LayoutControlItem();";
    }

    protected override string AddISupportInitializeBegin() {
      return $@"((System.ComponentModel.ISupportInitialize)(this.{ControlName}.Properties)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.{LayoutName})).BeginInit();";
    }

    protected override string AddISupportInitializeEnd() {
      return $@"((System.ComponentModel.ISupportInitialize)(this.{ControlName}.Properties)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.{LayoutName})).EndInit();";
    }

    protected override string AddPropsSetup() {
      return $@"// 
                // {ControlName}
                //
                this.{ControlName}.Name = ""{ControlName}"";
                this.{ControlName}.StyleController = this.MainLayout;
                this.{ControlName}.Properties.Caption = ""{_caption}"";
                // 
                // {LayoutName}
                // 
                this.{LayoutName}.BackColor = System.Drawing.Color.White;
                this.{LayoutName}.Control = this.{ControlName};
                this.{LayoutName}.Name = ""{LayoutName}"";
                this.{LayoutName}.ReadOnly = false;
                this.{LayoutName}.Text = ""{_caption}"";
                this.{LayoutName}.TextSize = new System.Drawing.Size(50, 13);
                this.{LayoutName}.TextVisible = false;";
    }
  }

  public class cmCheckEditEditor : CheckEditEditor {
    public override bool IsDefaultForAssignType => true;
    public override string EditorName => "cmCheckEdit";
    public override string Category => "Crm";

    public cmCheckEditEditor() { }

    public cmCheckEditEditor(string name, string caption)
      : base(name, caption, true) { }
  }

  public class gxCheckEditEditor : CheckEditEditor {
    public override bool IsDefaultForAssignType => true;
    public override string EditorName => "gxCheckEdit";
    public override string Category => "Glx";

    public gxCheckEditEditor() { }

    public gxCheckEditEditor(string name, string caption)
      : base(name, caption, true) { }
  }

}
