using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maliang;
public partial class Admin_Order_MoneyOrder : System.Web.UI.Page
{
    public string tixian;
    private string uid
    {
        get
        {
            return ViewState["uid"] == null ? "" : ViewState["uid"].ToString();
        }
        set
        {
            ViewState["uid"] = value;
        }
    }
    SqlHelper her = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }

        }
        GetLeijiTixian();
        GridBind();
    }
    //绑定GridView
    public void GridBind()
    {
        string sql = @"  select a.*,c.*,c.Huming ckr,b.nickname,b.tRealName,b.MemberPhone,b.headimgurl from [MoneyOrder] a 
                    join ML_Member b on a.AnID=b.nID left join [Bank] c on a.AnID=c.Cid  where 1=1 ";

        if (!string.IsNullOrEmpty(nickname.Value.Trim()))
        {
            sql += " and b.nickname like '%" + nickname.Value.Trim() + "%'";
        }
        if (!string.IsNullOrEmpty(realname.Value.Trim()))
        {
            sql += " and b.tRealName like '%" + realname.Value.Trim() + "%'";
        }

        if (Convert.ToInt32(DropState.SelectedItem.Value) >= 0)
        {
            sql += " and a.OrderState =" + DropState.SelectedItem.Value + " ";
        }

        if (!string.IsNullOrEmpty(tBegin.Value.Trim()))
        {
            sql += " and a.CreateTime >='" + tBegin.Value.Trim() + "'";
        }

        if (!string.IsNullOrEmpty(tEnd.Value.Trim()))
        {
            sql += " and a.CreateTime <='" + tEnd.Value.Trim() + "'";
        }
        sql += " order by a.CreateTime desc";
        DataTable dt = her.ExecuteDataTable(sql);
        Pagination2.MDataTable = dt;
        Pagination2.MGridView = gridField;
    }
    /// <summary>
    /// 技术累计打款
    /// </summary>
    private void GetLeijiTixian()
    {
        string sql = "  select ISNULL( SUM(AllPrice),0) from MoneyOrder where OrderState=1";
        tixian = StringDelHTML.PriceToStringLow(Convert.ToInt32(her.ExecuteScalar(sql)));
    }

    protected void btnSearh_Click(object sender, EventArgs e)
    {
        GridBind();
        Pagination2.Refresh();
    }
    protected void gridField_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {

            Label lbuptime = gridField.Rows[i].FindControl("lbuptime") as Label;


            Label lbprice = gridField.Rows[i].FindControl("lbprice") as Label;
            lbprice.Text = StringDelHTML.PriceToStringLow(Convert.ToInt32(lbprice.Text));

            Label lblName = gridField.Rows[i].FindControl("lbstate") as Label;
            Button btnUpdate = gridField.Rows[i].FindControl("btnUpdate") as Button;

            if (lblName.Text == "False")
            {
                lblName.Text = "未处理";
                btnUpdate.Visible = true;
                lbuptime.Text = "";
            }
            else
            {
                lblName.Text = "已处理";
                btnUpdate.Visible = false;
                //lbuptime.Text = lbuptime.Text;
            }
        }
    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "detail")
        {
            //写个触发器比较好
            string sql1 = string.Format(@"update  MoneyOrder set OrderState=1,UpdateTime='{0}'
                            where nID={1}", System.DateTime.Now.ToString(), e.CommandArgument.ToString());

            string sql = "select AnID,Tixian from [MoneyOrder] where nID=" + e.CommandArgument.ToString();
            DataTable dt = her.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                string sql2 = string.Format("update ML_Member set AllMoney = AllMoney+{0} where nID={1} "
                , Convert.ToInt32(dt.Rows[0]["Tixian"]), Convert.ToInt32(dt.Rows[0]["AnID"]));
                if (her.ExecuteNonQuery(sql2))//先转账，再去修改订单状态。免得转账失败了，但是订单却修改成功了
                {
                    if (her.ExecuteNonQuery(sql1))
                    {
                        GridBind();
                        Pagination2.Refresh();
                        ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改成功')</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改失败，请联系管理员')</script>");
                    }

                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('转账失败，请联系管理员')</script>");

                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('数据查询失败了，请联系管理员')</script>");
            }


            // int rowIndex = ((GridViewRow)((Button)e.CommandSource).NamingContainer).RowIndex;
            // Label lb = (Label)gridField.Rows[rowIndex].FindControl("lbprice");
            //ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + lb.Text + "')</script>");



        }


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
    protected void btn_Expot_Click(object sender, EventArgs e)
    {
        string sql = @"select a.nID, (CONVERT(float,a.Allprice)/100) ordermoney,c.TypeName,c.Zhanghao,c.Huming,c.[address],
                     b.MemberPhone ,'' nullvalue,b.nickname,b.MemberCode,d.chongfu from  MoneyOrder a 
                     join  ML_Member b on a.AnID=b.nID left join [Bank] c on a.AnID=c.Cid 
                     left join (
 
	                     select cc.Zhanghao chongfu from  MoneyOrder aa join  ML_Member bb on aa.AnID=bb.nID
	                    left join [Bank] cc on aa.AnID=cc.Cid where aa.OrderState=0
	                     group by cc.zhanghao having count(cc.zhanghao)>1
 
                     ) d on c.Zhanghao=d.chongfu
                      where a.OrderState=0 order by c.Huming ";

        DataTable dt = her.ExecuteDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            GridViewExpot.DataSource = dt;
            GridViewExpot.DataBind();
            string day = System.DateTime.Now.ToString("yyyy-MM-dd");
            //string _Name = her.ExecuteScalar(sql).ToString();
            PageHelper.DataTableToExcel(this.GridViewExpot, day + "提现.xls");
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('无未处理数据可导出')</script>");
        }
    }


    /// <summary>
    /// 重写了这个，什么都不用干，就能解决导出时 类型“GridView”的控件 必须放在具有 runat。。。的错误
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}