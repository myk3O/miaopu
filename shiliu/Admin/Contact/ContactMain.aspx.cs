using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Contact_ContactMain : System.Web.UI.Page
{
    WebHelper web = new WebHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        imgdelete.Attributes.Add("onclick", "return confirm('请谨慎操作，你确认删除本记录?执行本操作将是不可逆的!')");
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
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
    public void GridBind()
    {
        SqlHelper her = new SqlHelper();
        string sql = "select a.*,b.* from dbo.ML_MemberMessage a  join ML_Member  b on a.sid0=b.nID  where 1=1";
        if (keyName.Value != "")
        {
            sql += " and (b.tRealName like '%" + keyName.Value.Trim() + "%' or b.MemberPhone like '%" + keyName.Value.Trim() + "%' or b.MemberName like '%" + keyName.Value.Trim() + "%' or a.tMemo like '%" + keyName.Value.Trim() + "%' )";
        }
        if (txtBegin.Value == "")
        {
            if (txtEnd.Value != "")
            {
                sql += " and (a.dtAddTime < '" + txtEnd.Value.Trim() + " 23:59:59')";
            }
        }
        else
        {
            if (txtEnd.Value == "")
            {
                sql += " and (a.dtAddTime > '" + txtBegin.Value.Trim() + " 00:00:00')";
            }
            else
            {
                sql += " and (a.dtAddTime > '" + txtBegin.Value.Trim() + " 00:00:00') and (a.dtAddTime < '" + txtEnd.Value.Trim() + " 23:59:59')";
            }
        }

        sql += " order by a.dtPubTime desc";
        DataTable dt = her.ExecuteDataTable(sql);
        Pagination2.MDataTable = dt;
        Pagination2.MGridView = gridField;
    }
    protected void gridField_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor=\"#F6F9FA\"");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=\"" + e.Row.Style["BACKGROUND-COLOR"] + "\"");
        }
    }
    protected void gridField_DataBound(object sender, EventArgs e)
    {

    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            bool success = web.DelMessage(e.CommandArgument.ToString());
            if (success)
            {
                GridBind();
                Pagination2.Refresh();
            }
        }

        if (e.CommandName == "res")
        {
            string url = string.Format("ContactEdit.aspx?id={0}&ceid={1}", e.CommandArgument.ToString(), gridField.PageIndex);

            Response.Redirect(url);
        }
    }
    protected void btnSearh_Click(object sender, EventArgs e)
    {
        GridBind();
        Pagination2.Refresh();
    }
    protected void imgdelete_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            CheckBox ckb = (CheckBox)gridField.Rows[i].FindControl("CheckSel");
            if (ckb.Checked)
            {
                bool success = web.DelMessage(gridField.DataKeys[i].Value.ToString());
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
        Response.Redirect("NewsEdit.aspx?ceid=" + gridField.PageIndex);
    }
}