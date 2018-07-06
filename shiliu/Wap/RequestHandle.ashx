<%@ WebHandler Language="C#" Class="RequestHandle" %>

using System;
using System.Web;
using WeiPay;
using Newtonsoft.Json;

public class RequestHandle : IHttpHandler
{
    MemberHelper mb = new MemberHelper();
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            string menuname = context.Request.QueryString["menuname"].ToString();
            string code = context.Request.QueryString["code"].ToString();
            LogUtil.WriteLog("menuname:[" + menuname + "] code:" + code);
            if (!string.IsNullOrEmpty(code))
            {
                string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", PayConfig.AppId, PayConfig.AppSecret, code);
                string returnStr = HttpUtil.Send("", url);
                LogUtil.WriteLog("通过code换取网页授权,返回值包含(access_token，openid)：" + returnStr);
                OpenModel obj = new OpenModel();
                try
                {
                    obj = JsonConvert.DeserializeObject<OpenModel>(returnStr);
                    if (mb.Verification(obj.openid))
                    {
                        string jurl = "http://tv.gongxue168.com/wap/index.aspx";
                        //有用户，则跳转到指定菜单
                        switch (menuname)
                        {
                            case "1": break;
                            case "2": jurl = "http://tv.gongxue168.com/wap/Login.aspx"; break;
                            default: break;
                        }
                        //跳转到页面
                        context.Response.Redirect(jurl);
                    }
                    else
                    {
                        //跳转到错误页面
                        context.Response.Redirect("error.html");
                    }

                }
                catch (Exception e)
                {
                    LogUtil.WriteLog("obj转换错误" + e.Message);
                }
            }
            else
            {
                //跳转到错误页面
                context.Response.Redirect("error.html");
            }
        }
        catch (Exception ex) { LogUtil.WriteLog("错误日志:" + ex.Message); }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}