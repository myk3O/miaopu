<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductEdit.aspx.cs" Inherits="Admin_Pruduct_ProductEdit" %>

<%@ Register Assembly="Brettle.Web.NeatUpload.HashedInputFile" Namespace="Brettle.Web.NeatUpload"
    TagPrefix="HashedUpload" %>
<%@ Register Assembly="Brettle.Web.NeatUpload" Namespace="Brettle.Web.NeatUpload"
    TagPrefix="Upload" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="../../swfupload/swfupload.js"></script>
    <link href="../../css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/select.css" rel="stylesheet" type="text/css" />
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
    <form id="form1" method="post" runat="server">
        <div>
            <div class="place">
                <span>位置：</span>
                <ul class="placeul">
                    <li><a href="#">首页</a></li>
                    <li>编辑视频</li>
                </ul>
            </div>
            <div class="formbody">
                <div id="usual1" class="usual">
                    <div id="tab2" class="tabson">
                        <table id="tab" runat="server" class="tablelistx" width="100%" style="margin: 0 auto;">
                            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                                <td width="25%" align="right" class="auto-style3">视频系列：
                                </td>
                                <td width="75%" align="left" class="auto-style3">
                                    <ul class="seachformA">
                                        <li>
                                            <asp:DropDownList ID="DropGroup" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="r234" runat="server" ErrorMessage="*请选择系列" ForeColor="Red"
                                                ControlToValidate="DropGroup" InitialValue="-1" ValidationGroup="btn"></asp:RequiredFieldValidator>
                                        </li>
                                    </ul>
                                </td>
                            </tr>

                            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                                <td width="25%" align="right">视频名称：
                                </td>
                                <td width="75%" align="left">
                                    <asp:TextBox ID="txtTlitle" runat="server" Width="350px" class="scinputx"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="r5" runat="server" ErrorMessage="*请输入名称" ForeColor="Red"
                                        ControlToValidate="txtTlitle" ValidationGroup="btn"></asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1);">
                                <td width="25%" align="right" class="auto-style1">视频目录：
                                </td>
                                <td width="75%" align="left" class="auto-style1">

                                    <asp:TextBox ID="txtmulu" runat="server" Width="350px" class="scinputx"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatormulu" runat="server" ErrorMessage="*请输入目录" ForeColor="Red"
                                        ControlToValidate="txtmulu" ValidationGroup="btn"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                                     <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                                <td width="25%" align="right" class="auto-style1">视频封面：
                                </td>
                                <td width="75%" align="left" class="auto-style1">
                                    <div style="margin-left: 11px">
                                        <asp:FileUpload ID="viewFiles1" runat="server" class="scinputx" Width="350px" /><a
                                            style="color: Red">(*图片大小建议不超过1M)</a>
                                        <%--             <input type="file" accept="image/*;capture=camera" runat="server" id="viewFiles1" />--%>
                                        <img id="viewImg1" style="width: 124px; height: 100px; float: left;" />
                                        <%=picUrl %>
                                    </div>
                                </td>
                            </tr>
                            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1);">
                                <td width="25%" align="right" class="auto-style1">播放次序（数字小的先播放）：
                                </td>
                                <td width="75%" align="left" class="auto-style1">

                                    <asp:TextBox ID="txtpaixu" runat="server" Width="350px" class="scinputx" onkeyup="value=value.replace(/[^\d]/g,'')"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*请输入播放次序" ForeColor="Red"
                                        ControlToValidate="txtpaixu" ValidationGroup="btn"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                                <td align="right">发布时间：
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ReadOnly="true" ID="txtPubtime" CssClass="scinputx" onClick="WdatePicker()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" height="25" align="center" bgcolor="#FFFFFF">
                                    <asp:Button runat="server" ID="ImgbtnSub" CssClass="btn1 btn-xs btn-default" Text="确定"
                                        ValidationGroup="btn" OnClick="imgSub_Click" />
                                    &nbsp;&nbsp;<asp:Button runat="server" ID="imgs" CssClass="btn1 btn-xs btn-default"
                                        Text="取消" OnClick="imgback_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hidPurl" runat="server" />
        <asp:HiddenField ID="hidPName" runat="server" />
        <asp:HiddenField ID="hidVurl" runat="server" />
        <asp:HiddenField ID="hidVName" runat="server" />
    </form>
</body>
</html>
<script type="text/javascript">
    function jump(e) {
        var str = "../ImgConfig/ProductSelect.aspx?paixu=" + e;
        window.open(str, 'top', 'width=1000,height=700,menubar=0,scrollbars=1, resizable=1,status=1,titlebar=0,toolbar=0,location=0');
    }

    (function () {
        //-----------视频封面
        var viewFiles1 = document.getElementById("viewFiles1");
        var viewImg1 = document.getElementById("viewImg1");
        function viewFile1(file) {
            //通过file.size可以取得图片大小
            var reader = new FileReader();
            reader.onload = function (evt) {
                viewImg1.src = evt.target.result;
            }
            reader.readAsDataURL(file);
        }
        viewFiles1.addEventListener("change", function () {
            //通过 this.files 取到 FileList ，这里只有一个
            viewFile1(this.files[0]);
        }, false);

    })();
</script>
