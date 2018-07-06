<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WeekXueBa.aspx.cs" Inherits="Admin_Order_WeekXueBa" %>


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
    <link rel="stylesheet" href="../../Wap/CSS/sub.css" />
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
                    <li><a href="#">学霸排行</a></li>
                </ul>
            </div>
            <div class="formbody">
                <div id="usual1" class="usual">
                    <div id="tab2" class="tabson">
                        <div>

                            <ul class="seachformx">
                                <li>
                                    <label style="text-align: left; width: 100px;">
                                        当前日期所在周</label><input name="" runat="server" id="tBegin" type="text" class="scinput"
                                            onclick="WdatePicker()" />
                                </li>

                                <li style="margin-left: 30%">
                                    <asp:Button runat="server" ID="Button1" CssClass="btn btn-xs btn-default" Text="查询"
                                        OnClick="btnSearh_Click" />
                                </li>
                            </ul>
                            <ul class="seachformx">
                            </ul>
                        </div>


                        <div class="inContent">
                            <div class="tab">
                                <ul class="menu">
                                    <li class="active"><span><%=year %>年第<%=weekYear %>周</span></li>
                                </ul>
                                <dl class="MyBranch">
                                    <%=class1 %>
                                </dl>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hid" runat="server" />
    </form>
</body>
</html>
