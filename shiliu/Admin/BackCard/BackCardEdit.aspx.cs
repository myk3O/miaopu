using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_BackCard_BackCardEdit : System.Web.UI.Page
{
    Bank bk = new Bank();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["anid"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")
            {
                GridBind(Request.QueryString["id"].ToString());
            }

        }
    }

    //绑定GridView
    public void GridBind(string id)
    {
        DataTable dt = bk.GetBankOne(id);
        if (dt.Rows.Count > 0)
        {
            txtType.Text = dt.Rows[0]["TypeName"].ToString();
            txtzh.Text = dt.Rows[0]["Zhanghao"].ToString();
            txtName.Text = dt.Rows[0]["Huming"].ToString();
            txtbank.Text = dt.Rows[0]["Kaihu"].ToString();

            hid.Value = dt.Rows[0]["nID"].ToString();

        }
    }
    //初始化界面
    public void Initialization()
    {
        txtType.Text = "";
        txtzh.Text = "";
        txtName.Text = "";
        txtbank.Text = "";
    }


    protected void btnSub_Click(object sender, EventArgs e)
    {
        if (hid.Value == "")//nID
        {
            Addmak();
        }
        else
        {
            updatemak(hid.Value);
        }
    }
    public void Addmak()
    {
        bool success = bk.BankInsert(Session["anid"].ToString(), txtType.Text.Trim(), txtzh.Text.Trim(), txtName.Text.Trim(), txtbank.Text.Trim());
        if (success)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加完成')</script>");
            //Initialization();
            Response.Redirect("BackCardMain.aspx");
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加失败')</script>");
        }
    }
    public void updatemak(string id)
    {
        bool success = bk.BankUpdate(hid.Value, txtType.Text.Trim(), txtzh.Text.Trim(), txtName.Text.Trim(), txtbank.Text.Trim());
        if (success)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改完成')</script>");
            Response.Redirect("BackCardMain.aspx");
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改失败')</script>");

        }
    }
    protected void imgback_Click(object sender, EventArgs e)
    {
        Response.Redirect("BackCardMain.aspx");
    }


}