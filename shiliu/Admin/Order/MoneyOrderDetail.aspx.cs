using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maliang;
using System.Text;

public partial class Admin_Order_MoneyOrderDetail : System.Web.UI.Page
{
    public string allprice;
    public string bankStr;
    private int oprice
    {
        get
        {
            return ViewState["oprice"] == null ? 0 : Convert.ToInt32(ViewState["oprice"]);
        }
        set
        {
            ViewState["oprice"] = value;
        }
    }
    private string anID
    {
        get
        {
            return ViewState["anID"] == null ? "" : ViewState["anID"].ToString();
        }
        set
        {
            ViewState["anID"] = value;
        }
    }
    private string nID
    {
        get
        {
            return ViewState["nID"] == null ? "" : ViewState["nID"].ToString();
        }
        set
        {
            ViewState["nID"] = value;
        }
    }
    private string Price
    {
        get
        {
            return ViewState["Price"] == null ? "" : ViewState["Price"].ToString();
        }
        set
        {
            ViewState["Price"] = value;
        }
    }
    SqlHelper her = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["anid"] == null) { Response.Redirect("../../Error.aspx"); }

        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                nID = Request.QueryString["id"].ToString();

                string sql = "select * from MoneyOrder where nID=" + nID;
                DataTable dtt = her.ExecuteDataTable(sql);
                foreach (DataRow dr in dtt.Rows)
                {
                    anID = dr["AnID"].ToString();
                    if (dr["OrderState"].ToString() == "False")
                    {
                        Button1.Visible = true;
                    }
                    else
                    {
                        Button1.Visible = false;

                    }


                }
            }
            else
            {
                Response.Redirect("MoneyOrder.aspx");
            }

            GridBind();
            GetBank();
            MoneyRequest();
        }
    }
    private void GetBank()
    {
        StringBuilder sb = new StringBuilder();
        Bank bk = new Bank();
        DataTable dtbank = bk.GetBankByAgent(anID);
        foreach (DataRow dr in dtbank.Rows)
        {
            sb.AppendLine("<ul class='seachformx'>");
            sb.AppendLine("<li>");
            sb.AppendLine("<label style='text-align: left; width: 140px'>账号类型：" + dr["TypeName"].ToString() + "</label>");
            sb.AppendLine("<label style='text-align: left; width: 300px'>账号：" + dr["Zhanghao"].ToString() + "</label><br/>");
            sb.AppendLine("<label style='text-align: left; width: 140px'>户名：" + dr["Huming"].ToString() + "</label>");
            sb.AppendLine("<label style='text-align: left; width: 300px'>开户行：" + dr["Kaihu"].ToString() + "</label>");
            sb.AppendLine("</li>");
            sb.AppendLine("</ul>");
        }
        bankStr = sb.ToString();
    }

    private void MoneyRequest()
    {
        string sql = "select AllPrice from MoneyOrder where nID=" + nID;
        oprice = her.ExecuteScalar(sql) == DBNull.Value ? 0 : Convert.ToInt32(her.ExecuteScalar(sql).ToString());
        allprice = Price = StringDelHTML.PriceToStringLow(oprice);
    }
    /// <summary>
    /// 确认提现
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearh_Click(object sender, EventArgs e)
    {
        string sql = "update MoneyOrder set OrderState=1 where nID=" + nID;
        if (her.ExecuteNonQuery(sql))
        {
            string sql2 = string.Format(@"update ML_Order set MoneyState=2 where MoneyOrderID={0}", nID);
            if (her.ExecuteNonQuery(sql2))
            {
                string sql3 = string.Format(@"update ML_Member set AllMoney=AllMoney+{0} where nID={1}", oprice, anID);
                if (her.ExecuteNonQuery(sql3))
                {
                    Response.Redirect("MoneyOrder.aspx");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('确认失败，请联系管理员')</script>");
            }
        }
    }
    //绑定GridView
    public void GridBind()
    {
        string sql = @"
select a.*,b.MemberPhone,b.nickname,c.tRealName,e.StateName,f.name dlName from ML_Order a 
left join ML_Member b on a.OrderUserid=b.nID
left join ML_HomeMaking c on a.Auntid=c.nID
left join ML_OrderState e on a.OrderState=e.nID
left join DB_ApplyShop f on b.nID=f.userID  where a.MoneyOrderID=" + nID;

        DataTable dt = her.ExecuteDataTable(sql);
        Pagination2.MDataTable = dt;
        Pagination2.MGridView = gridField;
    }


    protected void gridField_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {

            Label lbprice = gridField.Rows[i].FindControl("lbprice") as Label;
            lbprice.Text = StringDelHTML.PriceToStringLow(Convert.ToInt32(lbprice.Text));

        }
    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gridField_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor=\"#F6F9FA\"");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=\"" + e.Row.Style["BACKGROUND-COLOR"] + "\"");
        }
    }
    protected void gridField_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("MoneyOrder.aspx");

    }


}