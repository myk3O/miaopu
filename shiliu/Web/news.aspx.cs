using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Maliang;

public partial class Web_news : System.Web.UI.Page
{
    public string activityStr;
    SqlHelper sh = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetSource();
        }
    }


    private void GetSource()
    {
        StringBuilder sb = new StringBuilder();
        string sql = "select * from ML_News order by oTop desc,dtAddTime desc";
        DataTable dt = sh.ExecuteDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            string time = Convert.ToDateTime(dr["dtAddTime"]).ToString("yyyy-MM-dd HH:mm");
            string tMemo = StringDelHTML.Centers(StringDelHTML.DelHTML(dr["tMemo"].ToString()), 40);
            //string title = dr["nID"].ToString();
            sb.AppendLine("<li class='list-item NewsList'><a href='NewsShow.aspx?id=" + dr["nID"].ToString() + "'>");
            sb.AppendLine("<img src='../Admin/upload_Img/News/" + dr["tPic"].ToString() + "' width='250' height='160' /></a>");
            sb.AppendLine("<h1><a href='NewsShow.aspx?id=" + dr["nID"].ToString() + "'>" + dr["tTitle"].ToString() + "</a></h1>");
            sb.AppendLine("<h2>" + time + "</h2>");
            sb.AppendLine("<h3>" + tMemo + "</h3>");
            sb.AppendLine("<div class='cls'>");
            sb.AppendLine("</div>");
            sb.AppendLine("</li>");
        }
        activityStr = sb.ToString();
    }
}