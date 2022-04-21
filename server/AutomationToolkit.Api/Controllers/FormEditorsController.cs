using Core.FormGenerator;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class FormEditorsController : ControllerBase
  {

    public FormEditorsController()
    {
    }

    [HttpGet]
    public IActionResult Get()
    {
      List<IFormEditorInfo> res = new List<IFormEditorInfo>();

      var formEditors = Assembly.GetAssembly(typeof(Core.FormGenerator.FormEditors.BaseEditor))
        .GetTypes()
        .Where(p => p.GetInterfaces().Contains(typeof(IFormEditorInfo)) && !p.IsAbstract);
      foreach (var formEditor in formEditors)
      {
        var instantiatedType = (IFormEditorInfo)Activator.CreateInstance(formEditor);
        res.Add(instantiatedType);
      }

      return new JsonResult(res);
    }
  }
}

