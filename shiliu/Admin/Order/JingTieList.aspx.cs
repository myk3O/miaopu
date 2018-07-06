using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maliang;
using System.Data;


public partial class Admin_Order_JingTieList : System.Web.UI.Page
{
    public string allJinTie = "";
    SqlHelper her = new SqlHelper();
    MemberHelper Menber = new MemberHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        //imgdelete.Attributes.Add("onclick", "return confirm('请谨慎操作，你确认删除本记录?执行本操作将是不可逆的!')");
        if (!IsPostBack)
        {
            BindDrop(drpUserLevel);
            drpUserLevel.Items.Insert(0, new ListItem("请选择", "-1", true));
            if (Request.QueryString["ceid"] != null && Request.QueryString["ceid"].ToString() != "")
            {
                hid.Value = Request.QueryString["ceid"].ToString();
            }

        }
        GridBind();
        if (hid.Value != "")
        {
            gridField.PageIndex = int.Parse(hid.Value);
            hid.Value = "";
        }
    }
    //绑定下拉菜单
    public void BindDrop(DropDownList drop)
    {
        string sql = "select nID,levelName from ML_MemberLevel";
        DataTable dt = her.ExecuteDataTable(sql);
        drop.DataSource = dt;
        drop.DataValueField = "nID";
        drop.DataTextField = "levelName";
        drop.DataBind();
    }

    public DataTable GetSource()
    {
        string sql = @"   select a.nID, a.nickname,a.tRealName,a.headimgurl, b.levelName,
                  c.allpri,c.price,c.part,c.CreateTime,d.nickname ncn,d.headimgurl hiu from  ML_Member a 
                  left join ML_MemberLevel b on a.fxslevel=b.nID
                  join T_UserJiXiao c on a.nID=c.bid join ML_Member d on c.uid=d.nID  where 1=1";
        if (nickName.Text.Trim() != "")
        {
            sql += " and a.nickname like '%" + nickName.Text.Trim() + "%'";
        }
        if (txtname.Text.Trim() != "")
        {
            sql += " and a.tRealName like '%" + txtname.Text.Trim() + "%'";
        }
        //if (DropisJXS.SelectedItem.Value != "-1")
        //{
        //    sql += " and isJXS=" + DropisJXS.SelectedItem.Value + " ";
        //}
        if (drpUserLevel.SelectedItem.Value != "-1")
        {
            sql += " and a.fxslevel=" + drpUserLevel.SelectedItem.Value + "";
        }

        if (Request.QueryString["uid"] != null && Request.QueryString["uid"].ToString() != "")
        {
            sql += " and a.FatherFXSID=" + Request.QueryString["uid"].ToString() + " ";

        }
        if (!string.IsNullOrEmpty(tBegin.Value.Trim()))
        {
            sql += " and c.CreateTime >='" + tBegin.Value.Trim() + "'";
        }

        if (!string.IsNullOrEmpty(tEnd.Value.Trim()))
        {
            sql += " and c.CreateTime <='" + tEnd.Value.Trim() + "'";
        }

        sql += " order by c.CreateTime desc";
        DataTable dt = her.ExecuteDataTable(sql);
        return dt;
    }
    public void GridBind()
    {
        Pagination1.MDataTable = GetSource();
        Pagination1.MGridView = gridField;
    }
    protected void imgdelete_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            CheckBox ckb = (CheckBox)gridField.Rows[i].FindControl("CheckSel");
            if (ckb.Checked)
            {
                bool success = Menber.MemberDelete(gridField.DataKeys[i].Value.ToString());
                if (!success)
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误！请重试')</script>");
                }
            }
        }
        GridBind();
        Pagination1.Refresh();
    }

    protected void btnSearh_Click(object sender, EventArgs e)
    {
        GridBind();
        Pagination1.Refresh();
    }

    protected void gridField_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor=\"#F6F9FA\"");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=\"" + e.Row.Style["BACKGROUND-COLOR"] + "\"");
        }
    }

    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "apply")
        {
            string sql = "  select sum(price) from T_UserJiXiao where bid= " + e.CommandArgument.ToString();
            int pri = her.ExecuteScalar(sql) == null ? 0 : Convert.ToInt32(her.ExecuteScalar(sql));

            string sql2 = " select tRealName from ML_Member where nID=" + e.CommandArgument.ToString(); ;

            allJinTie = her.ExecuteScalar(sql2) + "_累计津贴：" + StringDelHTML.PriceToStringLow(pri);
            // Response.Redirect("../Members/MyBranchMain.aspx?uid=" +  + "&&ceid=" + gridField.PageIndex);
        }

    }

    protected void gridField_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            Label lbSex = (Label)gridField.Rows[i].FindControl("lbSex");
            lbSex.Text = StringDelHTML.PriceToStringLow(Convert.ToInt32(lbSex.Text));

            Label lbPhone = (Label)gridField.Rows[i].FindControl("lbPhone");

            lbPhone.Text = StringDelHTML.PriceToStringLow(Convert.ToInt32(lbPhone.Text));


            Label lbMemberPass = (Label)gridField.Rows[i].FindControl("lbMemberPass");

            lbMemberPass.Text = (Convert.ToDouble(lbMemberPass.Text)) * 100 + "%";


            //Label lbisJXS = (Label)gridField.Rows[i].FindControl("lbisJXS");
            //if (lbisJXS.Text.ToLower() == "true")
            //{
            //    lbisJXS.Text = "是";
            //}
            //else
            //{
            //    lbisJXS.Text = "否";
            //}
        }
    }
}