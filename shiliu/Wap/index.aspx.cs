using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maliang;
using System.Text;
using System.Data;


public partial class Wap_index : System.Web.UI.Page
{
    SqlHelper her = new SqlHelper();
    public string banner;
    public string HotStr;
    public string RiHanStr;
    public string TuiJianStr;
    public string pro1;
    public string pro2;
    public string pro3;
    public string videoType;
    public string freevideo;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetBanner();
            pro3 = GetProduct();
            freevideo = getFreeVideo();
        }
    }

    private void GetBanner()
    {
        StringBuilder sb = new StringBuilder();
        string sql = "select top 6 * from ML_Image order by nPaiXu ";

        DataTable dt = her.ExecuteDataTable(sql);

        foreach (DataRow dr in dt.Rows)
        {
            sb.AppendLine("<li>");
            sb.AppendLine("<a href='VideoShow.aspx?pid=" + dr["tMemo"].ToString() + "'>");
            sb.AppendLine("<img src='../Admin/upload_Img/Logo_Img/" + dr["imgUrl"].ToString() + "' /></a></li>");
        }
        banner = sb.ToString();
    }



    private string GetProduct()
    {
        StringBuilder sb = new StringBuilder();
        string sql = @"select  * from ML_VideoComment where oHide=1 and oFree=0 order by addTime desc ";//where oHide=1

        DataTable dt = her.ExecuteDataTable(sql);

        foreach (DataRow dr in dt.Rows)
        {
            string vname = StringDelHTML.Centers(dr["vName"].ToString(), 6);
            string price = StringDelHTML.PriceToStringLow(Convert.ToInt32(dr["Price"]));
            sb.AppendLine("<dd><a href='VideoShow.aspx?pid=" + dr["nID"].ToString() + "'>");
            sb.AppendLine("<img src='../upload_Img/VideoImg/" + dr["vPic"].ToString() + "' />");
            sb.AppendLine("<h1>¥ " + price + " </h1>");
            sb.AppendLine("<p>" + vname + "</p></a></dd>");
        }
        return sb.ToString();

    }

    private string getFreeVideo()
    {
        StringBuilder sb = new StringBuilder();
        string sql = @"select  * from ML_VideoComment where oHide=1 and oFree=1 order by addTime desc ";//where oHide=1

        DataTable dt = her.ExecuteDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string vname = StringDelHTML.Centers(dr["vName"].ToString(), 6);
                string price = StringDelHTML.PriceToStringLow(Convert.ToInt32(dr["Price"]));
                sb.AppendLine("<dd><a href='VideoShow.aspx?pid=" + dr["nID"].ToString() + "'>");
                sb.AppendLine("<img src='../upload_Img/VideoImg/" + dr["vPic"].ToString() + "' />");
                //sb.AppendLine("<h1>¥ " + price + " </h1>");
                sb.AppendLine("<p>" + vname + "</p></a></dd>");
            }
        }
        else
        {
            sb.Append("<h1 style='text-align: center'>敬请期待！ </h1>");
        }
        return sb.ToString();
    }
}