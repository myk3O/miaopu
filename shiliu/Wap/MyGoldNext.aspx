<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyGoldNext.aspx.cs" Inherits="Wap_MyGoldNext" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="MobileOptimized" content="240" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <title>学分转赠</title>
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/sub.css" />
    <script type="text/javascript">
        (function ($) {
            $.getUrlParam = function (name) {
                var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                var r = window.location.search.substr(1).match(reg);
                if (r != null) return unescape(r[2]); return null;
            }
        })(jQuery);


        $(function () {
            $('.tab2 ul.menu li').click(function () {
                //获得当前被点击的素索引值
                var Index = $(this).index();
                //给菜单添加选择样式
                $(this).addClass('active').siblings().removeClass('active');
                //显示对应的div
                $('.tab2').children('div').eq(Index).show().siblings('div').hide();

            });

            var code = $.getUrlParam("code"); //好友code
            var lg = window.localStorage.getItem("LoginUserId");
            if (code != null && code != "") {
                $.ajax({
                    type: "post",
                    async: false,
                    cache: false,
                    url: "Handler.ashx",
                    dataType: "json",
                    data: {
                        "Method": "GetMemberByCode",
                        "code": code
                    },
                    success: function (data) {
                        if (data != null && data != "") {
                            $("#MyGoldNextTList").empty();
                            $("#MyGoldNextTList").append(data);
                        }
                    },
                    error: function () {
                        alert('连接超时');
                    }

                });
            }
        });


        function ZhuanBi() {
            var touid = $('#ToUserID').text();
            var tmemo = $('#txtMemo').val();
            var loginUser = window.localStorage.getItem("LoginUserId");
            if (loginUser == null || loginUser == "") {
                window.location.href = "Login.aspx";
            }
            if (touid == loginUser) {
                alert('不能转赠自己');
                return false;
            }
            var canMoney = $('#hidpri').val();
            var money = parseInt((parseFloat(canMoney)).toFixed(0));
            var zhuanMoney = $('#txtMoney').val().trim();
            if (zhuanMoney == "") {
                alert('请输入转赠学分');
                return false;
            }
            var zmoney = parseInt((parseFloat(zhuanMoney) * 100).toFixed(0));

            if (zmoney > money) {//想转出大于可转出
                alert('超出可转赠学分');
                return false;
            }
            if (zmoney == 0 || zmoney % 5000 != 0) {//不是50的倍数
                alert('需大于0且只能为50的整数倍');
                return false;
            }

            if (confirm('确认转赠？')) {
                $.ajax({
                    type: "post",
                    async: false,
                    cache: false,
                    url: "Handler.ashx",
                    dataType: "json",
                    data: {
                        "Method": "ApplyZhuanBi",
                        "fromUid": loginUser,
                        "toUid": touid,
                        "tMemo": tmemo,
                        "zMoney": zmoney
                    },
                    success: function (e) {
                        if (e) {
                            alert('转赠成功');
                            window.location.href = "MyGold.aspx?uid=" + loginUser;
                        } else {
                            alert('转赠失败');
                        }

                    },
                    error: function () {
                        alert('连接超时');
                    },
                });
            }


        }
    </script>
</head>
<body aria-atomic="False" style="background: #f0eff5;">
    <input type="hidden" runat="server" name="txtyj" id="hidpri" />
    <div class="inContent">
        <dl class="MyGoldNextT">
            <dd id="MyGoldNextTList">
                <img />
                <h1></h1>
                <p></p>
            </dd>
        </dl>
        <dl class="MyGoldNext">
            <dd><span>额度：</span><input type="tel" name="" id="txtMoney" placeholder="只能为50的整数倍" onkeyup="value=value.replace(/[^\d]/g,'')" /></dd>
            <dd><span>备注：</span><input type="text" name="" id="txtMemo" maxlength="20" placeholder="20个字以内" /></dd>
            <dt>
                <input type="button" name="" value="确认转出" class="cur" onclick="ZhuanBi()" /><span>可转币学分（<%=CanGetPri %>）</span></dt>
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
