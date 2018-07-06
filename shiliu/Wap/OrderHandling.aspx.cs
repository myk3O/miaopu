using Maliang;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using WeiPay;

public partial class Wap_OrderHandling : System.Web.UI.Page
{
    //页面输出 不用操作
    public static string Code = "";     //微信端传来的code
    public static string PrepayId = ""; //预支付ID
    public static string Sign = "";     //为了获取预支付ID的签名
    public static string PaySign = "";  //进行支付需要的签名
    public static string Package = "";  //进行支付需要的包
    public static string TimeStamp = ""; //时间戳 程序生成 无需填写
    public static string NonceStr = ""; //随机字符串  程序生成 无需填写


    public string orderCode;//订单编号
    public string orderTime;//下单时间
    public string orderState;
    public string Product;
    public string orderAddress;
    public string proPrice;//订单总金额

    public string kuaidiName;
    public string kuaidiCode;

    public string huiyuanPrice = "";
    public string xuexibi;
    private string UserOpenId = ""; //微信用户openid；
    private string pID
    {
        get
        {
            return ViewState["pID"].ToString();
        }
        set
        {
            ViewState["pID"] = value;
        }
    }
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
    SqlHelper her = new SqlHelper();
    tongji tj = new tongji();
    StylistHelp sh = new StylistHelp();
    PayHelp ph = new PayHelp();
    private double learnpt = Convert.ToDouble(ConfigurationManager.AppSettings["learnPart"]);//学习币比例
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["pid"] != null && Request.QueryString["pid"].ToString() != "")
            {
                pID = Request.QueryString["pid"].ToString();
                if (Request.QueryString["uid"] != null && Request.QueryString["uid"].ToString() != "")
                {

                    nID = Request.QueryString["uid"].ToString();
                    LogUtil.WriteLog("支付页面，用户=" + nID);
                    //获取用户openId
                    //GetUserOpenId();
                    GetOpenID(nID);
                    //生成欲支付订单
                    GetProductPrice();
                    PayMoney();

                    //下面这些是求 “可使用学习币”
                    int PayMoneyNO = sh.getPayMoneyNO(nID);//申请中
                    int FenXiangYJ = tj.GetFenXiangYJ(nID);//分享佣金
                    int PayMoneys = sh.getPayMoney(nID);//已提现
                    int jiangxj = tj.GetSumMoneyByUser(nID);//累计奖学金,分红
                    int jintie = tj.GetJx(nID);//绩效，津贴
                    //总金=分享佣金+全球分红+绩效+邀请关注(2016/3/30)
                    double allmakemoney = FenXiangYJ + jiangxj + jintie + tj.GetAttentionXF(nID);
                    //当前剩余资金=总金-已提现-已申请
                    // double allpri = allmakemoney - PayMoneys - PayMoneyNO;
                    //总学习币=总金的一定比例
                    double allxxb = tj.Round((allmakemoney) * learnpt, 0);
                    //可用学习币=总学习币-已使用学习币+转入学习币+首次关注赠送(2016/3/30)
                    double canxxb = allxxb - sh.getUsedXueXb(nID) + tj.GetXueXibi(nID) + tj.GetAttentionXXB(nID);
                    //可提现资金=当前剩余资金-总学习币
                    // double canpri = allpri - allxxb;

                    //学习币
                    xuexibi = StringDelHTML.DoublePriceToString(canxxb);
                    Gwb.Value = xuexibi;

                }
                else
                {
                    LogUtil.WriteLog("用户ID丢失");

                    Response.Redirect("errors.html");

                }
            }
            else
            {
                LogUtil.WriteLog("产品ID丢失");

                Response.Redirect("errors.html");

            }

        }
    }
    private void GetOpenID(string uid)
    {
        string sql = "select openid from ML_Member where nID=" + uid;
        string openid = her.ExecuteScalar(sql) == null ? "" : her.ExecuteScalar(sql).ToString();
        this.UserOpenId = openid;

    }
    private void GetProductPrice()
    {
        string sql = "select Price from ML_VideoComment where nID=" + pID;

        string price = her.ExecuteScalar(sql) == null ? "0.01" : her.ExecuteScalar(sql).ToString() == "" ? "0.01" : StringDelHTML.PriceToStringLow(Convert.ToInt32(her.ExecuteScalar(sql)));
        LogUtil.WriteLog("产品价格=" + price);
        //判断是否已经是会员
        if (ph.Memberisfxs(nID))
        {
            //再次购买，产品打1折
            price = (Convert.ToDouble(price) * 0.1).ToString();
            huiyuanPrice = "您已享受会员价1折购买";
        }
        this.proPrice = price;
        orderprice.Value = StringDelHTML.PriceToIntUp(proPrice).ToString();
    }
    /// <summary>
    /// 获取当前用户的微信 OpenId，如果知道用户的OpenId请不要使用该函数
    /// </summary>
    private void GetUserOpenId()
    {

        string code = Request.QueryString["code"];
        if (string.IsNullOrEmpty(code))
        {
            string backUrl = PayConfig.SendUrl + "?uid=" + nID;
            string code_url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state=lk#wechat_redirect", PayConfig.AppId, backUrl);
            Response.Redirect(code_url);
        }
        else
        {
            LogUtil.WriteLog(" ============ 开始 获取微信用户相关信息 =====================");

            #region 获取支付用户 OpenID================
            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", PayConfig.AppId, PayConfig.AppSecret, code);
            string returnStr = HttpUtil.Send("", url);
            LogUtil.WriteLog("Send 页面  returnStr 第一个：" + returnStr);

            var obj = JsonConvert.DeserializeObject<OpenModel>(returnStr);

            url = string.Format("https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={0}&grant_type=refresh_token&refresh_token={1}", PayConfig.AppId, obj.refresh_token);
            returnStr = HttpUtil.Send("", url);
            obj = JsonConvert.DeserializeObject<OpenModel>(returnStr);

            LogUtil.WriteLog("Send 页面  access_token：" + obj.access_token);
            LogUtil.WriteLog("Send 页面  openid=" + obj.openid);

            url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}", obj.access_token, obj.openid);
            returnStr = HttpUtil.Send("", url);
            LogUtil.WriteLog("Send 页面  returnStr：" + returnStr);

            this.UserOpenId = obj.openid;

            LogUtil.WriteLog(" ============ 结束 获取微信用户相关信息 =====================");
            #endregion
        }
    }



    /// <summary>
    /// 生成欲支付订单
    /// </summary>
    private void PayMoney()
    {

        #region 支付操作============================


        #region 基本参数===========================
        //时间戳 
        TimeStamp = TenpayUtil.getTimestamp();
        //随机字符串 
        NonceStr = TenpayUtil.getNoncestr();

        //创建支付应答对象
        var packageReqHandler = new RequestHandler(Context);
        //初始化
        packageReqHandler.init();
        Random ra = new Random();
        DateTime timeNow = System.DateTime.Now;
        orderCode = "GXSP" + nID + timeNow.ToString("yyyyMMddhhmmss") + ra.Next(0, 1000).ToString();
        ordercode.Value = orderCode;
        //var pr = StringDelHTML.PriceToIntUp(proPrice);
        //设置package订单参数  具体参数列表请参考官方pdf文档，请勿随意设置
        packageReqHandler.setParameter("body", "共学视频购买"); //商品信息 127字符
        packageReqHandler.setParameter("appid", PayConfig.AppId);
        packageReqHandler.setParameter("mch_id", PayConfig.MchId);
        packageReqHandler.setParameter("nonce_str", NonceStr.ToLower());
        packageReqHandler.setParameter("notify_url", PayConfig.NotifyUrl);
        packageReqHandler.setParameter("openid", this.UserOpenId);
        packageReqHandler.setParameter("out_trade_no", orderCode); //商家订单号
        packageReqHandler.setParameter("spbill_create_ip", Page.Request.UserHostAddress); //用户的公网ip，不是商户服务器IP
        packageReqHandler.setParameter("total_fee", (orderprice.Value).ToString()); //商品金额,以分为单位(money * 100).ToString()//Convert.ToInt32(0.01)*100).ToString()
        packageReqHandler.setParameter("trade_type", "JSAPI");
        if (!string.IsNullOrEmpty(nID))
        {
            string fatherid = Request.QueryString["agent"] == null ? "0" : Request.QueryString["agent"].ToString() == "" ? "0" : Request.QueryString["agent"].ToString();
            string attach = nID + "," + fatherid + "," + proPrice + "," + orderCode + "," + pID;
            packageReqHandler.setParameter("attach", attach);//自定义参数 127字符
        }

        #endregion

        #region sign===============================
        Sign = packageReqHandler.CreateMd5Sign("key", PayConfig.AppKey);
        LogUtil.WriteLog("WeiPay 页面  sign：" + Sign);
        #endregion

        #region 获取package包======================
        packageReqHandler.setParameter("sign", Sign);

        string data = packageReqHandler.parseXML();
        LogUtil.WriteLog("WeiPay 页面  package（XML）：" + data);

        string prepayXml = HttpUtil.Send(data, "https://api.mch.weixin.qq.com/pay/unifiedorder");
        LogUtil.WriteLog("WeiPay 页面  package（Back_XML）：" + prepayXml);

        //获取预支付ID
        var xdoc = new XmlDocument();
        xdoc.LoadXml(prepayXml);
        XmlNode xn = xdoc.SelectSingleNode("xml");
        XmlNodeList xnl = xn.ChildNodes;
        if (xnl.Count > 7)
        {
            PrepayId = xnl[7].InnerText;
            Package = string.Format("prepay_id={0}", PrepayId);
            LogUtil.WriteLog("WeiPay 页面  package：" + Package);
        }
        #endregion

        #region 设置支付参数 输出页面  该部分参数请勿随意修改 ==============
        var paySignReqHandler = new RequestHandler(Context);
        paySignReqHandler.setParameter("appId", PayConfig.AppId);
        paySignReqHandler.setParameter("timeStamp", TimeStamp);
        paySignReqHandler.setParameter("nonceStr", NonceStr);
        paySignReqHandler.setParameter("package", Package);
        paySignReqHandler.setParameter("signType", "MD5");
        PaySign = paySignReqHandler.CreateMd5Sign("key", PayConfig.AppKey);

        LogUtil.WriteLog("WeiPay 页面  paySign：" + PaySign);
        #endregion
        #endregion
    }
}