using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CustomerConfig : System.Web.UI.Page
{
    WebHelper web = new WebHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../Error.aspx"); }
        if (!IsPostBack)
        {
            tab.Visible = false;
        }
        GridBind();
    }
    public void GridBind()
    {
        SqlHelper her = new SqlHelper();
        string sql = "select * from ML_CustomerConfig order by nPaiXu asc";
        DataTable dt = her.ExecuteDataTable(sql);
        Pagination2.MDataTable = dt;
        Pagination2.MGridView = gridField;
    }
    protected void gridField_DataBound(object sender, EventArgs e)
    {

    }
    protected void gridField_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            bool success = web.DelCustomer(e.CommandArgument.ToString());
            if (success)
            {
                GridBind();
                Pagination2.Refresh();
            }
        }
        if (e.CommandName == "update")
        {
            DataTable dt = web.SelCustomer(e.CommandArgument.ToString());
            if (dt.Rows.Count > 0)
            {
                tab.Visible = true;
                imgAdd.Visible = false;
                imgSub.Visible = true;
                txtfenleiName.Text = dt.Rows[0]["tTilte"].ToString();
                txtnum.Text = dt.Rows[0]["nPaiXu"].ToString();
                txtMemo.Text = dt.Rows[0]["tMemo"].ToString();
                hid1.Value = dt.Rows[0]["nID"].ToString();
            }
        }
    }
    protected void imgAdd_Click(object sender, EventArgs e)
    {
        if (txtfenleiName.Text == "" || txtnum.Text == "" || txtMemo.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请输入！')</script>");
            return;
        }
        if (web.AddCustomer(txtfenleiName.Text.Trim(), txtnum.Text.Trim(), txtMemo.Text.Trim()))
        {
            //ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加成功！')</script>");
            tab.Visible = false;
            GridBind();
            Pagination2.Refresh();
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
        bool success = false;
        success = web.updateCustomer(hid1.Value, txtfenleiName.Text.Trim(), txtnum.Text.Trim(), txtMemo.Text.Trim());
        if (success)
        {
            //ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加成功！')</script>");
            tab.Visible = false;
            GridBind();
            Pagination2.Refresh();
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        tab.Visible = true;
        imgAdd.Visible = true;
        imgSub.Visible = false;
        txtfenleiName.Text = "";
        txtnum.Text = "";
        txtMemo.Text = "";
    }
    protected void gridField_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor=\"#F6F9FA\"");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=\"" + e.Row.Style["BACKGROUND-COLOR"] + "\"");
        }
    }
}