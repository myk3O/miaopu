<%@ Page Language="C#" AutoEventWireup="true" CodeFile="news.aspx.cs" Inherits="Web_news" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Web/footer.ascx" TagName="footer" TagPrefix="ft" %>
<%@ Register Src="~/Web/TopHead.ascx" TagName="header" TagPrefix="hd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>新闻列表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="keywords" content="">
    <meta name="description" content="">
    <meta name="generator" content="ShopEx 4.8.5">
    <meta name="robots" content="noindex,noarchive,nofollow" />
    <link rel="icon" href="../image/bitbug_favicon.ico" type="image/x-icon" />
    <link rel="bookmark" href="../image/bitbug_favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" href="statics/style.css" type="text/css" />
    <script type="text/javascript" src="statics/script/tools.js"></script>
    <script type="text/javascript" src="statics/script/goodscupcake.js"></script>
    <link href="themes/1394732638/images/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="themes/1394732638/images/upc.js"></script>
</head>
<body>
    <hd:header ID="Header1" runat="server" />
    <div class="main innerbox">
        <div class="nav">
            <div class="Navigation">
                您当前的位置： <span>首页</span> > <span class="now">活动资讯</span>
            </div>
        </div>
        <%--        <script>

            window.addEvent("domready", function () {

                $$(".MemberMenuList span")[0].setStyle("border-top", "none");

                $$(".MemberMenuList").each(function (item, index) {

                    item.addEvents({

                        mouseenter: function () {
                            this.getElement('span').addClass('hover');
                            $$(".MemberMenuList ul")[index].setStyle("background", "#f2f2f2");
                        },
                        mouseleave: function () {
                            this.getElement('span').removeClass('hover');
                            $$(".MemberMenuList ul")[index].setStyle("background", "#fff");
                        }
                    });

                });

                $$(".memberlist > tbody > tr").each(function (item, index) {

                    if (index > 0 && index % 2 == 0) { item.setStyle("background", "#f7f7f7"); }

                });
            });
        </script>--%>
        <div class="MemberBox">
            <div id="demo">
                <ul class="list">
                    <%--  <li class="list-item NewsList"><a href="NewsShow.html">
                        <img src="#" width="250" height="160" /></a>
                        <h1>
                            <a href="NewsShow.html">2014年11月份开班总汇！</a></h1>
                        <h2>
                            2014/10/27</h2>
                        <h3>
                            培训目标：从事母婴护理（月嫂）工作，为孕产妇分娩前后、新生儿出生后提供保健护理方面工作。</h3>
                        <div class="cls">
                        </div>
                    </li>--%>
                    <%=activityStr %>
                </ul>
                <div class="jplist-panel">
                    <div class="jplist-pagination" data-control-type="pagination" data-control-name="paging"
                        data-control-action="paging" data-items-per-page="8">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <ft:footer ID="Footer1" runat="server" />
</body>
</html>
