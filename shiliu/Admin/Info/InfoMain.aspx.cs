using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Info_InfoMain : System.Web.UI.Page
{
    InfoHelper info = new InfoHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        imgdelete.Attributes.Add("onclick", "return confirm('请谨慎操作，你确认删除本记录?执行本操作将是不可逆的!')");
        if (!IsPostBack)
        {
            DropGroup.Items.Add(new ListItem("请选择", "-1"));
            BindDrop(DropGroup, "dbo.ML_InfoClass");
           // DropState.Items.Add(new ListItem("请选择", "-1"));
            //BindDrop(DropState, "dbo.ML_InfoClassMain");
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
        string sql = @"select ML_Info.*,ML_InfoClass.tClassName from dbo.ML_Info inner join dbo.ML_InfoClass on ML_Info.sid0=ML_InfoClass.nID where 1=1";
        if (DropGroup.SelectedItem.Value != "-1")
        {
            sql += " and sid0=" + DropGroup.SelectedItem.Value;
        }
        DataTable dt = her.ExecuteDataTable(sql);
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    if (dt.Rows[i]["dtPubTime"].ToString() != "" && dt.Rows[i]["dtPubTime"] != null)
        //    {
        //        dt.Rows[i]["dtPubTime"] = Convert.ToDateTime(dt.Rows[i]["dtPubTime"]).ToString("yyyy-MM-dd");
        //    }
        //}
        Pagination2.MDataTable = dt;
        Pagination2.MGridView = gridField;
    }
    //绑定下拉菜单
    public void BindDrop(DropDownList drop, string names)
    {
        SqlHelper her = new SqlHelper();
        string sql = "select nID,tClassName from " + names;
        DataTable dt = her.ExecuteDataTable(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            drop.Items.Add(new ListItem(dt.Rows[i]["tClassName"].ToString(), dt.Rows[i]["nID"].ToString()));
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
    protected void gridField_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            Label tMemo = gridField.Rows[i].FindControl("tMemo") as Label;
            tMemo.Text = StringDelHTML.Centers(StringDelHTML.DelHTML(tMemo.Text), 70);
        }
    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            bool success = info.InfoDelete(e.CommandArgument.ToString());
            if (success)
            {
                GridBind();
                Pagination2.Refresh();
            }
        }
        if (e.CommandName == "update")
        {
            int aa = gridField.PageIndex;
            Response.Redirect("InfoEdit.aspx?id=" + e.CommandArgument.ToString() + "&&ceid=" + gridField.PageIndex);
        }
    }
    //protected void DropState_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (DropState.SelectedItem.Value != "-1")
    //    {
    //        DropGroup.Items.Clear();
    //        SqlHelper her = new SqlHelper();
    //        string sql = "select nID,tClassName from ML_InfoClass where sid0=" + DropState.SelectedItem.Value;
    //        DataTable dt = her.ExecuteDataTable(sql);
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            DropGroup.Items.Add(new ListItem(dt.Rows[i]["tClassName"].ToString(), dt.Rows[i]["nID"].ToString()));
    //        }
    //    }
    //    else
    //    {
    //        DropGroup.Items.Add(new ListItem("请选择", "-1"));
    //        BindDrop(DropGroup, "dbo.ML_InfoClass");
    //    }
    //}
    protected void imgdelete_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            CheckBox ckb = (CheckBox)gridField.Rows[i].FindControl("CheckSel");
            if (ckb.Checked)
            {
                bool success = info.InfoDelete(gridField.DataKeys[i].Value.ToString());
                if (!success)
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误！请重试')</script>");
                }
            }
        }
        GridBind();
        Pagination2.Refresh();
    }
    protected void btnSearh_Click(object sender, EventArgs e)
    {
        GridBind();
        Pagination2.Refresh();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("InfoEdit.aspx?ceid=" + gridField.PageIndex);
    }
}