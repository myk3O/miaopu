using Maliang;
using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using WeiPay;

public partial class Wap_ProductShow : System.Web.UI.Page
{
    SqlHelper her = new SqlHelper();
    public string Url;
    public string price = "5.00";
    public string tmemo;
    public string didcount;

    public string nearVideo;
    public string classid;
    public string title;

    private string pID
    {
        get
        {
            return ViewState["pID"].ToString();
        }
        set
        {
            ViewState["pID"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["pid"] != null && Request.QueryString["pid"] != "")
            {
                pID = Request.QueryString["pid"].ToString();
                GetVideo();
                GetNearVideo();
                Read();
            }
            else
            {
                Response.Redirect("errors.html");
            }
        }
    }

    private void Read()
    {
        string sql = "update ML_Video set VideoCode=VideoCode+1 where nID=" + pID;
        her.ExecuteNonQuery(sql);
    }




    private void GetVideo()
    {
        StringBuilder sb = new StringBuilder();

        string sql = "select * from ML_Video where nID=" + pID;
        DataTable dt = her.ExecuteDataTable(sql);

        if (dt.Rows.Count > 0)
        {
            price = dt.Rows[0]["Price"] == null ? "0.01" : dt.Rows[0]["Price"].ToString() == "" ? "0.01" : StringDelHTML.PriceToStringLow(Convert.ToInt32(dt.Rows[0]["Price"]));
            sb.AppendLine("<dd>" + dt.Rows[0]["VideoName"].ToString() + "[" + price + "元]<span>");
            sb.AppendLine("<img src='Img/sub_03.jpg' onclick=\"document.getElementById('mcover').style.display = 'block'; \" /></span></dd>");
            sb.AppendLine("<dt>" + dt.Rows[0]["tMemo"].ToString() + "</dt>");
            tmemo = sb.ToString();
            didcount = dt.Rows[0]["VideoCode"].ToString();
            classid = dt.Rows[0]["sid0"].ToString();
            title = dt.Rows[0]["VideoName"].ToString() == "" ? "共学视频" : dt.Rows[0]["VideoName"].ToString();
            Url = "../upload_Img/" + dt.Rows[0]["tVideo"].ToString();
            //Url = "http://v.chinesecom.cn/1.mp4";
        }
        else
        {
            //未找到相关视频
        }

    }


    private void GetNearVideo()
    {
        StringBuilder sb = new StringBuilder();
        string sql = "select top 3 * from  ML_Video where sid0='" + classid + "' order by dtAddTime desc ";
        DataTable dt = her.ExecuteDataTable(sql);

        foreach (DataRow dr in dt.Rows)
        {
            string tmemo = StringDelHTML.Centers(StringDelHTML.DelHTML(dr["tmemo"].ToString()), 50);
            sb.AppendLine("<dd><a href='ProductShow.aspx?pid=" + dr["nID"].ToString() + "'>");
            sb.AppendLine("<img src='../upload_Img/VideoImg/" + dr["tPic"].ToString() + "' />");
            sb.AppendLine("<h1>" + dr["VideoName"].ToString() + "</h1>");
            sb.AppendLine("<h3>" + tmemo + "</h3>");
            sb.AppendLine("<h4>" + dr["VideoCode"].ToString() + "次播放</h4>");
            sb.AppendLine("</a></dd>");
        }
        nearVideo = sb.ToString();
    }



    public string Get_signature()
    { 
        //加密/校验流程：  
        //string noncestr = "noncestr=Wm3WZYTPz0wzccnW";
        //string timestamp = "timestamp=1414587457";
        //string jsapi_ticket = "jsapi_ticket=sM4AOVdWfPE4DxkXGEs8VMCPGGVi4C3VM0P37wVUCFvkVAy_90u5h9nbSlYy3-Sl-HhTdfl2fzFy1AOcHKP7qg";
        string noncestr = "noncestr=" + TenpayUtil.getNoncestr();
        string timestamp = "timestamp=" + TenpayUtil.getTimestamp();
        string jsapi_ticket = "";
        if (HttpRuntime.Cache["jsapi_ticket"] == null)
        {
            //重新获取jsapi_ticket
        }
        else
        {
            jsapi_ticket = "jsapi_ticket=" + HttpRuntime.Cache["jsapi_ticket"].ToString();
        }
        //string url = "url=http://mp.weixin.qq.com?params=value";
        string url = "url=" + Request.Url.ToString();
        //1. 对所有待签名参数按照字段名的ASCII 码从小到大排序（字典序）后，
        //使用URL键值对的格式（即key1=value1&key2=value2…）拼接成字符串string1 
        string[] ArrTmp = { noncestr, timestamp, jsapi_ticket, url };
        Array.Sort(ArrTmp);//字典排序  
        //2.将三个参数字符串拼接成一个字符串进行sha1加密  
        string tmpStr = string.Join("&", ArrTmp);
        tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
        tmpStr = tmpStr.ToLower();
        return tmpStr;

    }


}