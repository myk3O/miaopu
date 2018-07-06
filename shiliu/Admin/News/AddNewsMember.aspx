<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNewsMember.aspx.cs" Inherits="Admin_News_AddNewsMember" %>

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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">首页</a></li>
                <li>会员信息列表</li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div id="tab2" class="tabson">
                    <label style="font-size: 14px;">
                        [查询条件]</label>
                    <div>
                        <ul class="seachform">
                            <li>
                                <label>
                                    年级</label><asp:TextBox ID="txtClass" runat="server" CssClass="scinputx"></asp:TextBox></li>
                            <li>
                                <asp:DropDownList ID="DropName" runat="server">
                                    <asp:ListItem Value="1">姓名</asp:ListItem>
                                    <asp:ListItem Value="2">家长姓名</asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li>
                                <asp:TextBox ID="txtName" runat="server" CssClass="scinputx"></asp:TextBox></li>
                            <li>
                                <asp:DropDownList ID="DropState" runat="server">
                                    <asp:ListItem Value="-1">审核状态</asp:ListItem>
                                    <asp:ListItem Value="1">已审核</asp:ListItem>
                                    <asp:ListItem Value="0">未审核</asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li>
                                <label>
                                    &nbsp;</label><asp:Button runat="server" ID="ImgSrs" CssClass="btn btn-xs btn-default"
                                        Text="查询" OnClick="ImgSrs_Click" />
                                <%--<input name="" type="button" class="scbtn" value="查询" />--%></li>
                        </ul>
                    </div>
                    <table class="tablelist">
                        <tr>
                            <td>
                                <asp:GridView ID="gridField" runat="server" Width="100%" CssClass="gridview_m" EmptyDataText=""
                                    HeaderStyle-Font-Size="13px" AllowPaging="True" DataKeyNames="nID" HeaderStyle-CssClass="headbackground"
                                    AutoGenerateColumns="False" CellPadding="0" CellSpacing="0" OnRowDataBound="gridField_RowDataBound"
                                    OnDataBound="gridField_DataBound" OnRowCommand="gridField_RowCommand" PageSize="10">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckBox2" runat="server" onclick="SelectAllCheckboxes(this)" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckSel" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="12%" Font-Size="13px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="姓名" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbRelName" runat="server" Text='<%#Eval("tRealName") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="12%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="联系电话" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbPhone" runat="server" Text='<%#Eval("MemberPhone") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="12%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="就读学校" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbschool" runat="server" Text='<%#Eval("MCompany") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="12%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="年级" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbClass" runat="server" Text='<%#Eval("MBusiness") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="12%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="家长姓名" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("MemberName") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="12%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="审核状态" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblState" runat="server" Text='<%#Eval("oCheck") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="12%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="headbackground" Font-Bold="True" Height="25px"></HeaderStyle>
                                    <PagerSettings Visible="False" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <uc1:Pagination ID="Pagination1" runat="server" />
                </div>
                <div align="center" class="bootstrap-admin-panel-content">
                    <asp:Button runat="server" ID="imgdelete" class="btn btn-xs btn-default" Text="勾选发送"
                        OnClick="imgdelete_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnAdd" class="btn btn-xs btn-default" Text="取消" OnClick="btnAdd_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnSendAll" class="btn btn-xs btn-default" Text="全部发送"
                        OnClick="btnSendAll_Click" OnClientClick="return confirm('是否发送给所有的学员？')" />
                    <br />
                    <asp:HiddenField ID="hid" runat="server" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
