using DoCare.Hosting.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using VNext.Entity;

namespace DoCare.Hosting.EntityConfiguration
{
    /// <summary>
    /// 实体配置类：患者就诊关系数据信息
    /// </summary>
    public partial class LabReportConfiguration : EntityTypeConfigurationBase<LisReport, Guid>
    {
        public override Type DbContextType => typeof(DefaultDbContext);

        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<LisReport> builder)
        {
            builder.ToTable("LAB_REPORT");
            builder.HasIndex(t => new { t.TestNo, t.ReportItemCode, t.ItemNo }).HasName("labreport_index").IsUnique();
        }
    }
}
