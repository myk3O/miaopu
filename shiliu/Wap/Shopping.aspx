<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Shopping.aspx.cs" Inherits="Wap_Shopping" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="MobileOptimized" content="240" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <title>消费记录</title>
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/sub.css" />
</head>
<body aria-atomic="False">
    <div class="inContent">
        <dl class="MyPresent">
            <dt style="font-size: large">消费记录</dt>
            <%=liststr %>
            <%--      <dd>1.桃子（90.00）<span>2015-09-03 00:01:34</span></dd>
            <dd>2.QZQ （80.00）<span>2015-09-03 00:01:34</span></dd>
            <dd>3.桃子（90.00）<span>2015-09-03 00:01:34</span></dd>
            <dd>4.QZQ （80.00）<span>2015-09-03 00:01:34</span></dd>
            <dd>5.桃子（90.00）<span>2015-09-03 00:01:34</span></dd>
            <dd>6.QZQ （80.00）<span>2015-09-03 00:01:34</span></dd>--%>
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
<script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js" type="text/javascript"></script>
<script type="text/javascript">
    document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
        // 通过下面这个API隐藏右上角按钮hideOptionMenu
        WeixinJSBridge.call('hideOptionMenu');
    });

</script>