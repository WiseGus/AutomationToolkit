using Core.FormGenerator.Model;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class FormGenController : ControllerBase
  {
    private readonly IFormGenService _formGenService;

    public FormGenController(IFormGenService formGenService)
    {
      _formGenService = formGenService;
    }

    [HttpGet]
    public async Task<IActionResult> GetGlxPoco([FromQuery] FormGenInfo data)
    {
      var res = await _formGenService.GetGlxPoco(data);

      return new JsonResult(res);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] FormGenInfo data)
    {
      var res = await _formGenService.GenerateFormDesigner(data);

      return new JsonResult(res);
    }
  }
}
