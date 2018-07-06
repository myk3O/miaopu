<%@ WebHandler Language="C#" Class="GetQuestion" %>

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

public class GetQuestion : IHttpHandler, IRequiresSessionState
{
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
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        //context.Response.CacheControl = "public";
        //context.Response.Expires = 1; //1分钟
    }

    public void GetQuestions(HttpContext context)
    {
        StringBuilder sb = new StringBuilder();
        string sql = @"select a.sid0, a.nID,a.Q1,a.Q2,a.Q3,a.Q4,a.tTitlle,a.oHide from  ML_Gongdi a
                        join ML_GongdiClass b on a.sid0=b.nID where b.sid2=1 order by a.nPaixu";
        //where c.sid1=" + userid;

        DataTable dt = her.ExecuteDataTable(sql);

        if (dt.Rows.Count > 0)
        {
            //sb.AppendLine("<li id='hiddenV' title='" + dt.Rows[0]["sid0"].ToString() + "' style='display: none'></li>");
            //sb.AppendLine("<input type='hidden' id='hiddenV' value='" + dt.Rows[0]["sid0"].ToString() + "'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var nID = dt.Rows[i]["nID"].ToString();
                var sid0 = dt.Rows[0]["sid0"].ToString();

                if (dt.Rows[i]["oHide"].ToString() == "False")
                {
                    sb.AppendLine("<li data-input-trigger id='" + sid0 + "'>");
                    sb.AppendLine("<label class='fs-field-label fs-anim-upper' for='" + nID + "' data-info='您的反馈对我们非常重要'> " + dt.Rows[i]["tTitlle"].ToString() + "</label>");
                    sb.AppendLine(" <div class='fs-radio-group fs-radio-custom clearfix fs-anim-lower'>");
                    sb.AppendLine(" <span><input id='" + nID + "a' name='" + nID + "' type='radio' value='A' /><label for='" + nID + "a' class='radio-A'> " + dt.Rows[i]["Q1"].ToString() + "</label></span>");
                    sb.AppendLine(" <span><input id='" + nID + "b' name='" + nID + "' type='radio' value='B' /><label for='" + nID + "b' class='radio-B'> " + dt.Rows[i]["Q2"].ToString() + "</label></span>");
                    sb.AppendLine("<span> <input id='" + nID + "c' name='" + nID + "' type='radio' value='C' /><label for='" + nID + "c' class='radio-C'> " + dt.Rows[i]["Q3"].ToString() + "</label></span>");
                    sb.AppendLine("<span> <input id='" + nID + "d' name='" + nID + "' type='radio' value='D' /><label for='" + nID + "d' class='radio-D'> " + dt.Rows[i]["Q4"].ToString() + "</label></span>");
                    sb.AppendLine("</div></li>");
                }
                else
                {
                    sb.AppendLine("<li id='" + sid0 + "'> <label class='fs-field-label fs-anim-upper' for='" + nID + "' data-info='请输入内容'>  " + dt.Rows[i]["tTitlle"].ToString() + "</label>");
                    sb.AppendLine("<textarea class='fs-anim-lower' id='" + nID + "' name='" + nID + "'  placeholder='在这里填写内容' required/ ></textarea> </li>");

                }

            }
        }
        else
        {
            sb.AppendLine("<div style='font-size: 16px; text-align: center; margin-top: 100px; color: #ccc'>未找到相关问卷</div>");
        }

        ResponseData(context, js.Serialize(sb.ToString()));
    }



    public void SubmitXuanZe(HttpContext context)
    {
        if (context.Request["nID"] != null && context.Request["nID"].ToString() != "" && context.Request["sid0"] != null && context.Request["sid0"].ToString() != "")
        {
            if (context.Request["chk"] != null && context.Request["chk"].ToString() != "")
            {
                int chouse = 1;
                var str = context.Request["chk"].ToString();
                string _rusult = str.Substring(str.Length - 1, 1);
                switch (_rusult)
                {
                    case "a": chouse = 1; break;
                    case "b": chouse = 2; break;
                    case "c": chouse = 3; break;
                    case "d": chouse = 4; break;
                    default: chouse = 1; break;
                }

                string insert = string.Format(@"insert into ML_GongdiXuanZe values({0},{1},{2},'{3}')"
                    , Convert.ToInt32(context.Request["sid0"]), Convert.ToInt32(context.Request["nID"]), chouse, System.DateTime.Now.ToString());
                bool success = her.ExecuteNonQuery(insert);
                if (success)
                {
                    ResponseData(context, js.Serialize("OK"));
                }

            }


        }
    }


    public void SubmitWenDa(HttpContext context)
    {
        if (context.Request["nID"] != null && context.Request["nID"].ToString() != "" && context.Request["sid0"] != null && context.Request["sid0"].ToString() != "")
        {
            if (context.Request["content"] != null && context.Request["content"].ToString() != "")
            {
                string insert = string.Format(@"insert into ML_GongdiWenDa values({0},{1},'{2}','{3}')"
                    , Convert.ToInt32(context.Request["sid0"].ToString()), Convert.ToInt32(context.Request["nID"].ToString())
                    , context.Request["content"].ToString(), System.DateTime.Now.ToString());
                bool success = her.ExecuteNonQuery(insert);
                if (success)
                {
                    ResponseData(context, js.Serialize("OK"));
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