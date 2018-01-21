using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using Api.Util.FormGenerator;

namespace Api.Controllers
{
  [Route("api/[controller]")]
  public class FormgenController : Controller
  {
    public FormgenController()
    {
    }

    [HttpGet("pocoinfo")]
    public IActionResult Get(string assemblyPath, string fullClassName)
    {
      var parser = new POCOParser(assemblyPath, fullClassName);
      var res = parser.Parse();

      return new JsonResult(res);
    }
  }
}
