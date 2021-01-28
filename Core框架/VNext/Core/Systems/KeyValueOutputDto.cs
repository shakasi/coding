using System;
using System.ComponentModel;
using VNext.Data;
using VNext.Entity;
using VNext.Mapping;

namespace VNext.Systems
{
    /// <summary>
    /// 输出DTO:键值数据
    /// </summary>
    [MapFrom(typeof(KeyValue))]
    public class KeyValueOutputDto : IOutputDto
    {
        /// <summary>
        /// 获取或设置 编号
        /// </summary>
        [DisplayName("编号")]
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置 数据值JSON
        /// </summary>
        [DisplayName("数据值JSON")]
        public string ValueJson { get; set; }

        /// <summary>
        /// 获取或设置 数据值类型名
        /// </summary>
        [DisplayName("数据值类型名")]
        public string ValueType { get; set; }

        /// <summary>
        /// 获取或设置 数据键名
        /// </summary>
        [DisplayName("数据键名")]
        public string Key { get; set; }

        /// <summary>
        /// 获取或设置 数据值
        /// </summary>
        [DisplayName("数据值")]
        public object Value { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        [DisplayName("是否锁定")]
        public bool IsLocked { get; set; }
    }
}