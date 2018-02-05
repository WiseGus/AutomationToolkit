using Api.Util.FormGenerator;
using Api.Util.FormGenerator.DatasourceParsers;
using Api.Util.FormGenerator.FormEditorFactories;
using Api.Util.FormGenerator.FormEditors;
using Api.Util.FormGenerator.Model;
using Api.Util.FormGenerator.Visitors;
using Microsoft.AspNetCore.Mvc;
using SLnet.Sand.Schema;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
  [Route("api/[controller]")]
  public class FormGenController : Controller
  {

    public FormGenController()
    {
    }

    [HttpGet]
    public IActionResult GetGlxPoco(FormGenInfo data)
    {
      IDatasourceParser parser;
      if (data.IsGlxSchema)
      {
        parser = new slsSchemaTableParser(data.TableXmlPath);
      }
      else
      {
        parser = new POCOParser(data.AssemblyPath, data.ClassFullName);
      }
      var res = parser.Parse();

      return new JsonResult(res);
    }

    [HttpPost]
    public IActionResult Post([FromBody] FormGenInfo data)
    {
      var desInfo = new DesignerInfo
      {
        Todo = @"The following actions must be done manually...
                1. Enable 'Localizable' from the property editor.
                2. Add the designer.cs namespace",
        Namespace = "// TODO: Add namespace"
      };

      var editorVisitors = new List<IEditorVisitor>();

      IFormEditorFactory formEditorFactory;
      if (data.IsGlxSchema)
      {
        slsSchemaTable schemaTable = new slsSchemaTableParser(data.TableXmlPath).GetSchemaTable();
        formEditorFactory = new GlxFormEditorFactory(schemaTable);
        desInfo.ClassName = schemaTable.Name + "F";

        var gxControls = data.PropertiesInfo[0].FormEditor.StartsWith("gx");
        var safeCollectionName = schemaTable.Name.Remove(0, 2);
        data.PropertiesInfo.Insert(0, new FormEditorInfo { Name = safeCollectionName, FormEditor = gxControls ? "gxObjectCollectionSource" : "cmObjectCollectionSource" });
        data.PropertiesInfo.Insert(1, new FormEditorInfo { Name = safeCollectionName, FormEditor = gxControls ? "gxBindingSource" : "cmBindingSource" });
        data.PropertiesInfo.Insert(2, new FormEditorInfo { Name = safeCollectionName, FormEditor = gxControls ? "gxErrorProvider" : "cmErrorProvider" });

        data.PropertiesInfo.Add(new FormEditorInfo { Name = safeCollectionName + "F", FormEditor = "MainLayout" });
      }
      else
      {
        formEditorFactory = new PocoFormEditorFactory();
        var classFullNameSplit = data.ClassFullName.Split('.');
        desInfo.ClassName = classFullNameSplit[classFullNameSplit.Length - 1] + "F";
      }

      var editors = new List<IApplyFormEditor>();
      foreach (var info in data.PropertiesInfo)
      {
        IApplyFormEditor editor = formEditorFactory.Create(info);
        editors.Add(editor);
        if (editor is IEditorVisitor)
        {
          editorVisitors.Add(editor as IEditorVisitor);
        }
      }

      foreach (var editor in editors)
      {
        editor.Apply();
        VisitFormEditor(editorVisitors, editor);
        FillDesignerInfo(desInfo, editor);
      }

      var des = new CsDesignerTemplate();
      var res = des.GenerateDesigner(desInfo);

      return new JsonResult(res);
    }

    private void VisitFormEditor(List<IEditorVisitor> editorVisitors, IApplyFormEditor editor)
    {
      foreach (var visitor in editorVisitors)
      {
        if (editor is IEditorVisitable)
        {
          (editor as IEditorVisitable).Accept(visitor);
        }
      }
    }

    private static void FillDesignerInfo(DesignerInfo desInfo, IApplyFormEditor editor)
    {
      desInfo.Declarations.AddRange(editor.Declarations);
      desInfo.ISupportInitializeBegin.AddRange(editor.ISupportInitializeBegin);
      desInfo.ISupportInitializeEnd.AddRange(editor.ISupportInitializeEnd);
      desInfo.PropsSetup.AddRange(editor.PropsSetup);
      desInfo.Instantiations.AddRange(editor.Instantiations);
    }
  }
}
