using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml;

namespace Cuscapi.Utils
{
    public class XmlHelper
    {
        private string _xmlPath;
        private XmlDocument _document;

        public XmlHelper(string xmlPath)
        {
            this._xmlPath = xmlPath;
            if (!File.Exists(_xmlPath))
            {
                throw new Exception("没有XML文件");
            }
            XmlReader reader = null;
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                //忽略注释
                settings.IgnoreComments = true;
                reader = XmlReader.Create(_xmlPath, settings);
                _document = new XmlDocument();
                _document.Load(reader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// 通过XmlNode获取InnerText，无递归
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public string GetValueByNode(XmlNode node)
        {
            if (node != null)
            {
                if (node.HasChildNodes && node.ChildNodes.Count == 1 && node.ChildNodes[0].GetType() == typeof(XmlText))
                    return node.InnerText;
            }
            return null;
        }

        /// <summary>
        /// 通过节点名，找XmlDocument的值，有递归
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            XmlNode node = GetXmlNodeByName(key);
            if (node != null)
            {
                if (node.HasChildNodes && node.ChildNodes.Count == 1 && node.ChildNodes[0].GetType() == typeof(XmlText))
                    return node.InnerText;
            }
            return null;
        }

        /// <summary>
        /// 通过节点名，属性名，找XmlDocument的属性值，有递归
        /// </summary>
        /// <param name="key"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        public string GetAttrValue(string key, string attr)
        {
            XmlNode node = GetXmlNodeByName(key);
            if (node != null)
            {
                XmlAttribute attribute = node.Attributes[attr];
                if (attribute != null)
                    return attribute.Value;
            }
            return "";
        }

        /// <summary>
        /// 通过XmlNode和属性名，找属性值，无递归
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        public string GetAttrValueByNode(XmlNode node, string attr)
        {
            if (node != null)
            {
                XmlAttribute attribute = node.Attributes[attr];
                if (attribute != null)
                    return attribute.Value;
            }
            return "";
        }

        /// <summary>
        /// 通过XmlDocument和节点名字，找XmlNode，有递归
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public XmlNode GetXmlNodeByName(string name)
        {
            return GetXmlNodeByName(_document, name);
        }

        /// <summary>
        /// 通过XmlNode和节点名找到子XmlNode，有递归
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public XmlNode GetXmlNodeByName(XmlNode currentNode, string name)
        {
            XmlNode result = null;
            XmlNodeList list = currentNode.ChildNodes;
            foreach (XmlNode node in list)
            {
                if (node.Name == name)
                    return node;

                result = GetXmlNodeByName(node, name);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        /// <summary>
        /// 无递归
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<XmlNode> GetXmlNodesByName(string name)
        {
            List<XmlNode> resultList = new List<XmlNode>();
            XmlNodeList nodeList = _document.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == name)
                {
                    resultList.Add(node);
                }
            }
            return resultList;
        }

        /// <summary>
        /// 无递归
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<XmlNode> GetXmlNodesByName(XmlNode currentNode, string name)
        {
            List<XmlNode> resultList = new List<XmlNode>();
            XmlNodeList nodeList = currentNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Name == name)
                {
                    resultList.Add(node);
                }
            }
            return resultList;
        }

        public T Reader<T>(string name, string attr) where T : new()
        {
            T t = new T();
            PropertyInfo[] tPropertys = typeof(T).GetProperties();

            XmlNodeList nodeList = GetXmlNodeByName(name).ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                foreach (PropertyInfo pro in tPropertys)
                {
                    if (pro.Name.ToUpper().Equals(node.Name.ToUpper()) && pro.CanWrite)
                    {
                        if (node.HasChildNodes)
                        {
                            List<string> strList = new List<string>();
                            foreach (XmlNode nodeSun in node.ChildNodes)
                            {
                                string tValue = GetAttrValueByNode(nodeSun, attr);
                                if (!string.IsNullOrEmpty(tValue))
                                {
                                    strList.Add(tValue);
                                    pro.SetValue(t, Convert.ChangeType(strList, pro.PropertyType), null);
                                }
                            }
                        }
                        else
                        {
                            string tValue = GetAttrValueByNode(node, attr);
                            if (!string.IsNullOrEmpty(tValue))
                            {
                                pro.SetValue(t, Convert.ChangeType(tValue, pro.PropertyType), null);
                            }
                        }
                        break;
                    }
                }
            }
            return t;
        }

        public bool WriteXml<T>(T t, string attr)
        {
            XmlWriter writer = null;
            try
            {
                writer = XmlWriter.Create(_xmlPath);

                PropertyInfo[] tPropertys = typeof(T).GetProperties();
                foreach (PropertyInfo pro in tPropertys)
                {
                    XmlNode node = GetXmlNodeByName(pro.Name);
                    var proValue = pro.GetValue(t, null);
                    if (proValue != null)
                    {
                        if (proValue is IEnumerable<string>)
                        {
                            StringBuilder vSB = new StringBuilder();
                            foreach (string v in proValue as IEnumerable<string>)
                            {
                                vSB.Append(string.Format("\r\n<Attachment Value=\"{0}\"/>", v));
                            }
                            if (vSB.Length > 2)
                            {
                                node.InnerText = vSB.Remove(0, 2).ToString();
                            }

                        }
                        else
                        {
                            SetAttrValueByNode(node, attr, proValue as string);
                        }
                    }
                }

                _document.Save(writer);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        public bool WriteXml()
        {
            XmlWriter writer = null;
            try
            {
                writer = XmlWriter.Create(_xmlPath);
                _document.Save(writer);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        public void SetAttrValue(string key, string attr, string value)
        {
            XmlNode node = GetXmlNodeByName(key);
            if (node != null)
            {
                XmlAttribute attribute = node.Attributes[attr];
                attribute.Value = value;
            }
        }

        public void SetAttrValueByNode(XmlNode node, string attr, string value)
        {
            if (node != null)
            {
                XmlAttribute attribute = node.Attributes[attr];
                attribute.Value = value;
            }
        }
    }
}