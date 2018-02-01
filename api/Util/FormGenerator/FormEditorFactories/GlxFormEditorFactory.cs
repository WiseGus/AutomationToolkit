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

    private IApplyFormEditor DoCreateGlxEditor(FormEditorInfo info) {
      IApplyFormEditor editor;

      switch (info.FormEditor) {
        case "gxCalcEditEditor":
          editor = new gxCalcEditEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name, null);
          break;
        case "gxCheckEditEditor":
          editor = new gxCheckEditEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name, null);
          break;
        case "gxDateEditEditor":
          editor = new gxDateEditEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name, null);
          break;
        case "gxLookupEnumerationEditor":
          editor = new gxLookupEnumerationEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name, "", null);
          break;
        case "gxLookupSelectorEditor":
          editor = new gxLookupSelectorEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name, "", null);
          break;
        case "gxMasterSelectorEditor":
          editor = new gxMasterSelectorEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name, "", null);
          break;
        case "gxSpinEditEditor":
          editor = new gxSpinEditEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name, null);
          break;
        default:
          editor = new gxTextEditEditor(info.Name, _schemaTable.Description ?? _schemaTable.Name, null);
          break;
      }

      return editor;
    }

    private IApplyFormEditor DoCreateCrmEditor(FormEditorInfo propInfo) {
      IApplyFormEditor editor;

      switch (propInfo.FormEditor) {
        case "cmCalcEditEditor":
          editor = new cmCalcEditEditor(propInfo.Name, _schemaTable.Description ?? _schemaTable.Name, null);
          break;
        case "cmCheckEditEditor":
          editor = new cmCheckEditEditor(propInfo.Name, _schemaTable.Description ?? _schemaTable.Name, null);
          break;
        case "cmDateEditEditor":
          editor = new cmDateEditEditor(propInfo.Name, _schemaTable.Description ?? _schemaTable.Name, null);
          break;
        case "cmLookupEnumerationEditor":
          editor = new cmLookupEnumerationEditor(propInfo.Name, _schemaTable.Description ?? _schemaTable.Name, "", null);
          break;
        case "cmLookupSelectorEditor":
          editor = new cmLookupSelectorEditor(propInfo.Name, _schemaTable.Description ?? _schemaTable.Name, "", null);
          break;
        case "cmMasterSelectorEditor":
          editor = new cmMasterSelectorEditor(propInfo.Name, _schemaTable.Description ?? _schemaTable.Name, "", null);
          break;
        case "cmSpinEditEditor":
          editor = new cmSpinEditEditor(propInfo.Name, _schemaTable.Description ?? _schemaTable.Name, null);
          break;
        default:
          editor = new cmTextEditEditor(propInfo.Name, _schemaTable.Description ?? _schemaTable.Name, null);
          break;
      }

      return editor;
    }
  }
}
