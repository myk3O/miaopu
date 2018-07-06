using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maliang;
using System.Text;
using System.Data;

public partial class Wap_MyGold : System.Web.UI.Page
{
    SqlHelper her = new SqlHelper();
    public string fromstr;
    public string tostr;
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
            int userid = Convert.ToInt32(uID);
            string sql = string.Format(@"select a.*,b.nickname fromname,b.MemberCode fromcode,c.MemberCode tocode,c.nickname toname from ML_ZhuanBi a 
                                      join ML_Member b on a.fromUser=b.nID 
                                      join ML_Member c on a.toUser=c.nID
                                      where a.fromUser={0} or a.toUser={0} order by a.CreateTime  desc", userid);
            DataTable dt = her.ExecuteDataTable(sql);
            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string money = StringDelHTML.PriceToStringLow(Convert.ToInt32(dr["zrMoney"]));

                    if (dr["fromUser"].ToString().Equals(uID))//转出
                    {
                        string nickname = StringDelHTML.Centers(dr["toname"].ToString(), 5);
                        sb.AppendLine("<dd>");
                        sb.AppendLine("<h1>转让编号：" + dr["zrCode"].ToString() + "<span>转出</span></h1>");
                        sb.AppendLine("<h3>转给好友：" + nickname + " <span>共学ID：" + dr["tocode"].ToString() + "</span></h3>");
                        sb.AppendLine("<h4>转出金额：<span>¥ " + money + "</span></h4>");
                        sb.AppendLine("<h5>转出时间：<span>" + dr["CreateTime"].ToString() + "</span></h5>");
                        sb.AppendLine("<h5>备注：<span>" + dr["tMemo"].ToString() + "</span></h5>");
                        sb.AppendLine("<h2>转让状态：<span>已完成</span></h2>");
                        sb.AppendLine("</dd>");

                    }
                    else
                    {
                        string nickname = StringDelHTML.Centers(dr["fromname"].ToString(), 5);
                        sb.AppendLine("<dd>");
                        sb.AppendLine("<h1>转让编号：" + dr["zrCode"].ToString() + "<span class='cur'>转入</span></h1>");
                        sb.AppendLine("<h3>转入人：" + nickname + " <span>共学ID：" + dr["fromcode"].ToString() + "</span></h3>");
                        sb.AppendLine("<h4>转入金额：<span class='cur'>¥ " + money + "</span></h4>");
                        sb.AppendLine("<h5>转入时间：<span>" + dr["CreateTime"].ToString() + "</span></h5>");
                        sb.AppendLine("<h5>备注：<span>" + dr["tMemo"].ToString() + "</span></h5>");
                        sb.AppendLine("<h2>转让状态：<span class='cur'>已完成</span></h2>");
                        sb.AppendLine("</dd>");
                    }


                }

            }
            else
            {
                sb.Append("<dd><h1 style='text-align: center'>这里什么也没留下 </h1></dd>");
            }
            fromstr = sb.ToString();

        }
        else
        {
            Response.Redirect("errors.html");
        }

    }
}