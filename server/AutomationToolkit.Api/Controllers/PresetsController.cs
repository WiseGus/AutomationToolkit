using Core.Interfaces.Services;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;

namespace Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class PresetsController : ControllerBase
  {
    private readonly IPresetsService _presetsService;

    public PresetsController(IPresetsService presetsService)
    {
      _presetsService = presetsService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var presetsList = await _presetsService.GetPresets();

      return new JsonResult(presetsList, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
    }

    [HttpGet("{alias}")]
    public async Task<IActionResult> Get(string alias)
    {
      var presetObj = await _presetsService.GetPresetById(alias);

      return new JsonResult(presetObj, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Preset value)
    {
      await _presetsService.AddOrUpdatePreset(value);

      return new OkResult();
    }

    [HttpPut("{alias}")]
    public async Task<IActionResult> Put(string alias, [FromBody] Preset value)
    {
      value.Alias = alias;

      await _presetsService.AddOrUpdatePreset(value);

      return Ok();
    }

    [HttpDelete("{alias}")]
    public async Task<IActionResult> Delete(string alias)
    {
      await _presetsService.RemovePreset(alias);

      return Ok();
    }
  }
}
