<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyProfileEdit.aspx.cs" Inherits="Wap_MyProfileEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="MobileOptimized" content="240" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <title>编辑资料 </title>
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
            //$.ajax({
            //    type: "post",
            //    async: false,
            //    cache: false,
            //    url: "Handler.ashx",
            //    dataType: "json",
            //    data: { "Method": "GetProvince" },
            //    success: function (Province) {
            //        if (Province != null && Province != "") {
            //            $('select[name=shop_province]').prepend(Province);
            //        }
            //    },
            //    error: function () {
            //        alert('连接超时');
            //    }
            //});
            //$('select[name=shop_province]').change(function (obj) {
            //    var obj = $(this);
            //    var region_id = $(this).val();
            //    $('select[name=shop_city]').html('<option value="">请选择</option>');
            //    $('select[name=district]').html('<option value="">请选择</option>');

            //    var text = obj.find("option:selected").text();
            //    var str = $('input[name=area]').val();
            //    var arr = str.split('-');
            //    $('input[name=area]').val(text + '-' + arr[1] + '-' + arr[2]);
            //    $('input[name=aprovince]').val(region_id); //存放省code
            //    $('input[name=acity]').val(''); //清空市code
            //    $('input[name=aarea]').val(''); //清空区域code
            //    SetShi(region_id);
            //});
            //$('select[name=shop_city]').change(function (obj) {
            //    var obj = $(this);
            //    var region_id = $(this).val();
            //    $('select[name=district]').html('<option value="">请选择</option>');
            //    var text = obj.find("option:selected").text();
            //    var str = $('input[name=area]').val();
            //    var arr = str.split('-');
            //    $('input[name=area]').val(arr[0] + '-' + text + '-' + arr[2]);

            //    $('input[name=acity]').val(region_id); //存放市code
            //    $('input[name=aarea]').val(''); //清空区域code
            //    Setarea(region_id);
            //});
            //$('select[name=district]').change(function (obj) {
            //    var obj = $(this);
            //    var region_id = $(this).val();

            //    var text = obj.find("option:selected").text();

            //    var str = $('input[name=area]').val();
            //    var arr = str.split('-');
            //    $('input[name=area]').val(arr[0] + '-' + arr[1] + '-' + text);
            //    $('input[name=aarea]').val(region_id); //存放区域code
            //});


            loginUser = window.localStorage.getItem("LoginUserId"); //先看缓存
            if (loginUser != null || loginUser != "") {
                $.ajax({
                    type: "post",
                    async: false,
                    cache: false,
                    url: "Handler.ashx",
                    dataType: "json",
                    data: {
                        "Method": "GetMemberObjectBynID",
                        "userID": loginUser
                    },
                    success: function (data) {
                        if (data != null && data != "") {
                            $("#pname").val(data.tRealName);
                            $("#idcard").val(data.MemberPass);
                            // $("#paddr").val(data.MemberPass);
                            $("#pphone").val(data.MemberPhone);
                            $("#pqq").val(data.MemberEmail);
                        }
                    },
                    error: function () {
                        alert('连接超时');
                    }

                });



                //$.ajax({
                //    url: 'Handler.ashx',
                //    type: 'post',
                //    data: { "Method": "GetAddrDetail", "userID": loginUser },
                //    dataType: 'json',
                //    success: function (e) {
                //        if (e != null && e != "") {
                //            $('input[name=aprovince]').val(e.Sheng); //存放省code
                //            $('input[name=acity]').val(e.Shi); //存放市code
                //            $('input[name=aarea]').val(e.area); //存放区域code
                //            $('input[name=area]').val(e.YouBian); //存放YouBian
                //            $('input[name=tujing]').val(e.PeopleName);//获取途径
                //            $('input[name=job]').val(e.Phone);//职业
                //            $('select[name=shop_province]').val(e.Sheng);
                //            $('#paddr').val(e.YouBian); //存放地址
                //            SetShi(e.Sheng);
                //            Setarea(e.Shi);
                //            var arr = e.YouBian.split('-');
                //            //$('select[name=shop_city]').val(e.Shi);
                //            $('select[name=shop_city]').html('<option value=' + e.Shi + '>' + arr[1] + '</option>');
                //            //$('select[name=district]').val(e.area);
                //            $('select[name=district]').html('<option value=' + e.area + '>' + arr[2] + '</option>');
                //            // $("select[name=shop_city option[text='大庆市']").attr("selected", true);   //设置Select的Text值为jQuery的项选中
                //            $("select[name=tujing] option:contains('" + e.PeopleName + "')").attr('selected', true);
                //            //$('select[name=job]').find("option[text='" + e.Phone + "']").attr("selected", true);
                //            $("select[name=job] option:contains('" + e.Phone + "')").attr('selected', true);
                //        }
                //    }
                //})
            }
        });


        function Save() {
            loginUser = window.localStorage.getItem("LoginUserId"); //先看缓存
            var userName = $("#pname").val().trim();
            if (userName == undefined || userName == "") {
                alert('真实姓名不能空！');
                return false;
            }


            var idcard = $("#idcard").val().trim();
            if (idcard == undefined || idcard == "") {
                alert('身份证号码不能为空！');
                return false;
            }
            var isIDCard2 = /^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$/;
            if (idcard != "" && !isIDCard2.test(idcard)) {
                alert("身份证号码格式不正确！");
                return false;
            }

            var userPhone = $("#pphone").val().trim();
            if (userPhone == undefined || userPhone == "") {
                alert('手机号不能空！');
                return false;
            }
            var regPartton = /^1[3|4|5|7|8][0-9]\d{8}$/;
            if (userPhone != "" && !regPartton.test(userPhone)) {
                alert("手机号码格式不正确！");
                return false;
            }
            var pqq = $("#pqq").val().trim();
            //if (pqq == undefined || pqq == "") {
            //    alert('QQ不能空！');
            //    return false;
            //}
            //var mail = $("#pemail").val().trim();
            //var re = /^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$/
            //if (mail == "" && !re.test(mail)) {
            //    alert("邮箱格式不正确");
            //    return false;
            //}
            //var age = $("#page").val().trim();
            //if (age == undefined || age == "") {
            //    alert('年龄不能空！');
            //    return false;
            //}
            //if ($('input[name=aprovince]').val().trim() == '' || $('input[name=acity]').val().trim() == '' || $('input[name=aarea]').val().trim() == '') {
            //    alert('所在地不能为空');
            //    return false;
            //}

            //var tuijian = $('select[name=tujing]').find("option:selected").text();
            //if (tuijian == undefined || tuijian == '请选择') {
            //    alert('获取途径不能为空！');
            //    return false;
            //}

            //var job = $('select[name=job]').find("option:selected").text();
            //if (job == undefined || job == '请选择') {
            //    alert('职业不能为空！');
            //    return false;
            //}

            $.ajax({
                type: "post",
                async: false,
                cache: false,
                url: "Handler.ashx",
                dataType: "json",
                data: {
                    "Method": "UpdateMemberBynID",
                    "userID": loginUser,
                    "name": userName,
                    "idcard": idcard,
                    //"addr": $('input[name=area]').val(),
                    "phone": userPhone,
                    //"email": mail,
                    "qq": pqq
                    // "aprovince": $('input[name=aprovince]').val(),
                    //"acity": $('input[name=acity]').val(),
                    // "aarea": $('input[name=aarea]').val(),
                    //"tujing": $('select[name=tujing]').find("option:selected").text(),
                    //"job": $('select[name=job]').find("option:selected").text()
                },
                success: function (e) {
                    if (e) {

                        var fg = $.getUrlParam("jp"); //是否是申请提现页面跳转过来的
                        if (fg == null || fg == "") {
                            window.location.href = "MyProfile.aspx?uid=" + loginUser;
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
        function SetShi(region_id) {
            var data = { 'region_id': region_id, 'Method': "GetCity" };
            $.ajax({
                url: 'Handler.ashx',
                type: 'post',
                data: data,
                dataType: 'json',
                success: function (e) {
                    if (e != null && e != "") {
                        $('select[name=shop_city]').prepend(e);
                    }
                }
            })
        }
        function Setarea(region_id) {
            var data = { 'region_id': region_id, 'Method': "GetArea" };
            $.ajax({
                url: 'Handler.ashx',
                type: 'post',
                data: data,
                dataType: 'json',
                success: function (e) {
                    if (e != null && e != "") {
                        $('select[name=district]').prepend(e);
                    }
                }
            })
        }
    </script>
</head>
<body aria-atomic="False">
    <input type="hidden" value="" name="aprovince" />
    <input type="hidden" value="" name="acity" />
    <input type="hidden" value="" name="aarea" />
    <input type="hidden" value="" name="tujing" />
    <input type="hidden" value="" name="job" />
    <input name="area" type="hidden" value="" />
    <div class="inContent">
        <div class="StylistT" style="text-align: center">
            <%--     <img src="Img/Styl_01.png" />
            <h1>昵称：东方红</h1>
            <h2>注册日期：2015-08-21</h2>--%>
            为了保证您的提款质量，以下所有内容必须认真填写
        </div>
        <dl class="MyProfile">
            <dd><span>姓 名：</span><input type="text" id="pname" value="" placeholder="您的真实姓名" /></dd>
            <dd><span>证 件：</span><input type="text" id="idcard" value="" placeholder="您的18位身份证号码" /></dd>
            <dd><span>手 机：</span><input type="tel" id="pphone" value="" placeholder="您的手机号码" /></dd>
            <dd style="display:none"><span>Q Q：</span><input type="tel" id="pqq" value="" placeholder="您的常用QQ" /></dd>
            <%--            <dd style="display:none"><span>年 龄：</span><input type="tel" id="page" value="" placeholder="您的真实年龄"  /></dd>
            <dd style="display:none"><span>所在地：</span><i class="fa fa-caret-down Pfe_01"></i><i class="fa fa-caret-down Pfe_02"></i><i class="fa fa-caret-down Pfe_03"></i>
                <select class="MyProfileSel" name="shop_province">
                </select>
                <select class="MyProfileSel" name="shop_city">
                    <option value="">请选择</option>
                </select>
                <select class="MyProfileSel" name="district">
                    <option value="">请选择</option>
                </select></dd>
            <dd style="display: none"><span>地 区：</span><input type="text" id="paddr" value="" placeholder="可不填" /></dd>
            <dd style="display:none"><span>邮 箱：</span><input type="email" id="pemail" value="" placeholder="您的常用邮箱" /></dd>
            <dd style="display:none"><span>获取途径：</span><i class="fa fa-caret-down Pfe_04"></i>
                <select class="MyPfSel" name="tujing">
                    <option>请选择</option>
                    <option>微信</option>
                    <option>微博</option>
                    <option>QQ</option>
                    <option>朋友介绍</option>
                    <option>视频媒体</option>
                    <option>音频媒体</option>
                    <option>百度</option>
                    <option>其他</option>
                </select></dd>
            <dd style="display:none"><span>职业：</span><i class="fa fa-caret-down Pfe_04"></i>
                <select class="MyPfSel" name="job">
                    <option>请选择</option>
                    <option>老板</option>
                    <option>店长</option>
                    <option>美甲师</option>
                    <option>其他</option>
                </select></dd>--%>
            <dt>
                <input type="button" name="" id="btnUpdateMsg" value="保存信息" onclick="Save()" /></dt>
            <dt>
                <input type="button" name="" value="返回" onclick="window.location.href = 'javascript:history.go(-1);'" /></dt>
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