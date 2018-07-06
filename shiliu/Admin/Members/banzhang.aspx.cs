using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maliang;
using WeiPay;


public partial class Admin_Members_banzhang : System.Web.UI.Page
{
    private int _level
    {
        get
        {
            return Convert.ToInt32(ViewState["_level"]);
        }
        set
        {
            ViewState["_level"] = value;
        }
    }
    public string levelName;
    public string yingyee;
    public string userCount;
    SqlHelper her = new SqlHelper();
    MemberHelper Menber = new MemberHelper();
    tongji tj = new tongji();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        //imgdelete.Attributes.Add("onclick", "return confirm('请谨慎操作，你确认删除本记录?执行本操作将是不可逆的!')");
        if (!IsPostBack)
        {
            btnBack.Visible = false;
            if (Request.QueryString["level"] != "" && Request.QueryString["level"] != null)
            {
                _level = Convert.ToInt32(Request.QueryString["level"]);

                getlevelName(_level);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('获取数据失败！')</script>");
            }

        }
        if (_level != null)
            GridBind(_level);
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
    public void getlevelName(int level)
    {
        string sql = "select levelName from ML_MemberLevel where nID=" + level;
        levelName = her.ExecuteScalar(sql).ToString();
    }
    public DataTable GetSource(int level)
    {
        DataTable dt = new DataTable();
        SqlHelper her = new SqlHelper();
        string sql = string.Empty;
        switch (level)
        {
            case 3: sql = @"select a.*,b.levelName from  ML_Member a left join ML_MemberLevel b on a.fxslevel=b.nID  
                    where datediff(day,a.fxsTimeBegin,getdate()-1)>=0 and  a.fxslevel=3 or 
                    datediff(day,a.fxsTimeEnd,getdate())=0 and a.fxslevel=4 or
                    datediff(day,a.level3Time,getdate())=0 and a.fxslevel=5
                     order by a.fxsTimeBegin desc"; break;
            case 4: sql = @"select a.*,b.levelName from  ML_Member a left join ML_MemberLevel b on a.fxslevel=b.nID  
                    where datediff(day,a.fxsTimeEnd,getdate()-1)>=0 and  a.fxslevel=4 or 
                    datediff(day,a.level3Time,getdate())=0 and a.fxslevel=5
                     order by a.fxsTimeBegin desc"; break;
            case 5: sql = @"  select a.*,b.levelName from  ML_Member a left join ML_MemberLevel b on a.fxslevel=b.nID  
                    where datediff(day,a.level3Time,getdate()-1)>=0 and  a.fxslevel=5 order by a.level3Time desc"; break;
            default: break;
        }
        if (!string.IsNullOrEmpty(sql))
        {
            dt = her.ExecuteDataTable(sql);
            userCount = dt.Rows.Count.ToString();
            yingyee = StringDelHTML.PriceToStringLow(tj.AllMoneyToday());
        }
        return dt;
    }
    public void GridBind(int level)
    {
        Pagination1.MDataTable = GetSource(level);
        Pagination1.MGridView = gridField;
    }
    protected void imgdelete_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            CheckBox ckb = (CheckBox)gridField.Rows[i].FindControl("CheckSel");
            if (ckb.Checked)
            {
                bool success = Menber.MemberDelete(gridField.DataKeys[i].Value.ToString());
                if (!success)
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误！请重试')</script>");
                }
            }
        }
        // GridBind();
        // Pagination1.Refresh();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Member_Main.aspx");
    }
    protected void ImgSrs_Click1(object sender, EventArgs e)
    {
        TemplateMsg tm = new TemplateMsg();
        DateTime dt = System.DateTime.Now;
        string dtDate = dt.ToLongDateString().ToString();
        for (int i = 0; i < 50; i++)
        {
            int avg = 1000 + i;
            DataTable table = tj.GetUserByLevel(6);
            string opendid = table.Rows[0]["openid"].ToString();
            string url = "http://tv.gongxue168.com/wap/Global.aspx?uid=" + table.Rows[0]["nID"].ToString();
            //微信模板，每日分红
            tm.CommissionRemind(opendid, url, StringDelHTML.PriceToStringLow(avg), dtDate);
        }

    }
    protected void ImgSrs_Click(object sender, EventArgs e)
    {
        int money = StringDelHTML.PriceToIntUp(price.Text.Trim());
        //插入全球分红，每日
        DateTime dt = System.DateTime.Now;
        if (dt.Hour > 10)//每天10点以后的数据
        {
            DataTable table = tj.GetUserByLevel(_level);
            if (table.Rows.Count > 0)
            {
                if (tj.InsertFenHongLastDay(table, _level, money, true))
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('分红成功')</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('您已提交过昨日的分红')</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('无可分红对象')</script>");
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('每日10点后才能提交分红')</script>");
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

    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "apply")
        {
            Response.Redirect("MemberMain.aspx?uid=" + e.CommandArgument.ToString() + "&&ceid=" + gridField.PageIndex);
        }

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



            //Label lbisJXS = (Label)gridField.Rows[i].FindControl("lbisJXS");
            //if (lbisJXS.Text.ToLower() == "true")
            //{
            //    lbisJXS.Text = "是";
            //}
            //else
            //{
            //    lbisJXS.Text = "否";
            //}



        }
    }
}