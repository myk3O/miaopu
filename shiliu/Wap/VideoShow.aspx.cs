using Maliang;
using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using WeiPay;


public partial class Wap_VideoShow : System.Web.UI.Page
{
    public string link = "http://tv.gongxue168.com/Wap/Index.aspx?agent=";
    public string imgUrl = "http://tv.gongxue168.com/upload_Img/TeacherImg/";
    SqlHelper her = new SqlHelper();
    public string Url;
    public string price = "0.00";
    public string tmemo;
    public string didcount;
    public string TypeName;
    public string nearVideo;
    public string title;
    public string vpic;

    public string timg;
    public string Vdiscrib;
    public string teacherMemo;
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
            if (Request.QueryString["pid"] != null && Request.QueryString["pid"].ToString() != "")
            {
                pID = Request.QueryString["pid"].ToString();
                GetVideo();
                GetClassVideo();
                GetBOVideo();
                link = "http://tv.gongxue168.com/Wap/VideoShow.aspx?pid=" + pID + "&agent=";
                imgUrl = "http://tv.gongxue168.com/upload_Img/TeacherImg/" + timg;
            }
            else
            {
                Response.Redirect("errors.html");
            }

        }
    }

    private void Read(string cid)
    {
        string sql = "update ML_Video set VideoCode=VideoCode+1 where nID=" + cid;
        her.ExecuteNonQuery(sql);
    }




    private void GetVideo()
    {
        StringBuilder sb = new StringBuilder();

        string sql = "select * from ML_VideoComment a left join T_Teacher b on a.teacherID=b.nID where a.nID=" + pID;
        DataTable dt = her.ExecuteDataTable(sql);

        if (dt.Rows.Count > 0)
        {
            price = dt.Rows[0]["Price"] == null ? "0.01" : dt.Rows[0]["Price"].ToString() == "" ? "0.01" : StringDelHTML.PriceToStringLow(Convert.ToInt32(dt.Rows[0]["Price"]));
            price = "全套<b>" + price + "</b>";
            if (dt.Rows[0]["oFree"].ToString() == "True")
            {
                string sqlteacher = "select top 1 nID from ML_VideoComment where teacherID=" + dt.Rows[0]["teacherID"] + " and  oFree=0 ";

                price = "<a href='VideoShow.aspx?pid=" + her.ExecuteScalar(sqlteacher) + "'><b>查看老师更多视频</b></a>";
            }
            tmemo = dt.Rows[0]["vMemo"].ToString();
            title = dt.Rows[0]["vName"].ToString() == "" ? "共学视频" : dt.Rows[0]["vName"].ToString();
            Vdiscrib = dt.Rows[0]["vdiscrib"].ToString();
            timg = dt.Rows[0]["teacherImg"].ToString();
            vpic = dt.Rows[0]["vPic"].ToString();

            if (!string.IsNullOrEmpty(dt.Rows[0]["teacherImg"].ToString()))
            {
                sb.AppendLine("<img src='../upload_Img/TeacherImg/" + dt.Rows[0]["teacherImg"].ToString() + "' />");
            }

            sb.AppendLine("<h1><span>" + dt.Rows[0]["teacherName"].ToString() + "</span>" + dt.Rows[0]["teacherdiscrib"].ToString() + "</h1>");
            //var tm = StringDelHTML.Centers(StringDelHTML.DelHTML(dt.Rows[0]["teacherMemo"].ToString()), 20);
            var tm = dt.Rows[0]["teacherMemo"].ToString();
            //sb.AppendLine("<h2>" + tm + "<a href='Teacher.aspx?pid=" + pID + "'>详情 ></a></h2>");
            sb.AppendLine("<h2>" + tm + "</h2>");

            teacherMemo = sb.ToString();

        }
        else
        {
            //未找到相关视频
        }

    }
    private void GetBOVideo()
    {
        string sql = string.Empty;
        if (Request.QueryString["cid"] != null && Request.QueryString["cid"].ToString() != "")
        {
            sql = "select  * from  ML_Video where nID=" + Request.QueryString["cid"].ToString();
        }
        else
        {
            sql = "select  * from  ML_Video where sid0='" + pID + "' order by oTop ";
        }
        DataTable dt = her.ExecuteDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            didcount = dt.Rows[0]["VideoCode"].ToString();
            //Url = "../upload_Img/" + dt.Rows[0]["vUrl"].ToString();
            Url = dt.Rows[0]["tVideo"].ToString();
            TypeName = dt.Rows[0]["VideoName"].ToString();
            Read(dt.Rows[0]["nID"].ToString());
        }
    }



    private void GetClassVideo()
    {
        StringBuilder sb = new StringBuilder();
        string sql = "select  * from  ML_Video where sid0='" + pID + "' order by oTop ";
        DataTable dt = her.ExecuteDataTable(sql);
        sb.AppendLine(" <dt>" + title + "（共" + dt.Rows.Count + "段视频）</dt>");
        foreach (DataRow dr in dt.Rows)
        {
            var ctPic = dr["tPic"].ToString();//每部视频独立图片，图片在upload_Img/Video/文件目录下
            string vname = StringDelHTML.Centers(dr["VideoName"].ToString(), 12);
            //sb.AppendLine("<dd><a style='color:#f9b025;text-decoration:underline' href='VideoShow.aspx?pid=" + pID + "&cid=" + dr["nID"].ToString() + "'>");
            sb.AppendLine("<dd><a href='VideoShow.aspx?pid=" + pID + "&cid=" + dr["nID"].ToString() + "'>");
            sb.AppendLine("<img src='../upload_Img/VideoImg/" + vpic + "' />" + vname + "</a></dd>");
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