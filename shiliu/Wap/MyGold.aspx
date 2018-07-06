<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyGold.aspx.cs" Inherits="Wap_MyGold" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="MobileOptimized" content="240" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <title>学分转购物币</title>
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/sub.css" />
    <script type="text/javascript">
        $(function () {
            $('.tab2 ul.menu li').click(function () {
                //获得当前被点击的素索引值
                var Index = $(this).index();
                //给菜单添加选择样式
                $(this).addClass('active').siblings().removeClass('active');
                //显示对应的div
                $('.tab2').children('div').eq(Index).show().siblings('div').hide();

            });




        });
        //找找是否有这个用户
        function zhuanbi() {
            var memberid = $("#memberid").val().trim();
            if (memberid == undefined || memberid == "") {
                alert('请输入编号！');
                return false;
            }

            $.ajax({
                type: "post",
                async: false,
                cache: false,
                url: "Handler.ashx",
                dataType: "json",
                data: {
                    "Method": "IsMemberExitByCode",
                    "MemberCode": memberid
                },
                success: function (data) {
                    if (data) {
                        var lg = window.localStorage.getItem("LoginUserId");
                        if (lg != null && lg != "") {
                            window.location.href = "MyGoldNext.aspx?code=" + memberid + "&uid=" + lg;
                        }
                    } else {
                        alert('未找到相关用户');
                    }
                },
                error: function () {
                    alert('连接超时');
                }

            });
        }

    </script>
</head>
<body aria-atomic="False" style="background: #f0eff5;">
    <div class="inContent">
        <div class="tab2">
            <ul class="menu">
                <li class="active">学分转学习币</li>
                <li>记录</li>
            </ul>
            <div class="con1">
                <dl class="MyGold">
                    <dd><span>好友编号：</span><input type="tel" name="" id="memberid" placeholder="输入学员编号" /></dd>
                    <dt>
                        <input type="button" name="" value="下一步" class="cur" onclick="zhuanbi()" /></dt>
                </dl>
            </div>
            <div class="con2">
                <dl class="MyGoldTwo">
                    <%=fromstr %>              
                </dl>
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

<script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js" type="text/javascript"></script>
<script type="text/javascript">
    document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
        // 通过下面这个API隐藏右上角按钮hideOptionMenu
        WeixinJSBridge.call('hideOptionMenu');
    });

</script>