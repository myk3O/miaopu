using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_News_AddNewsMember : System.Web.UI.Page
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
    MemberHelper Menber = new MemberHelper();
    SqlHelper her = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        //imgdelete.Attributes.Add("onclick", "return confirm('请谨慎操作，你确认删除本记录?执行本操作将是不可逆的!')");
        if (!IsPostBack)
        {
            // BindDrop(DropGroup);
            //  DropGroup.Items.Insert(0, new ListItem("所属群组", "-1", true));
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
                //Response.Redirect("Member_Edit.aspx?ceid=" + gridField.PageIndex);
                ClientScript.RegisterStartupScript(GetType(), "", "<script>window.close();</script>");
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
        string sql = "select nID,tClassName from [ML_MemberClass]";
        DataTable dt = her.ExecuteDataTable(sql);
        drop.DataSource = dt;
        drop.DataValueField = "nID";
        drop.DataTextField = "tClassName";
        drop.DataBind();
    }

    public DataTable GetSource()
    {
        SqlHelper her = new SqlHelper();
        string sql = string.Format(@"select * from dbo.ML_Member a 
                                           left join
                                           (
                                             select * from ML_NewsClassMain 
                                             where sid0={0}
                                           ) b
                                           on a.nID=b.sid1
                                           where b.sid1 is null", _ID);
        if (txtClass.Text.Trim() != "")
        {
            sql += " and a.MBusiness like '%" + txtClass.Text.Trim() + "%'";
        }

        else if (DropName.SelectedItem.Value == "1" && txtName.Text.Trim() != "") { sql += " and a.tRealName like '%" + txtName.Text.Trim() + "%'"; }
        else if (txtName.Text.Trim() != "") { sql += " and a.MemberName like  '%" + txtName.Text.Trim() + "%'"; }
        //  if (DropGroup.SelectedItem.Value != "-1") { sql += " and ML_MemberClass.nID='" + DropGroup.SelectedItem.Value + "'"; }
        if (DropState.SelectedItem.Value != "-1") { sql += " and a.oCheck='" + DropState.SelectedItem.Value + "'"; }
        DataTable dt = her.ExecuteDataTable(sql);
        return dt;
    }
    public void GridBind()
    {
        Pagination1.MDataTable = GetSource();
        Pagination1.MGridView = gridField;
    }

    protected void ImgSrs_Click(object sender, EventArgs e)
    {
        GridBind();
        Pagination1.Refresh();
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
            Label lblState = (Label)gridField.Rows[i].FindControl("lblState");
            if (lblState.Text == "True")
            {
                lblState.Text = "已审核";
            }
            else
            {
                lblState.Text = "未审核";
            }
        }
    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }


    protected string ConvertName(bool state)
    {
        if (state.ToString() == bool.TrueString)
        {
            return "已审核";
        }
        else
        {
            return "未审核";
        }
    }



    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Member_Edit.aspx?ceid=" + gridField.PageIndex);
        ClientScript.RegisterStartupScript(GetType(), "", "<script>window.close();</script>");
    }

    /// <summary>
    /// 选中发送
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgdelete_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            CheckBox ckb = (CheckBox)gridField.Rows[i].FindControl("CheckSel");
            if (ckb.Checked)
            {
                bool success = XueYuan_Add(gridField.DataKeys[i].Value.ToString());
                //if (!success)
                //{
                //    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误！请重试')</script>");
                //}
                //else
                //{
                //    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加成功')</script>");
                //}
            }
        }
        GridBind();
        Pagination1.Refresh();
        AjaxAlert.AlertMsgAndNoFlush(this, "发送成功");
        //ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发送成功')</script>");
    }
    /// <summary>
    /// 发送给全部用户
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSendAll_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"select * from dbo.ML_Member a 
                                           left join
                                           (
                                             select * from ML_NewsClassMain 
                                             where sid0={0}
                                           ) b
                                           on a.nID=b.sid1
                                           where b.sid1 is null", _ID);
        DataTable dt = her.ExecuteDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            U_ShowDivPic(this.Page, "正在导入中…");
            foreach (DataRow dr in dt.Rows)
            {
                XueYuan_Add(dr["nID"].ToString());
            }

            GridBind();
            Pagination1.Refresh();

            this.Page.Response.Write("<script language=javascript>;HideWait();</script>");
        }
        else
        {
            AjaxAlert.AlertMsgAndNoFlush(this, "无未发送学员");
            //ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('无未发送学员')</script>");
        }
    }
    //插入
    private bool XueYuan_Add(string UserID)
    {
        string sql = string.Format("insert into ML_NewsClassMain values ({0},{1},0,'','',0,0,'{2}')", _ID, UserID, System.DateTime.Now);
        return her.ExecuteNonQuery(sql);
    }

    /// <summary>
    /// 显示处理等待层-图片
    /// </summary>
    /// <param name="show_str"></param>
    /// <returns></returns>
    public void U_ShowDivPic(System.Web.UI.Page page, string show_str)
    {
        page.Response.Write("<STYLE type=text/css>");
        page.Response.Write("..p9 {FONT-FAMILY: 宋体; FONT-SIZE: 9pt; COLOR: #ffffff; TEXT-DECORATION: none}");
        page.Response.Write("..content01 {COLOR: #444444; FONT-SIZE: 12px; LINE-HEIGHT: 20px; TEXT-DECORATION: none}");
        page.Response.Write("A:hover { color:#666666; TEXT-DECORATION: underline}");
        page.Response.Write("</STYLE>");

        page.Response.Write("<div id='mydiv' style='DISPLAY: inline; Z-INDEX: 102; POSITION: relative;TOP: 80px' ms_positioning='FlowLayout'>");
        page.Response.Write("<br>");
        page.Response.Write("<table ALIGN=CENTER BORDER='0' WIDTH='100%' CELLSPACING='0' CELLPADDING='0' style='z-index: 1111; position: absolute; margin:0 auto;left:50%'>");

        page.Response.Write("<tr><td align='center' class='content01'><IMG SRC='../Upload/wait.gif' ></td></tr>");
        page.Response.Write("<tr><td align='center' class='content01'><b><font color='#ccc'>" + show_str + "</font></b></td></tr>"); // 字符串参数
        page.Response.Write("</table>");
        page.Response.Write("");
        page.Response.Write("</div>");

        page.Response.Write("<script language=javascript>;");
        page.Response.Write("function StartShowWait(){mydiv.style.visibility = 'visible'; }");
        page.Response.Write("function HideWait(){mydiv.style.visibility = 'hidden';}");
        page.Response.Write("StartShowWait();</script>");
        page.Response.Flush();
    }
}