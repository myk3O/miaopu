<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;
using Maliang;

public class Handler : IHttpHandler, IRequiresSessionState
{
    WebHelper wh = new WebHelper();
    SqlHelper her = new SqlHelper();
    JavaScriptSerializer js = new JavaScriptSerializer();
    public void ProcessRequest(HttpContext context)
    {

        if (context.Request["Method"] != null)
        {
            //获取执行方法的名称
            string methodName = context.Request["Method"].ToString();
            //反射执行指定方法
            MethodInfo minfo = this.GetType().GetMethod(methodName);
            if (minfo != null)
            {
                minfo.Invoke(this, new object[] { context });
            }
        }
    }

    /// <summary>
    /// 获取首页滚图
    /// </summary>
    /// <param name="context"></param>
    public void GetBanner(HttpContext context)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        DataTable dt = new DataTable();
        dt = wh.SelImg(1);
        int maxCount = dt.Rows.Count > 6 ? 6 : dt.Rows.Count;//最多6个
        sb.AppendLine("<ul class='ei-slider-large'>");
        sb2.AppendLine("<div class='thumbs'>");
        sb2.AppendLine("<ul class='ei-slider-thumbs'>");
        sb2.AppendLine("<li class='ei-slider-element'></li>");
        for (int i = 0; i < maxCount; i++)
        {
            sb.AppendLine("<li>");
            if (string.IsNullOrEmpty(dt.Rows[i]["tMemo"].ToString()))
            {
                sb.AppendLine("<a href='product.aspx' title='" + dt.Rows[i]["tilte"].ToString() + "'>");
            }
            else
            {
                sb.AppendLine("<a href='product.aspx?id=" + dt.Rows[i]["tMemo"].ToString() + "' title='" + dt.Rows[i]["tilte"].ToString() + "'>");
            }
            sb.AppendLine("<img class='img' src='../Admin/upload_Img/Logo_Img/" + dt.Rows[i]["imgUrl"].ToString() + "' alt='" + dt.Rows[i]["tilte"].ToString() + "'></a></span> </li>");


            //加点
            sb2.AppendLine("<li><a href='#'></a></li>");

        }

        sb.AppendLine("</ul>");
        sb2.AppendLine("</ul>");
        sb2.AppendLine("</div>");
        //Banner = sb.ToString();
        //pagenavi = sb2.ToString();
        ResponseData(context, js.Serialize(sb.ToString() + sb2.ToString()));
    }
    /// <summary>
    /// 获取预支付订单状态
    /// </summary>
    /// <param name="context"></param>
    public void GetOrderHandle(HttpContext context)
    {
        bool isHandle = true;//未处理，一直查询
        string orderid = context.Request["orderid"] == null ? "" : context.Request["orderid"].ToString();
        if (!string.IsNullOrEmpty(orderid))
        {
            string sql = "select OrderState from ML_Order  where nID=" + orderid;
            DataTable dt = new DataTable();
            while (isHandle)
            {
                System.Threading.Thread.Sleep(4000);//3秒查询一次

                if (her.ExecuteScalar(sql) != null && her.ExecuteScalar(sql).ToString() != "" && Convert.ToInt32(her.ExecuteScalar(sql)) == 2)
                {
                    isHandle = false;//订单已经支付

                    ResponseData(context, js.Serialize(true));
                }
            }
        }
    }
    /// <summary>
    /// 内容输出
    /// </summary>
    /// <param name="context"></param>
    /// <param name="jsonData"></param>
    public void ResponseData(HttpContext context, string jsonData)
    {
        //context.Response.Cache.SetNoStore();
        context.Response.Clear();
        // context.Response.Charset="utf-8";
        context.Response.ContentType = "text/plain";
        context.Response.Write(jsonData);
        context.Response.End();
        // context.Response.Cache.SetNoStore();
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
} 