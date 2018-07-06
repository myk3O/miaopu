<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pay.aspx.cs" Inherits="Wap_Pay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="MobileOptimized" content="240" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <title>我要支付</title>
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/sub.css" />
    <script>

        var historyurl = "Login.aspx";
        var pid;

        (function ($) {
            $.getUrlParam = function (name) {
                var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                var r = window.location.search.substr(1).match(reg);
                if (r != null) return unescape(r[2]); return null;
            }
        })(jQuery);

        $(function () {
            // $('#hidagent').val(window.localStorage.getItem("agent"));

            pid = $.getUrlParam('pid');
            if (pid != null && pid != "") {
                historyurl = "ProductShow.aspx?pid=" + pid;
            }

        });
    </script>
</head>
<body aria-atomic="False">
    <input type="hidden" runat="server" id="ordercode" />
    <input type="hidden" runat="server" id="orderprice" />
    <div class="inContent">

        <div id="memberList" style="margin-top:20px">

            <div class="StylistZ">
                <abbr>
                    购买本视频共需要支付<a style="color: red; font-size: 20px"><%= proPrice%></a>元
                    <input type="button" value="立即支付" id="btnhuiyuan" onclick="ordertj();" /></abbr>
            </div>
        </div>

    </div>
    <div class="FootbqK"></div>
    <div class="Footer">
        <script>
            $(function () {
                var lg = window.localStorage.getItem("LoginUserId");
                if (lg != null && lg != "") {
                    $.ajax({
                        type: "post",
                        async: false,
                        cache: false,
                        url: "Handler.ashx",
                        dataType: "json",
                        data: {
                            "Method": "Memberisfxs",
                            "userID": lg
                        },
                        success: function (data) {
                            if (data) {
                                var text1 = "<a href='BuyVideo.aspx?uid=" + lg + "'>";
                                text1 += " <img src='Img/Micro_06.png' />";
                                text1 += "<p>已购视频</p></a>";
                                $("#footer1").empty();
                                $("#footer1").html(text1);


                                var text2 = "<a href='MyLogin.aspx?uid=" + lg + "'>";
                                text2 += " <img src='Img/foot_02.jpg' />";
                                text2 += "<p>学习中心</p></a>";
                                $("#footer2").empty();
                                $("#footer2").html(text2);

                            }
                        },
                        error: function () {
                            alert('连接超时');
                        }

                    });

                }
            });

        </script>
        <ul>
            <li><a href="index.aspx">
                <img src="Img/foot_01.png" />
                <p>首页</p>
            </a></li>
            <li><a href="MonthRanking.aspx">
                <img src="Img/foot_02.png" />
                <p>榜上有名</p>
            </a></li>
            <li id="footer1"><a href="Product.aspx">
                <img src="Img/foot_03.png" />
                <p>成为学员</p>
            </a></li>
            <li id="footer2"><a href="Login.aspx" class="cur">
                <img src="Img/foot_04.png" />
                <p>学习中心</p>
            </a></li>
        </ul>
    </div>
</body>
</html>
<script>

    //提交订单
    function ordertj() {
        WeixinJSBridge.invoke('getBrandWCPayRequest', {
            "appId": "<%= WeiPay.PayConfig.AppId %>", //公众号名称，由商户传入
            "timeStamp": "<%= TimeStamp %>", //时间戳
            "nonceStr": "<%= NonceStr %>", //随机串
            "package": "<%= Package %>", //扩展包
            "signType": "MD5", //微信签名方式:1.sha1
            "paySign": "<%= PaySign %>" //微信签名
        },
           function (res) {
               if (res.err_msg == "get_brand_wcpay_request:ok") {
                   //alert("微信支付成功!");
                   $.ajax({
                       type: "post",
                       async: false,
                       cache: false,
                       url: "Handler.ashx",
                       dataType: "json",
                       data: {
                           "Method": "MakeOrder",
                           "userID": window.localStorage.getItem("LoginUserId"),
                           "proID": pid,
                           "agentID": window.localStorage.getItem("agent"),
                           "OrderCode": $('#ordercode').val(),
                           "Price": $('#orderprice').val(),
                       },
                       success: function (e) {
                           if (e) {
                               window.location.href = historyurl;
                           } else {
                               alert('支付异常，请联系商家！');
                           }

                       },
                       error: function () {
                           alert('连接超时');
                       }

                   });


               } else if (res.err_msg == "get_brand_wcpay_request:cancel") {
                   alert("用户取消支付!");
               } else {
                   alert(res.err_msg);
                   alert("支付失败!");
               }
               // 使用以上方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回ok，但并不保证它绝对可靠。
               //因此微信团队建议，当收到ok返回时，向商户后台询问是否收到交易成功的通知，若收到通知，前端展示交易成功的界面；若此时未收到通知，商户后台主动调用查询订单接口，查询订单的当前状态，并反馈给前端展示相应的界面。
           });



    }

</script>
