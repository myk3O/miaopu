<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Tablelabe.ascx.cs" Inherits="ERP2008_UserControl_Tabletext" %>
 <table border="0" cellpadding="0" cellspacing="0" width="330px">
<tr >
         <td style="width: 70px" class="td-text">
             留言时间:</td>
         <td style="width: 95px">
             <asp:Label ID="Label2" runat="server"></asp:Label>&nbsp;&nbsp;
             <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ForeColor="Blue" Visible="False">修改</asp:LinkButton>
         </td>
    </tr>
    <tr>
        <td  class="td-text" >
            内容:
        </td>
        <td style="width: 260px">
         <asp:Label ID="Label3" runat="server" ></asp:Label></td>
    </tr>
</table>
<br />
<hr style="background-color:Black;width:330px"/>
