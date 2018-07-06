using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maliang;
using System.Text;
using System.Data;

public partial class Wap_MemberOne : System.Web.UI.Page
{
    SqlHelper her = new SqlHelper();
    public string userstring;

    public string title;
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


    private string iD
    {
        get
        {
            return ViewState["iD"].ToString();
        }
        set
        {
            ViewState["iD"] = value;
        }
    }

    private string cT
    {
        get
        {
            return ViewState["cT"].ToString();
        }
        set
        {
            ViewState["cT"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
        {
            iD = Request.QueryString["id"].ToString();
        }

        if (Request.QueryString["ct"] != null && Request.QueryString["ct"].ToString() != "")
        {
            cT = Request.QueryString["ct"].ToString();
        }

        if (Request.QueryString["uid"] != null && Request.QueryString["uid"].ToString() != "")
        {
            uID = Request.QueryString["uid"].ToString();
            getUser(uID);
        }
        else
        {
            Response.Redirect("errors.html");
        }

    }


    private void getUser(string uid)
    {
        StringBuilder sb = new StringBuilder();
        string sql = string.Empty;
        switch (iD)
        {
            case "1": sql = "select * from ML_Member where FatherFXSID=" + uid; title = "一级会员" + cT; ; break;
            case "2": sql = string.Format(@"  select a.* from ML_Member a join 
                          (select nID from ML_Member where FatherFXSID={0}) b
                          on a.FatherFXSID=b.nID", uid); title = "二级会员" + cT; break;
            case "3": sql = string.Format(@"    select d.* from  ML_Member d  join 
                              ( select a.nID from ML_Member a join 
                              (select nID from ML_Member where FatherFXSID={0}) b
                              on a.FatherFXSID=b.nID )c on d.FatherFXSID=c.nID", uid); title = "三级会员" + cT; break;
            default: break;
        }

        DataTable dt = her.ExecuteDataTable(sql);

        foreach (DataRow dr in dt.Rows)
        {
            string img = dr["headimgurl"].ToString() == "" ? "img/Styl_01.png" : dr["headimgurl"].ToString();
            string nickname = dr["nickname"].ToString() == "" ? "共学用户" : dr["nickname"].ToString();
            sb.AppendLine("<dd><a href='#'>");
            sb.AppendLine(" <img src='" + img + "'/>");
            sb.AppendLine(" <h1>昵称：" + nickname + "</h1>");
            sb.AppendLine(" <h2>关注时间：" + Convert.ToDateTime(dr["fxsTimeBegin"]).ToString("yyyy-MM-dd") + "</h2>");
            sb.AppendLine("</a></dd>");
        }
        userstring = sb.ToString();
    }
}