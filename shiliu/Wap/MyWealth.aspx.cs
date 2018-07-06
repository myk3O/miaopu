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

public partial class Wap_MyWealth : System.Web.UI.Page
{
    //public string leave1Pri = "0";
    //public string leave2Pri = "0";
    //public string leave3Pri = "0";
    public string allMypri = "0.00";//累计佣金
    public string nid;
    public string allMoney = "0.00";
    public string jingtie = "0.00";
    public string CanGetPri = "0.00";
    public string guanzhu = "0.00";
    public string xuexibi = "0.00";
    //private double leave1 = Convert.ToDouble(ConfigurationManager.AppSettings["leave1"]);
    //private double leave2 = Convert.ToDouble(ConfigurationManager.AppSettings["leave2"]);
    //private double leave3 = Convert.ToDouble(ConfigurationManager.AppSettings["leave3"]);
    tongji tj = new tongji();
    StylistHelp sh = new StylistHelp();
    Config cf = new Config();
    private string nID
    {
        get
        {
            return ViewState["nID"].ToString();
        }
        set
        {
            ViewState["nID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["uid"] != null && Request.QueryString["uid"].ToString() != "")
        {
            double learnpt = cf.learnPart;//学习币比例
            nID = Request.QueryString["uid"].ToString();
            nid = nID;
            int PayMoneyNO = sh.getPayMoneyNO(nID);//申请中
            int FenXiangYJ = tj.GetFenXiangYJ(nID);//分享佣金
            int PayMoney = sh.getPayMoney(nID);//已提现
            int jiangxj = tj.GetSumMoneyByUser(nID);//累计奖学金,分红
            int jintie = tj.GetJx(nID);//绩效，津贴
            //总金=分享佣金+全球分红+绩效+邀请关注(2016/3/30)
            double allmakemoney = FenXiangYJ + jiangxj + jintie + tj.GetAttentionXF(nID);
            //当前剩余资金=总金-已提现-已申请-转学习币
            double allpri = allmakemoney - PayMoney - PayMoneyNO - tj.GetMoneyToXueBi(nID);
            //总学习币=总金的一定比例
            double allxxb = tj.Round((allmakemoney) * learnpt, 0);
            //可用学习币=总学习币-已使用学习币+转入学习币+首次关注赠送(2016/3/30)
            double canxxb = allxxb - sh.getUsedXueXb(nID) + tj.GetXueXibi(nID) + tj.GetAttentionXXB(nID);
            //可提现资金=当前剩余资金-总学习币
            double canpri = allpri - allxxb;

            //学习币
            xuexibi = StringDelHTML.DoublePriceToString(canxxb);
            //累计奖学金,全球分红
            allMoney = StringDelHTML.DoublePriceToString(jiangxj);
            //累计分享学分
            //leave1Pri = sh.getOneLeavePrice(uID);
            //leave2Pri = sh.getTwoLeavePrice(uID);
            //leave3Pri = sh.getThreeLeavePrice(uID);
            //double price1 = leave1 * (double.Parse(leave1Pri));
            //double price2 = leave2 * (double.Parse(leave2Pri));
            //double price3 = leave3 * (double.Parse(leave3Pri));
            //leave1Pri = StringDelHTML.DoublePriceToString(price1);
            //leave2Pri = StringDelHTML.DoublePriceToString(price2);
            //leave3Pri = StringDelHTML.DoublePriceToString(price3);
            allMypri = StringDelHTML.DoublePriceToString(FenXiangYJ);
            //累计津贴
            jingtie = StringDelHTML.DoublePriceToString(jintie);
            //累计 邀请关注奖励
            guanzhu = StringDelHTML.DoublePriceToString(tj.GetAttentionXF(nID)); 
            //当前可提现资金=总金-已提现-已申请+全球分红+绩效-学习币

            CanGetPri = StringDelHTML.DoublePriceToString(canpri);

        }
        else
        {
            Response.Redirect("errors.html");
        }
    }
}