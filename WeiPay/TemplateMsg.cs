using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WeiPay
{
    public class TemplateMsg
    {
        string accesstoken;

        public TemplateMsg()
        {
            ServerAuth sa = new ServerAuth();
            if (HttpRuntime.Cache["access_token"] == null)
            {
                accesstoken = sa.Get_access_token();
            }
            else
            {
                accesstoken = HttpRuntime.Cache["access_token"].ToString();
            }
        }



        /// <summary>
        /// 佣金提醒模板
        /// </summary>
        public void CommissionRemind(string openid, string url, string money, string time)
        {
            string posturl = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", accesstoken);
            string json = "{\"touser\":\"" + openid + "\",\"template_id\":\"i4ksn1DxBGofkL_pqv5dO-dkbBIY-q8I3PZR4_y692g\",\"url\":\"" + url + "\",\"topcolor\":\"#7B68EE\",";
            json += "\"data\":{";
            json += "\"first\":{\"value\":\"今日奖学金已到账！\",\"color\":\"#743A3A\"},";
            json += "\"keyword1\":{\"value\":\"" + money + "学分\",\"color\":\"#FF0000\"},";
            json += "\"keyword2\":{\"value\":\"" + time + "\",\"color\":\"#0000FF\"},";
            json += "\"remark\":{\"value\":\"您可点击【详情】，查看每日奖学金详细信息\",\"color\":\"#008000\"}";
            json += "}}";

            WeiPay.HttpUtil.Send(json, posturl);
        }

        /// <summary>
        /// 推荐奖励模板
        /// </summary>
        public void NominateReward(string openid, string nickname, string url, string money, string time)
        {

            string posturl = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", accesstoken);
            string json = "{\"touser\":\"" + openid + "\",\"template_id\":\"2NXTSEd2MuiyKHwd6iaNTHLS9OUoJ3pr7e3smlMLINU\",\"url\":\"" + url + "\",\"topcolor\":\"#7B68EE\",";
            json += "\"data\":{";
            json += "\"first\":{\"value\":\"您获得一份推荐佣金！\",\"color\":\"#743A3A\"},";
            json += "\"keyword1\":{\"value\":\"" + nickname + "\",\"color\":\"#0000FF\"},";
            json += "\"keyword2\":{\"value\":\"" + money + "学分\",\"color\":\"#FF0000\"},";
            json += "\"keyword3\":{\"value\":\"" + time + "\",\"color\":\"#0000FF\"},";
            json += "\"remark\":{\"value\":\"您可点击【详情】，查看获得的推荐佣金\",\"color\":\"#008000\"}";
            json += "}}";

            WeiPay.HttpUtil.Send(json, posturl);
        }


        /// <summary>
        /// 邀请关注成功通知(邀请的人)
        /// </summary>
        /// <param name="openid">接收微信消息的openid</param>
        /// <param name="openid">接收微信消息的用户的uID</param>
        /// <param name="nicknameStu">被邀请人</param>
        /// <param name="money"></param>
        /// <param name="nicknameFat">邀请人</param>
        /// <param name="time"></param>
        public void InvitationFollow(string openid, string uid, string nicknameStu, string money, string nicknameFat, string time)
        {
            string url = "http://tv.gongxue168.com/wap/AttentionList.aspx?uid=" + uid;
            string posturl = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", accesstoken);
            string json = "{\"touser\":\"" + openid + "\",\"template_id\":\"MYkJ1aGS-WZuT3O7AGOZVyJuswDx9V8k_eKT33iKQFg\",\"url\":\"" + url + "\",\"topcolor\":\"#7B68EE\",";
            json += "\"data\":{";
            json += "\"first\":{\"value\":\"您好，以下会员是通过您的二维码关注我们的：\",\"color\":\"#743A3A\"},";
            json += "\"keyword1\":{\"value\":\"" + nicknameStu + "\",\"color\":\"#FF0000\"},";
            json += "\"keyword2\":{\"value\":\"" + time + "\",\"color\":\"#0000FF\"},";
            json += "\"keyword3\":{\"value\":\"" + nicknameFat + "\",\"color\":\"#0000FF\"},";
            json += "\"remark\":{\"value\":\"恭喜获取【" + money + "】个学分作为奖励，您可点击【详情】，查看邀请的学员\",\"color\":\"#008000\"}";
            json += "}}";

            WeiPay.HttpUtil.Send(json, posturl);
        }


        /// <summary>
        /// 邀请关注成功通知(被邀请的人)
        /// </summary>
        /// <param name="openid">接收微信消息的openid</param>
        /// <param name="openid">url查看用户的uID</param>
        /// <param name="nicknameStu">被邀请人</param>
        /// <param name="money"></param>
        /// <param name="nicknameFat">邀请人</param>
        /// <param name="time"></param>
        public void InvitationFollowed(string openid, string uid, string nicknameStu, string money, string nicknameFat, string time)
        {
            string url = "http://tv.gongxue168.com/wap/Login.aspx?uid=" + uid;
            string posturl = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", accesstoken);
            string json = "{\"touser\":\"" + openid + "\",\"template_id\":\"MYkJ1aGS-WZuT3O7AGOZVyJuswDx9V8k_eKT33iKQFg\",\"url\":\"" + url + "\",\"topcolor\":\"#7B68EE\",";
            json += "\"data\":{";
            json += "\"first\":{\"value\":\"您好！感谢您关注【共学教育】微信公众号\",\"color\":\"#743A3A\"},";
            json += "\"keyword1\":{\"value\":\"" + nicknameStu + "\",\"color\":\"#FF0000\"},";
            json += "\"keyword2\":{\"value\":\"" + time + "\",\"color\":\"#0000FF\"},";
            json += "\"keyword3\":{\"value\":\"" + nicknameFat + "\",\"color\":\"#0000FF\"},";
            json += "\"remark\":{\"value\":\"我们已赠送" + money + "个学习币到您的账户中，成为会员后可任意消费\",\"color\":\"#008000\"}";
            json += "}}";

            WeiPay.HttpUtil.Send(json, posturl);
        }
    }
}
