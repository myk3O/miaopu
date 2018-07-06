<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminEdit.aspx.cs" Inherits="Admin_AdminManag_AdminEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/select.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/jquery.js"></script>
    <script type="text/javascript" src="../../js/jquery.idTabs.min.js"></script>
    <script type="text/javascript" src="../../js/select-ui.min.js"></script>
    <script type="text/javascript" src="../../editor/kindeditor.js"></script>
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
    </script>
    <style type="text/css">
        .auto-style3 {
            height: 35px;
        }
    </style>
    <script type="text/javascript">
        function check() {
            var password = document.getElementById("txtpass");
            var passagion = document.getElementById("txtpassagion");
            if (password.value != passagion.value) {
                document.getElementById("lbltishi4").innerText = "两次密码输入不一致";
                return;
            } else {
                document.getElementById("lbltishi4").innerText = "";
            }
        }
        function check1() {
            var password = document.getElementById("txtpass");
            var passagion = document.getElementById("txtpassagion");
            if (password.value == "") {
                document.getElementById("lbltishi3").innerText = "用户密码不能为空！";
                return;
            } else {
                document.getElementById("lbltishi3").innerText = "";
            }
        }
        function check2() {
            var txtname = document.getElementById("txtname");
            if (txtname.value == "") {
                document.getElementById("lbltishi2").innerText = "用户名不能为空！";
                return;
            } else {
                document.getElementById("lbltishi2").innerText = "";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="place">
                <span>位置：</span>
                <ul class="placeul">
                    <li><a href="#">首页</a></li>
                    <li>编辑管理员信息</li>
                </ul>
            </div>
            <div class="formbody">
                <div id="usual1" class="usual">
                    <div>
                    </div>
                </div>
                <script type="text/javascript">
                    $("#usual1 ul").idTabs();
                </script>
                <script type="text/javascript">
                    $('.tablelist tbody tr:odd').addClass('odd');
                </script>
            </div>
        </div>
        <table id="tab" runat="server" class="tablelistx" width="50%" style="margin: 0 auto;">
            <%--      <tr style="border-bottom:1px solid rgba(0,0,0,0.1)">
                <td width="40%" align="right" class="auto-style3">所属群组：</td>
                <td width="60%" align="left" class="auto-style3">
                    <asp:DropDownList ID="dropGroup" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>--%>
            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                <td align="right">用 户 名：
                </td>
                <td align="left">
                    <input name="" runat="server" id="txtName" type="text" class="scinputx" />
                    <asp:RequiredFieldValidator ID="r1" runat="server" ErrorMessage="*请输入" ForeColor="Red"
                        ControlToValidate="txtName" ValidationGroup="btn"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                <td align="right">用户密码：
                </td>
                <td align="left">
                    <%--<input name="" runat="server" id="txtPass" type="password" class="scinputx" />--%>
                    <asp:TextBox runat="server" ID="txtPass" CssClass="scinputx" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*请输入" ForeColor="Red"
                        ControlToValidate="txtPass" ValidationGroup="btn"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                <td align="right">确认密码：
                </td>
                <td align="left">
                    <asp:TextBox runat="server" ID="txtPassAgan" CssClass="scinputx" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*请输入" ForeColor="Red"
                        ControlToValidate="txtPassAgan" ValidationGroup="btn"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                <td align="right">真实姓名：
                </td>
                <td align="left">
                    <input name="" runat="server" id="txtRealName" type="text" class="scinputx" />
                </td>
            </tr>
            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                <td align="right">审核状态：
                </td>
                <td align="left">
                    <asp:CheckBox ID="ckb" Checked="true" runat="server" Text=" 通过审核" />
                </td>
            </tr>
            <tr>
                <td colspan="2" height="25" align="center" bgcolor="#FFFFFF">&nbsp;<asp:Button runat="server" ID="btnSub" CssClass="btn1 btn-xs btn-default" Text="确定"
                    OnClick="btnSub_Click" ValidationGroup="btn" />
                    &nbsp;<asp:Button runat="server" ID="btnClose" CssClass="btn1 btn-xs btn-default"
                        Text="取消" OnClick="btnClose_Click" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hid" runat="server" />
    </form>
</body>
</html>
