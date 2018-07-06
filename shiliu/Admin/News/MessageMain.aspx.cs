using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_News_MessageMain : System.Web.UI.Page
{
    private string _userID
    {
        get
        {
            return (ViewState["_userID"]).ToString();
        }
        set
        {
            ViewState["_userID"] = value;
        }
    }
    SqlHelper her = new SqlHelper();
    NewsHelper news = new NewsHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        //imgdelete.Attributes.Add("onclick", "return confirm('请谨慎操作，你确认删除本记录?执行本操作将是不可逆的!')");
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            tab.Visible = false;
            BindDrop(DropDownList1);
            //BindDrop(DropGroup, "ML_NewsClass");
            // DropGroup.Items.Insert(0, new ListItem("所有分类", "-1", true));
            if (Request.QueryString["ceid"] != "" && Request.QueryString["ceid"] != null)
            {
                hid.Value = Request.QueryString["ceid"].ToString();
                string aa = Request.QueryString["ceid"].ToString();
            }
            if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")//查看个人消息
            {
                _userID = Request.QueryString["id"].ToString();

            }
            else
            {
                btnAdd.Visible = false;
                _userID = "";
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
        string sql = @"select a.nID, c.tTitle,a.dtPubTime,b.tRealName,b.MBusiness,b.MemberPhone,b.MemberName,a.oHide,a.sid2 
                        from ML_NewsClassMain a
                        inner join  ML_Member b
                        on a.sid1=b.nID
                        inner join ML_News c 
                        on a.sid0=c.nID where 1=1 ";

        if (_userID != "")//查看个人消息
        {
            sql += " and a.sid1=" + _userID;
        }
        if (txtUserName.Text.Trim() != "")
        {
            sql += " and b.tRealName like '%" + txtUserName.Text.Trim() + "%'";
        }
        if (txtClass.Text.Trim() != "")
        {
            sql += " and b.MBusiness like '%" + txtClass.Text.Trim() + "%'";
        }
        sql += " and a.oHide='" + DropGroup.SelectedItem.Value + "'";//未阅读
        sql += " and a.sid2='" + DropName.SelectedItem.Value + "'";
        sql += " order by a.sid1 desc,a.dtPubTime desc";

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
    public void BindDrop(DropDownList drop)
    {
        SqlHelper her = new SqlHelper();
        string sql = "select nID,tClassName from dbo.ML_NewsClass";
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
            if (((Label)(e.Row.FindControl("lbdo"))).Text == "True")
            {
                ((Button)(e.Row.FindControl("btnDelete"))).Visible = false;
            }
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor=\"#F6F9FA\"");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=\"" + e.Row.Style["BACKGROUND-COLOR"] + "\"");
        }
    }
    protected void gridField_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            Label lbread = (Label)gridField.Rows[i].FindControl("lbread");
            if (lbread.Text == "True")
            {
                lbread.Text = "是";
            }
            else
            {
                lbread.Text = "否";
            }
            Label lbdo = (Label)gridField.Rows[i].FindControl("lbdo");
            if (lbdo.Text == "True")
            {
                lbdo.Text = "是";
            }
            else
            {
                lbdo.Text = "否";
            }
        }

    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            bool success = news.updateDo(e.CommandArgument.ToString());
            if (success)
            {
                AjaxAlert.AlertMsgAndNoFlush(this, "发送成功");
                GridBind();
                Pagination2.Refresh();
            }
            else
            {
                AjaxAlert.AlertMsgAndNoFlush(this, "发送失败");
            }
        }

    }
    protected void btnSearh_Click(object sender, EventArgs e)
    {
        GridBind();
        Pagination2.Refresh();
    }


    //个人添加消息
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        tab.Visible = true;
        btnAdd.Visible = false;
        //Response.Redirect("NewsEdit.aspx?ceid=" + gridField.PageIndex);
    }


    //保存
    protected void imgSub_Click(object sender, EventArgs e)
    {
        if (txtTlitle.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请选择消息标题！')</script>");
            return;
        }
        string strTime = System.DateTime.Now.ToString();
        bool success = false;

        success = news.NewsInsert(DropDownList1.SelectedItem.Value, txtTlitle.Text.Trim(), hid.Value, content1.InnerText, "", RadioState.SelectedItem.Value, strTime, "1");

        if (success)
        {
            if (XueYuan_Add(MaxnID(), _userID))
            {
                GridBind();
                Pagination2.Refresh();
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加成功！')</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加失败！')</script>");
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加失败！')</script>");
        }

    }

    /// <summary>
    /// 插入消息
    /// </summary>
    /// <param name="_nid"></param>
    /// <param name="UserID"></param>
    /// <returns></returns>
    private bool XueYuan_Add(int _nid, string UserID)
    {
        string sql = string.Format("insert into ML_NewsClassMain values ({0},{1},0,'','',0,0,'{2}')", _nid, UserID, System.DateTime.Now);
        return her.ExecuteNonQuery(sql);
    }

    private int MaxnID()
    {
        int nID = 0;
        string sql = "select Max(nID) from ML_News";
        if (her.ExecuteScalar(sql) != null)
        {
            nID = Convert.ToInt32(her.ExecuteScalar(sql));
        }
        return nID;
    }
    //取消
    protected void imgback_Click(object sender, EventArgs e)
    {
        tab.Visible = false;
        btnAdd.Visible = true;
    }
}