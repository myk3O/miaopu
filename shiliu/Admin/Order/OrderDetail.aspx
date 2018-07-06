<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderDetail.aspx.cs" Inherits="Admin_Order_OrderDetail" %>

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
                <%--          <hr>
               <div class="misc-info">
                    <h3>
                        订单状态</h3>
                    <dl>
                        <dt>订单状态：</dt>
                        <dd>
                            等待确认
                        </dd>
                    </dl>
                    <div class="clearfix">
                    </div>
                </div>
                <hr>
                <!-- 订单信息 -->
                <div class="misc-info">
                    <h3>
                        订单信息</h3>
                    <dl>
                        <dt>订单编号：</dt>
                        <dd>
                            924037379402258
                        </dd>
                        <dt>下单时间：</dt>
                        <dd>
                            2015-01-04 14:32:55
                        </dd>
                    </dl>
                    <dl>
                        <dt>申请方：</dt>
                        <dd>
                            ddddd</dd>
                        <dt>处理方：</dt>
                        <dd>
                            大厦d</dd>
                    </dl>
                    <dl>
                        <dt>数量：</dt>
                        <dd>
                            100</dd>
                    </dl>
                    <div class="clearfix">
                    </div>
                </div>
                <hr>
                <div class="addr_and_note1">
                    <h3>
                        收款账号</h3>
                    <dl>
                        <dt>账号类型：</dt>
                        <dd>
                            支付宝
                        </dd>
                    </dl>
                    <dl>
                        <dt>账号：</dt>
                        <dd>
                            1231231231321312
                        </dd>
                    </dl>
                    <dl>
                        <dt>户名：</dt>
                        <dd>
                            张三
                        </dd>
                    </dl>
                    <dl>
                        <dt>开户行：</dt>
                        <dd>
                            建行上海
                        </dd>
                    </dl>
                    <div class="clearfix">
                    </div>
                </div>
                <hr>
                <div class="addr_and_note">
                    <dl>
                        <dt>收货地址： </dt>
                        <dd>
                            木木，2132142342 ，上海 上海市 宝山区 张庙街道 共和新路5895号，200431
                        </dd>
                    </dl>
                </div>
                <hr>
                <div class="addr_and_note1">
                    <h3>
                        退货申请</h3>
                    <dl>
                        <dt>退货数量：</dt>
                        <dd>
                            100
                        </dd>
                    </dl>
                    <dl>
                        <dt>退货原因：</dt>
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
    <div class="formbody">
        <div id="divKuaiDi" class="usual" runat="server" visible="false">
            <div id="Div2" class="tabson">
                <table id="Table1" runat="server" class="tablelistx" width="100%" style="margin: 0 auto;">
                    <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                        <td align="right">
                            快递名称：
                        </td>
                        <td align="left">
                            <%--<asp:TextBox runat="server" ID="txtkuaidi" CssClass="scinputx"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*请输入"
                                ForeColor="Red" ControlToValidate="txtkuaidi" ValidationGroup="btnQr"></asp:RequiredFieldValidator>--%>
                            <asp:DropDownList ID="dpkuaidi" runat="server">
                                <asp:ListItem Selected="True" Text="顺丰">顺丰</asp:ListItem>
                                <asp:ListItem Text="申通">申通</asp:ListItem>
                                <asp:ListItem Text="圆通">圆通</asp:ListItem>
                                <asp:ListItem Text="韵达">韵达</asp:ListItem>
                                <asp:ListItem Text="邮政小包">邮政小包</asp:ListItem>
                                <asp:ListItem Text="中通">中通</asp:ListItem>
                                <asp:ListItem Text="汇通">汇通</asp:ListItem>
                                <asp:ListItem Text="EMS">EMS</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="r234" runat="server" ErrorMessage="*请选择" ForeColor="Red"
                                ControlToValidate="dpkuaidi" InitialValue="-1" ValidationGroup="btnQr"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="border-bottom: 1px solid rgba(0,0,0,0.1);">
                        <td align="right">
                            快递单号：
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtKdCode" CssClass="scinputx"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="r88" runat="server" ErrorMessage="*请输入" ForeColor="Red"
                                ControlToValidate="txtKdCode" ValidationGroup="btnQr"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
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
                            <asp:Button runat="server" ID="btnQr" CssClass="btn1 btn-xs btn-default" OnClientClick="return confirm('确认发货')"
                                Text="确认发货" OnClick="btnQr_Click" ValidationGroup="btnQr" Visible="false" />
                            &nbsp;&nbsp;<asp:Button runat="server" ID="btnJJ" CssClass="btn1 btn-xs btn-default"
                                Visible="false" OnClientClick="return confirm('请确保仓库已经收到用户退货')" Text="确认退货" OnClick="btnJJ_Click" />
                            &nbsp;&nbsp;<asp:Button runat="server" ID="btnTHQ" CssClass="btn1 btn-xs btn-default"
                                Visible="false" OnClientClick="return confirm('确认已收到客户回寄商品，并且已退款用户')" Text="确认退款"
                                OnClick="btnTHQ_Click" />
                            &nbsp;&nbsp;<asp:Button runat="server" ID="btnTHJ" CssClass="btn1 btn-xs btn-default"
                                Visible="false" OnClientClick="return confirm('拒绝用户无理申请，直接完成交易')" Text="拒绝退款申请"
                                OnClick="btnTHJ_Click" />
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
