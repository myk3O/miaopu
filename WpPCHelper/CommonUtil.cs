using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
/// <summary>
/// 
/// </summary>
namespace WpPCHelper
{
    public class CommonUtil
    {
        public static bool IsNumeric(String str)
        {
            try
            {
                int.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string ArrayToXml(Dictionary<string, string> arr)
        {
            String xml = "<xml>";

            foreach (KeyValuePair<string, string> pair in arr)
            {
                String key = pair.Key;
                String val = pair.Value;
                //if (IsNumeric(val))
                //{
                //    xml += "<" + key + ">" + val + "</" + key + ">";

                //}
                //else
                    xml += "<" + key + "><![CDATA[" + val + "]]></" + key + ">";
            }

            xml += "</xml>";
            return xml;
        }
        public static Dictionary<string,string> ReceivePostXmlData(string xmlData)
        {
            if (!string.IsNullOrEmpty(xmlData))
            {                
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlData);
                //得到XML文档根节点
                XmlElement root = doc.DocumentElement;
                XmlNodeList nodeList = root.ChildNodes;
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (XmlNode node in nodeList)
                {
                    dic.Add(node.Name, node.InnerText);                   
                }
                return dic;
            }
            return null;
        }

        /// 得到配置项的值
        /// <summary>
        /// 得到配置项的值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetConfigSetting(string name)
        {
            return System.Configuration.ConfigurationManager.AppSettings[name];
        }


        public static string BuildRandomStr(int length)
        {
            Random rand = new Random();

            int num = rand.Next();

            string str = num.ToString();

            if (str.Length > length)
            {
                str = str.Substring(0, length);
            }
            else if (str.Length < length)
            {
                int n = length - str.Length;
                while (n > 0)
                {
                    str.Insert(0, "0");
                    n--;
                }
            }

            return str;
        }
    }
}
