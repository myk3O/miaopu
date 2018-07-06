<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductShow.aspx.cs" Inherits="Wap_ProductShow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="MobileOptimized" content="240" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <title><%=title %></title>
    <script src="Js/jquery.min.js" type="text/javascript"></script>

    <link rel="stylesheet" href="css/sub.css" />
</head>
<body aria-atomic="False" style="background: #eeeeee;">
    <input type="hidden" runat="server" id="hidagent" />
    <input type="hidden" runat="server" id="hiduser" />
    <div class="subProShowT">
        <%-- <img src="Img/sub_02.jpg" />--%>

        <div id="video" class="container" style="margin: 20px auto 20px auto">
            <div id="a1">
            </div>
        </div>
    </div>

    <script type="text/javascript" src="../js/offlights.js"></script>
    <script type="text/javascript" src="../ckplayer/ckplayer.js" charset="utf-8"></script>

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


        $(function () {
            var agent = window.localStorage.getItem("agent") == null ? "0" : window.localStorage.getItem("agent");
            var ProId = $.getUrlParam('pid');
            var isFree = false;
            var divstr = "";
            var loginUser = window.localStorage.getItem("LoginUserId"); //先看缓存

            if (loginUser == null || loginUser == "") {//回传还是没有

                window.location.href = "UserAuth.ashx"; //重新授权获取

            } else {
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
                            divstr = "<abbr>您已经购买过该视频了<input type='button' value='VIP' /></abbr>";
                        } else {
                            var url = "Pay.aspx?uid=" + loginUser + "&pid=" + ProId + "&agent=" + agent;
                            divstr = "<abbr>购买之后才能观看该视频<input type='button' value='我要购买' onclick=\"window.location.href='" + url + "' \" /></abbr>";
                        }
                        $("#divstr").empty();
                        $("#divstr").append(divstr);
                    },
                    error: function () {
                        alert('连接超时');
                    }

                });
            }

            ///视频播放控件
            var url;
            if (isFree) {
                url = 'http://v.chinesecom.cn/1.mp4';
            } else {
                url = '';
                alert('请先购买');
            }

            var flashvars = {
                f: url,
                c: 0,
                loaded: 'loadedHandler',
                p: 0,
                e: 0
            };
            var video = [url];
            CKobject.embed('../ckplayer/ckplayer.swf', 'a1', 'ckplayer_a1', '100%', '100%', true, flashvars, video);


        });



    </script>

    <div class="inContent">

        <div id="memberList">

            <div class="StylistZ" id="divstr">
                <abbr>
                    只需要支付<%=price %>,就能永久播放该视频
                <input type="button" value="立即支付" id="btnhuiyuan" onclick="ordertj();" /></abbr>
            </div>
        </div>

    </div>
    <div class="inContent">
        <div id="mcover" onclick="document.getElementById('mcover').style.display='';" style="">
            <img src="img/guide.png">
        </div>
        <dl class="subProd">
            <%--            <dd>夏季清凉水果美甲<span><img src="Img/sub_03.jpg" onclick="document.getElementById('mcover').style.display = 'block';" /></span></dd>
            <dt>又到周一啦，今天小编给大家分享一个夏季清凉水果美甲， 有好多可爱的水果图案，是不是很可爱很特别呢？ 告诉小编你最喜欢哪一款吧！ 对了，我们微信公众号上也可以看视频了哦， 手机上更加方便不用再去找啦， 关注“111”，就可以看视频了哦~~~</dt>--%>
            <%=tmemo %>
        </dl>
        <dl class="subProd01">
            <dd><%--onclick="document.getElementById('mcover').style.display = 'block';"--%>
                <abbr><%=didcount %></abbr>次点播</dd>
        </dl>
        <div class="subProd02">
            <a href="Product.aspx">更多 视频 <i class="fa fa-angle-right"></i></a>
        </div>
        <dl class="subProd03">
            <dt>相关视频</dt>
            <%=nearVideo %>
            <%--     <dd>
                <img src="Img/sub_01.jpg" />
                <h1>梦幻美甲宝盒 - 广告</h1>
                <h3>夏季清凉水果美甲，有好多可爱的水果图案</h3>
                <h4>2882.4万次播放</h4>
            </dd>--%>
            <%--    <dd>
                <img src="Img/sub_01.jpg" />
                <h1>梦幻美甲宝盒 - 广告</h1>
                <h3>夏季清凉水果美甲，有好多可爱的水果图案</h3>
                <h4>2882.4万次播放</h4>
            </dd>--%>
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
        // 通过下面这个API隐藏右上角按钮
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
            link: 'http://www.missbeauty.net.cn/Wap/Index.aspx?agent=' + u, // 分享链接
            imgUrl: 'http://www.missbeauty.net.cn/Wap/Img/share.jpg', // 分享图标
            desc: '快来看看小伙伴推荐的视频吧', // 分享描述
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
            link: 'http://www.missbeauty.net.cn/Wap/Index.aspx?agent=' + u, // 分享链接
            imgUrl: 'http://www.missbeauty.net.cn/Wap/Img/share.jpg', // 分享图标

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
            link: 'http://www.missbeauty.net.cn/Wap/Index.aspx?agent=' + u, // 分享链接
            imgUrl: 'http://www.missbeauty.net.cn/Wap/Img/share.jpg', // 分享图标
            desc: '快来看看小伙伴推荐的视频吧', // 分享描述         
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
