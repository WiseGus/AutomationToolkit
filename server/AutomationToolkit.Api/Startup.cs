using Api.Services;
using Core;
using Core.Compiler;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Repositories;
using Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;

namespace Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

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
}
