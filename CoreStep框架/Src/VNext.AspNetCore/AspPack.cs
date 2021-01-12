using Microsoft.AspNetCore.Builder;
using VNext.Packs;

namespace VNext.AspNetCore
{
    /// <summary>
    ///  ����AspNetCore������Packģ�����
    /// </summary>
    public abstract class AspPack : Pack
    {
        /// <summary>
        /// Ӧ��AspNetCore�ķ���ҵ��
        /// </summary>
        /// <param name="app">AspӦ�ó��򹹽���</param>
        public virtual void UsePack(IApplicationBuilder app)
        {
            base.UsePack(app.ApplicationServices);
        }
    }
}