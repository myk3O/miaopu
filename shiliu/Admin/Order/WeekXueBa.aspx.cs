using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maliang;
using System.Globalization;
using System.Text;

public partial class Admin_Order_WeekXueBa : System.Web.UI.Page
{
    public string class1 = "";
    public string weekYear = "";
    public string year = "";
    Config cf = new Config();
    tongji tj = new tongji();
    private string allMypri = "0";//累计佣金
    StylistHelp sh = new StylistHelp();
    SqlHelper her = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
            //InitPage();
            DateTime dtnow = System.DateTime.Now;
            class1 = getMonthRadnking(dtnow);
            weekYear = GetWeekOfYear(dtnow).ToString();
            year = dtnow.Year.ToString();
        }


    }

    /// <summary>
    /// 获取指定日期，在为一年中为第几周
    /// </summary>
    /// <param name="dt">指定时间</param>
    /// <reutrn>返回第几周</reutrn>
    private int GetWeekOfYear(DateTime dt)
    {
        GregorianCalendar gc = new GregorianCalendar();
        int weekOfYear = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

        return weekOfYear;
    }


    protected void btnSearh_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(tBegin.Value.Trim()))
        {
            DateTime dtnow = Convert.ToDateTime(tBegin.Value.Trim());
            class1 = getMonthRadnking(dtnow);
            weekYear = GetWeekOfYear(dtnow).ToString();
            year = dtnow.Year.ToString();
        }


    }

    public string getMonthRadnking(DateTime dtime)
    {
        string sql = @"select a.nID,a.nickname,a.headimgurl,b.levelName,a.fxslevel from ML_Member a join ML_MemberLevel b 
                    on a.fxslevel=b.nID where a.fxslevel>=3";  //至少得班长级别

        DataTable dt = her.ExecuteDataTable(sql);
        StringBuilder sb = new StringBuilder();
        Dictionary<string, UserInfo> dicPri = new Dictionary<string, UserInfo>();
        foreach (DataRow dr in dt.Rows)
        {
            var id = dr["nID"].ToString();
            // leave1Pri = sh.getOneLeavePriceOneMonth(id);
            //leave2Pri = sh.getTwoLeavePrice(id);
            //leave3Pri = sh.getThreeLeavePrice(id);
            double price1 = (double.Parse(sh.getOneLeavePriceOneWeek(id, dtime)));
            // double price2 = leave2 * (double.Parse(leave2Pri));
            //double price3 = leave3 * (double.Parse(leave3Pri));
            allMypri = StringDelHTML.DoublePriceToString(price1);// 当月 1级分销销售累计佣金
            UserInfo ui = new UserInfo()
            {
                nID = dr["nID"].ToString(),
                nickname = StringDelHTML.Centers(dr["nickname"].ToString(), 10),
                levelname = dr["levelName"].ToString(),
                price = price1,
                pic = dr["headimgurl"].ToString(),
                fxslevel = Convert.ToInt32(dr["fxslevel"])

            };
            dicPri.Add(id, ui);
        }
        dicPri = dicPri.OrderByDescending(d => d.Value.price).Where(d => d.Value.price > 0).ToDictionary(d => d.Key, d => d.Value);



        int rows = 0;
        foreach (KeyValuePair<string, UserInfo> dic in dicPri)
        {
            rows++;
            while (rows <= 15)//100以内排名
            {
                string pri = StringDelHTML.DoublePriceToString(dic.Value.price);
                string img = dic.Value.pic == "" ? "img/Styl_01.png" : dic.Value.pic;
                sb.AppendLine("<dd><a><img src='" + img + "' />");
                sb.AppendLine("<h1>昵称：" + dic.Value.nickname + "</h1>");
                sb.AppendLine("<h2>" + dic.Value.levelname + "</h2>");
                sb.AppendLine("<h2>学分：￥" + pri + "</h2>");
                if (rows <= cf.xuebaCount)//前30有奖励
                {
                    sb.AppendLine("<span style='color:red'>" + rows + "</span></a></dd>");
                }
                else
                {
                    sb.AppendLine("<span style='color:black'>" + rows + "</span></a></dd>");
                }
                break;
            }
        }
        if (string.IsNullOrEmpty(sb.ToString()))
        {
            sb.Append("<h1 style='text-align: center; margin-top: 20px'>这里什么也没留下 </h1>");
        }
        return sb.ToString();
    }

}