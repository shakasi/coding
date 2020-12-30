using System;

namespace VNext.AspNetCore.Swagger
{
    /// <summary>
    /// 标记此过滤器，API将在Swagger界面中隐藏
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class HiddenApiAttribute : Attribute
    { }
}
