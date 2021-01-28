using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;

namespace VNext.CodeGenerator
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ForeignRelation
    {
        OneToMany = 0,
        ManyToOne,
        OneToOne,
        OwnsOne,
        OwnsMany
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DeleteBehavior
    {
        ClientSetNull = 0,
        Restrict,
        SetNull,
        Cascade
    }

    /// <summary>
    /// 主键类型
    /// </summary>
    public enum PrimaryKeyType
    {
        [Description("System.Guid")]
        Guid = 0,
        [Description("System.String")]
        String,
        [Description("System.Int32")]
        Int32,
        [Description("System.Int64")]
        Int64,
    }

    /// <summary>
    /// 属性类型
    /// </summary>
    public enum PropertyType
    {
        [Description("System.String")]
        String = 0,
        [Description("System.Int32")]
        Int32,
        [Description("System.Boolean")]
        Boolean,
        [Description("System.Double")]
        Double,
        [Description("System.DateTime")]
        DateTime,
        [Description("System.Guid")]
        Guid,
        [Description("System.Int64")]
        Int64,
        [Description("ICollection<>")]
        ICollection
    }

    /// <summary>
    /// 数据权限标识
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataAuthFlag
    {
        [Description("用户权限标志")]
        UserFlag = 0,
    }
}
