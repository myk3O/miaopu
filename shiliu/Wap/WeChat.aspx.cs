using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiPay;

public partial class Wap_WeChat : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ServerAuth sa = new ServerAuth();
        sa.recMessage();

        //ServerAuth.Auth();
    }
}