using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Main_Left : System.Web.UI.Page
{
    public string urlLeft;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["acid"] == null || Session["acid"].ToString() == "") { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            StringBuilder sb = new StringBuilder();


            //sb.AppendLine("<dd><div class='title'><span> <img src='../../images/leftico02.png' /></span>我的信息</div>");
            //sb.AppendLine("<ul class='menuson'>");
            //sb.AppendLine("<li><cite></cite><a href='../DaiLi/HomeMakByMak.aspx' target='rightFrame'>总部信息</a><i></i></li>");
            //sb.AppendLine("</ul></dd>");

            sb.AppendLine("<dd><div class='title'><span> <img src='../../images/leftico02.png' /></span>网站基本设置</div>");
            sb.AppendLine("<ul class='menuson'>");
            sb.AppendLine("<li><cite></cite><a href='../biliConfig.aspx' target='rightFrame'>全站比例配置</a><i></i></li>");
            sb.AppendLine("<li><cite></cite><a href='../SetConfig.aspx' target='rightFrame'>网站信息设置</a><i></i></li>");
            sb.AppendLine("<li><cite></cite><a href='../ImgConfig/ImgMain.aspx' target='rightFrame'>首页图片设置</a><i></i></li>");
            //sb.AppendLine("<li><cite></cite><a href='../CustomerConfig.aspx' target='rightFrame'>QQ客服</a><i></i></li>");
            sb.AppendLine("</ul></dd>");

            //sb.AppendLine("<dd><div class='title'><span> <img src='../../images/leftico02.png' /></span>企业信息管理</div>");
            //sb.AppendLine("<ul class='menuson'>");
            //sb.AppendLine("<li><cite></cite><a href='../Info/InfoMain.aspx' target='rightFrame'>企业信息列表</a><i></i></li>");
            //sb.AppendLine("<li><cite></cite><a href='../Info/InfoEdit.aspx' target='rightFrame'>企业信息添加</a><i></i></li>");
            //sb.AppendLine("<li><cite></cite><a href='../Info/InfoClass.aspx' target='rightFrame'>企业信息分类</a><i></i></li>");
            //sb.AppendLine("</ul></dd>");


            sb.AppendLine("<dd><div class='title'><span> <img src='../../images/leftico02.png' /></span>公告管理</div>");
            sb.AppendLine("<ul class='menuson'>");
            sb.AppendLine("<li><cite></cite><a href='../News/NewsMain.aspx' target='rightFrame'>公告列表</a><i></i></li>");
            sb.AppendLine("<li><cite></cite><a href='../News/NewsEdit.aspx' target='rightFrame'>公告添加</a><i></i></li>");
            sb.AppendLine("</ul></dd>");


            //sb.AppendLine("<dd><div class='title'><span> <img src='../../images/leftico02.png' /></span>代理商管理</div>");
            //sb.AppendLine("<ul class='menuson'>");
            //sb.AppendLine("<li><cite></cite><a href='../Members/MemberMain.aspx' target='rightFrame'>代理商列表</a><i></i></li>");
            //sb.AppendLine("<li><cite></cite><a href='../Members/ApplyShopMain.aspx' target='rightFrame'>申请列表</a><i></i></li>");

            //sb.AppendLine("</ul></dd>");

            sb.AppendLine("<dd><div class='title'><span> <img src='../../images/leftico02.png' /></span>排行统计</div>");
            sb.AppendLine("<ul class='menuson'>");
            sb.AppendLine("<li><cite></cite><a href='../Order/OrderPrice.aspx' target='rightFrame'>分红统计</a><i></i></li>");
            sb.AppendLine("<li><cite></cite><a href='../Order/WeekXueBa.aspx' target='rightFrame'>榜上有名</a><i></i></li>");
            sb.AppendLine("<li><cite></cite><a href='../Order/JingTieList.aspx' target='rightFrame'>津贴记录</a><i></i></li>");
            sb.AppendLine("</ul></dd>");

            sb.AppendLine("<dd><div class='title'><span> <img src='../../images/leftico02.png' /></span>提现管理</div>");
            sb.AppendLine("<ul class='menuson'>");
            sb.AppendLine("<li><cite></cite><a href='../Order/MoneyOrder.aspx' target='rightFrame'>提现申请记录</a><i></i></li>");

            sb.AppendLine("</ul></dd>");


            sb.AppendLine("<dd><div class='title'><span> <img src='../../images/leftico02.png' /></span>订单管理</div>");
            sb.AppendLine("<ul class='menuson'>");
            sb.AppendLine("<li><cite></cite><a href='../Order/OrderMain.aspx' target='rightFrame'>交易记录</a><i></i></li>");
            //sb.AppendLine("<li><cite></cite><a href='../Order/OrderMainFaHuo.aspx' target='rightFrame'>待发货订单</a><i></i></li>");
            //sb.AppendLine("<li><cite></cite><a href='../Order/OrderMainTuiHuo.aspx' target='rightFrame'>退货订单</a><i></i></li>");
            //sb.AppendLine("<li><cite></cite><a href='../Order/OrderMainTuiKuan.aspx' target='rightFrame'>待退款订单</a><i></i></li>");
            //sb.AppendLine("<li><cite></cite><a href='../Order/OrderMainGuanBi.aspx' target='rightFrame'>交易关闭</a><i></i></li>");
            //sb.AppendLine("<li><cite></cite><a href='../Order/OrderMainWanCheng.aspx' target='rightFrame'>交易完成</a><i></i></li>");
            sb.AppendLine("</ul></dd>");

            sb.AppendLine("<dd><div class='title'><span> <img src='../../images/leftico02.png' /></span>讲师管理</div>");
            sb.AppendLine("<ul class='menuson'>");
            sb.AppendLine("<li><cite></cite><a href='../Teacher/TeacherEdit.aspx' target='rightFrame'>添加讲师</a><i></i></li>");
            sb.AppendLine("<li><cite></cite><a href='../Teacher/TeacherMain.aspx' target='rightFrame'>讲师列表</a><i></i></li>");

            //sb.AppendLine("<li><cite></cite><a href='../Pruduct/ServceClass.aspx' target='rightFrame'>视频分类</a><i></i></li>");
            //sb.AppendLine("<li><cite></cite><a href='../Pruduct/ServceEdit_A.aspx' target='rightFrame'>添加规格属性</a><i></i></li>");
            //sb.AppendLine("<li><cite></cite><a href='../Pruduct/ServceMain_A.aspx' target='rightFrame'>产品规格属性列表</a><i></i></li>");
            //sb.AppendLine("<li><cite></cite><a href='../Pruduct/ServceClass3.aspx' target='rightFrame'>酒的颜色</a><i></i></li>");
            //sb.AppendLine("<li><cite></cite><a href='../Pruduct/ServceClass2.aspx' target='rightFrame'>酒内糖份</a><i></i></li>");
            //sb.AppendLine("<li><cite></cite><a href='../Pruduct/ServceClass1.aspx' target='rightFrame'>含不含二氧化碳</a><i></i></li>");
            sb.AppendLine("</ul></dd>");

            sb.AppendLine("<dd><div class='title'><span> <img src='../../images/leftico02.png' /></span>视频管理</div>");
            sb.AppendLine("<ul class='menuson'>");
            //sb.AppendLine("<li><cite></cite><a href='../Pruduct/ServceEdit.aspx' target='rightFrame'>添加视频</a><i></i></li>");
            sb.AppendLine("<li><cite></cite><a href='../Pruduct/ProductClassEdit.aspx' target='rightFrame'>添加系列</a><i></i></li>");
            sb.AppendLine("<li><cite></cite><a href='../Pruduct/ProductClassMain.aspx' target='rightFrame'>系列列表</a><i></i></li>");
            sb.AppendLine("<li><cite></cite><a href='../Pruduct/ProductEdit.aspx' target='rightFrame'>添加视频</a><i></i></li>");
            sb.AppendLine("<li><cite></cite><a href='../Pruduct/ProductMain.aspx' target='rightFrame'>视频列表</a><i></i></li>");

            //sb.AppendLine("<li><cite></cite><a href='../Pruduct/ServceClass.aspx' target='rightFrame'>视频分类</a><i></i></li>");
            //sb.AppendLine("<li><cite></cite><a href='../Pruduct/ServceEdit_A.aspx' target='rightFrame'>添加规格属性</a><i></i></li>");
            //sb.AppendLine("<li><cite></cite><a href='../Pruduct/ServceMain_A.aspx' target='rightFrame'>产品规格属性列表</a><i></i></li>");
            //sb.AppendLine("<li><cite></cite><a href='../Pruduct/ServceClass3.aspx' target='rightFrame'>酒的颜色</a><i></i></li>");
            //sb.AppendLine("<li><cite></cite><a href='../Pruduct/ServceClass2.aspx' target='rightFrame'>酒内糖份</a><i></i></li>");
            //sb.AppendLine("<li><cite></cite><a href='../Pruduct/ServceClass1.aspx' target='rightFrame'>含不含二氧化碳</a><i></i></li>");
            sb.AppendLine("</ul></dd>");


            sb.AppendLine("<dd><div class='title'><span> <img src='../../images/leftico02.png' /></span>会员信息管理</div>");
            sb.AppendLine("<ul class='menuson'>");
            sb.AppendLine("<li><cite></cite><a href='../Members/Member_Main.aspx' target='rightFrame'>会员信息列表</a><i></i></li>");
            sb.AppendLine("<li><cite></cite><a href='../Members/banzhang.aspx?level=3' target='rightFrame'>班长奖学金</a><i></i></li>");
            sb.AppendLine("<li><cite></cite><a href='../Members/banzhang.aspx?level=4' target='rightFrame'>班主任奖学金</a><i></i></li>");
            sb.AppendLine("<li><cite></cite><a href='../Members/banzhang.aspx?level=5' target='rightFrame'>校长奖学金</a><i></i></li>");
            //sb.AppendLine("<li><cite></cite><a href='../Members/MemberMain.aspx' target='rightFrame'>经销商列表</a><i></i></li>");
            sb.AppendLine("</ul></dd>");


            sb.AppendLine("<dd><div class='title'><span> <img src='../../images/leftico02.png' /></span>管理员管理</div>");
            sb.AppendLine("<ul class='menuson'>");
            sb.AppendLine("<li><cite></cite><a href='../AdminManag/AdminMain.aspx' target='rightFrame'>管理员列表</a><i></i></li>");
            sb.AppendLine("<li><cite></cite><a href='../AdminManag/AdminEdit.aspx' target='rightFrame'>管理员添加</a><i></i></li>");
            sb.AppendLine("</ul></dd>");

            urlLeft = sb.ToString();
        }
    }
}