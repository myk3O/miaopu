<%@ Page Language="C#" AutoEventWireup="true" CodeFile="member-addReceiver.aspx.cs"
    Inherits="Web_member_addReceiver" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Web/footer.ascx" TagName="footer" TagPrefix="ft" %>
<%@ Register Src="~/Web/TopHead.ascx" TagName="header" TagPrefix="hd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>添加收货地址</title>
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
                您当前的位置： <span class="now">添加收货地址</span>
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
            $(function () {

                $.ajax({
                    type: "post",
                    async: false,
                    cache: false,
                    url: "../Wap/Handler.ashx",
                    dataType: "json",
                    data: { "Method": "GetProvince" },
                    beforeSend: function () {
                        //showLoader();
                    },
                    success: function (Province) {
                        if (Province != null && Province != "") {
                            //$('select[name=shop_province]').append('<option value="">请选择</option>');
                            $('select[name=shop_province]').prepend(Province);
                        }

                    },
                    error: function () {
                        alert('连接超时');
                    },
                    complete: function () {
                        //  $('#list_hd').listview('refresh');
                        // hideLoader();
                    }
                });




                $('select[name=shop_province]').change(function (obj) {
                    var obj = $(this);
                    var region_id = $(this).val();
                    $('select[name=shop_city]').html('<option value="">请选择</option>');
                    $('select[name=district]').html('<option value="">请选择</option>');
                    if (!region_id) {

                        $('input[name=area]').val('');
                        return false;
                    }
                    var text = obj.find("option:selected").text();
                    $('input[name=area]').val('mainland:' + text + ':' + region_id);
                    $('input[name=aprovince]').val(region_id); //存放省code
                    $('input[name=acity]').val(''); //清空市code
                    $('input[name=aarea]').val(''); //清空区域code
                    var data = { 'region_id': region_id, 'Method': "GetCity" };
                    $.ajax({
                        url: '../Wap/Handler.ashx',
                        type: 'post',
                        data: data,
                        dataType: 'json',
                        success: function (e) {
                            if (e != null && e != "") {
                                $('select[name=shop_city]').prepend(e);
                            }
                        }
                    })
                });
                $('select[name=shop_city]').change(function (obj) {
                    var obj = $(this);
                    var region_id = $(this).val();
                    $('select[name=district]').html('<option value="">请选择</option>');
                    var ship_area = $('input[name=area]').val();
                    var strs = new Array();
                    strs = ship_area.split(':');
                    var city = strs[1].split('/');
                    if (region_id == '') {
                        var html = strs[0] + ':' + city[0] + ':' + $('select[name=shop_province]').find("option:selected").text();
                        $('input[name=area]').val(html);
                        return false;
                    }

                    var text = obj.find("option:selected").text();
                    var html = strs[0] + ':' + city[0] + '/' + text + ':' + region_id;
                    $('input[name=area]').val(html);
                    $('input[name=acity]').val(region_id); //存放市code
                    $('input[name=aarea]').val(''); //清空区域code
                    var data = { 'region_id': region_id, 'Method': "GetArea" };
                    $.ajax({
                        url: '../Wap/Handler.ashx',
                        type: 'post',
                        data: data,
                        dataType: 'json',
                        success: function (e) {
                            if (e != null && e != "") {
                                $('select[name=district]').prepend(e);
                            }
                        }
                    })
                });
                $('select[name=district]').change(function (obj) {
                    var obj = $(this);
                    var region_id = $(this).val();
                    var ship_area = $('input[name=area]').val();
                    var strs = new Array();
                    strs = ship_area.split(':');

                    var city = strs[1].split('/');
                    if (region_id == '') {
                        var html = strs[0] + ':' + city[0] + '/' + city[1] + ':' + $('select[name=shop_city]').find("option:selected").text();
                        $('input[name=ship_area]').val(html);
                        return false;
                    }

                    var text = obj.find("option:selected").text();
                    var html = strs[0] + ':' + city[0] + '-' + city[1] + '-' + text + ':' + region_id;
                    $('input[name=area]').val(html);
                    $('input[name=aarea]').val(region_id); //存放区域code
                });
            });
            function getbrack() {
                window.location.href = "member-receiver.aspx";
            }
            function addAddress() {
                var loginUser = $('#LoginUserID').val();
                if (loginUser == null || loginUser == "") {
                    //MyChangePage("Person/Individual.html");
                    window.location.href = "passport-login.aspx";
                    return false;
                }
                if ($('input[name=acity]').val().trim() == '' || $('input[name=acity]').val().trim() == '' || $('input[name=aarea]').val().trim() == '' ||
            $('input[name=name]').val().trim() == '' || $('#addr').val().trim() == '' || $('input[name=mobile]').val().trim() == '') {
                    alert('信息不能有空');
                    return false;
                }
                var addr = $('input[name=area]').val().split(':');
                var data = { 'Method': "addAddress", 'UserID': loginUser,
                    'aprovince': $('input[name=aprovince]').val(), 'acity': $('input[name=acity]').val(), 'aarea': $('input[name=aarea]').val(),
                    'aname': $('input[name=name]').val(), 'address': $('#addr').val(),
                    'yCode': addr[1], 'Phone': $('input[name=mobile]').val()
                };
                $.ajax({
                    url: '../Wap/Handler.ashx',
                    type: 'post',
                    data: data,
                    dataType: 'json',
                    success: function (e) {
                        if (e) {
                            getbrack();
                        } else {
                            alert('添加失败');
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
                                        <li><a href="member-security.aspx" target="_self">修改密码</a></li>
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
                    <input type="hidden" value="" name="aprovince" />
                    <input type="hidden" value="" name="acity" />
                    <input type="hidden" value="" name="aarea" />
                    <input name="area" type="hidden" value="" />
                    <div style="">
                        <div class="title">
                            添加收货地址</div>
                        <div class="FormWrap">
                            <div class="division">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="forform">
                                    <tr>
                                        <th>
                                            <em>*</em>收货人姓名：
                                        </th>
                                        <td>
                                            <input autocomplete="off" class="x-input inputstyle" class="inputstyle" name="name"
                                                placeholder="请填写收货人的真实姓名" type="text" required="true" id="name" size="30" value="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <em>*</em>联系电话：
                                        </th>
                                        <td>
                                            <input autocomplete="off" class="x-input inputstyle" class="inputstyle" name="mobile"
                                                placeholder="收货人电话号码" type="text" id="mobile" size="30" value="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <em>*</em>所在地区：
                                        </th>
                                        <td>
                                            <select name="shop_province" id="selProvinces">
                                            </select>
                                            <select name="shop_city" id="selCities">
                                                <option value="">请选择</option>
                                            </select>
                                            <select name="district" id="selDistricts">
                                                <option value="">请选择</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <em>*</em>详细地址：
                                        </th>
                                        <td>
                                            <textarea class="inputstyle" required="true" name="addr" id="addr" placeholder="不必重复填写地区"
                                                rows="2" cols="30"></textarea>
                                        </td>
                                    </tr>
                               <%--     <tr>
                                        <th>
                                            <em></em>邮政编码：
                                        </th>
                                        <td>
                                            <input autocomplete="off" class="x-input inputstyle" class="inputstyle" name="zip"
                                                size="30" required="true" type="text" />
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <th>
                                        </th>
                                        <td>
                                            <input type="button" name="submit" value="保存" onclick="addAddress()" />
                                            <input class="btn enter" value="返回" onclick="getbrack()" type="button" />
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
