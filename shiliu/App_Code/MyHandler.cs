using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MyHandler
/// </summary>
public class MyHandler : IHttpHandler
{
    public MyHandler()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool IsReusable
    {
        get { return true; }
    }
    public void ProcessRequest(HttpContext ctx)
    {
        //ctx.Response.Write("sorry");
        if (ctx.Request.Url.ToString().Contains("v.chinesecom.cn"))
        {
            ctx.Response.Write("sorry");
        }
    }
}