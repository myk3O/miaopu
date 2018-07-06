<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoneyOrderDetail.aspx.cs"
    Inherits="Admin_Order_MoneyOrderDetail" %>

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
    <%--<script type="text/javascript" src="../../js/jquery.idTabs.min.js"></script>
    <script type="text/javascript" src="../../js/select-ui.min.js"></script>
    <script type="text/javascript" src="../../editor/kindeditor.js"></script>--%>
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
        .bootstrap-admin-panel-content
        {
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
                <li><a href="#">提现申请详情</a></li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div id="tab2" class="tabson">
                    <div>
                        <ul class="seachformx">
                            <li>
                                <label style="text-align: left; width: 140px">
                                    提现金额：<%=allprice%>元</label>
                            </li>
                        </ul>
                        <h3>
                            收款账号</h3>
                        <%=bankStr %>
                        <ul class="seachformx">
                            <li style="margin-left: 30%">
                                <asp:Button runat="server" ID="Button1" Visible="false" CssClass="btn btn-xs btn-default"
                                    Text="确认打款" OnClick="btnSearh_Click" OnClientClick="return confirm('已打款对方，方可确认该申请')" />
                            </li>
                        </ul>
                    </div>
                    <table class="tablelist">
                        <tr>
                            <td>
                                <asp:GridView ID="gridField" runat="server" Width="100%" CssClass="gridview_m" EmptyDataText=""
                                    HeaderStyle-Font-Size="13px" AllowPaging="True" ShowHeader="true" DataKeyNames="nID"
                                    AutoGenerateColumns="False" CellPadding="0" CellSpacing="0" OnRowCommand="gridField_RowCommand"
                                    OnRowUpdating="gridField_RowUpdating" OnRowDataBound="gridField_RowDataBound"
                                    PageSize="15" OnDataBound="gridField_DataBound">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="订单编号" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="code" runat="server" Text='<%#Eval("OrderCode") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="订单归属" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbhomeMak" runat="server" Text='<%#Eval("dlName") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                        </asp:TemplateField>
                                      <asp:TemplateField HeaderText="微信账号" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="phone" runat="server" Text='<%#Eval("nickname") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="收货联系人" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lianxiren" runat="server" Text='<%#Eval("Lianxiren") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="联系电话" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbphone" runat="server" Text='<%#Eval("PhoneNumber") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="订单价格（元）" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbprice" runat="server" Text='<%#Eval("OrderPrice") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="订单状态" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbstate" runat="server" Text='<%#Eval("StateName") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="12%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="下单时间" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbloTop" runat="server" Text='<%#Eval("CreateTime") %>'></asp:Label>
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
                    <uc1:Pagination ID="Pagination2" runat="server" />
                </div>
                <div align="center" class="bootstrap-admin-panel-content">
                    <asp:Button runat="server" ID="btnReturn" class="btn btn-xs btn-default" Text="返回"
                        OnClick="btnReturn_Click" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hid" runat="server" />
    </form>
</body>
</html>
