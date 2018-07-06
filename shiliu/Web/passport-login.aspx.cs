using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maliang;
using System.Data;
public partial class Web_passport_login : System.Web.UI.Page
{
    SqlHelper her = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string pwd = EntityUtils.StringToMD5(fpwd.Value.Trim(), 32);
        string sql = string.Format(@"select * from ML_Member where (MemberPhone='{0}' and MemberPass='{1}') or (MemberName='{0}' and MemberPass='{1}') ", fname.Value.Trim(), pwd);
        DataTable dt = her.ExecuteDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            Session.Add("LoginUserId", dt.Rows[0]["nID"].ToString());//简单登录
            Session.Add("LoginUserName", dt.Rows[0]["MemberPhone"].ToString());//简单登录
            Response.Redirect("member.aspx");
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('账号不存在！')</script>");
        }
    }
}