using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Main_Main : System.Web.UI.Page
{
    public string url;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["anid"] != null && Session["anid"] != "")
        {

            if (Session["anid"].ToString() == "1")
            {
                url = "<frame src='Right.aspx' name='rightFrame' id='rightFrame' title='rightFrame' />";
            }
            else
            {
                url = "<frame src='../DaiLi/HomeMakByOne.aspx' name='rightFrame' id='rightFrame' title='rightFrame' />";
            }
        }
    }
}