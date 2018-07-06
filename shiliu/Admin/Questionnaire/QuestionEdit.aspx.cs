using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Questionnaire_QuestionEdit : System.Web.UI.Page
{
    private string _nID
    {
        get
        {
            return ViewState["_nID"].ToString();
        }
        set
        {
            ViewState["_nID"] = value;
        }
    }

    private string _btnID
    {
        get
        {
            return ViewState["_btnID"].ToString();
        }
        set
        {
            ViewState["_btnID"] = value;
        }
    }
    private int _state
    {
        get
        {
            return Convert.ToInt32(ViewState["_state"]);
        }
        set
        {
            ViewState["_state"] = value;
        }
    }
    //private int _state = 0;//选择
    QuestionHelper info = new QuestionHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            if (Request.QueryString["nID"] != null)
            {
                _nID = Request.QueryString["nID"].ToString();
            }
            else
            {

                //跳转到错误页面
                // _nID = "";
            }
            tab.Visible = false;
            //Dropfenlei.Items.Add(new ListItem("请选择", "-1"));
            //BindDrop(Dropfenlei, "ML_InfoClassMain");
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
        gridField.DataSource = info.SelQuInfoClaess(_nID);
        gridField.DataBind();
    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            if (info.QusetionInfoDelete(e.CommandArgument.ToString()))
            {
                GridBind();
            }
        }
        if (e.CommandName == "update")
        {
            tab.Visible = true;
            GridViewRow drv = ((GridViewRow)(((Button)(e.CommandSource)).Parent.Parent)); //此得出的值是表示那行被选中的索引值
            //此获取的值为GridView中绑定数据库中的主键值,取值方法是选中的行中的第一列的值,drv.RowIndex取得是选中行的索引
            //int id = Convert.ToInt32(gridField.Rows[drv.RowIndex].Cells[0].Text);
            Label lbread = (Label)gridField.Rows[drv.RowIndex].FindControl("oHide");
            if (lbread.Text == "问答题")
            {
                _btnID = "";
                trid.Visible = false;
            }
            else
            {
                _btnID = "btnAdd";
                trid.Visible = true;
            }
            DataTable dt = info.SelQuInfo(e.CommandArgument.ToString());
            if (dt.Rows.Count > 0)
            {
                txtfenleiName.Text = dt.Rows[0]["tTitlle"].ToString();
                qu1.Text = dt.Rows[0]["Q1"].ToString();
                qu2.Text = dt.Rows[0]["Q2"].ToString();
                qu3.Text = dt.Rows[0]["Q3"].ToString();
                qu4.Text = dt.Rows[0]["Q4"].ToString();
                txtPaixu.Text = dt.Rows[0]["nPaixu"].ToString();
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
    /// <summary>
    /// 添加跳转
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        _btnID = (sender as Button).ID;
        if (_btnID != "btnAdd")
        {
            _state = 1;
            if (!info.IsWenDa(_nID))
            {
                trid.Visible = false;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('问答题已经达到最大个数3！')</script>");

                return;
            }
        }
        else//选择题 
        {
            _state = 0;
            if (!info.IsXuanZe(_nID))
            {
                trid.Visible = true;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('选择题已经达到最大个数10！')</script>");

                return;
            }
        }
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
        if (_btnID != "btnAdd")//问答题
        {
            if (txtfenleiName.Text == "" || txtPaixu.Text == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('不能为空！')</script>");
                return;
            }
        }
        else
        {

            if (txtfenleiName.Text == "" || txtPaixu.Text == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('不能为空！')</script>");
                return;
            }
            if (qu1.Text == "" || qu2.Text == "" || qu3.Text == "" || qu4.Text == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('选项不能为空！')</script>");
                return;
            }
        }
        if (info.QusetionInfoInsert(txtfenleiName.Text.Trim(), qu1.Text.Trim(), qu2.Text.Trim(), qu3.Text.Trim(), qu4.Text.Trim(), _state, Convert.ToInt32(txtPaixu.Text.Trim()), System.DateTime.Now.ToString(), _nID))
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
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgSub_Click1(object sender, EventArgs e)
    {
        if (_btnID != "btnAdd")//问答题
        {
            if (txtfenleiName.Text == "" || txtPaixu.Text == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('不能为空！')</script>");
                return;
            }
        }
        else
        {

            if (txtfenleiName.Text == "" || txtPaixu.Text == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('不能为空！')</script>");
                return;
            }
            if (qu1.Text == "" || qu2.Text == "" || qu3.Text == "" || qu4.Text == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('选项不能为空！')</script>");
                return;
            }
        }
        if (info.QusetionInfoUpdate(txtfenleiName.Text.Trim(), qu1.Text.Trim(), qu2.Text.Trim(), qu3.Text.Trim(), qu4.Text.Trim(), Convert.ToInt32(txtPaixu.Text.Trim()), hid.Value))
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
            Label lbread = (Label)gridField.Rows[i].FindControl("oHide");
            if (lbread.Text == "True")
            {
                lbread.Text = "问答题";
            }
            else
            {
                lbread.Text = "选择题";
            }

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
}