using AutomationToolkit.Core.FormGenerator;
using AutomationToolkit.Core.FormGenerator.FormEditors;
using AutomationToolkit.Core.FormGenerator.Visitors;

namespace AutomationToolkit.Infrastructure.FormGenerator.Visitors;

public abstract class ObjectCollectionSourceEditor : BaseEditor, IEditorVisitor, IIgnoreVisit
{
public override string Name => _name;
public override string ControlName => "oc" + _name;
public override string LayoutName => "oc" + _name;

private string _name;
private string _namespacePrefix;
private string _controlPrefix;

public ObjectCollectionSourceEditor(string name, bool isCrm)
{
  _name = name;
  _namespacePrefix = isCrm ? "Crm" : "Glx";
  _controlPrefix = isCrm ? "cm" : "gx";
}

protected override string AddDeclaration()
{
  return $@"private {_namespacePrefix}.Core.WinControls.Data.{_controlPrefix}ObjectCollectionSource {ControlName};";
}

protected override string AddInstantiation()
{
  return $@"this.{ControlName} = new {_namespacePrefix}.Core.WinControls.Data.{_controlPrefix}ObjectCollectionSource(this.components);";
}

protected override string AddISupportInitializeBegin()
{
  return string.Empty;
}

protected override string AddISupportInitializeEnd()
{
  return string.Empty;
}

protected override string AddPropsSetup()
{
  return $@"// 
                // {ControlName}
                //
                this.{ControlName}.CollectionType = typeof({_namespacePrefix}.Data.DataObjects.{_controlPrefix}{_name}Collection);";
}

public void Visit(IApplyFormEditor editor)
{
  if (editor is BindingSourceEditor)
  {
    var data = $@"this.{editor.ControlName}.DataSource = this.{ControlName};";
    editor.PropsSetup.Insert(0, data);
  }

  if (editor is IIgnoreVisit)
  {
    return;
  }
}
}

public class cmObjectCollectionSourceEditor : ObjectCollectionSourceEditor
{
public cmObjectCollectionSourceEditor(string name)
  : base(name, true) { }
}

public class gxObjectCollectionSourceEditor : ObjectCollectionSourceEditor
{
public gxObjectCollectionSourceEditor(string name)
  : base(name, true) { }
}

