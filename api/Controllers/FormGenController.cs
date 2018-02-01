using Api.Util.FormGenerator;
using Api.Util.FormGenerator.DatasourceParsers;
using Api.Util.FormGenerator.FormEditorFactories;
using Api.Util.FormGenerator.FormEditors;
using Api.Util.FormGenerator.Model;
using Microsoft.AspNetCore.Mvc;
using SLnet.Sand.Schema;

namespace Api.Controllers {
  [Route("api/[controller]")]
  public class FormGenController : Controller {

    public FormGenController() {
    }

    [HttpGet]
    public IActionResult GetGlxPoco(FormGenInfo data) {
      IDatasourceParser parser;
      if (data.IsGlxSchema) {
        parser = new slsSchemaTableParser(data.TableXmlPath);
      }
      else {
        parser = new POCOParser(data.AssemblyPath, data.ClassFullName);
      }
      var res = parser.Parse();

      return new JsonResult(res);
    }

    [HttpPost]
    public IActionResult Post([FromBody] FormGenInfo data) {
      var desInfo = new DesignerInfo {
        Todo = @"The following actions must be done manually...
                1. Enable 'Localizable' from the property editor.
                2. Add the designer.cs namespace",
        Namespace = "// TODO: Add namespace"
      };

      IFormEditorFactory formEditorFactory;
      if (data.IsGlxSchema) {
        slsSchemaTable schemaTable = new slsSchemaTableParser(data.TableXmlPath).GetSchemaTable();
        formEditorFactory = new GlxFormEditorFactory(schemaTable);
        desInfo.ClassName = schemaTable.Name;
      }
      else {
        formEditorFactory = new PocoFormEditorFactory();
        var classFullNameSplit = data.ClassFullName.Split('.');
        desInfo.ClassName = classFullNameSplit[classFullNameSplit.Length - 1];
      }

      foreach (var info in data.PropertiesInfo) {
        IApplyFormEditor editor = formEditorFactory.Create(info);
        editor.Apply();

        desInfo.Declarations.AddRange(editor.Declarations);
        desInfo.ISupportInitializeBegin.AddRange(editor.ISupportInitializeBegin);
        desInfo.ISupportInitializeEnd.AddRange(editor.ISupportInitializeEnd);
        desInfo.PropsSetup.AddRange(editor.PropsSetup);
        desInfo.Instantiations.AddRange(editor.Instantiations);
      }

      var des = new CsDesignerTemplate();
      var res = des.GenerateDesigner(desInfo);

      return new JsonResult(res);
    }
  }
}
