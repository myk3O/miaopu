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

public partial class Wap_MyBranch : System.Web.UI.Page
{
    tongji tj = new tongji();
    StylistHelp sh = new StylistHelp();
    SqlHelper her = new SqlHelper();
    public string userstring;
    public string OneLeaveUser = "0";
    public string TwoLeaveUser = "0";
    public string ThreeLeaveUser = "0";

    //private double leave1 = Convert.ToDouble(ConfigurationManager.AppSettings["leave1"]);
    //private double leave2 = Convert.ToDouble(ConfigurationManager.AppSettings["leave2"]);
    //private double leave3 = Convert.ToDouble(ConfigurationManager.AppSettings["leave3"]);

    //public string leave1Pri = "0";
    //public string leave2Pri = "0";
    //public string leave3Pri = "0";


    public string class1 = "";
    public string class2 = "";
    public string class3 = "";

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
            getUser2(uID);
            getUser3(uID);
            //OneLeaveUser = sh.getOneLeaveUser(uID);
            // TwoLeaveUser = sh.getTwoLeaveUser(uID);
            //ThreeLeaveUser = sh.getThreeLeaveUser(uID);
        }
        else
        {
            Response.Redirect("errors.html");
        }
    }

    private void getUser1(string uid)
    {
        string sql = @"select a.nID,a.nickname,a.headimgurl,b.levelName from ML_Member a join ML_MemberLevel b 
                    on a.fxslevel=b.nID where a.FatherFXSID=" + uid + " and a.fxslevel>1";
        DataTable dt = her.ExecuteDataTable(sql);
        OneLeaveUser = dt.Rows.Count.ToString();
        class1 = getStr(dt);
    }
    private void getUser2(string uid)
    {
        string sql = string.Format(@"  select a.nID,a.nickname,a.headimgurl,b.levelName from ML_Member a 
                            join ML_MemberLevel b  on a.fxslevel=b.nID  join 
                          (select nID from ML_Member where FatherFXSID={0}) c
                          on a.FatherFXSID=c.nID where a.fxslevel>1", uid);
        DataTable dt = her.ExecuteDataTable(sql);
        TwoLeaveUser = dt.Rows.Count.ToString();
        class2 = getStr(dt);
    }


    private void getUser3(string uid)
    {
        string sql = string.Format(@"    select d.nID,d.nickname,d.headimgurl,e.levelName from  ML_Member d
                                 join ML_MemberLevel e  on d.fxslevel=e.nID join 
                              ( select a.nID from ML_Member a join 
                              (select nID from ML_Member where FatherFXSID={0}) b
                              on a.FatherFXSID=b.nID )c on d.FatherFXSID=c.nID where d.fxslevel>1", uid);
        DataTable dt = her.ExecuteDataTable(sql);
        ThreeLeaveUser = dt.Rows.Count.ToString();
        class3 = getStr(dt);
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
                pic = dr["headimgurl"].ToString()

            };
            dicPri.Add(id, ui);
        }
        dicPri = dicPri.OrderByDescending(d => d.Value.price).ToDictionary(d => d.Key, d => d.Value);
        int rows = 0;
        foreach (KeyValuePair<string, UserInfo> dic in dicPri)
        {
            rows++;
            if (rows > 20)
            {
                break;
            }
            string pri = StringDelHTML.DoublePriceToString(dic.Value.price);
            string img = dic.Value.pic == "" ? "img/Styl_01.png" : dic.Value.pic;
            sb.AppendLine("<dd><a><img src='" + img + "' />");
            sb.AppendLine("<h1>昵称：" + dic.Value.nickname + "</h1>");
            sb.AppendLine("<h2>" + dic.Value.levelname + "</h2>");
            sb.AppendLine("<h2>学分：￥" + pri + "</h2>");
            sb.AppendLine("<span>" + rows + "</span></a></dd>");
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
    }


}