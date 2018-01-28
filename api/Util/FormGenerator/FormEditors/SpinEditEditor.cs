namespace Api.Util.FormGenerator.FormEditors
{

  public abstract class SpinEditEditor : IFormEditorInfo, IApplyFormEditor
  {
    public AssignType AssignType => AssignType.Int32;
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

    public SpinEditEditor() { }

    public SpinEditEditor(string name, string caption, string bindingSourceName, bool isCrm)
    {
      _name = name;
      _caption = caption;
      _bindingSourceName = bindingSourceName;
      _namespacePrefix = isCrm ? "Crm" : "Glx";
      _controlPrefix = isCrm ? "cm" : "gx";
    }

    public string AddDeclaration()
    {
      return $@"private {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}SpinEdit {ControlName};
                private {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}LayoutControlItem {LayoutName};";
    }

    public string AddInstantiation()
    {
      return $@"this.{ControlName} = new {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}SpinEdit();
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
                this.{ControlName}.EditValue = new decimal(new int[] {{
                      0,0,
                      0,
                      0}});
                this.{ControlName}.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {{
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)}});
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

  public class cmSpinEditEditor : SpinEditEditor
  {
    public override bool IsDefaultForAssignType => false;
    public override string EditorName => "cmSpinEdit";
    public override string Category => "Crm";

    public cmSpinEditEditor() { }

    public cmSpinEditEditor(string name, string caption, string bindingSourceName)
      : base(name, caption, bindingSourceName, true) { }
  }

  public class gxSpinEditEditor : SpinEditEditor
  {
    public override bool IsDefaultForAssignType => false;
    public override string EditorName => "gxSpinEdit";
    public override string Category => "Glx";

    public gxSpinEditEditor() { }

    public gxSpinEditEditor(string name, string caption, string bindingSourceName)
      : base(name, caption, bindingSourceName, true) { }
  }

}
