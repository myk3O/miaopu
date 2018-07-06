using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Maliang;
public partial class Web_member_orders : System.Web.UI.Page
{
    public string orderCount;
    public string orderList;
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
                // GetOrder();
                GetOrderPay();
            }
        }

    }
    private void GetOrder()
    {
        string sql = string.Format(@"select * from ML_Order where OrderUserid={0} and  OrderState!={1} order by CreateTime desc", UserID, 1);
        DataTable dt = her.ExecuteDataTable(sql);
        orderCount = dt.Rows.Count.ToString();
    }
    private void GetOrderPay()
    {
        string stateStr = string.Empty;
        StringBuilder sb = new StringBuilder();
        string sql = string.Format(@"select * from ML_Order where OrderUserid={0} order by CreateTime desc", UserID);
        DataTable dt = her.ExecuteDataTable(sql);

        foreach (DataRow dr in dt.Rows)
        {

            string sqlPname = "select top 1 b.tTitle from ML_OrderProduct a join ML_ServiceArea b on a.proID=b.nID where a.orderID=" + dr["nID"].ToString();
            var proName = her.ExecuteScalar(sqlPname) == null ? "" : her.ExecuteScalar(sqlPname).ToString();
            string time = Convert.ToDateTime(dr["CreateTime"]).ToString("yyyy-MM-dd HH:mm");
            string nID = dr["nID"].ToString();
            switch (Convert.ToInt32(dr["OrderState"].ToString()))
            {
                case 1: stateStr = "<td><span class='point'><a href='order-create.aspx?nID=" + nID + "'>前往付款</a> </span></td>"; break;
                default: stateStr = "<td><span class='point'><a href='order-detail.aspx?id=" + nID + "'>查看详情</a> </span></td>"; break;
            }

            sb.AppendLine("<td width='40%'>");
            sb.AppendLine("<a class='intro' href='order-detail.aspx?id=" + nID + "'>" + proName + "</a></td>");
            sb.AppendLine("<td><a href='order-detail.aspx?id=" + nID + "'>" + dr["OrderCode"].ToString() + "</a></td>");
            sb.AppendLine("<td>" + time + "</td>");
            sb.AppendLine("<td>￥" + dr["OrderPrice"].ToString() + ".00</td>");
            sb.AppendLine(stateStr);
            sb.AppendLine("</tr>");
        }
        orderList = sb.ToString();
    }
}