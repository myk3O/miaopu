using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_News_NewsMain : System.Web.UI.Page
{
    NewsHelper news = new NewsHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        imgdelete.Attributes.Add("onclick", "return confirm('请谨慎操作，你确认删除本记录?执行本操作将是不可逆的!')");
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            BindDrop(DropGroup, "ML_NewsClass");
            DropGroup.Items.Insert(0, new ListItem("所有分类", "-1", true));
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
        string sql = "select * from  ML_News where oHide=0";
        if (keyName.Value != "")
        {
            sql += " and (tTitle like '%" + keyName.Value.Trim() + "%' or dtPubTime like '%" + keyName.Value.Trim() + "%')";
        }
        if (DropGroup.SelectedItem.Value != "-1") { sql += " and ML_NewsClass.nID='" + DropGroup.SelectedItem.Value + "'"; }
        if (DropName.SelectedItem.Value != "-1") { sql += " and oTop='" + DropName.SelectedItem.Value + "'"; }
        sql += " order by oNewest desc,dtAddTime desc";
        DataTable dt = her.ExecuteDataTable(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //if (dt.Rows[i]["dtPubTime"].ToString() != "" && dt.Rows[i]["dtPubTime"] != null)
            //{
            //    dt.Rows[i]["dtPubTime"] = Convert.ToDateTime(dt.Rows[i]["dtPubTime"]).ToString("yyyy-MM-dd");
            //}
        }
        Pagination2.MDataTable = dt;
        Pagination2.MGridView = gridField;
    }
    //绑定下拉菜单
    public void BindDrop(DropDownList drop, string name)
    {
        SqlHelper her = new SqlHelper();
        string sql = "select * from " + name;
        DataTable dt = her.ExecuteDataTable(sql);
        drop.DataSource = dt;
        drop.DataValueField = "nID";
        drop.DataTextField = "tClassName";
        drop.DataBind();
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
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            Label lblState = (Label)gridField.Rows[i].FindControl("lblzhiding");
            if (lblState.Text == "True")
            {
                lblState.Text = "已发布";
            }
            else
            {
                lblState.Text = "未发布";
            }

            Label lbtMemo = (Label)gridField.Rows[i].FindControl("lbtMemo");
            lbtMemo.Text = StringDelHTML.Centers(StringDelHTML.DelHTML(lbtMemo.Text), 80);
        }
    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            bool success = news.NewsDelete(e.CommandArgument.ToString());
            if (success)
            {
                GridBind();
                Pagination2.Refresh();
            }
        }
        if (e.CommandName == "update")
        {
            Response.Redirect("NewsEdit.aspx?id=" + e.CommandArgument.ToString() + "&&ceid=" + gridField.PageIndex);
        }
        if (e.CommandName == "btnUser")
        {
            string jumpUrl = @"window.open('AddNewsMember.aspx?id=" + e.CommandArgument + "', 'top', 'width=1000,height=700,menubar=0,scrollbars=1, resizable=1,status=1,titlebar=0,toolbar=0,location=0')";

            ClientScript.RegisterStartupScript(GetType(), "", "<script>" + jumpUrl + "</script>");
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
                bool success = news.NewsDelete(gridField.DataKeys[i].Value.ToString());
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