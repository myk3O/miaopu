using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Maliang;


public partial class Wap_Shopping : System.Web.UI.Page
{
    SqlHelper her = new SqlHelper();
    public string liststr;
    private string nID
    {
        get
        {
            return ViewState["nID"].ToString();
        }
        set
        {
            ViewState["nID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["uid"] != null && Request.QueryString["uid"].ToString() != "")
        {
            StringBuilder sb = new StringBuilder();
            nID = Request.QueryString["uid"].ToString();
            //DataTable dt = tj.GetFenXiangJL(nID);


            string sql = @"select a.OcType,a.CreateTime,a.OrderPrice,b.vName from ML_Order a
                           left join ML_VideoComment b on a.OcID=b.nID where a.OrderUserid=" + nID + " and a.OcType='学习币支付' order by a.CreateTime desc ";
            DataTable dt = her.ExecuteDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                string pri = StringDelHTML.PriceToStringLow(Convert.ToInt32(dr["OrderPrice"].ToString()));

                sb.AppendLine("<dd>" + dr["vName"].ToString() + "（" + pri + "）<span>" + dr["CreateTime"].ToString() + "</span></dd>");
            }
            if (string.IsNullOrEmpty(sb.ToString()))
            {
                sb.Append("<h1 style='text-align: center; margin-top: 20px'>这里什么也没留下 </h1>");
            }
            liststr = sb.ToString();
        }
        else
        {
            Response.Redirect("errors.html");
        }

    }

}