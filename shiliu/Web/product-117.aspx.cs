using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maliang;
using System.Data;
using System.Text;

public partial class Web_product_117 : System.Web.UI.Page
{
    public string imgStr;
    public string productStr;
    public string productMemo;
    public string tuijian;
    public string hotSold;
    public string kucun;
    public string _title;
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
    SqlHelper sh = new SqlHelper();
    ServceHelper sher = new ServceHelper();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")
        {
            nID = Request.QueryString["id"].ToString();
            GetProduct(nID);
            GetHotProduct();
            GetTJProduct();

            productId.Value = nID;
        }
        else
        {
            Response.Redirect("index.aspx");
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

    private void GetProduct(string nid)
    {
        StringBuilder sbimg = new StringBuilder();
        StringBuilder sbpro = new StringBuilder();
        DataTable dt = sher.getMang(nid);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sbimg.AppendLine("<div class='goods-detail-pic' bigpicsrc='../Admin/upload_Img/Pruduct/" + dt.Rows[i]["tPic"].ToString() + "'>");
            sbimg.AppendLine("<a href='../Admin/upload_Img/Pruduct/" + dt.Rows[i]["tPic"].ToString() + "' target='_blank'");
            sbimg.AppendLine("style='color: #fff; font-size: 263px; width: 380px; height: 380px; font-size: 332.5px; font-family: Arial;display: table-cell; vertical-align: middle;'>");
            sbimg.AppendLine("<img src='../Admin/upload_Img/Pruduct/" + dt.Rows[i]["tPic"].ToString() + "' alt='" + dt.Rows[i]["tTitle"].ToString() + "' style='width: 380px;height: 380px;' />");
            sbimg.AppendLine("</a></div>");



            sbpro.AppendLine("<h1 class='goodsname'>" + dt.Rows[i]["tTitle"].ToString() + "</h1>");
            sbpro.AppendLine("<ul class='goodsprops clearfix'>");
            sbpro.AppendLine("<li><span>品 牌：</span> 马赛特酒庄</li>");
            sbpro.AppendLine("<li><span>系 列：</span> " + dt.Rows[i]["ptclass"].ToString() + "</li>");
            sbpro.AppendLine("<li><span>产 地：</span> " + dt.Rows[i]["chandi"].ToString() + "</li>");
            sbpro.AppendLine("</ul>");
            sbpro.AppendLine("<ul class='goods-price list'>");
            sbpro.AppendLine("<li><span>市场价：</span><i class='mktprice1'> ￥" + dt.Rows[i]["price"].ToString() + ".00 </i></li>");
            sbpro.AppendLine("<li><span>销售价：</span> <span class='price1'>￥" + dt.Rows[i]["price"].ToString() + ".00 </span></li>");
            sbpro.AppendLine("</ul>");
            productStr = sbpro.ToString();
            imgStr = sbimg.ToString();
            productMemo = dt.Rows[i]["tMemo"].ToString();
            kucun = dt.Rows[i]["kucun"].ToString();

            kuCun.Value = kucun;
            price.Value = dt.Rows[i]["price"].ToString();

            _title = "<span><a href='gallery-100-grid.aspx?id=" + dt.Rows[i]["classID1"].ToString() + "'>" + dt.Rows[i]["ptclass"].ToString() + "</a></span> <span>&raquo;</span><span>" + dt.Rows[i]["tTitle"].ToString() + "</span> <span>&raquo;</span>";

        }
    }

    private void GetHotProduct()
    {
        StringBuilder sb = new StringBuilder();
        string sql = @"select top 5 * from ML_ServiceArea aa left  join (
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
        hotSold = sb.ToString();
    }


    private void GetTJProduct()
    {
        StringBuilder sb = new StringBuilder();
        string sql = @"select top 8 * from ML_ServiceArea  order by oHide  desc";

        DataTable dt = sh.ExecuteDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            sb.AppendLine("<li id='item-" + dr["nID"].ToString() + "' style='width: 100%' class='item textcenter'>");
            sb.AppendLine(" <div class='goodsbox'>");
            sb.AppendLine("<div class='goods_pic'>");
            sb.AppendLine("<a target='_blank' href='product-117.aspx?id=" + dr["nID"].ToString() + "' style='width: 220px; height: 220px;' class='pic'>");
            sb.AppendLine(" <img src='../Admin/upload_Img/Pruduct/" + dr["tPic"].ToString() + "' alt='" + dr["tTitle"].ToString() + "' /></a></div>");
            sb.AppendLine("<div class='info'>");
            sb.AppendLine("<div class='gname'>");
            sb.AppendLine(" <a href='product-117.aspx?id=" + dr["nID"].ToString() + "' title='" + dr["tTitle"].ToString() + "'>" + dr["tTitle"].ToString() + "</a></div>");
            sb.AppendLine(" <div class='goodsinfo'>");
            sb.AppendLine("<div class='price'>");
            sb.AppendLine("<span class='salewords'>销售价:</span> <span class='money'>￥</span> <em class='price1'>" + dr["price"].ToString() + ".00  </em>");
            sb.AppendLine(" </div> </div> </div> </div>");
            sb.AppendLine("</li>");
        }
        tuijian = sb.ToString();
    }




    //protected void btnBuy_Click(object sender, EventArgs e)
    //{

    //}



    //protected void btnAddCart_Click(object sender, EventArgs e)
    //{

    //}
}