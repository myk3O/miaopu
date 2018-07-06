<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImgEdit.aspx.cs" Inherits="Admin_ImgEdit" %>

<%@ Register Src="ProductSelect.ascx" TagName="Pagination" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑首页广告图</title>
    <link href="../../css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/select.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">首页</a></li>
                <li>首页广告图</li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div id="tab2" class="tabson">
                    <table id="tab" runat="server" class="tablelistx" width="70%" style="margin: 0 auto;">
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td width="25%" align="right" class="auto-style3">
                                所属分类：
                            </td>
                            <td width="75%" align="left" class="auto-style3">
                                <ul class="seachformA">
                                    <li>
                                        <asp:DropDownList ID="DropGroup" runat="server">
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </td>
                        </tr>
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td align="right">
                                图片名称：
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtPicName" CssClass="scinputx"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td width="25%" align="right" class="auto-style1">
                                广告图片：
                            </td>
                            <td width="75%" align="left" class="auto-style1">
                                <div style="margin-left: 11px">
                                    <asp:FileUpload ID="FileUpload1" runat="server" class="scinputx" Width="350px" /><a
                                    style="color: Red">(*图片大小建议不超过1M)</a>
                                </div>
                            </td>
                        </tr>
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td align="right">
                                链接地址：
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtUrl" ReadOnly="true" CssClass="scinputx" Visible="false"></asp:TextBox>
                                <asp:Button runat="server" ID="btnSelect" CssClass="btn1 btn-xs btn-default" Text="点击选择"
                                    OnClick="btnSelect_Click" />
                            </td>
                        </tr>
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td align="right">
                                排序：
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtNum" CssClass="scinputx"></asp:TextBox>
                            </td>
                        </tr>
                        <%--            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td align="right">
                                发布时间：
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ReadOnly="true" ID="txtPubtime" CssClass="scinputx" onClick="WdatePicker()"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr>
                            <td colspan="2" height="25" align="center" bgcolor="#FFFFFF">
                                <asp:Button runat="server" ID="ImgbtnSub" CssClass="btn1 btn-xs btn-default" Text="确定"
                                    OnClick="imgSub_Click" />
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
    <asp:HiddenField ID="hidSp" runat="server" />
    </form>
</body>
</html>
