using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Members_MyBranchMain : System.Web.UI.Page
{
    SqlHelper her = new SqlHelper();
    MemberHelper Menber = new MemberHelper();
    public string OneLeaveUser = "0";
    public string TwoLeaveUser = "0";
    public string ThreeLeaveUser = "0";


    private string uID
    {
        get
        {
            return ViewState["uID"].ToString();
        }
        set
        {
            ViewState["uID"] = value;
        }
    }

    private int donow
    {
        get
        {
            return ViewState["donow"] == null ? 1 : Convert.ToInt32(ViewState["donow"]);
        }
        set
        {
            ViewState["donow"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        //imgdelete.Attributes.Add("onclick", "return confirm('请谨慎操作，你确认删除本记录?执行本操作将是不可逆的!')");
        if (!IsPostBack)
        {
            //btnBack.Visible = false;

            //BindDrop(drpUserLevel);
            //drpUserLevel.Items.Insert(0, new ListItem("请选择", "-1", true));
            if (Request.QueryString["ceid"] != null && Request.QueryString["ceid"].ToString() != "")
            {
                hid.Value = Request.QueryString["ceid"].ToString();
            }
            if (Request.QueryString["uid"] != null && Request.QueryString["uid"].ToString() != "")
            {
                uID = Request.QueryString["uid"].ToString();
                GridBind(getUser1(uID));
                getUser2(uID);
                getUser3(uID);
            }

        }
    }
    //绑定下拉菜单
    public void BindDrop(DropDownList drop)
    {
        string sql = "select nID,levelName from ML_MemberLevel";
        DataTable dt = her.ExecuteDataTable(sql);
        drop.DataSource = dt;
        drop.DataValueField = "nID";
        drop.DataTextField = "levelName";
        drop.DataBind();
    }


    public void GridBind(DataTable dt)
    {
        Pagination1.MDataTable = dt;
        Pagination1.MGridView = gridField;
    }
    protected void imgdelete_Click(object sender, EventArgs e)
    {

    }
    private DataTable getUser1(string uid)
    {
        string sql = @"select a.*,b.levelName from ML_Member a join ML_MemberLevel b 
                    on a.fxslevel=b.nID where a.FatherFXSID=" + uid + " and a.fxslevel>1";
        DataTable dt = her.ExecuteDataTable(sql);
        OneLeaveUser = dt.Rows.Count.ToString();
        return dt;
    }
    private DataTable getUser2(string uid)
    {
        string sql = string.Format(@"  select a.*,b.levelName from ML_Member a 
                            join ML_MemberLevel b  on a.fxslevel=b.nID  join 
                          (select nID from ML_Member where FatherFXSID={0}) c
                          on a.FatherFXSID=c.nID where a.fxslevel>1", uid);
        DataTable dt = her.ExecuteDataTable(sql);
        TwoLeaveUser = dt.Rows.Count.ToString();
        return dt;
    }


    private DataTable getUser3(string uid)
    {
        string sql = string.Format(@"    select d.*,e.levelName from  ML_Member d
                                 join ML_MemberLevel e  on d.fxslevel=e.nID join 
                              ( select a.nID from ML_Member a join 
                              (select nID from ML_Member where FatherFXSID={0}) b
                              on a.FatherFXSID=b.nID )c on d.FatherFXSID=c.nID where d.fxslevel>1", uid);
        DataTable dt = her.ExecuteDataTable(sql);
        ThreeLeaveUser = dt.Rows.Count.ToString();
        return dt;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Member_Main.aspx?ceid=" + hid.Value);
    }
    protected void ImgSrs_Click1(object sender, EventArgs e)
    {
        GridBind(getUser1(uID));
        getUser2(uID);
        getUser3(uID);
        Pagination1.Refresh();
    }
    protected void ImgSrs_Click2(object sender, EventArgs e)
    {
        getUser1(uID);
        getUser3(uID);
        GridBind(getUser2(uID));
        Pagination1.Refresh();
    }
    protected void ImgSrs_Click3(object sender, EventArgs e)
    {
        getUser1(uID);
        getUser2(uID);
        GridBind(getUser3(uID));
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
    }

    protected void gridField_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            Label lbSex = (Label)gridField.Rows[i].FindControl("lbSex");
            if (lbSex.Text == "1")
            {
                lbSex.Text = "男";
            }
            else if (lbSex.Text == "2")
            {
                lbSex.Text = "女";
            }
            else
            {
                lbSex.Text = "未确定";
            }

            Label lbAllMoney = (Label)gridField.Rows[i].FindControl("lbAllMoney");

            lbAllMoney.Text = StringDelHTML.PriceToStringLow(Convert.ToInt32(lbAllMoney.Text));


        }
    }
}