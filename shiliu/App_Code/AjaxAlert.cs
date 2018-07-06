using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Net;
using System.IO;

/// <summary>
///AjaxAlert 的摘要说明
/// </summary>
public static class AjaxAlert
{
    /// <summary>
    /// 弹出消息框并且转向到新的URL
    /// </summary>
    /// <param name="controlName">控件名称</param>
    /// <param name="message">消息内容</param>
    /// <param name="toURL">连接地址</param>
    public static void AlertAndRedirect(Control controlName, string message, string toURL)
    {
        ScriptManager.RegisterClientScriptBlock(controlName, controlName.GetType(), "提示", "alert('" + message + "');location.href='" + toURL + "'", true);

    }

    /// <summary>
    /// 弹出消息框
    /// </summary>
    /// <param name="controlName">控件名称</param>
    /// <param name="message">消息内容</param>
    public static void AlertMsgAndNoFlush(Control controlName, string message)
    {
        ScriptManager.RegisterClientScriptBlock(controlName, controlName.GetType(), "提示", "alert('" + message + "');", true);
    }


    /// <summary>  
    /// 短信发送验证码
    /// </summary>
    /// <param name="context"></param>
    public static bool SendMsg(string title, string msg, string name, string phone)
    {
        string userMsg = string.Format(@"学员{0}。[{1}]，{2}", name, title, msg);
        string strResult = "";
        bool blResult = false;
        int successCount = 0;
        try
        {
            string province = string.Format(@"http://121.52.221.108/send/gsend.aspx?name=lezhi&pwd=lezhi123&dst={0}&msg={1}&sequeid=12345", phone, HttpUtility.UrlEncode(userMsg, Encoding.GetEncoding("GB2312")));
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(province);
            myRequest.Method = "post";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                HttpWebResponse HttpWResp = (HttpWebResponse)myRequest.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, Encoding.GetEncoding("GBK"));
                StringBuilder strBuilder = new StringBuilder();
                while (-1 != sr.Peek())
                {
                    strBuilder.Append(sr.ReadLine());
                }
                //num=2&success=1393710***4,1393710***5&faile=&err=发送成功&errid=0
                strResult = strBuilder.ToString();
                int num = strResult.IndexOf("=") + 1;
                string countstr = strResult.Substring(num, strResult.Length - num);
                num = countstr.IndexOf("&");
                successCount = Convert.ToInt32(countstr.Substring(0, num));
                if (successCount > 0)
                {
                    blResult = true;
                    // ResponseData(context, callback + "(" + js.Serialize("ok") + ")");
                }
                else
                {
                    blResult = false;
                    // ResponseData(context, callback + "(" + js.Serialize("error") + ")");
                }
            }
            catch (Exception exp)
            {
                strResult = "错误：" + exp.Message;
                blResult = false;
            }
        }
        catch (Exception exp)
        {
            strResult = "错误：" + exp.Message;
            blResult = false;
        }
        return blResult;

    }
}