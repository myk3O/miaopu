using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maliang;

public partial class Admin_Activity_ActiveClass : System.Web.UI.Page
{
    ActiveHelp newshepler = new ActiveHelp();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            tab.Visible = false;
            GridBind();
        }
    }
    public void GridBind()
    {
        SqlHelper her = new SqlHelper();
        string sql = "select * from ActiveClass order by nPaiXu asc";
        DataTable dt = her.ExecuteDataTable(sql);
        gridField.DataSource = dt;
        gridField.DataBind();
    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            if (newshepler.DelNewsClass(e.CommandArgument.ToString()))
            {
                GridBind();
            }
        }
        if (e.CommandName == "update")
        {
            DataTable dt = newshepler.SelNewsClass(e.CommandArgument.ToString());
            if (dt.Rows.Count > 0)
            {
                txtfenleiName.Text = dt.Rows[0]["tClassName"].ToString();
                txtnum.Text = dt.Rows[0]["nPaiXu"].ToString();
                hid.Value = dt.Rows[0]["nID"].ToString();
                tab.Visible = true;
                imgAdd.Visible = false;
                imgSub.Visible = true;
            }
        }
    }
    protected void gridField_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor=\"#F6F9FA\"");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=\"" + e.Row.Style["BACKGROUND-COLOR"] + "\"");
        }
    }
    protected void gridField_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void imgAdd_Click(object sender, EventArgs e)
    {
        if (txtfenleiName.Text == "" || txtnum.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请输入！')</script>");
            return;
        }
        if (newshepler.addNewsClass(txtfenleiName.Text.Trim(), txtnum.Text.Trim()))
        {
            //ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加成功！')</script>");
            tab.Visible = false;
            GridBind();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加失败！')</script>");
            return;
        }
    }
    protected void imgSub_Click(object sender, EventArgs e)
    {
        if (txtfenleiName.Text == "" || txtnum.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请输入！')</script>");
            return;
        }
        if (newshepler.updateNewsClass(hid.Value, txtfenleiName.Text.Trim(), txtnum.Text.Trim()))
        {
            tab.Visible = false;
            GridBind();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('失败！')</script>");
            return;
        }
    }
    protected void imgback_Click(object sender, EventArgs e)
    {
        tab.Visible = false;
    }
    protected void gridField_DataBound(object sender, EventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        tab.Visible = true;
        imgAdd.Visible = true;
        imgSub.Visible = false;
        txtfenleiName.Text = "";
        txtnum.Text = "";
    }
}