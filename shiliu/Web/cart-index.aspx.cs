using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Maliang;
public partial class Web_cart_index : System.Web.UI.Page
{
    public string cartStr;
    public string Price;
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
                LoginUserID.Value = UserID;
                GetCart();
            }
        }
    }


    private void GetCart()
    {
        int price = 0;
        StringBuilder sb = new StringBuilder();
        string sql = "select a.*,b.tPic,b.tTitle,b.price,b.kucun from ML_Cart a join ML_ServiceArea b on a.ProID=b.nID where a.UserID=" + UserID + " order by createtime desc";

        DataTable dt = her.ExecuteDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            cartnonemsg.Visible = false;
            foreach (DataRow dr in dt.Rows)
            {
                string nID = dr["nID"].ToString();
                int allPrice = Convert.ToInt32(dr["ProCount"]) * Convert.ToInt32(dr["price"]);
                price += allPrice;
                sb.AppendLine("<tr>");
                sb.AppendLine(" <td><div class='cart-product-img' style='width: 50px; height: 50px;'>");
                sb.AppendLine("<a href='product-117.aspx?id=" + dr["ProID"].ToString() + "' target='_blank'><img style='width: 50px; height: 50px;' src='../Admin/upload_Img/Pruduct/" + dr["tPic"].ToString() + "' /></a></div></td>");
                sb.AppendLine("<td style='text-align: left'>");
                sb.AppendLine(" <a href='product-117.aspx?id=" + dr["ProID"].ToString() + "' target='_blank'>" + dr["tTitle"].ToString() + "</a> </td>");
                sb.AppendLine(" <td class='mktprice1' style='width: 80px; '> ￥" + dr["price"].ToString() + ".00 </td>");
                sb.AppendLine(" <td style='width:80px; '>￥<span class='price1' id='price" + nID + "'>" + dr["price"].ToString() + ".00</span></td>");
                sb.AppendLine(" <td>");
                sb.AppendLine("<div class='Numinput'>");
                sb.AppendLine(" <input type='text' class='_x_ipt textcenter'id='input_item_" + nID + "' value='" + dr["ProCount"].ToString() + "' orig='1' changed='" + dr["kucun"].ToString() + "' onkeyup='change_quantity(" + nID + ", this,1326);'  />");
                sb.AppendLine("<span class='numadjust increase' onclick='add_quantity(" + nID + ");'></span>");
                sb.AppendLine("<span class='numadjust decrease' onclick='decrease_quantity(" + nID + ");'></span></div> </td>");
                sb.AppendLine(" <td class='itemTotal fontcolorRed' style='width:80px; '><span class='price2' id='item" + nID + "_subtotal'>￥" + allPrice.ToString() + ".00</span></td>");
                sb.AppendLine(" <td><span class='lnk quiet fontcolorRed' onclick='drop_cart_item(" + nID + ",this,1326);'>");
                sb.AppendLine("<img src='statics/transparent.gif' alt='删除' style='width: 13px; height: 13px; background-image: url(statics/bundle.gif); ");
                sb.AppendLine(" background-repeat: no-repeat; background-position: 0 -27px;' /></span>");
                sb.AppendLine(" </td>");
                sb.AppendLine(" </tr>");

            }
        }
        else
        {
            cartitems.Visible = false;
            cartnonemsg.Visible = true;
        }
        cartStr = sb.ToString();
        Price = price.ToString();
    }

}