<%@ Page Language="C#" AutoEventWireup="true" CodeFile="biliConfig.aspx.cs" Inherits="Admin_biliConfig" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="place">
                <span>位置：</span>
                <ul class="placeul">
                    <li><a href="#">首页</a></li>
                    <li><a href="#">分配比例设置</a></li>
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
                    <td width="30%" align="right" class="auto-style3">一班佣金：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="leave1" CssClass="scinputx" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="r5" runat="server" ErrorMessage="*不能为空" ForeColor="Red"
                            ControlToValidate="leave1" ValidationGroup="btn"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                    <td align="right">二班佣金：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="leave2" CssClass="scinputx" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*不能为空" ForeColor="Red"
                            ControlToValidate="leave2" ValidationGroup="btn"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                    <td align="right">三班佣金：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="leave3" CssClass="scinputx" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*不能为空" ForeColor="Red"
                            ControlToValidate="leave3" ValidationGroup="btn"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                    <td width="30%" align="right" class="auto-style3">班长奖学金：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="leave_1" CssClass="scinputx" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*不能为空" ForeColor="Red"
                            ControlToValidate="leave_1" ValidationGroup="btn"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                    <td align="right">班主任奖学金：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="leave_2" CssClass="scinputx" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*不能为空" ForeColor="Red"
                            ControlToValidate="leave_2" ValidationGroup="btn"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                    <td align="right">校长奖学金：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="leave_3" CssClass="scinputx" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*不能为空" ForeColor="Red"
                            ControlToValidate="leave_3" ValidationGroup="btn"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                    <td width="30%" align="right" class="auto-style3">班长条件(人)：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="leave1count" CssClass="scinputx" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*不能为空" ForeColor="Red"
                            ControlToValidate="leave1count" ValidationGroup="btn"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                    <td align="right">班主任条件(人)：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="leave2count" CssClass="scinputx" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*不能为空" ForeColor="Red"
                            ControlToValidate="leave2count" ValidationGroup="btn"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                    <td align="right">校长条件(人)：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="leave3count" CssClass="scinputx" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*不能为空" ForeColor="Red"
                            ControlToValidate="leave3count" ValidationGroup="btn"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                     <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                    <td width="30%" align="right" class="auto-style3">班长津贴：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="jixiao3" CssClass="scinputx" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*不能为空" ForeColor="Red"
                            ControlToValidate="jixiao3" ValidationGroup="btn"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                    <td width="30%" align="right" class="auto-style3">班主任津贴：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="jixiao1" CssClass="scinputx" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*不能为空" ForeColor="Red"
                            ControlToValidate="jixiao1" ValidationGroup="btn"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                    <td align="right">校长津贴：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="jixiao2" CssClass="scinputx" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*不能为空" ForeColor="Red"
                            ControlToValidate="jixiao2" ValidationGroup="btn"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                    <td align="right">购物币比例：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="learnPart" CssClass="scinputx" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*不能为空" ForeColor="Red"
                            ControlToValidate="learnPart" ValidationGroup="btn"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                    <td align="right">税费：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="shuifei" CssClass="scinputx" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*不能为空" ForeColor="Red"
                            ControlToValidate="shuifei" ValidationGroup="btn"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                    <td align="right">学霸排名个数(前几名)：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="xuebaCount" CssClass="scinputx" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*不能为空" ForeColor="Red"
                            ControlToValidate="xuebaCount" ValidationGroup="btn"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                       <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                    <td align="right">首次关注赠送学习币：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="txtgzFirst" CssClass="scinputx" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*不能为空" ForeColor="Red"
                            ControlToValidate="txtgzFirst" ValidationGroup="btn"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                       <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                    <td align="right">推荐关注赠送学分：
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="txtgzPart" CssClass="scinputx" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="*不能为空" ForeColor="Red"
                            ControlToValidate="txtgzPart" ValidationGroup="btn"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" height="25" align="center" bgcolor="#FFFFFF">&nbsp;
                        <asp:Button runat="server" ID="btnSub" CssClass="btn1 btn-xs btn-default" Text="确定" ValidationGroup="btn"
                            OnClick="btnSub_Click" />
                        &nbsp;<asp:Button runat="server" ID="btnClose" CssClass="btn1 btn-xs btn-default"
                            Text="取消" OnClick="btnClose_Click" Visible="false" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hid" runat="server" />
        </div>
    </form>
</body>
</html>
