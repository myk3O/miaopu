<%@ Page Language="C#" AutoEventWireup="true" CodeFile="member-setting.aspx.cs" Inherits="Web_member_setting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Web/footer.ascx" TagName="footer" TagPrefix="ft" %>
<%@ Register Src="~/Web/TopHead.ascx" TagName="header" TagPrefix="hd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>会员中心</title>
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
</head>
<body>

    <hd:header ID="Header1" runat="server" />
    <div class="main innerbox">
        <div class="nav">
            <div class="Navigation">
                您当前的位置： <span class="now">首页</span>
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
                                        <li><a href="member-orders.html" target="_self"><b>我的订单</b></a></li>
                                    </ul>
                                </li>
                                <li class="MemberMenuList"><span>
                                    <div class="m_3" style="font-size: 14px;">
                                        个人设置</div>
                                </span>
                                    <ul>
                                        <li><a href="member-security.html" target="_self">修改密码</a></li>
                                        <li><a href="member-receiver.html" target="_self">收货地址</a></li>
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
                            个人信息</div>
                        <form method="post" action="member-saveMember.html" id='form_saveMember' class="section">
                        <div class="FormWrap" style="background: none; border: none; padding: 0; margin: 0;">
                            <div class="division" style="border: none;">
                                <table class="forform" width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <th>
                                            <em>*</em>Email：
                                        </th>
                                        <td>
                                            <input autocomplete="off" class="x-input inputstyle" class="inputstyle" name="email"
                                                type="text" required="true" vtype="email" value="451534279@qq.com" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="border-top: 1px solid #f1f1f1;">
                            </div>
                            <div class="division" style="border: none;">
                                <table class="forform" width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <th>
                                            姓名
                                        </th>
                                        <td>
                                            <input autocomplete="off" class="x-input inputstyle _x_ipt" class="inputstyle _x_ipt"
                                                name="name" required="false" type="text" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            性别
                                        </th>
                                        <td>
                                            <input type='radio' name='sex' value='1' checked>
                                            <label>
                                                男</label>
                                            <input type='radio' name='sex' value='0'>
                                            <label>
                                                女</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            出生日期
                                        </th>
                                        <td>
                                            <input type="hidden" name="_DTYPE_DATE[]" value="birthday" />
                                            <input class="cal cal" size="10" maxlength="10" autocomplete="off" class="cal" name="birthday"
                                                value="" id="el-aa0f6-bf1dc" type="text" vtype="date" />
                                            <script>                                                $("el-aa0f6-bf1dc").makeCalable();</script>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            地区
                                        </th>
                                        <td>
                                            <span package="mainland" class="span _x_ipt" vtype="area2">
                                                <input type="hidden" name="area" />
                                                <select onchange="selectArea(this,this.value,2)">
                                                    <option value="_NULL_">请选择...</option>
                                                    <option has_c="true" value="1">北京</option>
                                                    <option has_c="true" value="22">天津</option>
                                                    <option has_c="true" value="44">河北省</option>
                                                    <option has_c="true" value="243">山西省</option>
                                                    <option has_c="true" value="386">内蒙古自治区</option>
                                                    <option has_c="true" value="512">辽宁省</option>
                                                    <option has_c="true" value="647">吉林省</option>
                                                    <option has_c="true" value="730">黑龙江省</option>
                                                    <option has_c="true" value="889">上海</option>
                                                    <option has_c="true" value="912">江苏省</option>
                                                    <option has_c="true" value="1051">浙江省</option>
                                                    <option has_c="true" value="1164">安徽省</option>
                                                    <option has_c="true" value="1305">福建省</option>
                                                    <option has_c="true" value="1409">江西省</option>
                                                    <option has_c="true" value="1535">山东省</option>
                                                    <option has_c="true" value="1715">河南省</option>
                                                    <option has_c="true" value="1912">湖北省</option>
                                                    <option has_c="true" value="2046">湖南省</option>
                                                    <option has_c="true" value="2197">广东省</option>
                                                    <option has_c="true" value="2364">广西壮族自治区</option>
                                                    <option has_c="true" value="2502">海南省</option>
                                                    <option has_c="true" value="2529">重庆</option>
                                                    <option has_c="true" value="2572">四川省</option>
                                                    <option has_c="true" value="2798">贵州省</option>
                                                    <option has_c="true" value="2906">云南省</option>
                                                    <option has_c="true" value="3068">西藏自治区</option>
                                                    <option has_c="true" value="3156">陕西省</option>
                                                    <option has_c="true" value="3284">甘肃省</option>
                                                    <option has_c="true" value="3398">青海省</option>
                                                    <option has_c="true" value="3458">宁夏回族自治区</option>
                                                    <option has_c="true" value="3491">新疆维吾尔自治区</option>
                                                    <option has_c="true" value="3620">台湾省</option>
                                                    <option has_c="true" value="3698">香港特别行政区</option>
                                                    <option has_c="true" value="3720">澳门特别行政区</option>
                                                </select>
                                            </span>
                                            <script>
                                                addEvent('domready', function () {
                                                    validatorMap.set('area2', ['你没选择完整的地区', function (el, v) {
                                                        var els = el.getElements('select');
                                                        if (els.length == 1 && (els[0].getValue() == '' || els[0].getValue() == '_NULL_')) {
                                                            return true;
                                                        } else {
                                                            return els.every(function (sel) {
                                                                var selValue = sel.getValue();
                                                                var v = selValue != '' && selValue != '_NULL_';
                                                                if (!v) {
                                                                    sel.focus();
                                                                    return false;
                                                                }
                                                                return true;
                                                            });
                                                        }
                                                        return true;
                                                    }
		   ]);
                                                });
                                            </script>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            联系地址
                                        </th>
                                        <td>
                                            <input autocomplete="off" class="x-input inputstyle _x_ipt" class="inputstyle _x_ipt"
                                                name="addr" required="false" type="text" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            邮编
                                        </th>
                                        <td>
                                            <input autocomplete="off" class="x-input inputstyle _x_ipt" class="inputstyle _x_ipt"
                                                name="zip" required="false" type="text" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            手机号码
                                        </th>
                                        <td>
                                            <input autocomplete="off" class="x-input inputstyle _x_ipt" class="inputstyle _x_ipt"
                                                name="mobile" required="false" type="text" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            身份证号
                                        </th>
                                        <td>
                                            <input class='inputstyle _x_ipt' name='15' vtype='alphaint' type='text' value='' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            固定电话
                                        </th>
                                        <td>
                                            <input autocomplete="off" class="x-input inputstyle _x_ipt" class="inputstyle _x_ipt"
                                                name="tel" required="false" type="text" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                        </th>
                                        <td>
                                            <input class="actbtn btn-save" type="submit" value="保存" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        </form>
                    </div>
                </div>
                <script>

                    window.addEvent('domready', function () {
                        validatorMap.set('selectc', ['本项必填', function (element, value) {

                            var checkboxelement = element.getParent('td').getElements('input[name$=]]');
                            var flag = checkboxelement.some(function (i) {
                                return !!i.checked;
                            });
                            checkboxelement.addEvent('change', function () {
                                validator.removeCaution(element);
                                validator.test($('form_saveMember'), element);
                                checkboxelement.removeEvent('change', arguments.callee);
                            });
                            return flag;
                        } ])
                    });
                </script>
                <div class="clear">
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
