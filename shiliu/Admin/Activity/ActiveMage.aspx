<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActiveMage.aspx.cs" Inherits="Admin_ActiveManag_ActiveMage" %>

<%@ Register Src="../../Control/Pagination.ascx" TagName="Pagination" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>活动列表</title>
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
                    <li>活动列表</li>
                </ul>
            </div>
            <div class="formbody">
                <div id="usual1" class="usual">
                    <div id="tab2" class="tabson">
                        <div>
                            <ul class="seachform">
                                <li>
                                    <label>综合查询</label><input name="" runat="server" id="keyName" type="text" class="scinput" /></li>
                                <li>
                                    <label>所属群组</label><asp:DropDownList runat="server" ID="DropGroup"></asp:DropDownList>
                                </li>
                               <%-- <li>
                                    <label>置顶状态</label><asp:DropDownList runat="server" ID="DropName">
                                        <asp:ListItem Value="-1">请选择</asp:ListItem>
                                        <asp:ListItem Value="1">是</asp:ListItem>
                                        <asp:ListItem Value="0">否</asp:ListItem>
                                    </asp:DropDownList>
                                </li>--%>
                                <li>
                                    <label>&nbsp;</label><asp:Button runat="server" ID="btnSearh" CssClass="btn btn-xs btn-default" Text="查询" OnClick="btnSearh_Click" />
                                    <%--<input name="" type="button" class="scbtn" value="查询" />--%></li>
                            </ul>
                        </div>
                        <table class="tablelist">
                            <tr>
                                <td>
                                    <asp:GridView ID="gridField" runat="server" Width="100%" CssClass="gridview_m"
                                        EmptyDataText="" HeaderStyle-Font-Size="13px" AllowPaging="True"
                                        DataKeyNames="nID" HeaderStyle-CssClass="headbackground" AutoGenerateColumns="False"
                                        CellPadding="0" CellSpacing="0" OnRowDataBound="gridField_RowDataBound" OnDataBound="gridField_DataBound" OnRowCommand="gridField_RowCommand" PageSize="15">
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
                                            <asp:TemplateField HeaderText="活动类别" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lblzixun" runat="server" Text='<%#Eval("tClassNameb") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>

                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="活动标题" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbltlite" runat="server" Text='<%#Eval("tMemo") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>

                                                <HeaderStyle Font-Bold="False"></HeaderStyle>

                                                <ItemStyle HorizontalAlign="Left" Font-Size="13px" Width="" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="活动内容" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="dtMethods" runat="server" Text='<%#Eval("dtActMethods") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>

                                                <HeaderStyle Font-Bold="False"></HeaderStyle>

                                                <ItemStyle HorizontalAlign="Left" Font-Size="13px" Width="" />
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
                                            <asp:TemplateField HeaderText="管    理" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Button runat="server" ID="btnUpdate" CssClass="btn2 btn-xs btn-default" Text="编辑" CausesValidation="False" CommandName="update" CommandArgument='<%# Eval("nID")%>' />
                                                        <asp:Button runat="server" ID="btnDelete" CssClass="btn2 btn-xs btn-default" Text="删除" CausesValidation="False" CommandName="del" CommandArgument='<%# Eval("nID")%>' />
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
                        <asp:Button runat="server" ID="imgdelete" class="btn btn-xs btn-default" Text="删除" OnClick="imgdelete_Click" />
                        <br />
                        <asp:HiddenField ID="hid" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
