using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WpPCHelper;
using System.IO;

public partial class Web_NativeNotify : System.Web.UI.Page
{
    Order or = new Order();
    WxPayHelper helper = new WxPayHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.RequestType == "POST")
        {
            try
            {
                StreamReader reader = new StreamReader(Request.InputStream);
                String xmlData = reader.ReadToEnd();
                helper.ReceivePostXmlData(xmlData);
                LogUtil.WriteLog("Notify_接收post来的xmlData=" + xmlData);
                if (helper.CheckSign())
                {
                    Dictionary<string, string> dicBack = helper.GetParameter();//获取所有参数
                    if (dicBack != null && dicBack.Keys.Contains("return_code"))
                    {
                        if (dicBack["return_code"] == "SUCCESS")
                        {
                            LogUtil.WriteLog("return_code=SUCCESS");
                            if (dicBack["result_code"] == "SUCCESS")
                            {
                                LogUtil.WriteLog("result_code=SUCCESS");
                                string out_trade_no = dicBack["out_trade_no"];//商品订单号
                                LogUtil.WriteLog("out_trade_no=" + out_trade_no);
                                //string attach_no = dicBack["attach"];//订单id
                                //1.验证商户订单号是否被处理
                                //2.处理过直接返回成功，否则返回
                                //此处根据out_trade_no 处理业务数据
                                //attach  订单id
                                string pid = dicBack["attach"];
                                LogUtil.WriteLog("待处理订单id=" + pid);
                                or.UpdateOrderPay(pid, "微信支付", 2);
                                //Response.Redirect("order-detail.aspx?id=" + pid);
                                //处理业务数据结束

                                LogUtil.WriteLog("Notify_验证签名成功");
                                helper.SetReturnParameter("return_code", "SUCCESS");
                                helper.SetReturnParameter("return_msg", "");
                            }
                        }
                        if (dicBack["return_code"] == "FAIL")
                        {
                            LogUtil.WriteLog("Notify_验证签名成功");
                            helper.SetReturnParameter("return_code", "SUCCESS");
                            helper.SetReturnParameter("return_msg", dicBack["return_msg"]);
                        }
                    }
                }
                else
                {
                    LogUtil.WriteLog("Notify_验证签名失败");
                    helper.SetReturnParameter("return_code", "FAIL");
                    helper.SetReturnParameter("return_msg", "签名失败");
                }
                string xmlStr = helper.GetReturnXml();
                LogUtil.WriteLog("Notify_返回xml=" + xmlStr);
                Response.ContentType = "text/xml";
                Response.Clear();
                Response.Write(xmlStr);
                // Response.End();
                //Response.Redirect("order-detail.aspx?id=69");
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog("Notify_异常了" + ex);
            }
            finally
            {
                Response.End();
            }
        }
    }
}