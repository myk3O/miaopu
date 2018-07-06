<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImgMain.aspx.cs" Inherits="Admin_ImgConfig_ImgMain" %>

<%@ Register Src="../../Control/Pagination.ascx" TagName="Pagination" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>广告列表</title>
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
                <li>广告列表</li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div id="tab2" class="tabson">
                    <div style="display: none">
                        <ul class="seachform">
                            <li>
                                <label>
                                    所属分类</label><asp:DropDownList runat="server" ID="DropGroup" OnSelectedIndexChanged="DropState_SelectedIndexChanged">
                                    </asp:DropDownList>
                            </li>
                            <li>
                                <label>
                                    &nbsp;</label><asp:Button runat="server" ID="btnSearh" CssClass="btn btn-xs btn-default"
                                        Text="查询" OnClick="btnSearh_Click" />
                            </li>
                        </ul>
                    </div>
                    <table class="tablelist">
                        <tr>
                            <td>
                                <asp:GridView ID="gridField" runat="server" Width="100%" CssClass="gridview_m" EmptyDataText=""
                                    HeaderStyle-Font-Size="13px" AllowPaging="True" DataKeyNames="id" HeaderStyle-CssClass="headbackground"
                                    AutoGenerateColumns="False" CellPadding="0" CellSpacing="0" OnRowDataBound="gridField_RowDataBound"
                                    OnRowCommand="gridField_RowCommand" OnDataBound="gridField_DataBound" PageSize="4">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckBox2" runat="server" onclick="SelectAllCheckboxes(this)" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckSel" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="8%" Font-Size="13px" />
                                        </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="所属分类" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="tclassname" runat="server" Text='<%#Eval("tClassName") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" Font-Size="13px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="图片名称" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="title" runat="server" Text='<%#Eval("tilte") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" Font-Size="13px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="图片" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <asp:Image ID="img1" ImageUrl='<%#Eval("imgUrl") %>' runat="server" Width="90%" />
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" Font-Size="13px" Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="链接" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="tMemo" runat="server" Text='<%#Eval("tMemo") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" Font-Size="13px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="排序" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="nPaiXu" runat="server" Text='<%#Eval("nPaiXu") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" Font-Size="13px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="发布时间" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="dtPubTime" runat="server" Text='<%# Eval("dtAddTime") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="管    理" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Button runat="server" ID="btnUpdate" CssClass="btn2 btn-xs btn-default" Text="编辑"
                                                        CausesValidation="False" CommandName="update" CommandArgument='<%# Eval("id")%>' />
                                                    <asp:Button runat="server" ID="btnDelete" CssClass="btn2 btn-xs btn-default" Text="删除"
                                                        CausesValidation="False" CommandName="del" CommandArgument='<%# Eval("id")%>' />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="20%" />
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
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="imgdelete" class="btn btn-xs btn-default" Text="删除"
                        OnClick="imgdelete_Click" />
                    <br />
                    <asp:HiddenField ID="hid" runat="server" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
