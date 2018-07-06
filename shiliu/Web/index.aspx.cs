using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Web_index : System.Web.UI.Page
{
    public string pagenavi;
    public string Banner;
    public string ProductTab;
    WebHelper wh = new WebHelper();
    ServceHelper sh = new ServceHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetBanner();
            GetProduct();
            if (Session["LoginUserId"] == null || Session["LoginUserId"] == "")
            {
                // Response.Redirect("passport-login.aspx");
            }
            else
            {
                LoginUserID.Value = Session["LoginUserId"].ToString();
            }
        }
    }

    private void GetBanner()
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        DataTable dt = new DataTable();
        dt = wh.SelImg(1);
        int maxCount = dt.Rows.Count > 6 ? 6 : dt.Rows.Count;//最多6个
        for (int i = 0; i < maxCount; i++)
        {
            sb.AppendLine("<li>");
            if (string.IsNullOrEmpty(dt.Rows[i]["tMemo"].ToString()))
            {
                sb.AppendLine("<a href='product.aspx' title='" + dt.Rows[i]["tilte"].ToString() + "'>");
            }
            else
            {
                sb.AppendLine("<a href='product.aspx?id=" + dt.Rows[i]["tMemo"].ToString() + "' title='" + dt.Rows[i]["tilte"].ToString() + "'>");
            }
            sb.AppendLine("<img class='img' src='../Admin/upload_Img/Logo_Img/" + dt.Rows[i]["imgUrl"].ToString() + "' alt='" + dt.Rows[i]["tilte"].ToString() + "'></a></span> </li>");


            //加点
            sb2.AppendLine("<li><a href='#'></a></li>");

        }
        Banner = sb.ToString();
        pagenavi = sb2.ToString();
    }
    private void GetProduct()
    {
        int flag = 0;
        StringBuilder sb = new StringBuilder();
        DataTable dt = sh.GetProduct();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            flag++;
            if (i % 4 == 0)//4的整数倍
            {
                sb.AppendLine("<div class='goods_tab'>");
                sb.AppendLine("<ul class='goodslist'>");
            }

            sb.AppendLine("<li class='item textcenter'>");
            sb.AppendLine("<div class='goodsbox'>");
            sb.AppendLine("<div class='goods_pic'>");
            sb.AppendLine("<a target='_blank' href='product-117.aspx?id=" + dt.Rows[i]["nID"].ToString() + "' style='width: 220px; height: 220px;' class='pic'>");
            sb.AppendLine("<img src='../Admin/upload_Img/Pruduct/" + dt.Rows[i]["tPic"].ToString() + "' alt='" + dt.Rows[i]["tTitle"].ToString() + "' />");
            sb.AppendLine("</a>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class='info'>");
            sb.AppendLine("<div class='gname'>");
            sb.AppendLine("<a href='product-117.aspx?id=" + dt.Rows[i]["nID"].ToString() + "' title='" + dt.Rows[i]["tTitle"].ToString() + "'>" + dt.Rows[i]["tTitle"].ToString() + "</a>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class='goodsinfo'>");
            sb.AppendLine("<div class='price'>");
            sb.AppendLine("<span class='salewords'>销售价:</span> <span class='money'>￥</span> <em class='price1'> " + dt.Rows[i]["price"].ToString() + ".00 </em>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("<p class='btn_add'>");
            sb.AppendLine("<a href='javascript:void(0);'  class='add_to_cart' onclick='drop_cart_item(" + dt.Rows[i]["nID"].ToString() + ",this);' kucun='" + dt.Rows[i]["kucun"].ToString() + "'>加入购物车</a> ");
            sb.AppendLine("<a class='learn_more' href='product-117.aspx?id=" + dt.Rows[i]["nID"].ToString() + "' title='了解更多'>了解更多</a></p>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</li>");

            if (flag == 4 || i == dt.Rows.Count - 1)
            {
                flag = 0;
                sb.AppendLine("</ul>");
                sb.AppendLine("</div>");
            }

        }

        ProductTab = sb.ToString();
    }

}