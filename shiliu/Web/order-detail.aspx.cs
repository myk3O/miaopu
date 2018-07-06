using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Maliang;
public partial class Web_order_detail : System.Web.UI.Page
{
    public string orderheader;//订单编号
    public string orderbody;//下单时间
    public string orderState;
    public string Product;
    public string AllPrice;

    private string UserID
    {
        get
        {
            return ViewState["UserID"] == null ? "" : ViewState["UserID"].ToString();
        }
        set
        {
            ViewState["UserID"] = value;
        }
    }


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
    SqlHelper her = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["LoginUserId"] == null || Session["LoginUserId"] == "")
            {
                Response.Redirect("passport-login.aspx");
            }
            else
            {
                UserID = Session["LoginUserId"].ToString();

            }

            if (Request.QueryString["id"] != "" && Request.QueryString["id"] != null)
            {
                nID = Request.QueryString["id"].ToString();
                GetOrder(nID);
                GetProduct(nID);
            }
        }

    }
    private void GetOrder(string nid)
    {
        StringBuilder sbh = new StringBuilder();
        StringBuilder sbf = new StringBuilder();

        string sql = " select a.*,b.StateName from ML_Order a join ML_OrderState b on a.OrderState=b.nID where a.nID=" + nid;
        DataTable dt = her.ExecuteDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            string orderTime = Convert.ToDateTime(dr["CreateTime"]).ToString("yyyy-MM-dd HH:mm");


            sbh.AppendLine("<tr>");
            sbh.AppendLine("<td><h4>订单编号：" + dr["OrderCode"].ToString() + "</h4></td>");
            sbh.AppendLine("<td>下单日期：" + orderTime + "</td>");
            sbh.AppendLine("<td>支付方式：" + dr["OcType"].ToString() + "</td>");
            sbh.AppendLine("<td>状态：" + dr["StateName"].ToString() + "</td>");
            sbh.AppendLine("</tr>");

            sbf.AppendLine("<tr>");
            sbf.AppendLine("<th>收货人姓名：</th>");
            sbf.AppendLine("<td>" + dr["Lianxiren"].ToString() + "</td>");
            sbf.AppendLine("<th>联系电话：</th>");
            sbf.AppendLine("<td>" + dr["PhoneNumber"].ToString() + "</td>");
            sbf.AppendLine("</tr>");

            if (Convert.ToInt32(dr["OrderState"].ToString()) > 2)//待收货才有
            {
                sbf.AppendLine("<tr>");
                sbf.AppendLine("<th>物流公司：</th>");
                sbf.AppendLine("<td>" + dr["worktime"].ToString() + "</td>");
                sbf.AppendLine("<th>物流单号：</th>");
                sbf.AppendLine("<td>" + dr["workComment"].ToString() + "</td>");
                sbf.AppendLine("</tr>");
            }
            if (Convert.ToInt32(dr["OrderState"].ToString()) == 3)//待收货
            {
                qrShouhuo.Visible = true;
                timeitem.Visible = true;
            }
            else
            {
                qrShouhuo.Visible = false;
                timeitem.Visible = false;
            }
            sbf.AppendLine("<tr>");
            sbf.AppendLine("<th valign='top'>配送地区：</th>");
            sbf.AppendLine("<td colspan='3' valign='top'>" + dr["OspecialStr"].ToString() + "</td>");
            sbf.AppendLine("</tr>");

            sbf.AppendLine("<tr>");
            sbf.AppendLine("<th valign='top'> 收货人地址：</th>");
            sbf.AppendLine("<td colspan='3' valign='top'>" + dr["Address"].ToString() + "</td>");
            sbf.AppendLine("</tr>");

            sbf.AppendLine("<tr>");
            sbf.AppendLine("<th valign='top'>订单附言：</th>");
            sbf.AppendLine("<td colspan='3' valign='top'>" + dr["tComment"].ToString() + "</td>");
            sbf.AppendLine("</tr>");


            AllPrice = dr["OrderPrice"].ToString();

        }
        orderheader = sbh.ToString();
        orderbody = sbf.ToString();
    }

    private void GetProduct(string nid)
    {
        StringBuilder sb = new StringBuilder();
        string sql = "select a.*,b.tPic,b.tTitle,b.price from ML_OrderProduct a join ML_ServiceArea b on a.proID=b.nID where a.orderID=" + nid;
        DataTable dt = her.ExecuteDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            sb.AppendLine("<tr>");
            sb.AppendLine("<td> <a href='product-117.aspx?id=" + dr["proID"].ToString() + "' target='_blank'>");
            sb.AppendLine("<img src='../Admin/upload_Img/Pruduct/" + dr["tPic"].ToString() + "' style='width: 50px; height: 50px;' /></a></td>");
            sb.AppendLine("<td> <a href='product-117.aspx?id=" + dr["proID"].ToString() + "' target='_blank'>" + dr["tTitle"].ToString() + "</a></td>");
            sb.AppendLine("<td>" + dr["price"].ToString() + "</td>");
            sb.AppendLine("<td>" + dr["probyCount"].ToString() + "</td>");
            sb.AppendLine("<td>" + dr["proPrice"].ToString() + ".00</td>");
            sb.AppendLine("</tr>");
        }

        Product = sb.ToString();
    }
}