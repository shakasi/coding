using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Services.Description;
using System.Reflection;
using System.Xml.Serialization;
using System.Web.Services.Protocols;
using System.Security.Cryptography.X509Certificates;

namespace Shaka.Utils
{
    public class WebServiceProxy
    {
        #region 私有变量和属性
        private string _wsdlUrl = string.Empty;
        //既生成的代理程序集的名字(比如地址"http://localhost:3015/WebServiceTest.asmx?wsdl"
        //名字即为"WebServiceTest")
        private string _assName = string.Empty;
        //证书
        X509Certificate[] _x509CertificateArray = null;
        //既生成的代理程序集的路径
        private string _assPath = string.Empty;
        //代理类命名空间
        private string _wsdlNamespace = "Shaka.WebService.DynamicWebLoad";
        //代理类类型
        private Type _type = null;
        //代理类的实例
        private object _instance = null;
        private object Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (this)
                    {
                        if (_instance == null)
                        {
                            _instance = Activator.CreateInstance(_type);
                            return this._instance;
                        }
                        else
                        {
                            return this._instance;
                        }
                    }
                }
                else
                {
                    return this._instance;
                }
            }
        }
        #endregion

        public WebServiceProxy(string wsdlUrl, X509Certificate[] x509CertificateArray)
        {
            //曾经发现java的服务用http://192.168.0.1/WebService.wsdl
            string addressSuffix = "wsdl";
            if (wsdlUrl.Substring(wsdlUrl.Length - 4).ToLower() != addressSuffix)
            {
                wsdlUrl += addressSuffix;
            }
            this._wsdlUrl = wsdlUrl;
            _x509CertificateArray = x509CertificateArray;
            this._assName = GetClassName(_wsdlUrl);
            this._assPath = Path.GetTempPath() + _assName + ".dll";
            CreateServiceAssembly();
        }

        /// <summary>  
        /// 根据WebService的URL，生成一个本地的dll
        /// </summary>  
        /// <param name="url">WebService的UR</param>  
        /// <returns></returns>  
        private void CreateServiceAssembly()
        {
            //判断是不是已经根据服务地址生成了代理程序集
            //if (CheckCache())
            //{
            //    InitTypeName();
            //    return;
            //}
            if (string.IsNullOrEmpty(_wsdlUrl))
            {
                return;
            }
            try
            {
                // 1. 使用 WebClient 下载 WSDL 信息。  
                WebClient web = new WebClient();
                //web.Credentials = new NetworkCredential("sijsh", "Sky456852");
                //web.Credentials = new NetworkCredential("abcd", "abcd123456");
                Stream stream = web.OpenRead(_wsdlUrl);
                // 2. 创建和格式化 WSDL 文档。  
                ServiceDescription description = ServiceDescription.Read(stream);
                // 3. 创建客户端代理代理类。  
                ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
                importer.ProtocolName = "Soap"; // 指定访问协议。  
                importer.Style = ServiceDescriptionImportStyle.Client; // 生成客户端代理。  
                importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;
                importer.AddServiceDescription(description, null, null); // 添加 WSDL 文档。
                // 4. 使用 CodeDom 编译客户端代理类。  
                CodeNamespace nmspace = new CodeNamespace(_wsdlNamespace); // 为代理类添加命名空间，缺省为全局空间。  
                CodeCompileUnit unit = new CodeCompileUnit();
                unit.Namespaces.Add(nmspace);
                importer.Import(nmspace, unit);//流解析和编译关联
                CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
                CompilerParameters parameter = new CompilerParameters();
                parameter.GenerateExecutable = false;
                parameter.OutputAssembly = _assPath;
                parameter.ReferencedAssemblies.Add("System.dll");
                parameter.ReferencedAssemblies.Add("System.XML.dll");
                parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
                parameter.ReferencedAssemblies.Add("System.Data.dll");
                CompilerResults result = provider.CompileAssemblyFromDom(parameter, unit);
                stream.Close();
                provider.Dispose();
                if (result.Errors.HasErrors)
                {
                    // 显示编译错误信息  
                    System.Text.StringBuilder sb = new StringBuilder();
                    foreach (CompilerError ce in result.Errors)
                    {
                        sb.Append(ce.ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }
                InitTypeName();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 得到类型对象
        /// </summary>
        private void InitTypeName()
        {
            System.Reflection.Assembly assembly = Assembly.LoadFrom(_assPath);
            Type[] types = assembly.GetTypes();
            string typeName = string.Empty;
            foreach (Type t in types)
            {
                if (t.BaseType == typeof(SoapHttpClientProtocol))
                {
                    typeName = t.Name;
                }
            }
            _type = assembly.GetType(_wsdlNamespace + "." + typeName, true, true);
        }

        /// <summary>
        /// 执行代理类指定方法，有返回值
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public object ExecuteQuery(string methodName, object[] param)
        {
            object returnObj = null;
            try
            {
                MethodInfo mi = _type.GetMethod(methodName);
                #region 设置属性
                //PropertyInfo piTimeout = Instance.GetType().GetProperty("Timeout");
                //piTimeout.SetValue(Instance, 2000, null);

                //PropertyInfo piUrl = Instance.GetType().GetProperty("Url");//获得Url

                //myRequest.ClientCertificates.AddRange(x509CertificateArray);
                //myRequest.Credentials = new NetworkCredential("sijsh", "Sky456852");

                //PropertyInfo piClientCertificates = Instance.GetType().GetProperty("ClientCertificates");
                //piClientCertificates.SetValue(Instance, _x509CertificateArray, null);

                //PropertyInfo piCredentials = Instance.GetType().GetProperty("Credentials");
                //piCredentials.SetValue(Instance, new NetworkCredential("sijsh", "Sky456852"), null);
                //piCredentials.SetValue(Instance, new NetworkCredential("abcd", "abcd123456", null);

                #endregion
                returnObj = mi.Invoke(Instance, param);
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnObj;
        }

        private string GetClassName(string url)
        {
            string[] parts = url.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');
            return pps[0];
        }

        private bool CheckCache()
        {
            if (File.Exists(_assPath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}