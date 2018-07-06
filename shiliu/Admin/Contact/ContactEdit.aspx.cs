using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Contact_ContactEdit : System.Web.UI.Page
{
    NewsHelper newshelper = new NewsHelper();
    SqlHelper her = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        // if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            Initialization();

            imgs.Attributes["onclick"] = "vbscript:history.back()";

            if (Request.QueryString["id"] != "" && Request.QueryString["id"] != null)
            {
                imgs.Visible = true;
                Interface(Request.QueryString["id"].ToString());
            }
        }
    }

    //界面初始化
    public void Initialization()
    {
        txtTlitle.Text = "";

        content1.InnerText = "";

    }
    //根据ID查询信息并加载到界面
    public void Interface(string id)
    {
        string sql = "select * from ML_MemberMessage where nID=" + id;
        DataTable dt = her.ExecuteDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            txtTlitle.Text = dt.Rows[0]["tMemo"].ToString(); //留言内容

        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('获取数据失败！')</script>");
        }
    }


    //修改事件
    public void SubmitUpd(string ID)
    {

        if (!string.IsNullOrEmpty(content1.InnerText.Trim()))
        {
            string sql = @"update ML_MemberMessage set tTilte= '" + content1.InnerText.Trim() + "' , oHide=1, oTop=0 where nID=" + ID;
            if (her.ExecuteNonQuery(sql))
            {

                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改完成！')</script>");
                Response.Redirect("ContactMain.aspx?ceid=" + Request.QueryString["ceid"].ToString());
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改失败！')</script>");
            }

        }
    }
    protected void imgSub_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
        {
            SubmitUpd(Request.QueryString["id"].ToString());
        }
    }
    protected void imgback_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["ceid"] != "" && Request.QueryString["ceid"] != null)
        {
            Response.Redirect("ContactMain.aspx?ceid=" + Request.QueryString["ceid"].ToString());
        }
        else
        {
            Response.Redirect("ContactMain.aspx.aspx");
        }
    }
}