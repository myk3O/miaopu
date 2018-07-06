using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Maliang;

public partial class Web_gallery_100_grid : System.Web.UI.Page
{
    public string hotProduct;
    public string productClass;
    public string proCount;
    public string urlJump;
    public string ListTypeUrl;
    SqlHelper sh = new SqlHelper();


    private string nID
    {
        get
        {
            return ViewState["nID"] == null ? "1" : ViewState["nID"].ToString();
        }
        set
        {
            ViewState["nID"] = value;
        }
    }

    ServceHelper sher = new ServceHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != "" && Request.QueryString["id"] != null)
            {
                nID = Request.QueryString["id"].ToString();
                GetHotProduct();
                GetProductByClass(nID);

                string className = Request.QueryString["cN"] == null ? "" : Request.QueryString["cN"].ToString();
                urlJump = "<span><a href='gallery-100-grid.aspx?id=" + nID + "&cN= " + className + "' alt='' title=''> " + className + "</a></span> <span>&raquo;</span>";
                ListTypeUrl = "<a href='gallery-100-index.aspx?id=" + nID + "&cN= " + className + "' title='图文列表'><span class='list_index'>图文列表</span></a>";
                ListTypeUrl += "<a href='gallery-100-grid.aspx?id=" + nID + "&cN= " + className + "' title='橱窗'><span class='list_grid'>橱窗</span></a>";
            }

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



    private void GetHotProduct()
    {
        StringBuilder sb = new StringBuilder();
        string sql = @"select top 8 * from ML_ServiceArea aa left  join (
  select COUNT(probyCount) procount,b.proID from ML_Order a 
  join ML_OrderProduct b on a.nID=b.orderID
   where a.Auntid=1 and a.OrderState>=3 group by b.proID ) bb on aa.nID=bb.proID order by bb.procount  desc";

        DataTable dt = sh.ExecuteDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            sb.AppendLine("<li id='item-" + dr["nID"].ToString() + "' style='width: 100%' class='item textcenter'>");
            sb.AppendLine(" <div class='goodsbox'>");
            sb.AppendLine("<div class='goods_pic'>");
            sb.AppendLine("<a target='_blank' href='product-117.aspx?id=" + dr["nID"].ToString() + "' style='width: 220px; height: 220px;' class='pic'>");
            sb.AppendLine(" <img src='../Admin/upload_Img/Pruduct/" + dr["tPic"].ToString() + "' alt='" + dr["tTitle"].ToString() + "' /></a></div>");
            sb.AppendLine("<div class='info'>");
            sb.AppendLine("<div class='gname' style='height: 40px; line-height: 20px;'>");
            sb.AppendLine(" <a href='product-117.aspx?id=" + dr["nID"].ToString() + "' title='" + dr["tTitle"].ToString() + "'>" + dr["tTitle"].ToString() + "</a></div>");
            sb.AppendLine(" <div class='goodsinfo'>");
            sb.AppendLine("<div class='price'>");
            sb.AppendLine("<span class='salewords'>销售价:</span> <span class='money'>￥</span> <em class='price1'>" + dr["price"].ToString() + ".00  </em>");
            sb.AppendLine(" </div> </div> </div> </div>");
            sb.AppendLine("</li>");
        }
        hotProduct = sb.ToString();
    }



    private void GetProductByClass(string cid)
    {
        int flag = 0;
        StringBuilder sb = new StringBuilder();
        DataTable dt = sher.GetProduct(cid);
        proCount = dt.Rows.Count.ToString();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            flag++;
            if (i % 4 == 0)//4的整数倍
            {
                sb.AppendLine("<tr valign='top'>");
            }

            sb.AppendLine("<td id='pdt-" + dt.Rows[i]["nID"].ToString() + "' product='" + dt.Rows[i]["nID"].ToString() + "' width='25%;'>");
            sb.AppendLine("<div class='items-gallery'>");
            sb.AppendLine("<div class='goodpic' style='height: 220px;'>");
            sb.AppendLine("<a target='_blank' href='product-117.aspx?id=" + dt.Rows[i]["nID"].ToString() + "' style='width: 220px; height: 220px;'>");
            sb.AppendLine("<img src='../Admin/upload_Img/Pruduct/" + dt.Rows[i]["tPic"].ToString() + "' alt='" + dt.Rows[i]["tTitle"].ToString() + "' /> </a> </div>");
            sb.AppendLine("<div class='goodinfo'>");
            sb.AppendLine("<table width='100%' border='0' cellpadding='0' cellspacing='0' class='entry-content'>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td colspan='2'>");
            sb.AppendLine("<h6><a href='product-117.aspx?id=" + dt.Rows[i]["nID"].ToString() + "' title='" + dt.Rows[i]["tTitle"].ToString() + "' class='entry-title'>" + dt.Rows[i]["tTitle"].ToString() + "</a></h6>");
            sb.AppendLine("</td></tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td colspan='2'>");
            sb.AppendLine("<ul><li><span class='price1'>￥" + dt.Rows[i]["price"].ToString() + ".00</span></li></ul>");
            sb.AppendLine("</td></tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td> <span class='mktprice1'>￥" + dt.Rows[i]["price"].ToString() + ".00</span></td>");
            sb.AppendLine("<td>");
            sb.AppendLine("<ul class='button'>");
            sb.AppendLine("<li class='addcart'><a onclick='drop_cart_item(" + dt.Rows[i]["nID"].ToString() + ",this);' type='g' kucun='" + dt.Rows[i]["kucun"].ToString() + "' class='listact'");
            sb.AppendLine(" target='_dialog_minicart' title='加入购物车' rel='nofollow'>加入购物车</a> </li>");
            sb.AppendLine("<li class='vdetail zoom'><a title='红酒' href='product-117.aspx?id=" + dt.Rows[i]["nID"].ToString() + "' ");
            sb.AppendLine("pic='' target='_blank' class='listact'>查看详细 </a></li>");
            sb.AppendLine("</ul></td></tr></table>");
            sb.AppendLine("</div></div>");
            sb.AppendLine("</td>");
            if (flag == 4 || i == dt.Rows.Count - 1)
            {
                flag = 0;
                int colCount = 4 - flag;
                sb.AppendLine("<td colspan='" + colCount + "'>&nbsp;</td>");
                sb.AppendLine("</tr>");
            }
        }
        productClass = sb.ToString();
    }
}