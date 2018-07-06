<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HighChart.aspx.cs" Inherits="Admin_HighChart" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="http://www.js-css.cn/jscode/jquery.min.js"></script>
    <script type="text/javascript" src="../js/jquery-1.6.2.min.js"></script>
    <script src="../js/jquery.date_input.pack.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/highcharts.js"></script>
    <script language="javascript" type="text/javascript" src="My97DatePicker/WdatePicker.js"></script>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="Main/Right.aspx">首页</a></li>
                <li>统计图表</li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div id="tab2" class="tabson">
                    <div>
                        <ul class="seachform">
                            <li>
                                <label>
                                    选择时段:</label>
                                <asp:TextBox runat="server" class="scinput" ID="txtStatisticsStartTime" onClick="WdatePicker()"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredStartTime" ControlToValidate="txtStatisticsStartTime"
                                    ErrorMessage="*" ForeColor="Red" Display="Dynamic" ValidationGroup="UCDateTime"></asp:RequiredFieldValidator>
                                <asp:TextBox runat="server" class="scinput" ID="txtStatisticsEndTime" onClick="WdatePicker()"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtStatisticsEndTime"
                                    ErrorMessage="*" ForeColor="Red" Display="Dynamic" ValidationGroup="UCDateTime"></asp:RequiredFieldValidator>
                            </li>
                            <li>
                                <asp:ImageButton runat="server" ID="imgbtnSearch" ImageUrl="../images/ico06.png"
                                    ToolTip="Search" ValidationGroup="UCDateTime" Height="25px" OnClick="imgbtnSearch_Click"
                                    Width="30px" /></li>
                        </ul>
                    </div>
                    <div runat="server" id="BrokenlineChartShow" style="margin-top: 20px; font-size: 12px;">
                        <div runat="server" id="noData" visible="false" style="text-align: center; color: Red;">
                            <asp:Literal ID="Literal14" runat="server" Text="当前">
                            </asp:Literal>！
                        </div>
                        <div runat="server" id="DivChart" style="width: 100%; margin-top: 10px;">
                            <div id="Linecontainer" style="min-width: 740px; width: 68%; height: 550px;">
                            </div>
                        </div>
                        <script type="text/javascript">
                                    var chart;
                                    $(document).ready(function () {
                                        <%=result%>
                                    });
                        </script>
                    </div>
                    <%--<asp:Label runat="server" ForeColor="Green" ID="lblTootip"></asp:Label>
                            <asp:Literal ID="Literal15" runat="server" Text="方式显示"></asp:Literal>--%>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
