using System;
using Api.Util.FormGenerator.FormEditors;
using Api.Util.FormGenerator.Model;
using SLnet.Sand.Schema;

namespace Api.Util.FormGenerator.FormEditorFactories
{
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
        case "gxCalcEditEditor":
          editor = new gxCalcEditEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name);
          break;
        case "gxCheckEditEditor":
          editor = new gxCheckEditEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name);
          break;
        case "gxDateEditEditor":
          editor = new gxDateEditEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name);
          break;
        case "gxLookupEnumerationEditor":
          editor = new gxLookupEnumerationEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name, "");
          break;
        case "gxLookupSelectorEditor":
          editor = new gxLookupSelectorEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name, "");
          break;
        case "gxMasterSelectorEditor":
          editor = new gxMasterSelectorEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name, "");
          break;
        case "gxSpinEditEditor":
          editor = new gxSpinEditEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name);
          break;
        case "gxObjectCollectionSourceEditor":
          editor = new gxObjectCollectionSourceEditor(info.Name);
          break;
        case "gxBindingSourceEditor":
          editor = new gxBindingSourceEditor(info.Name);
          break;
        case "gxErrorProviderEditor":
          editor = new gxErrorProviderEditor(info.Name);
          break;
        default:
          editor = new gxTextEditEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name);
          break;
      }

      return editor;
    }

    private IApplyFormEditor DoCreateCrmEditor(FormEditorInfo info)
    {
      IApplyFormEditor editor;

      switch (info.FormEditor)
      {
        case "cmCalcEditEditor":
<<<<<<< HEAD
          editor = new cmCalcEditEditor(propInfo.Name, GetFieldDescription(propInfo.Name), null);
          break;
        case "cmCheckEditEditor":
          editor = new cmCheckEditEditor(propInfo.Name, GetFieldDescription(propInfo.Name), null);
          break;
        case "cmDateEditEditor":
          editor = new cmDateEditEditor(propInfo.Name, GetFieldDescription(propInfo.Name), null);
          break;
        case "cmLookupEnumerationEditor":
          editor = new cmLookupEnumerationEditor(propInfo.Name, GetFieldDescription(propInfo.Name), "", null);
          break;
        case "cmLookupSelectorEditor":
          editor = new cmLookupSelectorEditor(propInfo.Name, GetFieldDescription(propInfo.Name), "", null);
          break;
        case "cmMasterSelectorEditor":
          editor = new cmMasterSelectorEditor(propInfo.Name, GetFieldDescription(propInfo.Name), "", null);
          break;
        case "cmSpinEditEditor":
          editor = new cmSpinEditEditor(propInfo.Name, GetFieldDescription(propInfo.Name), null);
          break;
        default:
          editor = new cmTextEditEditor(propInfo.Name, GetFieldDescription(propInfo.Name), null);
=======
          editor = new cmCalcEditEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name);
          break;
        case "cmCheckEditEditor":
          editor = new cmCheckEditEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name);
          break;
        case "cmDateEditEditor":
          editor = new cmDateEditEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name);
          break;
        case "cmLookupEnumerationEditor":
          editor = new cmLookupEnumerationEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name, "");
          break;
        case "cmLookupSelectorEditor":
          editor = new cmLookupSelectorEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name, "");
          break;
        case "cmMasterSelectorEditor":
          editor = new cmMasterSelectorEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name, "");
          break;
        case "cmSpinEditEditor":
          editor = new cmSpinEditEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name);
          break;
        case "cmObjectCollectionSourceEditor":
          editor = new cmObjectCollectionSourceEditor(info.Name);
          break;
        case "cmBindingSourceEditor":
          editor = new cmBindingSourceEditor(info.Name);
          break;
        case "cmErrorProviderEditor":
          editor = new cmErrorProviderEditor(info.Name);
          break;
        default:
          editor = new cmTextEditEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name);
>>>>>>> 225e0940144c4df94036fd87fb80c6ecfcfe9d04
          break;
      }

      return editor;
    }

    private string GetFieldDescription(string fieldName) {
      var fieldObj = _schemaTable.Fields.Find(p => p.Alias == fieldName);
      return fieldObj.Description ?? fieldObj.Alias;
    }
  }
}
