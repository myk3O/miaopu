using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtname.Value = "";
            txtpass.Value = "";
            Session.Contents.Clear();//清空session
            //获取cookies的值
            HttpCookie cookie = Request.Cookies["AdminName"];
            if (cookie != null) { txtname.Value = cookie.Value; }
            HttpCookie cookie1 = Request.Cookies["AdminPass"];
            if (cookie1 != null) { txtpass.Attributes.Add("value", cookie1.Value); }
            HttpCookie cookie2 = Request.Cookies["RememberMe"];
            if (cookie2 != null)
            {
                if (cookie2 != null && cookie2.Value == "1")
                {
                    ckb.Checked = true;
                }
            }
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        LoginVerification logVer = new LoginVerification();
        bool sesscue = logVer.Login(txtname.Value.Trim(), EntityUtils.StringToMD5(txtpass.Value.Trim(), 16));
        if (!sesscue)
        {
            lbltxt.Text = "温馨提示：您的用户名或密码错误!";
            //ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('用户名或密码错误！')</script>");
            return;
        }
        else
        {
            DataTable dt = logVer.loginmanige(txtname.Value.Trim(), EntityUtils.StringToMD5(txtpass.Value.Trim(), 16));
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["oCheck"].ToString() == "False")
                {
                    lbltxt.Text = "温馨提示：该用户未通过审核!";
                    //ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('用户未通过审核！')</script>");
                    return;
                }
                DataTable dtt = logVer.GetnIDcID();

                Session.Add("AdminName", txtname.Value.Trim());
                Session.Add("aid", dt.Rows[0]["nID"].ToString());
                Session.Add("anid", dtt.Rows[0]["nID"].ToString());//nID
                Session.Add("acid", dtt.Rows[0]["cid"].ToString());//cid
                Session.Add("LastTime", dt.Rows[0]["dtLastTime"].ToString());
                Session.Timeout = 600;
                logVer.updatelogin(txtname.Value.Trim(), EntityUtils.StringToMD5(txtpass.Value.Trim(), 16));
                if (ckb.Checked)
                {
                    //将界面的值写入cookies
                    HttpCookie cookie = new HttpCookie("AdminName");
                    cookie.Value = txtname.Value.Trim();
                    cookie.Expires = DateTime.Now.AddDays(14);
                    HttpCookie cookie1 = new HttpCookie("AdminPass");
                    cookie1.Value = txtpass.Value.Trim();
                    cookie1.Expires = DateTime.Now.AddDays(14);
                    HttpCookie cookie2 = new HttpCookie("RememberMe");
                    cookie2.Value = "1";
                    cookie2.Expires = DateTime.Now.AddDays(14);
                    Response.Cookies.Add(cookie);
                    Response.Cookies.Add(cookie1);
                    Response.Cookies.Add(cookie2);

                }
                else
                {
                    //将空值写入cookies
                    HttpCookie cookie = new HttpCookie("AdminName");
                    cookie.Value = "";
                    cookie.Expires = DateTime.Now.AddDays(14);
                    HttpCookie cookie1 = new HttpCookie("AdminPass");
                    cookie1.Value = "";
                    cookie1.Expires = DateTime.Now.AddDays(14);
                    HttpCookie cookie2 = new HttpCookie("RememberMe");
                    cookie2.Value = "";
                    cookie2.Expires = DateTime.Now.AddDays(14);
                    Response.Cookies.Add(cookie);
                    Response.Cookies.Add(cookie1);
                    Response.Cookies.Add(cookie2);
                }
                Response.Redirect("Main/Main.aspx");
            }
            else
            {
                Session.Contents.Clear();
            }

        }
    }
}