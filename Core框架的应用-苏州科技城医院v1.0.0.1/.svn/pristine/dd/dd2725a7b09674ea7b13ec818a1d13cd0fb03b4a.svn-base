using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using VNext.AspNetCore;
using VNext.Options;

namespace DoCare.WebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddVNext(tryAddConfiguration: ConfigurationReader.Appsettings, startsWith: "DoCare")
                .AddPacks();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<JsonExceptionHandlerMiddleware>()
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseVNext();
        }
    }
}