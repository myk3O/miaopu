<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JingTieList.aspx.cs" Inherits="Admin_Order_JingTieList" %>

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
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
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
    <style type="text/css">
        .bootstrap-admin-panel-content {
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
                    <li><a href="#">津贴流水</a></li>
                </ul>
            </div>
            <div class="formbody">
                <div id="usual1" class="usual">
                    <div id="tab2" class="tabson">
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

                                <li style="width: 20%">
                                    <label>
                                        用户等级：</label><asp:DropDownList runat="server" ID="drpUserLevel">
                                        </asp:DropDownList>
                                </li>

                            </ul>
                            <ul class="seachformx">
                                <li>
                                    <label style="text-align: left;">
                                        下单时间</label><input name="" runat="server" id="tBegin" type="text" class="scinput"
                                            onclick="WdatePicker()" />
                                </li>
                                <li>
                                    <label>
                                        至</label><input name="" runat="server" id="tEnd" type="text" class="scinput" onclick="WdatePicker()" />
                                </li>
                                <li style="margin-left: 30%">
                                    <asp:Button runat="server" ID="Button1" CssClass="btn btn-xs btn-default" Text="查询"
                                        OnClick="btnSearh_Click" />
                                </li>
                            </ul>
                            <ul class="seachformx">

                                <li>
                                    <label style="text-align: left; width: 200px; color: red">
                                        <%=allJinTie %></label>
                                </li>

                            </ul>
                            <ul class="seachformx">
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
                                            <asp:TemplateField HeaderText="享受津贴" HeaderStyle-Font-Bold="false">
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
                                            <asp:TemplateField HeaderText="等级名称" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbisJXS" runat="server" Text='<%#Eval("levelName") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="6%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="订单总价" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbSex" runat="server" Text='<%#Eval("allpri") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="4%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="津贴金额" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbPhone" runat="server" Text='<%#Eval("price") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="6%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="津贴提成" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbMemberPass" runat="server" Text='<%#Eval("part") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="5%" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="下单时间" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbschool" runat="server" Text='<%#Eval("CreateTime") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="9%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="购买用户昵称" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbAllMoney" runat="server" Text='<%#Eval("ncn") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="8%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="购买用户" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Image ID="img2" ImageUrl='<%#Eval("hiu") %>' runat="server" Width="90%" />

                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="5%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="管    理" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>

                                                        <asp:Button runat="server" CssClass="btn2 btn-xs btn-default" ID="btnapply" CausesValidation="False"
                                                            CommandName="apply" CommandArgument='<%# Eval("nID")%>' Text="累计津贴" Width="80px" />

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
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hid" runat="server" />
    </form>
</body>
</html>

