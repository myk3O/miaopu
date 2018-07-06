using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maliang;

public partial class Admin_ActiveManag_ActiveMage : System.Web.UI.Page
{
    ActiveHelp news = new ActiveHelp();
    protected void Page_Load(object sender, EventArgs e)
    {
        imgdelete.Attributes.Add("onclick", "return confirm('请谨慎操作，你确认删除本记录?执行本操作将是不可逆的!')");
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            BindDrop(DropGroup, "ActiveClass");
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
        string sql = " select a.*,b.tClassName tClassNameb from Active a left join ActiveClass b on a.sid0=b.nID where 1=1";
        if (keyName.Value != "")
        {
            sql += " and (a.tMemo like '%" + keyName.Value.Trim() + "%' or dtPubTime like '%" + keyName.Value.Trim() + "%')";
        }
        if (DropGroup.SelectedItem.Value != "-1") { sql += " and a.sid0='" + DropGroup.SelectedItem.Value + "'"; }
       // if (DropName.SelectedItem.Value != "-1") { sql += " and oTop='" + DropName.SelectedItem.Value + "'"; }
        sql += " order by a.dtAddTime desc";
        DataTable dt = her.ExecuteDataTable(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["dtPubTime"].ToString() != "" && dt.Rows[i]["dtPubTime"] != null)
            {
                dt.Rows[i]["dtPubTime"] = Convert.ToDateTime(dt.Rows[i]["dtPubTime"]).ToString("yyyy-MM-dd");
            }
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
            Label lbMethods = (Label)gridField.Rows[i].FindControl("dtMethods");

            lbMethods.Text = StringDelHTML.Centers(StringDelHTML.DelHTML(lbMethods.Text),50);
            //Label lblState = (Label)gridField.Rows[i].FindControl("lblzhiding");
            //if (lblState.Text == "True")
            //{
            //    lblState.Text = "是";
            //}
            //else
            //{
            //    lblState.Text = "否";
            //}
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
            Response.Redirect("ActiveEdit.aspx?id=" + e.CommandArgument.ToString() + "&&ceid=" + gridField.PageIndex);
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
        Response.Redirect("ActiveEdit.aspx?ceid=" + gridField.PageIndex);
    }
}