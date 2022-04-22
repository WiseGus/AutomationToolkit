using System.Threading.Tasks;
using AutomationToolkit.Core.Model;
using AutomationToolkit.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutomationToolkit.Api.Controllers;

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
