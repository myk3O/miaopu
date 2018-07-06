<%@ Control Language="C#" AutoEventWireup="true" CodeFile="footer.ascx.cs" Inherits="Web_footer" %>
<div class="footer">
    <div class="innerbox">
        <div class="copyright">
            <div class="copyright_info">
                Copyright &copy; 2007-2013 <a href="http://maset.com.cn" title="maset.com.cn" target="_blank">
                    maset.com.cn</a> All Right Reserved. <a href="http://www.miitbeian.gov.cn" target="_blank">
                        沪ICP备13011682号</a>
            </div>
        </div>
    </div>
</div>
<%--<script type="text/javascript" src="js/jquery.min.js"></script>--%>
<!-- 代码 开始 -->
<div id="leftsead">
    <ul>
        <li><a href="javascript:void(0)" class="youhui">
            <img src="images/l02.png" width="47" height="49" class="shows" />
            <img src="images/a.png" width="57" height="49" class="hides" />
            <img src="images/weixin.jpg" width="145" class="2wm" style="display: none; margin: -100px 57px 0 0" />
        </a></li>
        <%-- <a href="http://wpa.qq.com/msgrd?v=3&uin=2934315729&site=qq&menu=yes" target="_blank">手机和PC通用--%>
        <%-- <a href="tencent://message/?uin=2934315729&Site=masetchina.com.cn&Menu=yes" target="_blank">仅PC可用--%>
        <li><a href="http://wpa.qq.com/msgrd?v=3&uin=2934315729&site=qq&menu=yes" target="_blank">
            <div class="hides" style="width: 161px; display: none;" id="qq">
                <div class="hides" id="p1">
                    <img src="images/ll04.png">
                </div>
                <div class="hides" id="p2">
                    <span style="color: #FFF; font-size: 13px">2934315729</span>
                </div>
            </div>
            <img src="images/l04.png" width="47" height="49" class="shows" />
        </a></li>
        <li id="tel"><a href="javascript:void(0)">
            <div class="hides" style="width: 161px; display: none;" id="tels">
                <div class="hides" id="p1">
                    <img src="images/ll05.png">
                </div>
                <div class="hides" id="p3">
                    <span style="color: #FFF; font-size: 12px">021-54484418</span>
                </div>
            </div>
            <img src="images/l05.png" width="47" height="49" class="shows" />
        </a></li>
        <li id="btn"><a id="top_btn">
            <div class="hides" style="width: 161px; display: none">
                <img src="images/ll06.png" width="161" height="49" />
            </div>
            <img src="images/l06.png" width="47" height="49" class="shows" />
        </a></li>
    </ul>
</div>
<script>
    $(document).ready(function () {
        $("#leftsead a").hover(function () {
            if ($(this).prop("className") == "youhui") {
                $(this).children("img.hides").show();
            } else {
                $(this).children("div.hides").show();
                $(this).children("img.shows").hide();
                $(this).children("div.hides").animate({ marginRight: '0px' }, '0');
            }
        }, function () {
            if ($(this).prop("className") == "youhui") {
                $(this).children("img.hides").hide();
            } else {
                $(this).children("div.hides").animate({ marginRight: '-163px' }, 0, function () { $(this).hide(); $(this).next("img.shows").show(); });
            }
        });
        $("#top_btn").click(function () { if (scroll == "off") return; $("html,body").animate({ scrollTop: 0 }, 600); });
        //右侧导航 - 二维码
        $(".youhui").mouseover(function () {
            $(this).children(".2wm").show();
        })
        $(".youhui").mouseout(function () {
            $(this).children(".2wm").hide();
        });
    });
</script>
<!-- 代码 结束 -->
