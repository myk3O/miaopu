using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using WpPCHelper;
using System.Data;
using Maliang;

public partial class Web_NativeCallBack : System.Web.UI.Page
{
    //二 返回订单信息 
    //xmlData=<xml><appid><![CDATA[wx657cc528349e9b62]]></appid>
    //<openid><![CDATA[oQq_ejvnwWaCWi8-xyV_dgyIu6XE]]></openid>
    //<mch_id><![CDATA[1232858102]]></mch_id>
    //<is_subscribe><![CDATA[Y]]></is_subscribe>
    //<nonce_str><![CDATA[WerqxWcVhzz6yvRV]]></nonce_str>
    //<product_id><![CDATA[85]]></product_id>
    //<sign><![CDATA[56026AEE43047369A3045FF38C518DCB]]></sign>
    //</xml>
    SqlHelper her = new SqlHelper();
    WxPayHelper helper = new WxPayHelper();

    public static string PrepayId = ""; //预支付ID
    public static string Package = "";  //进行支付需要的包
    public static string Sign = "";     //为了获取预支付ID的签名
    protected void Page_Load(object sender, EventArgs e)
    {
        //1.接受微信平台post 过来的XML信息
        //2.验证签名，从XML中获取ProductID，获取产品信息
        //2.1设置out_trade_no，total_fee，notify_url，trade_type，product_id 参数，提交统一接口         
        //3.提交统一接口后获取PrepayId
        //4.将PrepayId 和return 根据参数拼接生xml信息（其中包含生成的package参数）输出
        if (Request.RequestType == "POST")
        {
            try
            {
                StreamReader reader = new StreamReader(Request.InputStream);
                String xmlData = reader.ReadToEnd();
                helper.ReceivePostXmlData(xmlData);
                LogUtil.WriteLog("接收post来的xmlData=" + xmlData);
                if (helper.CheckSign())
                {
                    LogUtil.WriteLog("签名验证通过");
                    string proId = helper.GetXMLNode("product_id");
                    LogUtil.WriteLog("产品id=" + proId);

                    string sql = " select * from ML_Order  where nID=" + proId;
                    DataTable dt = her.ExecuteDataTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        int pri = Convert.ToInt32((Convert.ToDouble(dt.Rows[0]["OrderPrice"]) * 100));

                        #region 业务处理
                        helper.SetParameter("body", "马赛特酒品");
                        helper.SetParameter("out_trade_no", dt.Rows[0]["OrderCode"].ToString());
                        helper.SetParameter("total_fee", pri.ToString());//这里单位是分
                        helper.SetParameter("notify_url", "http://masetchina.com.cn/Web/NativeNotify.aspx");
                        helper.SetParameter("trade_type", "NATIVE");
                        helper.SetParameter("attach", proId);
                        /*<xml><appid><![CDATA[wx657cc528349e9b62]]></appid>
                        <openid><![CDATA[oQq_ejvnwWaCWi8-xyV_dgyIu6XE]]></openid>
                        <mch_id><![CDATA[1232858102]]></mch_id>
                        <is_subscribe><![CDATA[Y]]></is_subscribe>
                        <nonce_str><![CDATA[Mpc7gr1JEXA41Yj4]]></nonce_str>
                        <product_id><![CDATA[85]]></product_id>
                        <body><![CDATA[马赛特酒品]]></body>
                        <out_trade_no><![CDATA[M20150410015830803]]></out_trade_no>
                        <total_fee><![CDATA[1]]></total_fee>
                        <notify_url><![CDATA[http://masetchina.com.cn/Web/NativeNotify.aspx]]></notify_url>
                        <trade_type><![CDATA[NATIVE]]></trade_type>
                        <sign><![CDATA[6950D2527F9F740BE65651CE72970BD2]]></sign>
                        </xml>*/
                        string prepay_id = helper.GetPrepayId();
                        LogUtil.WriteLog("prepay_id=" + prepay_id);

                        if (!string.IsNullOrEmpty(prepay_id))
                        {
                            helper.SetReturnParameter("return_code", "SUCCESS");
                            helper.SetReturnParameter("result_code", "SUCCESS");
                            helper.SetReturnParameter("prepay_id", prepay_id);
                            helper.SetReturnParameter("appid", helper.GetAppId);
                            helper.SetReturnParameter("mch_id", helper.GetMch_Id);
                            helper.SetReturnParameter("nonce_str", WpPCHelper.TenpayUtil.getNoncestr());
                        }
                        else
                        {
                            helper.SetReturnParameter("return_code", "SUCCESS");//返回状态码
                            helper.SetReturnParameter("result_code", "FAIL");//业务结果
                            helper.SetReturnParameter("err_code_des", "预订单生产失败");
                            LogUtil.WriteLog("预订单生产失败");
                        }
                        #endregion
                    }
                    else
                    {
                        helper.SetReturnParameter("return_code", "SUCCESS");//返回状态码
                        helper.SetReturnParameter("result_code", "FAIL");//业务结果
                        helper.SetReturnParameter("err_code_des", "此订单无效");//业务结果
                        LogUtil.WriteLog("订单数据获取失败");
                    }

                }
                else
                {
                    helper.SetReturnParameter("return_code", "FAIL");
                    helper.SetReturnParameter("return_msg", "签名失败");
                    LogUtil.WriteLog("签名验证没有通过");
                }
                string xmlStr = helper.GetReturnXml();
                LogUtil.WriteLog("返回xml=" + xmlStr);
                Response.ContentType = "text/xml";
                Response.Clear();
                Response.Write(xmlStr);
                // Response.End();
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog("异常了" + ex);
            }
            finally
            {
                Response.End();
            }
        }
    }
}