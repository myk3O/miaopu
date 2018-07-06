using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Maliang;

public partial class Web_member_receiver : System.Web.UI.Page
{
    public string addresslist;
    SqlHelper her = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["LoginUserId"] == null || Session["LoginUserId"] == "")
            {
                Response.Redirect("passport-login.aspx");
            }
            else
            {
                GetAddressList();
                //  LoginUserID.Value = Session["LoginUserId"].ToString();
            }
        }
    }


    private void GetAddressList()
    {
        StringBuilder sb = new StringBuilder();
        string sql = @"select * from ML_UserAddress where UserID='" + Session["LoginUserId"].ToString() + "' order by createtime desc";
        DataTable dt = her.ExecuteDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            string nID = dr["nID"].ToString();
            sb.AppendLine("<tr>");
            sb.AppendLine("<td>" + dr["PeopleName"].ToString() + "</td>");
            sb.AppendLine("<td class='textwrap' style='text-align: left;'>" + dr["YouBian"].ToString() + " " + dr["Address"].ToString() + "</td>");
            sb.AppendLine("<td>" + dr["Phone"].ToString() + "</td>");
            sb.AppendLine("<td><a href='member-editReceiver.aspx?id=" + dr["nID"].ToString() + "'>修改</a>&nbsp;&nbsp;<a href='' onclick='drop_addr_item(" + nID + ");'>删除</a></td>");
            sb.AppendLine("</tr>");

        }
        addresslist = sb.ToString();
    }

}