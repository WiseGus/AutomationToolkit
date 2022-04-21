using Core.Interfaces.Services;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class GenerateProjectsController : ControllerBase
  {
    private readonly IGenerateProjectsService _generateProjectsService;

    public GenerateProjectsController(IGenerateProjectsService generateProjectsService)
    {
      _generateProjectsService = generateProjectsService;
    }

    [HttpPost]
    public Task Post([FromBody] Preset value)
    {
      return _generateProjectsService.Generate(value);
    }
  }
}
