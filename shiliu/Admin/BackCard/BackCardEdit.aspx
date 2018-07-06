<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BackCardEdit.aspx.cs" Inherits="Admin_BackCard_BackCardEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/select.css" rel="stylesheet" type="text/css" />
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
                <li>编辑收款账号</li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div id="tab2" class="tabson">
                    <table id="tab" runat="server" class="tablelistx" width="100%" style="margin: 0 auto;">
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1);">
                            <td align="right">
                                账号类型名称（如：建行/支付宝）：
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtType" CssClass="scinputx" Width="50%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*请输入"
                                    ForeColor="Red" ControlToValidate="txtType" ValidationGroup="btn"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td align="right">
                                账号：
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtzh" CssClass="scinputx"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*请输入"
                                    ForeColor="Red" ControlToValidate="txtzh" ValidationGroup="btn"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1);">
                            <td align="right">
                                户名/姓名：
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtName" CssClass="scinputx"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="r88" runat="server" ErrorMessage="*请输入" ForeColor="Red"
                                    ControlToValidate="txtName" ValidationGroup="btn"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1);">
                            <td align="right">
                                开户行：
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtbank" CssClass="scinputx"></asp:TextBox><span>（支付宝类型可不填）</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="25" align="center" bgcolor="#FFFFFF">
                                <asp:Button runat="server" ID="ImgbtnSub" CssClass="btn1 btn-xs btn-default" Text="确定"
                                    OnClick="btnSub_Click" ValidationGroup="btn" />
                                &nbsp;&nbsp;<asp:Button runat="server" ID="imgs" CssClass="btn1 btn-xs btn-default"
                                    Text="取消" OnClick="imgback_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hid" runat="server" />
    </form>
</body>
</html>
