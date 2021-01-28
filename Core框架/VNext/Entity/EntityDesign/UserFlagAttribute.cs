using System;

namespace VNext.Entity
{
    /// <summary>
    /// 用户标记，用于标示用户属性/字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class UserFlagAttribute : Attribute
    {
        /// <summary>
        /// 当前用户标识
        /// </summary>
        public const string Token = "@CurrentUserId";
    }
}