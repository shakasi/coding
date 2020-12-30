using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.Linq;
using VNext.Packs;
using VNext.Mapping;
using IMapper = VNext.Mapping.IMapper;

namespace VNext.AutoMapper
{
    /// <summary>
    /// AutoMapper模块
    /// </summary>
    [Description("AutoMapper模块")]
    public class AutoMapperPack : Pack
    {
        /// <summary>
        /// 获取 模块级别
        /// </summary>
        public override PackLevel Level => PackLevel.Framework;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.TryAddSingleton<MapperConfigurationExpression>(new MapperConfigurationExpression());
            services.TryAddSingleton<IMapFromAttributeTypeFinder, MapFromAttributeTypeFinder>();
            services.TryAddSingleton<IMapToAttributeTypeFinder, MapToAttributeTypeFinder>();
            services.AddSingleton<IMapTuple, MapTupleProfile>();

            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public override void UsePack(IServiceProvider provider)
        {
            ILogger logger = provider.GetLogger<AutoMapperPack>();
            MapperConfigurationExpression cfg = provider.GetService<MapperConfigurationExpression>();

            //获取已注册到IoC的所有Profile
            IMapTuple[] tuples = provider.GetServices<IMapTuple>().ToArray();
            foreach (IMapTuple mapTuple in tuples)
            {
                mapTuple.CreateMap();
                cfg.AddProfile(mapTuple as Profile);
                logger.LogInformation($"初始化对象映射配对：{mapTuple.GetType()}");
            }

            //各个模块DTO的 IAutoMapperConfiguration 映射实现类
            IAutoMapperConfiguration[] configs = provider.GetServices<IAutoMapperConfiguration>().ToArray();
            foreach (IAutoMapperConfiguration config in configs)
            {
                config.CreateMaps(cfg);
                logger.LogInformation($"初始化对象映射配对：{config.GetType()}");
            }

            MapperConfiguration configuration = new MapperConfiguration(cfg);
            IMapper mapper = new AutoMapperMapper(configuration);
            MapperExtensions.SetMapper(mapper);
            logger.LogInformation($"初始化对象映射对象到 MapperExtensions：{mapper.GetType()}，共包含 {configuration.GetMappers().Count()} 个映射配对");

            IsEnabled = true;
        }
    }
}