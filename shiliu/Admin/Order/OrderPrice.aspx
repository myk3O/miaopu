<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderPrice.aspx.cs" Inherits="Admin_Order_OrderPrice" %>

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
                    <li><a href="#">分红流水</a></li>
                </ul>
            </div>
            <div class="formbody">
                <div id="usual1" class="usual">
                    <div id="tab2" class="tabson">
                        <div>
                            <ul class="seachformx">
                                <li style="margin-left: 70%">
                                    <label style="text-align: left; width: 200px">
                                        平台累计分红：<%=Fenhong %>元</label>
                                </li>
                            </ul>
                            <ul class="seachformx">
                                <li style="margin-left: 0%">
                                    <label style="text-align: left; width: 200px; color: red">
                                        查询日期：<%=timeDay %></label>
                                </li>
                            </ul>

                            <ul class="seachformx">

                                <li>
                                    <label style="text-align: left; width: 200px">
                                        班长人数：<%=banzhangrenshu %>人</label>
                                </li>
                                <li>
                                    <label style="text-align: left; width: 200px">
                                        班长分红：<%=banzhangfenhong %>元</label>
                                </li>
                                <li>
                                    <label style="text-align: left; width: 200px">
                                        班长总分红：<%=banzhangAllmoney %>元</label>
                                </li>
                            </ul>

                            <ul class="seachformx">

                                <li>
                                    <label style="text-align: left; width: 200px">
                                        班主任人数：<%=zhurenrenshu %>人</label>
                                </li>
                                <li>
                                    <label style="text-align: left; width: 200px">
                                        班主任分红：<%=zhurenfenhong %>元</label>
                                </li>
                                <li>
                                    <label style="text-align: left; width: 200px">
                                        班主任总分红：<%=zhurenAllmoney %>元</label>
                                </li>
                            </ul>

                            <ul class="seachformx">

                                <li>
                                    <label style="text-align: left; width: 200px">
                                        校长人数：<%=xiaozhangrenshu %>人</label>
                                </li>
                                <li>
                                    <label style="text-align: left; width: 200px">
                                        校长分红：<%=xiaozhangfenhong %>元</label>
                                </li>
                                <li>
                                    <label style="text-align: left; width: 200px">
                                        校长总分红：<%=xiaozhangAllmoney %>元</label>
                                </li>
                            </ul>
                        </div>
                        <table class="tablelist">

                            <tr>
                                <td>
                                    <asp:GridView ID="gridField" runat="server" Width="100%" CssClass="gridview_m" EmptyDataText=""
                                        HeaderStyle-Font-Size="13px" AllowPaging="True" ShowHeader="true" DataKeyNames="dtAddTime"
                                        AutoGenerateColumns="False" CellPadding="0" CellSpacing="0" OnRowCommand="gridField_RowCommand"
                                        OnRowUpdating="gridField_RowUpdating" OnRowDataBound="gridField_RowDataBound"
                                        PageSize="8" OnDataBound="gridField_DataBound">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="分红日期" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbdtAddTime" runat="server" Text='<%#Eval("dtAddTime") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="8%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="当日累计分红（元）" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbMemberNum" runat="server" Text='<%#Eval("MemberNum") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="8%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="管    理" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Button runat="server" ID="btnUpdate" CssClass="btn2 btn-xs btn-default" Text="详情" Width="80px"
                                                            CausesValidation="False" CommandName="detail" CommandArgument='<%# Eval("dtAddTime")%>' />
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
                        <uc1:Pagination ID="Pagination2" runat="server" />

                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hid" runat="server" />
    </form>
</body>
</html>
