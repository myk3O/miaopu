<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomeEdit.aspx.cs" Inherits="Admin_DaiLi_HomeEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <style type="text/css">
        body, html
        {
            width: 100%;
            height: 100%;
            margin: 0;
            font-family: "微软雅黑";
            font-size: 14px;
        }
        
        #l-map
        {
            height: 84%;
            width: 100%;
        }
        
        #r-result
        {
            width: 100%;
        }
    </style>
    <%--    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />--%>
    <title></title>
    <link href="../../css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/select.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/jquery.js"></script>
    <script type="text/javascript" src="../../js/jquery.idTabs.min.js"></script>
    <script type="text/javascript" src="../../js/select-ui.min.js"></script>
    <%--    <script type="text/javascript" src="../../editor/kindeditor.js"></script>
    <script type="text/javascript">
        function SelectAllCheckboxes(spanChk) {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
        }


    </script>--%>
    <!--编辑器 -->
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <link rel="stylesheet" href="../../Kindeditor/themes/default/default.css" />
    <link rel="stylesheet" href="../../Kindeditor/plugins/code/prettify.css" />
    <script charset="utf-8" src="../../Kindeditor/kindeditor.js"></script>
    <script charset="utf-8" src="../../Kindeditor/lang/zh_CN.js"></script>
    <script charset="utf-8" src="../../Kindeditor/plugins/code/prettify.js"></script>
    <script type="text/javascript" src="../../Scripts/JSshowHand.js"></script>
    <script>
        KindEditor.ready(function (K) {
            var editor1 = K.create('#content1', {
                cssPath: '../../Kindeditor/plugins/code/prettify.css',
                uploadJson: '../../Kindeditor/asp.net/upload_json.ashx',
                fileManagerJson: '../../Kindeditor/asp.net/file_manager_json.ashx',
                allowFileManager: true,
                afterCreate: function () {
                    var self = this;
                    K.ctrl(document, 13, function () {
                        self.sync();
                        K('form[name=example]')[0].submit();
                    });
                    K.ctrl(self.edit.doc, 13, function () {
                        self.sync();
                        K('form[name=example]')[0].submit();
                    });
                }
            });
            prettyPrint();
        });
    </script>
    <style type="text/css">
        .auto-style3
        {
            height: 35px;
        }
    </style>
    <script type="text/javascript">
        function check() {
            document.getElementById("lab1").style.display = "none";
        }
        function check1() {
            document.getElementById("lab2").style.display = "none";
        }
        function check2() {
            document.getElementById("lab3").style.display = "none";
        }
    </script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=E7a6ff327a126a4f49ff6e8de0d707d7"></script>
    <script src="../../js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <link href="../../css/loginDialog.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function ($) {
            //弹出登录
            $("#example1").hover(function () {
                //$(this).stop().animate({
                //    opacity: '1'
                //}, 600);
            }, function () {
                //$(this).stop().animate({
                //    opacity: '0.6'
                //}, 1000);
            }).on('click', function () {
                //$("body").append("<div id='mask'></div>");
                //$("#mask").addClass("mask").fadeIn("slow");
                $("#LoginBox").fadeIn("slow");
            });
            //
            //按钮的透明度
            $("#loginbtn1").hover(function () {
                $(this).stop().animate({
                    opacity: '1'
                }, 600);
            }, function () {
                $(this).stop().animate({
                    opacity: '0.8'
                }, 1000);
            });
            //文本框不允许为空---按钮触发
            $("#loginbtn1").on('click', function () {
                //$("#hideLng").fadeOut("fast");
            });

            //关闭
            $(".close_btn").hover(function () { $(this).css({ color: 'black' }) }, function () { $(this).css({ color: '#999' }) }).on('click', function () {
                $("#LoginBox").fadeOut("fast");
                $("#mask").css({ display: 'none' });
            });

            $("#loginbtn1").click(function () { $(this).css({ color: 'black' }) }, function () { $(this).css({ color: '#999' }) }).on('click', function () {

                $("#LoginBox").fadeOut("fast");
                $("#mask").css({ display: 'none' });
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">首页</a></li>
                <li>编辑代理商信息</li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div>
                </div>
            </div>
            <script type="text/javascript">
                //  $("#usual1 ul").idTabs();
            </script>
            <script type="text/javascript">
                $('.tablelist tbody tr:odd').addClass('odd');
            </script>
        </div>
    </div>
    <div id="body">
        <div id="LoginBox">
            <div class="row1">
                百度地图定位<a href="javascript:void(0)" title="关闭窗口" class="close_btn" id="closeBtn">×</a>
            </div>
            <div id="r-result">
                搜索:<input type="text" id="suggestId" size="20" value="百度" style="width: 300px;" /></div>
            <div class="row">
                <a href="#" id="loginbtn1" style="font-size: 20px">确定</a>
            </div>
            <div id="l-map">
            </div>
            <div id="searchResultPanel" style="border: 1px solid #C0C0C0; width: 150px; height: auto;
                display: none;">
            </div>
        </div>
    </div>
    <table id="tab" runat="server" class="tablelistx" width="80%" style="margin: 0 auto;">
        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
            <td align="right">
                所在城市：
            </td>
            <td align="left">
                <asp:DropDownList ID="dropSheng" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropSheng_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;<asp:DropDownList ID="dropShi" runat="server">
                </asp:DropDownList>
                <%--<asp:RequiredFieldValidator ID="r2" runat="server" ErrorMessage="*请选择" ForeColor="Red"
                    ControlToValidate="dropShi" InitialValue="-1"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
            <td align="right">
                公司地址：
            </td>
            <td align="left">
                <input name="" runat="server" id="txtAddress" type="text" readonly="readonly" class="scinputx" />
                <a href="#" id="example1">点击定位</a>
                <input id="hideLng" runat="server" value="" type="text" style="display: none" />
                <input id="hideLat" runat="server" value="" type="text" style="display: none" />
            </td>
        </tr>
        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1); display: none">
            <td width="40%" align="right" class="auto-style3">
                编&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 码：
            </td>
            <td align="left">
                <input name="" runat="server" id="txtCode" type="text" readonly="readonly" class="scinputx" />
            </td>
        </tr>
        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1);">
            <td width="40%" align="right" class="auto-style3">
                邮&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 编：
            </td>
            <td align="left">
                <input name="" runat="server" id="txtYouBian" type="text" class="scinputx" />
            </td>
        </tr>
        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1); display: none">
            <td align="right">
                *登&nbsp; 录 名：
            </td>
            <td align="left">
                <input name="" runat="server" id="txtName" type="text" class="scinputx" onfocus="check1()" />
                <asp:RequiredFieldValidator ID="r1" runat="server" ErrorMessage="*请输入" ForeColor="Red"
                    ControlToValidate="txtName" ValidationGroup="btn"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1); display: none">
            <td align="right">
                *登录密码：
            </td>
            <td align="left">
                <%--<input name="" runat="server" id="txtPass" type="password" class="scinputx" />--%>
                <asp:TextBox runat="server" ID="txtPass" CssClass="scinputx" onfocus="check2()"></asp:TextBox>
                <asp:RequiredFieldValidator ID="r3" runat="server" ErrorMessage="*请输入" ForeColor="Red"
                    ControlToValidate="txtPass" ValidationGroup="btn"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1); display: none">
            <td align="right">
                确认密码：
            </td>
            <td align="left">
                <asp:TextBox runat="server" ID="txtPassAgan" CssClass="scinputx" onfocus="check2()"></asp:TextBox>
            </td>
        </tr>
        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1); display: none">
            <td align="right">
                *代理级别：
            </td>
            <td align="left">
                <asp:DropDownList ID="dropDailiSystem" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropDailiSystem_SelectedIndexChanged">
                </asp:DropDownList>
        <%--        <asp:RequiredFieldValidator ID="r234" runat="server" ErrorMessage="*请选择" ForeColor="Red"
                    ControlToValidate="dropDailiSystem" InitialValue="-1" ValidationGroup="btn"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1); display: none">
            <td align="right">
                *上级代理：
            </td>
            <td align="left">
                <asp:DropDownList ID="dropFatherDaili" runat="server">
                </asp:DropDownList>
              <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*请选择"
                    ForeColor="Red" ControlToValidate="dropFatherDaili" ValidationGroup="btn" InitialValue="-1"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
            <td align="right">
                *公司/个人名称：
            </td>
            <td align="left">
                <input name="" runat="server" id="txtCompany" type="text" class="scinputx" />
                <asp:RequiredFieldValidator ID="r5" runat="server" ErrorMessage="*请输入" ForeColor="Red"
                    ControlToValidate="txtCompany" ValidationGroup="btn"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
            <td align="right">
                *联系人：
            </td>
            <td align="left">
                <input name="" runat="server" id="txtlxr" type="text" class="scinputx" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*请输入"
                    ForeColor="Red" ControlToValidate="txtlxr" ValidationGroup="btn"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
            <td align="right">
                相关证件：
            </td>
            <td align="left">
                <div style="margin-left: 11px">
                    <asp:FileUpload ID="FileUpload1" runat="server" class="scinputx" Width="200px" />
                </div>
            </td>
        </tr>
        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
            <td align="right">
                *固定电话：
            </td>
            <td align="left">
                <input name="" runat="server" id="txtPhone" type="text" class="scinputx" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*请输入"
                    ForeColor="Red" ControlToValidate="txtPhone" ValidationGroup="btn"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
            <td align="right">
                *手机：
            </td>
            <td align="left">
                <input name="" runat="server" id="txtMebile" type="text" class="scinputx" />
                <asp:RegularExpressionValidator ID="r6" runat="server" ErrorMessage="*格式错误" ForeColor="Red"
                    ValidationExpression="1[3|5|7|8|][0-9]{9}" ControlToValidate="txtMebile" ValidationGroup="btn"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*请输入"
                    ForeColor="Red" ControlToValidate="txtMebile" ValidationGroup="btn"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
            <td align="right">
                *邮箱：
            </td>
            <td align="left">
                <input name="" runat="server" id="txtEmail" type="text" class="scinputx" />
                <asp:RegularExpressionValidator ID="r77" runat="server" ErrorMessage="*格式错误" ForeColor="Red"
                    ValidationExpression="^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$"
                    ControlToValidate="txtEmail" ValidationGroup="btn"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*请输入"
                    ForeColor="Red" ControlToValidate="txtEmail" ValidationGroup="btn"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
            <td align="right">
                其他信息：
            </td>
            <td align="left">
                <div style="margin-left: 11px">
                    <textarea id="content1" ruant="sever" rows="8" class="scinputx" style="width: 693px;
                        height: 200px; visibility: hidden;" runat="server"></textarea>
                </div>
            </td>
        </tr>
        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
            <td align="right">
                审核状态：
            </td>
            <td align="left">
                <asp:CheckBox ID="ckb" runat="server" Text=" 通过审核" />
            </td>
        </tr>
        <tr>
            <td colspan="2" height="25" align="center" bgcolor="#FFFFFF">
                &nbsp;<asp:Button runat="server" ID="btnSub" CssClass="btn1 btn-xs btn-default" Text="确定"
                    OnClick="btnSub_Click" ValidationGroup="btn" />
                &nbsp;<asp:Button runat="server" ID="btnClose" CssClass="btn1 btn-xs btn-default"
                    Text="取消" OnClick="btnClose_Click" OnClientClick="javascript:history.go(-1);return false;" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hid" runat="server" />
    <asp:HiddenField ID="hid1" runat="server" />
    <asp:HiddenField ID="hid2" runat="server" />
    <asp:HiddenField ID="hid3" runat="server" />
    <%--     <asp:HiddenField ID="hideLnd" runat="server" />
            <asp:HiddenField ID="hideLat" runat="server" />--%>
    </form>
</body>
</html>
<script type="text/javascript">
    // 百度地图API功能
    function G(id) {
        return document.getElementById(id);
    }
    var geoc = new BMap.Geocoder();
    var map = new BMap.Map("l-map");
    map.centerAndZoom("上海", 12);                   // 初始化地图,设置城市和地图级别。
    map.enableScrollWheelZoom();   //启用滚轮放大缩小，默认禁用
    map.enableContinuousZoom();    //启用地图惯性拖拽，默认禁用
    var ac = new BMap.Autocomplete(    //建立一个自动完成的对象
		{
		"input": "suggestId"
		, "location": map
});

    ac.addEventListener("onhighlight", function (e) {  //鼠标放在下拉列表上的事件
        var str = "";
        var _value = e.fromitem.value;
        var value = "";
        if (e.fromitem.index > -1) {
            value = _value.province + _value.city + _value.district + _value.street + _value.business;
        }
        str = "FromItem<br />index = " + e.fromitem.index + "<br />value = " + value;

        value = "";
        if (e.toitem.index > -1) {
            _value = e.toitem.value;
            value = _value.province + _value.city + _value.district + _value.street + _value.business;
        }
        str += "<br />ToItem<br />index = " + e.toitem.index + "<br />value = " + value;
        G("searchResultPanel").innerHTML = str;
    });

    var myValue;
    ac.addEventListener("onconfirm", function (e) {    //鼠标点击下拉列表后的事件
        var _value = e.item.value;
        myValue = _value.province + _value.city + _value.district + _value.street + _value.business;
        G("searchResultPanel").innerHTML = "onconfirm<br />index = " + e.item.index + "<br />myValue = " + myValue;

        setPlace();
    });

    function setPlace() {
        map.clearOverlays();    //清除地图上所有覆盖物
        function myFun() {
            var pp = local.getResults().getPoi(0).point;    //获取第一个智能搜索的结果
            map.centerAndZoom(pp, 18);
            map.addOverlay(new BMap.Marker(pp));    //添加标注
            var lng = pp.lng;
            var lat = pp.lat;
            $("#hideLng").val(lng);
            $("#hideLat").val(lat);
        }
        var local = new BMap.LocalSearch(map, { //智能搜索
            onSearchComplete: myFun
        });
        local.search(myValue);
        $("#txtAddress").val(myValue);
    }
    //单击获取点击的经纬度
    map.addEventListener("click", function (e) {
        //alert(e.point.lng + "," + e.point.lat);
        $("#hideLng").val(e.point.lng);
        $("#hideLat").val(e.point.lat);
        var point = new BMap.Point(e.point.lng, e.point.lat);
        map.clearOverlays();    //清除地图上所有覆盖物
        var marker = new BMap.Marker(point);  // 创建标注
        map.addOverlay(marker);              // 将标注添加到地图中
        //逆地址解析
        var pt = e.point;
        geoc.getLocation(pt, function (rs) {
            var addComp = rs.addressComponents;
            //alert(addComp.province + ", " + addComp.city + ", " + addComp.district + ", " + addComp.street + ", " + addComp.streetNumber);
            var strMark = addComp.province + ", " + addComp.city + ", " + addComp.district + ", " + addComp.street + ", " + addComp.streetNumber;


            var label = new BMap.Label(strMark, { offset: new BMap.Size(20, -10) });
            marker.setLabel(label);
            $("#txtAddress").val(label.content);

        });
    });

</script>
