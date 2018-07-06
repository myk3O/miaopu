using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Members_MemberKeCheng : System.Web.UI.Page
{
    private string _ID
    {
        get
        {
            return (ViewState["_ID"]).ToString();
        }
        set
        {
            ViewState["_ID"] = value;
        }
    }
    ServceHelper servce = new ServceHelper();
    InfoHelper info = new InfoHelper();
    SqlHelper her = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        imgdelete.Attributes.Add("onclick", "return confirm('请谨慎操作，你确认删除本记录?执行本操作将是不可逆的!')");
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            //DropGroup.Items.Add(new ListItem("请选择", "-1"));
            //DropGroup.Items.Add(new ListItem("请选择", "-1"));
            //DropDownList1.Items.Add(new ListItem("请选择", "-1"));
            //DropDownList2.Items.Add(new ListItem("请选择", "-1"));
            //BindDrop(DropGroup, "ML_ServiceMainClass");
            //BindDrop(DropDownList1, "ML_ServiceAreaClass");
            //BindDrop(DropDownList2, "ML_ServiceAreaClass2");

            if (Request.QueryString["ceid"] != "" && Request.QueryString["ceid"] != null)
            {
                hid.Value = Request.QueryString["ceid"].ToString();
                string aa = Request.QueryString["ceid"].ToString();
            }
            if (Request.QueryString["id"] != "" && Request.QueryString["id"] != null)
            {
                _ID = Request.QueryString["id"].ToString();
            }
            else
            {
                Response.Redirect("../../Error.aspx");
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
        string sql = @"select c.nID, b.tTitle ,a.tTitle+'('+a.tPic+' '+a.tMemoPicList+')' tName,a.tWriter,a.tFromWhere,a.nHit,
                       convert(varchar(100),a.dtEditTime,23)+'日至'+convert(varchar(100),a.dtAddTime,23)+'日' as tTime ,a.tMemo
                        from  ML_Policies  a
                        join ML_ServiceArea b on a.cid0=b.nID  join ML_PoliciesClass c
                        on a.nID=c.sid0 where c.sid1=" + _ID;

        if (txtBeginT.Value.Trim() != "") { sql += " and a.dtEditTime > '" + txtBeginT.Value.Trim() + "'"; }

        if (txtEndT.Value.Trim() != "") { sql += " and a.dtAddTime < '" + txtEndT.Value.Trim() + "'"; }

        if (txtTeacher.Value.Trim() != "") { sql += " and a.tFromWhere like '%" + txtTeacher.Value.Trim() + "%'"; }

        if (txtJS.Value.Trim() != "") { sql += " and a.nHit like '%" + txtJS.Value.Trim() + "%'"; }

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
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            //Label tMemo = gridField.Rows[i].FindControl("lbbz") as Label;
            //tMemo.Text = StringDelHTML.Centers(StringDelHTML.DelHTML(tMemo.Text), 50);
            //Label lblzhiding = gridField.Rows[i].FindControl("lbbz") as Label;

        }
    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "del")
        {
            string sql = "delete from ML_PoliciesClass where nID=" + e.CommandArgument.ToString();
            bool success = her.ExecuteNonQuery(sql);
            if (success)
            {
                GridBind();
                Pagination2.Refresh();
            }
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
                bool success = servce.ServiceDelete(gridField.DataKeys[i].Value.ToString());
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
        Response.Redirect("Member_Main.aspx?ceid=" + gridField.PageIndex);
    }


    protected void btn_Expot_Click(object sender, EventArgs e)
    {
        GridViewExpot.DataSource = GetSource();
        GridViewExpot.DataBind();
        string sql = "select tRealName from dbo.ML_Member where nID=" + _ID;
        string _Name = her.ExecuteScalar(sql).ToString();
        PageHelper.DataTableToExcel(this.GridViewExpot, _Name + "_报名课程.xls");
    }


    /// <summary>
    /// 重写了这个，什么都不用干，就能解决导出时 类型“GridView”的控件 必须放在具有 runat。。。的错误
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}