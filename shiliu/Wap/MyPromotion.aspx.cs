using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maliang;
using System.Text;
using System.Data;
using System.Configuration;

public partial class Wap_MyPromotion : System.Web.UI.Page
{
    tongji tj = new tongji();
    StylistHelp sh = new StylistHelp();
    SqlHelper her = new SqlHelper();

    //private double leave1 = Convert.ToDouble(ConfigurationManager.AppSettings["leave1"]);
    //private double leave2 = Convert.ToDouble(ConfigurationManager.AppSettings["leave2"]);
    //private double leave3 = Convert.ToDouble(ConfigurationManager.AppSettings["leave3"]);

    //public string leave1Pri = "0";
    //public string leave2Pri = "0";
    //public string leave3Pri = "0";


    public string class1 = "";

    public string allMypri = "0";//累计佣金
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
            getUser1(uID);
        }
        else
        {
            Response.Redirect("errors.html");
        }


    }

    private void getUser1(string uid)
    {
        string sql = @"select a.nID,a.nickname,a.headimgurl,b.levelName,a.fxslevel,a.dtAddTime from ML_Member a join ML_MemberLevel b 
                    on a.fxslevel=b.nID where a.FatherFXSID=" + uid;
        DataTable dt = her.ExecuteDataTable(sql);
        class1 = getStr(dt);
    }

    private string getStr(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        Dictionary<string, UserInfo> dicPri = new Dictionary<string, UserInfo>();
        foreach (DataRow dr in dt.Rows)
        {
            var id = dr["nID"].ToString();
            //leave1Pri = sh.getOneLeavePrice(id);
            //leave2Pri = sh.getTwoLeavePrice(id);
            //leave3Pri = sh.getThreeLeavePrice(id);
            //double price1 = leave1 * (double.Parse(leave1Pri));
            //double price2 = leave2 * (double.Parse(leave2Pri));
            //double price3 = leave3 * (double.Parse(leave3Pri));
            double pri = tj.GetFenXiangYJ(id);
            allMypri = StringDelHTML.DoublePriceToString(pri);//累计佣金
            UserInfo ui = new UserInfo()
            {
                nID = dr["nID"].ToString(),
                nickname = dr["nickname"].ToString(),
                levelname = dr["levelName"].ToString(),
                price = pri,
                pic = dr["headimgurl"].ToString(),
                fxslevel = Convert.ToInt32(dr["fxslevel"]),
                dtime = Convert.ToDateTime(dr["dtAddTime"])
            };
            dicPri.Add(id, ui);
        }
        //dicPri = dicPri.OrderByDescending(d => d.Value.price).OrderByDescending(d => d.Value.fxslevel).ToDictionary(d => d.Key, d => d.Value);
        dicPri = dicPri.OrderByDescending(d => d.Value.dtime).ToDictionary(d => d.Key, d => d.Value);
        foreach (KeyValuePair<string, UserInfo> dic in dicPri)
        {
            string pri = StringDelHTML.DoublePriceToString(dic.Value.price);
            string img = dic.Value.pic == "" ? "img/Styl_01.png" : dic.Value.pic;
            sb.AppendLine("<dd><a><img src='" + img + "' />");
            sb.AppendLine("<h1>昵称：" + dic.Value.nickname + "</h1>");
            sb.AppendLine("<h2>" + dic.Value.levelname + "</h2>");
            sb.AppendLine("<h2>学分：￥" + pri + "</h2>");
            sb.AppendLine("<span>" + dic.Value.dtime.ToShortDateString() + "</span></a></dd>");
            sb.AppendLine("</a></dd>");
        }
        if (string.IsNullOrEmpty(sb.ToString()))
        {
            sb.Append("<h1 style='text-align: center; margin-top: 20px'>这里什么也没留下 </h1>");
        }
        return sb.ToString();
    }


    public class UserInfo
    {
        public string nID;
        public string nickname;
        public string levelname;
        public double price;
        public string pic;
        public int fxslevel;
        public DateTime dtime;
    }

}