using AutomationToolkit.Core.FormGenerator;
using Microsoft.AspNetCore.Mvc;

namespace AutomationToolkit.Api.Controllers;

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

        var projectAssemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(p => p.FullName.StartsWith("AutomationToolkit"));

        foreach (var assembly in projectAssemblies)
        {
            var formEditors = assembly.GetTypes().Where(p => p.GetInterfaces().Contains(typeof(IFormEditorInfo)) && !p.IsAbstract);

            foreach (var formEditor in formEditors)
            {
                var instantiatedType = (IFormEditorInfo)Activator.CreateInstance(formEditor);
                res.Add(instantiatedType);
            }
        }

        return new JsonResult(res);
    }
}

