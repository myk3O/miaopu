<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomeMakByMak.aspx.cs" Inherits="Admin_DaiLi_HomeMakByMak" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%--    <link href="../../css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/select.css" rel="stylesheet" type="text/css" />--%>
    <style type="text/css">
body {
	font-size: 12px;
	margin: 0px;
	padding: 0px;
}
.order-info{border:1px solid #999; padding:10px; width:90%; margin:0 outo;}
.order-info dl {
	width: 860px;
	overflow: hidden;
	line-height: 30px
}
.order-info dt, .order-info dd {
	min-height: 30px;
	_height: 30px
}
.order-info hr {
	margin: 10px 0 5px;
*margin:0
}
.order-info .clearfix {
	clear: both;
	line-height: 0
}
.addr_and_note dt, .invoice-info dt, .misc-info dt, .misc-info dd, .contact-info dt, .contact-info dd {
	display: inline;
	float: left
}
.misc-info dl {
	margin-left: 35px;
	border-bottom: 1px solid #f4f4f4
}
.misc-info dt {
	width: 100px;
	text-align: right
}
.misc-info dd {
	width: 182px;
	text-align: left
}
.misc-info .stage-time dl {
	display: none
}
.misc-info .stage-time .current {
	display: block
}
.misc-info .stage-time .view-all {
	display: inline-block;
	margin: 5px 0 0 63px
}
.misc-info .expand dl {
	display: block
}
.contact-info dl {
	margin-left: 50px
}
.contact-info dt {
	width: 85px;
	text-align: right
}
.contact-info dd {
	width: 200px
}
.misc-info th, .misc-info td, .contact-info th, .contact-info td, .dealer-info th, .dealer-info td {
	padding: 5px 10px 5px 0
}
.contact-info td {
	text-align: left
}
.contact-info .nickname, .contact-info .name, .contact-info .mail {
	margin-right: 10px
}
.contact-info .fast-buy-tel {
	padding-right: 15px;
	line-height: 19px;
}
.misc-info span {
	display: inline;
	float: left
}
.misc-info .label {
	width: 90px;
	margin-right: 5px;
	text-align: right
}
.contract-info {
	padding: 10px;
	background: #fff;
	border: 1px solid #ddd;
	color: #666
}
.contract-info li {
	margin: 2px 0;
	padding-left: 85px;
	line-height: 1.5
}
.contract-info .label, .contract-info .desc {
	display: inline-block;
	overflow: hidden;
	vertical-align: top
}
.contract-info .label {
	width: 85px;
	margin-left: -85px;
	text-align: right;
	white-space: nowrap
}
.contract-info .desc {
	width: 310px;
	word-wrap: break-word
}

.addr_and_note dt, .invoice-info dt, .misc-info dt, .misc-info dd, .contact-info dt, .contact-info dd {
	display: inline;
	float: left
}
.addr_and_note {
	line-height: 190%
}
.addr_and_note dt {
	width: 78px;
	font-weight: 700
}
.addr_and_note dd {
	margin-bottom: 4px;
	padding-left: 80px;
*padding-left:0;
	word-wrap: break-word
}





.addr_and_note1 dl {
	margin-left: 50px
}
.addr_and_note1 dl dt {
	width: 85px;
	text-align: right; float:left;
}
.addr_and_note1 dl dd {
	width: 600px; float:left;
}
</style>
    <script src="../../Js/jquery-1.4.3.min.js" type="text/javascript"></script>
    <script src="../../Js/jquery.jqprint-0.3.js" type="text/javascript"></script>
    <script language="javascript">
        function a() {
            $("#ddd").jqprint();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="button" style="margin-left: 50%" onclick="a()" value="打印" />
    <div id="ddd">
        <div class="order-info">
            <div class="bd">
                <%=BindStr%>
                <%--            <div class="misc-info">
                    <h3>
                        代理归属</h3>
                    <dl>
                        <dt>上级代理：</dt>
                        <dd>
                            等待确认
                        </dd>
                    </dl>
                    <dl>
                        <dt>账号：</dt>
                        <dd>
                            ddddd</dd>
                        <dt>密码：</dt>
                        <dd>
                            大厦d</dd>
                    </dl>
                    <div class="clearfix">
                    </div>
                </div>
                <hr>
                <!-- 基本信息 -->
                <div class="misc-info">
                    <h3>
                        基本信息</h3>
                    <dl>
                        <dt>个人/公司名称：</dt>
                        <dd>
                            924037379402258
                        </dd>
                        <dt>联系人：</dt>
                        <dd>
                            2015-01-04 14:32:55
                        </dd>
                    </dl>
                    <dl>
                        <dt>固定电话：</dt>
                        <dd>
                            ddddd</dd>
                        <dt>手机：</dt>
                        <dd>
                            1231231231</dd>
                    </dl>
                    <dl>
                        <dt>邮箱：</dt>
                        <dd>
                            100</dd>
                    </dl>
                    <div class="clearfix">
                    </div>
                </div>
                <hr>
                <div class="addr_and_note1">
                    <h3>
                        商城地址</h3>
                    <dl>
                        <dt>地址：</dt>
                        <dd>
                            支付宝
                        </dd>
                    </dl>
                    <dl>
                        <dt>二维码：</dt>
                        <dd>
                            <img src="../Upload/pic/60403d14-1c66-4be0-9cbc-7da20834f1f8.png" />
                        </dd>
                    </dl>
                    <div class="clearfix">
                    </div>
                </div>
                <hr>
                <div class="addr_and_note">
                    <dl>
                        <dt>邮编： </dt>
                        <dd>
                            324019
                        </dd>
                        <dt>地址： </dt>
                        <dd>
                            木木，2132142342 ，上海 上海市 宝山区 张庙街道 共和新路5895号，200431
                        </dd>
                    </dl>
                </div>
                <hr>
                <div class="addr_and_note1">
                    <h3>
                        其他信息</h3>
                    <dl>
                        <dt>相关证件：</dt>
                        <dd>
                            木木
                        </dd>
                        <dt>其他信息：</dt>
                        <dd>
                            木木，2132142342 ，上海 上海市 宝山区 张庙街道 共和新路5895号，200431 木木，2132142342 ，上海 上海市 宝山区 张庙街道 共和新路5895号，200431
                            木木，2132142342 ，上海 上海市 宝山区 张庙街道 共和新路5895号，200431 木木，2132142342 ，上海 上海市 宝山区 张庙街道 共和新路5895号，200431
                            木木，2132142342 ，上海 上海市 宝山区 张庙街道 共和新路5895号，200431 木木，2132142342 ，上海 上海市 宝山区 张庙街道 共和新路5895号，200431
                        </dd>
                    </dl>
                    <div class="clearfix">
                    </div>
                </div>--%>
            </div>
        </div>
    </div>
    <br />
    <br />
    <br />
    <div class="formbody">
        <div id="usual1" class="usual">
            <div id="tab2" class="tabson">
                <table id="tab" runat="server" width="90%" style="margin: 0 auto;">
                    <tr>
                        <td height="25" align="center" bgcolor="#FFFFFF">
                            <asp:Button runat="server" ID="btnEdit" CssClass="btn1 btn-xs btn-default" Text="编辑" OnClick="btnEdit_Click" />
                            &nbsp;&nbsp;<asp:Button runat="server" ID="Button3" CssClass="btn1 btn-xs btn-default"
                                OnClientClick="javascript:history.go(-1);return false;" Text="返回" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <br />
    <br />
    <br />
    </form>
</body>
</html>
