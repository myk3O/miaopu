<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VideoShow.aspx.cs" Inherits="Wap_VideoShow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="MobileOptimized" content="240" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <title>视频播放</title>
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/sub.css" />
</head>
<body aria-atomic="False" style="background: #eeeeee;">
    <script type="text/javascript" src="../js/offlights.js"></script>
    <script type="text/javascript" src="../ckplayer/ckplayer.js" charset="utf-8"></script>
    <dl class="subProd">
        <dd><%=TypeName %> <%--<span>
                <img src="Img/sub_03.jpg" onclick="document.getElementById('mcover').style.display = 'block';" /></span>--%></dd>
        <dt><%=Vdiscrib %></dt>
    </dl>
    <div class="subProShowT">
        <div id="video" class="container" style="margin: 20px auto 20px auto">
            <div id="a1">
            </div>
        </div>
    </div>


    <script type="text/javascript">

        ///视频播放控件
        function loadedHandler() {
            if (CKobject.getObjectById('ckplayer_a1').getType()) {//说明使用html5播放器
                //alert('播放器已加载，调用的是HTML5播放模块');
            }
            else {
                // alert('播放器已加载，调用的是Flash播放模块');
            }
        }
        (function ($) {
            $.getUrlParam = function (name) {
                var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                var r = window.location.search.substr(1).match(reg);
                if (r != null) return unescape(r[2]); return null;
            }
        })(jQuery);

        var isFree = false;
        var divstr = "";

        $(function () {

            //如果url里面有agent,则是分享过来的
            var ProId = $.getUrlParam('pid');
            var agent = $.getUrlParam("agent");
            if (agent == null || agent == "" || agent == "null") {
                window.localStorage.setItem("VideoUrl", "");//不是分享过来的就清空
                agent = window.localStorage.getItem("agent");
                if (agent == null || agent == "" || agent == "null") {
                    agent = 0;
                }
            } else {

                var backurl = "http://tv.gongxue168.com/Wap/VideoShow.aspx?pid=" + ProId;
                window.localStorage.setItem("VideoUrl", backurl);//分享过来的就存起来
            }

            window.localStorage.setItem("agent", agent);

            var loginUser = window.localStorage.getItem("LoginUserId"); //先看缓存
            if (loginUser == null || loginUser == "") {//回传还是没有
                window.location.href = "UserAuth.ashx?agent=" + agent; //重新授权获取
            } else {

                $.ajax({
                    type: "post",
                    async: false,
                    cache: false,
                    url: "Handler.ashx",
                    dataType: "json",
                    data: {
                        "Method": "GetMemberBynID",
                        "userID": loginUser
                    },
                    success: function (data) {
                        if (data != null && data != "") {
                            getvideo(loginUser, ProId, agent);
                        } else {
                            window.localStorage.setItem("LoginUserId", "");
                            window.location.href = "UserAuth.ashx"; //重新授权获取
                        }
                    },
                    error: function () {
                        alert('连接超时');
                    }

                });

            }



            ///视频播放控件
            var url;
            if (isFree) {
                url = '<%=Url%>';
        } else {
            url = '';
                // alert('请先购买');
        }

            var flashvars = {
                i: 0,
                c: 0,
                p: 0,
                e: 0,
                loaded: 'loadedHandler',
            };
            var support = ['all'];
            var video = [url];
            CKobject.embedHTML5('video', 'ckplayer_a1', '100%', '100%', video, flashvars, support);
        });


    function getvideo(loginUser, ProId, agent) {
        window.localStorage.setItem("LoginUserId", loginUser); //存缓存
        $.ajax({
            type: "post",
            async: false,
            cache: false,
            url: "Handler.ashx",
            dataType: "json",
            data: {
                "Method": "IsForFree",
                "userID": loginUser,
                "proID": ProId
            },
            success: function (data) {
                isFree = data;
                if (isFree) {
                    $('#foot').show();
                    $('#coufoot').hide();
                } else {
                    $('#foot').hide();
                    $('#coufoot').show();
                    var url = "OrderHandling.aspx?uid=" + loginUser + "&pid=" + ProId + "&agent=" + agent;
                    divstr = "<input type='button' value='立即购买' onclick=\"window.location.href='" + url + "' \" />"

                }
                $("#divstr").empty();
                $("#divstr").append(divstr);
            },
            error: function () {
                alert('连接超时');
            }

        });
    }
    </script>
    <div class="inContent">
        <div id="mcover" onclick="document.getElementById('mcover').style.display='';" style="">
            <img src="img/guide.png">
        </div>

        <dl class="subProd01">
            <dd>
                <abbr><%=didcount %></abbr>次点播<span><b><%=price %></b></span></dd>
        </dl>

        <dl class="subProd04">
            <%=nearVideo %>
            <%--           <dd>
                <img src="Img/ind_05.jpg" />1、朋友圈的...</dd>
            <dd>
                <img src="Img/ind_05.jpg" />2、微电商叫...</dd>
            <dd>
                <img src="Img/ind_05.jpg" />3、通过微信...</dd>
            <dd>
                <img src="Img/ind_05.jpg" />4、平台微商...</dd>
            <dd>
                <img src="Img/ind_05.jpg" />5、朋友圈的...</dd>
            <dd>
                <img src="Img/ind_05.jpg" />6、微电商叫...</dd>--%>
        </dl>
        <dl class="subCourse">
            <dt>讲师介绍</dt>
            <dd>
                <%=teacherMemo %>
                <%-- <img src="img/Styl_01.png" />
                <h1><span>方雨</span>  WeMedia联合创始人</h1>
                <h2>曾在羊城晚报担任记者。8年电商操盘经历，曾任博爱医疗集团网络总监、真购网副总裁、帕...<a href="teacher.aspx ">详情 ></a></h2>--%>
            </dd>
        </dl>
        <dl class="subCourse01">
            <dt>课程介绍</dt>
            <dd><%=tmemo %>
            </dd>
        </dl>
        <%--  <dl class="subCourse02">

              <dt>课时1微商的四种销售模式</dt>
            <dd><a href="VideoShow.aspx?">1.1朋友圈的微商销售</a></dd>
            <dd><a href="VideoShow.aspx?">1.2微电商叫微分销销售</a></dd>
            <dd><a href="VideoShow.aspx?">1.3通过微信售卖服务</a></dd>
            <dd><a href="VideoShow.aspx?">1.4平台微商销售</a></dd>
        </dl>--%>
    </div>
    <div class="FootbqK"></div>
    <div class="CouFooter" id="coufoot">
        <ul>
            <li><a href="Index.aspx">
                <img src="Img/foot_01.png" />
                <p>首页</p>
            </a></li>
            <li id="divstr">
                <input type="button" value="立即购买" onclick="window.location.href = 'OrderHandling.aspx?pid='" /></li>
        </ul>
    </div>
    <div class="Footer" id="foot">
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
<script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js" type="text/javascript"></script>
<script type="text/javascript">

    var u = window.localStorage.getItem("LoginUserId");

    wx.config({
        debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
        appId: '<%= WeiPay.PayConfig.AppId %>', // 必填，公众号的唯一标识
        timestamp: '<%= WeiPay.TenpayUtil.getTimestamp() %>', // 必填，生成签名的时间戳
        nonceStr: '<%= WeiPay.TenpayUtil.getNoncestr() %>', // 必填，生成签名的随机串
        signature: '<%=Get_signature()%>',// 必填，签名，见附录1
        jsApiList: [
             'getNetworkType',
             'checkJsApi',  //判断当前客户端版本是否支持指定JS接口
             'onMenuShareTimeline', //分享给好友
             'onMenuShareAppMessage', //分享到朋友圈
             'onMenuShareQQ',  //分享到QQ
             'onMenuShareWeibo' //分享到微博
        ] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
    });
    document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
        // 通过下面这个API隐藏右上角按钮hideOptionMenu
        WeixinJSBridge.call('showOptionMenu');
    });


    wx.ready(function () {

        // 1 判断当前版本是否支持指定 JS 接口，支持批量判断

        wx.checkJsApi({
            jsApiList: [
              'getNetworkType',
              'checkJsApi',
              'onMenuShareTimeline'
            ],
            success: function (res) {
                //alert(JSON.stringify(res));
            }
        });

        // config信息验证后会执行ready方法，所有接口调用都必须在config接口获得结果之后，
        //config是一个客户端的异步操作，所以如果需要在页面加载时就调用相关接口，
        //则须把相关接口放在ready函数中调用来确保正确执行。对于用户触发时才调用的接口，
        //则可以直接调用，不需要放在ready函数中。

        //获取网络状态接口
        wx.getNetworkType({
            success: function (e) {
                var networkType = e.networkType; // 返回网络类型2g，3g，4g，wifi
                if (e.networkType != "wifi") {
                    alert("检查到您现在在非WiFi状态下观看视频，请注意流量");
                }
            }
        });

        // 2. 分享接口
        // 2.1 监听“分享给朋友”
        wx.onMenuShareAppMessage({
            title: '<%=title %>', // 分享标题
            link: '<%=link%>' + u, // 分享链接
            imgUrl: '<%=imgUrl%>', // 分享图标
            desc: '<%=TypeName %>', // 分享描述
            success: function () {
                //alert('已分享');
            },
            cancel: function () {
                //alert('已取消');
            },
        });
        // “分享到朋友圈”
        wx.onMenuShareTimeline({
            title: '<%=title %>', // 分享标题
            link: '<%=link%>' + u, // 分享链接
            imgUrl: '<%=imgUrl%>', // 分享图标

            success: function () {
                // alert('已分享');
            },
            cancel: function () {
                // alert('已取消');
            },
            fail: function (res) {
                //alert(JSON.stringify(res));
            }
        });
        //获取“分享到QQ”按钮点击状态及自定义分享内容接口
        wx.onMenuShareQQ({
            title: '<%=title %>', // 分享标题
            link: '<%=link%>' + u, // 分享链接
            imgUrl: '<%=imgUrl%>', // 分享图标
            desc: '<%=TypeName %>', // 分享描述         
            success: function () {
                // 用户确认分享后执行的回调函数
            },
            cancel: function () {
                // 用户取消分享后执行的回调函数
            }
        });
    });
    wx.error(function (res) {
        alert(res.errMsg);  //打印错误消息。及把 debug:false,设置为debug:ture就可以直接在网页上看到弹出的错误提示
    });



</script>
</html>
