<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QusetionClass.aspx.cs" Inherits="Admin_Questionnaire_QusetionClass" %>

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
    <script type="text/javascript" src="../../js/jquery.js"></script>
    <script type="text/javascript" src="../../js/jquery.idTabs.min.js"></script>
    <script type="text/javascript" src="../../js/select-ui.min.js"></script>
    <script type="text/javascript" src="../../editor/kindeditor.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">首页</a></li>
                <li>问卷分类</li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div id="tab2" class="tabson">
                    <table class="tablelist">
                        <tr>
                            <td>
                                <asp:GridView ID="gridField" runat="server" Width="100%" CssClass="gridview_m" EmptyDataText=""
                                    HeaderStyle-Font-Size="13px" AllowPaging="True" ShowHeader="true" DataKeyNames="nID"
                                    AutoGenerateColumns="False" CellPadding="0" CellSpacing="0" OnDataBound="gridField_DataBound"
                                    OnRowCommand="gridField_RowCommand" OnRowUpdating="gridField_RowUpdating" OnRowDataBound="gridField_RowDataBound" PageSize="10">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="问卷名称" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblfenleis" runat="server" Text='<%#Eval("tClassName") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="添加时间" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblxuhao" runat="server" Text='<%#Eval("dtAddTime") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="是否发布" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbisfabu" runat="server" Text='<%#Eval("sid2") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="管    理" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Button runat="server" ID="Button4" CssClass="btn2 btn-xs btn-default" Text="发布"
                                                        CausesValidation="False" CommandName="fabu" CommandArgument='<%# Eval("nID")%>' />
                                                    <asp:Button runat="server" ID="Button5" CssClass="btn2 btn-xs btn-default" Text="取消"
                                                        CausesValidation="False" CommandName="quxiao" CommandArgument='<%# Eval("nID")%>' />
                                                    <asp:Button runat="server" ID="btnUpdate" CssClass="btn2 btn-xs btn-default" Text="编辑"
                                                        CausesValidation="False" CommandName="update" CommandArgument='<%# Eval("nID")%>' />
                                                    <asp:Button runat="server" ID="btnDelete" CssClass="btn2 btn-xs btn-default" Text="删除"
                                                        CausesValidation="False" CommandName="del" CommandArgument='<%# Eval("nID")%>' />
                                                    <asp:Button runat="server" ID="Button1" CssClass="btn2 btn-xs btn-default" Text="问卷"
                                                        CausesValidation="False" CommandName="Qu" CommandArgument='<%# Eval("nID")%>' />
                                                    <asp:Button runat="server" ID="Button2" CssClass="btn2 btn-xs btn-default" Text="选择"
                                                        CausesValidation="False" CommandName="Toge" CommandArgument='<%# Eval("nID")%>' />
                                                    <asp:Button runat="server" ID="Button3" CssClass="btn2 btn-xs btn-default" Text="问答"
                                                        CausesValidation="False" CommandName="wenda" CommandArgument='<%# Eval("nID")%>' />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="30%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="headbackground" Font-Bold="True" Height="25px"></HeaderStyle>
                                    <PagerSettings Visible="False" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <uc1:Pagination ID="Pagination2" runat="server" />
                </div>
                <div align="center" class="bootstrap-admin-panel-content">
                    <asp:Button runat="server" ID="btnAdd" class="btn btn-xs btn-default" Text="添加" OnClick="btnAdd_Click" />
                </div>
            </div>
            <script type="text/javascript">
                $("#usual1 ul").idTabs();
            </script>
            <script type="text/javascript">
                $('.tablelist tbody tr:odd').addClass('odd');
            </script>
        </div>
        <br />
        <table id="tab" runat="server" class="tablelistx" width="50%" style="margin: 0 auto;">
            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                <td align="right">
                    问卷名称：
                </td>
                <td align="left">
                    <asp:TextBox runat="server" ID="txtfenleiName" CssClass="scinputx"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" height="25" align="center" bgcolor="#FFFFFF">
                    <asp:Button runat="server" ID="imgAdd" CssClass="btn1 btn-xs btn-default" Text="添加"
                        OnClick="imgAdd_Click1" />
                    &nbsp;<asp:Button runat="server" ID="imgSub" CssClass="btn1 btn-xs btn-default" Text="确定"
                        OnClick="imgSub_Click1" />
                    &nbsp;<asp:Button runat="server" ID="imgback" CssClass="btn1 btn-xs btn-default"
                        Text="取消" OnClick="imgback_Click1" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hid" runat="server" />
    </form>
</body>
</html>
