﻿using AutomationToolkit.Core.FormGenerator;
using AutomationToolkit.Core.FormGenerator.FormEditors;
namespace AutomationToolkit.Infrastructure.FormGenerator.FormEditors;

public abstract class DateEditEditor : BaseEditor, IFormEditorInfo
{
    public AssignType AssignType => AssignType.DateTime;
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

    public DateEditEditor() { }

    public DateEditEditor(string name, string caption, bool isCrm)
    {
        _name = name;
        _caption = caption;
        _namespacePrefix = isCrm ? "Crm" : "Glx";
        _controlPrefix = isCrm ? "cm" : "gx";
    }

    protected override string AddDeclaration()
    {
        return $@"private {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}DateEdit {ControlName};
                private {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}LayoutControlItem {LayoutName};";
    }

    protected override string AddInstantiation()
    {
        return $@"this.{ControlName} = new {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}DateEdit();
                this.{LayoutName} = new {_namespacePrefix}.Core.WinControls.DevExp.{_controlPrefix}LayoutControlItem();";
    }

    protected override string AddISupportInitializeBegin()
    {
        return $@"((System.ComponentModel.ISupportInitialize)(this.{ControlName}.Properties.CalendarTimeProperties)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.{ControlName}.Properties)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.{LayoutName})).BeginInit();";
    }

    protected override string AddISupportInitializeEnd()
    {
        return $@"((System.ComponentModel.ISupportInitialize)(this.{ControlName}.Properties.CalendarTimeProperties)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.{ControlName}.Properties)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.{LayoutName})).EndInit();";
    }

    protected override string AddPropsSetup()
    {
        return $@"// 
                // {ControlName}
                //
                this.{ControlName}.Name = ""{ControlName}"";
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

public class cmDateEditEditor : DateEditEditor
{
    public override bool IsDefaultForAssignType => true;
    public override string EditorName => "cmDateEdit";
    public override string Category => "Crm";

    public cmDateEditEditor() { }

    public cmDateEditEditor(string name, string caption)
      : base(name, caption, true) { }
}

public class gxDateEditEditor : DateEditEditor
{
    public override bool IsDefaultForAssignType => true;
    public override string EditorName => "gxDateEdit";
    public override string Category => "Glx";

    public gxDateEditEditor() { }

    public gxDateEditEditor(string name, string caption)
      : base(name, caption, true) { }
}

