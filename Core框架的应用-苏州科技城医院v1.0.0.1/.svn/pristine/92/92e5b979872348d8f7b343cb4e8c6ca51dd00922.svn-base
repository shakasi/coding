using DoCare.Hosting.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using VNext.Entity;

namespace DoCare.Hosting.EntityConfiguration
{
    /// <summary>
    /// 实体配置类：患者挂号数据
    /// </summary>
    public partial class PatientRegistConfiguration : EntityTypeConfigurationBase<PatientRegist, Guid>
    {
        public override Type DbContextType => typeof(DefaultDbContext);

        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<PatientRegist> builder)
        {
            builder.ToTable("PATIENT_REGIST");
            builder.HasIndex(t => new { t.PatientId, t.VisitId }).HasName("patientid_visitid_index").IsUnique();
            builder.HasIndex(t => t.IdNo).HasName("idno_index");
            builder.HasIndex(t => t.CardId).HasName("cardid_index");
        }
    }
}
