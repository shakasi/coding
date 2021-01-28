using Microsoft.Extensions.DependencyInjection;

using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace VNext.Identity
{
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// 获取在<see cref="OnlineUser"/>线用户信息
        /// </summary>
        public static async Task<OnlineUser> GetOnlineUser(this IServiceProvider provider)
        {
            ClaimsPrincipal principal = provider.GetService<IPrincipal>() as ClaimsPrincipal;
            if (principal == null || !principal.Identity.IsAuthenticated)
            {
                return null;
            }

            string userName = principal.Identity.Name;
            IOnlineUserProvider onlineUserProvider = provider.GetService<IOnlineUserProvider>();
            if (onlineUserProvider == null)
            {
                return null;
            }

            OnlineUser onlineUser = await onlineUserProvider.GetOrCreate(userName);
            return onlineUser;
        }
    }
}