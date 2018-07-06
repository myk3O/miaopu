using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Wap_Global : System.Web.UI.Page
{
    public string liststr;
    tongji tj = new tongji();
    private string uID
    {
        get
        {
            return ViewState["uID"].ToString();
        }
        set
        {
            ViewState["uID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["uid"] != null && Request.QueryString["uid"].ToString() != "")
        {
            uID = Request.QueryString["uid"].ToString();
            GetList();
        }
        else
        {
            Response.Redirect("errors.html");
        }
    }


    private void GetList()
    {
        StringBuilder sb = new StringBuilder();
        DataTable dt = tj.GetAvgMoneyListByUser(uID);
        foreach (DataRow dr in dt.Rows)
        {
            string money = StringDelHTML.PriceToStringLow(Convert.ToInt32(dr["AvgMoney"]));
            sb.AppendLine("<dd>" + money + "<span>" + dr["CreateTime"].ToString() + "</span></dd>");
        }
        if (string.IsNullOrEmpty(sb.ToString()))
        {
            sb.Append("<h1 style='text-align: center; margin-top: 20px'>这里什么也没留下 </h1>");
        }
        liststr = sb.ToString();
    }
}