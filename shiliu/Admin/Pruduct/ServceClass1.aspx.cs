using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maliang;

public partial class Admin_Pruduct_ServceClass1 : System.Web.UI.Page
{
    ServceHelper servce = new ServceHelper();
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

        string sql = "select * from dbo.ML_ServiceAreaClass1 order by nPaiXu asc";
        DataTable dt = her.ExecuteDataTable(sql);
        gridField.DataSource = dt;
        gridField.DataBind();
    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            if (servce.DelServiceClass1(e.CommandArgument.ToString()))
            {
                GridBind();
            }
        }
        if (e.CommandName == "update")
        {
            DataTable dt = servce.SelServiceClass1(e.CommandArgument.ToString());
            if (dt.Rows.Count > 0)
            {
                string aa = dt.Rows[0]["sid0"].ToString();
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        tab.Visible = true;
        imgAdd.Visible = true;
        imgSub.Visible = false;
        txtfenleiName.Text = "";
        txtnum.Text = "";
    }
    protected void imgAdd_Click(object sender, EventArgs e)
    {
        if (txtfenleiName.Text == "" || txtnum.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请输入！')</script>");
            return;
        }
        bool success = servce.addServiceClass1(txtfenleiName.Text.Trim(), txtnum.Text.Trim());
        if (success)
        {
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
        if (servce.updateServiceClass1(hid.Value, txtfenleiName.Text.Trim(), txtnum.Text.Trim()))
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
}