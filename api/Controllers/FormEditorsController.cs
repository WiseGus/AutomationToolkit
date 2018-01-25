using Api.Util.FormGenerator.FormEditors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Api.Controllers {
  [Route("api/[controller]")]
  public class FormEditorsController : Controller {

    public FormEditorsController() {
    }

    [HttpGet]
    public IActionResult Get() {
      List<IFormEditorInfo> res = new List<IFormEditorInfo>();

      var formEditors = Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(p => p.GetInterfaces().Contains(typeof(IFormEditorInfo)));
      foreach (var formEditor in formEditors) {
        var instantiatedType = (IFormEditorInfo)Activator.CreateInstance(formEditor);
        res.Add(instantiatedType);
      }

      return new JsonResult(res);
    }
  }
}

