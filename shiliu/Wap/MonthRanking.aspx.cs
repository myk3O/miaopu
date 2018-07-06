using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maliang;
using System.Text;
using System.Data;
using System.Configuration;

public partial class Wap_MonthRanking : System.Web.UI.Page
{
    //StylistHelp sh = new StylistHelp();
    //SqlHelper her = new SqlHelper();
    MonthPaiHang mp = new MonthPaiHang();
    //private double leave1 = Convert.ToDouble(ConfigurationManager.AppSettings["leave1"]);
    //private double leave2 = Convert.ToDouble(ConfigurationManager.AppSettings["leave2"]);
    //private double leave3 = Convert.ToDouble(ConfigurationManager.AppSettings["leave3"]);

    //public string leave1Pri = "0";
    //public string leave2Pri = "0";
    //public string leave3Pri = "0";

    SqlHelper her = new SqlHelper();
    public string class1 = "";
    public string class2 = "";
    public string allMypri = "0";//累计佣金


    protected void Page_Load(object sender, EventArgs e)
    {
        getUser1();
        GetHotTVByTeacher();
    }

    private void getUser1()
    {
        if (HttpRuntime.Cache["MonthRanking"] == null)
        {
            HttpRuntime.Cache.Insert("MonthRanking", mp.getMonthRadnking());
            class1 = HttpRuntime.Cache["MonthRanking"].ToString();
            //HttpRuntime.Cache与HttpContext.Current.Cache是同一对象,建议使用HttpRuntime.Cache                
        }
        else
        {
            class1 = HttpRuntime.Cache["MonthRanking"].ToString();
        }
    }

    //private string getStr(DataTable dt)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    Dictionary<string, UserInfo> dicPri = new Dictionary<string, UserInfo>();
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        var id = dr["nID"].ToString();
    //        // leave1Pri = sh.getOneLeavePriceOneMonth(id);
    //        //leave2Pri = sh.getTwoLeavePrice(id);
    //        //leave3Pri = sh.getThreeLeavePrice(id);
    //        double price1 = (double.Parse(sh.getOneLeavePriceOneMonth(id)));
    //        // double price2 = leave2 * (double.Parse(leave2Pri));
    //        //double price3 = leave3 * (double.Parse(leave3Pri));
    //        allMypri = StringDelHTML.DoublePriceToString(price1);// 当月 1级分销销售累计佣金
    //        UserInfo ui = new UserInfo()
    //        {
    //            nID = dr["nID"].ToString(),
    //            nickname = StringDelHTML.Centers(dr["nickname"].ToString(), 10),
    //            levelname = dr["levelName"].ToString(),
    //            price = price1,
    //            pic = dr["headimgurl"].ToString(),
    //            fxslevel = Convert.ToInt32(dr["fxslevel"])

    //        };
    //        dicPri.Add(id, ui);
    //    }
    //    dicPri = dicPri.OrderByDescending(d => d.Value.price).ToDictionary(d => d.Key, d => d.Value);



    //    int rows = 0;
    //    foreach (KeyValuePair<string, UserInfo> dic in dicPri)
    //    {
    //        rows++;
    //        while (rows <= 100)//100以内排名
    //        {
    //            string pri = StringDelHTML.DoublePriceToString(dic.Value.price);
    //            string img = dic.Value.pic == "" ? "img/Styl_01.png" : dic.Value.pic;
    //            sb.AppendLine("<dd><a><img src='" + img + "' />");
    //            sb.AppendLine("<h1>昵称：" + dic.Value.nickname + "</h1>");
    //            sb.AppendLine("<h2>" + dic.Value.levelname + "</h2>");
    //            sb.AppendLine("<h2>学分：￥" + pri + "</h2>");
    //            if (rows <= 30)//前30有奖励
    //            {
    //                sb.AppendLine("<span style='color:red'>" + rows + "</span></a></dd>");
    //            }
    //            else
    //            {
    //                sb.AppendLine("<span style='color:black'>" + rows + "</span></a></dd>");
    //            }
    //            break;
    //        }
    //    }
    //    if (string.IsNullOrEmpty(sb.ToString()))
    //    {
    //        sb.Append("<h1 style='text-align: center; margin-top: 20px'>这里什么也没留下 </h1>");
    //    }
    //    return sb.ToString();
    //}


    private void GetHotTVByTeacher()
    {
        StringBuilder sb = new StringBuilder();
        string sql = @"  select a.nID, a.teacherID, a.vName,b.teacherImg,b.teacherName,c.hot from ML_VideoComment a 
                      join T_Teacher b on a.teacherID=b.nID
                      join ( select OcID, COUNT(*) hot from ML_Order where datediff(Month,CreateTime,getdate())=1 group by OcID )c on a.nID=c.OcID
                      where a.oFree =0  order by c.hot desc ";

        DataTable dt = her.ExecuteDataTable(sql);


        int rows = 0;
        foreach (DataRow dr in dt.Rows)
        {
            //<a href="VideoShow.aspx?pid=5"><b>查看老师更多视频</b></a>
            rows++;
            string img = dr["teacherImg"].ToString() == "" ? "img/Styl_01.png" : dr["teacherImg"].ToString();
            sb.AppendLine("<dd><a href='VideoShow.aspx?pid=" + dr["nID"].ToString() + "'><img src='../upload_Img/TeacherImg/" + img + "' />");
            sb.AppendLine("<h1>老师：" + dr["teacherName"].ToString() + "</h1>");
            sb.AppendLine("<h2>课程：" + dr["vName"].ToString() + "</h2>");
            //sb.AppendLine("<h2>排名：" + dr["hot"].ToString() + "</h2>");
            if (rows == 1 || rows == 2)
            {
                sb.AppendLine("<span><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i></span>");
            }
            if (rows == 3 || rows == 4)
            {
                sb.AppendLine("<span><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star-o'></i></span>");
            }
            if (rows >= 5)
            {
                sb.AppendLine("<span><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star-o'></i><i class='fa fa-star-o'></i></span>");
            }
            sb.AppendLine("</a></dd>");
        }
        class2 = sb.ToString();
    }

}