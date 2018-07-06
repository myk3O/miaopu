using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ImgConfig_ProductSelect : System.Web.UI.UserControl
{
    ServceHelper servce = new ServceHelper();
    InfoHelper info = new InfoHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        imgdelete.Attributes.Add("onclick", "return confirm('请谨慎操作，你确认删除本记录?执行本操作将是不可逆的!')");
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            DropGroup.Items.Add(new ListItem("请选择", "-1"));
            DropDownList1.Items.Add(new ListItem("请选择", "-1"));
            DropDownList2.Items.Add(new ListItem("请选择", "-1"));
            DropDownList3.Items.Add(new ListItem("请选择", "-1"));
            BindDrop(DropGroup, "ML_ServiceMainClass");
            BindDrop(DropDownList1, "ML_ServiceAreaClass");
            BindDrop(DropDownList2, "ML_ServiceAreaClass2");
            BindDrop(DropDownList3, "ML_ServiceAreaClass1");
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
    public void BindDrop(DropDownList drop, string names)
    {
        SqlHelper her = new SqlHelper();
        string sql = "select nID,tClassName from " + names + " order by npaixu";
        DataTable dt = her.ExecuteDataTable(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            drop.Items.Add(new ListItem(dt.Rows[i]["tClassName"].ToString(), dt.Rows[i]["nID"].ToString()));
        }
    }

    public DataTable GetSource()
    {
        SqlHelper her = new SqlHelper();
        string sql = @"select a.*,b.tClassName name1,c.tClassName name2,d.tClassName name3,e.tClassName name4 from dbo.ML_ServiceArea a 
left join ML_ServiceMainClass b on a.cid0=b.nID
left join ML_ServiceAreaClass c on a.cid1=c.nID
left join ML_ServiceAreaClass2 d  on a.cid2=d.nID
left join ML_ServiceAreaClass1 e  on a.cid3=e.nID where 1=1";
        if (keyName.Value.Trim() != "") { sql += " and (a.tTitle like '%" + keyName.Value.Trim() + "%' or a.dtAddTime like '%" + keyName.Value.Trim() + "%')"; }
        if (DropName.SelectedItem.Value != "-1") { sql += " and a.oHide=" + DropName.SelectedItem.Value + ""; }
        if (DropGroup.SelectedItem.Value != "-1") { sql += " and a.cid0=" + DropGroup.SelectedItem.Value + ""; }
        if (DropDownList1.SelectedItem.Value != "-1") { sql += " and a.cid1=" + DropDownList1.SelectedItem.Value + ""; }
        if (DropDownList2.SelectedItem.Value != "-1") { sql += " and a.cid2=" + DropDownList2.SelectedItem.Value + ""; }
        if (DropDownList3.SelectedItem.Value != "-1") { sql += " and a.cid3=" + DropDownList3.SelectedItem.Value + ""; }
        DataTable dt = her.ExecuteDataTable(sql);
        return dt;
    }

    public void GridBind()
    {

        Pagination2.MDataTable = GetSource();
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
                bool success = servce.ServiceDelete(gridField.DataKeys[i].Value.ToString());
                if (!success)
                {
                    //  ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误！请重试')</script>");
                }
            }
        }
        GridBind();
        Pagination2.Refresh();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //Response.Redirect("ImgEdit.aspx?proID=" + HiddenField1.Value);
        //Session["SelectProID"] = HiddenField1.Value;
        (this.Parent.FindControl("txtUrl") as TextBox).Text = "123";
        //ClientScript.RegisterStartupScript(GetType(), "", "<script>window.close();</script>");
    }



    protected void btn_Expot_Click(object sender, EventArgs e)
    {

    }
}