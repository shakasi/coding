using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using VNext.Options;

namespace DoCare.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webHostBuilder =>
            {
                webHostBuilder.UseConfiguration(ConfigurationReader.Appsettings);
                webHostBuilder.UseStartup<Startup>();
                webHostBuilder.UseKestrel().UseUrls("http://*:4888");
            });
    }
}
