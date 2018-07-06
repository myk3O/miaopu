using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AdminMain : System.Web.UI.Page
{
    AdminManagHelper AdminMH = new AdminManagHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        imgdelete.Attributes.Add("onclick", "return confirm('请谨慎操作，你确认删除本记录?执行本操作将是不可逆的!')");
        if (!IsPostBack)
        {
            //BindDrop(DropGroup);
            //DropGroup.Items.Insert(0, new ListItem("所属群组", "-1", true));
            if (Request.QueryString["ceid"] != "" && Request.QueryString["ceid"] != null)
            {
                hid.Value = Request.QueryString["ceid"].ToString();
                string aa = Request.QueryString["ceid"].ToString();
            }
        }
        GridBind();
        if (hid.Value != "")
        {
            gridField.PageIndex = int.Parse(hid.Value);
            hid.Value = "";
        }
    }
    //绑定下拉菜单
    public void BindDrop(DropDownList drop)
    {
        SqlHelper her = new SqlHelper();
        string sql = "select nID,tClassName from [ML_AdminClass]";
        DataTable dt = her.ExecuteDataTable(sql);
        drop.DataSource = dt;
        drop.DataValueField = "nID";
        drop.DataTextField = "tClassName";
        drop.DataBind();
    }
    public void GridBind()
    {
        SqlHelper her = new SqlHelper();
        string sql = "select * from dbo.ML_Admin";// and ML_Admin.nID!=1
        if (keyName.Value != "")
        {
            sql += " and (tAdminName = '" + keyName.Value.Trim() + "' or tRealName =  '" + keyName.Value.Trim() + "')";
        }
        // if (DropGroup.SelectedItem.Value != "-1") { sql += " and tClassName='" + DropGroup.SelectedItem.Text + "'"; }
        if (DropState.SelectedItem.Value != "-1") { sql += " and oCheck='" + DropState.SelectedItem.Value + "'"; }
        DataTable dt = her.ExecuteDataTable(sql);
        Pagination2.MDataTable = dt;
        Pagination2.MGridView = gridField;
    }
    protected void ImgSrs_Click(object sender, ImageClickEventArgs e)
    {
        GridBind();
        Pagination2.Refresh();
    }
    protected void gridField_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            Label lblState = (Label)gridField.Rows[i].FindControl("lblState");



            if (lblState.Text == "True")
            {
                lblState.Text = "已审核";
            }
            else
            {
                lblState.Text = "未审核";
            }

            Label lblohide = (Label)gridField.Rows[i].FindControl("lblhide");
            if (lblohide.Text == "True")
            {
                //((Button)gridField.Rows[i].FindControl("btnUpdate")).Visible = false;
                ((Button)gridField.Rows[i].FindControl("btnDelete")).Visible = false;
            }

        }
    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            bool success = AdminMH.AdminDelete(e.CommandArgument.ToString());
            if (success)
            {
                GridBind();
                Pagination2.Refresh();
            }
        }
        if (e.CommandName == "update")
        {
            Response.Redirect("AdminEdit.aspx?id=" + e.CommandArgument.ToString() + "&&ceid=" + gridField.PageIndex);
        }
    }
    protected void imgAdd_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AdminEdit.aspx?ceid=" + gridField.PageIndex);
    }
    protected void btnSearh_Click(object sender, EventArgs e)
    {
        GridBind();
        Pagination2.Refresh();
    }
    protected void gridField_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void imgdelete_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            CheckBox ckb = (CheckBox)gridField.Rows[i].FindControl("CheckSel");
            if (ckb.Checked)
            {
                bool success = AdminMH.AdminDelete(gridField.DataKeys[i].Value.ToString());
                if (!success)
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误！请重试')</script>");
                }
            }
        }
        GridBind();
        Pagination2.Refresh();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminEdit.aspx?ceid=" + gridField.PageIndex);
    }
    protected void gridField_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (e.Row.RowIndex == 0)
            //{
            //    ((Button)(e.Row.FindControl("btnUpdate"))).Visible = false;
            //    ((Button)(e.Row.FindControl("btnDelete"))).Visible = false;
            //}
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor=\"#F6F9FA\"");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=\"" + e.Row.Style["BACKGROUND-COLOR"] + "\"");
        }
    }
}