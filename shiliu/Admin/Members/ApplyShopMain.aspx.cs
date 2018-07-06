using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maliang;
using System.Data;
public partial class Admin_Members_ApplyShopMain : System.Web.UI.Page
{
    private string nID
    {
        get
        {
            return ViewState["nID"] == null ? "" : ViewState["nID"].ToString();
        }
        set
        {
            ViewState["nID"] = value;
        }
    }
    SqlHelper her = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            // BindDrop(DropGroup);
            //  DropGroup.Items.Insert(0, new ListItem("所属群组", "-1", true));
            if (Request.QueryString["ceid"] != "" && Request.QueryString["ceid"] != null)
            {
                hid.Value = Request.QueryString["ceid"].ToString();
            }
            if (Request.QueryString["nID"] != "" && Request.QueryString["nID"] != null)
            {
                nID = Request.QueryString["nID"].ToString();
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
        string sql = @"select a.*,b.headimgurl from  DB_ApplyShop a join ML_Member b on a.userID=b.nID where 1=1";
        if (txtName.Text.Trim() != "")
        {
            sql += " and a.phone like '%" + txtName.Text.Trim() + "%'";
        }
        if (!string.IsNullOrEmpty(nID))
        {
            sql += " and a.userID=" + nID;
        }
        sql += " order by a.CreateTime desc";
        DataTable dt = her.ExecuteDataTable(sql);
        return dt;
    }
    public void GridBind()
    {
        Pagination1.MDataTable = GetSource();
        Pagination1.MGridView = gridField;
    }
    protected void imgdelete_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            CheckBox ckb = (CheckBox)gridField.Rows[i].FindControl("CheckSel");
            if (ckb.Checked)
            {
                //bool success = Menber.MemberDelete(gridField.DataKeys[i].Value.ToString());
                //if (!success)
                //{
                //    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误！请重试')</script>");
                //}
            }
        }
        GridBind();
        Pagination1.Refresh();
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

    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        if (e.CommandName == "upfxsF")
        {
            //Response.Redirect("OrderDetail.aspx?id=" + e.CommandArgument.ToString());

            //bool success = Menber.MemberUpdateFxs(e.CommandArgument.ToString(), 0);
            //if (success)
            //{
            //    GridBind();
            //    Pagination1.Refresh();
            //}

        }
    }
    private string GetClassName(string id)
    {
        string sql = "select tClassName from ML_ServiceMainClass where nID=" + id;
        return her.ExecuteScalar(sql) == null ? "" : her.ExecuteScalar(sql).ToString();
    }
    protected void gridField_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            string cnameL = string.Empty;
            Label lbcalssname = (Label)gridField.Rows[i].FindControl("lbcalssname");
            string[] cname = lbcalssname.Text.Split(',');
            for (int j = 0; j < cname.Length - 1; j++)
            {
                cnameL += GetClassName(cname[j]) + "<br/>";
            }

            lbcalssname.Text = cnameL;

        }
    }
}