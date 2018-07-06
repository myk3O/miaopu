using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Admin_Order_OrderDetail : System.Web.UI.Page
{
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
    SqlHelper her = new SqlHelper();
    Order or = new Order();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["anid"] == null)
        {
            Response.Redirect("../../Error.aspx");
        }

        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")
            {
                nID = Request.QueryString["id"].ToString();

            }
            else
            {
                Response.Redirect("../../Error.aspx");
            }

        }
        BindSource();

    }

    private void BindSource()
    {
        int orderstate = 0;
        string strState = string.Empty;
        DataTable dt = or.GetOrderOne(nID);//订单信息
        StringBuilder sb = new StringBuilder();
        if (dt.Rows.Count > 0)
        {
            orderstate = Convert.ToInt32(dt.Rows[0]["OrderState"]);
            string auntname = "佳奥";
            string auntphone = "";
            string strsql = "select * from DB_ApplyShop where userID=" + dt.Rows[0]["Auntid"].ToString();
            DataTable dtp = her.ExecuteDataTable(strsql);
            foreach (DataRow drr in dtp.Rows)
            {
                auntname = drr["name"] == null ? "" : drr["name"].ToString();
                auntphone = drr["phone"] == null ? "" : drr["phone"].ToString();
            }

            string sql = string.Format(@"  select b.proPrice,b.yongjin from ML_Order a 
  join ML_OrderProduct b on a.nID=b.orderID  
  where a.nID={0}", nID);
            double dd = 0.00;
            foreach (DataRow dr in her.ExecuteDataTable(sql).Rows)
            {
                dd += Convert.ToInt32(dr["proPrice"]) * Convert.ToInt32(dr["yongjin"]);
            }
            string yj = (dd / 10000).ToString("0.00");


            string allpri = StringDelHTML.PriceToStringLow(Convert.ToInt32(dt.Rows[0]["OrderPrice"].ToString()));
            //订单状态
            sb.AppendLine("<hr><div class='misc-info'>");
            sb.AppendLine("<h3>订单状态</h3>");
            sb.AppendLine("<dl><dt>订单状态：</dt> <dd>" + dt.Rows[0]["StateName"].ToString() + "</dd>");
            sb.AppendLine("<dt>支付方式：</dt> <dd>" + dt.Rows[0]["OcType"].ToString() + "</dd></dl>");
            sb.AppendLine("<div class='clearfix'></div></div>");
            //订单信息
            sb.AppendLine("<hr><div class='misc-info'>");
            sb.AppendLine("<h3>订单信息</h3>");
            sb.AppendLine("<dl>");
            sb.AppendLine("<dt>订单归属：</dt> <dd>" + auntname + "</dd>");
            sb.AppendLine("<dt>归属人电话：</dt> <dd>" + auntphone + "</dd>");
            sb.AppendLine("</dl>");
            sb.AppendLine("<dl>");
            sb.AppendLine("<dt>订单编号：</dt> <dd>" + dt.Rows[0]["OrderCode"].ToString() + "</dd>");
            sb.AppendLine("<dt>下单时间：</dt> <dd>" + dt.Rows[0]["CreateTime"].ToString() + "</dd>");
            sb.AppendLine("</dl>");

            sb.AppendLine("<dl>");
            sb.AppendLine("<dt>收货联系人：</dt> <dd>" + dt.Rows[0]["Lianxiren"].ToString() + "</dd>");
            sb.AppendLine("<dt>收货人电话：</dt> <dd>" + dt.Rows[0]["PhoneNumber"].ToString() + "</dd>");
            sb.AppendLine("</dl>");
            sb.AppendLine("<dl><dt>用户留言：</dt> <dd>" + dt.Rows[0]["tComment"].ToString() + "</dd></dl>");

            sb.AppendLine("<dl>");
            sb.AppendLine("<dt>订单总价（元）：</dt> <dd>" + allpri + "</dd>");
            sb.AppendLine("<dt>可获佣金（元）：</dt> <dd>" + yj + "</dd>");
            sb.AppendLine("</dl>");

            sb.AppendLine("<div class='clearfix'></div></div>");

            //收货地址
            sb.AppendLine("<hr><div class='addr_and_note'>");
            sb.AppendLine("<dl><dt>收货地址：</dt> <dd>" + dt.Rows[0]["OspecialStr"].ToString() + "， " + dt.Rows[0]["Address"].ToString() + "</dd></dl>");
            sb.AppendLine("</dl></div>");
            //收款账号

            //DataTable dtbank = or.GetBankOne(dt.Rows[0]["Auntid"].ToString());
            //foreach (DataRow dr in dtbank.Rows)
            //{
            //    sb.AppendLine("<hr><div class='addr_and_note1'>");
            //    sb.AppendLine("<h3>收款账号</h3>");
            //    sb.AppendLine("<dl><dt>账号类型：</dt> <dd>" + dr["TypeName"].ToString() + "</dd></dl>");
            //    sb.AppendLine("<dl><dt>账号：</dt> <dd>" + dr["Zhanghao"].ToString() + "</dd></dl>");
            //    sb.AppendLine("<dl><dt>户名：</dt> <dd>" + dr["Huming"].ToString() + "</dd></dl>");
            //    sb.AppendLine("<dl><dt>开户行：</dt> <dd>" + dr["Kaihu"].ToString() + "</dd></dl>");
            //    sb.AppendLine("<div class='clearfix'></div></div>");
            //}

            //订单内所有产品
            sb.AppendLine("<hr><div class='addr_and_note1'>");
            sb.AppendLine("<h3>订单内具体产品</h3>");
            string sqlPro = @" select a.*,b.SkuName,b.Price,c.nID as PID,c.tTitle,c.tPic from ML_OrderProduct a 
                          left join ML_Product b on a.ProID=b.nID
                          join  ML_ServiceArea c on b.cid0=c.nId where a.orderID=" + nID;
            DataTable dtpro = her.ExecuteDataTable(sqlPro);
            foreach (DataRow dr in dtpro.Rows)
            {
                string[] arrpic = dr["tPic"].ToString().Split(';');
                string TopImg = arrpic[0].ToString();
                string pri = StringDelHTML.PriceToStringLow(Convert.ToInt32(dr["Price"]));
                string prism = StringDelHTML.PriceToStringLow(Convert.ToInt32(dr["proPrice"]));
                sb.AppendLine("<dl><dt>产品名称：</dt> <dd>" + dr["tTitle"].ToString() + "</dd></dl>");
                sb.AppendLine("<dl><dt>购买数量：</dt> <dd>" + dr["probyCount"].ToString() + "</dd></dl>");
                sb.AppendLine("<dl><dt>小计：</dt> <dd>" + prism + "</dd></dl>");
                sb.AppendLine("<dl><dt>规格属性：</dt> <dd>" + dr["SkuName"].ToString() + "</dd></dl>");
                sb.AppendLine("<dl><dt>产品图片：</dt> <dd><img width='100px' src='../../upload_Img/" + TopImg + "' /></dd></dl>");
            }
            sb.AppendLine("<div class='clearfix'></div></div>");


            //if (Convert.ToInt32(dt.Rows[0]["OrderState"]) > 4)
            //{
            //    //退款说明   <hr>
            //    sb.AppendLine("<hr><div class='addr_and_note1'>");
            //    sb.AppendLine("<h3>退货申请</h3>");
            //    sb.AppendLine("<dl><dt>退货数量：</dt> <dd>" + dt.Rows[0]["OcID"].ToString() + "</dd></dl>");
            //    sb.AppendLine("<dl><dt>退货原因：</dt> <dd>" + dt.Rows[0]["tComment"].ToString() + "</dd></dl>");
            //    sb.AppendLine("<div class='clearfix'></div></div>");
            //}

            if (orderstate >= 3)//待收货
            {
                //快递信息
                sb.AppendLine("<hr><div class='misc-info'>");
                sb.AppendLine("<h3>快递信息</h3>");
                sb.AppendLine("<dl>");
                sb.AppendLine("<dt>快递名称：</dt> <dd>" + dt.Rows[0]["worktime"].ToString() + "</dd>");
                sb.AppendLine("<dt>快递单号：</dt> <dd>" + dt.Rows[0]["workComment"].ToString() + "</dd>");
                sb.AppendLine("</dl>");
                sb.AppendLine("<div class='clearfix'></div></div>");
            }
            if (orderstate == 5 || orderstate == 7 || orderstate == 9)//退货
            {
                string sqlth = "select * from ML_TuiHuo where orderID= " + nID;
                DataTable dtdt = her.ExecuteDataTable(sqlth);
                if (dtdt.Rows.Count > 0)
                {
                    //退货信息
                    sb.AppendLine("<hr><div class='misc-info'>");
                    sb.AppendLine("<h3>寄回快递信息</h3>");
                    sb.AppendLine("<dl>");
                    sb.AppendLine("<dt>快递名称：</dt> <dd>" + dtdt.Rows[0]["wName"].ToString() + "</dd>");
                    sb.AppendLine("<dt>快递单号：</dt> <dd>" + dtdt.Rows[0]["wCode"].ToString() + "</dd>");
                    sb.AppendLine("</dl>");
                    sb.AppendLine("<dl>");
                    sb.AppendLine("<dt>退货原因：</dt> <dd>" + dtdt.Rows[0]["reason"].ToString() + "</dd>");
                    sb.AppendLine("<dt>其他说明：</dt> <dd>" + dtdt.Rows[0]["mark"].ToString() + "</dd>");
                    sb.AppendLine("</dl>");
                    sb.AppendLine("<div class='clearfix'></div></div>");
                }
            }


            if (Convert.ToInt32(dt.Rows[0]["OrderState"]) == 2)
            {
                btnQr.Visible = true;
                divKuaiDi.Visible = true;
            }
            else
            {
                btnQr.Visible = false;
                divKuaiDi.Visible = false;
            }

            if (Convert.ToInt32(dt.Rows[0]["OrderState"]) == 5)
            {
                btnJJ.Visible = true;
            }
            else
            {
                btnJJ.Visible = false;
            }

            if (Convert.ToInt32(dt.Rows[0]["OrderState"]) == 7)
            {
                btnTHQ.Visible = true;
                btnTHJ.Visible = true;
            }
            else
            {
                btnTHQ.Visible = false;
                btnTHJ.Visible = false;
            }

        }

        BindStr = sb.ToString();



        strState = dt.Rows[0]["StateName"].ToString();


    }

    /// <summary>
    /// 确认发货
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQr_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtKdCode.Text.Trim()))
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('快递单号不能为空')</script>");
            return;
        }
        if (or.OrderKuaiDi(nID, dpkuaidi.SelectedItem.Text, txtKdCode.Text.Trim()))
        {

            bool success = or.updateOrderFH(nID, 3);
            if (!success)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误！请重试')</script>");
            }
            else
            {
                //imgdelete.Attributes.Add("onclick", "return confirm('请谨慎操作，你确认删除本记录?执行本操作将是不可逆的!')");
                // Button3.Attributes.Add("onclick", "javascript:history.go(-2);return false;");
                //ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('提交成功')</script>");
                //Response.Write("<script>window.location.reload();</script>");
                // ClientScript.RegisterStartupScript(GetType(), "", "<script>window.location.reload();</script>");
                // AjaxAlert.AlertAndRedirect(this,"发货成功","");
                BindSource();
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误！请重试')</script>");
        }
    }

    /// <summary>
    /// 确认退货
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnJJ_Click(object sender, EventArgs e)
    {
        bool success = or.DelOrder(nID, 7);
        if (!success)
        {
            or.ChengKuCun(nID, true);//还原库存
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误！请重试')</script>");
        }
        else
        {
            BindSource();
        }
    }

    /// <summary>
    /// 确认退款，关闭交易
    /// </summary>
    /// <param name="sender"></param>
    /// <param7 name="e"></param>
    protected void btnTHQ_Click(object sender, EventArgs e)
    {
        bool success = or.DelOrder(nID, 9);
        if (!success)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误！请重试')</script>");
        }
        else
        {
            BindSource();
        }
    }

    /// <summary>
    /// 拒绝退货/退款
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnTHJ_Click(object sender, EventArgs e)
    {
        bool success = or.DelOrder(nID, 10);
        if (!success)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误！请重试')</script>");
        }
        else
        {
            BindSource();
        }

    }



}