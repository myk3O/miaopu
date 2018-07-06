﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomeMakMainDL.aspx.cs" Inherits="Admin_DaiLi_HomeMakMainDL" %>

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
                <li><a href="#">代理商管理</a></li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div id="tab2" class="tabson">
                    <div>
                        <ul class="seachformx">
                            <li>
                                <label>
                                    综合查询</label><input name="" runat="server" id="keyName" type="text" class="scinput" />
                            </li>
                            <li>
                                <label style="text-align: right; width: 140px">
                                    代理级别：</label><asp:DropDownList runat="server" ID="dropDailiSystem" AutoPostBack="True"
                                        OnSelectedIndexChanged="dropDailiSystem_SelectedIndexChanged">
                                    </asp:DropDownList>
                            </li>
                            <li>
                                <label style="text-align: right; width: 140px">
                                    上级代理：</label><asp:DropDownList runat="server" ID="dropFatherDaili">
                                    </asp:DropDownList>
                            </li>
                            <li>
                                <label style="text-align: right; width: 60px">
                                    状态：</label>
                                <asp:DropDownList runat="server" ID="DropState">
                                    <asp:ListItem Value="-1">全部</asp:ListItem>
                                    <asp:ListItem Value="1">已启用</asp:ListItem>
                                    <asp:ListItem Value="0">已禁用</asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li>
                                <asp:Button runat="server" ID="btnSearh" CssClass="btn btn-xs btn-default" Text="查询"
                                    OnClick="btnSearh_Click" />
                                <%--<input name="" type="button" class="scbtn" value="查询" />--%></li>
                        </ul>
                    </div>
                    <table class="tablelist">
                        <tr>
                            <td>
                                <asp:GridView ID="gridField" runat="server" Width="100%" CssClass="gridview_m" EmptyDataText=""
                                    HeaderStyle-Font-Size="13px" AllowPaging="True" ShowHeader="true" DataKeyNames="nID"
                                    AutoGenerateColumns="False" CellPadding="0" CellSpacing="0" OnDataBound="gridField_DataBound"
                                    OnRowCommand="gridField_RowCommand" OnRowUpdating="gridField_RowUpdating" OnRowDataBound="gridField_RowDataBound"
                                    PageSize="15">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="上级代理" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblGroup" runat="server" Text='<%#Eval("nLogNum") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="代理级别" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblcity" runat="server" Text='<%#Eval("tClassName") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="个人/公司名称" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("tRealName") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="联系人" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblPhone" runat="server" Text='<%#Eval("HomeIntegral") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="固定电话" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("HomePhone") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="手机" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblRealname" runat="server" Text='<%#Eval("HomeMobile") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="商城地址" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblurl" runat="server" Text='<%#Eval("CreatorName") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="18%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="状态" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblState" runat="server" Text='<%#Eval("oCheck") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="管    理" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Button runat="server" ID="btnUpdate" CssClass="btn2 btn-xs btn-default" Visible="false"
                                                        Text="编辑" CausesValidation="False" CommandName="update" CommandArgument='<%# Eval("nID")%>' />
                                                    <asp:Button runat="server" ID="btnJinyong" CssClass="btn2 btn-xs btn-default" Text="禁用"
                                                        Visible="false" CausesValidation="False" CommandName="jinyong" CommandArgument='<%# Eval("nID")%>' />
                                                    <asp:Button runat="server" ID="btnJiechu" CssClass="btn2 btn-xs btn-default" Text="启用"
                                                        Visible="false" CausesValidation="False" CommandName="qiyong" CommandArgument='<%# Eval("nID")%>' />
                                                    <asp:Button runat="server" ID="btnAddr" Width="100px" CssClass="btn2 btn-xs btn-default"
                                                        Visible="false" Text="生成商城地址" CausesValidation="False" CommandName="AddUrl" CommandArgument='<%# Eval("nID")%>' />
                                                    <asp:Button runat="server" ID="btndingdan" CssClass="btn2 btn-xs btn-default" Text="订单"
                                                        CausesValidation="False" CommandName="dingdan" CommandArgument='<%# Eval("nID")%>' />
                                                    <asp:Button runat="server" ID="btn_detail" CssClass="btn2 btn-xs btn-default" Text="详细"
                                                        CausesValidation="False" CommandName="detail" CommandArgument='<%# Eval("nID")%>' />
                                                    <asp:HiddenField runat="server" ID="hid" Value='<%# Eval("nID") %>' />
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
                    <asp:Button runat="server" ID="btnAdd" class="btn btn-xs btn-default" Visible="false"
                        Text="添加" OnClick="btnAdd_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="imgdelete" class="btn btn-xs btn-default" Visible="false"
                        Text="禁用" OnClick="imgdelete_Click" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hid" runat="server" />
    </form>
</body>
</html>