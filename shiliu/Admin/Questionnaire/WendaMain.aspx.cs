using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Questionnaire_WendaMain : System.Web.UI.Page
{
    public string _nID
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


    public string _sid1
    {
        get
        {
            return ViewState["_sid1"].ToString();
        }
        set
        {
            ViewState["_sid1"] = value;
        }
    }
    public string _QuestionName
    {
        get
        {
            return ViewState["_QuestionName"].ToString();
        }
        set
        {
            ViewState["_QuestionName"] = value;
        }
    }

    //private int _state = 0;//选择
    QuestionHelper info = new QuestionHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            Div1.Visible = false;
            if (Request.QueryString["nID"] != null)
            {
                _sid1 = "0";
                _QuestionName = "";
                _nID = Request.QueryString["nID"].ToString();
            }
            else
            {

                //跳转到错误页面
                // _nID = "";
            }
            //Dropfenlei.Items.Add(new ListItem("请选择", "-1"));
            //BindDrop(Dropfenlei, "ML_InfoClassMain");

            GridBind();

        }
        Pagination2.MDataTable = info.SelQuWenDaResult(_nID, _sid1);
        Pagination2.MGridView = GridView1;
        Pagination2.Refresh();
    }

    public void GridBind()
    {
        gridField.DataSource = info.SelQuWenDa(_nID);
        gridField.DataBind();
    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "WenDa")
        {
            Div1.Visible = true;
            _sid1 = e.CommandArgument.ToString();
            _QuestionName = info.SelQuestionName(e.CommandArgument.ToString());
            //GridView1.DataSource = info.SelQuWenDaResult(_nID, e.CommandArgument.ToString());
            //GridView1.DataBind();
            Pagination2.MDataTable = info.SelQuWenDaResult(_nID, e.CommandArgument.ToString());
            Pagination2.MGridView = GridView1;

            Pagination2.Refresh();
        }

    }

    protected void gridField_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }


    protected void gridField_DataBound(object sender, EventArgs e)
    {
        //for (int i = 0; i < gridField.Rows.Count; i++)
        //{
        //    Label lbread = (Label)gridField.Rows[i].FindControl("oHide");
        //    if (lbread.Text == "True")
        //    {
        //        lbread.Text = "问答题";
        //    }
        //    else
        //    {
        //        lbread.Text = "选择题";
        //    }

        //}

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