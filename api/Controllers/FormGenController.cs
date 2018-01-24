using Api.Util.FormGenerator;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
  [Route("api/[controller]")]
  public class FormgenController : Controller {
    public FormgenController() {
    }

    [HttpGet("pocoinfosch")]
    public IActionResult Get(string tableXmlPath) {
      var parser = new slSchemaTableParser(tableXmlPath);
      var res = parser.Parse();

      return new JsonResult(res);
    }

    [HttpGet("pocoinfoasm")]
    public IActionResult Get(string assemblyPath, string classFullName) {
      var parser = new POCOParser(assemblyPath, classFullName);
      var res = parser.Parse();

      return new JsonResult(res);
    }
  }
}
