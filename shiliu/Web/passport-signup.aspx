<%@ Page Language="C#" AutoEventWireup="true" CodeFile="passport-signup.aspx.cs"
    Inherits="Web_passport_signup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Web/footer.ascx" TagName="footer" TagPrefix="ft" %>
<%@ Register Src="~/Web/TopHead.ascx" TagName="header" TagPrefix="hd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>注册</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="keywords" content="">
    <meta name="description" content="">
    <meta name="generator" content="ShopEx 4.8.5">
    <meta name="robots" content="noindex,noarchive,nofollow" />
    <link rel="icon" href="../image/bitbug_favicon.ico" type="image/x-icon" />
    <link rel="bookmark" href="../image/bitbug_favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" href="statics/style.css" type="text/css" />
    <script type="text/javascript" src="statics/script/tools.js"></script>
    <script type="text/javascript" src="statics/script/goodscupcake.js"></script>
    <link href="themes/1394732638/images/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="themes/1394732638/images/upc.js"></script>
    <script src="../Wap/Js/jquery.js"></script>
    <script charset="utf-8" type="text/javascript" src="../Wap/Js/jquery_002.js"></script>
    <script src="../Wap/Js/PageShow.js" type="text/javascript"></script>
</head>
<body>
    <hd:header runat="server" />
    <div class="main innerbox">
        <div class="nav">
            <div class="Navigation">
                您当前的位置： <span>首页</span> > <span class="now">注册</span>
            </div>
        </div>
        <script>

            window.addEvent("domready", function () {

                $$(".MemberMenuList span")[0].setStyle("border-top", "none");

                $$(".MemberMenuList").each(function (item, index) {

                    item.addEvents({

                        mouseenter: function () {
                            this.getElement('span').addClass('hover');
                            $$(".MemberMenuList ul")[index].setStyle("background", "#f2f2f2");
                        },
                        mouseleave: function () {
                            this.getElement('span').removeClass('hover');
                            $$(".MemberMenuList ul")[index].setStyle("background", "#fff");
                        }
                    });

                });

                $$(".memberlist > tbody > tr").each(function (item, index) {

                    if (index > 0 && index % 2 == 0) { item.setStyle("background", "#f7f7f7"); }

                });

            });

        </script>
        <script type="text/javascript" src="statics/script/formplus.js"></script>
        <script type="text/javascript">            //注册表单验证
            $(function () {
                //获取验证码
                $("input#code_Button").on("click", function () {
                    var fname = $("input[name='user_name']");
                    var fpwd = $("input[name='password']");
                    var fpwdcf = $("input[name='password_confirm']");
                    var ocode = $("input#code_Button");

                    if (validate_pwd(fpwd.val().trim(), fpwdcf.val().trim())) {   //验证密码        

                        if (IsMobile(fname.val().trim())) {
                            var ss = "";
                            $.ajax({
                                type: "post",
                                async: false,
                                cache: false,
                                url: "../Wap/Handler.ashx",
                                dataType: "json",
                                data: {
                                    "Method": "CheckUserNameIsExit",
                                    "user_name": fname.val().trim()
                                },
                                success: function (data) {
                                    if (data) {
                                        // $("#checking_user").html("手机号已被注册");
                                        alert('手机号已被注册');
                                    } else {
                                        ocode.attr('disabled', "true");   //90秒后重新启动发送按钮    
                                        // ocode.addClass("code1");
                                        start_sms_button(ocode); //90秒倒计时
                                        window.localStorage.setItem("PhoneNum", fname.val());
                                        $.ajax({
                                            type: "post",
                                            async: false,
                                            cache: false,
                                            url: "../Wap/Handler.ashx",
                                            dataType: "json",
                                            data: {
                                                "Method": "Get_YZM",
                                                "fName": fname.val().trim()
                                            },
                                            success: function (data) {
                                                if (data.StrResult != 'ok') {
                                                    //showMsg(data.msg);
                                                    alert(data.StrResult);
                                                } else {
                                                    window.localStorage.setItem("SessionYZM", data.YZM);
                                                }
                                            },
                                            error: function () {
                                                alert('连接超时');
                                            }

                                        });
                                    }
                                },
                                error: function () {
                                    alert('连接超时');
                                }
                            });
                        }

                    }

                });


                //注册按钮
                $("#Register_Button").on("click", function () {
                    var yzm = window.localStorage.getItem("SessionYZM");
                    var Phone = window.localStorage.getItem("PhoneNum");
                    var fname = $("input[name='user_name']");
                    var fpwd = $("input[name='password']");
                    var rcode = $("input[name='rcode']");
                    if (fname == undefined || fpwd == undefined || rcode == undefined) { return false; }
                    var passCart = rcode.val().trim();
                    if (yzm != passCart) {
                        alert('验证码不正确');
                        return false;
                    }
                    if (Phone != fname.val()) {
                        alert('手机号码已更改，请重新发送验证');
                        window.localStorage.setItem("SessionYZM", "");
                        return false;
                    }

                    $.ajax({
                        type: "post",
                        async: false,
                        cache: false,
                        url: "../Wap/Handler.ashx",
                        dataType: "json",
                        data: {
                            "Method": "Sign_User",
                            "fName": fname.val().trim(),
                            "fPwd": fpwd.val().trim()
                        },
                        beforeSend: function () {
                            //showLoader();
                        },
                        success: function (data) {
                            if (data != 'ok') {
                                alert(data);
                            } else {
                                alert("注册成功,前往登录");
                                window.location.href = "passport-login.aspx";
                            }
                        },
                        error: function () {
                            alert('连接超时');
                        },
                        complete: function () {
                            //hideLoader();
                        }
                    });
                });
            });
        </script>
        <div class="MemberLogin">
            <div class="MemberLostL">
                <h4>
                    欢迎注册</h4>
                <ul>
                    <li>
                        <input id="user_name" name="user_name" type="text" class="test" maxlength="11" placeholder="手机号码" />
                    </li>
                    <li>
                        <input id="password" name="password" type="password" class="test" placeholder="密&nbsp;&nbsp;&nbsp;码" />
                    </li>
                    <li>
                        <input name="password_confirm" type="password" class="test" placeholder="确认密码" />
                    </li>
                    <li>
                        <input name="rcode" type="text" class="testL" placeholder="验证码" /><input name="button"
                            id="code_Button" class="testA" type="button" value="获取验证码" />
                    </li>
                </ul>
                <h6>
                    <input name="Submit" id="Register_Button" type="button" value="立即注册" />
                </h6>
            </div>
            <div class="MemberLoginR">
                <p>
                    如果你已经是会员，</p>
                请进入 <a href="passport-login.aspx">登录页面</a></div>
        </div>
        <div style="display: none;">
            <div class="memberlist-tip">
                <div class="tip">
                    <div class="tip-title">
                    </div>
                    <div class="tip-text">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <ft:footer runat="server" />
</body>
</html>
