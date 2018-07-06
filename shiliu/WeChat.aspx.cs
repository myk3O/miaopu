using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiPay;

public partial class WeChat : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //ServerAuth.Auth();
        ServerAuth sa = new ServerAuth();
        sa.recMessage();
    }
}