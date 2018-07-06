using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Info_InfoClass : System.Web.UI.Page
{
    InfoHelper info = new InfoHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            tab.Visible = false;
            Dropfenlei.Items.Add(new ListItem("请选择", "-1"));
            BindDrop(Dropfenlei, "ML_InfoClassMain");
            GridBind();
        }
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
    public void GridBind()
    {
        SqlHelper her = new SqlHelper();
        string sql = "select ML_InfoClass.*,ML_InfoClassMain.nID as tid,ML_InfoClassMain.tClassName as tidname from dbo.ML_InfoClass inner join dbo.ML_InfoClassMain on ML_InfoClass.sid0=dbo.ML_InfoClassMain.nID";
        sql += " order by ML_InfoClassMain.nPaiXu asc,ML_InfoClass.nPaiXu asc";
        DataTable dt = her.ExecuteDataTable(sql);
        gridField.DataSource = dt;
        gridField.DataBind();
    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del") 
        {
            if (info.DelInfoClass(e.CommandArgument.ToString()))
            {
                GridBind();
            }
        }
        if (e.CommandName == "update")
        {
            DataTable dt = info.SelInfoClass(e.CommandArgument.ToString());
            if (dt.Rows.Count > 0)
            {
                Dropfenlei.SelectedValue = dt.Rows[0]["sid0"].ToString();
                txtfenleiName.Text = dt.Rows[0]["tClassName"].ToString();
                txtnum.Text = dt.Rows[0]["nPaiXu"].ToString();
                hid.Value = dt.Rows[0]["nID"].ToString();
                tab.Visible = true;
                imgAdd.Visible = false;
                imgSub.Visible = true;
            }
        }
    }
   
    protected void gridField_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Dropfenlei.SelectedValue = "-1";
        tab.Visible = true;
        imgAdd.Visible = true;
        imgSub.Visible = false;
        txtfenleiName.Text = "";
        txtnum.Text = "";
    }
    protected void imgAdd_Click1(object sender, EventArgs e)
    {
        if (Dropfenlei.SelectedItem.Value == "-1")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请选择！')</script>");
            return;
        }
        if (txtfenleiName.Text == "" || txtnum.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请输入！')</script>");
            return;
        }
        if (info.addInfoClass(Dropfenlei.SelectedItem.Value, txtfenleiName.Text.Trim(), txtnum.Text.Trim()))
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
    protected void imgSub_Click1(object sender, EventArgs e)
    {
        if (Dropfenlei.SelectedItem.Value == "-1")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请选择！')</script>");
            return;
        }
        if (txtfenleiName.Text == "" || txtnum.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请输入！')</script>");
            return;
        }
        if (info.updateInfoClass(hid.Value, Dropfenlei.SelectedItem.Value, txtfenleiName.Text.Trim(), txtnum.Text.Trim()))
        {
            //ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加成功！')</script>");
            tab.Visible = false;
            GridBind();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('失败！')</script>");
            return;
        }
    }
    protected void imgback_Click1(object sender, EventArgs e)
    {
        tab.Visible = false;
    }
    protected void gridField_DataBound(object sender, EventArgs e)
    {

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