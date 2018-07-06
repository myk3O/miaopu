<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuestionMain.aspx.cs" Inherits="Admin_Questionnaire_QuestionMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        body
        {
            font-family: '微软雅黑';
            margin: 0 auto;
            min-width: 980px;
        }
        ul
        {
            display: block;
            margin: 0;
            padding: 0;
            list-style: none;
        }
        li
        {
            display: block;
            margin: 0;
            padding: 0;
            list-style: none;
        }
        img
        {
            border: 0;
        }
        dl, dt, dd, font
        {
            margin: 0;
            padding: 0;
            display: block;
        }
        a, a:focus
        {
            text-decoration: none;
            color: #000;
            outline: none;
            blr: expression(this.onFocus=this.blur());
        }
        a:hover
        {
            color: #00a4ac;
            text-decoration: none;
        }
        cite
        {
            font-style: normal;
        }
        h2
        {
            font-weight: normal;
        }
        .place
        {
            height: 40px;
            background: url(../../images/righttop.gif) repeat-x;
            font-size: 12px;
        }
        .place span
        {
            line-height: 40px;
            font-weight: bold;
            float: left;
            margin-left: 12px;
        }
        .placeul li
        {
            float: left;
            line-height: 40px;
            padding-left: 7px;
            padding-right: 12px;
            background: url(../../images/rlist.gif) no-repeat right;
        }
        
        .placeulss li
        {
            line-height: 40px;
            padding-left: 7px;
            padding-right: 12px;
            background: url(../../images/rlist.gif) no-repeat right;
        }
        .placeul li:last-child
        {
            background: none;
        }
        .formbody
        {
            padding: 0.5% 5%;
        }
    </style>
    <script src="../../Js/jquery-1.4.3.min.js" type="text/javascript"></script>
    <script src="../../Js/jquery.jqprint-0.3.js" type="text/javascript"></script>
    <script language="javascript">
        function a() {
            $("#ddd").jqprint();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="button" style="margin-left: 50%" onclick="a()" value="打印" />
    <div id="ddd">
        <div class="place">
            <span></span>
            <ul class="placeulss" style="text-align: center">
                <li><a href="#"></a></li>
                <li style="text-align: center">
                    <%=BindTitle%></li>
            </ul>
        </div>
        <div class="formbody">
            <%=BindStr %>
            <%--   <div style="line-height: 32px; font-weight: bold; font-size: 14px; margin-top: 20px;">
                <span style="color: #3d81ee;">第1题：</span><span style="color: #333333;">请问你的CSS已经使用了()年？</span></div>
            <table width="100%" border="0" cellpadding="6" cellspacing="1" bgcolor="#CBCBCB"
                style="font-size: 12px;">
                <thead>
                    <tr align="center" style="font-weight: bold; background: #f5f8fa">
                        <td>
                            选项
                        </td>
                        <td align="center" style="width: 10%;">
                            小计
                        </td>
                        <td align="left" style="width: 30%;">
                            比例
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <tr style="background: white">
                        <td bgcolor="#FFFFFF" val="1">
                            1年以下
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            10
                        </td>
                        <td align="" bgcolor="#FFFFFF">
                            <div style="margin-top: 3px; float: left;">
                                15.87%</div>
                        </td>
                    </tr>
                    <tr style="background: #eff6fb">
                        <td bgcolor="#FFFFFF" val="2">
                            1-2年
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            17
                        </td>
                        <td align="" bgcolor="#FFFFFF">
                            <div style="margin-top: 3px; float: left;">
                                26.98%</div>
                        </td>
                    </tr>
                    <tr style="background: white">
                        <td bgcolor="#FFFFFF" val="3">
                            2-3年
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            17
                        </td>
                        <td align="" bgcolor="#FFFFFF">
                            <div style="margin-top: 3px; float: left;">
                                26.98%</div>
                        </td>
                    </tr>
                    <tr style="background: #eff6fb">
                        <td bgcolor="#FFFFFF" val="4">
                            3年以上
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            19
                        </td>
                        <td align="" bgcolor="#FFFFFF">
                            <div style="margin-top: 3px; float: left;">
                                30.16%</div>
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <b>本题有效填写人次</b>
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <b>63</b>
                        </td>
                        <td bgcolor="#FFFFFF">
                        </td>
                    </tr>
                </tfoot>
            </table>
            <div style="line-height: 32px; font-weight: bold; font-size: 14px; margin-top: 20px;">
                <span style="color: #3d81ee;">第2题：</span><span style="color: #333333;">请问你的CSS已经使用了()年？</span></div>
            <table width="100%" border="0" cellpadding="6" cellspacing="1" bgcolor="#CBCBCB"
                style="font-size: 12px;">
                <thead>
                    <tr align="center" style="font-weight: bold; background: #f5f8fa">
                        <td>
                            选项
                        </td>
                        <td align="center" style="width: 10%;">
                            小计
                        </td>
                        <td align="left" style="width: 30%;">
                            比例
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <tr style="background: white">
                        <td bgcolor="#FFFFFF" val="1">
                            1年以下
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            10
                        </td>
                        <td align="" bgcolor="#FFFFFF">
                            <div style="margin-top: 3px; float: left;">
                                15.87%</div>
                        </td>
                    </tr>
                    <tr style="background: #eff6fb">
                        <td bgcolor="#FFFFFF" val="2">
                            1-2年
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            17
                        </td>
                        <td align="" bgcolor="#FFFFFF">
                            <div style="margin-top: 3px; float: left;">
                                26.98%</div>
                        </td>
                    </tr>
                    <tr style="background: white">
                        <td bgcolor="#FFFFFF" val="3">
                            2-3年
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            17
                        </td>
                        <td align="" bgcolor="#FFFFFF">
                            <div style="margin-top: 3px; float: left;">
                                26.98%</div>
                        </td>
                    </tr>
                    <tr style="background: #eff6fb">
                        <td bgcolor="#FFFFFF" val="4">
                            3年以上
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            19
                        </td>
                        <td align="" bgcolor="#FFFFFF">
                            <div style="margin-top: 3px; float: left;">
                                30.16%</div>
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <b>本题有效填写人次</b>
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <b>63</b>
                        </td>
                        <td bgcolor="#FFFFFF">
                        </td>
                    </tr>
                </tfoot>
            </table>
            <div style="line-height: 32px; font-weight: bold; font-size: 14px; margin-top: 20px;">
                <span style="color: #3d81ee;">第3题：</span><span style="color: #333333;">请问你的CSS已经使用了()年？</span></div>
            <table width="100%" border="0" cellpadding="6" cellspacing="1" bgcolor="#CBCBCB"
                style="font-size: 12px;">
                <thead>
                    <tr align="center" style="font-weight: bold; background: #f5f8fa">
                        <td>
                            选项
                        </td>
                        <td align="center" style="width: 10%;">
                            小计
                        </td>
                        <td align="left" style="width: 30%;">
                            比例
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <tr style="background: white">
                        <td bgcolor="#FFFFFF" val="1">
                            1年以下
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            10
                        </td>
                        <td align="" bgcolor="#FFFFFF">
                            <div style="margin-top: 3px; float: left;">
                                15.87%</div>
                        </td>
                    </tr>
                    <tr style="background: #eff6fb">
                        <td bgcolor="#FFFFFF" val="2">
                            1-2年
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            17
                        </td>
                        <td align="" bgcolor="#FFFFFF">
                            <div style="margin-top: 3px; float: left;">
                                26.98%</div>
                        </td>
                    </tr>
                    <tr style="background: white">
                        <td bgcolor="#FFFFFF" val="3">
                            2-3年
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            17
                        </td>
                        <td align="" bgcolor="#FFFFFF">
                            <div style="margin-top: 3px; float: left;">
                                26.98%</div>
                        </td>
                    </tr>
                    <tr style="background: #eff6fb">
                        <td bgcolor="#FFFFFF" val="4">
                            3年以上
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            19
                        </td>
                        <td align="" bgcolor="#FFFFFF">
                            <div style="margin-top: 3px; float: left;">
                                30.16%</div>
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <b>本题有效填写人次</b>
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <b>63</b>
                        </td>
                        <td bgcolor="#FFFFFF">
                        </td>
                    </tr>
                </tfoot>
            </table>--%>
        </div>
    </div>
    </form>
</body>
</html>
