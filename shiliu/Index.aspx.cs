using System;
using System.Threading;
using WeiPay;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.Sleep(5);
        Response.Write("<Script>parent.location.href ='Wap/Index.aspx'</Script>");
        //Response.Write("<Script>parent.location.href ='Wap/WeChat.aspx'</Script>");
      
    }

}