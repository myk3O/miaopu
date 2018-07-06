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

public partial class Wap_Ranking : System.Web.UI.Page
{
    //    Config cf = new Config();
    //    tongji tj = new tongji();
    MonthPaiHang mp = new MonthPaiHang();
    //private double leave1 = Convert.ToDouble(ConfigurationManager.AppSettings["leave1"]);
    //private double leave2 = Convert.ToDouble(ConfigurationManager.AppSettings["leave2"]);
    //private double leave3 = Convert.ToDouble(ConfigurationManager.AppSettings["leave3"]);

    //public string leave1Pri = "0";
    //public string leave2Pri = "0";
    //public string leave3Pri = "0";

    public string MyTop = "100+";

    public string class1 = "";

    public string allMypri = "0";//累计佣金

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["uid"] != null && Request.QueryString["uid"].ToString() != "")
        {
            uID = Request.QueryString["uid"].ToString();
        }
        getUser1();
    }

    private void getUser1()
    {
        StringBuilder sb = new StringBuilder();
        Dictionary<string, UserInfo> dicPri = HttpRuntime.Cache["Ranking"] as Dictionary<string, UserInfo>;

        if (HttpRuntime.Cache["Ranking"] == null)
        {
            HttpRuntime.Cache.Insert("Ranking", mp.getRadnking());
            dicPri = HttpRuntime.Cache["Ranking"] as Dictionary<string, UserInfo>;
            //HttpRuntime.Cache与HttpContext.Current.Cache是同一对象,建议使用HttpRuntime.Cache                
        }


        int rows = 0;
        foreach (KeyValuePair<string, UserInfo> dic in dicPri)
        {
            rows++;
            while (rows <= 100)//100以内排名
            {
                string pri = StringDelHTML.DoublePriceToString(dic.Value.price);
                string img = dic.Value.pic == "" ? "img/Styl_01.png" : dic.Value.pic;
                sb.AppendLine("<dd><a><img src='" + img + "' />");
                sb.AppendLine("<h1>昵称：" + dic.Value.nickname + "</h1>");
                sb.AppendLine("<h2>" + dic.Value.levelname + "</h2>");
                sb.AppendLine("<h2>学分：" + pri + "</h2>");
                // sb.AppendLine("<span>" + rows + "</span></a></dd>");
                if (!string.IsNullOrEmpty(uID))
                {
                    if (uID.Equals(dic.Key))
                    {
                        sb.AppendLine("<span style='color:red;'>我</span></a></dd>");
                        MyTop = rows.ToString();
                    }
                    else
                    {
                        sb.AppendLine("<span>" + rows + "</span></a></dd>");
                    }
                }
                else
                {
                    sb.AppendLine("<span>" + rows + "</span></a></dd>");
                }
                break;
            }
        }
        if (string.IsNullOrEmpty(sb.ToString()))
        {
            sb.Append("<h1 style='text-align: center; margin-top: 20px'>这里什么也没留下 </h1>");
        }
        class1 = sb.ToString();
    }

    //private string getStr(DataTable dt)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    Dictionary<string, UserInfo> dicPri = new Dictionary<string, UserInfo>();
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        var id = dr["nID"].ToString();
    //        //leave1Pri = sh.getOneLeavePrice(id);
    //        //leave2Pri = sh.getTwoLeavePrice(id);
    //        //leave3Pri = sh.getThreeLeavePrice(id);
    //        //double price1 = leave1 * (double.Parse(leave1Pri));
    //        //double price2 = leave2 * (double.Parse(leave2Pri));
    //        //double price3 = leave3 * (double.Parse(leave3Pri));
    //        int FenXiangYJ = tj.GetFenXiangYJ(id);//分享佣金
    //        int jiangxj = tj.GetSumMoneyByUser(id);//累计奖学金,分红
    //        int jintie = tj.GetJx(id);//绩效，津贴
    //        //总金=分享佣金+全球分红+绩效
    //        double allmakemoney = FenXiangYJ + jiangxj + jintie;
    //        allMypri = StringDelHTML.DoublePriceToString(allmakemoney);//累计佣金
    //        UserInfo ui = new UserInfo()
    //        {
    //            nID = dr["nID"].ToString(),
    //            nickname = StringDelHTML.Centers(dr["nickname"].ToString(), 8),
    //            levelname = dr["levelName"].ToString(),
    //            price = allmakemoney,
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
    //            // sb.AppendLine("<span>" + rows + "</span></a></dd>");
    //            if (!string.IsNullOrEmpty(uID))
    //            {
    //                if (uID.Equals(dic.Key))
    //                {
    //                    sb.AppendLine("<span style='color:red;'>我</span></a></dd>");
    //                    MyTop = rows.ToString();
    //                }
    //                else
    //                {
    //                    sb.AppendLine("<span>" + rows + "</span></a></dd>");
    //                }
    //            }
    //            else
    //            {
    //                sb.AppendLine("<span>" + rows + "</span></a></dd>");
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

}