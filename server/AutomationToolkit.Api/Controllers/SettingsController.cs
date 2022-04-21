using Core.Interfaces.Services;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class SettingsController : ControllerBase
  {
    private readonly IAppSettingsService _appSettingsService;

    public SettingsController(IAppSettingsService appSettingsService)
    {
      _appSettingsService = appSettingsService;
    }

    [HttpGet("ping")]
    public IActionResult Ping()
    {
      return new JsonResult("pong");
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var res = await _appSettingsService.GetKeywords();

      return new JsonResult(res, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] IEnumerable<Keyword> data)
    {
      await _appSettingsService.SaveKeywords(data);

      return new OkResult();
    }
  }
}
