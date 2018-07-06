<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stylist.aspx.cs" Inherits="Wap_Stylist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="MobileOptimized" content="240" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <title>微店管理</title>
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/sub.css" />
    <script type="text/javascript">
        $(document).ready(function () {

            $("#firstpane .menu_body:eq(0)").show();
            $("#firstpane h3.menu_head").click(function () {
                $(this).addClass("current").next("div.menu_body").slideToggle(300).siblings("div.menu_body").slideUp("slow");
                $(this).siblings().removeClass("current");
            });

            $("#secondpane .menu_body:eq(0)").show();
            $("#secondpane h3.menu_head").mouseover(function () {
                $(this).addClass("current").next("div.menu_body").slideDown(500).siblings("div.menu_body").slideUp("slow");
                $(this).siblings().removeClass("current");
            });

        });
    </script>
    <script>
        (function ($) {
            $.getUrlParam = function (name) {
                var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                var r = window.location.search.substr(1).match(reg);
                if (r != null) return unescape(r[2]); return null;
            }
        })(jQuery);

        $(function () {
            var loginUser = window.localStorage.getItem("LoginUserId"); //先看缓存

            if (loginUser == null || loginUser == "") {

                window.location.href = "UserAuth.ashx"; //重新授权获取

            } else {
                window.localStorage.setItem("LoginUserId", loginUser); //存缓存
                //$('#hiduser').val(loginUser);
            }

            $.ajax({
                type: "post",
                async: false,
                cache: false,
                url: "Handler.ashx",
                dataType: "json",
                data: {
                    "Method": "GetMemberBynIDList",
                    "userID": loginUser
                },
                success: function (data) {
                    if (data != null && data != "") {
                        //alert(data);
                        $("#memberList").empty();
                        $("#memberList").append(data);
                    }
                },
                error: function () {
                    alert('连接超时');
                }

            });


        });
    </script>
</head>
<body aria-atomic="False">
    <input type="hidden" runat="server" id="hiduser" />
    <div class="inContent">
        <div id="mcover2" onclick="document.getElementById('mcover2').style.display='';" style="">
            <img src="img/guide1.png">
        </div>
        <div class="StylistT">
            <div id="memberList">
                <img src="Img/Styl_01.png" />
                <h1>昵称：共学用户</h1>
            </div>
            <h2>可提现佣金：<span style="color: red; font-size: 15px"><%=CanGetPri %></span>元</h2>
            <h2>提现中佣金：<span style="color: red; font-size: 15px"><%=getingPri %></span>元</h2>
        </div>
        <div class="StylistZ"><span>累计销售：<%= soldPri%>元</span><span>已返佣金：<%=getPri %>元</span></div>
        <div id="firstpane" class="menu_list">
            <h3 class="menu_head current">我的会员<span><%=allUser %>人</span></h3>
            <div style="display: block" class="menu_body">
                <a href="MemberOne.aspx?id=1&uid=<%= nid%>&ct=<%=OneLeaveUser %>">一级会员<span><%=OneLeaveUser %>人<i class="fa fa-angle-right"></i></span></a>
                <a href="MemberOne.aspx?id=2&uid=<%= nid%>&ct=<%=TwoLeaveUser %>">二级会员<span><%=TwoLeaveUser %>人<i class="fa fa-angle-right"></i></span></a>
                <a href="MemberOne.aspx?id=3&uid=<%= nid%>&ct=<%=ThreeLeaveUser %>">三级会员<span><%=ThreeLeaveUser %>人<i class="fa fa-angle-right"></i></span></a>
            </div>
            <h3 class="menu_head">我的推广<span><%=allOrder %>单</span></h3>
            <div style="display: none" class="menu_body">
                <a href="#">一级推广<span><%=leave1OrderCount %>单</span></a>
                <a href="#">二级推广<span><%=leave2OrderCount %>单</span></a>
                <a href="#">三级推广<span><%=leave3OrderCount %>单</span></a>
            </div>
            <h3 class="menu_head">我的佣金<span><%=allMypri %>元</span></h3>
            <div style="display: none" class="menu_body">
                <a href="#">一级佣金<span><%=leave1Pri %>元</span></a>
                <a href="#">二级佣金<span><%=leave2Pri %>元</span></a>
                <a href="#">三级佣金<span><%=leave3Pri %>元</span></a>
            </div>
        </div>
        <input type="hidden" runat="server" name="txtyj" id="hidpri" />
        <div class="StylistZ">
            <abbr style="width: 58%; display: none" id="btntx">
                <input name="btnapply" type="button" value="申请提现" onclick="applyMoney() " disabled="disabled" /></abbr>
            <li style="text-align: center">
                <p>
                    ———————— 温馨小提示 ————————
                </p>
                <h6>每月的27，28号为公司出账日，
                    才能显示提现按钮，请需要<br />
                    提取佣金的用户在这2天提交申请</h6>
                <br />
            </li>
        </div>
    </div>

    <div class="FootbqK"></div>
    <div class="Footer">
        <script>
            $(function () {
                var lg = window.localStorage.getItem("LoginUserId");
                if (lg != null && lg != "") {
                    var text = "<a href='Stylist.aspx?uid=" + lg + "'>";
                    text += " <img src='Img/foot_02.jpg' />";
                    text += "<p>微店管理</p></a>";
                    $("#footer").empty();
                    $("#footer").html(text);
                }



                //时间控制

                var myDate = new Date();
                var today = myDate.getDate();

                //if (true) {
                if (today == 27 || today == 28) {
                    $('input[name=btnapply]').removeAttr("disabled");
                    $('#btntx').css('display', '');
                }

            });

            function doMoney(u) {
                var yongjin = $('#hidpri').val();
                var money = (parseFloat(yongjin) * 100).toFixed(0);
                if (money < 1) {//小于1分钱
                    alert('可提现佣金必需大于1分钱');
                    return false;
                }

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
                            window.location.reload();
                        } else {
                            alert('申请失败');
                        }

                    },
                    error: function () {
                        alert('连接超时');
                    },
                });
            }

            function applyMoney() {
                var loginUser = window.localStorage.getItem("LoginUserId");
                if (loginUser == null || loginUser == "") {
                    //MyChangePage("Person/Individual.html");
                    window.location.href = "Login.aspx";
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
        <ul>
            <li><a href="index.aspx" class="cur">
                <img src="Img/foot_01.jpg" />
                <p>我的微店</p>
            </a></li>
            <li id="footer"><a href="Stylist.aspx">
                <img src="Img/foot_02.jpg" />
                <p>微店管理</p>
            </a></li>
            <li><a href="Login.aspx">
                <img src="Img/foot_03.jpg" />
                <p>学习中心</p>
            </a></li>
        </ul>
    </div>
</body>
</html>

<script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js" type="text/javascript"></script>
<script type="text/javascript">
    document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
        // 通过下面这个API隐藏右上角按钮
        WeixinJSBridge.call('hideOptionMenu');
    });

</script>
