using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using VNext.Extensions;
using VNext.Options;
using VNext.Packs;

namespace VNext.AspNetCore.Swagger
{
    /// <summary>
    /// Swagger模块基类
    /// </summary>
    [DependsOnPacks(typeof(AspNetCorePack))]
    public abstract class SwaggerPackBase : AspPack
    {
        private VNextOptions _vNextpOptions;

        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override PackLevel Level => PackLevel.Application;

        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 100;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            IConfiguration configuration = services.GetConfiguration();
            _vNextpOptions = configuration.GetVNextOptions();
            if (_vNextpOptions?.Swagger?.Enabled != true)
            {
                return services;
            }

            services.AddMvcCore().AddApiExplorer();
            services.AddSwaggerGen(options =>
            {
                if (_vNextpOptions?.Swagger?.Endpoints?.Count > 0)
                {
                    foreach (SwaggerEndpoint endpoint in _vNextpOptions.Swagger.Endpoints)
                    {
                        options.SwaggerDoc($"{endpoint.Version}",
                            new OpenApiInfo() { Title = endpoint.Title, Version = endpoint.Version });
                    }

                    options.DocInclusionPredicate((version, desc) =>
                    {
                        if (!desc.TryGetMethodInfo(out MethodInfo method))
                        {
                            return false;
                        }

                        string[] versions = method.DeclaringType.GetAttributes<ApiExplorerSettingsAttribute>().Select(m => m.GroupName).ToArray();
                        if (version.ToLower() == "v1" && versions.Length == 0)
                        {
                            return true;
                        }

                        return versions.Any(m => m.ToString() == version);
                    });
                }

                Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml").ToList().ForEach(file =>
                {
                    options.IncludeXmlComments(file);
                });
                //权限Token
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Description = "请输入带有Bearer的Token，形如 “Bearer {Token}” ",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new[] { "readAccess", "writeAccess" }
                    }
                });
                options.DocumentFilter<HiddenApiFilter>();
            });

            return services;
        }

        /// <summary>
        /// 应用AspNetCore的服务业务
        /// </summary>
        /// <param name="app">Asp应用程序构建器</param>
        public override void UsePack(IApplicationBuilder app)
        {
            if (_vNextpOptions?.Swagger?.Enabled != true)
            {
                return;
            }

            SwaggerOption swagger = _vNextpOptions.Swagger;
            app.UseSwagger().UseSwaggerUI(options =>
            {
                if (swagger.Endpoints?.Count > 0)
                {
                    foreach (SwaggerEndpoint endpoint in swagger.Endpoints)
                    {
                        options.SwaggerEndpoint(endpoint.Url, endpoint.Title);
                    }
                }

                options.RoutePrefix = swagger.RoutePrefix;

                if (swagger.MiniProfiler)
                {
                    options.IndexStream = () => GetType().Assembly.GetManifestResourceStream("VNext.AspNetCore.Swagger.index.html");
                }
            });

            IsEnabled = true;
        }
    }
}