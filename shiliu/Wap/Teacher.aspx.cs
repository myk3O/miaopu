using Maliang;
using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;

public partial class Wap_Teacher : System.Web.UI.Page
{
    NewsHelper nh = new NewsHelper();
    SqlHelper her = new SqlHelper();
    public string teacherMemo;
    protected void Page_Load(object sender, EventArgs e)
    {

        string sql = "  select top 1 nID, tMemo from [ML_News] where oTop=1 order by dtAddTime desc";
        DataTable dt = her.ExecuteDataTable(sql);

        if (dt.Rows.Count > 0)
        {
            teacherMemo = dt.Rows[0]["tMemo"].ToString();
        }
        else
        {
            teacherMemo = "<h1 style='text-align: center; margin-top: 20px'>这里什么也没留下 </h1>";
        }
        if (Request.QueryString["uid"] != null && Request.QueryString["uid"].ToString() != "")
        {
            Read(Request.QueryString["uid"].ToString(), dt.Rows[0]["nID"].ToString());
        }
    }

    private void Read(string uid, string newid)
    {
        if (!nh.IsNewsRead(uid))
        {
            string sql = string.Format(@"insert into ML_NewsRead(userId,newsId,CeeateTime) 
                        values({0},{1},'{2}')", uid, newid, System.DateTime.Now.ToString());
            her.ExecuteNonQuery(sql);
        }
    }
}