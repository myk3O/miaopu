using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class Wap_MyPresent : System.Web.UI.Page
{
    public string uid;
    tongji tj = new tongji();
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
            uid = nID;
            DataTable dt = tj.GetMoneyOrderList(nID);

            foreach (DataRow dr in dt.Rows)
            {
                string pri = StringDelHTML.PriceToStringLow(Convert.ToInt32(dr["AllPrice"]));
                if (dr["OrderState"].ToString().ToLower().Equals("true"))
                {
                    sb.AppendLine("<dd>完成（" + pri + "）<span>" + dr["UpdateTime"].ToString() + "</span></dd>");

                }
                else
                {
                    sb.AppendLine("<dd>处理中（" + pri + "）<span>" + dr["CreateTime"].ToString() + "</span></dd>");
                }


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