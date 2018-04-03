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
        #region ˽�б���������
        private string _wsdlUrl = string.Empty;
        //�����ɵĴ�����򼯵�����(�����ַ"http://localhost:3015/WebServiceTest.asmx?wsdl"
        //���ּ�Ϊ"WebServiceTest")
        private string _assName = string.Empty;
        //֤��
        X509Certificate[] _x509CertificateArray = null;
        //�����ɵĴ�����򼯵�·��
        private string _assPath = string.Empty;
        //�����������ռ�
        private string _wsdlNamespace = "Shaka.WebService.DynamicWebLoad";
        //����������
        private Type _type = null;
        //�������ʵ��
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
            //��������java�ķ�����http://192.168.0.1/WebService.wsdl
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
        /// ����WebService��URL������һ�����ص�dll
        /// </summary>  
        /// <param name="url">WebService��UR</param>  
        /// <returns></returns>  
        private void CreateServiceAssembly()
        {
            //�ж��ǲ����Ѿ����ݷ����ַ�����˴������
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
                // 1. ʹ�� WebClient ���� WSDL ��Ϣ��  
                WebClient web = new WebClient();
                //web.Credentials = new NetworkCredential("sijsh", "Sky456852");
                //web.Credentials = new NetworkCredential("abcd", "abcd123456");
                Stream stream = web.OpenRead(_wsdlUrl);
                // 2. �����͸�ʽ�� WSDL �ĵ���  
                ServiceDescription description = ServiceDescription.Read(stream);
                // 3. �����ͻ��˴�������ࡣ  
                ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
                importer.ProtocolName = "Soap"; // ָ������Э�顣  
                importer.Style = ServiceDescriptionImportStyle.Client; // ���ɿͻ��˴���  
                importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;
                importer.AddServiceDescription(description, null, null); // ��� WSDL �ĵ���
                // 4. ʹ�� CodeDom ����ͻ��˴����ࡣ  
                CodeNamespace nmspace = new CodeNamespace(_wsdlNamespace); // Ϊ��������������ռ䣬ȱʡΪȫ�ֿռ䡣  
                CodeCompileUnit unit = new CodeCompileUnit();
                unit.Namespaces.Add(nmspace);
                importer.Import(nmspace, unit);//�������ͱ������
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
                    // ��ʾ���������Ϣ  
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
        /// �õ����Ͷ���
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
        /// ִ�д�����ָ���������з���ֵ
        /// </summary>
        /// <param name="methodName">������</param>
        /// <param name="param">����</param>
        /// <returns></returns>
        public object ExecuteQuery(string methodName, object[] param)
        {
            object returnObj = null;
            try
            {
                MethodInfo mi = _type.GetMethod(methodName);
                #region ��������
                //PropertyInfo piTimeout = Instance.GetType().GetProperty("Timeout");
                //piTimeout.SetValue(Instance, 2000, null);

                //PropertyInfo piUrl = Instance.GetType().GetProperty("Url");//���Url

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