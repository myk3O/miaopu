<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TeacherEdit.aspx.cs" Inherits="Admin_Teacher_TeacherEdit" %>

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
            var editor2 = K.create('#txttMemo', {
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
                    <li>编辑讲师</li>
                </ul>
            </div>
            <div class="formbody">
                <div id="usual1" class="usual">
                    <div id="tab2" class="tabson">
                        <table id="tab" runat="server" class="tablelistx" width="100%" style="margin: 0 auto;">



                            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1);">
                                <td width="25%" align="right" class="auto-style1">讲师名称：
                                </td>
                                <td width="75%" align="left" class="auto-style1">

                                    <asp:TextBox ID="txtname" runat="server" Width="350px" class="scinputx"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="r5" runat="server" ErrorMessage="*请输入名称" ForeColor="Red"
                                        ControlToValidate="txtname" ValidationGroup="btn"></asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1);">
                                <td width="25%" align="right" class="auto-style1">讲师职称：
                                </td>
                                <td width="75%" align="left" class="auto-style1">

                                    <asp:TextBox ID="txtjob" runat="server" Width="350px" class="scinputx"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*请输入职称" ForeColor="Red"
                                        ControlToValidate="txtjob" ValidationGroup="btn"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                                <td width="25%" align="right" class="auto-style1">讲师头像：
                                </td>
                                <td width="75%" align="left" class="auto-style1">
                                    <div style="margin-left: 11px">
                                        <asp:FileUpload ID="viewFiles2" runat="server" class="scinputx" Width="350px" /><a
                                            style="color: Red">(*图片大小建议不超过1M)</a>
                                        <%--             <input type="file" accept="image/*;capture=camera" runat="server" id="viewFiles1" />--%>
                                        <img id="viewImg2" style="width: 124px; height: 100px; float: left;" />
                                        <%=picTou %>
                                    </div>
                                </td>
                            </tr>
                            <tr style="border-bottom: 1px solid rgba(0,0,0,0.1)">
                                <td align="right">讲师详细介绍：
                                </td>
                                <td align="left">
                                    <div style="margin-left: 11px">
                                        <textarea id="txttMemo" ruant="sever" cols="100" rows="8" class="scinputx" style="width: 700px; height: 200px; visibility: hidden;" runat="server"></textarea>
                                    </div>
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
        <asp:HiddenField ID="hidTou" runat="server" />
    </form>
</body>
</html>
<script type="text/javascript">
    (function () {
        //------------教师头像
        var viewFiles2 = document.getElementById("viewFiles2");
        var viewImg2 = document.getElementById("viewImg2");
        function viewFile2(file) {
            //通过file.size可以取得图片大小
            var reader = new FileReader();
            reader.onload = function (evt) {
                viewImg2.src = evt.target.result;
            }
            reader.readAsDataURL(file);
        }
        viewFiles2.addEventListener("change", function () {
            //通过 this.files 取到 FileList ，这里只有一个
            viewFile2(this.files[0]);
        }, false);
    })();
</script>
