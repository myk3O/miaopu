<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactEdit.aspx.cs" Inherits="Admin_Contact_ContactEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <!--编辑器 -->
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <link rel="stylesheet" href="../../Kindeditor/themes/default/default.css" />
    <link rel="stylesheet" href="../../Kindeditor/plugins/code/prettify.css" />
    <script charset="utf-8" src="../../Kindeditor/kindeditor.js"></script>
    <script charset="utf-8" src="../../Kindeditor/lang/zh_CN.js"></script>
    <script charset="utf-8" src="../../Kindeditor/plugins/code/prettify.js"></script>
    <script type="text/javascript" src="../../Scripts/JSshowHand.js"></script>
    <script>
        KindEditor.ready(function (K) {
            var editor1 = K.create('#content1', {
                cssPath: '../../Kindeditor/plugins/code/prettify.css',
                uploadJson: '../../Kindeditor/asp.net/upload_json.ashx',
                fileManagerJson: '../../Kindeditor/asp.net/file_manager_json.ashx',
                allowFileManager: true,
                afterCreate: function () {
                    var self = this;
                    K.ctrl(document, 13, function () {
                        self.sync();
                        K('form[name=example]')[0].submit();
                    });
                    K.ctrl(self.edit.doc, 13, function () {
                        self.sync();
                        K('form[name=example]')[0].submit();
                    });
                }
            });
            prettyPrint();
        });
    </script>
    <style type="text/css">
        .auto-style1
        {
            height: 18px;
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
                <li>留言回复</li>
            </ul>
        </div>
        <div class="formbody">
            <div id="usual1" class="usual">
                <div id="tab2" class="tabson">
                    <table id="tab" runat="server" class="tablelistx" width="100%" style="margin: 0 auto;">
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td width="25%" align="right">
                                用户留言：
                            </td>
                            <td width="75%" align="left">
                                <asp:TextBox ID="txtTlitle" runat="server" Enabled="false" Width="350px" class="scinputx"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                            <td align="right">
                                回复内容：
                            </td>
                            <td align="left">
                                <div style="margin-left: 11px">
                                    <textarea id="content1" ruant="sever" cols="100" rows="8" class="scinputx" style="width: 700px;
                                        height: 200px; visibility: hidden;" runat="server"></textarea>
                                    <%--<asp:TextBox runat="server" ID="txtfenleiName" CssClass="scinputx"></asp:TextBox>--%>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="25" align="center" bgcolor="#FFFFFF">
                                <asp:Button runat="server" ID="ImgbtnSub" CssClass="btn1 btn-xs btn-default" Text="确定"
                                    OnClick="imgSub_Click" />
                                &nbsp;&nbsp;<asp:Button runat="server" ID="imgs" CssClass="btn1 btn-xs btn-default"
                                    Text="取消" OnClick="imgback_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hid" runat="server" />
    </form>
</body>
</html>
