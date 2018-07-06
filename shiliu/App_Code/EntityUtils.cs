using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Microsoft.VisualBasic;
using Maliang;
using System.Security.Cryptography;
using System.IO;

/// <summary>
/// 加密 的摘要说明
/// </summary>
public class EntityUtils
{
    /// <summary>
    /// 加密算法
    /// </summary>
    /// <param name="str">要加密的字符</param>
    /// <returns>加密后的字符</returns>
    public static string Encrypt(string str)
    {
        int length = str.Length;
        if (length % 2 != 0)
        {
            str += "g";
            length++;
        }

        string addstr = "";
        string ch1;
        string ch2;
        int ch5;

        for (int i = 0; i < length; i += 2)
        {
            ch1 = str.Substring(i, 1);
            ch2 = str.Substring(i + 1, 1);
            Encoding e = Encoding.ASCII;
            if (Encoding.ASCII.GetBytes(ch1).GetValue(0).Equals(Encoding.ASCII.GetBytes(ch2).GetValue(0)))
            {
                ch5 = Math.Abs(Math.Abs(sbyte.Parse(Encoding.ASCII.GetBytes(ch1).GetValue(0).ToString())) - 40);
            }
            else
            {
                ch5 = Math.Abs(Math.Abs(sbyte.Parse(Encoding.ASCII.GetBytes(ch2).GetValue(0).ToString())) - 80);
            }
            //把字符串反转
            string newch1 = Math.Abs(sbyte.Parse(Encoding.ASCII.GetBytes(ch1).GetValue(0).ToString())) + "";
            char[] strArray = newch1.ToCharArray();
            Array.Reverse(strArray);
            newch1 = new string(strArray);

            addstr += Math.Abs(sbyte.Parse(Encoding.ASCII.GetBytes(ch2).GetValue(0).ToString()) / 2) + "" + ch5 + newch1;
            //Response.Write(addstr + "<br>");
        }

        int newLength = addstr.Length;
        do
        {
            if (newLength % 4 == 0)
                break;
            addstr += "8";
            newLength++;
        } while (newLength % 4 != 0);

        string jiami = "";
        for (int i = 0; i < newLength; i += 4)
        {
            ch1 = "2" + addstr.Substring(i, 4);
            //jiami += Microsoft.VisualBasic.Strings.ChrW(int.Parse(ch1)).ToString();
        }
        //Response.Write(jiami + "<br>");
        return jiami;
    }

    /// <summary>
    /// 对字符串进行换行处理
    /// </summary>
    /// <param name="str">要处理的字符串</param>
    /// <param name="charNumber">每行多少个字符</param>
    /// <returns></returns>
    public static string StringNewline(string str, int rowCharNumber)
    {
        if (str.Length > 0)
        {
            string newlineStr = ""; //换行后的字符串
            for (int i = 0; i < str.Length; i++)
            {
                if (i % rowCharNumber == 0)
                {
                    if (str.Substring(i, str.Length - i).Length > rowCharNumber)
                    {
                        newlineStr += str.Substring(i, rowCharNumber) + "<br />";
                    }
                    else
                    {
                        newlineStr += str.Substring(i, str.Length - i);
                    }
                }
            }
            return newlineStr;
        }
        return "";
    }
    //Md5加密算法 该算法不可逆 str:输入字符串 i:加密位数（16或32）
    public static string StringToMD5(string str, int i)
    {
        //获取要加密的字段，并转化为Byte[]数组
        byte[] data = System.Text.Encoding.Unicode.GetBytes(str.ToCharArray());
        //建立加密服务
        System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        //加密Byte[]数组
        byte[] result = md5.ComputeHash(data);
        //将加密后的数组转化为字段
        if (i == 16 && str != string.Empty)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
        }
        else if (i == 32 && str != string.Empty)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
        }
        else
        {
            switch (i)
            {
                case 16: return "000000000000000";
                case 32: return "000000000000000000000000000000";
                default: return "请确保调用函数时第二个参数为16或32";
            }
        }
    }
    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string EncryptOutStr(string str)
    {
        string htext = "";
        for (int i = 0; i < str.Length; i++)
        {
            htext += (char)(str[i] + 10 - 1 * 2);
        }
        return htext;
    }
    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Decrypt(string str)
    {
        string dtext = "";
        for (int i = 0; i < str.Length; i++)
        {
            dtext += (char)(str[i] - 10 + 1 * 2);
        }
        return dtext;
    }


    public static string _KEY = "HQDCKEY1";  //密钥   
    public static string _IV = "HQDCKEY2";   //向量   

    /// <summary>   
    /// 加密   
    /// </summary>   
    /// <param name="data"></param>   
    /// <returns></returns>   
    public static string EncodeString(string data)
    {

        byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(_KEY);
        byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(_IV);

        DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
        int i = cryptoProvider.KeySize;
        MemoryStream ms = new MemoryStream();
        CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

        StreamWriter sw = new StreamWriter(cst);
        sw.Write(data);
        sw.Flush();
        cst.FlushFinalBlock();
        sw.Flush();

        string strRet = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        if (strRet.Contains("+"))
        {
            strRet = strRet.Replace("+", "＿");
        }
        return strRet;
    }

    /// <summary>   
    /// 解密   
    /// </summary>   
    /// <param name="data"></param>   
    /// <returns></returns>   
    public static string DecodeString(string data)
    {

        byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(_KEY);
        byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(_IV);

        byte[] byEnc;

        try
        {
            data.Replace("_%_", "/");
            data.Replace("-%-", "#");
            data = data.Replace("＿", "+");
            byEnc = Convert.FromBase64String(data);

        }
        catch
        {
            return null;
        }

        DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
        MemoryStream ms = new MemoryStream(byEnc);
        CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
        StreamReader sr = new StreamReader(cst);
        return sr.ReadToEnd();
    }

}

