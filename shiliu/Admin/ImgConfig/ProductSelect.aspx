<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductSelect.aspx.cs" Inherits="Admin_ImgConfig_ProductSelect" %>

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
    <form id="form1" runat="server">
    <div>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">首页</a></li>
                <li>产品列表</li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div id="tab2" class="tabson">
                    <div>
                    <ul class="seachform">
                                <li style="width: 20%">
                                    <label>
                                        视频系列</label><input name="" runat="server" id="keyName" type="text" class="scinput" /></li>
                        <%--        <li>
                                    <label>
                                        视频系列</label><asp:DropDownList runat="server" ID="DropGroup">
                                        </asp:DropDownList>
                                </li>--%>
                                
                                <li style="width: 20%">
                                    <label>
                                        是否上架</label><asp:DropDownList runat="server" ID="DropTh">
                                            <asp:ListItem Value="-1">请选择</asp:ListItem>
                                            <asp:ListItem Value="1">是</asp:ListItem>
                                            <asp:ListItem Value="0">否</asp:ListItem>
                                        </asp:DropDownList>
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
                                    OnDataBound="gridField_DataBound" OnRowCommand="gridField_RowCommand" PageSize="6">
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
                                       <asp:TemplateField HeaderText="视频系列" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbvName" runat="server" Text='<%#Eval("vName") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="8%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="价格(元)" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbprice" runat="server" Text='<%#Eval("Price") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="目录" HeaderStyle-Font-Bold="false" Visible="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbvUrl" runat="server" Text='<%#Eval("vUrl") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="5%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="是否上架" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lblth" runat="server" Text='<%#Eval("oHide") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="描述" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbvMemo" runat="server" Text='<%#Eval("vMemo") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="图片" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Image ID="img1" ImageUrl='<%#Eval("vPic") %>' runat="server" Width="50%" />
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="8%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="产品总售（元）" HeaderStyle-Font-Bold="false">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lballpri" runat="server" Text='<%#Eval("allpris") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="13px" Width="7%" />
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
    </form>
</body>
</html>
