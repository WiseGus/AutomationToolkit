namespace Api.Util.FormGenerator.FormEditors {

  public abstract class LookupSelectorEditor : BaseEditor, IApplyFormEditor {
    public AssignType AssignType => AssignType.Guid;
    public abstract bool IsDefaultForAssignType { get; }
    public abstract string EditorName { get; }
    public abstract string Category { get; }

    public override string ControlName => "ctrl" + _name;
    public override string LayoutName => "lo" + _name;

    private string _name;
    private string _caption;
    private string _bindingSourceName;
    private string _namespacePrefix;
    private string _controlPrefix;
    private string _definition;

    public LookupSelectorEditor() { }

    public LookupSelectorEditor(string name, string caption, string bindingSourceName, bool isCrm, string definition) {
      _name = name;
      _caption = caption;
      _bindingSourceName = bindingSourceName;
      _namespacePrefix = isCrm ? "Crm" : "Glx";
      _controlPrefix = isCrm ? "cm" : "gx";
      _definition = definition;
    }

    protected override string AddDeclaration() {
      return $@"private {_namespacePrefix}.Core.WinControls.AdvControls.{_controlPrefix}LookupSelector {ControlName};
                private {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}LayoutControlItem {LayoutName};";
    }

    protected override string AddInstantiation() {
      return $@"this.{ControlName} = new {_namespacePrefix}.Core.WinControls.AdvControls.{_controlPrefix}LookupSelector();
                this.{LayoutName}= new {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}LayoutControlItem();";
    }

    protected override string AddISupportInitializeBegin() {
      return $@"((System.ComponentModel.ISupportInitialize)(this.{ControlName}.Properties)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.{ControlName})).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.{LayoutName})).BeginInit();";
    }

    protected override string AddISupportInitializeEnd() {
      return $@"((System.ComponentModel.ISupportInitialize)(this.{ControlName}.Properties)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.{ControlName})).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.{LayoutName})).EndInit();";
    }

    protected override string AddPropsSetup() {
      return $@"// 
                // {ControlName}
                //
                this.{ControlName}.DataBindings.Add(new System.Windows.Forms.Binding(""EditValue"", this.{_bindingSourceName}, ""{ControlName}"", true));
                this.{ControlName}.AutoFetchData = true;
                this.{ControlName}.EditValue = ""{ControlName}"";            
                this.{ControlName}.LookupDefinition = {_definition};
                this.{ControlName}.Name = ""{ControlName}"";
                this.{ControlName}.Properties.AutoFetchData = true;
                this.{ControlName}.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText;
                this.{ControlName}.Properties.ShowEditFormButton = false;
                this.{ControlName}.ShowEditFormButton = false;
                this.{ControlName}.StyleController = this.MainLayout;
                this.{ControlName}.ToolTip = null;
                this.{ControlName}.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Application;
                this.{ControlName}.ToolTipTitle = null;
                // 
                // {LayoutName}
                // 
                this.{ LayoutName}.BackColor = System.Drawing.Color.White;
                this.{ LayoutName}.Control = this.{ ControlName};
                this.{ LayoutName}.Name = ""{ LayoutName}"";
                this.{ LayoutName}.ReadOnly = false;
                this.{ LayoutName}.Text = ""{ _caption}"";
                this.{ LayoutName}.TextSize = new System.Drawing.Size(50, 13); ";
    }
  }

  public class cmLookupSelectorEditor : LookupSelectorEditor {
    public override bool IsDefaultForAssignType => false;
    public override string EditorName => "cmLookupSelector";
    public override string Category => "Crm";

    public cmLookupSelectorEditor() { }

    public cmLookupSelectorEditor(string name, string caption, string bindingSourceName, string definition)
      : base(name, caption, bindingSourceName, true, definition) { }
  }

  public class gxLookupSelectorEditor : LookupSelectorEditor {
    public override bool IsDefaultForAssignType => false;
    public override string EditorName => "gxLookupSelector";
    public override string Category => "Glx";

    public gxLookupSelectorEditor() { }

    public gxLookupSelectorEditor(string name, string caption, string bindingSourceName, string definition)
      : base(name, caption, bindingSourceName, true, definition) { }
  }

}
