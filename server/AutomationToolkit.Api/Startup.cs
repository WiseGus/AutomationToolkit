using AutomationToolkit.Api.Services;
using AutomationToolkit.Core;
using AutomationToolkit.Core.Compiler;
using AutomationToolkit.Core.Repositories;
using AutomationToolkit.Core.Services;
using AutomationToolkit.Infrastructure;
using AutomationToolkit.Infrastructure.Compiler;
using AutomationToolkit.Infrastructure.Repositories;
using AutomationToolkit.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;

namespace AutomationToolkit.Api;

public class Startup
{
public Startup(IConfiguration configuration)
{
  Configuration = configuration;
}

public IConfiguration Configuration { get; }

    [System.Obsolete]
    public void ConfigureServices(IServiceCollection services)
{
  services.AddScoped<IAppSettingsRepository, AppSettingsRepository>();
  services.AddScoped<IPresetsRepository, PresetsRepository>();
  services.AddScoped<IAppSettingsService, AppSettingsService>();
  services.AddTransient<IKeywordReplace, KeywordReplace>();
  services.AddTransient<ICompiler, ExecutronCompiler>();
  services.AddTransient<IFormGenService, FormGenService>();
  services.AddTransient<IPresetsService, PresetsService>();
  services.AddTransient<IGenerateProjectsService, GenerateProjectsService>();

  services.AddControllers()
    .AddNewtonsoftJson(x =>
    {
      x.SerializerSettings.Converters.Add(new StringEnumConverter(false));
    });
  services.AddMvc();
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
  if (env.IsDevelopment())
  {
    app.UseDeveloperExceptionPage();
  }

  app.UseRouting();

  app.UseAuthorization();

  app.UseEndpoints(endpoints =>
  {
    endpoints.MapControllers();
  });
}
}
