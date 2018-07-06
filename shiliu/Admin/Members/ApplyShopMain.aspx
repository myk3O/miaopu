<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyShopMain.aspx.cs" Inherits="Admin_Members_ApplyShopMain" %>

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
                <li>开店申请列表</li>
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
                                    手机账号</label>
                                <asp:TextBox ID="txtName" runat="server" CssClass="scinputx"></asp:TextBox></li>
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
                                    OnDataBound="gridField_DataBound" OnRowCommand="gridField_RowCommand" PageSize="5">
                                    <Columns>
                                        <asp:TemplateField HeaderText="申请人" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <asp:Image ID="img1" ImageUrl='<%#Eval("headimgurl") %>' runat="server" Width="90%" />
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" Font-Size="13px" Width="6%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="姓名" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbname" runat="server" Text='<%#Eval("name") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="联系电话" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbphone" runat="server" Text='<%#Eval("phone") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="客服QQ" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbQQ" runat="server" Text='<%#Eval("QQnumb") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="职务" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lboccup" runat="server" Text='<%#Eval("Occupation") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="7%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="性别" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbSex" runat="server" Text='<%#Eval("Sex") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="申请时间" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbtime" runat="server" Text='<%#Eval("CreateTime") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="代理产品" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbcalssname" runat="server" Text='<%#Eval("proClassID") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="管    理" HeaderStyle-Font-Bold="false" Visible="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Button runat="server" ID="Button1" CssClass="btn2 btn-xs btn-default" Text="开店"
                                                        CausesValidation="False" CommandName="upfxsT" OnClientClick="return confirm('确认用户成为分销商，并生成店铺')"
                                                        CommandArgument='<%# Eval("nID")%>' />
                                                    <asp:HiddenField runat="server" ID="hid" Value='<%# Eval("nID") %>' />
                                                    <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("nID") %>' />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="5%" />
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
                    <asp:Button runat="server" ID="Button3" CssClass="btn1 btn-xs btn-default" OnClientClick="javascript:history.go(-1);return false;"
                        Text="返回" />
                </div>
                <div align="center" class="bootstrap-admin-panel-content">
                    <br />
                    <asp:HiddenField ID="hid" runat="server" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
