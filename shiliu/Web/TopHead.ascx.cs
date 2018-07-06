using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maliang;
using System.Data;
using System.Text;

public partial class Web_TopHead : System.Web.UI.UserControl
{
    public string UserName;
    private string UserID
    {
        get
        {
            return ViewState["UserID"] == null ? "" : ViewState["UserID"].ToString();
        }
        set
        {
            ViewState["UserID"] = value;
        }
    }

    public string aboutUrl;
    public string productClass;

    public string cartCount = "0";
    SqlHelper sh = new SqlHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetSource();
            GetProductClass();
            if (Session["LoginUserId"] == null || Session["LoginUserId"].ToString() == "")
            {
                // Response.Redirect("passport-login.aspx");
            }
            else
            {
                UserID = Session["LoginUserId"].ToString();

                GetCartCount();

            }

            if (Session["LoginUserName"] != null && Session["LoginUserName"].ToString() != "")
            {
                UserName = Session["LoginUserName"].ToString();
                string header = string.Empty;
                string footer = string.Empty;
                //手机号截取
                if (UserName.Length >= 11)
                {
                    header = UserName.Substring(0, 3);
                    footer = UserName.Substring(UserName.Length - 4, 4);
                }
                UserName = header + "****" + footer;
            }
            else
            {
                UserName = "会员登录";
            }

        }
    }
    private void GetCartCount()
    {
        string sql = "select Count(*) from ML_Cart where UserID=" + UserID;
        cartCount = sh.ExecuteScalar(sql) == null ? "0" : sh.ExecuteScalar(sql).ToString();
    }

    private void GetSource()
    {
        StringBuilder sb = new StringBuilder();
        string sql = "select * from ML_InfoClass order by npaixu";
        DataTable dt = sh.ExecuteDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            sb.AppendLine("<li><a href='about.aspx?id=" + dr["nID"].ToString() + "'>" + dr["tClassName"].ToString() + "</a></li>");
        }
        aboutUrl = sb.ToString();
    }

    private void GetProductClass()
    {
        StringBuilder sb = new StringBuilder();
        string sql = "select * from ML_ServiceMainClass order by npaixu";
        DataTable dt = sh.ExecuteDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            sb.AppendLine("<li class='m-cat-depth-1 cat-item lv1 mod_cate_li first'>");
            sb.AppendLine("<div class='sm_bg'></div>");
            sb.AppendLine("<h3 class='cat-root-box'>");
            sb.AppendLine("<a class='gay lv1_title mod_cate_r1' href='gallery-100-grid.aspx?id=" + dr["nID"].ToString() + "&cN=" + dr["tClassName"].ToString() + "'><span>" + dr["tClassName"].ToString() + "</span> </a>");
            sb.AppendLine("<div class='cat-lv2-redundancy mod_cate_r2'></div>");
            sb.AppendLine("</h3>");
            sb.AppendLine("</li>");
        }
        productClass = sb.ToString();
    }
}