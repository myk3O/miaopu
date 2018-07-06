<%@ WebHandler Language="C#" Class="QrCode" %>
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Web;
using System.Drawing.Imaging;
using WpPCHelper;
using System.Collections.Generic;
using System.IO;
public class QrCode : IHttpHandler
{

    WxPayHelper helper = new WxPayHelper();
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request["orderid"] != null && context.Request["orderid"] != "")
        {
            string qrurl = CreateQRCodeUrl(context.Request["orderid"].ToString());

            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            Gma.QrCodeNet.Encoding.QrCode qrCode = new Gma.QrCodeNet.Encoding.QrCode();
            qrEncoder.TryEncode(qrurl, out qrCode);
            using (MemoryStream ms = new MemoryStream())
            {
                var renderer = new GraphicsRenderer(new FixedModuleSize(4, QuietZoneModules.Two));
                renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
                context.Response.ContentType = "image/png";
                context.Response.OutputStream.Write(ms.GetBuffer(), 0, (int)ms.Length);
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    public string CreateQRCodeUrl(string orderid)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("appid", helper.GetAppId);
        dic.Add("mch_id", helper.GetMch_Id);
        dic.Add("nonce_str", TenpayUtil.getNoncestr().ToLower());//TenpayUtil.getNoncestr()
        dic.Add("product_id", orderid);
        dic.Add("time_stamp", TenpayUtil.getTimestamp());
        dic.Add("sign", helper.GetSign(dic));

        string url = WxPayHelper.FormatBizQueryParaMap(dic, false);//这里不要url编码
        WeiPay.LogUtil.WriteLog("二维码地址生成" + url);
        return "weixin://wxpay/bizpayurl?" + url;
    }
}