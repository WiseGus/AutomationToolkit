using AutomationToolkit.Core.FormGenerator;
using AutomationToolkit.Core.FormGenerator.FormEditorFactories;
using AutomationToolkit.Core.FormGenerator.Model;
using AutomationToolkit.Core.FormGenerator.Visitors;
using AutomationToolkit.Infrastructure.FormGenerator.FormEditors;
using AutomationToolkit.Infrastructure.FormGenerator.Visitors;
using SLnet.Sand.Schema;

namespace AutomationToolkit.Infrastructure.FormGenerator.FormEditorFactories;

public class GlxFormEditorFactory : IFormEditorFactory
{

private slsSchemaTable _schemaTable;

public GlxFormEditorFactory(slsSchemaTable schemaTable)
{
  _schemaTable = schemaTable;
}

public IApplyFormEditor Create(FormEditorInfo info)
{
  if (info.FormEditor.StartsWith("gx"))
  {
    return DoCreateGlxEditor(info);
  }
  else
  {
    return DoCreateCrmEditor(info);
  }
}

private IApplyFormEditor DoCreateGlxEditor(FormEditorInfo info)
{
  IApplyFormEditor editor;

  switch (info.FormEditor)
  {
    case "gxCalcEdit":
      editor = new gxCalcEditEditor(info.Name, GetFieldDescription(info.Name));
      break;
    case "gxCheckEdit":
      editor = new gxCheckEditEditor(info.Name, GetFieldDescription(info.Name));
      break;
    case "gxDateEdit":
      editor = new gxDateEditEditor(info.Name, GetFieldDescription(info.Name));
      break;
    case "gxLookupEnumeration":
      editor = new gxLookupEnumerationEditor(info.Name, GetFieldDescription(info.Name), "");
      break;
    case "gxLookupSelector":
      editor = new gxLookupSelectorEditor(info.Name, GetFieldDescription(info.Name), "");
      break;
    case "gxMasterSelector":
      editor = new gxMasterSelectorEditor(info.Name, GetFieldDescription(info.Name), "");
      break;
    case "gxSpinEdit":
      editor = new gxSpinEditEditor(info.Name, GetFieldDescription(info.Name));
      break;
    case "gxObjectCollectionSource":
      editor = new gxObjectCollectionSourceEditor(info.Name);
      break;
    case "gxBindingSource":
      editor = new gxBindingSourceEditor(info.Name);
      break;
    case "gxErrorProvider":
      editor = new gxErrorProviderEditor(info.Name);
      break;
    case "MainLayout":
      editor = new MainLayoutEditor();
      break;
    case "MainLayoutGroup":
      editor = new MainLayoutGroupEditor();
      break;
    case "MainPanel":
      editor = new MainPanelEditor();
      break;
    default:
      editor = new gxTextEditEditor(info.Name, GetFieldDescription(info.Name));
      break;
  }

  return editor;
}

private IApplyFormEditor DoCreateCrmEditor(FormEditorInfo info)
{
  IApplyFormEditor editor;

  switch (info.FormEditor)
  {
    case "cmCalcEdit":
      editor = new cmCalcEditEditor(info.Name, GetFieldDescription(info.Name));
      break;
    case "cmCheckEdit":
      editor = new cmCheckEditEditor(info.Name, GetFieldDescription(info.Name));
      break;
    case "cmDateEdit":
      editor = new cmDateEditEditor(info.Name, GetFieldDescription(info.Name));
      break;
    case "cmLookupEnumeration":
      editor = new cmLookupEnumerationEditor(info.Name, GetFieldDescription(info.Name), "");
      break;
    case "cmLookupSelector":
      editor = new cmLookupSelectorEditor(info.Name, GetFieldDescription(info.Name), "");
      break;
    case "cmMasterSelector":
      editor = new cmMasterSelectorEditor(info.Name, GetFieldDescription(info.Name), "");
      break;
    case "cmSpinEdit":
      editor = new cmSpinEditEditor(info.Name, GetFieldDescription(info.Name));
      break;
    case "cmObjectCollectionSource":
      editor = new cmObjectCollectionSourceEditor(info.Name);
      break;
    case "cmBindingSource":
      editor = new cmBindingSourceEditor(info.Name);
      break;
    case "cmErrorProvider":
      editor = new cmErrorProviderEditor(info.Name);
      break;
    case "MainLayout":
      editor = new MainLayoutEditor();
      break;
    case "MainLayoutGroup":
      editor = new MainLayoutGroupEditor();
      break;
    case "MainPanel":
      editor = new MainPanelEditor();
      break;
    default:
      editor = new cmTextEditEditor(info.Name, GetFieldDescription(info.Name));
      break;
  }

  return editor;
}

private string GetFieldDescription(string fieldName)
{
  var fieldObj = _schemaTable.Fields.Find(p => p.Alias == fieldName);
  return fieldObj.Description ?? fieldObj.Alias;
}
}
