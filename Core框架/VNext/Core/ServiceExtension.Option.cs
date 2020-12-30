using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using VNext.Data;
using VNext.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceExtensions
    {
        /// <summary>
        /// 从<see cref="IConfiguration"/>创建<see cref="VNextOptions"/>
        /// </summary>
        public static VNextOptions GetVNextOptions(this IConfiguration configuration)
        {
            VNextOptions options = new VNextOptions();
            new VNextOptionsSetup(configuration).Configure(options);
            return options;
        }

        /// <summary>
        /// 从<see cref="IServiceProvider"/>创建<see cref="VNextOptions"/>
        /// </summary>
        public static VNextOptions GetVNextOptions(this IServiceProvider provider)
        {
            return provider.GetService<IOptions<VNextOptions>>()?.Value;
        }
    }
}