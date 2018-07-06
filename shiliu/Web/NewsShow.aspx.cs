using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maliang;
using System.Text;

public partial class Web_NewsShow : System.Web.UI.Page
{
    public string newstitle;
    public string newsStr;
    private string nID
    {
        get
        {
            return ViewState["nID"] == null ? "1" : ViewState["nID"].ToString();
        }
        set
        {
            ViewState["nID"] = value;
        }
    }
    SqlHelper sh = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                nID = Request.QueryString["id"].ToString();
            }

            GetSource(nID);
        }
    }

    private void GetSource(string nid)
    {
        StringBuilder sb = new StringBuilder();
        string sql = "select * from ML_News where nID=" + nid;
        DataTable dt = sh.ExecuteDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            newstitle = dr["tTitle"].ToString();
            string time = Convert.ToDateTime(dr["dtAddTime"]).ToString("yyyy-MM-dd HH:mm");
            sb.AppendLine("<h2>" + newstitle + "<p>发布时间：" + time + "</p></h2>");
            sb.AppendLine("<p>" + dr["tMemo"].ToString() + "</p>");
        }
        newsStr = sb.ToString();
    }
}