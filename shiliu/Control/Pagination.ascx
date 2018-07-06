<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pagination.ascx.cs" Inherits="Pagination" %>

<script type="text/javascript" language="javascript">
    function check()
    {
        var ch = document.getElementById("Pagination2_txtGotoPage").value;
        if(!(/^([0-9]{1,6})$/.test(ch)))
        {
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
    .pageK {float:right; width:30px; height:26px; margin:0 6px; border:1px solid #cbcbcb;text-align:center;}
    .pageG{float:right;}
</style>

<asp:Panel ID="Panel1" runat="server" CssClass="style155" Height="23px" Width="100%"
    HorizontalAlign="Right">
    <div style="float:right; width:350px; margin-right:10%;margin-top:10px;"><asp:ImageButton ID="lbtTop" runat="server" CausesValidation="False" ImageUrl="~/Images/fanye1.jpg" OnClick="lbtTop_Click"  />
    <asp:ImageButton ID="lbtBack" runat="server" CausesValidation="False" ImageUrl="~/Images/fanye2.jpg" OnClick="lbtBack_Click"  />
    <asp:ImageButton ID="lbtNext" runat="server" CausesValidation="False" ImageUrl="~/Images/fanye3.jpg" OnClick="lbtNext_Click" />
    <asp:ImageButton ID="lbtEnd" runat="server" CausesValidation="False" ImageUrl="~/Images/fanye4.jpg" OnClick="lbtEnd_Click" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGotoPage" Display="Dynamic" ErrorMessage="*不能为空">*不能为空</asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtGotoPage" Display="Dynamic" ErrorMessage="*输入错误" Operator="GreaterThan" Type="Integer" ValueToCompare="0">*输入错误</asp:CompareValidator>
    <asp:ImageButton ID="imgbutGo" runat="server" CssClass="pageG" CausesValidation="False" ImageUrl="../Images/fanye5.jpg" OnClick="imgbtnGo_Click" />
    <asp:TextBox ID="txtGotoPage" runat="server" CssClass="pageK"></asp:TextBox>
    </div><div style="float:right; width:20%; line-height:26px;margin-top:10px;"><asp:Label ID="lblPageCount" runat="server"></asp:Label>
    <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
    <asp:Label ID="lblRecordPerPage" runat="server"></asp:Label></div>
    </asp:Panel>
