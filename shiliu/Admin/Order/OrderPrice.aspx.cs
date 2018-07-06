using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maliang;

public partial class Admin_Order_OrderPrice : System.Web.UI.Page
{
    private DateTime _statisticsBeginDate;
    private DateTime _statisticsEndDate;
    SqlHelper her = new SqlHelper();
    public string banzhangrenshu = "0";
    public string banzhangfenhong = "0";
    public string banzhangAllmoney = "0";
    public string zhurenrenshu = "0";
    public string zhurenfenhong = "0";
    public string zhurenAllmoney = "0";
    public string xiaozhangrenshu = "0";
    public string xiaozhangfenhong = "0";
    public string xiaozhangAllmoney = "0";

    public string Fenhong;


    public string timeDay;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
            //InitPage();

            tongji(System.DateTime.Now);
            timeDay = System.DateTime.Now.ToShortDateString();
            
        }
        GetSumFenhong();
        GridBind();
    }

    private void tongji(DateTime dt)
    {
        DataTable table3 = GetFenhong(dt, 3);
        if (table3.Rows.Count > 0)
        {
            banzhangrenshu = table3.Rows[0]["UserCount"].ToString();
            banzhangfenhong = StringDelHTML.PriceToStringLow(Convert.ToInt32(table3.Rows[0]["AvgMoney"]));
            banzhangAllmoney = StringDelHTML.PriceToStringLow(Convert.ToInt32(table3.Rows[0]["UserCount"]) * Convert.ToInt32(table3.Rows[0]["AvgMoney"]));
        }
        DataTable table4 = GetFenhong(dt, 4);
        if (table4.Rows.Count > 0)
        {
            zhurenrenshu = table4.Rows[0]["UserCount"].ToString();
            zhurenfenhong = StringDelHTML.PriceToStringLow(Convert.ToInt32(table4.Rows[0]["AvgMoney"]));
            zhurenAllmoney = StringDelHTML.PriceToStringLow(Convert.ToInt32(table4.Rows[0]["UserCount"]) * Convert.ToInt32(table4.Rows[0]["AvgMoney"]));
        }

        DataTable table5 = GetFenhong(dt, 5);
        if (table5.Rows.Count > 0)
        {
            xiaozhangrenshu = table5.Rows[0]["UserCount"].ToString();
            xiaozhangfenhong = StringDelHTML.PriceToStringLow(Convert.ToInt32(table5.Rows[0]["AvgMoney"]));
            xiaozhangAllmoney = StringDelHTML.PriceToStringLow(Convert.ToInt32(table5.Rows[0]["UserCount"]) * Convert.ToInt32(table5.Rows[0]["AvgMoney"]));

        }
    }


    private void GetSumFenhong()
    {
        string sql = "select isnull( sum(AvgMoney),0) from T_AvgMoneyLastDay";
        int allmoney = her.ExecuteScalar(sql) == null ? 0 : Convert.ToInt32(her.ExecuteScalar(sql));
        Fenhong = StringDelHTML.PriceToStringLow(allmoney);
    }

    private void InitPage()
    {
        _statisticsBeginDate = DateTime.Parse(string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-7)));
        _statisticsEndDate = DateTime.Parse(string.Format("{0:yyyy-MM-dd}", DateTime.Now));
    }
    //绑定GridView
    public void GridBind()
    {
        DataTable dt = EverydayAdd(_statisticsBeginDate, DateTime.Now, "T_AvgMoneyLastDay");
        Pagination2.MDataTable = dt;
        Pagination2.MGridView = gridField;
    }

    /// <summary>
    /// 每日分红统计
    /// </summary>
    /// <param name="dd">初始日期</param>
    /// <param name="dti">结束日期</param>
    /// <param name="tab">所要查询的表名如：ML_HomeAunt</param>
    /// <returns></returns>
    public DataTable EverydayAdd(DateTime dd, DateTime dti, string tab)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("dtAddTime");
        dt.Columns.Add("MemberNum");
        string sql = string.Format(@"select CONVERT(varchar(12),DATEADD(day,number,'2016-01-11 12:00:37.000'),23) as dtAddTime,
                                    (select isnull( SUM(AvgMoney),0) AvgMoney  from {0} where CONVERT(varchar(12),CreateTime,23)=CONVERT(varchar(12),DATEADD(day,number,'2016-01-11 12:00:37.000'),23)
                                    group by CONVERT(varchar(12),CreateTime,23) )as MemberNum
                                    from master..spt_values where type = 'P' and '{1}'>= DATEADD(day,number,'2016-01-11 12:00:37.000')  order by dtAddTime desc", tab, dti);
        dt = her.ExecuteDataTable(sql);
        return dt;
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
            Label lbprice = gridField.Rows[i].FindControl("lbMemberNum") as Label;
            if (lbprice.Text != null && lbprice.Text != "")
            {
                lbprice.Text = StringDelHTML.PriceToStringLow(Convert.ToInt32(lbprice.Text));
            }
            else
            {
                lbprice.Text = "0";
            }

        }
    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "detail")
        {
            DateTime dtnow=Convert.ToDateTime(e.CommandArgument.ToString());
            tongji(dtnow);
            //Button btn = (Button)e.CommandSource;
            //btn.Visible = false;
            timeDay = dtnow.ToShortDateString();
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

    private DataTable GetFenhong(DateTime dt, int levle)
    {
        string dateb = dt.ToString("yyyy-MM-dd");
        string datee = dt.AddDays(1).ToString("yyyy-MM-dd");
        string sql = string.Format(@"select * from T_AvgMoneyDay where CreateTime>'{0}' and CreateTime<='{1}' and userlevel={2} ", dateb, datee, levle);
        return her.ExecuteDataTable(sql);
    }
}