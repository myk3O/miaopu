using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maliang;
using System.Data;

public partial class Web_about : System.Web.UI.Page
{
    public string _title;
    public string dataSource;
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
        string sql = "select top 1 a.*,b.tClassName from ML_Info a join ML_InfoClass b on a.sid0=b.nID where a.sid0=" + nid;
        DataTable dt = sh.ExecuteDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            _title = dr["tClassName"].ToString();
            dataSource = dr["tMemo"].ToString();
        }
    }
}