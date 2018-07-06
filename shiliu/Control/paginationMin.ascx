<%@ Control Language="C#" AutoEventWireup="true" CodeFile="paginationMin.ascx.cs" Inherits="Control_paginationMin" %>
<script type="text/javascript" language="javascript">
    function check() {
        var ch = document.getElementById("Pagination2_txtGotoPage").value;
        if (!(/^([0-9]{1,6})$/.test(ch))) {
            alert("输入错误!");
            return;
        }
    }
</script>
<style type="text/css">
        
        .style155 {
            FONT-WEIGHT: 400;
            FONT-FAMILY: "宋体";
            FONT-SIZE: 12px;
            text-align: right;
            border-top: 0px solid;
            border-spacing: 0px solid;
            height: 28px;
        }
</style>

<asp:Panel ID="Panel1" runat="server" CssClass="style155" Height="23px" Width="100%"
    HorizontalAlign="Right">
    <div style="float:right; width:350px; margin-right:10%"><asp:ImageButton ID="lbtTop" runat="server" CausesValidation="False" ImageUrl="~/Images/fanye1.jpg" OnClick="lbtTop_Click"  />
    <asp:ImageButton ID="lbtBack" runat="server" CausesValidation="False" ImageUrl="~/Images/fanye2.jpg" OnClick="lbtBack_Click"  />
    <asp:ImageButton ID="lbtNext" runat="server" CausesValidation="False" ImageUrl="~/Images/fanye3.jpg" OnClick="lbtNext_Click" />
    <asp:ImageButton ID="lbtEnd" runat="server" CausesValidation="False" ImageUrl="~/Images/fanye4.jpg" OnClick="lbtEnd_Click" />
    <asp:TextBox ID="txtGotoPage" runat="server" Width="60px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGotoPage" Display="Dynamic" ErrorMessage="*不能为空">*不能为空</asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtGotoPage" Display="Dynamic" ErrorMessage="*输入错误" Operator="GreaterThan" Type="Integer" ValueToCompare="0">*输入错误</asp:CompareValidator>
    <asp:ImageButton ID="imgbutGo" runat="server" CausesValidation="False" ImageUrl="../Img/Go.jpg" OnClick="imgbtnGo_Click" />
    </div><div style="float:right; width:20%; line-height:24px;"><asp:Label ID="lblPageCount" runat="server"></asp:Label>
    <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
    <asp:Label ID="lblRecordPerPage" runat="server"></asp:Label></div>
    </asp:Panel>

