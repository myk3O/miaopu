<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Member_Edit.aspx.cs" Inherits="Admin_Members_Member_Edit" %>

<%@ Register Assembly="Brettle.Web.NeatUpload.HashedInputFile" Namespace="Brettle.Web.NeatUpload"
    TagPrefix="HashedUpload" %>
<%@ Register Assembly="Brettle.Web.NeatUpload" Namespace="Brettle.Web.NeatUpload"
    TagPrefix="Upload" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/select.css" rel="stylesheet" type="text/css" />
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
            var editor1 = K.create('#txtMark', {
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




        function check() {
            var password = document.getElementById("txtpass");
            var passagion = document.getElementById("txtpassagion");
            if (password.value != passagion.value) {
                document.getElementById("lbltishi4").innerText = "两次密码输入不一致！";
                //alert("两次密码输入不一致");
                return;
            }
        }
        function check1() {
            var password = document.getElementById("txtpass");
            var passagion = document.getElementById("txtpassagion");
            if (password.value == "") {
                //alert("用户密码不能为空");
                document.getElementById("lbltishi3").innerText = "用户密码不能为空！";
                return;
            }
        }
        function check2() {
            var txtname = document.getElementById("txtRealname");
            if (txtname.value == "") {
                // alert("用户名不能为空");
                document.getElementById("lbRealname").innerText = "用户名不能为空！";
                return;
            }
        }

        function check3() {
            var txtPhone = document.getElementById("txtPhone");
            if (txtPhone.value == "") {
                // alert("用户名不能为空");
                document.getElementById("lbPhone").innerText = "联系电话不能为空！";
                return;
            }
        }
    </script>
    <style type="text/css">
        .auto-style1
        {
            height: 18px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">首页</a></li>
                <li>编辑会员信息</li>
            </ul>
        </div>
        <input name="ID" id="ID" type="hidden" value="<%= ID %>" />
        <div class="formbody">
            <div id="usual1" class="usual">
                <div id="tab2" class="tabson">
                    <table id="tab" runat="server" class="tablelistx" width="100%" style="margin: 0 auto;">
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td width="25%" align="right">
                                姓名：
                            </td>
                            <td width="75%" align="left">
                                <asp:TextBox ID="txtRealname" runat="server" Width="350px" class="scinputx" onBlur="check2()"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbRealname" runat="server" CssClass="style2" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td width="25%" align="right">
                                联系电话：
                            </td>
                            <td width="75%" align="left">
                                <asp:TextBox ID="txtPhone" runat="server" Width="350px" class="scinputx" onBlur="check3()"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbPhone" runat="server" CssClass="style2" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td width="25%" align="right">
                                就读学校：
                            </td>
                            <td width="75%" align="left">
                                <asp:TextBox ID="txtStudy" runat="server" Width="350px" class="scinputx"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td width="25%" align="right">
                                年级：
                            </td>
                            <td width="75%" align="left">
                                <asp:TextBox ID="txtClass" runat="server" Width="350px" class="scinputx"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td width="25%" align="right">
                                家长姓名：
                            </td>
                            <td width="75%" align="left">
                                <asp:TextBox ID="txtName" runat="server" Width="350px" class="scinputx"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td width="25%" align="right">
                                缴费金额：
                            </td>
                            <td width="75%" align="left">
                                <asp:TextBox ID="txt_feiyong" runat="server" Width="350px" class="scinputx"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td width="25%" align="right">
                                用户密码：
                            </td>
                            <td width="75%" align="left">
                                <asp:TextBox ID="txtpass" runat="server" TextMode="Password" Width="350px" class="scinputx"
                                    onBlur="check1()"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbltishi3" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td width="25%" align="right">
                                确认密码：
                            </td>
                            <td width="75%" align="left">
                                <asp:TextBox ID="txtpassagion" runat="server" TextMode="Password" Width="350px" class="scinputx"
                                    onBlur="check()"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbltishi4" runat="server" CssClass="style2" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td align="right">
                                审核状态：
                            </td>
                            <td>
                                <asp:RadioButtonList ID="ckb" runat="server" RepeatDirection="Horizontal" Font-Size="10px"
                                    Width="113px">
                                    <asp:ListItem Selected="True" Value="1">是</asp:ListItem>
                                    <asp:ListItem Value="0">否</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td align="right">
                                备注：
                            </td>
                            <td align="left">
                                <div style="margin-left: 11px">
                                    <textarea id="txtMark" ruant="sever" cols="100" rows="8" class="scinputx" style="width: 700px;
                                        height: 200px; visibility: hidden;" runat="server"></textarea>
                                    <%--<asp:TextBox runat="server" ID="txtfenleiName" CssClass="scinputx"></asp:TextBox>--%>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="25" align="center" bgcolor="#FFFFFF">
                                <asp:Button runat="server" ID="Submit" CssClass="btn1 btn-xs btn-default" Text="确定"
                                    OnClick="Submit_Click" OnClientClick="" />
                                &nbsp;&nbsp;<asp:Button runat="server" ID="imgback" CssClass="btn1 btn-xs btn-default"
                                    Text="取消" OnClick="imgback_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="25" align="right" bgcolor="#FFFFFF">
                                <asp:FileUpload ID="fuOpen" runat="server" />
                                <font style="color: Red; float: right">仅支持.xls后缀名文件</font>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnUpload" runat="server" CssClass="btn1 btn-xs btn-default" Text="批量导入"
                                    OnClick="btnUpload_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hid" runat="server" />
    <asp:HiddenField ID="hidSp" runat="server" />
    </form>
</body>
</html>
