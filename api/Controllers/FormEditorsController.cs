using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
  [Route("api/[controller]")]
  public class FormEditorsController : Controller {
    public FormEditorsController() {
    }

    [HttpGet]
    public IActionResult Get() {
      object res = null;

      return new JsonResult(res);
    }
  }
}
