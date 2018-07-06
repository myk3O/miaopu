<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetConfig.aspx.cs" Inherits="Admin_SetConfig" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/select.css" rel="stylesheet" type="text/css" />
    <!--编辑器 -->
    <script language="javascript" type="text/javascript" src="My97DatePicker/WdatePicker.js"></script>
    <link rel="stylesheet" href="../Kindeditor/themes/default/default.css" />
    <link rel="stylesheet" href="../Kindeditor/plugins/code/prettify.css" />
    <script charset="utf-8" src="../Kindeditor/kindeditor.js"></script>
    <script charset="utf-8" src="../Kindeditor/lang/zh_CN.js"></script>
    <script charset="utf-8" src="../Kindeditor/plugins/code/prettify.js"></script>
    <script type="text/javascript" src="../Scripts/JSshowHand.js"></script>
    <script>
        KindEditor.ready(function (K) {
            var editor1 = K.create('#content1', {
                cssPath: '../Kindeditor/plugins/code/prettify.css',
                uploadJson: '../Kindeditor/asp.net/upload_json.ashx',
                fileManagerJson: '../Kindeditor/asp.net/file_manager_json.ashx',
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">首页</a></li>
                <li><a href="#">网站信息设置</a></li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div>
                </div>
            </div>
        </div>
        <table id="tab" runat="server" class="tablelistx" width="100%" style="margin: 0 auto;">
            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                <td width="30%" align="right" class="auto-style3">
                    网站地址：
                </td>
                <td width="70%" align="left" class="auto-style3">
                    <asp:TextBox runat="server" ID="txtAdress" CssClass="scinputx" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                <td align="right">
                    网站名称：
                </td>
                <td align="left">
                    <asp:TextBox runat="server" ID="txtName" CssClass="scinputx" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                <td align="right">
                    网站描述：
                </td>
                <td align="left">
                    <%--<input name="" runat="server" id="txtPass" type="password" class="scinputx" />--%>
                    <asp:TextBox runat="server" ID="txtCont" CssClass="scinputx" TextMode="MultiLine"
                        Height="80px" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                <td align="right">
                    网站关键字：
                </td>
                <td align="left">
                    <asp:TextBox runat="server" ID="txtKey" CssClass="scinputx" TextMode="MultiLine"
                        Height="80px" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                <td align="right">
                    免责声明：
                </td>
                <td align="left">
                    <div style="margin-left: 11px">
                        <textarea id="content1" ruant="sever" cols="100" rows="8" class="scinputx" style="width: 700px;
                            height: 200px; visibility: hidden;" runat="server"></textarea>
                        <%--<asp:TextBox runat="server" ID="txtfenleiName" CssClass="scinputx"></asp:TextBox>--%>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" height="25" align="center" bgcolor="#FFFFFF">
                    &nbsp;<asp:Button runat="server" ID="btnSub" CssClass="btn1 btn-xs btn-default" Text="确定"
                        OnClick="btnSub_Click" />
                    &nbsp;<asp:Button runat="server" ID="btnClose" CssClass="btn1 btn-xs btn-default"
                        Text="取消" OnClick="btnClose_Click" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hid" runat="server" />
        <asp:HiddenField ID="hid2" runat="server" />
    </div>
    </form>
</body>
</html>
