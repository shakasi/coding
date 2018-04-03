using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shaka.IDAL
{
    /// <summary>
    /// 用户接口（不同的数据库访问类实现接口达到多数据库的支持）
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// 用id和pwd登陆
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="loginPwd"></param>
        /// <returns>
        /// 返回登陆信息，分四种情况：
        /// 1、返回一条完整数据，则OK。
        /// 2、返回数据中pwd为空，则pwd不对。
        /// 3、返回空数据，则id不对。
        /// 4、报错
        /// </returns>
        Shaka.Model.User UserLogin(string loginId, string loginPwd);
    }
}
