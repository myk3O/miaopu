<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberMain.aspx.cs" Inherits="Admin_Member_MemberMain"
    EnableEventValidation="false" %>

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
                                        微信昵称</label>
                                    <asp:TextBox ID="nickName" runat="server" CssClass="scinputx"></asp:TextBox></li>
                                <li>

                                    <label>
                                        用户姓名</label>
                                    <asp:TextBox ID="txtname" runat="server" CssClass="scinputx"></asp:TextBox></li>
                                <%--               <li style="width: 20%">
                                    <label>
                                        是否分销</label><asp:DropDownList runat="server" ID="DropisJXS">
                                            <asp:ListItem Value="-1">请选择</asp:ListItem>
                                            <asp:ListItem Value="1">是</asp:ListItem>
                                            <asp:ListItem Value="0">否</asp:ListItem>
                                        </asp:DropDownList>
                                </li>--%>
                                <li style="width: 20%">
                                    <label>
                                        所属班级：</label><asp:DropDownList runat="server" ID="drpUserLevel">
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
                                            <%--       <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckBox2" runat="server" onclick="SelectAllCheckboxes(this)" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckSel" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="6%" Font-Size="13px" />
                                        </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="微信头像" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <asp:Image ID="img1" ImageUrl='<%#Eval("headimgurl") %>' runat="server" Width="90%" />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" Font-Size="13px" Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="微信昵称" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbnickname" runat="server" Text='<%#Eval("nickname") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="8%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="用户姓名" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbname" runat="server" Text='<%#Eval("tRealName") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="性别" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbSex" runat="server" Text='<%#Eval("MemberSex") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="4%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="手机号码" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbPhone" runat="server" Text='<%#Eval("MemberPhone") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="6%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="身份证" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbMemberPass" runat="server" Text='<%#Eval("MemberPass") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="5%" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="注册时间" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbschool" runat="server" Text='<%#Eval("dtAddTime") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="9%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="等级名称" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbisJXS" runat="server" Text='<%#Eval("levelName") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="6%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="已提现（元）" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbAllMoney" runat="server" Text='<%#Eval("AllMoney") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="8%" />
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
                        <br />
                        <asp:HiddenField ID="hid" runat="server" />
                    </div>
                    <div align="center" class="bootstrap-admin-panel-content">
                        <asp:Button runat="server" ID="btnBack" class="btn btn-xs btn-default" Text="返回" OnClick="btnBack_Click" />
                        &nbsp;&nbsp;&nbsp;
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
