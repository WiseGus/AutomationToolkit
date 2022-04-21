namespace Core.FormGenerator.FormEditors
{

  public abstract class MasterSelectorEditor : BaseEditor, IFormEditorInfo
  {
    public AssignType AssignType => AssignType.Guid;
    public abstract bool IsDefaultForAssignType { get; }
    public abstract string EditorName { get; }
    public abstract string Category { get; }

    public override string Name => _name;
    public override string ControlName => "ctrl" + _name;
    public override string LayoutName => "lo" + _name;

    private string _name;
    private string _caption;
    private string _namespacePrefix;
    private string _controlPrefix;
    private string _definition;

    public MasterSelectorEditor() { }

    public MasterSelectorEditor(string name, string caption, bool isCrm, string definition)
    {
      _name = name;
      _caption = caption;
      _namespacePrefix = isCrm ? "Crm" : "Glx";
      _controlPrefix = isCrm ? "cm" : "gx";
      _definition = definition;
    }

    protected override string AddDeclaration()
    {
      return $@"private {_namespacePrefix}.Core.WinControls.AdvControls.{_controlPrefix}MasterSelector {ControlName};
                private {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}LayoutControlItem {LayoutName};";
    }

    protected override string AddInstantiation()
    {
      return $@"this.{ControlName} = new {_namespacePrefix}.Core.WinControls.AdvControls.{_controlPrefix}MasterSelector();
                this.{LayoutName} = new {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}LayoutControlItem();";
    }

    protected override string AddISupportInitializeBegin()
    {
      return $@"((System.ComponentModel.ISupportInitialize)(this.{ControlName}.Properties)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.{ControlName})).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.{LayoutName})).BeginInit();";
    }

    protected override string AddISupportInitializeEnd()
    {
      return $@"((System.ComponentModel.ISupportInitialize)(this.{ControlName}.Properties)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.{ControlName})).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.{LayoutName})).EndInit();";
    }

    protected override string AddPropsSetup()
    {
      return $@"// 
                // {ControlName}
                //
                this.{ControlName}.AllowMultiSelect = false;
                this.{ControlName}.DisplayTextField1 = null;
                this.{ControlName}.DisplayTextField2 = null;
                this.{ControlName}.EditValue = ""{ControlName}"";
                this.{ControlName}.FilterFields = null;
                this.{ControlName}.MasterDefinition = ""{_definition}"";
                this.{ControlName}.Name = ""{ControlName}"";
                this.{ControlName}.OtherRelativeFields = null;
                this.{ControlName}.Properties.AllowMultiSelect = false;
                this.{ControlName}.Properties.DisplayField = SLnet.Sand.WinControls.AdvControls.Repository.slsRepositoryMasterSelector.DisplayFieldType.DisplayField1;
                this.{ControlName}.Properties.DisplayTextField = null;
                this.{ControlName}.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText;
                this.{ControlName}.Properties.FilterFields = null;
                this.{ControlName}.Properties.HideRelativeBrowsers = false;
                this.{ControlName}.Properties.OtherRelativeFields = null;
                this.{ControlName}.Properties.ShowActionsButton = false;
                this.{ControlName}.Properties.ShowEditFormButton = false;
                this.{ControlName}.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                this.{ControlName}.ShowActionsButton = false;
                this.{ControlName}.ShowEditFormButton = false;
                this.{ControlName}.ToolTip = null;
                this.{ControlName}.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Application;
                this.{ ControlName}.ToolTipTitle = null;
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

  public class cmMasterSelectorEditor : MasterSelectorEditor
  {
    public override bool IsDefaultForAssignType => false;
    public override string EditorName => "cmMasterSelector";
    public override string Category => "Crm";

    public cmMasterSelectorEditor() { }

    public cmMasterSelectorEditor(string name, string caption, string definition)
      : base(name, caption, true, definition) { }
  }

  public class gxMasterSelectorEditor : MasterSelectorEditor
  {
    public override bool IsDefaultForAssignType => false;
    public override string EditorName => "gxMasterSelector";
    public override string Category => "Glx";

    public gxMasterSelectorEditor() { }

    public gxMasterSelectorEditor(string name, string caption, string definition)
      : base(name, caption, true, definition) { }
  }

}
