using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Web_member_addReceiver : System.Web.UI.Page
{
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
                LoginUserID.Value = Session["LoginUserId"].ToString();
            }
        }
    }
}