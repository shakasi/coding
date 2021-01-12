using Microsoft.AspNetCore.Builder;
using VNext.Packs;

namespace VNext.AspNetCore
{
    /// <summary>
    ///  基于AspNetCore环境的Pack模块基类
    /// </summary>
    public abstract class AspPack : Pack
    {
        /// <summary>
        /// 应用AspNetCore的服务业务
        /// </summary>
        /// <param name="app">Asp应用程序构建器</param>
        public virtual void UsePack(IApplicationBuilder app)
        {
            base.UsePack(app.ApplicationServices);
        }
    }
}