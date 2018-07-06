using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Main_Content : System.Web.UI.Page
{
    SqlHelper her = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            Content();
        }
    }
    public void Content()
    {
        string sql = string.Format(@"select * from ML_Admin where nID={0}", Session["aid"].ToString());
        DataTable dt = her.ExecuteDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            lbltilte.InnerText = dt.Rows[0]["tRealName"].ToString() + "(" + dt.Rows[0]["tAdminName"].ToString() + ")" + StringDataTime.GetAftenoon() + "好，欢迎使用网站后台管理系统";
            lbltime.InnerText = "您上次登录的时间：" + Session["LastTime"].ToString();
        }
    }
}