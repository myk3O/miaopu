using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_BackCard_BackCardMain : System.Web.UI.Page
{
    Bank bk = new Bank();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["anid"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            if (Request.QueryString["ceid"] != "" && Request.QueryString["ceid"] != null)
            {
                gridField.PageIndex = int.Parse(Request.QueryString["ceid"].ToString());
            }
        }
        GridBind();
    }
    //绑定GridView
    public void GridBind()
    {
        string where = "";
        if (keyName.Value.Trim() != "") { where += " and Zhanghao like '%" + keyName.Value.Trim() + "%' or Huming like '%" + keyName.Value.Trim() + "%' or Kaihu like '%" + keyName.Value.Trim() + "%'"; }

        DataTable dt = bk.GetBank(Session["anid"].ToString(), where);
        Pagination2.MDataTable = dt;
        Pagination2.MGridView = gridField;
    }


    protected void btnSearh_Click(object sender, EventArgs e)
    {
        GridBind();
        Pagination2.Refresh();
    }
    protected void gridField_DataBound(object sender, EventArgs e)
    {

    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "update")
        {
            Response.Redirect("BackCardEdit.aspx?id=" + e.CommandArgument.ToString() + "&&ceid=" + gridField.PageIndex);
        }
        if (e.CommandName == "dingdan")
        {
            bool success = bk.DelBank(e.CommandArgument.ToString());
            if (!success)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误！请重试')</script>");
            }
            else
            {
                GridBind();
                Pagination2.Refresh();
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
        Response.Redirect("BackCardEdit.aspx?ceid=" + gridField.PageIndex);
    }
    protected void imgdelete_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            CheckBox ckb = (CheckBox)gridField.Rows[i].FindControl("CheckSel");
            if (ckb.Checked)
            {
                var nID = gridField.DataKeys[i].Value.ToString();
                bool success = bk.DelBank(nID);
                if (!success)
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误！请重试')</script>");
                }
            }
        }
        GridBind();
        Pagination2.Refresh();
    }
}