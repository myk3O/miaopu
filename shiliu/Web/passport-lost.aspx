<%@ Page Language="C#" AutoEventWireup="true" CodeFile="passport-lost.aspx.cs" Inherits="Web_passport_lost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Web/footer.ascx" TagName="footer" TagPrefix="ft" %>
<%@ Register Src="~/Web/TopHead.ascx" TagName="header" TagPrefix="hd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>找回密码</title>
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
    <hd:header ID="Header1" runat="server" />
    <div class="main innerbox">
        <div class="nav">
            <div class="Navigation">
                您当前的位置： <span>首页</span> > <span class="now">找回密码</span>
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

                $('#part2').hide();
                //获取验证码
                $("input#code_Button").on("click", function () {
                    var fname = $("input[name='user_name']"); //电话号码
                    var ocode = $("input#code_Button"); //获取验证码按钮
                    if (IsMobile(fname.val().trim())) {

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
                });


                //下一步按钮
                $("#Register_Button").on("click", function () {
                    var yzm = window.localStorage.getItem("SessionYZM");
                    var Phone = window.localStorage.getItem("PhoneNum");
                    var fname = $("input[name='user_name']");
                    var rcode = $("input[name='rcode']");
                    if (fname == undefined || rcode == undefined) { return false; }
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
                    //window.location.href = "forgetpwd_2.aspx";

                    $('#part1').hide();
                    $('#part2').show();
                });
            });

            function getbrack() {
                window.location.href = "passport-login.aspx";
            }


            function btnSubmit() {
                var Phone = window.localStorage.getItem("PhoneNum");
                if (Phone == null || Phone == "") {
                    alert('手机号码错误');
                    window.location.href = "passport-login.aspx";
                    return false;
                }
                if ($('input[name=passwd]').val().trim() == '' || $('input[name=passwd_re]').val().trim() == '') {
                    alert('新密码不能空');
                    return false;
                }
                if ($('input[name=passwd]').val().trim() != $('input[name=passwd_re]').val().trim()) {
                    alert('两次新密码不相同');
                    return false;
                }
                ///修改密码
                $.ajax({
                    url: '../Wap/Handler.ashx',
                    type: 'post',
                    data: { 'Method': "CheckPwdByPhone",
                        'Phone': Phone,
                        'passwd': $('input[name=passwd]').val().trim()
                    },
                    dataType: 'json',
                    success: function (e) {
                        if (e != null && e == "ok") {
                            alert('修改成功，返回登录');
                            window.location.href = "passport-login.aspx";
                        } else {
                            alert(e);
                        }
                    },
                    error: function () {
                        alert('连接超时');
                    }
                })

            }
        </script>
        <div class="MemberLogin">
            <div class="MemberLostL">
                <h4>
                    找回密码</h4>
                <div id='part1'>
                    <ul>
                        <li>
                            <input id="user_name" name="user_name" type="text" class="test" placeholder="注册时使用的手机号" />
                        </li>
                        <li>
                            <input name="rcode" type="text" class="testL" placeholder="验证码" /><input name="button"
                                id="code_Button" class="testA" type="button" value="获取验证码" />
                        </li>
                    </ul>
                    <h6>
                        <ul>
                            <li>
                                <input name="Submit" id="Register_Button" type="button" value="下一步" />
                            </li>
                            <li>
                                <input class="btn enter" value="返回" onclick="getbrack()" type="button" />
                            </li>
                        </ul>
                    </h6>
                </div>
                <div id='part2'>
                    <ul>
                        <li>
                            <input name="passwd" type="password" class="test" placeholder="请填写新密码" />
                        </li>
                        <li>
                            <input name="passwd_re" type="password" class="test" placeholder="再次填写新密码" />
                        </li>
                    </ul>
                    <h6>
                        <input id="btnSubmit1" onclick="btnSubmit()" type="button" value="确认" />
                    </h6>
                </div>
            </div>
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
    <ft:footer ID="Footer1" runat="server" />
</body>
</html>
