<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberKeCheng.aspx.cs" Inherits="Admin_Members_MemberKeCheng" %>

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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">首页</a></li>
                <li>学员参加课程</li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div id="tab2" class="tabson">
                    <div>
                        <ul class="seachform">
                            <li>
                                <label>
                                    开课时间</label>
                                <input runat="server" readonly="readonly" type="text" id="txtBeginT" class="scinputx"
                                    onclick="WdatePicker()" />
                            </li>
                            <li>
                                <label>
                                    至</label>
                                <input runat="server" readonly="readonly" type="text" id="txtEndT" class="scinputx"
                                    onclick="WdatePicker()" />
                            </li>
                            <li>
                                <label>
                                    上课老师</label><input name="" runat="server" id="txtTeacher" type="text" class="scinput" />
                            </li>
                            <li>
                                <label>
                                    教室</label><input name="" runat="server" id="txtJS" type="text" class="scinput" />
                            </li>
                            <li>
                                <label>
                                    &nbsp;</label><asp:Button runat="server" ID="btnSearh" CssClass="btn btn-xs btn-default"
                                        Text="查询" OnClick="btnSearh_Click" />
                                <%--<input name="" type="button" class="scbtn" value="查询" />--%></li>
                        </ul>
                    </div>
                    <table class="tablelist">
                        <tr>
                            <td>
                                <asp:GridView ID="gridField" runat="server" Width="100%" CssClass="gridview_m" EmptyDataText=""
                                    HeaderStyle-Font-Size="13px" AllowPaging="True" DataKeyNames="nID" HeaderStyle-CssClass="headbackground"
                                    AutoGenerateColumns="False" CellPadding="0" CellSpacing="0" OnRowDataBound="gridField_RowDataBound"
                                    OnDataBound="gridField_DataBound" OnRowCommand="gridField_RowCommand" PageSize="15">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckBox2" runat="server" onclick="SelectAllCheckboxes(this)" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckSel" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="4%" Font-Size="13px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="课程名称" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbname1" runat="server" Text='<%#Eval("tTitle") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="上课日期" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbname2" runat="server" Text='<%#Eval("tName") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="开课时间" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbname3" runat="server" Text='<%#Eval("tTime") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="课时数" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbltlite" runat="server" Text='<%#Eval("tWriter") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="上课老师" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblzhiding" runat="server" Text='<%#Eval("tFromWhere") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="教室" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblPubtime" runat="server" Text='<%#Eval("nHit") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                        </asp:TemplateField>
                                        <%--                 <asp:TemplateField HeaderText="备注" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbbz" runat="server" Text='<%#Eval("tMemo") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="15%" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="管    理" HeaderStyle-Font-Bold="false" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Button runat="server" ID="btnDelete" CssClass="btn2 btn-xs btn-default" Text="删除"
                                                        CausesValidation="False" CommandName="del" CommandArgument='<%# Eval("nID")%>' />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" />
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
                            <asp:TemplateField HeaderText="课程名称" HeaderStyle-Font-Bold="false">
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="lbname1" runat="server" Text='<%#Eval("tTitle") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="上课日期" HeaderStyle-Font-Bold="false">
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="lbname2" runat="server" Text='<%#Eval("tName") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="开课时间" HeaderStyle-Font-Bold="false">
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="lbname3" runat="server" Text='<%#Eval("tTime") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="20%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="课时数" HeaderStyle-Font-Bold="false">
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="lbltlite" runat="server" Text='<%#Eval("tWriter") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="上课老师" HeaderStyle-Font-Bold="false">
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="lblzhiding" runat="server" Text='<%#Eval("tFromWhere") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="教室" HeaderStyle-Font-Bold="false">
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="lblPubtime" runat="server" Text='<%#Eval("nHit") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="headbackground" Font-Bold="True" Height="25px"></HeaderStyle>
                    </asp:GridView>
                </div>
                <div align="center" class="bootstrap-admin-panel-content">
                    <asp:Button runat="server" ID="btnAdd" class="btn btn-xs btn-default" Text="返回" OnClick="btnAdd_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="imgdelete" class="btn btn-xs btn-default" Text="批量删除"
                        OnClick="imgdelete_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_Expot" CssClass="btn btn-xs btn-default" runat="server" Text="导出"
                        OnClick="btn_Expot_Click" />
                    <br />
                    <asp:HiddenField ID="hid" runat="server" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
