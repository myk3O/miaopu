<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderMainGuanBi.aspx.cs" Inherits="Admin_OrderMainGuanBi" %>

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
                <li><a href="#">订单列表</a></li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div id="tab2" class="tabson">
                    <div>
                        <ul class="seachformx">
                            <li style="display: none">
                                <label style="text-align: right; width: 140px">
                                    订单归属代理：</label><asp:DropDownList runat="server" ID="dropFatherDaili">
                                    </asp:DropDownList>
                            </li>
                            <li>
                                <label>
                                    订单归属</label><input name="" runat="server" id="txtDaili" type="text" class="scinput" />
                            </li>
                            <li>
                                <label>
                                    订单编号</label><input name="" runat="server" id="code" type="text" class="scinput" />
                            </li>
                            <li>
                                <label>
                                    收货联系人</label><input name="" runat="server" id="username" type="text" class="scinput" />
                            </li>
                            <li>
                                <label>
                                    订单状态</label>交易关闭
                            </li>
                        </ul>
                        <ul class="seachformx">
                            <li>
                                <label style="text-align: right; width: 140px">
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
                    </div>
                    <table class="tablelist">
                        <tr>
                            <td>
                                <asp:GridView ID="gridField" runat="server" Width="100%" CssClass="gridview_m" EmptyDataText=""
                                    HeaderStyle-Font-Size="13px" AllowPaging="True" ShowHeader="true" DataKeyNames="nID"
                                    AutoGenerateColumns="False" CellPadding="0" CellSpacing="0" OnRowCommand="gridField_RowCommand"
                                    OnRowUpdating="gridField_RowUpdating" OnRowDataBound="gridField_RowDataBound"
                                    PageSize="10" OnDataBound="gridField_DataBound">
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
                                        <asp:TemplateField HeaderText="管    理" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Button runat="server" ID="btnUpdate" CssClass="btn2 btn-xs btn-default" Text="详细"
                                                        CausesValidation="False" CommandName="detail" CommandArgument='<%# Eval("nID")%>' />
                                                    <asp:Button runat="server" ID="btndel" CssClass="btn2 btn-xs btn-default" Text="删除"
                                                        CausesValidation="False" CommandName="del" OnClientClick="return confirm('确认删除订单')"
                                                        CommandArgument='<%# Eval("nID")%>' Visible="false" />
                                                    <%--     <asp:Button runat="server" ID="btnTh" CssClass="btn2 btn-xs btn-default" Text="发货"
                                                        CausesValidation="False" Visible="false" CommandName="th" CommandArgument='<%# Eval("nID")%>' />--%>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="15%" />
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
                        Visible="false" OnClick="btnReturn_Click" />
                    <asp:Button runat="server" ID="btnReturnFromDL" class="btn btn-xs btn-default" Text="返回"
                        Visible="false" OnClick="btnReturnFromDL_Click" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hid" runat="server" />
    </form>
</body>
</html>
