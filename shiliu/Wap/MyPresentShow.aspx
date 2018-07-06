<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyPresentShow.aspx.cs" Inherits="Wap_MyPresentShow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="MobileOptimized" content="240" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <title>兑换申请</title>
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/sub.css" />
    <script>
        (function ($) {
            $.getUrlParam = function (name) {
                var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                var r = window.location.search.substr(1).match(reg);
                if (r != null) return unescape(r[2]); return null;
            }
        })(jQuery);
        $(function () {

            loginUser = window.localStorage.getItem("LoginUserId"); //先看缓存
            if (loginUser != null || loginUser != "") {
                $.ajax({
                    type: "post",
                    async: false,
                    cache: false,
                    url: "Handler.ashx",
                    dataType: "json",
                    data: {
                        "Method": "GetBankCardByUid",
                        "userID": loginUser
                    },
                    success: function (data) {
                        if (data != null && data.Cid != null) {
                            $("#pbname").val(data.TypeName);
                            $("#pname").val(data.Huming);
                            $("#pkahao").val(data.Zhanghao);
                            $("#pzhname").val(data.Address);
                        } else {
                            if (confirm("您还未添加收款银行账号，是否前往")) {
                                window.location.href = "MyBankcard.aspx?jp=fg"
                            }
                        }
                    },
                    error: function () {
                        alert('连接超时');
                    }

                });
            }
        });


        function doMoney(u) {
            var yongjin = $('#hidpri').val();
            var money = (parseFloat(yongjin)).toFixed(2);
            if (money < 50) {//小于50钱
                alert('单次提取学分必需50起');
                return false;
            }
            var isMoney = true;
            $.ajax({
                type: "post",
                async: false,
                cache: false,
                url: "Handler.ashx",
                dataType: "json",
                data: {
                    "Method": "IsMemberMoneyWeek",
                    "UserID": u
                },
                success: function (e) {

                    isMoney = e;


                },
                error: function () {
                    alert('连接超时');
                },
            });


            if (isMoney) {
                alert('每周只能发起一次申请');
                return false;
            }
            var koushui = $('#hidkoushui').val();
            var text = "您提取学分：" + yongjin + "，另需扣税：" + koushui + "，您确定吗";
            if (confirm(text)) {
                $.ajax({
                    type: "post",
                    async: false,
                    cache: false,
                    url: "Handler.ashx",
                    dataType: "json",
                    data: {
                        "Method": "MoneyApplyByUser",
                        "UserID": u
                    },
                    success: function (e) {
                        if (e) {
                            alert('申请成功');
                            window.location.href = "MyPresent.aspx?uid=" + u;
                            //window.location.reload();
                        } else {
                            alert('申请失败');
                        }

                    },
                    error: function () {
                        alert('连接超时');
                    },
                });
            }

        }

        function applyMoney() {
            var loginUser = window.localStorage.getItem("LoginUserId");
            if (loginUser == null || loginUser == "") {
                //MyChangePage("Person/Individual.html");
                window.location.href = "Login.aspx";
            }
            var objDate = new Date();
            if (objDate.getDay() != 0) {
                alert('仅限每周日0点到24点兑换');
                return false;
            }
            $.ajax({
                type: "post",
                async: false,
                cache: false,
                url: "Handler.ashx",
                dataType: "json",
                data: {
                    "Method": "IsMemberMsg",
                    "UserID": loginUser
                },
                success: function (e) {
                    if (e) {
                        $.ajax({
                            type: "post",
                            async: false,
                            cache: false,
                            url: "Handler.ashx",
                            dataType: "json",
                            data: {
                                "Method": "IsMemberBank",
                                "UserID": loginUser
                            },
                            success: function (e) {
                                if (e) {
                                    doMoney(loginUser);
                                } else {
                                    if (confirm("您还未添加收款银行账号，是否前往")) {
                                        window.location.href = "MyBankcard.aspx?jp=fg"
                                    }
                                }

                            },
                            error: function () {
                                alert('连接超时');
                            },
                        });
                    } else {
                        if (confirm("您还未完善个人信息，是否前往")) {
                            window.location.href = "MyProfileEdit.aspx?jp=fg"
                        }
                    }

                },
                error: function () {
                    alert('连接超时');
                },
            });
        }

    </script>
</head>
<body aria-atomic="False">
    <input type="hidden" runat="server" name="txtyj" id="hidpri" />
    <input type="hidden" runat="server" name="txtyj" id="hidshuihou" />
    <input type="hidden" runat="server" name="txtyj" id="hidkoushui" />
    <input type="hidden" runat="server" name="txtyj" id="shuilv" />
    <div class="inContent">
        <dl class="MyPresentT">
            <dd>
                <h1>可兑换学分：</h1>
                <h2>￥<%=CanGetPri %></h2>
                <h1>提示：单次兑换学分均以50起提。且每周只能发起一次申请</h1>
            </dd>
        </dl>
        <ul class="MyPresentX">
            <%--  <li>
                <h3>申请提现金额：</h3>
                <input name="" type="tel" placeholder="只可提取整数部分学分" id="ordermoney" onkeyup="value=value.replace(/[^\d]/g,'')" onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))" />
            </li>--%>
            <%--           <li>
                <h3>申请提现金额：</h3>
                <input name="" type="text"  id="ordermoney"  readonly="true"/>
            </li>--%>
            <li>
                <h3>开户银行：</h3>
                <input name="" type="text" value="" readonly="true" id="pbname" />
            </li>
            <li>
                <h3>姓名：</h3>
                <input name="" type="text" value="" readonly="true" id="pname" />
            </li>
            <li>
                <h3>银行账号：</h3>
                <input name="" type="text" value="" readonly="true" id="pkahao" />
            </li>
            <li>
                <h3>开户行所在地：</h3>
                <input name="" type="text" value="" readonly="true" id="pzhname" />
            </li>
            <li>
                <input name="" type="button" value="确认" onclick="applyMoney()" />
            </li>
        </ul>
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




                //时间控制

                //var myDate = new Date();
                //var today = myDate.getDate();

                ////if (true) {
                //if (today == 27 || today == 28) {
                //    $('input[name=btnapply]').removeAttr("disabled");
                //    $('#btntx').css('display', '');
                //}
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