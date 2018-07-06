<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PoliciesEdit.aspx.cs" Inherits="Admin_Policies_PoliciesEdit" %>

<%@ Register Assembly="Brettle.Web.NeatUpload.HashedInputFile" Namespace="Brettle.Web.NeatUpload" TagPrefix="HashedUpload" %>
<%@ Register Assembly="Brettle.Web.NeatUpload" Namespace="Brettle.Web.NeatUpload" TagPrefix="Upload" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        .auto-style1 {
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
                    <li>编辑作品信息</li>
                </ul>
            </div>
            <div class="formbody">
                <div id="usual1" class="usual">
                    <div id="tab2" class="tabson">
                        <table id="tab" runat="server" class="tablelistx" width="70%" style="margin: 0 auto;">
                            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                                <td width="25%" align="right" class="auto-style3">所属分类：</td>
                                <td width="75%" align="left" class="auto-style3">
                                    <ul class="seachformA">
                                        <li>
                                            <asp:DropDownList ID="DropGroup" runat="server"></asp:DropDownList></li>
                                    </ul>
                                </td>
                            </tr>
                            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                                <td width="25%" align="right">新闻标题：</td>
                                <td width="75%" align="left">
                                    <asp:TextBox ID="txtTlitle" runat="server" Width="350px" class="scinputx"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                                <td width="25%" align="right" class="auto-style1">新闻图片：</td>
                                <td width="75%" align="left" class="auto-style1">
                                    <div style="margin-left: 11px">
                                        <asp:FileUpload ID="FileUpload1" runat="server" class="scinputx" Width="350px" />
                                    </div>
                                </td>
                            </tr>
                            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                                <td width="25%" align="right" class="auto-style1">上传视频：</td>
                                <td width="75%" align="left" class="auto-style1" style="float:left;">
                                    <div style="margin-left: 11px;">
                                        <Upload:InputFile ID="AttachFile" runat="server" class="scinputa" /><asp:Button ID="Upload" CssClass="btn1 btn-xs btn-default" runat="server" Text="上传文件" OnClick="Upload_Click" />
                                        <asp:Label ID="uploadPath" runat="server" Text="" Width="178px"></asp:Label>
                                        <div id="ProgressBar" style="display: none;" dir="ltr">
                                        </div>
                                </td>
                            </tr>
                            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                                <td align="right">内容信息：</td>
                                <td align="left">
                                    <div style="margin-left: 11px">
                                        <textarea id="content1" ruant="sever" cols="100" rows="8" class="scinputx" style="width: 700px; height: 200px; visibility: hidden;" runat="server"></textarea>
                                        <%--<asp:TextBox runat="server" ID="txtfenleiName" CssClass="scinputx"></asp:TextBox>--%>
                                    </div>
                                </td>
                            </tr>
                            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                                <td align="right">置顶状态：</td>
                                <td>
                                    <asp:RadioButtonList ID="RadioState" runat="server" RepeatDirection="Horizontal" Font-Size="10px" Width="113px">
                                        <asp:ListItem Value="1">是</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                                <td align="right">资讯来源：</td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtFromWhere" CssClass="scinputx"></asp:TextBox></td>
                            </tr>
                            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                                <td align="right">发布时间：</td>
                                <td align="left">
                                    <asp:TextBox runat="server" ReadOnly="true" ID="txtPubtime" CssClass="scinputx" onClick="WdatePicker()"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2" height="25" align="center" bgcolor="#FFFFFF">
                                    <asp:Button runat="server" ID="ImgbtnSub" CssClass="btn1 btn-xs btn-default" Text="确定" OnClick="imgSub_Click" />


                                    &nbsp;&nbsp;<asp:Button runat="server" ID="imgs" CssClass="btn1 btn-xs btn-default" Text="取消" OnClick="imgback_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hid" runat="server" />
        <asp:HiddenField ID="hidSp" runat="server" />
    </form>
</body>
</html>

