using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EmailConfig : System.Web.UI.Page
{
    AdminManagHelper adminMH = new AdminManagHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../Error.aspx"); }
        if (!IsPostBack)
        {
            Bind();
        }
    }
    //初始化
    public void Bind()
    {
        DataTable dt = adminMH.WebsiteInformation();
        if (dt.Rows.Count > 0)
        {
            txtStmp.Text = dt.Rows[0]["Estmp"].ToString();
            txtFemail.Text = dt.Rows[0]["EFemail"].ToString();
            txtFpass.Attributes.Add("value", dt.Rows[0]["EFpass"].ToString());
            //txtFname.Text = dt.Rows[0]["EFname"].ToString();
            txtSemail.Text = dt.Rows[0]["ESemail"].ToString();
            txtEmailname.Text = dt.Rows[0]["Ename"].ToString();
            hid.Value = dt.Rows[0]["nID"].ToString();
        }
        else
        {
            hid.Value = "";
        }
    }
    protected void ImgbtnSub_Click(object sender, ImageClickEventArgs e)
    {
        if (hid.Value != "")
        {
            bool success = adminMH.EmailUpdate(hid.Value, txtFemail.Text.Trim(), txtFpass.Text.Trim(), "", txtSemail.Text.Trim(), txtEmailname.Text.Trim(), txtStmp.Text.Trim());
            if (success)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('成功！')</script>");
                Bind();
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误，更新失败2！')</script>");
            }
        }
        else
        {
            bool success = adminMH.EmailInsert(txtFemail.Text.Trim(), txtFpass.Text.Trim(), "", txtSemail.Text.Trim(), txtEmailname.Text.Trim(), txtStmp.Text.Trim());
            if (success)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('成功！')</script>");
                Bind();
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误，更新失败1！')</script>");
            }
        }
    }
}