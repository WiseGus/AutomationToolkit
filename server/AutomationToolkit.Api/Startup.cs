using AutomationToolkit.Api.Services;
using AutomationToolkit.Core.Compiler;
using AutomationToolkit.Core.Repositories;
using AutomationToolkit.Core.Services;
using AutomationToolkit.Infrastructure;
using AutomationToolkit.Infrastructure.Compiler;
using AutomationToolkit.Infrastructure.Repositories;
using Newtonsoft.Json.Converters;

namespace AutomationToolkit.Api;

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
        services.AddScoped<IPresetsService, PresetsService>();
        services.AddScoped<IGenerateProjectsService, GenerateProjectsService>();

        services.AddTransient<IKeywordReplace, KeywordReplace>();
        services.AddTransient<ICompiler, ExecutronCompiler>();
#if SLNET
        services.AddTransient<IFormGenService, AutomationToolkit.Infrastructure.Services.FormGenService>();
#endif
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
