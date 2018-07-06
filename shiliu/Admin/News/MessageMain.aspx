<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageMain.aspx.cs" Inherits="Admin_News_MessageMain" %>

<%@ Register Src="../../Control/Pagination.ascx" TagName="Pagination" TagPrefix="uc1" %>
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
                <li>消息列表</li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div id="tab2" class="tabson">
                    <div>
                        <ul class="seachform">
                            <li>
                                <label>
                                    学员姓名</label><asp:TextBox ID="txtUserName" runat="server" CssClass="scinputx"></asp:TextBox></li>
                            <li>
                                <label>
                                    年级</label><asp:TextBox ID="txtClass" runat="server" CssClass="scinputx"></asp:TextBox></li>
                            <li>
                                <label>
                                    用户阅读通知</label><asp:DropDownList runat="server" ID="DropGroup">
                                        <asp:ListItem Value="0" Selected="True">未阅读</asp:ListItem>
                                        <asp:ListItem Value="1">已阅读</asp:ListItem>
                                    </asp:DropDownList>
                            </li>
                            <li>
                                <label>
                                    是否已处理</label><asp:DropDownList runat="server" ID="DropName">
                                        <asp:ListItem Value="0" Selected="True">未处理</asp:ListItem>
                                        <asp:ListItem Value="1">已处理</asp:ListItem>
                                    </asp:DropDownList>
                            </li>
                            <li>
                                <label>
                                    &nbsp;</label><asp:Button runat="server" ID="btnSearh" CssClass="btn btn-xs btn-default"
                                        Text="查询" OnClick="btnSearh_Click" />
                                <%--<input name="" type="button" class="scbtn" value="查询" />--%></li>
                        </ul>
                    </div>
                    <table class="tablelist">
                        <tr>
                            <td>
                                <asp:GridView ID="gridField" runat="server" Width="100%" CssClass="gridview_m" EmptyDataText=""
                                    HeaderStyle-Font-Size="13px" AllowPaging="True" DataKeyNames="nID" HeaderStyle-CssClass="headbackground"
                                    AutoGenerateColumns="False" CellPadding="0" CellSpacing="0" OnRowDataBound="gridField_RowDataBound"
                                    OnDataBound="gridField_DataBound" OnRowCommand="gridField_RowCommand" PageSize="15">
                                    <Columns>
                                        <%--         <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckBox2" runat="server" onclick="SelectAllCheckboxes(this)" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckSel" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="8%" Font-Size="13px" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="信息标题" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbtitle" runat="server" Text='<%#Eval("tTitle") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="发布时间" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblPubtime" runat="server" Text='<%#Eval("dtPubTime") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="学员姓名" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbrelname" runat="server" Text='<%#Eval("tRealName") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="年级" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbclass" runat="server" Text='<%#Eval("MBusiness") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="家长姓名" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbname" runat="server" Text='<%#Eval("MemberName") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="联系电话" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbphone" runat="server" Text='<%#Eval("MemberPhone") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="是否已阅" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbread" runat="server" Text='<%#Eval("oHide") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="6%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="是否处理" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbdo" runat="server" Text='<%#Eval("sid2") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="6%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="管    理" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Button runat="server" ID="btnDelete" Width="80px" CssClass="btn2 btn-xs btn-default"
                                                        Text="短信提醒" CausesValidation="False" CommandName="del" CommandArgument='<%# Eval("nID")%>' />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="headbackground" Font-Bold="True" Height="25px"></HeaderStyle>
                                    <PagerSettings Visible="False" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <uc1:Pagination ID="Pagination2" runat="server" />
                    <br />
                    <br />
                    <table id="tab" runat="server" class="tablelistx" width="100%" style="margin: 0 auto;">
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td width="25%" align="right" class="auto-style3">
                                消息级别：
                            </td>
                            <td width="75%" align="left" class="auto-style3">
                                <ul class="seachformA">
                                    <li>
                                        <asp:DropDownList ID="DropDownList1" runat="server">
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </td>
                        </tr>
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td width="25%" align="right">
                                消息标题：
                            </td>
                            <td width="75%" align="left">
                                <asp:TextBox ID="txtTlitle" runat="server" Width="350px" class="scinputx"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td align="right">
                                消息内容：
                            </td>
                            <td align="left">
                                <div style="margin-left: 11px">
                                    <textarea id="content1" ruant="sever" cols="100" rows="8" class="scinputx" style="width: 700px;
                                        height: 200px; visibility: hidden;" runat="server"></textarea>
                                    <%--<asp:TextBox runat="server" ID="txtfenleiName" CssClass="scinputx"></asp:TextBox>--%>
                                </div>
                            </td>
                        </tr>
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td align="right">
                                置顶状态：
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadioState" runat="server" RepeatDirection="Horizontal"
                                    Font-Size="10px" Width="113px">
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
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
                <div align="center" class="bootstrap-admin-panel-content">
                    <asp:Button runat="server" ID="btnAdd" class="btn btn-xs btn-default" Text="添加消息"
                        OnClick="btnAdd_Click" />
                    <br />
                    <asp:HiddenField ID="hid" runat="server" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
