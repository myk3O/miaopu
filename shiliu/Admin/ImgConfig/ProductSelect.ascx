<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductSelect.ascx.cs"
    Inherits="Admin_ImgConfig_ProductSelect" %>
<%@ Register Src="../../Control/Pagination.ascx" TagName="Pagination" TagPrefix="uc1" %>
<head id="Head1">
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

        function SelectCheckboxes(cb) {
            var obj = document.getElementsByName("cbox");
            for (i = 0; i < obj.length; i++) {
                //判斷obj集合中的i元素是否為cb，若否則表示未被點選   
                if (obj[i] != cb) obj[i].checked = false;
                //若是 但原先未被勾選 則變成勾選；反之 則變為未勾選   
                //else  obj[i].checked = cb.checked;   
                //若要至少勾選一個的話，則把上面那行else拿掉，換用下面那行   
                else obj[i].checked = true;
            }

        }

        function JumpBack() {
            $("input[name='cbox']:checkbox").each(function () {
                if ($(this).attr("checked")) {
                    $("#HiddenField1").val($(this).val());
                }
            })
        }
    </script>
</head>
<body>
    <div>
       
        <div class="formbody">
            <div id="usual1" class="usual">
                <div id="tab2" class="tabson">
                    <div>
                        <ul class="seachform">
                            <li>
                                <label>
                                    产品名称</label><input name="" runat="server" id="keyName" type="text" class="scinput" /></li>
                            <li>
                                <label>
                                    产品系列</label><asp:DropDownList runat="server" ID="DropGroup">
                                    </asp:DropDownList>
                            </li>
                            <li>
                                <label>
                                    &nbsp;</label><asp:Button runat="server" ID="btnSearh" CssClass="btn btn-xs btn-default"
                                        Text="查询" OnClick="btnSearh_Click" />
                                <%--<input name="" type="button" class="scbtn" value="查询" />--%></li>
                        </ul>
                        <ul class="seachform" style="display: none">
                            <li>
                                <label>
                                    酒的颜色</label><asp:DropDownList runat="server" ID="DropDownList2">
                                    </asp:DropDownList>
                            </li>
                            <li>
                                <label>
                                    酒内糖份</label><asp:DropDownList runat="server" ID="DropDownList1">
                                    </asp:DropDownList>
                            </li>
                            <li>
                                <label>
                                    二氧化碳</label><asp:DropDownList runat="server" ID="DropDownList3">
                                    </asp:DropDownList>
                            </li>
                            <li>
                                <label>
                                    推荐产品</label><asp:DropDownList runat="server" ID="DropName">
                                        <asp:ListItem Value="-1">请选择</asp:ListItem>
                                        <asp:ListItem Value="1">是</asp:ListItem>
                                        <asp:ListItem Value="0">否</asp:ListItem>
                                    </asp:DropDownList>
                            </li>
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
                                        <asp:TemplateField>
                                            <%--      <HeaderTemplate>
                                                <asp:CheckBox ID="CheckBox2" runat="server" onclick="SelectAllCheckboxes(this)" />
                                            </HeaderTemplate>--%>
                                            <ItemTemplate>
                                                <input type="checkbox" name="cbox" onclick="SelectCheckboxes(this);" value='<%# Eval("nID")%>' />
                                                <%--  <asp:CheckBox ID="CheckSel" runat="server" onclick="SelectCheckboxes(this)" />--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" Font-Size="13px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="产品系列" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbname1" runat="server" Text='<%#Eval("name1") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="产品名称" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lbltlite" runat="server" Text='<%#Eval("tTitle") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="库存" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblkucun" runat="server" Text='<%#Eval("kucun") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="单价" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblprice" runat="server" Text='<%#Eval("price") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
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
                    <asp:Button runat="server" ID="btnAdd" class="btn btn-xs btn-default" Text="选择" OnClick="btnAdd_Click"
                        OnClientClick="JumpBack()" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="imgdelete" Visible="false" class="btn btn-xs btn-default"
                        Text="批量删除" OnClick="imgdelete_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_Expot" Visible="false" CssClass="btn btn-xs btn-default" runat="server"
                        Text="导出" OnClick="btn_Expot_Click" />
                    <br />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="hid" runat="server" />
                </div>
            </div>
        </div>
    </div>
</body>
