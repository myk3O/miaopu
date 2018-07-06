using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Questionnaire_QusetionClass : System.Web.UI.Page
{
    QuestionHelper info = new QuestionHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            tab.Visible = false;
            //Dropfenlei.Items.Add(new ListItem("请选择", "-1"));
            //BindDrop(Dropfenlei, "ML_InfoClassMain");

        } 
        GridBind();
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
        Pagination2.MDataTable = info.getQu();
        Pagination2.MGridView = gridField;
        //gridField.DataSource = info.getQu();
        //gridField.DataBind();
    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            if (info.QusetionDelete(e.CommandArgument.ToString()))
            {
                GridBind();
                Pagination2.Refresh();
            }
        }
        if (e.CommandName == "update")
        {
            DataTable dt = info.SelQu(e.CommandArgument.ToString());
            if (dt.Rows.Count > 0)
            {
                // Dropfenlei.SelectedValue = dt.Rows[0]["sid0"].ToString();
                txtfenleiName.Text = dt.Rows[0]["tClassName"].ToString();
                // txtnum.Text = dt.Rows[0]["nPaiXu"].ToString();
                hid.Value = dt.Rows[0]["nID"].ToString();
                tab.Visible = true;
                imgAdd.Visible = false;
                imgSub.Visible = true;
            }
        }
        if (e.CommandName == "fabu")
        {
            if (info.CheckFabu())
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('已经有发布的问卷，请先关闭再操作！')</script>");
            }
            else
            {
                if (info.UpdateFabu(e.CommandArgument.ToString()))
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发布成功！')</script>");
                    GridBind();
                    Pagination2.Refresh();
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发布失败！')</script>");
                }
            }
        }
        if (e.CommandName == "quxiao")
        {
            if (info.UpdateFabuCancel(e.CommandArgument.ToString()))
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('取消成功！')</script>");
                GridBind();
                Pagination2.Refresh();
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('取消失败！')</script>");
            }

        }

        if (e.CommandName == "Qu")
        {
            Response.Redirect("QuestionEdit.aspx?nID=" + e.CommandArgument.ToString());
        }
        if (e.CommandName == "Toge")
        {
            Response.Redirect("QuestionMain.aspx?QCid=" + e.CommandArgument.ToString());
        }
        if (e.CommandName == "wenda")
        {
            Response.Redirect("WendaMain.aspx?nID=" + e.CommandArgument.ToString());
        }
    }

    protected void gridField_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    /// <summary>
    /// 添加跳转
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //Dropfenlei.SelectedValue = "-1";
        tab.Visible = true;
        imgAdd.Visible = true;
        imgSub.Visible = false;
        txtfenleiName.Text = "";
        // txtnum.Text = "";
    }
    /// <summary>
    /// 保存添加
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgAdd_Click1(object sender, EventArgs e)
    {
        if (txtfenleiName.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('名称不能为空！')</script>");
            return;
        }
        if (info.QusetionInsert(txtfenleiName.Text.Trim(), System.DateTime.Now.ToString()))
        {
            //ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加成功！')</script>");
            tab.Visible = false;
            GridBind();
            Pagination2.Refresh();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加失败！')</script>");
            return;
        }
    }
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgSub_Click1(object sender, EventArgs e)
    {
        if (txtfenleiName.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('名称不能为空！')</script>");
            return;
        }
        if (info.QusetionUpdate(txtfenleiName.Text.Trim(), hid.Value))
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
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            Label lbread = (Label)gridField.Rows[i].FindControl("lbisfabu");
            if (lbread.Text == "True")
            {
                lbread.Text = "已发布";
            }
            else
            {
                lbread.Text = "未发布";
            }

        }
    }
    protected void gridField_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((Label)(e.Row.FindControl("lbisfabu"))).Text == "True")
            {
                ((Button)(e.Row.FindControl("Button4"))).Visible = false;
            }
            else
            {
                ((Button)(e.Row.FindControl("Button5"))).Visible = false;
            }
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor=\"#F6F9FA\"");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=\"" + e.Row.Style["BACKGROUND-COLOR"] + "\"");
        }
    }
}