using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using VNext.Extensions;
using VNext.Options;

namespace DoCare.Hosting.Dtos
{
    public static class HospitalOption
    {
        /// <summary>
        /// 挂号有效天数;默认3天
        /// </summary>
        public static int? Days => ConfigurationReader.Appsettings["Days"]?.ToInt16() ?? 3;

        /// <summary>
        /// Sql配置
        /// </summary>
        public static List<SqlOption> SqlOptions => ConfigurationReader.Appsettings.GetInstance<List<SqlOption>>("Sqls");

    }
}
