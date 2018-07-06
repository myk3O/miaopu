<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailConfig.aspx.cs" Inherits="Admin_EmailConfig" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link re="stylesheet" type="text/css" href="../Content/default.css" />
    <title></title>
     <style type="text/css">
        .style1 {
            FONT-WEIGHT: 400;
            FONT-FAMILY: "宋体";
            FONT-SIZE: 12px;
            text-align: right;
            border-top: 0px solid;
            border-spacing: 0px solid;
            height: 28px;
        }
     </style>
</head>
<body style="margin-bottom:0px;margin-left:0px;margin-right:0px;margin-top:0px;">
<table align="center" width="100%" border="0" cellspacing="1" cellpadding="0">
  <tr class="c0">
    <td height="25" align="center" style="font-size:14px;font-weight:bold" background="../Images/bg.gif">邮件基本设置</td>
  </tr>
</table>
    <form id="mlform" runat="server">
<table align="center" width="80%" border="0" cellspacing="1" cellpadding="0" bgcolor="d5d4d4">
      <tr>
        <td height="25" align="center" bgcolor="#FFFFFF" class="style1">发件服务器：</td>
        <td bgcolor="#FFFFFF" style="font-size:12px;">
            <asp:TextBox ID="txtStmp" runat="server" Width="300px"></asp:TextBox>
            (例如:smtp.qq.com/smtp.163.com)</td>
      </tr>
      <tr>
        <td height="25" align="center" bgcolor="#FFFFFF" class="style1">发件邮箱：</td>
        <td bgcolor="#FFFFFF" style="font-size:12px;">
            <asp:TextBox ID="txtFemail" runat="server" Width="300px"></asp:TextBox>
          </td>
      </tr>
      <tr>
        <td width="35%" height="25" align="center" bgcolor="#FFFFFF" class="style1">发件密码：</td>
        <td width="65%" bgcolor="#FFFFFF" style="font-size:12px;">
            <asp:TextBox ID="txtFpass" runat="server" Width="300px" TextMode="Password"></asp:TextBox>
          </td>
      </tr>
      <tr>
        <td height="25" align="center" bgcolor="#FFFFFF" class="style1">收件邮箱：</td>
        <td bgcolor="#FFFFFF" style="font-size:12px;">
            <asp:TextBox ID="txtSemail" runat="server" Width="300px"></asp:TextBox>
          </td>
      </tr>
      <tr>
        <td height="25" align="center" bgcolor="#FFFFFF" class="style1">邮件标题：</td>
        <td bgcolor="#FFFFFF" style="font-size:12px;">
            <asp:TextBox ID="txtEmailname" runat="server" Width="300px"></asp:TextBox>
          </td>
      </tr>
<!--      <tr>
        <td height="25" align="center" bgcolor="#FFFFFF">滚动信息：</td>
        <td bgcolor="#FFFFFF" style="font-size:12px;"><textarea name="tPayMemo" cols="50" rows="5" ></textarea></td>
      </tr>
-->      <tr>
        <td colspan="2" height="25" align="center" bgcolor="#FFFFFF">
            <asp:ImageButton ID="ImgbtnSub" runat="server" ImageUrl="~/Img/submmit.jpg" OnClick="ImgbtnSub_Click" />
          </td>
    </tr>
</table>
        <asp:HiddenField ID="hid" runat="server" />
    </form>
</body>
</html>
