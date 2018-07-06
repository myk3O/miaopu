using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using WpPCHelper;
using Maliang;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing.Imaging;
using System.Text;

public partial class Web_order_create : System.Web.UI.Page
{
    public static string Sign = "";     //为了获取预支付ID的签名
    //----------------
    public string orderheader;//订单编号
    public string orderbody;//下单时间
    public string orderState;
    public string Product;
    public string AllPrice;

    private string UserID
    {
        get
        {
            return ViewState["UserID"] == null ? "" : ViewState["UserID"].ToString();
        }
        set
        {
            ViewState["UserID"] = value;
        }
    }


    private string nID
    {
        get
        {
            return ViewState["nID"] == null ? "1" : ViewState["nID"].ToString();
        }
        set
        {
            ViewState["nID"] = value;
        }
    }
    SqlHelper her = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["LoginUserId"] == null || Session["LoginUserId"] == "")
            {
                Response.Redirect("passport-login.aspx");
            }
            else
            {
                UserID = Session["LoginUserId"].ToString();

            }

            if (Request.QueryString["nID"] != "" && Request.QueryString["nID"] != null)
            {
                nID = Request.QueryString["nID"].ToString();
                GetOrder(nID);
                GetProduct(nID);
                litQrcode.Value = nID;
            }
        }

    }
    private void GetOrder(string nid)
    {
        StringBuilder sbh = new StringBuilder();
        StringBuilder sbf = new StringBuilder();

        string sql = " select a.*,b.StateName from ML_Order a join ML_OrderState b on a.OrderState=b.nID where a.nID=" + nid;
        DataTable dt = her.ExecuteDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            string orderTime = Convert.ToDateTime(dr["CreateTime"]).ToString("yyyy-MM-dd HH:mm");


            sbh.AppendLine("<tr>");
            sbh.AppendLine("<td><h4>订单编号：" + dr["OrderCode"].ToString() + "</h4></td>");
            sbh.AppendLine("<td>下单日期：" + orderTime + "</td>");
            sbh.AppendLine("<td>支付方式：" + dr["OcType"].ToString() + "</td>");
            sbh.AppendLine("<td>状态：" + dr["StateName"].ToString() + "</td>");
            sbh.AppendLine("</tr>");

            sbf.AppendLine("<tr>");
            sbf.AppendLine("<th>收货人姓名：</th>");
            sbf.AppendLine("<td>" + dr["Lianxiren"].ToString() + "</td>");
            sbf.AppendLine("<th>联系电话：</th>");
            sbf.AppendLine("<td>" + dr["PhoneNumber"].ToString() + "</td>");
            sbf.AppendLine("</tr>");

            if (Convert.ToInt32(dr["OrderState"].ToString()) > 2)//待收货才有
            {
                sbf.AppendLine("<tr>");
                sbf.AppendLine("<th>物流公司：</th>");
                sbf.AppendLine("<td>" + dr["worktime"].ToString() + "</td>");
                sbf.AppendLine("<th>物流单号：</th>");
                sbf.AppendLine("<td>" + dr["workComment"].ToString() + "</td>");
                sbf.AppendLine("</tr>");
            }

            sbf.AppendLine("<tr>");
            sbf.AppendLine("<th valign='top'>配送地区：</th>");
            sbf.AppendLine("<td colspan='3' valign='top'>" + dr["OspecialStr"].ToString() + "</td>");
            sbf.AppendLine("</tr>");

            sbf.AppendLine("<tr>");
            sbf.AppendLine("<th valign='top'> 收货人地址：</th>");
            sbf.AppendLine("<td colspan='3' valign='top'>" + dr["Address"].ToString() + "</td>");
            sbf.AppendLine("</tr>");

            sbf.AppendLine("<tr>");
            sbf.AppendLine("<th valign='top'>订单附言：</th>");
            sbf.AppendLine("<td colspan='3' valign='top'>" + dr["tComment"].ToString() + "</td>");
            sbf.AppendLine("</tr>");


            AllPrice = dr["OrderPrice"].ToString();

        }
        orderheader = sbh.ToString();
        orderbody = sbf.ToString();
    }

    private void GetProduct(string nid)
    {
        StringBuilder sb = new StringBuilder();
        string sql = "select a.*,b.tPic,b.tTitle,b.price from ML_OrderProduct a join ML_ServiceArea b on a.proID=b.nID where a.orderID=" + nid;
        DataTable dt = her.ExecuteDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            sb.AppendLine("<tr>");
            sb.AppendLine("<td> <a href='product-117.aspx?id=" + dr["proID"].ToString() + "' target='_blank'>");
            sb.AppendLine("<img src='../Admin/upload_Img/Pruduct/" + dr["tPic"].ToString() + "' style='width: 50px; height: 50px;' /></a></td>");
            sb.AppendLine("<td> <a href='product-117.aspx?id=" + dr["proID"].ToString() + "' target='_blank'>" + dr["tTitle"].ToString() + "</a></td>");
            sb.AppendLine("<td>" + dr["price"].ToString() + "</td>");
            sb.AppendLine("<td>" + dr["probyCount"].ToString() + "</td>");
            sb.AppendLine("<td>" + dr["proPrice"].ToString() + ".00</td>");
            sb.AppendLine("</tr>");
        }

        Product = sb.ToString();
    }

    //protected void btnPay_Click(object sender, EventArgs e)
    //{
    //    ImageAdd();
    //}


    WxPayHelper helper = new WxPayHelper();
    private string CreateQRCodeUrl()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("appid", helper.GetAppId);
        dic.Add("mch_id", helper.GetMch_Id);
        dic.Add("nonce_str", WpPCHelper.TenpayUtil.getNoncestr().ToLower());//TenpayUtil.getNoncestr()
        dic.Add("product_id", nID);
        dic.Add("time_stamp", WpPCHelper.TenpayUtil.getTimestamp());
        #region sign===============================
        Sign = helper.GetSign(dic);
        LogUtil.WriteLog("sign：" + Sign);
        dic.Add("sign", Sign);
        #endregion

        string urlce = WxPayHelper.FormatBizQueryParaMap(dic, false);//这里不要url编码

        LogUtil.WriteLog("urlce：" + urlce);

        string url = string.Format(@"sign={0}&appid={1}&mch_id={2}&product_id={3}&time_stamp={4}&nonce_str={5}"
           , Sign, helper.GetAppId, helper.GetMch_Id, nID, WpPCHelper.TenpayUtil.getTimestamp(), WpPCHelper.TenpayUtil.getNoncestr().ToLower());
        LogUtil.WriteLog("二维码地址生成" + url);
        return "weixin://wxpay/bizpayurl?" + urlce;


        /*------另一种方式--------*/

        //var packageReqHandler = new RequestHandler(Context);
        ////初始化
        //packageReqHandler.init();
        ////设置package订单参数  具体参数列表请参考官方pdf文档，请勿随意设置
        //packageReqHandler.setParameter("appid", PayConfig.AppId);
        //packageReqHandler.setParameter("mch_id", PayConfig.MchId);
        //packageReqHandler.setParameter("nonce_str", WeiPay.TenpayUtil.getNoncestr().ToLower());
        //packageReqHandler.setParameter("product_id", nID);
        //packageReqHandler.setParameter("time_stamp", WeiPay.TenpayUtil.getTimestamp());
        //#region sign===============================
        //Sign = packageReqHandler.CreateMd5Sign("key", PayConfig.AppKey);
        //LogUtil.WriteLog("WeiPay 页面  sign：" + Sign);
        //packageReqHandler.setParameter("sign", Sign);
        //#endregion
        //string data = packageReqHandler.parseXML();
        //LogUtil.WriteLog("WeiPay 页面  package（XML）：" + data);
        //string url = string.Format(@"sign={0}&appid={1}&mch_id={2}&product_id={3}&time_stamp={4}&nonce_str={5}"
        //    , Sign, PayConfig.AppId, PayConfig.MchId, nID, WeiPay.TenpayUtil.getTimestamp(), WeiPay.TenpayUtil.getNoncestr().ToLower());
        //LogUtil.WriteLog("WeiPay 页面  package（XML）：" + url);
        //return "weixin://wxpay/bizpayurl?" + url;
    }


    private void ImageAdd()
    {
        string qrurl = CreateQRCodeUrl();
        QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
        Gma.QrCodeNet.Encoding.QrCode qrCode = new Gma.QrCodeNet.Encoding.QrCode();
        qrEncoder.TryEncode(qrurl, out qrCode);
        using (MemoryStream ms = new MemoryStream())
        {
            var renderer = new GraphicsRenderer(new FixedModuleSize(4, QuietZoneModules.Two));
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            Response.ContentType = "image/png";
            Response.OutputStream.Write(ms.GetBuffer(), 0, (int)ms.Length);
        }
        LogUtil.WriteLog("二维码生成");
    }

}