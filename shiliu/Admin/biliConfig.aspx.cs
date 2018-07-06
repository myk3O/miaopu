using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Configuration;
using System.Text;

using Maliang;
public partial class Admin_biliConfig : System.Web.UI.Page
{
    SqlHelper her = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../Error.aspx"); }
        if (!IsPostBack)
        {
            Bindding();
        }

    }
    //初始化
    public void Bindding()
    {
        string sql = "select * from ML_SysBiLi";
        DataTable dt = her.ExecuteDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            leave1.Text = dt.Select("BiliName='leave1'")[0]["BiLi"].ToString();
            leave2.Text = dt.Select("BiliName='leave2'")[0]["BiLi"].ToString();
            leave3.Text = dt.Select("BiliName='leave3'")[0]["BiLi"].ToString();
            leave_1.Text = dt.Select("BiliName='leave_1'")[0]["BiLi"].ToString();
            leave_2.Text = dt.Select("BiliName='leave_2'")[0]["BiLi"].ToString();
            leave_3.Text = dt.Select("BiliName='leave_3'")[0]["BiLi"].ToString();
            leave1count.Text = dt.Select("BiliName='leave1count'")[0]["BiLi"].ToString();
            leave2count.Text = dt.Select("BiliName='leave2count'")[0]["BiLi"].ToString();
            leave3count.Text = dt.Select("BiliName='leave3count'")[0]["BiLi"].ToString();
            jixiao3.Text = dt.Select("BiliName='jixiao3'")[0]["BiLi"].ToString();
            jixiao1.Text = dt.Select("BiliName='jixiao1'")[0]["BiLi"].ToString();
            jixiao2.Text = dt.Select("BiliName='jixiao2'")[0]["BiLi"].ToString();
            learnPart.Text = dt.Select("BiliName='learnPart'")[0]["BiLi"].ToString();
            shuifei.Text = dt.Select("BiliName='shuifei'")[0]["BiLi"].ToString();
            xuebaCount.Text = dt.Select("BiliName='xuebaCount'")[0]["BiLi"].ToString();
            //
            txtgzFirst.Text = dt.Select("BiliName='gzFirst'")[0]["BiLi"].ToString();
            txtgzPart.Text = dt.Select("BiliName='gzPart'")[0]["BiLi"].ToString();
        }
    }


    protected void btnSub_Click(object sender, EventArgs e)
    {
        StringBuilder sql = new StringBuilder();

        sql.AppendLine(" update ML_SysBiLi  set BiLi='" + leave1.Text.Trim() + "' where BiliName='leave1';");
        sql.AppendLine(" update ML_SysBiLi  set BiLi='" + leave2.Text.Trim() + "' where BiliName='leave2';");
        sql.AppendLine(" update ML_SysBiLi  set BiLi='" + leave3.Text.Trim() + "' where BiliName='leave3';");
        sql.AppendLine(" update ML_SysBiLi  set BiLi='" + leave_1.Text.Trim() + "' where BiliName='leave_1';");
        sql.AppendLine(" update ML_SysBiLi  set BiLi='" + leave_2.Text.Trim() + "' where BiliName='leave_2';");
        sql.AppendLine(" update ML_SysBiLi  set BiLi='" + leave_3.Text.Trim() + "' where BiliName='leave_3';");
        sql.AppendLine(" update ML_SysBiLi  set BiLi='" + leave1count.Text.Trim() + "' where BiliName='leave1count';");
        sql.AppendLine(" update ML_SysBiLi  set BiLi='" + leave2count.Text.Trim() + "' where BiliName='leave2count';");
        sql.AppendLine(" update ML_SysBiLi  set BiLi='" + leave3count.Text.Trim() + "' where BiliName='leave3count';");
        sql.AppendLine(" update ML_SysBiLi  set BiLi='" + jixiao3.Text.Trim() + "' where BiliName='jixiao3';");
        sql.AppendLine(" update ML_SysBiLi  set BiLi='" + jixiao1.Text.Trim() + "' where BiliName='jixiao1';");
        sql.AppendLine(" update ML_SysBiLi  set BiLi='" + jixiao2.Text.Trim() + "' where BiliName='jixiao2';");
        sql.AppendLine(" update ML_SysBiLi  set BiLi='" + learnPart.Text.Trim() + "' where BiliName='learnPart';");
        sql.AppendLine(" update ML_SysBiLi  set BiLi='" + shuifei.Text.Trim() + "' where BiliName='shuifei';");
        sql.AppendLine(" update ML_SysBiLi  set BiLi='" + xuebaCount.Text.Trim() + "' where BiliName='xuebaCount';");
        //
        sql.AppendLine(" update ML_SysBiLi  set BiLi='" + txtgzFirst.Text.Trim() + "' where BiliName='gzFirst';");
        sql.AppendLine(" update ML_SysBiLi  set BiLi='" + txtgzPart.Text.Trim() + "' where BiliName='gzPart';");
        if (her.ExecuteNonQuery(sql.ToString()))
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('更新成功！')</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('更新失败！')</script>");
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }


}