using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoCare.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var a = CreateHostBuilder(args);
            var b = a.Build();
            b.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var a = Host.CreateDefaultBuilder(args);
            var b = a.ConfigureWebHostDefaults(aaaa);
            return b;

        }

        public static void aaaa(IWebHostBuilder webBuilder)
        {
            //webBuilder.UseConfiguration(ConfigurationReader.Appsettings);
            webBuilder.UseStartup<Startup>();//微软示例只有这行
            webBuilder.UseKestrel().UseUrls("http://*:4888");
        }
    }
}
