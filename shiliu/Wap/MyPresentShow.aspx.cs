using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Wap_MyPresentShow : System.Web.UI.Page
{
    public string CanGetPri = "0";//可提现
    public string koushui = "0";
    public string shuihou = "0";

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
    Config cf = new Config();
    tongji tj = new tongji();
    StylistHelp sh = new StylistHelp();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["uid"] != null && Request.QueryString["uid"].ToString() != "")
        {
            //shuilv.Value = System.Configuration.ConfigurationManager.AppSettings["shuifei"];
            double learnpt = cf.learnPart;//学习币比例
            double shuifei = Convert.ToDouble(cf.shuifei);
            nID = Request.QueryString["uid"].ToString();

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


            double ks = tj.Round(canpri * shuifei, 0);
            CanGetPri = StringDelHTML.DoublePriceToString(canpri);
            koushui = StringDelHTML.DoublePriceToString(ks);
            shuihou = StringDelHTML.DoublePriceToString(tj.Round(canpri - ks, 0));
            hidpri.Value = CanGetPri;
            hidshuihou.Value = shuihou;
            hidkoushui.Value = koushui;
        }
        else
        {
            Response.Redirect("errors.html");
        }
    }
}