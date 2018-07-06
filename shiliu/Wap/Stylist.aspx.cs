using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Maliang;
using System.Configuration;

public partial class Wap_Stylist : System.Web.UI.Page
{
    StylistHelp sh = new StylistHelp();
    public string nid;
    public string OneLeaveUser = "0";
    public string TwoLeaveUser = "0";
    public string ThreeLeaveUser = "0";
    public string allUser = "0";
    public string allOrder = "0";
    //private int price = 100;
    private double leave1 = Convert.ToDouble(ConfigurationManager.AppSettings["leave1"]);
    private double leave2 = Convert.ToDouble(ConfigurationManager.AppSettings["leave2"]);
    private double leave3 = Convert.ToDouble(ConfigurationManager.AppSettings["leave3"]);


    public string soldPri = "0";//总卖出
    public string getPri = "0"; //已提现
    public string CanGetPri = "0";//可提现
    public string getingPri = "0";//提现中
    public string allMypri = "0";//累计佣金


    public string leave1Pri = "0";
    public string leave2Pri = "0";
    public string leave3Pri = "0";


    public string leave1OrderCount = "0";
    public string leave2OrderCount = "0";
    public string leave3OrderCount = "0";


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
        if (!IsPostBack)
        {
            if (Request.QueryString["uid"] != null && Request.QueryString["uid"].ToString() != "")
            {
                nID = Request.QueryString["uid"].ToString();
                nid = nID;
                OneLeaveUser = sh.getOneLeaveUser(nID);
                TwoLeaveUser = sh.getTwoLeaveUser(nID);
                ThreeLeaveUser = sh.getThreeLeaveUser(nID);
                leave1Pri = sh.getOneLeavePrice(nID);
                leave2Pri = sh.getTwoLeavePrice(nID);
                leave3Pri = sh.getThreeLeavePrice(nID);
                leave1OrderCount = sh.getOneLeaveOrderCount(nID);
                leave2OrderCount = sh.getTwoLeaveOrderCount(nID);
                leave3OrderCount = sh.getThreeLeaveOrderCount(nID);

                allUser = (int.Parse(OneLeaveUser) + int.Parse(TwoLeaveUser) + int.Parse(ThreeLeaveUser)).ToString();
                allOrder = (int.Parse(leave1OrderCount) + int.Parse(leave2OrderCount) + int.Parse(leave3OrderCount)).ToString();

                soldPri = StringDelHTML.PriceToStringLow(int.Parse(leave1Pri) + int.Parse(leave2Pri) + int.Parse(leave3Pri));
                double price1 = leave1 * (double.Parse(leave1Pri));
                double price2 = leave2 * (double.Parse(leave2Pri));
                double price3 = leave3 * (double.Parse(leave3Pri));
                leave1Pri = StringDelHTML.DoublePriceToString(price1);
                leave2Pri = StringDelHTML.DoublePriceToString(price2);
                leave3Pri = StringDelHTML.DoublePriceToString(price3);

                getingPri = StringDelHTML.PriceToStringLow(sh.getPayMoneyNO(nID));//申请中
                allMypri = StringDelHTML.DoublePriceToString(price3 + price2 + price1);//累计佣金
                getPri = StringDelHTML.PriceToStringLow(sh.getPayMoney(nID));//已提现
                //当前可提现资金=总金-已提现-已申请
                CanGetPri = StringDelHTML.DoublePriceToString((price3 + price2 + price1) - sh.getPayMoney(nID) - sh.getPayMoneyNO(nID));
                hidpri.Value = CanGetPri;
            }
            else
            {
                Response.Redirect("errors.html");
            }
        }
    }

}