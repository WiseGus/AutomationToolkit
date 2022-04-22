using System.IO;
using System.Net;
using System.Threading.Tasks;
using AutomationToolkit.Core.Model;
using AutomationToolkit.Core.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AutomationToolkit.Api.Middleware;

public sealed class JsonExceptionMiddleware
{
public const string DefaultErrorMessage = "A server error occurred.";

private readonly IAppSettingsService _appSettingsService;
private readonly JsonSerializer _serializer;

public JsonExceptionMiddleware(IAppSettingsService appSettingsService)
{
  _appSettingsService = appSettingsService;
  _serializer = new JsonSerializer();
  _serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
}

public async Task Invoke(HttpContext context)
{
  context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
  context.Response.ContentType = "application/json";

  var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
  if (ex == null) return;

  var isDebugMode = await _appSettingsService.IsDebugMode();
  var error = new ApiError { Error = isDebugMode ? ex.ToString() : ex.Message };

  using (var writer = new StreamWriter(context.Response.Body))
  {
    _serializer.Serialize(writer, error);
    await writer.FlushAsync().ConfigureAwait(false);
  }
}
}
