<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderHandling.aspx.cs" Inherits="Wap_OrderHandling" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="MobileOptimized" content="240" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <title>订单支付</title>
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/sub.css" />
    <script>

        var historyurl = "index.aspx";
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
                historyurl = "VideoShow.aspx?pid=" + pid;
            }

        });
    </script>
</head>
<body aria-atomic="False">

    <input type="hidden" runat="server" id="ordercode" />
    <input type="hidden" runat="server" id="orderprice" />
    <input type="hidden" runat="server" id="Gwb" />
    <div class="inContent">
        <ul class="OrderHandling">
            <%-- <li>
                <h3>联系人姓名：</h3>
                <input name="" type="text" placeholder="必填项">
            </li>
            <li>
                <h3>联系人电话：</h3>
                <input name="" type="text" placeholder="必填项">
            </li>
            <li>
                <h3>您的收货区域：</h3><i class="fa fa-caret-down fa1"></i><i class="fa fa-caret-down fa2"></i><i class="fa fa-caret-down fa3"></i>
                <select>
                    <option>上海市</option>
                </select>
                <select>
                    <option>市辖区</option>
                </select>
                <select>
                    <option>宝山区</option>
                </select>
            </li>
            <li>
                <h3>邮寄地址：</h3>
                <input name="" type="text" value="共和新路5895号">
            </li>--%>
        </ul>
        <dl class="OrderHandlingX">
            <dt>总计货款：<span style="color: red">￥<%=proPrice %></span></dt>
            <span style="color:blue"><%=huiyuanPrice %></span>
            <dd>
                <h1>支付方式：</h1>
                <h2>
                    <input type="radio" name="radio" id="radio" value="1" /><label for="radio">学习币支付（余额：￥<%=xuexibi %>）</label></h2>
                <h2>
                    <input type="radio" name="radio" id="radio1" value="2" checked="checked" /><label for="radio1">微信在线支付</label></h2>
            </dd>

            <%--     <dd style="text-align: center">
                <span>*为了测试效果，实际订单价格会放大10000倍</span>   </dd>--%>
            <dd>
                <input name="" type="button" value="确认付款" onclick="ordertj();" />
            </dd>
        </dl>
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

        var radio = $("input[name=radio]:checked").val();
        if (radio != undefined && radio == "1") {//第一次必须是微信支付。否则notify.aspx里面函数怎么执行

            var xuexb = (parseFloat($('#Gwb').val()) * 100).toFixed(0);
            var oprice = (parseFloat($('#orderprice').val())).toFixed(0);
            if (parseInt(oprice) > parseInt(xuexb)) {//小于
                alert('学习币余额不足！');
                return false;
            }

            $.ajax({
                type: "post",
                async: false,
                cache: false,
                url: "Handler.ashx",
                dataType: "json",
                data: {
                    "Method": "MakeOrderXuexb",
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

        } else {

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

    }

</script>
