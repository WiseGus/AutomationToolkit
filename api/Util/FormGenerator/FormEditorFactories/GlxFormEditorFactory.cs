using System;
using Api.Util.FormGenerator.FormEditors;
using Api.Util.FormGenerator.Model;
using SLnet.Sand.Schema;

namespace Api.Util.FormGenerator.FormEditorFactories {
  public class GlxFormEditorFactory : IFormEditorFactory {

    private slsSchemaTable _schemaTable;

    public GlxFormEditorFactory(slsSchemaTable schemaTable) {
      _schemaTable = schemaTable;
    }

    public IApplyFormEditor Create(FormEditorInfo info) {
      if (info.FormEditor.StartsWith("gx")) {
        return DoCreateGlxEditor(info);
      }
      else {
        return DoCreateCrmEditor(info);
      }
    }

    private IApplyFormEditor DoCreateGlxEditor(FormEditorInfo propInfo) {
      IApplyFormEditor editor;

      switch (propInfo.FormEditor) {
        case "gxCalcEditEditor":
          editor = new gxCalcEditEditor(propInfo.Name, GetFieldDescription(propInfo.Name), null);
          break;
        case "gxCheckEditEditor":
          editor = new gxCheckEditEditor(propInfo.Name, GetFieldDescription(propInfo.Name), null);
          break;
        case "gxDateEditEditor":
          editor = new gxDateEditEditor(propInfo.Name, GetFieldDescription(propInfo.Name), null);
          break;
        case "gxLookupEnumerationEditor":
          editor = new gxLookupEnumerationEditor(propInfo.Name, GetFieldDescription(propInfo.Name), "", null);
          break;
        case "gxLookupSelectorEditor":
          editor = new gxLookupSelectorEditor(propInfo.Name, GetFieldDescription(propInfo.Name), "", null);
          break;
        case "gxMasterSelectorEditor":
          editor = new gxMasterSelectorEditor(propInfo.Name, GetFieldDescription(propInfo.Name), "", null);
          break;
        case "gxSpinEditEditor":
          editor = new gxSpinEditEditor(propInfo.Name, GetFieldDescription(propInfo.Name), null);
          break;
        default:
          editor = new gxTextEditEditor(propInfo.Name, GetFieldDescription(propInfo.Name), null);
          break;
      }

      return editor;
    }

    private IApplyFormEditor DoCreateCrmEditor(FormEditorInfo propInfo) {
      IApplyFormEditor editor;

      switch (propInfo.FormEditor) {
        case "cmCalcEditEditor":
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
