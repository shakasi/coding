namespace VNext.Tools.Http
{
    /// <summary>
    /// Http请求认证信息
    /// </summary>
    public class HttpAuth
    {
        public HttpAuth(AuthType authType, string authValue)
        {
            this.AuthType = authType;
            this.AuthValue = authValue;
        }

        public AuthType AuthType { get; set; }
        public string AuthValue { get; set; }
    }

    /// <summary>
    /// 认证类型
    /// </summary>
    public enum AuthType
    {
        BasicAuth = 0,
        BearerToken
    }

}
