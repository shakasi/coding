using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using VNext.Options;

namespace DoCare.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseConfiguration(ConfigurationReader.Appsettings);
                webBuilder.UseStartup<Startup>();//微软示例只有这行
                webBuilder.UseKestrel().UseUrls("http://*:4888");
            });
    }
}
