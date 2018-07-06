using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;

public partial class Admin_DaiLi_HomeMakByOne : System.Web.UI.Page
{
    public string MoneyAll;
    public string MoneyCan;
    public string MoneyNoCan;
    public string MoneyRequset;


    public string BindTitle;
    public string BindStr;
    private string nID
    {
        get
        {
            return ViewState["nID"].ToString();
        }
        set
        {
            ViewState["nID"] = value;
        }
    }

    private int BankCount
    {
        get
        {
            return Convert.ToInt32(ViewState["BankCount"]);
        }
        set
        {
            ViewState["BankCount"] = value;
        }
    }
    SqlHelper her = new SqlHelper();
    LoginVerification logVer = new LoginVerification();
    DaiLi makbll = new DaiLi();
    Bank bk = new Bank();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")//查看详细
            {
                btnQr.Visible = false;
                btnUpdate.Visible = false;
                nID = Request.QueryString["id"].ToString();
            }
            else
            {
                Button3.Visible = false;
                if (Session["anid"] == null) { Response.Redirect("../../Error.aspx"); } else { nID = Session["anid"].ToString(); }
            }

        }

        BindSource(nID);
        BingMoney(nID);
    }

    private void BingMoney(string nid)
    {
        MoneyAll = bk.GetMoneyAll(nid) == "" ? "0" : bk.GetMoneyAll(nid);
        MoneyCan = bk.GetMoneyOrder(nid, 0) == "" ? "0" : bk.GetMoneyOrder(nid, 0);
        MoneyNoCan = bk.GetMoneyOrderNoCan(nid) == "" ? "0" : bk.GetMoneyOrderNoCan(nid);
        MoneyRequset = bk.GetMoneyOrder(nid, 1) == "" ? "0" : bk.GetMoneyOrder(nid, 1);
    }
    private void BindSource(string nid)
    {
        string strState = string.Empty;
        DataTable dt = logVer.GetLoginBynID(nid);//账户信息
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string faterDLname = makbll.HomeMakName(dt.Rows[0]["nLogNum"].ToString());
            //代理归属
            sb.AppendLine("<hr><div class='misc-info'>");
            sb.AppendLine("<h3>代理归属</h3>");
            sb.AppendLine("<dl><dt>上级代理：</dt> <dd>" + faterDLname + "</dd></dl>");

            if (Request.QueryString["id"] != null && Request.QueryString["id"] != "" && Session["acid"].ToString() != "3")//不是总部查看详细
            {

            }
            else
            {
                sb.AppendLine("<dl>");
                sb.AppendLine("<dt>登录账号：</dt> <dd>" + dt.Rows[0]["HomeName"].ToString() + "</dd>");
                sb.AppendLine("<dt>登录密码：</dt> <dd>" + dt.Rows[0]["HomePass"].ToString() + "</dd>");
                sb.AppendLine("<dl>");
            }

            sb.AppendLine("<div class='clearfix'></div></div>");
            //基本信息
            sb.AppendLine("<hr><div class='misc-info'>");
            sb.AppendLine("<h3>基本信息</h3>");
            sb.AppendLine("<dl>");
            sb.AppendLine("<dt>个人/公司名称：</dt> <dd>" + dt.Rows[0]["tRealName"].ToString() + "</dd>");
            sb.AppendLine("<dt>联系人：</dt> <dd>" + dt.Rows[0]["HomeIntegral"].ToString() + "</dd>");
            sb.AppendLine("</dl>");
            sb.AppendLine("<dl>");
            sb.AppendLine("<dt>固定电话：</dt> <dd>" + dt.Rows[0]["HomePhone"].ToString() + "</dd>");
            sb.AppendLine("<dt>手机：</dt> <dd>" + dt.Rows[0]["HomeMobile"].ToString() + "</dd>");
            sb.AppendLine("</dl>");
            sb.AppendLine("<dl><dt>邮箱：</dt> <dd>" + dt.Rows[0]["HomeEmail"].ToString() + "</dd></dl>");
            sb.AppendLine("<div class='clearfix'></div></div>");



            //收款账号

            DataTable dtbank = bk.GetBankByAgent(nID);
            BankCount = dtbank.Rows.Count;
            foreach (DataRow dr in dtbank.Rows)
            {
                sb.AppendLine("<hr><div class='addr_and_note1'>");
                sb.AppendLine("<h3>收款账号</h3>");
                sb.AppendLine("<dl><dt>账号类型：</dt> <dd>" + dr["TypeName"].ToString() + "</dd></dl>");
                sb.AppendLine("<dl><dt>账号：</dt> <dd>" + dr["Zhanghao"].ToString() + "</dd></dl>");
                sb.AppendLine("<dl><dt>户名：</dt> <dd>" + dr["Huming"].ToString() + "</dd></dl>");
                sb.AppendLine("<dl><dt>开户行：</dt> <dd>" + dr["Kaihu"].ToString() + "</dd></dl>");
                sb.AppendLine("<div class='clearfix'></div></div>");
            }

            //商城地址   
            sb.AppendLine("<hr><div class='addr_and_note1'>");
            sb.AppendLine("<h3>商城地址</h3>");
            sb.AppendLine("<dl><dt>地址：</dt> <dd>" + dt.Rows[0]["CreatorName"].ToString() + "</dd></dl>");
            if (string.IsNullOrEmpty(dt.Rows[0]["HomePic"].ToString()))
            {
                sb.AppendLine("<dl><dt>二维码：</dt> <dd></dd></dl>");
            }
            else
            {
                sb.AppendLine("<dl><dt>二维码：</dt> <dd><img src='../Upload/pic/" + dt.Rows[0]["HomePic"].ToString() + "' /></dd></dl>");
            }
            sb.AppendLine("<div class='clearfix'></div></div>");


            //地址
            string addr = dt.Rows[0]["HomePro"].ToString() + " " + dt.Rows[0]["HomeCity"].ToString() + " " + dt.Rows[0]["HomeIntro"].ToString();
            sb.AppendLine("<hr><div class='misc-info'>");
            sb.AppendLine("<h3>详细地址</h3>");
            sb.AppendLine("<dl>");
            sb.AppendLine("<dt>邮编：</dt> <dd>" + dt.Rows[0]["CreatorCode"].ToString() + "</dd>");
            sb.AppendLine("<dt>地址：</dt> <dd>" + addr + "</dd>");
            sb.AppendLine("<dl>");
            sb.AppendLine("<div class='clearfix'></div></div>");



            //其他信息   
            sb.AppendLine("<hr><div class='addr_and_note1'>");
            sb.AppendLine("<h3>其他信息</h3>");
            if (string.IsNullOrEmpty(dt.Rows[0]["tPic"].ToString()))
            {
                sb.AppendLine("<dl><dt>相关证件：</dt> <dd></dd></dl>");
            }
            else
            {
                sb.AppendLine("<dl><dt>相关证件：</dt> <dd><img src='../Upload/" + dt.Rows[0]["tPic"].ToString() + "' /></dd></dl>");
            }
            sb.AppendLine("<dl><dt>其他：</dt><dd>" + dt.Rows[0]["tMemo"].ToString() + "</dd></dl>");
            sb.AppendLine("<div class='clearfix'></div></div>");


        }

        BindStr = sb.ToString();


    }




    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //BindSource(nID);
        Button3.Visible = true;
        divPwd.Visible = true;
        btnQr.Visible = true;
        btnUpdate.Visible = false;
    }
    /// <summary>
    /// 确认修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQr_Click(object sender, EventArgs e)
    {

        SqlParameter[] count = 
                {  
                    new SqlParameter("@HomePass",txtPwd.Text.Trim()),//密码
                     new SqlParameter("@nID",nID),//
                };
        string sql = "update ML_HomeMaking set HomePass=@HomePass where nID=@nID";
        if (her.ExecuteNonQuery(sql, count))
        {
            divPwd.Visible = false;
            btnQr.Visible = false;
            btnUpdate.Visible = true;
            Button3.Visible = false;
            BindSource(nID);
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改失败')</script>");
        }

    }
    /// <summary>
    /// 提现申请
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnMoney_Click(object sender, EventArgs e)
    {
        if (BankCount <= 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请先添加收款账号')</script>");
            return;
        }

        if (Convert.ToInt32(MoneyCan) <= 500)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('根据出账规则，每次可提现资金必须大于500元')</script>");
            return;
        }

        //添加提现申请记录
        if (bk.MoneyOrderInsert(nID, MoneyCan,"","",""))
        {
            string sqlMaxID = "select Max(nID) from MoneyOrder";
            string maxId = her.ExecuteScalar(sqlMaxID) == null ? "0" : her.ExecuteScalar(sqlMaxID).ToString();
            //申请提现
            string sql = string.Format(@"update ML_Order set MoneyState=1,MoneyOrderID={0} where Auntid={1} 
                                and MoneyState=0 and OrderState=4 and DelState=0  and OcType='微信支付' ", maxId, nID);
            if (her.ExecuteNonQuery(sql))
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('申请成功，我们会在1-3个工作日将资金打入您的账号中')</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('申请失败')</script>");
            }
        }

        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('申请失败')</script>");
        }


    }

}