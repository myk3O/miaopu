<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <link rel="stylesheet" href="Content/StyleText.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center">
    <asp:Label runat="server" ID="lblError" Text="您的登录已过期，请重新登录！" ForeColor="Red" CssClass="style1"></asp:Label>
    </div>
    </form>
</body>
</html>
