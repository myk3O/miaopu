using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Xml;
using Newtonsoft.Json;
using Model;
using System.Net;
using System.IO;
using System.Data;

namespace WeiPay
{
    public class ServerAuth
    {
        private static readonly object lockerSA = new object();
        MemberHelperModel mb = new MemberHelperModel();
        private static string Token = WeiPay.PayConfig.Token;

        /// <summary>  
        /// 微信公众平台操作类  
        /// </summary>  
        public static void Auth()
        {
            string echoStr = System.Web.HttpContext.Current.Request.QueryString["echoStr"];
            if (CheckSignature()) //校验签名是否正确  
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    System.Web.HttpContext.Current.Response.Write(echoStr); //返回原值表示校验成功  
                    System.Web.HttpContext.Current.Response.End();
                }
            }


        }

        /// <summary>  
        /// 验证微信签名  
        /// * 将token、timestamp、nonce三个参数进行字典序排序  
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密  
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信 。  
        /// </summary>  
        /// <returns></returns>  
        public static bool CheckSignature()
        {
            string signature = System.Web.HttpContext.Current.Request.QueryString["signature"];
            string timestamp = System.Web.HttpContext.Current.Request.QueryString["timestamp"];
            string nonce = System.Web.HttpContext.Current.Request.QueryString["nonce"];
            //加密/校验流程：  
            //1. 将token、timestamp、nonce三个参数进行字典序排序  
            string[] ArrTmp = { Token, timestamp, nonce };
            Array.Sort(ArrTmp);//字典排序  
            //2.将三个参数字符串拼接成一个字符串进行sha1加密  
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            //3.开发者获得加密后的字符串可与signature对比，标识该请求来源于微信 。  
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void recMessage()
        {
            //接收消息
            string postStr = "";//接收XML格式字符串
            if (System.Web.HttpContext.Current.Request.HttpMethod.ToLower() == "post")
            {
                //#region 读取消息XML
                System.IO.Stream s = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                postStr = System.Text.Encoding.UTF8.GetString(b);
                if (!string.IsNullOrEmpty(postStr))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(postStr);
                    XmlElement rootElement = doc.DocumentElement;
                    string shiliuID = rootElement.SelectSingleNode("ToUserName").InnerText;
                    string openID = rootElement.SelectSingleNode("FromUserName").InnerText;
                    if (rootElement.SelectSingleNode("MsgType").InnerText.Equals("event"))
                    {
                        WeiPay.LogUtil.WriteLogWx("公众号Event事件：" + rootElement.SelectSingleNode("Event").InnerText + ":" + openID);
                        string contents = "";
                        switch (rootElement.SelectSingleNode("Event").InnerText)
                        {
                            case "subscribe":
                                var EventKey = rootElement.SelectSingleNode("EventKey").InnerText;
                                WeiPay.LogUtil.WriteLogWx("EventKey:" + EventKey);
                                if (string.IsNullOrEmpty(EventKey))
                                {
                                    WeiPay.LogUtil.WriteLogWx("搜公众号关注，非扫码关注，不给注册");
                                    contents = "关注失败，因为您不是通过好友的推荐进入公众号！您可以先取消关注公众平台后，重新扫描您好友的二维码进行关注！";
                                }
                                else
                                {
                                    WeiPay.LogUtil.WriteLogWx("公众号关注，扫码关注");
                                    string header = "qrscene_";
                                    var scene_id = string.Empty;
                                    if (EventKey.Contains(header))
                                    {
                                        //这个是首次关注
                                        scene_id = EventKey.Replace(header, "");//获取 父nID
                                    }
                                    else
                                    {
                                        scene_id = EventKey;
                                    }
                                    WeiPay.LogUtil.WriteLogWx("scene_id:" + scene_id);

                                    //判断这个用户是否注册过,
                                    lock (lockerSA)
                                    {
                                        if (!mb.Verification(openID))
                                        {
                                            //没注册，则注册
                                            if (mb.MemberInsert(openID, "", 1, "", scene_id))
                                            {
                                                LogUtil.WriteLogWx("注册成功");
                                                UpdateOpenID(openID);//更新用户微信信息
                                                YaoQingGuanZhu(openID, scene_id);

                                                //
                                            }
                                            else
                                            {
                                                LogUtil.WriteLogWx("注册失败");
                                            }
                                        }
                                        else
                                        {
                                            //如果注册过（可能原因是，用户取消了公众号关注，后又自己关注进入）
                                            LogUtil.WriteLogWx("已经注册过,更新基本信息");
                                            UpdateOpenID(openID);
                                        }
                                    }
                                    contents = GetContents(openID);// 应该要注意判断一下，万一注册失败
                                }

                                break;
                            case "CLICK":
                                switch (rootElement.SelectSingleNode("EventKey").InnerText)
                                {
                                    case "vi":
                                        break;
                                    case "digital":
                                        break;
                                }
                                break;
                            case "SCAN"://用户已经关注时的推送
                                WeiPay.LogUtil.WriteLogWx("SCAN事件");
                                if (mb.Verification(openID))
                                {
                                    LogUtil.WriteLogWx("已经注册过:SCAN事件");
                                    UpdateOpenID(openID);
                                    contents = GetContents(openID);
                                }
                                else
                                {
                                    contents = "您已经关注过公众号！建议您先取消关注公众平台后，重新扫描您好友的二维码进行关注！";
                                }
                                break;

                            case "VIEW": //已经关注的用户，但是数据库里没有数据了，用户又点了菜单（一般不会有这种情况，除非数据库中数据丢失）
                                WeiPay.LogUtil.WriteLogWx("VIEW事件");
                                //判断这个用户是否注册过，注册过，扫了别人的码也没用
                                if (mb.Verification(openID))
                                {
                                    LogUtil.WriteLogWx("已经注册过，VIEW事件");
                                    UpdateOpenID(openID);
                                    contents = GetContents(openID);
                                }
                                else
                                {
                                    contents = "您已经关注过公众号！建议您先取消关注公众平台后，重新扫描您好友的二维码进行关注！";
                                }
                                break;
                            default: WeiPay.LogUtil.WriteLogWx("其他事件");
                                contents = "公众号暂不支持其他消息服务！";
                                break;
                        }

                        PostWxMsg(contents, openID, shiliuID);
                    }
                    else
                    {
                        WeiPay.LogUtil.WriteLogWx("微信用户点击的不是event事件");
                    }
                }
                else
                {
                    WeiPay.LogUtil.WriteLogWx("处理程序接收到消息为空");
                }

            }
            else
            {
                WeiPay.LogUtil.WriteLogWx("非Post返回数据");
            }


        }
        private string GetContents(string openid)
        {
            string cts = "边学习、边赚钱！您是第" + GetMemberCode(openid) + "位共学见习生，学习改变命运，分享成就未来，点击【<a href=\"http://tv.gongxue168.com/wap/Index.aspx\">立即学习</a>】，随时随地、想学就学！共学教育——移动商学院引领者！";

            return cts;
        }

        /// <summary>
        /// 获取用户code
        /// </summary>
        /// <param name="opendid"></param>
        /// <returns></returns>
        private string GetMemberCode(string opendid)
        {
            string code = "null";
            DataTable dtCode = mb.GetMemberByOpenID(opendid);
            if (dtCode.Rows.Count > 0)
            {
                code = dtCode.Rows[0]["MemberCode"].ToString();
            }
            return code;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="openid"></param>
        private void UpdateByOpenID(string openid, string fid)
        {
            string AccessToken = string.Empty;
            if (HttpRuntime.Cache["access_token"] == null)
            {
                AccessToken = Get_access_token();
            }
            else
            {
                AccessToken = HttpRuntime.Cache["access_token"].ToString();
            }
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", AccessToken, openid);
            string returnStr = WeiPay.HttpUtil.Send("", url);
            WeiPay.OpenModel obj = new WeiPay.OpenModel();
            try
            {
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<WeiPay.OpenModel>(returnStr);
            }
            catch (Exception e)
            {
                WeiPay.LogUtil.WriteLogWx("obj转换错误(UpdateByOpenID)" + e.Message);
            }
            //已经注册,修改一下
            if (mb.MemberUpdate(openid, obj.nickname, obj.sex, obj.headimgurl))
            {
                WeiPay.LogUtil.WriteLogWx("关注的时候，修改信息成功");
            }
            else
            {
                WeiPay.LogUtil.WriteLogWx("关注的时候，修改信息失败");
            }

        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="openid"></param>
        private void UpdateOpenID(string openid)
        {
            string AccessToken = string.Empty;
            if (HttpRuntime.Cache["access_token"] == null)
            {
                AccessToken = Get_access_token();
            }
            else
            {
                AccessToken = HttpRuntime.Cache["access_token"].ToString();
            }
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", AccessToken, openid);
            string returnStr = WeiPay.HttpUtil.Send("", url);
            WeiPay.OpenModel obj = new WeiPay.OpenModel();
            try
            {
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<WeiPay.OpenModel>(returnStr);
            }
            catch (Exception e)
            {
                WeiPay.LogUtil.WriteLogWx("obj转换错误(UpdateOpenID)" + e.Message + ";  returnStr:" + returnStr);
            }
            //已经注册,修改一下
            if (mb.MemberUpdate(openid, obj.nickname, obj.sex, obj.headimgurl))
            {
                WeiPay.LogUtil.WriteLogWx("关注的时候，修改信息成功");
            }
            else
            {
                WeiPay.LogUtil.WriteLogWx("关注的时候，修改信息失败");
            }

        }

        /// <summary>
        /// 邀请关注成功通知（发送给邀请人/被邀请人）
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="fid"></param>
        public void YaoQingGuanZhu(string openid, string fid)
        {
            ConfigModel cf = new ConfigModel();
            AttentionHelp ah = new AttentionHelp();
            SqlHelperModel her = new SqlHelperModel();
            DateTime time = System.DateTime.Now;
            TemplateMsg tm = new TemplateMsg();
            //查找被邀请人
            string sql1 = "select nID, nickname from ML_Member where openid='" + openid + "'";
            DataTable dt1 = her.ExecuteDataTable(sql1);
            string nIDStu = dt1.Rows[0]["nID"] == null ? "" : dt1.Rows[0]["nID"].ToString();
            string nicknameStu = dt1.Rows[0]["nickname"] == null ? "" : dt1.Rows[0]["nickname"].ToString();
            //查找邀请人
            string sql2 = "select nickname,openid from ML_Member where nID=" + fid;
            DataTable dt2 = her.ExecuteDataTable(sql2);
            string nicknameFat = dt2.Rows[0]["nickname"] == null ? "" : dt2.Rows[0]["nickname"].ToString();
            string openidFat = dt2.Rows[0]["openid"] == null ? "" : dt2.Rows[0]["openid"].ToString();
            //获取时间
            string longdate = time.ToLongDateString().ToString();
            string shorttime = time.ToShortTimeString().ToString();

            //发送给邀请人
            ah.AttentionInsert(nIDStu, fid, Convert.ToInt32(cf.gzPart));
            tm.InvitationFollow(openidFat, fid, nicknameStu, cf.gzPart.ToString(), nicknameFat, longdate + " " + shorttime);
            //发送给被邀请人
            ah.AttentionInserted(nIDStu, fid, Convert.ToInt32(cf.gzFirst));
            tm.InvitationFollowed(openid, nIDStu, nicknameStu, cf.gzFirst.ToString(), nicknameFat, longdate + " " + shorttime);
        }


        private void PostWxMsg(string content, string openid, string shiliuid)
        {
            var eventBackXml = "<xml><ToUserName><![CDATA[" + openid
                                  + "]]></ToUserName><FromUserName><![CDATA[" + shiliuid
                                  + "]]></FromUserName><CreateTime>" + DateTime.Now.ToString("yyyyMMddHHmmss")
                                  + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + content + "]]></Content></xml>";
            // HttpUtil.Send(eventBackXml, "https://api.mch.weixin.qq.com/pay/unifiedorder");
            HttpContext.Current.Response.Write(eventBackXml);
            HttpContext.Current.Response.End();
        }


        public void PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                //return ex.Message;
                // MessageBox.Show(ex.Message);
            }
            // return ret;
        }

        /// <summary>
        /// 获取并缓存access_token
        /// </summary>
        /// <returns></returns>
        public string Get_access_token()
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", WeiPay.PayConfig.AppId, WeiPay.PayConfig.AppSecret);
            string returnStr = WeiPay.HttpUtil.Send("", url);
            WeiPay.OpenModel obj = new WeiPay.OpenModel();
            try
            {
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<WeiPay.OpenModel>(returnStr);
            }
            catch (Exception e)
            {
                WeiPay.LogUtil.WriteLogWx("obj转换错误(Get_access_token)" + e.Message);
            }
            if (HttpRuntime.Cache["access_token"] == null)
            {
                HttpRuntime.Cache.Insert("access_token", obj.access_token);
                //HttpRuntime.Cache与HttpContext.Current.Cache是同一对象,建议使用HttpRuntime.Cache                
            }
            else
            {
                HttpRuntime.Cache.Remove("access_token");
                HttpRuntime.Cache.Insert("access_token", obj.access_token);
            }
            WeiPay.LogUtil.WriteLogWx("一般access_token：" + HttpRuntime.Cache["access_token"].ToString());
            return HttpRuntime.Cache["access_token"].ToString();
        }

        /// 调用微信接口获取带参数公众号永久二维码的ticket
        /// 使用方法：https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=TICKET
        ///二维码带的参数
        /// json:ticket:换取二维码的凭证，expire_seconds:凭证有效时间，url:二维码解析后的地址。此处返回ticket 否则返回错误码
        public string GetQrcode(string scene_str)
        {
            string codeUrl = string.Empty;
            string AccessToken = string.Empty;
            if (HttpRuntime.Cache["access_token"] == null)
            {
                AccessToken = Get_access_token();
            }
            else
            {
                AccessToken = HttpRuntime.Cache["access_token"].ToString();
            }

            string QrcodeUrl = string.Format("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}", AccessToken);//WxQrcodeAPI接口
            string PostJson = "{\"expire_seconds\": 1800, \"action_name\": \"QR_LIMIT_STR_SCENE\", \"action_info\": {\"scene\": {\"scene_str\": " + scene_str + "}}}";
            string returnStr = WeiPay.HttpUtil.Send(PostJson, QrcodeUrl);
            WeiPay.Qrcode qc = new WeiPay.Qrcode();
            try
            {
                qc = Newtonsoft.Json.JsonConvert.DeserializeObject<WeiPay.Qrcode>(returnStr);
            }
            catch (Exception e)
            {
                WeiPay.LogUtil.WriteLogWx("obj转换错误(GetQrcode)" + e.Message);
            }
            if (string.IsNullOrEmpty(qc.errcode))
            {
                codeUrl = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + qc.ticket;
            }
            else
            {
                WeiPay.LogUtil.WriteLogWx("错误的Json返回示例:" + qc.errcode);
            }
            return codeUrl;

        }


    }
}
