using System;

/// <summary>
/// 注意 DateTime 类型要写成 DateTime?
/// </summary>
namespace Shaka.Model
{
    /// <summary>
    /// 用户类
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户登录id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        [Check(CheckEmpty = true, ProShowName = "姓名")]
        public string UserName { get; set; }

        /// <summary>
        /// 用户登录密码
        /// </summary>
        [Check(RegexStr = "^[0-9]*[1-9][0-9]*$")]
        public string UserPwd { get; set; }

        /// <summary>
        /// 用户是否被启用
        /// </summary>
        public bool IsAble { get; set; }

        /// <summary>
        /// 用户是否修改密码（强制第一次登陆修改密码）
        /// </summary>
        public bool IfChangePwd { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddDate { get; set; }

        /// <summary>
        /// 用户简介
        /// </summary>
        [Check(MaxLength = 100, ProShowName = "用户简介")]
        public string Description { get; set; }
    }
}