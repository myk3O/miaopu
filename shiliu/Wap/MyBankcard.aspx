<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyBankcard.aspx.cs" Inherits="Wap_MyBankcard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="MobileOptimized" content="240" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <title>银行卡信息管理</title>
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

            $.ajax({
                type: "post",
                async: false,
                cache: false,
                url: "Handler.ashx",
                dataType: "json",
                data: { "Method": "GetProvince" },
                success: function (Province) {
                    if (Province != null && Province != "") {
                        $('select[name=shop_province]').prepend(Province);
                    }
                },
                error: function () {
                    alert('连接超时');
                }
            });
            $('select[name=shop_province]').change(function (obj) {
                var obj = $(this);
                var region_id = $(this).val();
                var text = obj.find("option:selected").text();
                $('input[name=aarea]').val(text); //存放省
                $('input[name=aprovince]').val(region_id); //存放省code
            });

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
                        if (data != null && data != "") {
                            $("#pbname").val(data.TypeName);
                            $("#pname").val(data.Huming);
                            $("#pkahao").val(data.Zhanghao);
                            $("#pzhname").val(data.Kaihu);
                            $('input[name=aprovince]').val(data.Code); //存放省code
                            $('select[name=shop_province]').val(data.Code);
                            $('input[name=aarea]').val(data.Address); //存放省
                        }
                        $("#pbname").val('中国农业银行');
                    },
                    error: function () {
                        alert('连接超时');
                    }

                });
            }
        });


        function bSave() {
            loginUser = window.localStorage.getItem("LoginUserId"); //先看缓存
            var TypeName = $("#pbname").val().trim();
            if (TypeName == undefined || TypeName == "") {
                alert('银行名称不能空！');
                return false;
            }
            var Huming = $("#pname").val().trim();
            if (Huming == undefined || Huming == "") {
                alert('持卡人姓名不能空！');
                return false;
            }
            var Zhanghao = $("#pkahao").val().trim();
            Zhanghao = Zhanghao.replace(/\s+/g, "");
            if (Zhanghao == undefined || Zhanghao == "") {
                alert("银行卡号不能空！");
                return false;
            }


            var code = $('input[name=aprovince]').val();
            var dizhi = $('input[name=aarea]').val();
            if (dizhi == undefined || dizhi == "" || code == "" || code == "") {
                alert("开户行所在地不能空！");
                return false;
            }

            var Kaihu = $("#pzhname").val().trim();
            //if (Kaihu == undefined || Kaihu == "") {
            //    alert("银行开户行不能空！");
            //    return false;
            //}

            $.ajax({
                type: "post",
                async: false,
                cache: false,
                url: "Handler.ashx",
                dataType: "json",
                data: {
                    "Method": "UpdateBankCartByCid",
                    "userID": loginUser,
                    "TypeName": TypeName,
                    "Huming": Huming,
                    "Zhanghao": Zhanghao,
                    "Kaihu": Kaihu,
                    "Code": code,
                    "Address": dizhi
                },
                success: function (e) {
                    if (e) {

                        var fg = $.getUrlParam("jp"); //是否是申请提现页面跳转过来的
                        if (fg == null || fg == "") {
                            window.location.href = "MyLogin.aspx?uid=" + loginUser;
                        } else {
                            window.location.href = "MyPresentShow.aspx?uid=" + loginUser;
                        }
                    } else {
                        alert('修改失败');
                    }
                },
                error: function () {
                    alert('连接超时');
                }

            });

        }

    </script>
</head>
<body>
    <div class="inContent">
        <input type="hidden" value="" name="aarea" />
        <input type="hidden" value="" name="aprovince" />
        <div class="StylistT" style="text-align: center">
            <%--     <img src="Img/Styl_01.png" />
            <h1>昵称：东方红</h1>
            <h2>注册日期：2015-08-21</h2>--%>
            为了保证您的兑换质量，以下所有内容必须认真填写<br />
            目前仅限《中国农业银行》
        </div>
        <ul class="MyBankcard">
            <li>
                <h3>银行名称：</h3>
                <input name="" type="text" value="中国农业银行" readonly="true" id="pbname" placeholder="中国农业银行" />
            </li>
            <li>
                <h3>持卡人姓名：</h3>
                <input name="" type="text" value="" id="pname" placeholder="持卡人姓名" />
            </li>
            <li>
                <h3>银行卡号：</h3>
                <input name="" type="tel" value="" id="pkahao" placeholder="不支持存折" />
            </li>
            <li>
                <h3>开户行所在地：</h3>
                <select class="MyProfileSel" name="shop_province">
                </select>
            </li>
            <li style="display: none">
                <h3>银行开户行：</h3>
                <input name="" type="text" value="" id="pzhname" placeholder="上海宝山支行" />

            </li>
            <li>
                <input name="" type="button" value="保存信息" onclick="bSave()" />
            </li>
            <%--   <li>
                <input type="button" name="" value="返回" onclick="window.location.href = 'javascript:history.go(-1);'" />
            </li>--%>
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