using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AdminManag_AdminEdit : System.Web.UI.Page
{
    AdminManagHelper adminhelper = new AdminManagHelper();
    string adnimID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        // if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            Initialization();
            // BindDrop(dropGroup);
            btnClose.Attributes["onclick"] = "vbscript:history.back()";
            if (Request.QueryString["id"] != "" && Request.QueryString["id"] != null)
            {
                btnClose.Visible = true;
                adnimID = Request.QueryString["id"].ToString();
                Interface(adnimID);
            }
        }
    }
    //界面初始化
    public void Initialization()
    {
        txtName.Value = "";
        txtRealName.Value = "";
        ckb.Checked = false;
        btnClose.Visible = false;
    }
    //绑定下拉菜单
    public void BindDrop(DropDownList drop)
    {
        SqlHelper her = new SqlHelper();
        string sql = "select nID,tClassName from [ML_AdminClass]";
        DataTable dt = her.ExecuteDataTable(sql);
        drop.DataSource = dt;
        drop.DataValueField = "nID";
        drop.DataTextField = "tClassName";
        drop.DataBind();
    }
    //根据ID查询信息并加载到界面
    public void Interface(string id)
    {
        DataTable dt = adminhelper.getMang(id);
        if (dt.Rows.Count > 0)
        {
            //dropGroup.SelectedValue = dt.Rows[0]["tClassID"].ToString();
            txtName.Value = dt.Rows[0]["tAdminName"].ToString();
            //txtPass.Attributes.Add("Value", dt.Rows[0]["tAdminPass"].ToString());
            //txtPassAgan.Attributes.Add("Value", dt.Rows[0]["tAdminPass"].ToString());
            txtPass.Text = dt.Rows[0]["tAdminPass"].ToString();
            txtPassAgan.Text = dt.Rows[0]["tAdminPass"].ToString();
            txtRealName.Value = dt.Rows[0]["tRealName"].ToString();
            if (dt.Rows[0]["oCheck"].ToString() == "True") { ckb.Checked = true; } else { ckb.Checked = false; }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('获取数据失败！')</script>");
        }
    }
    //添加事件
    public void SubmitAdd()
    {
        if (txtPass.Text.Trim() != txtPassAgan.Text.Trim())
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('两次密码不相同！')</script>");
            return;
        }
        if (adminhelper.Verification(txtName.Value.Trim()))
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加完成！')</script>");
            return;
        }

        int check = 0;
        if (ckb.Checked) { check = 1; }
        bool success = adminhelper.AdminInsert(txtName.Value.Trim(), EntityUtils.StringToMD5(txtPass.Text.Trim(), 16), "0", txtRealName.Value.Trim(), DateTime.Now.ToString(), check);
        if (success)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加完成！')</script>");
            //Initialization();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加失败！')</script>");
        }
    }
    //修改事件
    public void SubmitUpd(string ID)
    {
        int check = 0; bool success = false;
        if (ckb.Checked) { check = 1; }
        if (adminhelper.AdminSelPass(ID) == txtPass.Text.Trim())
        {
            success = adminhelper.AdminUpdate(ID, txtName.Value.Trim(), txtPass.Text.Trim(), "0", txtRealName.Value.Trim(), DateTime.Now.ToString(), check);
        }
        else
        {
            success = adminhelper.AdminUpdate(ID, txtName.Value.Trim(), EntityUtils.StringToMD5(txtPass.Text.Trim(), 16), "0", txtRealName.Value.Trim(), DateTime.Now.ToString(), check);
        }
        if (success)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改完成！')</script>");
            Response.Redirect("AdminMain.aspx?ceid=" + Request.QueryString["ceid"].ToString());
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改失败！')</script>");
        }
    }
    protected void btnSub_Click(object sender, EventArgs e)
    {
        if (txtName.Value == "" || txtName.Value == null) { return; }
        if (txtPass.Text == "" || txtPassAgan.Text == "" || txtPass.Text != txtPassAgan.Text) { return; }
        if (Request.QueryString["id"] == "" || Request.QueryString["id"] == null)
        {
            SubmitAdd();
        }
        else
        {
            SubmitUpd(Request.QueryString["id"].ToString());
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminMain.aspx");
    }
}