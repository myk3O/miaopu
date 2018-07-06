using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maliang;
using System.Data;
using System.Text;
/// <summary>
/// Summary description for MonthPaiHang
/// </summary>
public class MonthPaiHang
{
    private string allMypri = "0";//累计佣金
    StylistHelp sh = new StylistHelp();
    SqlHelper her = new SqlHelper();
    Config cf = new Config();
    tongji tj = new tongji();
    public MonthPaiHang()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string getMonthRadnking()
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
            double price1 = (double.Parse(sh.getOneLeavePriceOneWeek(id)));
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
        dicPri = dicPri.OrderByDescending(d => d.Value.price).Where(d => d.Value.price > 180000).ToDictionary(d => d.Key, d => d.Value);



        int rows = 0;
        foreach (KeyValuePair<string, UserInfo> dic in dicPri)
        {
            rows++;
            while (rows <= 30)//100以内排名
            {
                string pri = StringDelHTML.DoublePriceToString(dic.Value.price);
                string img = dic.Value.pic == "" ? "img/Styl_01.png" : dic.Value.pic;
                sb.AppendLine("<dd><a><img src='" + img + "' />");
                sb.AppendLine("<h1>昵称：" + dic.Value.nickname + "</h1>");
                sb.AppendLine("<h2>" + dic.Value.levelname + "</h2>");
                //sb.AppendLine("<h2>学分：￥" + pri + "</h2>");
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

    public Dictionary<string, UserInfo> getRadnking()
    {
        string sql = @"select a.nID,a.nickname,a.headimgurl,b.levelName,a.fxslevel from ML_Member a join ML_MemberLevel b 
                    on a.fxslevel=b.nID  where isJXS=1 ";
        DataTable dt = her.ExecuteDataTable(sql);
        Dictionary<string, UserInfo> dicPri = new Dictionary<string, UserInfo>();
        foreach (DataRow dr in dt.Rows)
        {
            var id = dr["nID"].ToString();
            int FenXiangYJ = tj.GetFenXiangYJ(id);//分享佣金
            int jiangxj = tj.GetSumMoneyByUser(id);//累计奖学金,分红
            int jintie = tj.GetJx(id);//绩效，津贴
            //总金=分享佣金+全球分红+绩效
            double allmakemoney = FenXiangYJ + jiangxj + jintie;
            allMypri = StringDelHTML.DoublePriceToString(allmakemoney);//累计佣金
            UserInfo ui = new UserInfo()
            {
                nID = dr["nID"].ToString(),
                nickname = StringDelHTML.Centers(dr["nickname"].ToString(), 8),
                levelname = dr["levelName"].ToString(),
                price = allmakemoney,
                pic = dr["headimgurl"].ToString(),
                fxslevel = Convert.ToInt32(dr["fxslevel"])

            };
            dicPri.Add(id, ui);
        }
        dicPri = dicPri.OrderByDescending(d => d.Value.price).ToDictionary(d => d.Key, d => d.Value);

        return dicPri;
        //int rows = 0;
        //foreach (KeyValuePair<string, UserInfo> dic in dicPri)
        //{
        //    rows++;
        //    while (rows <= 100)//100以内排名
        //    {
        //        string pri = StringDelHTML.DoublePriceToString(dic.Value.price);
        //        string img = dic.Value.pic == "" ? "img/Styl_01.png" : dic.Value.pic;
        //        sb.AppendLine("<dd><a><img src='" + img + "' />");
        //        sb.AppendLine("<h1>昵称：" + dic.Value.nickname + "</h1>");
        //        sb.AppendLine("<h2>" + dic.Value.levelname + "</h2>");
        //        sb.AppendLine("<h2>学分：￥" + pri + "</h2>");
        //        // sb.AppendLine("<span>" + rows + "</span></a></dd>");
        //        if (!string.IsNullOrEmpty(uID))
        //        {
        //            if (uID.Equals(dic.Key))
        //            {
        //                sb.AppendLine("<span style='color:red;'>我</span></a></dd>");
        //                MyTop = rows.ToString();
        //            }
        //            else
        //            {
        //                sb.AppendLine("<span>" + rows + "</span></a></dd>");
        //            }
        //        }
        //        else
        //        {
        //            sb.AppendLine("<span>" + rows + "</span></a></dd>");
        //        }
        //        break;
        //    }
        //}
        //if (string.IsNullOrEmpty(sb.ToString()))
        //{
        //    sb.Append("<h1 style='text-align: center; margin-top: 20px'>这里什么也没留下 </h1>");
        //}
        //return sb.ToString();

    }
    //public class UserInfo
    //{
    //    public string nID;
    //    public string nickname;
    //    public string levelname;
    //    public double price;
    //    public string pic;
    //    public int fxslevel;
    //}
}