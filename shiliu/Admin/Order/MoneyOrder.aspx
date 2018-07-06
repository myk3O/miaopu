<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoneyOrder.aspx.cs" Inherits="Admin_Order_MoneyOrder" %>

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
                    <li><a href="#">提现申请列表</a></li>
                </ul>
            </div>
            <div class="formbody">
                <div id="usual1" class="usual">
                    <div id="tab2" class="tabson">
                        <div>
                            <ul class="seachformx">

                                <li>
                                    <label style="text-align: left;">
                                        微信昵称</label><input name="" runat="server" id="nickname" type="text" class="scinput" />
                                </li>
                                <li>
                                    <label style="text-align: left;">
                                        用户姓名</label><input name="" runat="server" id="realname" type="text" class="scinput" />
                                </li>
                                <li>
                                    <label>
                                        订单状态：</label><asp:DropDownList runat="server" ID="DropState">
                                            <asp:ListItem Value="0" Selected="True">未处理</asp:ListItem>
                                            <asp:ListItem Value="1">已处理</asp:ListItem>
                                        </asp:DropDownList>
                                </li>
                            </ul>
                            <ul class="seachformx">
                                <li>
                                    <label style="text-align: left;">
                                        创建时间</label><input name="" runat="server" id="tBegin" type="text" class="scinput"
                                            onclick="WdatePicker()" />
                                </li>
                                <li>
                                    <label>
                                        至</label><input name="" runat="server" id="tEnd" type="text" class="scinput" onclick="WdatePicker()" />
                                </li>
                                <li style="margin-left: 30%">
                                    <asp:Button runat="server" ID="Button1" CssClass="btn btn-xs btn-default" Text="查询"
                                        OnClick="btnSearh_Click" />（当前累计打款<%=tixian %>）
                                </li>
                            </ul>
                            <ul class="seachformx">
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
                                            <asp:TemplateField HeaderText="头像" HeaderStyle-Font-Bold="false">
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
                                                        <asp:Label ID="lbrealname" runat="server" Text='<%#Eval("tRealName") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="6%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="联系电话" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbphone" runat="server" Text='<%#Eval("MemberPhone") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="8%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="提现（元）" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbprice" runat="server" Text='<%#Eval("AllPrice") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="8%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="提现状态" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbstate" runat="server" Text='<%#Eval("OrderState") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="6%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="银行" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbtn" runat="server" Text='<%#Eval("TypeName") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="6%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="开户行" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbKaihu" runat="server" Text='<%#Eval("address") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="6%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="持卡人" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbckr" runat="server" Text='<%#Eval("ckr") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="6%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="卡号" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbzh" runat="server" Text='<%#Eval("Zhanghao") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="6%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="创建时间" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbloTop" runat="server" Text='<%#Eval("CreateTime") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="8%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="打款时间" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbuptime" runat="server" Text='<%#Eval("UpdateTime") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="8%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="管    理" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Button runat="server" ID="btnUpdate" CssClass="btn2 btn-xs btn-default" Text="确认打款" Width="80px"
                                                            CausesValidation="False" CommandName="detail" CommandArgument='<%# Eval("nID")%>' OnClientClick="return confirm('请再次确认已经打款给客户后再修改状态')" />
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
                        <%--导出数据--%>
                        <asp:GridView ID="GridViewExpot" runat="server" Width="100%" CssClass="gridview_m"
                            EmptyDataText="" HeaderStyle-Font-Size="13px" DataKeyNames="nID" HeaderStyle-CssClass="headbackground"
                            AutoGenerateColumns="False" CellPadding="0" CellSpacing="0">
                            <Columns>
                                <asp:TemplateField HeaderText="微信昵称" HeaderStyle-Font-Bold="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="lbnickname" runat="server" Text='<%#Eval("nickname") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="客户编号" HeaderStyle-Font-Bold="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="lbMemberCode" runat="server" Text='<%#Eval("MemberCode") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="账号" HeaderStyle-Font-Bold="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="lbzhanghao" runat="server" Text='<%#Eval("Zhanghao") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="账户名称" HeaderStyle-Font-Bold="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="zhanghmc" runat="server" Text='<%#Eval("Huming") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="账户类型" HeaderStyle-Font-Bold="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="zhlx" runat="server" Text='<%#Eval("nullvalue") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="20%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="开户行行别" HeaderStyle-Font-Bold="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="lbkaihhb" runat="server" Text='<%#Eval("TypeName") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="账户所在省" HeaderStyle-Font-Bold="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="lbkaihszs" runat="server" Text='<%#Eval("address") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="账户所在市" HeaderStyle-Font-Bold="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="szs" runat="server" Text='<%#Eval("nullvalue") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="账户所属网点" HeaderStyle-Font-Bold="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="shwd" runat="server" Text='<%#Eval("nullvalue") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="手机" HeaderStyle-Font-Bold="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="lbshouj" runat="server" Text='<%#Eval("MemberPhone") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="金额" HeaderStyle-Font-Bold="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="lbjine" runat="server" Text='<%#Eval("ordermoney") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="重复" HeaderStyle-Font-Bold="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="lbchongfu" runat="server" Text='<%#Eval("chongfu") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Font-Size="13px" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="headbackground" Font-Bold="True" Height="25px"></HeaderStyle>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div align="center" class="bootstrap-admin-panel-content">

            <asp:Button ID="btn_Expot" CssClass="btn btn-xs btn-default" runat="server"
                Text="导出" OnClick="btn_Expot_Click" />
            <br />
        </div>
        <asp:HiddenField ID="hid" runat="server" />
    </form>
</body>
</html>
