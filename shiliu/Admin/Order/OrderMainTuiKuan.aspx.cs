
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_OrderMainTuiKuan : System.Web.UI.Page
{
    private string uID
    {
        get
        {
            return ViewState["uID"] == null ? "" : ViewState["uID"].ToString();
        }
        set
        {
            ViewState["uID"] = value;
        }
    }
    private string dlID
    {
        get
        {
            return ViewState["dlID"] == null ? "" : ViewState["dlID"].ToString();
        }
        set
        {
            ViewState["dlID"] = value;
        }
    }
    DaiLi makbll = new DaiLi();
    Order or = new Order();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["anid"] == null) { Response.Redirect("../../Error.aspx"); }
        if (Session["AdminName"] == null) { Response.Redirect("../Error.aspx"); }

        if (!IsPostBack)
        {
            if (Request.QueryString["uid"] != null && Request.QueryString["uid"].ToString() != "")
            {
                uID = Request.QueryString["uid"].ToString();
            }

            if (Request.QueryString["dlid"] != null && Request.QueryString["dlid"].ToString() != "")
            {
                dlID = Request.QueryString["dlid"].ToString();
            }
            else
            {
                dlID = "0";
            }

           // BindDropHomeMak(dropFatherDaili, "ML_HomeMaking", " ");
           // dropFatherDaili.Items.Insert(0, new ListItem("请选择", "-1"));
            //BindDrop(DropState, "ML_OrderState where nID<>3 and nID<>7");
          //  DropState.Items.Insert(0, new ListItem("请选择", "-1"));
            //dropFatherDaili.SelectedValue = Session["anid"].ToString();
            if (!string.IsNullOrEmpty(uID))//从用户过来
            {
                btnReturn.Visible = true;
                //dropFatherDaili.SelectedIndex = dropFatherDaili.Items.IndexOf(dropFatherDaili.Items.FindByValue(Session["anid"].ToString()));
            }
            else
            {
                //dropFatherDaili.SelectedIndex = dropFatherDaili.Items.IndexOf(dropFatherDaili.Items.FindByValue(dlID));
            }


            if (Request.QueryString["dlid"] != null && Request.QueryString["dlid"].ToString() != "")
            {
                btnReturnFromDL.Visible = true;
            }

        }
        GridBind();
    }
    //绑定下拉框
    public void BindDrop(DropDownList drop, string names)
    {
        drop.DataSource = or.GetOrderState(names);
        drop.DataValueField = "nID";
        drop.DataTextField = "StateName";
        drop.DataBind();
    }
    //绑定GridView
    public void GridBind()
    {
        string where = " where 1=1 and a.DelState=0 ";

        if (!string.IsNullOrEmpty(uID))
        {
            where += "and a.OrderUserid=" + uID + " ";
        }
        //if (dropFatherDaili.SelectedItem.Value != "-1")
        //{
        //    where += "and a.Auntid=" + dropFatherDaili.SelectedValue + " ";
        //}

        //where += "and a.Auntid=" + 0 + " ";
        if (!string.IsNullOrEmpty(txtDaili.Value.Trim()))
        {
            where += " and f.name like'%" + txtDaili.Value.Trim() + "%'";
        }
        if (!string.IsNullOrEmpty(username.Value.Trim()))
        {
            where += " and a.Lianxiren like'%" + username.Value.Trim() + "%'";
        }
        if (!string.IsNullOrEmpty(code.Value.Trim()))
        {
            where += " and a.OrderCode like'%" + code.Value.Trim() + "%'";
        }

        //if (Convert.ToInt32(DropState.SelectedItem.Value) > 0)
        //{
        //    where += " and a.OrderState =" + DropState.SelectedItem.Value + " ";
        //}
        where += " and a.OrderState =7 ";
        if (!string.IsNullOrEmpty(tBegin.Value.Trim()))
        {
            where += " and a.CreateTime >='" + tBegin.Value.Trim() + "'";
        }

        if (!string.IsNullOrEmpty(tEnd.Value.Trim()))
        {
            where += " and a.CreateTime <='" + tEnd.Value.Trim() + "'";
        }
        where += " order by a.CreateTime desc";
        DataTable dt = or.GetOrder(where);
        Pagination2.MDataTable = dt;
        Pagination2.MGridView = gridField;
    }
    //绑定下拉框
    public void BindDropHomeMak(DropDownList drop, string names, string where)
    {
        // IAdminBLL adminbll = BLLFactory.CreateAdminBLL();
        // DataTable dt = adminbll.AdminBind(names, where);
        drop.DataSource = makbll.HomeMakBind(names, where);
        drop.DataValueField = "nID";
        drop.DataTextField = "tRealName";
        drop.DataBind();
    }
    protected void btnSearh_Click(object sender, EventArgs e)
    {
        GridBind();
        Pagination2.Refresh();
    }
    protected void gridField_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            Label lbhomeMak = gridField.Rows[i].FindControl("lbhomeMak") as Label;
            if (string.IsNullOrEmpty(lbhomeMak.Text))
            {
                lbhomeMak.Text = "佳奥";
            }

            Label lbprice = gridField.Rows[i].FindControl("lbprice") as Label;
            lbprice.Text = StringDelHTML.PriceToStringLow(Convert.ToInt32(lbprice.Text));
        }
    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "detail")
        {
            Response.Redirect("OrderDetail.aspx?id=" + e.CommandArgument.ToString());
        }



        if (e.CommandName == "del")
        {
            //Response.Redirect("OrderDetail.aspx?id=" + e.CommandArgument.ToString());

            bool success = or.DelOrder(e.CommandArgument.ToString());
            if (success)
            {
                GridBind();
                Pagination2.Refresh();
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
    protected void gridField_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Members/Member_Main.aspx?ceid=" + gridField.PageIndex);

    }
    protected void btnReturnFromDL_Click(object sender, EventArgs e)
    {
        Response.Redirect("../DaiLi/HomeMakMain.aspx?ceid=" + gridField.PageIndex);

    }


}