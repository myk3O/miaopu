<%@ Page Language="C#" AutoEventWireup="true" CodeFile="member-security.aspx.cs"
    Inherits="Web_member_security" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Web/footer.ascx" TagName="footer" TagPrefix="ft" %>
<%@ Register Src="~/Web/TopHead.ascx" TagName="header" TagPrefix="hd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>修改密码</title>
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
</head>
<body>
    <hd:header runat="server" />
    <div class="main innerbox">
        <div class="nav">
            <div class="Navigation">
                您当前的位置： <span class="now">修改密码</span>
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
        <script type="text/javascript">

            function btnSubmit() {
                var loginUser = $('#LoginUserID').val();
                if (loginUser == null || loginUser == "") {
                    //MyChangePage("Person/Individual.html");
                    window.location.href = "passport-login.aspx";
                    return false;
                }
                if ($('input[name=old_passwd]').val().trim() == '') {
                    alert('旧密码不能空');
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
                ///判断原密码正确与否
                $.ajax({
                    url: '../Wap/Handler.ashx',
                    type: 'post',
                    data: { 'Method': "CheckPwd",
                        'UserID': loginUser,
                        'old_passwd': $('input[name=old_passwd]').val().trim(),
                        'passwd': $('input[name=passwd]').val().trim()
                    },
                    dataType: 'json',
                    success: function (e) {
                        if (e != null && e == "ok") {
                            //window.localStorage.setItem("LoginUserId", "");
                            alert('修改成功，返回登录');
                            // ajax调用ashx页面，清空session;  
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
        <input value="" id="LoginUserID" type="hidden" runat="server" /><%--session  id--%>
        <div class="MemberBox">
            <div class="MemberCenter">
           
                <div class="MemberSidebar">
                    <div class="MemberMenu">
                        <div class="title">
                        </div>
                        <div class="body">
                            <ul>
                                <li class="MemberMenuList"><span>
                                    <div class="m_0" style="font-size: 14px;">
                                        交易记录</div>
                                </span>
                                    <ul>
                                        <li><a href="member-orders.aspx" target="_self"><b>我的订单</b></a></li>
                                    </ul>
                                </li>
                                <li class="MemberMenuList"><span>
                                    <div class="m_3" style="font-size: 14px;">
                                        个人设置</div>
                                </span>
                                    <ul>
                                        <li class="current"><a href="member-security.aspx" target="_self">修改密码</a></li>
                                        <li><a href="member-receiver.aspx" target="_self">收货地址</a></li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                        <div class="foot">
                        </div>
                    </div>
                </div>
                <div class="MemberMain">
                    <div style="">
                        <div class="title">
                            修改密码</div>
                        <div class="FormWrap" style="background: none; border: none; padding: 0; margin: 0;">
                            <div class="division" style="border: none">
                                <table class="forform" width="100%" border="0" cellspacing="0" cellpadding="0" class="forform">
                                    <tr>
                                        <th>
                                            旧密码：
                                        </th>
                                        <td>
                                            <input autocomplete="off" class="x-input inputstyle" class="inputstyle" type="password"
                                                required="true" name="old_passwd" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            新密码：
                                        </th>
                                        <td>
                                            <input autocomplete="off" class="x-input inputstyle" class="inputstyle" type="password"
                                                required="true" name="passwd" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            确认新密码：
                                        </th>
                                        <td>
                                            <input autocomplete="off" class="x-input inputstyle" class="inputstyle" type="password"
                                                required="true" name="passwd_re" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                        </th>
                                        <td>
                                            <input class="actbtn btn-save" type="button" value="保存" id="btnSubmit1" onclick="btnSubmit()" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
    <ft:footer runat="server" />
</body>
</html>
