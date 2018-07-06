using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Maliang;
using System.Data.SqlClient;


public partial class ERP2008_UserControl_Tabletext : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (Session["Adminquanxian"] == "kehu" || Session["Adminquanxian"] == "quyu" || Session["Adminquanxian"] == "kuaqu")
            //{
            //    LinkButton1.Visible = true;
            //}
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //Page.RegisterStartupScript("", "<script>window.document.form1.hid1.value='" + LinkButton1.ValidationGroup + "';window.document.form1.hid2.value='1';window.document.form1.submit();</script>");
    }
}
