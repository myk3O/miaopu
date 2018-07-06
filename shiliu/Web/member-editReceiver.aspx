<%@ Page Language="C#" AutoEventWireup="true" CodeFile="member-editReceiver.aspx.cs"
    Inherits="Web_member_editReceiver" %>

<%@ Register Src="~/Web/footer.ascx" TagName="footer" TagPrefix="ft" %>
<%@ Register Src="~/Web/TopHead.ascx" TagName="header" TagPrefix="hd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>编辑收货地址</title>
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
    <hd:header ID="Header1" runat="server" />
    <div class="main innerbox">
        <div class="nav">
            <div class="Navigation">
                您当前的位置： <span class="now">编辑收货地址</span>
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
            //获取url？后面的参数
            (function ($) {
                $.getUrlParam = function (name) {
                    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                    var r = window.location.search.substr(1).match(reg);
                    if (r != null) return unescape(r[2]); return null;
                }
            })(jQuery);

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

                ///获取详细信息
                var nID = $.getUrlParam('id');
                $.ajax({
                    url: '../Wap/Handler.ashx',
                    type: 'post',
                    data: { "Method": "GetAddrDetail", "nID": nID },
                    dataType: 'json',
                    success: function (e) {
                        if (e != null && e != "") {
                            $('input[name=aprovince]').val(e.Sheng); //存放省code
                            $('input[name=acity]').val(e.Shi); //存放市code
                            $('input[name=aarea]').val(e.area); //存放区域code
                            $('input[name=area]').val(e.YouBian); //存放YouBian
                            $('input[name=name]').val(e.PeopleName);
                            $('#addr').val(e.AddressDe);
                            $('input[name=mobile]').val(e.Phone);
                            $('select[name=shop_province]').val(e.Sheng);
                            SetShi(e.Sheng);
                            Setarea(e.Shi);
                            var arr = e.YouBian.split('-');
                            //$('select[name=shop_city]').val(e.Shi);
                            $('select[name=shop_city]').html('<option value=' + e.Shi + '>' + arr[1] + '</option>');
                            //$('select[name=district]').val(e.area);
                            $('select[name=district]').html('<option value=' + e.area + '>' + arr[2] + '</option>');
                            // $("select[name=shop_city option[text='大庆市']").attr("selected", true);   //设置Select的Text值为jQuery的项选中
                        }
                    }
                })

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
                    var str = $('input[name=area]').val();
                    var arr = str.split('-');
                    $('input[name=area]').val(text + '-' + arr[1] + '-' + arr[2]);
                    //$('input[name=area]').val('mainland:' + text + ':' + region_id);
                    $('input[name=aprovince]').val(region_id); //存放省code
                    $('input[name=acity]').val(''); //清空市code
                    $('input[name=aarea]').val(''); //清空区域code
                    SetShi(region_id);
                });
                $('select[name=shop_city]').change(function (obj) {
                    var obj = $(this);
                    var region_id = $(this).val();
                    $('select[name=district]').html('<option value="">请选择</option>');
                    //                var ship_area = $('input[name=area]').val();
                    //                var strs = new Array();
                    //                strs = ship_area.split(':');
                    //                var city = strs[1].split('/');
                    if (region_id == '') {
                        //var html = strs[0] + ':' + city[0] + ':' + $('select[name=shop_province]').find("option:selected").text();
                        // $('input[name=area]').val(html);
                        return false;
                    }

                    var text = obj.find("option:selected").text();

                    var str = $('input[name=area]').val();
                    var arr = str.split('-');
                    $('input[name=area]').val(arr[0] + '-' + text + '-' + arr[2]);
                    //var html = strs[0] + ':' + city[0] + '/' + text + ':' + region_id;
                    // $('input[name=area]').val(html);
                    $('input[name=acity]').val(region_id); //存放市code
                    $('input[name=aarea]').val(''); //清空区域code
                    Setarea(region_id);
                });
                $('select[name=district]').change(function (obj) {
                    var obj = $(this);
                    var region_id = $(this).val();
                    //  var ship_area = $('input[name=area]').val();
                    // var strs = new Array();
                    // strs = ship_area.split(':');

                    // var city = strs[1].split('/');
                    if (region_id == '') {
                        //var html = strs[0] + ':' + city[0] + '/' + city[1] + ':' + $('select[name=shop_city]').find("option:selected").text();
                        // $('input[name=ship_area]').val(html);
                        return false;
                    }
                    var text = obj.find("option:selected").text();

                    var str = $('input[name=area]').val();
                    var arr = str.split('-');
                    $('input[name=area]').val(arr[0] + '-' + arr[1] + '-' + text);
                    // var html = strs[0] + ':' + city[0] + '-' + city[1] + '-' + text + ':' + region_id;
                    // $('input[name=area]').val(html);
                    $('input[name=aarea]').val(region_id); //存放区域code
                });
            });
            function SetShi(region_id) {
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
            }
            function Setarea(region_id) {
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
            }
            function getbrack() {
                window.location.href = "member-receiver.aspx";
            }
            function UpdateAddress() {
                var str = $('input[name=area]').val();
//                var loginUser = window.localStorage.getItem("LoginUserId");
//                if (loginUser == null || loginUser == "") {
//                    //MyChangePage("Person/Individual.html");
//                    window.location.href = "login.html";
//                    return false;
//                }
                if ($('input[name=acity]').val().trim() == '' || $('input[name=acity]').val().trim() == '' || $('input[name=aarea]').val().trim() == '' ||
            $('input[name=name]').val().trim() == '' || $('#addr').val().trim() == '' || $('input[name=mobile]').val().trim() == '') {
                    alert('信息不能有空');
                    return false;
                }
                var data = { 'Method': "UpdateAddress", 'nID': $.getUrlParam('id'),
                    'aprovince': $('input[name=aprovince]').val(), 'acity': $('input[name=acity]').val(), 'aarea': $('input[name=aarea]').val(),
                    'aname': $('input[name=name]').val(), 'address': $('#addr').val(),
                    'yCode': $('input[name=area]').val(), 'Phone': $('input[name=mobile]').val()
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
                            alert('更新失败');
                        }
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
                                    <%--       <tr>
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
                                            <input type="button" name="submit" value="保存" onclick="UpdateAddress()" />
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
    <ft:footer ID="Footer1" runat="server" />
</body>
</html>
