using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Admin_DaiLi_HomeMakByMak : System.Web.UI.Page
{
    public string BindTitle;
    public string BindStr;
    private string nID
    {
        get
        {
            return ViewState["nID"].ToString();
        }
        set
        {
            ViewState["nID"] = value;
        }
    }
    SqlHelper her = new SqlHelper();
    LoginVerification logVer = new LoginVerification();
    DaiLi makbll = new DaiLi();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            if (Session["anid"] == null) { Response.Redirect("../../Error.aspx"); } else { nID = Session["anid"].ToString(); }


            BindSource(nID);
        }
    }

    private void BindSource(string nid)
    {
        string strState = string.Empty;
        DataTable dt = logVer.GetLoginBynID(nid);//账户信息
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string faterDLname = makbll.HomeMakName(dt.Rows[0]["nLogNum"].ToString());
            //代理归属
            //sb.AppendLine("<hr><div class='misc-info'>");
            //sb.AppendLine("<h3>代理归属</h3>");
            //sb.AppendLine("<dl><dt>上级代理：</dt> <dd>" + faterDLname + "</dd></dl>");

            //if (Request.QueryString["id"] != null && Request.QueryString["id"] != "" && Session["acid"].ToString() != "3")//不是总部查看详细
            //{

            //}
            //else
            //{
            //    sb.AppendLine("<dl>");
            //    sb.AppendLine("<dt>登录账号：</dt> <dd>" + dt.Rows[0]["HomeName"].ToString() + "</dd>");
            //    sb.AppendLine("<dt>登录密码：</dt> <dd>" + dt.Rows[0]["HomePass"].ToString() + "</dd>");
            //    sb.AppendLine("<dl>");
            //}

            //sb.AppendLine("<div class='clearfix'></div></div>");
            //基本信息
            sb.AppendLine("<hr><div class='misc-info'>");
            sb.AppendLine("<h3>基本信息</h3>");
            sb.AppendLine("<dl>");
            sb.AppendLine("<dt>个人/公司名称：</dt> <dd>" + dt.Rows[0]["tRealName"].ToString() + "</dd>");
            sb.AppendLine("<dt>联系人：</dt> <dd>" + dt.Rows[0]["HomeIntegral"].ToString() + "</dd>");
            sb.AppendLine("</dl>");
            sb.AppendLine("<dl>");
            sb.AppendLine("<dt>固定电话：</dt> <dd>" + dt.Rows[0]["HomePhone"].ToString() + "</dd>");
            sb.AppendLine("<dt>手机：</dt> <dd>" + dt.Rows[0]["HomeMobile"].ToString() + "</dd>");
            sb.AppendLine("</dl>");
            sb.AppendLine("<dl><dt>邮箱：</dt> <dd>" + dt.Rows[0]["HomeEmail"].ToString() + "</dd></dl>");

            sb.AppendLine("<div class='clearfix'></div></div>");

            //商城地址   
            sb.AppendLine("<hr><div class='addr_and_note1'>");
            sb.AppendLine("<h3>商城地址</h3>");
            sb.AppendLine("<dl><dt>地址：</dt> <dd>" + dt.Rows[0]["CreatorName"].ToString() + "</dd></dl>");
            if (string.IsNullOrEmpty(dt.Rows[0]["HomePic"].ToString()))
            {
                sb.AppendLine("<dl><dt>二维码：</dt> <dd></dd></dl>");
            }
            else
            {
                sb.AppendLine("<dl><dt>二维码：</dt> <dd><img src='../Upload/pic/" + dt.Rows[0]["HomePic"].ToString() + "' /></dd></dl>");
            }
            sb.AppendLine("<div class='clearfix'></div></div>");


            //地址
            string addr = dt.Rows[0]["HomePro"].ToString() + " " + dt.Rows[0]["HomeCity"].ToString() + " " + dt.Rows[0]["HomeIntro"].ToString();
            sb.AppendLine("<hr><div class='misc-info'>");
            sb.AppendLine("<h3>详细地址</h3>");
            sb.AppendLine("<dl>");
            sb.AppendLine("<dt>邮编：</dt> <dd>" + dt.Rows[0]["CreatorCode"].ToString() + "</dd>");
            sb.AppendLine("<dt>地址：</dt> <dd>" + addr + "</dd>");
            sb.AppendLine("<dl>");
            sb.AppendLine("<div class='clearfix'></div></div>");



            //其他信息   
            sb.AppendLine("<hr><div class='addr_and_note1'>");
            sb.AppendLine("<h3>其他信息</h3>");
            if (string.IsNullOrEmpty(dt.Rows[0]["tPic"].ToString()))
            {
                sb.AppendLine("<dl><dt>相关证件：</dt> <dd></dd></dl>");
            }
            else
            {
                sb.AppendLine("<dl><dt>相关证件：</dt> <dd><img src='../Upload/" + dt.Rows[0]["tPic"].ToString() + "' /></dd></dl>");
            }
            sb.AppendLine("<dl><dt>其他：</dt><dd>" + dt.Rows[0]["tMemo"].ToString() + "</dd></dl>");
            sb.AppendLine("<div class='clearfix'></div></div>");


        }

        BindStr = sb.ToString();


    }


    protected void btnEdit_Click(object sender, EventArgs e)
    {
        // string nid = Session["anid"].ToString();
        Response.Redirect("HomeEdit.aspx?id=" + nID);
    }
}