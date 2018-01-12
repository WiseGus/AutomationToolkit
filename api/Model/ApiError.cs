using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Api.Model
{
  public sealed class ApiError
  {
    public string Error { get; set; }

    public ApiError()
    {
    }

    public ApiError(string message)
    {
      Error = Error;
    }
  }
}
