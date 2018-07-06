<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberEdit.aspx.cs" Inherits="Admin_Member_MemberEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="../../Scripts/JSshowHand.js"></script>
    <style type="text/css">
        .auto-style1
        {
            height: 25px;
        }
        .style1
        {
            font-weight: 400;
            font-family: "宋体";
            font-size: 12px;
            text-align: right;
            border-top: 0px solid;
            border-spacing: 0px solid; /*BACKGROUND: #F5F5F5;*/
            height: 28px;
        }
        .style2
        {
            font-weight: 400;
            font-family: "宋体";
            font-size: 12px;
            text-align: right;
            border-top: 0px solid;
            border-spacing: 0px solid;
            height: 28px;
        }
    </style>
</head>
<script language="javascript" type="text/javascript">
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
        var txtname = document.getElementById("txtRealname");
        if (txtname.value == "") {
            document.getElementById("lbRealname").innerText = "用户名不能为空！";
            return;
        } else {
            document.getElementById("lbRealname").innerText = "";
        }
    }
</script>
<body style="margin-top: 0px; margin-left: 0px; margin-right: 0px;">
    <table align="center" width="100%" border="0" cellspacing="1" cellpadding="0">
        <tr class="c0">
            <td height="25" background="../../Images/bg.gif" align="center" style="font-size: 14px;
                font-weight: bold">
                编辑会员信息
            </td>
        </tr>
    </table>
    <form id="mlform" runat="server">
    <input name="ID" id="ID" type="hidden" value="<%= ID %>" />
    <table align="center" width="80%" border="0" cellspacing="1" cellpadding="0" bgcolor="d5d4d4">
        <%--      <tr>
            <td height="25" align="center" bgcolor="#FFFFFF" class="style1">
                所属群组：
            </td>
            <td bgcolor="#FFFFFF" style="font-size: 12px;">
                <asp:DropDownList ID="dropGroup" runat="server" Width="123px">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>--%>
        <tr>
            <td width="26%" height="25" align="center" bgcolor="#FFFFFF" class="style1">
                姓名：
            </td>
            <td width="74%" bgcolor="#FFFFFF" style="font-size: 12px;">
                <asp:TextBox ID="txtRealname" runat="server" Width="150px" onBlur="check2()"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lbRealname" runat="server" CssClass="style2" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="26%" height="25" align="center" bgcolor="#FFFFFF" class="style1">
                联系电话：
            </td>
            <td width="74%" bgcolor="#FFFFFF" style="font-size: 12px;">
                <asp:TextBox ID="txtPhone" runat="server" Width="150px" onBlur=""></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lbPhone" runat="server" CssClass="style2" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="26%" height="25" align="center" bgcolor="#FFFFFF" class="style1">
                就读学校：
            </td>
            <td width="74%" bgcolor="#FFFFFF" style="font-size: 12px;">
                <asp:TextBox ID="txtStudy" runat="server" Width="150px" onBlur=""></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lbStudy" runat="server" CssClass="style2" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="26%" height="25" align="center" bgcolor="#FFFFFF" class="style1">
                年级：
            </td>
            <td width="74%" bgcolor="#FFFFFF" style="font-size: 12px;">
                <asp:TextBox ID="txtClass" runat="server" Width="150px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lbClass" runat="server" CssClass="style2" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="26%" height="25" align="center" bgcolor="#FFFFFF" class="style1">
                家长姓名：
            </td>
            <td width="74%" bgcolor="#FFFFFF" style="font-size: 12px;">
                <asp:TextBox ID="txtName" runat="server" Width="150px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lbName" runat="server" CssClass="style2" ForeColor="Red"></asp:Label>
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
        <tr>
            <td width="26%" height="25" align="center" bgcolor="#FFFFFF" class="style1">
                备注：
            </td>
            <td width="74%" bgcolor="#FFFFFF" style="font-size: 12px;">
                <asp:TextBox ID="txtMark" runat="server" Width="150px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lbMark" runat="server" CssClass="style2" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" align="center" bgcolor="#FFFFFF" class="style1">
                用户密码：
            </td>
            <td bgcolor="#FFFFFF" style="font-size: 12px;">
                <asp:TextBox ID="txtpass" runat="server" onBlur="check1()" TextMode="Password" Width="150px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lbltishi3" runat="server" CssClass="style2" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" align="center" bgcolor="#FFFFFF" class="style1">
                确认密码：
            </td>
            <td bgcolor="#FFFFFF" style="font-size: 12px;">
                <asp:TextBox ID="txtpassagion" runat="server" onBlur="check()" TextMode="Password"
                    Width="150px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lbltishi4" runat="server" CssClass="style2" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" bgcolor="#FFFFFF" class="style1">
                审核状态：
            </td>
            <td bgcolor="#FFFFFF" style="font-size: 12px;" class="auto-style1">
                <asp:CheckBox ID="ckb" runat="server" Checked="true" Text="通过审核" />
            </td>
        </tr>
        <tr>
            <td colspan="2" height="25" align="center" bgcolor="#FFFFFF">
                &nbsp;<asp:ImageButton ID="ImgbtnSub" runat="server" ImageUrl="~/Img/submmit.jpg"
                    OnClick="Submit_Click" />
                &nbsp;<asp:Image ID="imgs" runat="server" ImageUrl="~/Img/back.jpg" onmouseover="ShowHand('imgs')" />
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#FFFFFF">
                &nbsp;
                <asp:FileUpload ID="fuOpen" runat="server" />
            </td>
            <td height="25" align="right" bgcolor="#FFFFFF">
                &nbsp;
                <asp:Button ID="btnUpload" runat="server" Text="批量导入" OnClick="btnUpload_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
